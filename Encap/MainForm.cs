using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.ToolBlock;
using CsvHelper;
using DataAssemble;
using MvCamCtrl.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlHelper;
using BX_SetIPParm_Dll_space;
using BX_struct_space;
using 多通道数字电源_Dll_Space;
using Newtonsoft.Json;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using Cognex.VisionPro.CalibFix;
using Bing.Serialization;
using Cognex.VisionPro.ImageProcessing;
using 高精度数字电源;


namespace Encap
{

    public partial class MainForm : Form
    {

        #region 变量
        //远心0.011979，CCTV0.011586---像素面积0.0001435  直径0.5mm时面积0.19635 像素个数为：
        double scaleValue = 0.5 / 0.011979;
        int RecordIndex = 1;
        //服务器
        //public static TServer tServer;
        MSMQ mqRet = new MSMQ("result", "cmd");
        MSMQ mqOft = new MSMQ("offset", "cmd");
        MSMQ mqCType = new MSMQ("cTypeResult", "cType");
        //变量
        public enum RunMode { Auto = 0, Debug };
        RunMode runMode;
        public bool InitDone;
        public static string SN;
        public static int index;//0-QRCode,1--21Product,
        public CogToolBlock OffsetTool;
        public CogToolBlock ProductQRTool;
        public CogToolBlock wholePositionTool;
        public CalibFile calibFile;
        public List<CogToolBlock> visionTools;
        public ProductInfo currProductInfo;

        public static List<Fai> AllFai;

        //路径
        string Dir_pictureName = "";
        string Dir_ngPictureName = "";
        string Dir_imageName = "";
        string Dir_ngImageName = "";
        string DataFileDir = "";

        //文件
        private string pictureName;
        private string ngPictureName;
        private string imageName;
        private string ngImageName;

        #endregion

        #region 窗体事件
        public MainForm()
        {
            InitializeComponent();

            runMode = RunMode.Auto;
            InitDone = false;
            m_bGrabbing = false;

            pictureName = "";
            ngPictureName = "";
            imageName = "";
            ngImageName = "";

        }

        /// <summary>
        /// FormLoading事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProductInfo();
            LoadOutputConfig();
            LoadImgSettingConfig();
            ControlsStatus_WhenFormLoad();
            InitControlsDisplay();
            InitRuningCamrea();
            Log("[MainForm_Load]", "加载配置文件完成", LogLevel.Info);
            //CreatServe();
            InitLight();

            mqRet.ClrRcvQueue();
            mqRet.OnMsmqRcvedEvent += Mq_OnMsmqRcvedEvent;
            mqOft.ClrRcvQueue();
            mqOft.OnMsmqRcvedEvent += MqOft_OnMsmqRcvedEvent;
            mqCType.ClrRcvQueue();
            mqCType.OnMsmqRcvedEvent += MqCType_OnMsmqRcvedEvent;
            InitDone = true;

            InitVisionTools();
            GC.Collect();
        }

        private void MqCType_OnMsmqRcvedEvent(string msg)//msg=="Type1",msg=="Type2",msg=="Type3",...
        {
            try
            {
                Log("[MqCType_OnMsmqRcvedEvent]", "收到CMD：" + msg, LogLevel.Info);
                if (msg == currProductInfo.Product_name)
                {
                    Log("[MqCType_OnMsmqRcvedEvent]", "收到换型CMD：" + msg + ",与当前型号一致，无需切换视觉任务。", LogLevel.Info);
                    mqCType.SendMsg("OK");
                    Log("[MqCType_OnMsmqRcvedEvent]", "发送CMD：OK", LogLevel.Info);
                }
                else
                {
                    for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
                    {
                        if (msg == ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name)
                        {
                            Log("[MqCType_OnMsmqRcvedEvent]", "开始切换" + msg + "类型视觉任务，请等待。", LogLevel.Info);
                            currProductInfo = ALL_PRODUCT_TYPE.ProductInfoList[i];
                            mqCType.SendMsg("OK");
                            Log("[MqCType_OnMsmqRcvedEvent]", "发送CMD：OK", LogLevel.Info);
                            Application.Restart();
                            break;
                        }
                    }
                    mqCType.SendMsg("NG");
                    Log("[MqCType_OnMsmqRcvedEvent]", "发送CMD：NG", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                mqCType.SendMsg("NG");
                Log("[MqCType_OnMsmqRcvedEvent]", "换型失败，发送CMD：NG" + ex.Message, LogLevel.Error);
                MessageBox.Show("换型失败！" + ex.Message);
            }
        }

        private void MqOft_OnMsmqRcvedEvent(string msg)
        {
            //throw new NotImplementedException();
        }

        private void Mq_OnMsmqRcvedEvent(string msg)
        {
            try
            {
                Log("[ServerUpdateData]", "收到CMD：" + msg, LogLevel.Info);
                string[] revCMD = msg.Split(',');
                if (msg.Contains("Start"))
                {
                    GC.Collect();
                    runMode = RunMode.Auto;
                    Log("[Mq_OnMsmqRcvedEvent]", "连续触发已开始", LogLevel.Info);
                }
                else if (msg.Contains("End"))
                {
                    Log("[Mq_OnMsmqRcvedEvent]", "连续触发已结束", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log("[Mq_OnMsmqRcvedEvent]", ex.Message, LogLevel.Error);
            }
        }

        /// <summary>
        /// 初始化控件显示
        /// </summary>
        private void InitControlsDisplay()
        {
            for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
            {
                cmb_SltPro.Items.Add(ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name);
            }

            try
            {
                X_Nominal.Text = currProductInfo.Limitness.X_Nomial.ToString("0.000");
                Y_Nominal.Text = currProductInfo.Limitness.Y_Nominal.ToString("0.000");

                numX_USL.Text = currProductInfo.Limitness.X_USL.ToString("0.000");
                numX_LSL.Text = currProductInfo.Limitness.X_LSL.ToString("0.000");
                numY_USL.Text = currProductInfo.Limitness.Y_USL.ToString("0.000");
                numY_LSL.Text = currProductInfo.Limitness.Y_LSL.ToString("0.000");

                X_Tol_Plus.Text = currProductInfo.Limitness.X_Tol_Plus.ToString("0.000");
                X_Tol_Minus.Text = currProductInfo.Limitness.X_Tol_Minus.ToString("0.000");
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Y_Tol_Plus.Text = currProductInfo.Limitness.Y_Tol_Plus.ToString("0.000");
                Y_Tol_Minus.Text = currProductInfo.Limitness.Y_Tol_Minus.ToString("0.000");
            }
            catch (Exception ex)
            {
                Log("[set_lightPar_Click]", ex.Message, LogLevel.Error);
            }

            cmb_Record.SelectedIndex = 3;

            cmb_SltPro.SelectedItem = currProductInfo.Product_name;
            txt_Type.Text = currProductInfo.Product_name;
            cmb_SltPro.Visible = false;
            ChangeTypeCf.Visible = false;
            labelAll.Visible = false;

            bnStartGrab.Enabled = false;
            bnTriggerMode.Enabled = false;
            bnTriggerMode.Enabled = false;
            cbSoftTrigger.Enabled = false;
            bn_Triger.Enabled = false;

            try
            {
                List<NumericUpDown> numUDCH = new List<NumericUpDown>(4) { unmUDCH1, unmUDCH2, unmUDCH3, unmUDCH4 };
                for (int i = 0; i < 4; i++)
                {
                    numUDCH[i].Minimum = currProductInfo.HGSlights.Used ? currProductInfo.HGSlights.MinValue : currProductInfo.CSTlights.MinValue;
                    numUDCH[i].Maximum = currProductInfo.HGSlights.Used ? currProductInfo.HGSlights.MaxValue : currProductInfo.CSTlights.MaxValue;
                }
            }
            catch (Exception ex)
            {
                Log("[set_lightPar_Click]", ex.Message, LogLevel.Error);
                MessageBox.Show("设置HGS失败:" + ex.Message);
            }

            txtIP.Text = currImageSaveSetting.IPAdr;
            txtPort.Text = currImageSaveSetting.Port;

            //InitDgv();
        }

        /// <summary>
        /// 关闭软件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            DialogResult RESULT = MessageBox.Show("退出程序？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (RESULT == DialogResult.OK)
            {
                try
                {
                    ALL_PRODUCT_TYPE.SelectedProduct = currProductInfo.Product_name;
                    XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                    Log("[FormClosingSaveCurrProduct]", "保存当前产品型号至本地", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    Log("[FormClosingSaveCurrProduct]", ex.Message, LogLevel.Error);
                }
                CloseCamera();
                GC.Collect();
                Application.ExitThread();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
            */
            //以下自动关机时不弹窗提示
            try
            {
                ALL_PRODUCT_TYPE.SelectedProduct = currProductInfo.Product_name;
                XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                Log("[FormClosingSaveCurrProduct]", "保存当前产品型号至本地", LogLevel.Info);
            }
            catch (Exception ex)
            {
                Log("[FormClosingSaveCurrProduct]", ex.Message, LogLevel.Error);
            }
            CloseCamera();
            GC.Collect();
        }

        /// Tab切换事件
        private void Main_Tab_Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Main_Tab_Control.SelectedTab.Name == "tp任务编辑" || Main_Tab_Control.SelectedTab.Name == "tp光学调试")
            {
                form_confirm form_confirm1 = new form_confirm("非开发工程师请谨慎操作！");
                if (DialogResult.Yes == form_confirm1.ShowDialog()) { }
                else
                {
                    Main_Tab_Control.SelectedIndex = 0;
                }
            }

            if (Main_Tab_Control.SelectedTab.Name == "tp测试")
            {
                form_confirm form_confirm1 = new form_confirm("非开发工程师禁止操作！");
                if (DialogResult.Yes == form_confirm1.ShowDialog()) { }
                else
                {
                    Main_Tab_Control.SelectedIndex = 0;
                }
            }
        }

        #endregion

        #region 通讯

        /// 创建服务器
        public void CreatServe()
        {

        }


        /// 通讯MSG更新
        private void ServerUpdateMSG(object sender)
        {
            string msg = sender as string;
            Log("[ServerUpdateMSG]", msg, LogLevel.Info);
        }


        /// 通讯报错
        public void ServerUpdateErrorMSG(object sender)
        {
            string msg = sender as string;
            Log("[ServerUpdateErrorMSG]", msg, LogLevel.Error);
        }

        object obj = new object();
        /// 接收上位机数据
        public void ServerUpdateData(object sender)
        {
            lock (obj)
            {
                #region 运行交互
                try
                {
                    string msg = sender as string;
                    Log("[ServerUpdateData]", "收到CMD：" + msg, LogLevel.Info);
                    string[] revCMD = msg.Split(',');
                    if (msg.Contains("Start"))
                    {
                        GC.Collect();
                        runMode = RunMode.Auto;
                        //tServer.SendToClient(currImageSaveSetting.IPAdr, "Start");
                        Log("[ServerUpdateData]", "连续触发已开始", LogLevel.Info);
                    }
                    else if (msg.Contains("End"))
                    {

                        Log("[ServerUpdateData]", "连续触发已结束", LogLevel.Info);
                    }
                }
                catch (Exception ex)
                {
                    Log("[ServerUpdateData]", ex.Message, LogLevel.Error);
                }
                #endregion
            }
        }
        #endregion

        #region 加载系统配置

        bool b = false;
        //1.加载产品信息配置及视觉任务
        string ProductInfoPath = @"D:\Lead2DParameter\Config\ALL_PRODUCT_TYPE.xml";
        ALL_PRODUCT_TYPE ALL_PRODUCT_TYPE = new ALL_PRODUCT_TYPE("EncapBig");

        /// <summary>
        /// 加载产品配置信息
        /// </summary>
        public void LoadProductInfo()
        {
            try
            {
                if (!File.Exists(ProductInfoPath))
                {
                    Log("[LoadALL_PRODUCT_TYPE]", "ALL_PRODUCT_TYPE.xml文件丢失，尝试重建", LogLevel.Error);
                    ProductInfo newProductInfo = new ProductInfo("EncapBig");
                    newProductInfo.cameraInfo.Description = "EncapCameraSetting";
                    newProductInfo.cameraInfo.TriggerMode = TriggerMode.On;
                    newProductInfo.cameraInfo.Gain = 1;
                    newProductInfo.cameraInfo.Exposure = 145;

                    ALL_PRODUCT_TYPE.ProductInfoList = new List<ProductInfo>();
                    ALL_PRODUCT_TYPE.ProductInfoList.Add(newProductInfo);
                    XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                    ALL_PRODUCT_TYPE = (ALL_PRODUCT_TYPE)XmlSerializerHelper.ReadXML(ProductInfoPath, typeof(ALL_PRODUCT_TYPE), out b);
                    for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
                    {
                        if (ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name == ALL_PRODUCT_TYPE.SelectedProduct)
                        {
                            currProductInfo = ALL_PRODUCT_TYPE.ProductInfoList[i];
                        }
                    }
                }
                else
                {
                    ALL_PRODUCT_TYPE = (ALL_PRODUCT_TYPE)XmlSerializerHelper.ReadXML(ProductInfoPath, typeof(ALL_PRODUCT_TYPE), out b);
                    for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
                    {
                        if (ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name == ALL_PRODUCT_TYPE.SelectedProduct)
                        {
                            currProductInfo = ALL_PRODUCT_TYPE.ProductInfoList[i];
                        }
                    }
                    Log("[LoadProductInfo]", "ALL_PRODUCT_TYPE.xml文件读取成功", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log("[LoadProductInfo]", ex.Message, LogLevel.Error);
            }
        }

        //加载输出配置
        AllProductOutputConfig allProductOutputConfig = new AllProductOutputConfig(6);
        OutputConfig currOutputConfig = new OutputConfig("EncapNA", 3);
        string OutputConfigPath = @"D:\Lead2DParameter\Config\OutputConfig.xml";
        public void LoadOutputConfig()
        {
            try
            {
                if (!File.Exists(OutputConfigPath))
                {
                    Log("[OutputConfig]", "OutputConfig.xml文件丢失，尝试重建", LogLevel.Error);

                    XmlSerializerHelper.WriteXML((Object)allProductOutputConfig, OutputConfigPath, typeof(AllProductOutputConfig));
                    allProductOutputConfig = (AllProductOutputConfig)XmlSerializerHelper.ReadXML(OutputConfigPath, typeof(AllProductOutputConfig), out b);
                    UpdateOutputConfig();
                    Log("[OutputConfig]", "OutputConfig.xml文件重建并读取成功", LogLevel.Info);
                }
                else
                {
                    allProductOutputConfig = (AllProductOutputConfig)XmlSerializerHelper.ReadXML(OutputConfigPath, typeof(AllProductOutputConfig), out b);
                    UpdateOutputConfig();
                    Log("[OutputConfig]", "OutputConfig.xml文件读取成功", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log("[OutputConfig]", ex.Message, LogLevel.Error);
            }
        }

        private void UpdateOutputConfig()
        {
            for (int i = 0; i < allProductOutputConfig.outputConfigs.Count; i++)
            {
                if (allProductOutputConfig.outputConfigs[i].Product_name == currProductInfo.Product_name)
                {
                    currOutputConfig = allProductOutputConfig.outputConfigs[i];
                    break;
                }
            }
        }

        private void InitVisionTools()
        {
            //加载Offset任务
            OffsetTool = new CogToolBlock();
            //加载视觉任务
            visionTools = new List<CogToolBlock>();

            Thread thread = new Thread(() =>
            {
                UpdateVisionTask();
                this.Invoke(new Action(() => { InitDgv(); tip.Visible = false; }));
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void UpdateVisionTask()
        {
            try
            {
                OffsetTool.Dispose();
                //加载载具Offset任务
                OffsetTool = (CogToolBlock)CogSerializer.LoadObjectFromFile(currProductInfo.OffsetToolPath);
                //加载视觉任务
                visionTools.Clear();
                visionTools.Add((CogToolBlock)CogSerializer.LoadObjectFromFile(currProductInfo.VisionToolsPath));
                visionTools[0].Inputs["X_USL"].Value = currProductInfo.Limitness.X_USL;
                visionTools[0].Inputs["X_LSL"].Value = currProductInfo.Limitness.X_LSL;
                visionTools[0].Inputs["Y_USL"].Value = currProductInfo.Limitness.Y_USL;
                visionTools[0].Inputs["Y_LSL"].Value = currProductInfo.Limitness.Y_LSL;
                Log("[UpdateVisionTask]", "QR、Stitch、Vision任务加载完成", LogLevel.Info);
            }
            catch (Exception ex)
            {
                Log("[UpdateVisionTask]", "QR、Stitch、Vision任务加载失败;" + ex.Message.ToString(), LogLevel.Error);
            }
        }

        //2.加载图片保存配置
        string ImgConfigPath = @"D:\Lead2DParameter\Config\SaveImgSetting.xml";
        ImageSaveSetting currImageSaveSetting = new ImageSaveSetting();

        /// <summary>
        /// 加载图片配置文件
        /// </summary>
        public void LoadImgSettingConfig()
        {
            try
            {
                if (!File.Exists(ImgConfigPath))
                {
                    Log("[LoadImgSettingConfig]", "SaveImgSetting.xml 文件丢失，尝试重建", LogLevel.Error);
                    //File.Create(ImgConfigPath);
                    ImageSaveSetting imageSaveSetting = new ImageSaveSetting(true, true, true, true, 60, 10, 10, 20);
                    XmlSerializerHelper.WriteXML((Object)imageSaveSetting, ImgConfigPath, typeof(ImageSaveSetting));
                    currImageSaveSetting = (ImageSaveSetting)XmlSerializerHelper.ReadXML(ImgConfigPath, typeof(ImageSaveSetting), out b);
                }
                else
                {
                    currImageSaveSetting = (ImageSaveSetting)XmlSerializerHelper.ReadXML(ImgConfigPath, typeof(ImageSaveSetting), out b);
                    Log("[LoadProductInfo]", "SaveImgSetting.xml 文件读取成功", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log("[LoadImgSettingConfig]", ex.Message, LogLevel.Error);
            }
        }

        #endregion

        #region 主页面

        private void bn_ReconnectCam_Click(object sender, EventArgs e)
        {
            CloseClick();
            InitRuningCamrea();
            Log("[bn_ReconnectCam_Click]", "相机已重连！", LogLevel.Info);
        }

        private void bn_disConnectCam_Click(object sender, EventArgs e)
        {
            CloseClick();
            Log("[bn_disConnectCam_Click]", "相机已断开！", LogLevel.Info);
        }

        private void btn_ChangeProduct_Click(object sender, EventArgs e)
        {
            form_confirm fc = new form_confirm();
            if (DialogResult.Yes == fc.ShowDialog())
            {
                ChangeCancel.Visible = true;
                cmb_SltPro.Visible = true;
                ChangeTypeCf.Visible = true;
                labelAll.Visible = true;
                cmb_SltPro.SelectedItem = currProductInfo.Product_name;
            }
        }

        private void ChangeTypeCf_Click(object sender, EventArgs e)
        {
            progressBar2.Visible = true;
            for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
            {
                if (cmb_SltPro.SelectedItem.ToString() == ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name)
                {
                    currProductInfo = ALL_PRODUCT_TYPE.ProductInfoList[i];
                    break;
                }
            }

            ChangeCancel.Visible = false;
            cmb_SltPro.Visible = false;
            ChangeTypeCf.Visible = false;
            labelAll.Visible = false;

            tip.Visible = true;

            //相机赋值
            ConfigCameraParam();
            //拼图、检测、QR的视觉任务加载
            UpdateVisionTask();
            //输出项配置
            UpdateOutputConfig();
            //拼图检测QR等设置

            //初始化GDV
            InitDgv();
            GC.Collect();
            txt_Type.Text = currProductInfo.Product_name;
            progressBar2.Visible = false;
            MessageBox.Show("换型完成！");

            X_Nominal.Text = currProductInfo.Limitness.X_Nomial.ToString("0.000");
            Y_Nominal.Text = currProductInfo.Limitness.Y_Nominal.ToString("0.000");

            numX_USL.Text = currProductInfo.Limitness.X_USL.ToString("0.000");
            numX_LSL.Text = currProductInfo.Limitness.X_LSL.ToString("0.000");
            numY_USL.Text = currProductInfo.Limitness.Y_USL.ToString("0.000");
            numY_LSL.Text = currProductInfo.Limitness.Y_LSL.ToString("0.000");

            X_Tol_Plus.Text = currProductInfo.Limitness.X_Tol_Plus.ToString("0.000");
            X_Tol_Minus.Text = currProductInfo.Limitness.X_Tol_Minus.ToString("0.000");
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Y_Tol_Plus.Text = currProductInfo.Limitness.Y_Tol_Plus.ToString("0.000");
            Y_Tol_Minus.Text = currProductInfo.Limitness.Y_Tol_Minus.ToString("0.000");

        }

        private void ChangeCancel_Click(object sender, EventArgs e)
        {
            cmb_SltPro.Visible = false;
            ChangeTypeCf.Visible = false;
            labelAll.Visible = false;
            ChangeCancel.Visible = false;
        }

        /// <summary>
        /// 手动换型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_SltPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InitDone)
            {
                for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
                {
                    if (cmb_SltPro.SelectedItem.ToString() == ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name)
                    {
                        currProductInfo = ALL_PRODUCT_TYPE.ProductInfoList[i];
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 刷新表格显示
        /// </summary>
        private void UpdateDgv(int index)
        {
            //this.Invoke(new Action(() =>
            //{
            //    try
            //    {
            //        if (dataGridView1.Rows.Count>=200)
            //        {
            //            dataGridView1.Rows.Clear();
            //        }

            //        dataGridView1.Rows.Add(dataGridView1.Rows.Count, new DataGridViewRow());
            //        for (int i = 0; i < visionTools[0].Outputs.Count; i++)
            //        {
            //            if (index==0)
            //            {
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Value = visionTools[0].Outputs[i].Value;
            //            }
            //            else if (index==1)
            //            {
            //                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Value = "NG";
            //            }

            //        }
            //        //Log("[UpdateDgv]", "更新数据到界面", LogLevel.Info);
            //    }
            //    catch (Exception ex)
            //    {
            //        Log("[UpdateDgv]", ex.Message, LogLevel.Error);
            //    }
            //}));
        }

        /// <summary>
        /// 保存运行数据至本地
        /// </summary>
        public object locker_SaveResultData = new object();
        private void SaveData(int index)
        {
            lock (locker_SaveResultData)
            {
                try
                {
                    DataFileDir = Application.StartupPath + "\\ProductData\\" + currProductInfo.Product_name;
                    if (!Directory.Exists(DataFileDir))
                    {
                        Directory.CreateDirectory(DataFileDir);
                    }
                    string dataFilePath = DataFileDir + "\\" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".csv";
                    CsvFile file = new CsvFile();

                    //head
                    if (!File.Exists(dataFilePath))
                    {
                        CsvRecord recordHead = new CsvRecord();
                        recordHead.Fields.Add("时间");
                        recordHead.Fields.Add(ALL_PRODUCT_TYPE.SelectedProduct);

                        for (int i = 0; i < visionTools[0].Outputs.Count; i++)
                        {
                            recordHead.Fields.Add(visionTools[0].Outputs[i].Name);
                        }

                        file.Records.Add(recordHead);
                        CsvWriter writerHead = new CsvWriter();
                        writerHead.WriteCsv(file, dataFilePath);
                    }

                    //body
                    file.Records.Clear();
                    file.Populate(dataFilePath, false);
                    CsvRecord recordBody = new CsvRecord();
                    recordBody.Fields.Add(DateTime.Now.ToString("yyyyMMdd_HHmmss.fff"));

                    recordBody.Fields.Add(SN);
                    //判断输出类型，根据输出类型，依次采用不同方式追加值 updata 未完成

                    for (int i = 0; i < visionTools[0].Outputs.Count; i++)
                    {
                        recordBody.Fields.Add(visionTools[0].Outputs[i].Value.ToString());
                    }

                    file.Records.Add(recordBody);
                    CsvWriter writer = new CsvWriter();
                    writer.WriteCsv(file, dataFilePath);
                    //Log("[SaveResultData]", "数据已写入本地", LogLevel.Info);
                }
                catch (Exception ex)
                {
                    Log("[SaveResultData]", ex.Message, LogLevel.Error);
                }
            }
        }

        /// <summary>
        /// 刷新CogRecordDisplay显示
        /// </summary>
        /// <param name="cogRecordDisplay"></param>
        /// <param name="cogToolBlock"></param>
        private void UpdateDisplay(ICogImage img, Image image)
        {
            if (runMode == RunMode.Auto)
            {
                if (currImageSaveSetting.SaveOKPicture)
                {
                    Dir_pictureName = currImageSaveSetting.AllPicturesDir + "\\" + currProductInfo.Product_name + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                    if (!Directory.Exists(Dir_pictureName))
                    {
                        Directory.CreateDirectory(Dir_pictureName);
                    }
                    pictureName = Dir_pictureName + DateTime.Now.ToString("HHmmssfff") + ".png";
                    SaveGraphicsPictures(image, pictureName, currImageSaveSetting.PicQuality, 0);
                }
                if (currImageSaveSetting.SaveRawImg)
                {
                    Dir_imageName = currImageSaveSetting.AllRawImageDir + "\\" + currProductInfo.Product_name + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                    if (!Directory.Exists(Dir_imageName))
                    {
                        Directory.CreateDirectory(Dir_imageName);
                    }
                    imageName = Dir_imageName + DateTime.Now.ToString("HHmmssfff") + ".bmp";
                    SaveRawImage(0, imageName);
                }
            }
        }

        /// <summary>
        /// 双击页面运行记录清空内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox4_DoubleClick(object sender, EventArgs e)
        {
            listBoxRunLog.Items.Clear();
        }

        private bool isSwitched = false;

        private void listBoxRunLog_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripLOG.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void 复制选中内容ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listBoxRunLog.Items[listBoxRunLog.SelectedIndex].ToString());
        }

        #endregion

        #region 图片记录

        enum Pic_Quality
        {
            Low = 1,
            Middle,
            High
        }

        enum DirType
        {
            AllRaw = 1,
            NGRaw,
            AllPic,
            NGPic,
            StitchPic
        }

        private void ControlsStatus_WhenFormLoad()
        {
            //图片回看页面

            //隐藏tp测试
            //Main_Tab_Control.TabPages["tp测试"].Parent = null;
            numericUpDownNgPicDays.Value = currImageSaveSetting.NGPictureDays;
            numericUpDownAllPicDays.Value = currImageSaveSetting.OKPictureDays;
            numericUpDownNgRawPicDays.Value = currImageSaveSetting.NGImgDays;
            numericUpDownAllRawPicDays.Value = currImageSaveSetting.RawImgDays;
            numericUpDownStitchPicDays.Value = currImageSaveSetting.StitchPictureDays;

            chkb_NGpic.Checked = currImageSaveSetting.SaveNGPicture;
            chkb_Allpic.Checked = currImageSaveSetting.SaveOKPicture;
            chkb_NGimg.Checked = currImageSaveSetting.SaveNGImg;
            chkb_Allimg.Checked = currImageSaveSetting.SaveRawImg;
            chkb_Stitch.Checked = currImageSaveSetting.SaveStitchPicture;

            switch (currImageSaveSetting.PicQuality)
            {
                case 1:
                    rbtn_Low.Checked = true;
                    break;
                case 2:
                    rbtn_Mid.Checked = true;
                    break;
                case 3:
                    rbtn_High.Checked = true;
                    break;
                default:
                    break;
            }

            TXTngPicDir.Text = currImageSaveSetting.NGPicturesDir;
            TXTallPicDir.Text = currImageSaveSetting.AllPicturesDir;
            TXTrawngPicDir.Text = currImageSaveSetting.NGRawImageDir;
            TXTrawPicDir.Text = currImageSaveSetting.AllRawImageDir;
            TXTstitchPicDir.Text = currImageSaveSetting.StitchImageDir;

            //图片回看页面
            Control_Limited(groupBoxSaveDay, "", false);
            Control_Limited(groupBoxPicQuaility, "", false);
            Control_Limited(groupBoximgset, "btn_ModifyDays", false);



            //光学调试页面
            btn_swth_mode.Text = "自动运行";
            btn_swth_mode.BackColor = Color.Green;
            Control_Limited(groupBoxInitCamera, "btn_swth_mode", false);
            //Control_Limited(groupBoxInitCameraParam, "", false);
            //Control_Limited(groupBoxLight, "", false);
            Control_Limited(groupBoxTriggermode, "", false);
            //Control_Limited(null, "", false & !m_bGrabbing);
            Control_Limited(groupBoxDebugBrowse, "", false);
        }

        private void btn_savedays_Click(object sender, EventArgs e)
        {
            try
            {
                Control_Limited(groupBoxSaveDay, "", false);
                Control_Limited(groupBoxPicQuaility, "", false);
                Control_Limited(groupBoximgset, "btn_ModifyDays", false);
                labelSaveday.ForeColor = Color.Green;
                labelSaveday.Text = "Saved!";

                currImageSaveSetting.NGImgDays = (int)numericUpDownNgRawPicDays.Value;
                currImageSaveSetting.NGPictureDays = (int)numericUpDownNgPicDays.Value;
                currImageSaveSetting.RawImgDays = (int)numericUpDownAllRawPicDays.Value;
                currImageSaveSetting.OKPictureDays = (int)numericUpDownAllPicDays.Value;
                currImageSaveSetting.StitchPictureDays = (int)numericUpDownStitchPicDays.Value;

                currImageSaveSetting.SaveNGPicture = chkb_NGpic.Checked;
                currImageSaveSetting.SaveOKPicture = chkb_Allpic.Checked;
                currImageSaveSetting.SaveNGImg = chkb_NGimg.Checked;
                currImageSaveSetting.SaveRawImg = chkb_Allimg.Checked;
                currImageSaveSetting.SaveStitchPicture = chkb_Stitch.Checked;

                currImageSaveSetting.PicQuality = rbtn_Low.Checked == true ? 1 : (rbtn_Mid.Checked == true ? 2 : (rbtn_High.Checked == true ? 3 : 1));


                try
                {
                    XmlSerializerHelper.WriteXML((Object)currImageSaveSetting, ImgConfigPath, typeof(ImageSaveSetting));
                    labelSaveday.ForeColor = Color.Green;
                    labelSaveday.Text = "Save OK";
                }
                catch (Exception ex)
                {
                    labelSaveday.ForeColor = Color.Tomato;
                    labelSaveday.Text = ex.Message;

                    Log("[btn_savedays_Click]", ex.Message, LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                labelSaveday.ForeColor = Color.Tomato;
                labelSaveday.Text = ex.Message;
                Log("[btn_savedays_Click]", ex.Message, LogLevel.Error);
            }
        }

        private void btn_ModifyDays_Click(object sender, EventArgs e)
        {
            form_confirm f = new form_confirm();
            if (DialogResult.Yes == f.ShowDialog())
            {
                Control_Limited(groupBoxSaveDay, "", true);
                Control_Limited(groupBoxPicQuaility, "", true);
                Control_Limited(groupBoximgset, "", true);
            }
        }

        private void btn_ngPicture_Click(object sender, EventArgs e)
        {
            SelecteFolder(TXTngPicDir, currImageSaveSetting, DirType.NGPic);
        }

        private void btn_allPicture_Click(object sender, EventArgs e)
        {
            SelecteFolder(TXTallPicDir, currImageSaveSetting, DirType.AllPic);
        }

        private void btn_ngRaw_Click(object sender, EventArgs e)
        {
            SelecteFolder(TXTrawngPicDir, currImageSaveSetting, DirType.NGRaw);
        }

        private void btn_allRaw_Click(object sender, EventArgs e)
        {
            SelecteFolder(TXTrawPicDir, currImageSaveSetting, DirType.StitchPic);
        }

        private void btn_stitchPic_Click(object sender, EventArgs e)
        {
            SelecteFolder(TXTstitchPicDir, currImageSaveSetting, DirType.AllRaw);
        }

        private void SelecteFolder(TextBox tb, ImageSaveSetting temp_ImageSaveSetting, DirType dirType)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "★请选择一个文件夹★";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                switch (dirType)
                {
                    case DirType.AllRaw:
                        temp_ImageSaveSetting.AllRawImageDir = foldPath;
                        break;
                    case DirType.NGRaw:
                        temp_ImageSaveSetting.NGRawImageDir = foldPath;
                        break;
                    case DirType.AllPic:
                        temp_ImageSaveSetting.AllPicturesDir = foldPath;
                        break;
                    case DirType.NGPic:
                        temp_ImageSaveSetting.NGPicturesDir = foldPath;
                        break;
                    case DirType.StitchPic:
                        temp_ImageSaveSetting.StitchImageDir = foldPath;
                        break;
                    default:
                        break;
                }
                tb.Text = foldPath;
            }
        }

        /// <summary>
        /// 加载一张回看图形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_review_pic_Click(object sender, EventArgs e)
        {
            OpenFileDialog imgDialog = new OpenFileDialog();
            imgDialog.InitialDirectory = currImageSaveSetting.AllPicturesDir;
            //imgDialog.Filter
            imgDialog.ShowDialog();
            try
            {
                imgPath = imgDialog.FileName;
                if (imgPath.Contains(".bmp") || imgPath.Contains(".png") || imgPath.Contains(".jpg") || imgPath.Contains(".jpeg"))
                {
                    listBox_ReviewPic.Items.Add(imgPath);
                    listBox_ReviewPic.SelectedItem = listBox_ReviewPic.Items[listBox_ReviewPic.Items.Count - 1].ToString();
                    listBox_ReviewPic.SelectedIndex = listBox_ReviewPic.Items.Count - 1;
                }
            }
            catch (Exception ex) { Log("[btn_review_pic_Click]", ex.Message, LogLevel.Info); }
        }

        /// <summary>
        /// 加载文件夹图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_review_picFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.SelectedPath = currImageSaveSetting.AllPicturesDir;
            d.ShowDialog();
            string dirPath = d.SelectedPath;
            try
            {
                imgsPath = Directory.GetFiles(dirPath);
                for (int i = 0; i < imgsPath.Length; i++)
                {
                    if (imgsPath[i].Contains(".bmp") || imgsPath[i].Contains(".png") || imgsPath[i].Contains(".jpg") || imgsPath[i].Contains(".jpeg"))
                    {
                        listBox_ReviewPic.Items.Add(imgsPath[i]);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 移除选中图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_review_remove_itm_Click(object sender, EventArgs e)
        {
            try
            {
                listBox_ReviewPic.Items.Remove(listBox_ReviewPic.SelectedItem);
                DisplayReView.Image = null;
            }
            catch { }
        }

        /// <summary>
        /// 清空图像列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_review_clear_itm_Click(object sender, EventArgs e)
        {
            listBox_ReviewPic.Items.Clear();
            DisplayReView.Image = null;
        }

        /// <summary>
        /// 图像显示随索引变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_ReviewPic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox_ReviewPic.SelectedIndex != -1)
            {
                try
                {
                    DisplayReView.Image = new CogImage24PlanarColor(new Bitmap(listBox_ReviewPic.SelectedItem.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void labelSaveday_DoubleClick(object sender, EventArgs e)
        {
            labelSaveday.Text = "";
        }

        private void lb_NGPic_DoubleClick(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start("Explorer.exe", "C:\\");
            System.Diagnostics.Process.Start(currImageSaveSetting.NGPicturesDir);
        }

        private void lb_AllPic_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(currImageSaveSetting.AllPicturesDir);
        }

        private void lb_NGImg_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(currImageSaveSetting.NGRawImageDir);
        }

        private void lb_AllImg_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(currImageSaveSetting.AllRawImageDir);
        }

        private void lb_stitchImg_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(currImageSaveSetting.StitchImageDir);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime d = DateTime.Today;
            DateTime allpic = d.AddDays(-currImageSaveSetting.OKPictureDays);
            DateTime ngpic = d.AddDays(-currImageSaveSetting.NGPictureDays);
            DateTime ngraw = d.AddDays(-currImageSaveSetting.NGImgDays);
            DateTime allraw = d.AddDays(-currImageSaveSetting.RawImgDays);

            delfiles(new DirectoryInfo(currImageSaveSetting.AllPicturesDir), allpic);
            delfiles(new DirectoryInfo(currImageSaveSetting.NGPicturesDir), ngpic);
            delfiles(new DirectoryInfo(currImageSaveSetting.NGRawImageDir), ngraw);
            delfiles(new DirectoryInfo(currImageSaveSetting.AllRawImageDir), allraw);
        }

        public void delfiles(DirectoryInfo dirInfo, DateTime time)
        {
            try
            {
                if (dirInfo.GetFiles() != null)
                {
                    foreach (FileInfo Fileinfo in dirInfo.GetFiles())
                    {
                        if (Fileinfo.CreationTime < time)
                        {
                            Fileinfo.Delete();
                        }
                    }
                    try
                    {
                        dirInfo.Delete();
                    }
                    catch { }

                }
                try
                {
                    if (dirInfo.GetDirectories() != null)
                    {
                        foreach (DirectoryInfo dirInfo1 in dirInfo.GetDirectories())
                        {
                            delfiles(dirInfo1, time);
                        }
                    }
                }
                catch { }
            }
            catch { }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //每隔5分钟扫一次，每天晚上9：05——9：25期间扫描有效，进行图片删除
            if ((!this.backgroundWorker1.IsBusy) && (DateTime.Now.Hour == 21) && (DateTime.Now.Minute > 5 && DateTime.Now.Minute < 25))
            {
                this.backgroundWorker1.RunWorkerAsync();
            }
        }

        #endregion

        #region 任务编辑页面
        //变量
        string tempTBpath = "";
        CogToolBlock currTB = new CogToolBlock();

        string imgPath = "";
        string[] imgsPath = new string[] { };

        private void bn_SaveLimit_Click(object sender, EventArgs e)
        {
            try
            {
                currProductInfo.Limitness.X_Nomial = Convert.ToDouble(X_Nominal.Text);
                currProductInfo.Limitness.Y_Nominal = Convert.ToDouble(Y_Nominal.Text);

                currProductInfo.Limitness.X_USL = Convert.ToDouble(X_Nominal.Text) + Convert.ToDouble(X_Tol_Plus.Text);
                currProductInfo.Limitness.X_LSL = Convert.ToDouble(X_Nominal.Text) + Convert.ToDouble(X_Tol_Minus.Text);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                currProductInfo.Limitness.Y_USL = Convert.ToDouble(Y_Nominal.Text) + Convert.ToDouble(Y_Tol_Plus.Text);
                currProductInfo.Limitness.Y_LSL = Convert.ToDouble(Y_Nominal.Text) + Convert.ToDouble(Y_Tol_Minus.Text);

                currProductInfo.Limitness.X_Tol_Plus = Convert.ToDouble(X_Tol_Plus.Text);
                currProductInfo.Limitness.X_Tol_Minus = Convert.ToDouble(X_Tol_Minus.Text);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                currProductInfo.Limitness.Y_Tol_Plus = Convert.ToDouble(Y_Tol_Plus.Text);
                currProductInfo.Limitness.Y_Tol_Minus = Convert.ToDouble(Y_Tol_Minus.Text);

                XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                Log("[bn_SaveLimit_Click]", "保存SPC参数至本地", LogLevel.Info);
                MessageBox.Show("OK");

                X_Nominal.Text = currProductInfo.Limitness.X_Nomial.ToString("0.000");
                Y_Nominal.Text = currProductInfo.Limitness.Y_Nominal.ToString("0.000");

                numX_USL.Text = currProductInfo.Limitness.X_USL.ToString("0.000");
                numX_LSL.Text = currProductInfo.Limitness.X_LSL.ToString("0.000");
                numY_USL.Text = currProductInfo.Limitness.Y_USL.ToString("0.000");
                numY_LSL.Text = currProductInfo.Limitness.Y_LSL.ToString("0.000");

                X_Tol_Plus.Text = currProductInfo.Limitness.X_Tol_Plus.ToString("0.000");
                X_Tol_Minus.Text = currProductInfo.Limitness.X_Tol_Minus.ToString("0.000");
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                Y_Tol_Plus.Text = currProductInfo.Limitness.Y_Tol_Plus.ToString("0.000");
                Y_Tol_Minus.Text = currProductInfo.Limitness.Y_Tol_Minus.ToString("0.000");

            }
            catch (Exception ex)
            {
                Log("[bn_SaveLimit_Click]", ex.Message, LogLevel.Error);
            }
        }

        private void bn_runOnce_Click(object sender, EventArgs e)
        {
            listBoxpics_DoubleClick(null, null);
        }

        /// <summary>
        /// 手动加载VPP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_loadVpp_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog = new OpenFileDialog();
            openfiledialog.InitialDirectory = Path.GetDirectoryName(currProductInfo.VisionToolsPath);
            openfiledialog.Filter = "ToolBlock file|*.vpp";
            if (openfiledialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            tempTBpath = openfiledialog.FileName;
            label1.ForeColor = Color.Tomato;
            label1.Text = "Loading...";
            if (tempTBpath.Contains(".vpp"))
            {
                try
                {
                    currTB = (CogToolBlock)CogSerializer.LoadObjectFromFile(tempTBpath);
                    cogToolBlockEditV21.Subject = currTB;
                }
                catch { label1.Text = "err type!"; }

                tb_TBPath.Text = tempTBpath;

                label1.ForeColor = Color.Green;
                label1.Text = "complete!";
            }
            else
            {
                label1.Text = "fail!";
            }
        }

        /// <summary>
        /// 加载单张图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_load1Imag_Click(object sender, EventArgs e)
        {
            OpenFileDialog imgDialog = new OpenFileDialog();
            imgDialog.InitialDirectory = currImageSaveSetting.AllRawImageDir;
            imgDialog.Filter = @"Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpg)|*.jpg|所有合适文件(*.bmp,*.jpg)|*.bmp;*.jpg";// openFileDialog.Filter = @"Bitmap文件(*.bmp)|*.bmp|Jpeg文件(*.jpg)|*.jpg|所有合适文件(*.bmp,*.jpg)|*.bmp;*.jpg";
            imgDialog.ShowDialog();
            try
            {
                imgPath = imgDialog.FileName;
                listBoxpics.Items.Add(imgPath);
                listBoxpics.SelectedItem = listBoxpics.Items[listBoxpics.Items.Count - 1].ToString();
                listBoxpics.SelectedIndex = listBoxpics.Items.Count - 1;
            }
            catch { }
        }

        /// <summary>
        /// 加载选中文件夹图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_loadNImag_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.SelectedPath = currImageSaveSetting.AllRawImageDir;
            d.ShowDialog();
            string dirPath = d.SelectedPath;
            try
            {
                imgsPath = Directory.GetFiles(dirPath);
                for (int i = 0; i < imgsPath.Length; i++)
                {
                    if (imgsPath[i].Contains(".bmp") || imgsPath[i].Contains(".png") || imgsPath[i].Contains(".jpg") || imgsPath[i].Contains(".jpeg"))
                    {
                        listBoxpics.Items.Add(imgsPath[i]);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 双击图片名称运行当前图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxpics_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                lb_RunStatus.Text = "";
                string fname = listBoxpics.SelectedItem.ToString();
                string inputImgName = currTB.Inputs.Contains("InputImage") ? "InputImage" : "OutputImage";
                currTB.Inputs[inputImgName].Value = new CogImage8Grey((Bitmap)Image.FromFile(fname));
                currTB.Run();
                GetResults(currTB, currProductInfo);
            }
            catch (Exception ex)
            {
                lb_RunStatus.ForeColor = Color.Tomato;
                lb_RunStatus.Text = ex.Message;
            }
        }

        /// <summary>
        /// 获取运行结果
        /// </summary>
        /// <param name="tb">当前视觉任务</param>
        /// <param name="productInfo">产品信息</param>
        private void GetResults(CogToolBlock tb, ProductInfo productInfo)
        {
            try
            {
                for (int i = 0; i < tb.Outputs.Count; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        try
                        {
                            listBoxTBResults.Items.Add(tb.Outputs[i].Name + ":" + ((double)tb.Outputs[i].Value).ToString("0.000"));
                        }
                        catch
                        {
                            listBoxTBResults.Items.Add(tb.Outputs[i].Name + ":" + tb.Outputs[i].Value.ToString());
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                Log("[GetResults]", tb.Name + ":" + ex.Message, LogLevel.Error);
            }

        }

        /// <summary>
        /// 清除视觉任务运行结果显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxTBResults_DoubleClick(object sender, EventArgs e)
        {
            listBoxTBResults.Items.Clear();
        }

        /// <summary>
        /// 移除选中视觉工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_removeTBs_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("正在编辑的任务也会被刷新", "重要提示", MessageBoxButtons.YesNo))
            {
                try
                {
                    tb_TBPath.Text = "";
                    currTB = null;
                    cogToolBlockEditV21.Subject = null;
                }
                catch { }
            }
        }

        /// <summary>
        /// 移除选中图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_edt_rmv_pic_Click(object sender, EventArgs e)
        {
            try
            {
                listBoxpics.Items.Remove(listBoxpics.SelectedItem);
            }
            catch { }
        }

        /// <summary>
        /// 清空列表图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_edt_clr_pic_Click(object sender, EventArgs e)
        {
            listBoxpics.Items.Clear();
        }

        /// <summary>
        /// 清除视觉任务运行状态显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_RunStatus_DoubleClick(object sender, EventArgs e)
        {
            lb_RunStatus.Text = "";
        }

        /// <summary>
        /// 清除label内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label11_DoubleClick(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        /// <summary>
        /// 保存当前编辑的任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_saveEditVpp_Click(object sender, EventArgs e)
        {
            listBoxTBResults.Items.Add("正在保存并重新加载任务（耗时约20秒）,请耐心等待......");
            try
            {
                CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, tempTBpath);
                UpdateVisionTask();
                GC.Collect();
                listBoxTBResults.Items.Add("加载完成");
            }
            catch (Exception ex)
            {
                listBoxTBResults.Items.Add("保存出错：" + ex.Message);
                Log("[btn_saveEditVpp_Click]", ex.Message, LogLevel.Error);
            }
        }
        private void lb_nowEditVpp_DoubleClick(object sender, EventArgs e)
        {

        }

        private void bn_AddNewProduct_Click(object sender, EventArgs e)
        {
            AddNewProductForm addNewProductForm1 = new AddNewProductForm(ALL_PRODUCT_TYPE);
            addNewProductForm1.ShowDialog();
            //ALL_PRODUCT_TYPE newALL_PRODUCT_TYPE = addNewProductForm1.GetNewALL_PRODUCT_TYPE();
            cmb_SltPro.Items.Clear();
            for (int i = 0; i < ALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
            {
                cmb_SltPro.Items.Add(ALL_PRODUCT_TYPE.ProductInfoList[i].Product_name);
            }
        }
        #endregion

        #region 光学调试

        /// <summary>
        /// 模式切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_swth_mode_Click(object sender, EventArgs e)
        {
            if (btn_swth_mode.Text == "自动运行")
            {
                form_confirm f = new form_confirm();
                if (DialogResult.Yes == f.ShowDialog())
                {
                    Control_Limited(groupBoxInitCamera, "btn_swth_mode", true);
                    //Control_Limited(groupBoxInitCameraParam, "btn_swth_mode", true);
                    //Control_Limited(groupBoxLight, "btn_swth_mode", true);
                    Control_Limited(groupBoxTriggermode, "", true);

                    btn_swth_mode.Text = "调试状态";
                    btn_swth_mode.BackColor = Color.Tomato;
                }
            }
            else if (btn_swth_mode.Text == "调试状态")
            {
                CloseClick();

                Control_Limited(groupBoxInitCamera, "btn_swth_mode", false);
                //Control_Limited(groupBoxInitCameraParam, "btn_swth_mode", false);
                //Control_Limited(groupBoxLight, "btn_swth_mode", false);

                InitRuningCamrea();
                Display_debug.Image = null;
                GC.Collect();
                btn_swth_mode.Text = "自动运行";
                btn_swth_mode.BackColor = Color.Green;
            }
        }

        private void bn_Triger_Click(object sender, EventArgs e)
        {
            //要上位机对光源和相机同时触发
            //tServer.SendToClient(currImageSaveSetting.IPAdr, "Trig");
        }

        #endregion

        #region 公用函数

        /// <summary>
        /// 控件使能控制
        /// </summary>
        /// <param name="gb">作用于GroupBox的名字</param>
        /// <param name="unLimitedName">不设置的控件名字</param>
        /// <param name="able">使能还是不使能</param>
        private void Control_Limited(GroupBox gb, string unLimitedName, bool able)
        {
            try
            {
                foreach (Control control in gb.Controls)
                {
                    if (control.Name == unLimitedName)
                    {
                        control.Enabled = true;
                    }
                    else
                    {
                        control.Enabled = able;
                    }
                }
            }
            catch (Exception ex)
            {
                Log("[Control_Limited]", ex.Message, LogLevel.Error);
            }
        }

        /// <summary>
        /// 初始化表格显示
        /// </summary>
        private void InitDgv()
        {
            //try
            //{
            //    this.Invoke(new Action(() =>
            //    {
            //        //fai,curr,std,uptolerance,downtolerance,discribe  表头
            //        dataGridView1.ColumnCount = visionTools[0].Outputs.Count;
            //        for (int i = 0; i < visionTools[0].Outputs.Count; i++)
            //        {
            //            dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //            dataGridView1.Columns[i].HeaderCell.Value = visionTools[0].Outputs[i].Name;
            //        }
            //    }));
            //}
            //catch (Exception ex)
            //{
            //    Log("[InitDgv]", ex.Message, LogLevel.Error);
            //}
        }

        /// <summary>
        /// 保存原始图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="RawImageSaveName"></param>
        private void SaveRawImage(int tb, string RawImageSaveName)
        {
            try
            {
                string inputImgName = visionTools[tb].Inputs.Contains("InputImage") ? "InputImage" : "OutputImage";
                Bitmap b = new Bitmap((visionTools[tb].Inputs[inputImgName].Value as ICogImage).ToBitmap());
                Thread t1 = new Thread(() =>
                {
                    b.Save(RawImageSaveName);
                    b.Dispose();
                });
                t1.Start();
            }
            catch (Exception ex)
            {
                Log("[SaveRawImage]", "请检查磁盘是否已满，或其他问题:" + ex.Message, LogLevel.Error);
            }

        }

        /// <summary>
        /// 保存带图形图片
        /// </summary>
        /// <param name="PNGSaveName"></param>
        private void SaveGraphicsPictures(Image image, string PNGSaveName, int picQuality, int ob)
        {
            try
            {
                switch (picQuality)
                {
                    case 1:
                        SaveJPEGfile(image, PNGSaveName, 15);
                        break;
                    case 2:
                        SaveJPEGfile(image, PNGSaveName, 25);
                        break;
                    case 3:
                        SaveJPEGfile(image, PNGSaveName, 50);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log("[SaveGraphicsPictures]", "报错信息：" + ex.Message, LogLevel.Error);
            }
        }

        /// <summary>
        /// 保存缩略图
        /// </summary>
        /// <param name="bitmap">位图</param>
        /// <param name="filename">文件名</param>
        /// <param name="picQuality">图片质量,数字越大图像质量越好也愈大，MSDN未给范围，推荐25,50,75 </param>
        public void SaveJPEGfile(Image bitmap, string filename, long picQuality)
        {
            Image temp = (Image)bitmap.Clone();
            Bitmap bitmap1 = new Bitmap(temp);
            try
            {
                ImageCodecInfo myImageCodecInfo;
                myImageCodecInfo = GetEncoderInfo("image/jpeg");
                System.Drawing.Imaging.Encoder myEncoder;
                EncoderParameter myEncoderParameter;
                EncoderParameters myEncoderParameters;
                myEncoder = System.Drawing.Imaging.Encoder.Quality;
                myEncoderParameters = new EncoderParameters(1);
                myEncoderParameter = new EncoderParameter(myEncoder, picQuality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                Thread t1 = new Thread(() =>
                {
                    bitmap1.Save(filename, myImageCodecInfo, myEncoderParameters);
                    bitmap1.Dispose();
                });
                t1.Start();
            }
            catch (Exception ex)
            {
                Log("[SaveJPEGfile]", "报错：" + ex.Message, LogLevel.Error);
            }
        }

        /// <summary>
        /// 编码信息
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        #endregion

        #region CameraRun
        MyCamera.MV_FRAME_OUT_INFO_EX mV_FRAME_OUT_INFO_EX = new MyCamera.MV_FRAME_OUT_INFO_EX();
        MyCamera.MV_CC_DEVICE_INFO_LIST deviceInforList;
        MyCamera.MV_CC_DEVICE_INFO device;
        public MyCamera mCamera;
        MyCamera.cbOutputExdelegate ImageCallback;//生命回调委托

        //用于从驱动获取图像的缓存 Buffer for getting image from driver
        UInt32 m_nBufSizeForDriver = 3072 * 2048 * 3;
        byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];

        //用于保存图像的缓存 Buffer for saving image
        UInt32 m_nBufSizeForSaveImage = 4096 * 3000 * 3 * 3 + 2048;
        byte[] m_pBufForSaveImage = new byte[4096 * 3000 * 3 * 3 + 2048];



        /// <summary>
        /// 获取相机列表
        /// </summary>
        public void InitRuningCamrea()
        {
            DeviceListAcq();
            bnOpen_Click(null, null);
            //注册回调函数#
            ImageCallback = new MyCamera.cbOutputExdelegate(CallbackEx);//设置函数
            if (MyCamera.MV_OK != mCamera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero))//注册回调//MV_CC_RegisterImageCallBackForRGB_NET
            {
                Log("[InitCamrea]", "Register ImageCallBack failed", LogLevel.Error);
                MessageBox.Show("Register ImageCallBack failed");
                return;
            }
            mCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);//关闭自动曝光
            mCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);// Acquisition Mode  0:SingleFrame 1:MultiFrame 2:Continuous
            Log("[InitCamrea]", "AcquisitionMode:Continuous", LogLevel.Info);
            mCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);    // Trigger Mode：   0:OFF  1:ON
            Log("[InitCamrea]", "TriggerMode:ON", LogLevel.Info);
            mCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);// ch:触发源选择:0 - Line0; | en:Trigger source select: 0Line0(***);  1.Line1;  2.Line2;  3.Line3;  4.Counter;  7.Software(***);  8.FrequencyConverter
            Log("[InitCamrea]", "TriggerSource:Line0", LogLevel.Info);

            if (MyCamera.MV_OK != mCamera.MV_CC_StartGrabbing_NET())// 开始采集 Start Grabbing
            {
                Log("[InitCamrea]", "触发失败", LogLevel.Error);
                MessageBox.Show("触发失败");
                return;
            }
            ConfigCameraParam();
        }

        /// <summary>
        /// 配置相机参数
        /// </summary>
        public void ConfigCameraParam()
        {
            int nRet;
            nRet = mCamera.MV_CC_SetFloatValue_NET("ExposureTime", (float)currProductInfo.cameraInfo.Exposure);
            if (nRet != MyCamera.MV_OK)
            {
                Log("[ConfigCameraParam]", "设置曝光失败：" + nRet, LogLevel.Error);
            }
            else
            {
                Log("[ConfigCameraParam]", "设置曝光成功：" + currProductInfo.cameraInfo.Exposure, LogLevel.Info);
            }
            nRet = mCamera.MV_CC_SetFloatValue_NET("Gain", (float)currProductInfo.cameraInfo.Gain);
            if (nRet != MyCamera.MV_OK)
            {
                Log("[ConfigCameraParam]", "设置增益失败：" + nRet, LogLevel.Error);
            }
            else
            {
                Log("[ConfigCameraParam]", "设置增益成功：" + currProductInfo.cameraInfo.Gain, LogLevel.Info);
            }

        }

        public static bool scanedQR = false;
        public static int QRtimes = 0;
        /// <summary>
        /// 触发拍照_回调函数
        /// </summary>
        /// <param name="pData">图像缓存地址</param>
        /// <param name="pFrameInfo">图像信息</param>
        /// <param name="pUser">用户数据</param>
        private void CallbackEx(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            Log("[CallbackEx]", "CCD拍照触发", LogLevel.Info);
            ICogImage currImg = GetImg(pData, pFrameInfo);
            Thread thread = new Thread(RunTask);
            thread.IsBackground = true;
            thread.Start(currImg);
        }

        private static readonly object TaskRunLocker = new object();
        /// <summary>
        /// 任务运行
        /// </summary>
        /// <param name="obj"></param>
        private void RunTask(object Imgobj)
        {
            lock (TaskRunLocker)
            {
                ICogImage cogImage = Imgobj as ICogImage;
                Image recorImg = null;
                //偏移
                try
                {
                    string ofinputImgName = OffsetTool.Inputs.Contains("InputImage") ? "InputImage" : "OutputImage";
                    OffsetTool.Inputs[ofinputImgName].Value = cogImage;
                    OffsetTool.Run();
                    if (OffsetTool.RunStatus.Result == CogToolResultConstants.Accept)
                    {
                        string oftValue = OffsetTool.Outputs[0].Value.ToString();
                        mqOft.SendMsg(oftValue);
                        Log("[RunTask]", string.Format("偏移:{0}", oftValue), LogLevel.Info);
                    }
                    else
                    {
                        mqOft.SendMsg("0");
                        Log("[RunTask]", string.Format("偏移工具失败,默认补偿值0"), LogLevel.Error);
                    }
                }
                catch (Exception ex)
                {
                    mqOft.SendMsg(0);
                    Log("[RunTask]", string.Format("偏移工具失败:{0}", ex.Message), LogLevel.Error);
                }

                //识别
                try
                {
                    string inputimageName = visionTools[0].Inputs.Contains("InputImage") ? "InputImage" : "OutputImage";
                    visionTools[0].Inputs[inputimageName].Value = cogImage;
                    visionTools[0].Inputs["X_USL"].Value = currProductInfo.Limitness.X_USL;
                    visionTools[0].Inputs["X_LSL"].Value = currProductInfo.Limitness.X_LSL;
                    visionTools[0].Inputs["Y_USL"].Value = currProductInfo.Limitness.Y_USL;
                    visionTools[0].Inputs["Y_LSL"].Value = currProductInfo.Limitness.Y_LSL;
                    visionTools[0].Run();
                    Thread.Sleep(60);

                    cogRecordDisplay2.Image = cogImage;
                    cogRecordDisplay2.Record = visionTools[0].CreateLastRunRecord().SubRecords[0];
                    cogRecordDisplay2.Update();
                    cogRecordDisplay2.Fit(true);
                }
                catch (Exception ex)
                {
                    recorImg = cogImage.ToBitmap();
                    Log("[RunTask]", string.Format("检测工具失败:{0}", ex.Message), LogLevel.Error);
                }
                try
                {
                    recorImg = cogRecordDisplay2.CreateContentBitmap(CogDisplayContentBitmapConstants.Custom, null, 0);
                    //cogRecordDisplay2.Image = null;
                    //cogRecordDisplay2.Record.SubRecords.Clear();
                }
                catch (Exception ex)
                {
                    recorImg = cogImage.ToBitmap();
                    Log("[RunTask]", string.Format("未创建NG图形:{0}", ex.Message), LogLevel.Error);
                }

                //SaveData(0);

                UpdateDisplay(cogImage, recorImg);

                AllResult allResult = new AllResult(new List<RowResult>(), "", "");
                #region DEFINITION NG PIC IMG PATH
                if (currImageSaveSetting.SaveNGPicture)
                {
                    Dir_ngPictureName = currImageSaveSetting.NGPicturesDir + "\\" + currProductInfo.Product_name + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                }
                if (currImageSaveSetting.SaveNGImg)
                {
                    Dir_ngImageName = currImageSaveSetting.NGRawImageDir + "\\" + currProductInfo.Product_name + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
                }
                #endregion
                //工具运行OK
                if (visionTools[0].RunStatus.Result == CogToolResultConstants.Accept)
                {
                    UpdateDgv(0);
                    try
                    {
                        //var lstAllResult1= visionTools[0].Outputs;
                        //char[] RowClum = visionTools[0].Outputs[visionTools[0].Outputs.Count - 1].Name.ToString().ToCharArray();  ============
                        char[] RowClum = visionTools[0].Outputs[visionTools[0].Outputs.Count - 3].Name.ToString().ToCharArray();
                        int row = Convert.ToInt32(RowClum[1].ToString());
                        int clum = Convert.ToInt32(RowClum[2].ToString());
                        List<RowResult> rowsResult = new List<RowResult>();
                        #region 按照实际行列，创建i行j列的存储空间
                        for (int i = 0; i < row; i++)
                        {
                            var rowResultTemp = new RowResult(new List<SingleResult>());
                            for (int j = 0; j < clum; j++)
                            {
                                rowResultTemp.SingleRowResult.Add(new SingleResult("", true));
                            }
                            rowsResult.Add(rowResultTemp);
                        }
                        #endregion
                        //for (int i = 0; i < visionTools[0].Outputs.Count; i++)  ================
                        for (int i = 0; i < visionTools[0].Outputs.Count -2; i++)
                        {
                            string strName = visionTools[0].Outputs[i].Name;
                            string strVal = visionTools[0].Outputs[i].Value.ToString();
                            int curRow = int.Parse(strName.ToArray()[1].ToString()) - 1;
                            int curCol = int.Parse(strName.ToArray()[2].ToString()) - 1;
                            SingleResult curSingleResult = rowsResult[curRow].SingleRowResult[curCol];
                            if (strName.Contains("SN"))
                                curSingleResult.SN = strVal;
                            else if (strName.Contains("RT"))
                                curSingleResult.Result = strVal.Contains("NG") ? false : true;


                            Distance_X.Text = Convert.ToDouble(visionTools[0].Outputs[visionTools[0].Outputs.Count - 2].Value).ToString("0.000");
                            DistanceX.Text = Convert.ToDouble(visionTools[0].Outputs[visionTools[0].Outputs.Count - 2].Value).ToString("0.000");
                            Distance_Y.Text = Convert.ToDouble(visionTools[0].Outputs[visionTools[0].Outputs.Count - 1].Value).ToString("0.000");
                            DistanceY.Text = Convert.ToDouble(visionTools[0].Outputs[visionTools[0].Outputs.Count - 1].Value).ToString("0.000");

                            
                        }
                        allResult.AllRowsResult.AddRange(rowsResult);
                    }
                    catch (Exception ex)
                    {
                        Log("[RunTask]", string.Format("合并AllResult结果时出错：{0}", ex.Message), LogLevel.Error);
                    }

                    //工具OK结果NG时的存图
                    bool RtNG = true;
                   // for (int i = 0; i < visionTools[0].Outputs.Count / 2; i++)   ===========
                    for (int i = 0; i < (visionTools[0].Outputs.Count - 2)/ 2; i++)
                    {
                        bool b = true;
                        if (visionTools[0].Outputs[i * 2 + 1].Value.ToString().Contains("NG"))
                        {
                            b = false;
                        }
                        RtNG = RtNG & b;
                    }

                    if (!RtNG)
                    {
                        try
                        {
                            if (!Directory.Exists(Dir_ngPictureName) && currImageSaveSetting.SaveNGPicture)
                            {
                                Directory.CreateDirectory(Dir_ngPictureName);
                            }
                            if (!Directory.Exists(Dir_ngImageName) && currImageSaveSetting.SaveNGImg)
                            {
                                Directory.CreateDirectory(Dir_ngImageName);
                            }
                            ngPictureName = Dir_ngPictureName + DateTime.Now.ToString("HHmmssfff") + ".png";
                            if (currImageSaveSetting.SaveNGPicture)
                            {
                                SaveGraphicsPictures(recorImg, ngPictureName, currImageSaveSetting.PicQuality, 0);
                            }
                            ngImageName = Dir_ngImageName + DateTime.Now.ToString("HHmmssfff") + ".bmp";
                            if (currImageSaveSetting.SaveNGImg)
                            {
                                SaveRawImage(0, ngImageName);
                            }
                        }
                        catch (Exception ex)
                        {
                            Log("[RunTask]存NG图出错：", ex.Message, LogLevel.Warning);
                        }
                    }
                    allResult.PicPath = pictureName;
                    allResult.NGPath = ngPictureName;
                }
                //工具运行NG,
                else
                {
                    UpdateDgv(1);
                    Log("[RunTask]", string.Format("整体工具运行失败，可能是模型搜索失败或者FixtureTool未成功"), LogLevel.Info);
                    //工具运行失败代码：ToolErr 
                    try
                    {
                        //var lstAllResult1= visionTools[0].Outputs;

                        //char[] RowClum = visionTools[0].Outputs[visionTools[0].Outputs.Count - 1].Name.ToString().ToCharArray();   ============
                        char[] RowClum = visionTools[0].Outputs[visionTools[0].Outputs.Count - 3].Name.ToString().ToCharArray();
                        int row = Convert.ToInt32(RowClum[1].ToString());
                        int clum = Convert.ToInt32(RowClum[2].ToString());
                        List<RowResult> rowsResult = new List<RowResult>();
                        #region 按照实际行列，创建i行j列的存储空间
                        for (int i = 0; i < row; i++)
                        {
                            var rowResultTemp = new RowResult(new List<SingleResult>());
                            for (int j = 0; j < clum; j++)
                            {
                                rowResultTemp.SingleRowResult.Add(new SingleResult("", true));
                            }
                            rowsResult.Add(rowResultTemp);
                        }
                        #endregion
                        //for (int i = 0; i < visionTools[0].Outputs.Count; i++)
                        for (int i = 0; i < visionTools[0].Outputs.Count - 2; i++)
                        {
                            string strName = visionTools[0].Outputs[i].Name;
                            string strVal = visionTools[0].Outputs[i].Value.ToString();
                            int curRow = int.Parse(strName.ToArray()[1].ToString()) - 1;
                            int curCol = int.Parse(strName.ToArray()[2].ToString()) - 1;
                            SingleResult curSingleResult = rowsResult[curRow].SingleRowResult[curCol];
                            if (strName.Contains("SN"))
                                curSingleResult.SN = strVal;
                            else if (strName.Contains("RT"))
                                curSingleResult.Result = strVal.Contains("NG") ? false : true;


                            Distance_X.Text = "999";
                            DistanceX.Text = "999";
                            Distance_Y.Text = "999";
                            DistanceY.Text = "999";

                        }
                        allResult.AllRowsResult.AddRange(rowsResult);
                    }
                    catch (Exception ex)
                    {
                        Log("[RunTask]", string.Format("添加位置{0}ToolUnAccept运行结果时出错：{1}", 0, ex.Message), LogLevel.Error);
                    }
                    if (!Directory.Exists(Dir_ngPictureName) && currImageSaveSetting.SaveNGPicture)
                    {
                        Directory.CreateDirectory(Dir_ngPictureName);
                    }
                    if (!Directory.Exists(Dir_ngImageName) && currImageSaveSetting.SaveNGImg)
                    {
                        Directory.CreateDirectory(Dir_ngImageName);
                    }
                    ngPictureName = Dir_ngPictureName + DateTime.Now.ToString("HHmmssfff") + ".png";
                    if (currImageSaveSetting.SaveNGPicture)
                    {
                        SaveGraphicsPictures(recorImg, ngPictureName, currImageSaveSetting.PicQuality, 0);
                    }

                    ngImageName = Dir_ngImageName + DateTime.Now.ToString("HHmmssfff") + ".bmp";
                    if (currImageSaveSetting.SaveNGImg)
                    {
                        SaveRawImage(0, ngImageName);
                    }
                    allResult.PicPath = pictureName;
                    allResult.NGPath = ngPictureName;
                }

                string result = JsonConvert.SerializeObject(allResult);//AllResult
                //运行完成发送结果
                //tServer.SendToClient(currImageSaveSetting.IPAdr, result);
                mqRet.SendMsg(result);
                Log("[SendMsg]", string.Format("结果:{0}", result), LogLevel.Info);

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public ICogImage GetImg(IntPtr pData, MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo)
        {
            ICogImage image = null;
            Bitmap bmp = null;
            while (bmp == null)
            {
                // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
                uint m_nBufSizeForDriver = 3072 * 2048 * 3;
                byte[] m_pBufForDriver = new byte[3072 * 2048 * 3];

                // ch:用于保存图像的缓存 | en:Buffer for saving image
                uint m_nBufSizeForSaveImage = 3072 * 2048 * 3 * 3 + 2048;
                byte[] m_pBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];
                int nRet;
                uint nPayloadSize = 0;
                MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
                nRet = mCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);//一帧数据的大小
                if (MyCamera.MV_OK != nRet)
                {
                    bmp = null;
                    continue;
                }
                nPayloadSize = stParam.nCurValue;
                if (nPayloadSize > m_nBufSizeForDriver)
                {
                    m_nBufSizeForDriver = nPayloadSize;
                    m_pBufForDriver = new byte[m_nBufSizeForDriver];

                    // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                    // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                    m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 3000;
                    m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
                }
                MyCamera.MvGvspPixelType enDstPixelType;
                if (IsMonoData(stFrameInfo.enPixelType))
                {
                    enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
                }
                else if (IsColorData(stFrameInfo.enPixelType))
                {
                    enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
                }
                else
                {
                    bmp = null;
                    continue;
                }

                IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
                MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
                MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
                stConverPixelParam.nWidth = stFrameInfo.nWidth;
                stConverPixelParam.nHeight = stFrameInfo.nHeight;
                stConverPixelParam.pSrcData = pData;
                stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
                stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
                stConverPixelParam.enDstPixelType = enDstPixelType;
                stConverPixelParam.pDstBuffer = pImage;
                stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
                nRet = mCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
                if (MyCamera.MV_OK != nRet)
                {
                    bmp = null;
                    continue;
                }

                if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
                {
                    //************************Mono8 转 Bitmap*******************************
                    bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                    ColorPalette cp = bmp.Palette;
                    // init palette
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    // set palette back
                    bmp.Palette = cp;
                    //bmp.Save(@"C:\Users\20857\Desktop\1\1.bmp");
                }
                else
                {
                    //*********************RGB8 转 Bitmap**************************
                    for (int i = 0; i < stFrameInfo.nHeight; i++)
                    {
                        for (int j = 0; j < stFrameInfo.nWidth; j++)
                        {
                            byte chRed = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3];
                            m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2];
                            m_pBufForSaveImage[i * stFrameInfo.nWidth * 3 + j * 3 + 2] = chRed;
                        }
                    }
                    try
                    {
                        bmp = new Bitmap(stFrameInfo.nWidth, stFrameInfo.nHeight, stFrameInfo.nWidth * 3, PixelFormat.Format24bppRgb, pImage);
                    }
                    catch
                    {
                        bmp = null;
                        continue;
                    }

                }
                image = new CogImage8Grey(bmp);
            }
            return image;
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        public void CloseCamera()
        {
            if (MyCamera.MV_OK != mCamera.MV_CC_StopGrabbing_NET())//停止采集
            {
                Log("[CloseCamera]", "相机停止采集失败", LogLevel.Error);
                //MessageBox.Show("相机停止采集失败");
                //return;
            }

            if (MyCamera.MV_OK != mCamera.MV_CC_CloseDevice_NET())//关闭设备
            {
                Log("[CloseCamera]", "相机关闭失败", LogLevel.Error);
                MessageBox.Show("相机关闭失败");
                //return;
            }

            if (MyCamera.MV_OK != mCamera.MV_CC_DestroyDevice_NET())//销毁句柄，释放资源
            {
                Log("[CloseCamera]", "释放相机资源失败", LogLevel.Error);
                MessageBox.Show("释放相机资源失败");
            }
        }
        #endregion

        #region 海康相机Basic_Demo

        bool m_bGrabbing;

        // ch:显示错误信息 | en:Show error message
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            string errorMsg;
            if (nErrorNum == 0)
            {
                errorMsg = csMessage;
            }
            else
            {
                errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
            }

            switch (nErrorNum)
            {
                case MyCamera.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                case MyCamera.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                case MyCamera.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                case MyCamera.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                case MyCamera.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                case MyCamera.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                case MyCamera.MV_E_NODATA: errorMsg += " No data "; break;
                case MyCamera.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                case MyCamera.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                case MyCamera.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                case MyCamera.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                case MyCamera.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                case MyCamera.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                case MyCamera.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                case MyCamera.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                case MyCamera.MV_E_NETER: errorMsg += " Network error "; break;
            }

            MessageBox.Show(errorMsg, "PROMPT");
        }

        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;

                default:
                    return false;
            }
        }

        /************************************************************************
         *  @fn     IsColorData()
         *  @brief  判断是否是彩色数据
         *  @param  enGvspPixelType         [IN]           像素格式
         *  @return 成功，返回0；错误，返回-1 
         ************************************************************************/
        private Boolean IsColorData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGR12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerRG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerGB12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_BayerBG12_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YUV422_YUYV_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_YCBCR411_8_CBYYCRYY:

                    return true;

                default:
                    return false;
            }
        }

        private void bnEnum_Click(object sender, EventArgs e)
        {
            DeviceListAcq();
        }

        private void DeviceListAcq()
        {
            int nRet;
            // ch:创建设备列表 en:Create Device List
            System.GC.Collect();
            cbDeviceList.Items.Clear();
            nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceInforList);
            if (0 != nRet)
            {
                ShowErrorMsg("获取相机列表失败", 0);
                return;
            }

            // ch:在窗体列表中显示设备名 | en:Display device name in the form list
            for (int i = 0; i < deviceInforList.nDeviceNum; i++)
            {
                device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceInforList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));
                if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                    if (gigeInfo.chUserDefinedName != "")
                    {
                        cbDeviceList.Items.Add("GigE: " + gigeInfo.chUserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("GigE: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                    }
                }
                else if (device.nTLayerType == MyCamera.MV_USB_DEVICE)
                {
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(device.SpecialInfo.stUsb3VInfo, 0);
                    MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                    if (usbInfo.chUserDefinedName != "")
                    {
                        cbDeviceList.Items.Add("USB: " + usbInfo.chUserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                    }
                    else
                    {
                        cbDeviceList.Items.Add("USB: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                    }
                }
            }

            // ch:选择第一项 | en:Select the first item
            if (deviceInforList.nDeviceNum != 0)
            {
                cbDeviceList.SelectedIndex = 0;
            }
        }

        private void SetCtrlWhenOpen()
        {
            bnOpen.Enabled = false;

            //bnClose.Enabled = true;
            bnStartGrab.Enabled = true;
            bnStopGrab.Enabled = false;
            bnContinuesMode.Enabled = true;
            bnContinuesMode.Checked = true;
            bnTriggerMode.Enabled = true;
            cbSoftTrigger.Enabled = false;
            bnTriggerExec.Enabled = false;

            tbExposure.Enabled = true;
            tbGain.Enabled = true;
            tbFrameRate.Enabled = true;
            bnGetParam.Enabled = true;
            bnSetParam.Enabled = true;
            bn_Triger.Enabled = true;

            btn_浏览.Enabled = true;
        }

        private void bnOpen_Click(object sender, EventArgs e)
        {
            if (deviceInforList.nDeviceNum == 0 || cbDeviceList.SelectedIndex == -1)
            {
                ShowErrorMsg("无相机,请选择相机", 0);
            }
            int nRet = -1;

            try
            {
                // ch:获取选择的设备信息 | en:Get selected device information//设备信息结构体指针 转 设备信息结构体
                device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceInforList.pDeviceInfo[cbDeviceList.SelectedIndex], typeof(MyCamera.MV_CC_DEVICE_INFO));
            }
            catch (Exception ex)
            {
                ShowErrorMsg("获取相机失败" + ex.Message, 0);
            }

            // ch:打开设备 | en:Open device
            if (null == mCamera)
            {
                mCamera = new MyCamera();
                if (null == mCamera)
                {
                    ShowErrorMsg("创建相机失败", 0);
                }
            }

            nRet = mCamera.MV_CC_CreateDevice_NET(ref device);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("连接相机失败", 0);
            }

            nRet = mCamera.MV_CC_OpenDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                mCamera.MV_CC_DestroyDevice_NET();
                ShowErrorMsg("相机打开失败", nRet);
            }

            // ch:获取并设置网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = mCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = mCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                    {
                        Console.WriteLine("Warning: Set Packet Size failed {0:x8}", nRet);
                    }
                }
                else
                {
                    Console.WriteLine("Warning: Get Packet Size failed {0:x8}", nPacketSize);
                }
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            mCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", 2);// ch:工作在连续模式 | en:Acquisition On Continuous Mode
            mCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);    // ch:连续模式 | en:Continuous

            bnGetParam_Click(null, null);// ch:获取参数 | en:Get parameters

            // ch:控件操作 | en:Control operation
            SetCtrlWhenOpen();
        }

        private void SetCtrlWhenClose()
        {
            bnOpen.Enabled = true;

            //bnClose.Enabled = false;
            bnStartGrab.Enabled = false;
            bnStopGrab.Enabled = false;
            bnContinuesMode.Enabled = false;
            bnTriggerMode.Enabled = false;
            cbSoftTrigger.Enabled = false;
            bnTriggerExec.Enabled = false;
            bn_Triger.Enabled = false;

            bnSaveBmp.Enabled = false;
            bnSaveJpg.Enabled = false;
            tbExposure.Enabled = false;
            tbGain.Enabled = false;
            tbFrameRate.Enabled = false;
            bnGetParam.Enabled = false;
            bnSetParam.Enabled = false;

        }

        public void CloseClick()
        {
            // ch:关闭设备 | en:Close Device
            int nRet;

            nRet = mCamera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            nRet = mCamera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenClose();

            // ch:取流标志位清零 | en:Reset flow flag bit
            m_bGrabbing = false;
        }

        private void bnTriggerMode_CheckedChanged(object sender, EventArgs e)
        {
            // ch:打开触发模式 | en:Open Trigger Mode
            if (bnTriggerMode.Checked)
            {
                mCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);

                // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
                //               1 - Line1;
                //               2 - Line2;
                //               3 - Line3;
                //               4 - Counter;
                //               7 - Software;
                if (cbSoftTrigger.Checked)
                {
                    mCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                    if (m_bGrabbing)
                    {
                        bnTriggerExec.Enabled = true;
                    }
                }
                else
                {
                    mCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
                }
                cbSoftTrigger.Enabled = true;
            }

        }

        private void SetCtrlWhenStartGrab()
        {
            bnStartGrab.Enabled = false;
            bnStopGrab.Enabled = true;

            if (bnTriggerMode.Checked && cbSoftTrigger.Checked)
            {
                bnTriggerExec.Enabled = true;
            }

            bnSaveBmp.Enabled = true;
            bnSaveJpg.Enabled = true;
        }

        private void bnStartGrab_Click(object sender, EventArgs e)
        {
            int nRet;

            // ch:开始采集 | en:Start Grabbing
            nRet = mCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("触发失败", nRet);
                return;
            }

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStartGrab();

            // ch:标志位置位true | en:Set position bit true
            m_bGrabbing = true;


            // ch:显示 | en:Display
            nRet = mCamera.MV_CC_Display_NET(Display_debug.Handle);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("显示失败！", nRet);
            }
        }

        private void cbSoftTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSoftTrigger.Checked)
            {
                // ch:触发源设为软触发 | en:Set trigger source as Software
                mCamera.MV_CC_SetEnumValue_NET("TriggerSource", 7);
                if (m_bGrabbing)
                {
                    bnTriggerExec.Enabled = true;
                }
            }
            else
            {
                mCamera.MV_CC_SetEnumValue_NET("TriggerSource", 0);
                bnTriggerExec.Enabled = false;
                bn_Triger.Enabled = true;
            }
        }

        private void bnTriggerExec_Click(object sender, EventArgs e)
        {
            int nRet;

            // ch:触发命令 | en:Trigger command
            nRet = mCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("触发失败", nRet);
            }
        }

        private void SetCtrlWhenStopGrab()
        {
            bnStartGrab.Enabled = true;
            bnStopGrab.Enabled = false;

            bnTriggerExec.Enabled = false;

            bnSaveBmp.Enabled = false;
            bnSaveJpg.Enabled = false;
        }
        private void bnStopGrab_Click(object sender, EventArgs e)
        {
            int nRet = -1;
            // ch:停止采集 | en:Stop Grabbing
            nRet = mCamera.MV_CC_StopGrabbing_NET();
            if (nRet != MyCamera.MV_OK)
            {
                ShowErrorMsg("停止采集失败!", nRet);
            }

            // ch:标志位设为false | en:Set flag bit false
            m_bGrabbing = false;

            // ch:控件操作 | en:Control Operation
            SetCtrlWhenStopGrab();

        }

        private void bnSaveBmp_Click(object sender, EventArgs e)
        {
            int nRet;
            UInt32 nPayloadSize = 0;
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = mCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Get PayloadSize failed", nRet);
                return;
            }
            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            MyCamera.MV_FRAME_OUT_INFO_EX frame = new MyCamera.MV_FRAME_OUT_INFO_EX();
            // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec

            nRet = mCamera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref frame, 1000);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("No Data!", nRet);
                return;
            }

            MyCamera.MvGvspPixelType enDstPixelType;
            if (IsMonoData(frame.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8;
            }
            else if (IsColorData(frame.enPixelType))
            {
                enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;
            }
            else
            {
                ShowErrorMsg("No such pixel type!", 0);
                return;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();

            stConverPixelParam.nWidth = frame.nWidth;
            stConverPixelParam.nHeight = frame.nHeight;
            stConverPixelParam.pSrcData = pData;
            stConverPixelParam.nSrcDataLen = frame.nFrameLen;
            stConverPixelParam.enSrcPixelType = frame.enPixelType;
            stConverPixelParam.enDstPixelType = enDstPixelType;
            stConverPixelParam.pDstBuffer = pImage;
            stConverPixelParam.nDstBufferSize = m_nBufSizeForSaveImage;
            nRet = mCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
            //Log("HK位图格式", string.Format("nwidth:{0},nHeight:{1},nFrameLen:{2},enPixelType:{3}", frame.nWidth, frame.nHeight, frame.nFrameLen, frame.enPixelType),LogLevel.Communicate);
            if (MyCamera.MV_OK != nRet)
            {
                return;
            }

            if (enDstPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8)
            {
                //************************Mono8 转 Bitmap*******************************
                Bitmap bmp = new Bitmap(frame.nWidth, frame.nHeight, frame.nWidth * 1, PixelFormat.Format8bppIndexed, pImage);

                ColorPalette cp = bmp.Palette;
                // init palette
                for (int i = 0; i < 256; i++)
                {
                    cp.Entries[i] = Color.FromArgb(i, i, i);
                }
                // set palette back
                bmp.Palette = cp;

                if (Directory.Exists(currImageSaveSetting.DebugImgDir))
                {
                    Directory.CreateDirectory(currImageSaveSetting.DebugImgDir);
                }
                bmp.Save(currImageSaveSetting.DebugImgDir + DateTime.Now.ToString("mmdd_hhMMssfff") + ".bmp", ImageFormat.Bmp);
            }
            else
            {
                //*********************RGB8 转 Bitmap**************************
                for (int i = 0; i < frame.nHeight; i++)
                {
                    for (int j = 0; j < frame.nWidth; j++)
                    {
                        byte chRed = m_pBufForSaveImage[i * frame.nWidth * 3 + j * 3];
                        m_pBufForSaveImage[i * frame.nWidth * 3 + j * 3] = m_pBufForSaveImage[i * frame.nWidth * 3 + j * 3 + 2];
                        m_pBufForSaveImage[i * frame.nWidth * 3 + j * 3 + 2] = chRed;
                    }
                }
                try
                {
                    Bitmap bmp = new Bitmap(frame.nWidth, frame.nHeight, frame.nWidth * 3, PixelFormat.Format24bppRgb, pImage);

                    if (Directory.Exists(currImageSaveSetting.DebugImgDir))
                    {
                        Directory.CreateDirectory(currImageSaveSetting.DebugImgDir);
                    }
                    bmp.Save(currImageSaveSetting.DebugImgDir + DateTime.Now.ToString("mmdd_hhMMssfff") + ".bmp", ImageFormat.Bmp);
                }
                catch
                {
                }

            }
            ShowErrorMsg("Save Succeed!", 0);
        }

        private void bnSaveJpg_Click(object sender, EventArgs e)
        {
            int nRet;
            UInt32 nPayloadSize = 0;
            MyCamera.MVCC_INTVALUE stParam = new MyCamera.MVCC_INTVALUE();
            nRet = mCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Get PayloadSize failed", nRet);
                return;
            }
            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];

                // ch:同时对保存图像的缓存做大小判断处理 | en:Determine the buffer size to save image
                // ch:BMP图片大小：width * height * 3 + 2048(预留BMP头大小) | en:BMP image size: width * height * 3 + 2048 (Reserved for BMP header)
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }

            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

            // ch:超时获取一帧，超时时间为1秒 | en:Get one frame timeout, timeout is 1 sec
            nRet = mCamera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForDriver, ref stFrameInfo, 1000);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("No Data!", nRet);
                return;
            }

            IntPtr pImage = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForSaveImage, 0);
            MyCamera.MV_SAVE_IMAGE_PARAM_EX stSaveParam = new MyCamera.MV_SAVE_IMAGE_PARAM_EX();
            stSaveParam.enImageType = MyCamera.MV_SAVE_IAMGE_TYPE.MV_Image_Jpeg;
            stSaveParam.enPixelType = stFrameInfo.enPixelType;
            stSaveParam.pData = pData;
            stSaveParam.nDataLen = stFrameInfo.nFrameLen;
            stSaveParam.nHeight = stFrameInfo.nHeight;
            stSaveParam.nWidth = stFrameInfo.nWidth;
            stSaveParam.pImageBuffer = pImage;
            stSaveParam.nBufferSize = m_nBufSizeForSaveImage;
            stSaveParam.nJpgQuality = 80;
            nRet = mCamera.MV_CC_SaveImageEx_NET(ref stSaveParam);
            if (MyCamera.MV_OK != nRet)
            {
                ShowErrorMsg("Save Fail!", 0);
                return;
            }

            try
            {
                if (Directory.Exists(currImageSaveSetting.DebugImgDir))
                {
                    Directory.CreateDirectory(currImageSaveSetting.DebugImgDir);
                }
                FileStream file = new FileStream(currImageSaveSetting.DebugImgDir + DateTime.Now.ToString("mmdd_hhMMssfff") + ".jpg", FileMode.Create, FileAccess.Write);
                file.Write(m_pBufForSaveImage, 0, (int)stSaveParam.nImageLen);
                file.Close();
            }
            catch { }
            ShowErrorMsg("Save Succeed!", 0);
        }

        private void bnGetParam_Click(object sender, EventArgs e)
        {
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = mCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                tbExposure.Text = stParam.fCurValue.ToString("F1");
            }

            nRet = mCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                mCamera.MV_CC_GetFloatValue_NET("Gain", ref stParam);
                tbGain.Text = stParam.fCurValue.ToString("F1");
            }

            nRet = mCamera.MV_CC_GetFloatValue_NET("ExposureTime", ref stParam);
            if (MyCamera.MV_OK == nRet)
            {
                mCamera.MV_CC_GetFloatValue_NET("ResultingFrameRate", ref stParam);
                tbFrameRate.Text = stParam.fCurValue.ToString("F1");
            }
        }

        private void bnSetParam_Click(object sender, EventArgs e)
        {
            int nRet;
            mCamera.MV_CC_SetEnumValue_NET("ExposureAuto", 0);

            try
            {
                float.Parse(tbExposure.Text);
                float.Parse(tbGain.Text);
                float.Parse(tbFrameRate.Text);
            }
            catch
            {
                ShowErrorMsg("Please enter correct type!", 0);
                return;
            }
            try
            {
                nRet = mCamera.MV_CC_SetFloatValue_NET("ExposureTime", float.Parse(tbExposure.Text));
                if (nRet != MyCamera.MV_OK)
                {
                    ShowErrorMsg("Set Exposure Time Fail!", nRet);
                    Log("[bnSetParam]", "手动设置曝光失败：" + nRet, LogLevel.Error);
                }
                else
                {
                    currProductInfo.cameraInfo.Exposure = Convert.ToDouble(tbExposure.Text);
                    Log("[bnSetParam]", "手动设置曝光：" + tbExposure.Text, LogLevel.Info);
                }

                mCamera.MV_CC_SetEnumValue_NET("GainAuto", 0);
                nRet = mCamera.MV_CC_SetFloatValue_NET("Gain", float.Parse(tbGain.Text));
                if (nRet != MyCamera.MV_OK)
                {
                    ShowErrorMsg("Set Gain Fail!", nRet);
                    Log("[bnSetParam]", "手动设置增益失败：" + nRet, LogLevel.Error);
                }
                else
                {
                    currProductInfo.cameraInfo.Gain = Convert.ToDouble(tbGain.Text);
                    Log("[bnSetParam]", "手动设置增益：" + tbGain.Text, LogLevel.Info);
                }

                nRet = mCamera.MV_CC_SetFloatValue_NET("AcquisitionFrameRate", float.Parse(tbFrameRate.Text));
                if (nRet != MyCamera.MV_OK)
                {
                    ShowErrorMsg("Set Frame Rate Fail!", nRet);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMsg("参数类型错误：" + ex.Message, 0);
            }

        }

        private void btn_浏览_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog imgDialog = new OpenFileDialog();
                imgDialog.InitialDirectory = Application.StartupPath + "\\DebugImage";
                imgDialog.Filter = "|*.bmp|*.jpg|*.png|*.PNG|*.BMP";
                imgDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                Log("[btn_浏览_Click]", ex.Message, LogLevel.Error);
            }
        }

        private void bn_SaveCameraPar_Click(object sender, EventArgs e)
        {
            try
            {
                currProductInfo.cameraInfo.Exposure = Convert.ToDouble(tbExposure.Text);
                currProductInfo.cameraInfo.Gain = Convert.ToDouble(tbGain.Text);

                try
                {
                    XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                    Log("[bn_SaveCameraPar_Click]", "保存相机参数至本地", LogLevel.Info);
                    MessageBox.Show("OK");
                }
                catch (Exception ex)
                {
                    Log("[bn_SaveCameraPar_Click]", ex.Message, LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Log("[bn_SaveCameraPar_Click]", ex.Message, LogLevel.Error);
            }
        }

        private void bn_set_lightPar_Click(object sender, EventArgs e)
        {
            if (currProductInfo.HGSlights.Used)
            {
                try
                {
                    //bool bResXML = cls_mainParm.cls_readsaveXML.readXMLAndsetParm();
                    //List<NumericUpDown> numUDCH = new List<NumericUpDown>();
                    //numUDCH.Add(unmUDCH1);
                    //numUDCH.Add(unmUDCH2);
                    //numUDCH.Add(unmUDCH3);
                    //numUDCH.Add(unmUDCH4);
                    //for (int i = 0; i < 4; i++)
                    //{
                    //    cls_Channel temp_cls_Chan1 = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i].MyCopyTo();
                    //    //temp_cls_Chan1.stuLight.nDelay = Convert.ToInt32(tempTextDelyTimeBoxs[i].Text);
                    //    temp_cls_Chan1.stuLight.nWidth = Convert.ToInt32(numUDCH[i].Value);
                    //    cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i] = temp_cls_Chan1.MyCopyTo();
                    //    SendParm(cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i]);
                    //}

                    cls_Channel ch1 = new cls_Channel();
                    ch1.strChannel = "CH1";
                    ch1.stuLight.nWidth = Convert.ToInt32(unmUDCH1.Value);
                    SendParm(ch1);
                }
                catch (Exception ex)
                {
                    Log("[set_lightPar_Click]", ex.Message, LogLevel.Error);
                    MessageBox.Show("设置HGS失败:" + ex.Message);
                }
            }
            else if (currProductInfo.CSTlights.Used)
            {
                try
                {
                    //serialPort1.Write(string.Format("SA{0}#", unmUDCH1.Text));
                }
                catch (Exception ex)
                {
                    Log("[set_lightPar_Click]", ex.Message, LogLevel.Error);
                    MessageBox.Show("设置CST失败:" + ex.Message);
                }
            }
        }

        private void bn_save_lightPar_Click(object sender, EventArgs e)
        {
            try
            {
                if (currProductInfo.HGSlights.Used)
                {
                    currProductInfo.HGSlights.CH1.pulseWidth = unmUDCH1.Value.ToString();
                    currProductInfo.HGSlights.CH2.pulseWidth = unmUDCH2.Value.ToString();
                    currProductInfo.HGSlights.CH3.pulseWidth = unmUDCH3.Value.ToString();
                    currProductInfo.HGSlights.CH4.pulseWidth = unmUDCH4.Value.ToString();
                }
                else if (currProductInfo.CSTlights.Used)
                {
                    currProductInfo.CSTlights.CH1.pulseWidth = unmUDCH1.Value.ToString("0000");
                    currProductInfo.CSTlights.CH2.pulseWidth = unmUDCH2.Value.ToString("0000");
                    currProductInfo.CSTlights.CH3.pulseWidth = unmUDCH3.Value.ToString("0000");
                    currProductInfo.CSTlights.CH4.pulseWidth = unmUDCH4.Value.ToString("0000");
                }
                try
                {
                    XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                    Log("[bn_save_lightPar_Click]", "保存光源参数至本地", LogLevel.Info);
                    MessageBox.Show("OK");
                }
                catch (Exception ex)
                {
                    Log("[bn_save_lightPar_Click]", ex.Message, LogLevel.Error);
                }
            }
            catch (Exception ex)
            {
                Log("[bn_save_lightPar_Click]", ex.Message, LogLevel.Error);
            }
        }

        #endregion

        #region Log记录
        SimpleLogger mLog = new SimpleLogger(@"D:\Lead2DParameter\RunLog");


        /// <summary>
        /// 记录
        /// </summary>
        /// <param name="title">title</param>
        /// <param name="Content">content</param>
        /// <param name="logLevel">1.日常  2.提醒  3.警告</param>
        public void Log(string title, string Content, LogLevel level)
        {
            Invoke(new Action(() =>
            {
                if (listBoxRunLog.Items.Count >= 1500)
                {
                    listBoxRunLog.Items.Clear();
                }
                mLog.WriteInfoLog(title, level, Content);
                listBoxRunLog.Items.Add(DateTime.Now.ToString("MM-dd HH:mm:ss.fff"));
                listBoxRunLog.Items.Add(title + ":" + Content);
                listBoxRunLog.SelectedIndex = listBoxRunLog.Items.Count - 1;
            }
            ));
        }

        private void listBoxRunLog_DoubleClick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                listBoxRunLog.Items.Clear();
            }));
        }
        #endregion

        #region 测试


        //TCPIP_Test

        SingleClient singleClient;
        public void RegistrationEvent()
        {
            singleClient.UpdateClientMSG += new SingleClient.UpdateObjectDelegate(SingleClient_UpdateClientMSG);
            singleClient.UpdateClientErrorMSG += new SingleClient.UpdateObjectDelegate(SingleClient_UpdateClientErrorMSG);
            singleClient.UpdateClientData += new SingleClient.UpdateObjectDelegate(SingleClient_UpdateClientData);
        }

        private void SingleClient_UpdateClientMSG(object sender)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (listBox_TCPIP.Items.Count > 1000)
                    {
                        listBox_TCPIP.Items.Clear();
                    }
                    listBox_TCPIP.Items.Add("[UpdateClientMSG]" + sender.ToString());
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    listBox_TCPIP.Items.Add("[UpdateClientMSG_ERR]" + ex.Message);
                }));
            }
        }

        private void SingleClient_UpdateClientErrorMSG(object sender)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (listBox_TCPIP.Items.Count > 1000)
                    {
                        listBox_TCPIP.Items.Clear();
                    }
                    listBox_TCPIP.Items.Add("[UpdateClientErrorMSG]" + sender.ToString());
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    listBox_TCPIP.Items.Add("[UpdateClientErrorMSG_ERR]" + ex.Message);
                }));
            }
        }

        private void SingleClient_UpdateClientData(object sender)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    listBox_TCPIP.Items.Add("[UpdateClientData]" + sender.ToString());
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    listBox_TCPIP.Items.Add("[UpdateClientData_ERR]" + ex.Message);
                }));
            }
        }

        private void IP_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                this.Invoke(new Action(() =>
                {
                    singleClient = new SingleClient(IPClient.Text.Trim(), Convert.ToInt32(PortClient.Text.Trim()));
                }));
                RegistrationEvent();
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    listBox_TCPIP.Items.Add(ex.Message);
                }));
            }
        }

        private void IP_Disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                singleClient.Close();
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    listBox_TCPIP.Items.Add(ex.Message);
                }));
            }
        }

        private void listBox_TCPIP_DoubleClick(object sender, EventArgs e)
        {
            listBox_TCPIP.Items.Clear();
        }

        private void btn_singleTrg_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                singleClient.SendData(txt_Index.Text.Trim());
            }));
        }

        /// <summary>
        /// 离线测试用
        /// </summary>
        /// 
        private void bn_modifyIP_Click(object sender, EventArgs e)
        {
            currImageSaveSetting.IPAdr = txtIP.Text;
            currImageSaveSetting.Port = txtPort.Text;
            XmlSerializerHelper.WriteXML((Object)currImageSaveSetting, ImgConfigPath, typeof(ImageSaveSetting));
            CreatServe();
            Log("[modifyIP_Click]", string.Format("设置IP{0},端口{1}成功", currImageSaveSetting.IPAdr, currImageSaveSetting.Port), LogLevel.Info);
        }

        //test
        private void btn_SimulationStart_Click(object sender, EventArgs e)
        {
            //imgsPath
            int time = 0;
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i < Convert.ToInt32(txt_SimTotalNum.Text); i++)
                {
                    string ofinputImgName = OffsetTool.Inputs.Contains("InputImage") ? "InputImage" : "OutputImage";
                    RunTask(new CogImage8Grey(new Bitmap(imgsPath[i])));
                    this.Invoke(new Action(() =>
                    {
                        time = Convert.ToInt32(txt_SimTime.Text);
                    }));
                    Thread.Sleep(time);
                }
            });
            thread.IsBackground = true;
            thread.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MqCType_OnMsmqRcvedEvent(tb_Type.Text.Trim());
        }


        #endregion

        #region 光源控制

        public void InitLight()
        {
            if (currProductInfo.HGSlights.Used)
            {
                //HGSCotrol_Loaded();
                //连接
                HGSControl_Load();
                Control_Limited(CST_Group, null, false);
            }
            else if (currProductInfo.CSTlights.Used)
            {
                CSTControl_Load();
                Control_Limited(HGS_Group, null, false);
            }
            UpdateLightParam();
        }

        public void UpdateLightParam()
        {
            if (currProductInfo.HGSlights.Used)
            {
                try
                {
                    unmUDCH1.Value = Convert.ToDecimal(currProductInfo.HGSlights.CH1.pulseWidth);

                    cls_Channel ch1 = new cls_Channel();
                    ch1.strChannel = "CH1";
                    ch1.stuLight.nWidth = Convert.ToInt32(unmUDCH1.Value);
                    ch1.stuLight.nDelay = 0;
                    SendParm(ch1);
                }
                catch (Exception ex)
                {
                    Log("[UpdateLightParam]", ex.Message, LogLevel.Error);
                    MessageBox.Show("更新HGS失败:" + ex.Message);
                }
            }
            else if (currProductInfo.CSTlights.Used)
            {
                try
                {
                    unmUDCH1.Value = Convert.ToDecimal(currProductInfo.CSTlights.CH1.pulseWidth);
                    unmUDCH2.Value = Convert.ToDecimal(currProductInfo.CSTlights.CH2.pulseWidth);
                    unmUDCH3.Value = Convert.ToDecimal(currProductInfo.CSTlights.CH3.pulseWidth);
                    unmUDCH4.Value = Convert.ToDecimal(currProductInfo.CSTlights.CH4.pulseWidth);
                    //serialPort1.Write(string.Format("SA{0}#SB{1}#SC{2}#SD{3}#", currProductInfo.CSTlights.CH1.pulseWidth, currProductInfo.CSTlights.CH2.pulseWidth, currProductInfo.CSTlights.CH3.pulseWidth, currProductInfo.CSTlights.CH4.pulseWidth));
                }
                catch (Exception ex)
                {
                    Log("[UpdateLightParam]", ex.Message, LogLevel.Error);
                    MessageBox.Show("更新CST失败:" + ex.Message);
                }
            }
        }

        #endregion

        #region HGS光源控制

        private 多通道数字电源_Dll multDigital_Dll = new 多通道数字电源_Dll();
        BX_SetIPParm_Dll dll_setIP = new BX_SetIPParm_Dll();

        Class_mainParm cls_mainParm = new Class_mainParm();
        public delegateRecvInfo1 Recv1;
        public DelegateRecvLight recv_Light;
        public DelegateRecvSystemParm recv_SystemParm;

        public SemaphoreSlim m_liandongEndSemaphore;

        //public SemaphoreSlim m_liandongEndSemaphore;

        bool bPortconnect = false;//串口连接打开
        //TCP连接打开
        bool bTCPIPconnect = false;

        private void delegateRecvLight(object o)
        {
        }

        private void delegateRecvSystemParm(object o)
        {
        }

        private void HGSCotrol_Loaded()
        {
            //初始化
            bool bResXML = cls_mainParm.cls_readsaveXML.readXMLAndsetParm();    //读取打标文件的XML并且把参数设置到界面上

            if (cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    cls_Channel cls_chTemp = new cls_Channel();
                    cls_chTemp.strChannel = string.Format("CH{0}", i + 1);
                    cls_chTemp.nTrig = i;
                    cls_chTemp.nLight = i;
                    cls_chTemp.nTG = i;
                    cls_chTemp.nLightValue = 100;

                    cls_chTemp.stuLight.nDelay = 10;
                    cls_chTemp.stuLight.nWidth = 30;
                    cls_chTemp.stuOut.nDelay = 10;
                    cls_chTemp.stuOut.nWidth = 10;
                    cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel.Add(cls_chTemp);
                }

            }


            //tbxIP.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.strIP;
            //cmb_port.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.strPort;


            List<string> list_strport = new List<string>();
            // digitalDll.UpdateSerialPort(ref list_strport);
            //cmb_port.Items.Clear();
            //for (int i = 0; i < list_strport.Count; i++)
            //{
            //    cmb_port.Items.Add(list_strport[i]);
            //}

            Recv1 = new delegateRecvInfo1(delegateRecvInfo1);
            multDigital_Dll.RegistEvent1(ref Recv1);

            recv_Light = new DelegateRecvLight(delegateRecvLight);
            multDigital_Dll.RegistEvent_Light(ref recv_Light);

            recv_SystemParm = new DelegateRecvSystemParm(delegateRecvSystemParm);
            multDigital_Dll.RegistEvent_SystemParm(ref recv_SystemParm);

            m_liandongEndSemaphore = new SemaphoreSlim(0);
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cls_mainParm.cls_readsaveXML.saveXML();
        }

        private void delegateRecvInfo1(object o)
        {
            string strrecvData = (string)o;
            this.Invoke(new Action(() =>
            {
                this.textBox_data.Text = strrecvData;

                if (strrecvData.IndexOf("OK") != -1)
                {
                    m_liandongEndSemaphore.Release();   //收到回执
                }

                if (strrecvData == "OK-D")
                {
                    string strCh_TrigR = "";   //获取Trig通道
                    int nLedORCameraR = 0;
                    eRunMode eRunModeR = new eRunMode();
                    List<_stuPulseCurve> list_stuCurveR = new List<_stuPulseCurve>(0);
                    multDigital_Dll.GetFlashParm(ref eRunModeR, ref nLedORCameraR, ref strCh_TrigR, ref list_stuCurveR);

                    SetInterface(list_stuCurveR);   //设置界面
                }
            }));

        }

        //设置界面
        private void SetInterface(List<_stuPulseCurve> list_stuCurveRIn)
        {
            try
            {
                List<cls_Channel> cls_Channels = new List<cls_Channel>(4);
                for (int i = 0; i < list_stuCurveRIn.Count; i++)
                {
                    cls_Channels[i] = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i].MyCopyTo();
                    if ((list_stuCurveRIn[i].strName == class_CurveName.strnamesL[cls_Channels[i].nLight]) &&
                        (list_stuCurveRIn[i].list_stuPulse.Count > 0))
                    {
                        cls_Channels[i].stuLight.nDelay = list_stuCurveRIn[i].list_stuPulse[0].nDelay;
                        cls_Channels[i].stuLight.nWidth = list_stuCurveRIn[i].list_stuPulse[0].nWidth;
                    }

                    if ((list_stuCurveRIn[i].strName == class_CurveName.strnamesT[cls_Channels[i].nTG]) &&
                        (list_stuCurveRIn[i].list_stuPulse.Count > 0))
                    {
                        cls_Channels[i].stuOut.nDelay = list_stuCurveRIn[i].list_stuPulse[0].nDelay;
                        cls_Channels[i].stuOut.nWidth = list_stuCurveRIn[i].list_stuPulse[0].nWidth;
                    }

                    cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i] = cls_Channels[i].MyCopyTo();
                }

                textBox_lightTime1.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[0].stuLight.nWidth.ToString(); //发光时间
                CH_SendDelay1.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[0].stuLight.nDelay.ToString(); //发光延时

                textBox_lightTime2.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[1].stuLight.nWidth.ToString(); //发光时间
                CH_SendDelay2.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[1].stuLight.nDelay.ToString(); //发光延时

                textBox_lightTime3.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[2].stuLight.nWidth.ToString(); //发光时间
                CH_SendDelay3.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[2].stuLight.nDelay.ToString(); //发光延时

                textBox_lightTime4.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[3].stuLight.nWidth.ToString(); //发光时间
                CH_SendDelay4.Text = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[3].stuLight.nDelay.ToString(); //发光延时

            }
            catch (Exception ee)
            {

            }
        }

        //初始化光源控制器
        public void IntLightControl()
        {
            //初始化
            bool bResXML = cls_mainParm.cls_readsaveXML.readXMLAndsetParm();    //读取打标文件的XML并且把参数设置到界面上

            if (cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    cls_Channel cls_chTemp = new cls_Channel();
                    cls_chTemp.strChannel = string.Format("CH{0}", i + 1);
                    cls_chTemp.nTrig = i;
                    cls_chTemp.nLight = i;
                    cls_chTemp.nTG = i;
                    cls_chTemp.nLightValue = 100;

                    cls_chTemp.stuLight.nDelay = 50;
                    cls_chTemp.stuLight.nWidth = 100;
                    cls_chTemp.stuOut.nDelay = 0;
                    cls_chTemp.stuOut.nWidth = 500;
                    cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel.Add(cls_chTemp);
                }

            }
        }

        public void HGSControl_Load()
        {
            try
            {
                string strIP = tbxIP.Text;

                string strRes = "";
                if (dll_setIP.getRealIP(strIP, ref strRes) != 0)
                {
                    MessageBox.Show(strRes);
                    return;
                }

                int nport = int.Parse(tbxPort.Text);
                bTCPIPconnect = multDigital_Dll.connectTCPIP(strIP, nport);
                if (bTCPIPconnect)
                {
                    cls_mainParm.cls_readsaveXML.saveMgeClass.strIP = tbxIP.Text;
                    btn_TCPConnect.Enabled = false;
                    btn_TCPClose.Enabled = true;
                    Log("[HGSControl_Load]", "HGS已连接", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                Log("[ConnectHGSbyIP]", "HGS光源控制器连接失败:" + ex.Message, LogLevel.Error);
            }

        }

        public void ConnectLightCtrlbyIP()
        {
            if (bPortconnect)
            {
                MessageBox.Show("串口连接光源控制器中，请先断开串口连接");
                return;
            }

            string strIP = currProductInfo.HGSlights.IP;

            string strRes = "";
            if (dll_setIP.getRealIP(strIP, ref strRes) != 0)
            {
                MessageBox.Show(strRes);
                return;
            }

            int nport = int.Parse(tbxPort.Text);
            bTCPIPconnect = multDigital_Dll.connectTCPIP(strIP, nport);
            if (bTCPIPconnect)
            {
                cls_mainParm.cls_readsaveXML.saveMgeClass.strIP = tbxIP.Text;
                label_status.Text = "网口连接成功";
                btn_TCPConnect.Enabled = false;
                btn_TCPClose.Enabled = true;

                Thread th1 = new Thread(() => InitializeParm());    //初始化数字电源的参数
                th1.IsBackground = true;
                th1.Start();
            }
        }

        private void btn_TCPOpen_Click(object sender, EventArgs e)
        {
            string strIP = tbxIP.Text;
            string strRes = "";
            try
            {
                if (dll_setIP.getRealIP(strIP, ref strRes) != 0)
                {
                    MessageBox.Show(strRes);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有连接控制器:" + ex.Message);
            }

            int nport = int.Parse(tbxPort.Text);
            bTCPIPconnect = multDigital_Dll.connectTCPIP(strIP, nport);
            if (bTCPIPconnect)
            {
                cls_mainParm.cls_readsaveXML.saveMgeClass.strIP = tbxIP.Text;
                label_status.ForeColor = Color.Green;
                label_status.Text = "网口连接成功";
                btn_TCPConnect.Enabled = false;
                btn_TCPClose.Enabled = true;

                Thread th1 = new Thread(() => InitializeParm());    //初始化数字电源的参数
                th1.IsBackground = true;
                th1.Start();
            }
        }

        private void InitializeParm()
        {
            string strData = "";
            m_liandongEndSemaphore = new SemaphoreSlim(0);
            //多通道
            strData = multDigital_Dll.LianDong_MultChChange(eRunMode.Group);
            multDigital_Dll.SendData(strData);
            if (!m_liandongEndSemaphore.Wait(1000))
            {

            }

            //影射 Trigger通道：0
            List<int> list_nCh = new List<int>();
            list_nCh.Add(0);
            list_nCh.Add(1);
            list_nCh.Add(2);
            list_nCh.Add(3);
            strData = multDigital_Dll.softwareChannelMapping(list_nCh);
            multDigital_Dll.SendData(strData);
            if (!m_liandongEndSemaphore.Wait(1000))
            {

            }

            //参数设置 沿 上升
            strData = multDigital_Dll.SetTriggerMode(list_nCh, eTriggerMode.Edge, EPloarMode.Height);
            multDigital_Dll.SendData(strData);
            if (!m_liandongEndSemaphore.Wait(1000))
            {

            }

            List<int> listIn = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                listIn.Add(0);
            }

            for (int i = 0; i < cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel.Count; i++)
            {
                listIn[cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i].nLight] = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i].nLightValue;
            }

            strData = multDigital_Dll.setBright(listIn);
            multDigital_Dll.SendData(strData);
            if (!m_liandongEndSemaphore.Wait(1000))
            {

            }

            //软触发非使能
            strData = multDigital_Dll.SoftTrigEnable(false);
            multDigital_Dll.SendData(strData);
        }

        private void btn_setLightvalue_Click(object sender, EventArgs e)
        {
            try
            {
                labelError.Text = "";

                List<TextBox> tempTextLightTimeBoxs = new List<TextBox>();
                tempTextLightTimeBoxs.Add(textBox_lightTime1);//0-300
                tempTextLightTimeBoxs.Add(textBox_lightTime2);
                tempTextLightTimeBoxs.Add(textBox_lightTime3);
                tempTextLightTimeBoxs.Add(textBox_lightTime4);

                List<TextBox> tempTextDelyTimeBoxs = new List<TextBox>();
                tempTextDelyTimeBoxs.Add(CH_SendDelay1);//0-65535
                tempTextDelyTimeBoxs.Add(CH_SendDelay2);
                tempTextDelyTimeBoxs.Add(CH_SendDelay3);
                tempTextDelyTimeBoxs.Add(CH_SendDelay4);

                for (int i = 0; i < 4; i++)
                {
                    cls_Channel temp_cls_Chan1 = cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i].MyCopyTo();
                    temp_cls_Chan1.stuLight.nDelay = Convert.ToInt32(tempTextDelyTimeBoxs[i].Text);
                    temp_cls_Chan1.stuLight.nWidth = Convert.ToInt32(tempTextLightTimeBoxs[i].Text);
                    cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i] = temp_cls_Chan1.MyCopyTo();
                    SendParm(cls_mainParm.cls_readsaveXML.saveMgeClass.list_cls_Channel[i]);
                }
                tempTextLightTimeBoxs.Clear();
                tempTextDelyTimeBoxs.Clear();
            }
            catch (Exception ex)
            {
                labelError.Text += ex.Message.ToString();
            }
        }

        private void SendParm(cls_Channel cls_ChanIn)
        {
            List<_stuPulseCurve> list_stuPulseCurve = new List<_stuPulseCurve>(0);

            //Trig
            _stuPulseCurve stuPulseCurveTri = new _stuPulseCurve(0);
            stuPulseCurveTri.strName = class_CurveName.strnameTrig[cls_ChanIn.nTrig];
            _stuPulse stuPulseTri = new _stuPulse();
            stuPulseTri.nDelay = 0;
            stuPulseTri.nWidth = 300;
            stuPulseCurveTri.list_stuPulse.Add(stuPulseTri);
            list_stuPulseCurve.Add(stuPulseCurveTri);

            //Light
            _stuPulseCurve stuPulseCurveLight = new _stuPulseCurve(0);
            stuPulseCurveLight.strName = class_CurveName.strnamesL[cls_ChanIn.nLight];
            _stuPulse stuPulseLight = new _stuPulse();
            stuPulseLight.nDelay = cls_ChanIn.stuLight.nDelay;
            stuPulseLight.nWidth = cls_ChanIn.stuLight.nWidth;
            stuPulseCurveLight.list_stuPulse.Add(stuPulseLight);
            list_stuPulseCurve.Add(stuPulseCurveLight);

            //TG
            _stuPulseCurve stuPulseCurveTG = new _stuPulseCurve(0);
            stuPulseCurveTG.strName = class_CurveName.strnamesT[cls_ChanIn.nTG];
            _stuPulse stuPulseTG = new _stuPulse();
            stuPulseTG.nDelay = cls_ChanIn.stuOut.nDelay;
            stuPulseTG.nWidth = cls_ChanIn.stuOut.nWidth;
            stuPulseCurveTG.list_stuPulse.Add(stuPulseTG);
            list_stuPulseCurve.Add(stuPulseCurveTG);

            eDeviceType edeviceT = new eDeviceType();
            if (cls_mainParm.cls_readsaveXML.saveMgeClass.dcoff_time == 0.1)
            {
                edeviceT = eDeviceType.Type0_1;
            }
            else
            {
                edeviceT = eDeviceType.Type0_5;
            }

            string strData = multDigital_Dll.MultChParm(list_stuPulseCurve, edeviceT);
            multDigital_Dll.SendData(strData);
        }

        private void btn_SaveLightparam_Click(object sender, EventArgs e)
        {
            string strData = multDigital_Dll.SaveParmInFlash();
            multDigital_Dll.SendData(strData);
            cls_mainParm.cls_readsaveXML.saveXML();

            try
            {
                currProductInfo.HGSlights.CH1.pulseWidth = textBox_lightTime1.Text;
                currProductInfo.HGSlights.CH2.pulseWidth = textBox_lightTime2.Text;
                currProductInfo.HGSlights.CH3.pulseWidth = textBox_lightTime3.Text;
                currProductInfo.HGSlights.CH4.pulseWidth = textBox_lightTime4.Text;
                XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                textBox_data.Text = "保存成功";
            }
            catch (Exception ex)
            {
                COMSettingmsg.Text = "保存失败：" + ex.Message;
            }
        }

        private void CH1_Read_Click(object sender, EventArgs e)
        {
            eDeviceType edeviceT = new eDeviceType();
            if (cls_mainParm.cls_readsaveXML.saveMgeClass.dcoff_time == 0.1)
            {
                edeviceT = eDeviceType.Type0_1;
            }
            else
            {
                edeviceT = eDeviceType.Type0_5;
            }
            string strCh_TrigR = "";   //获取Trig通道
            int nLedORCameraR = 0;
            eRunMode eRunModeR = new eRunMode();
            List<_stuPulseCurve> list_stuCurveR = new List<_stuPulseCurve>(0);
            multDigital_Dll.GetFlashParm(ref eRunModeR, ref nLedORCameraR, ref strCh_TrigR, ref list_stuCurveR);

            SetInterface(list_stuCurveR);   //设置界面
            textBox_data.Text = "读取成功";
        }

        private void btn_TCPClose_Click(object sender, EventArgs e)
        {
            bTCPIPconnect = multDigital_Dll.UnconnectTCP();
            if (bTCPIPconnect)
            {
                label_status.ForeColor = Color.Red;
                label_status.Text = "网口关闭";
                btn_TCPConnect.Enabled = true;
                btn_TCPClose.Enabled = false;
            }
        }

        #endregion

        #region CST光源控制

        public void CSTControl_Load()
        {
            //if (!serialPort1.IsOpen)
            //{
            //    try
            //    {
            //        serialPort1.Open();
            //        cmb_COM.Items.Add(serialPort1.PortName);
            //        lbCOMconecStatus.ForeColor = Color.Lime;
            //        lbCOMconecStatus.Text = "已连接！";
            //        Log("[CSTControl_Load]", "CST光源控制器连接成功", LogLevel.Info);
            //    }
            //    catch (Exception ex)
            //    {
            //        lbCOMconecStatus.ForeColor = Color.Red;
            //        lbCOMconecStatus.Text = "未连接！";
            //        Log("[CSTControl_Load]", "CST光源控制器连接失败:" + ex.Message, LogLevel.Error);
            //        labelCOMerr.Text = ex.Message;
            //        MessageBox.Show(ex.Message);
            //    }
            //    SerialPortSettingButton();
            //}

            //try
            //{
            //    serialPort1.Write("SL#");
            //    serialPort1.Write(string.Format("SA{0}#", currProductInfo.CSTlights.CH1.pulseWidth));

            //    //显示
            //    COMSettingmsg.Text = "设置完成：" + string.Format("SA{0}#", currProductInfo.CSTlights.CH1.pulseWidth);
            //}
            //catch (Exception ex)
            //{
            //    COMSettingmsg.Text = "设置失败:" + ex.Message;
            //}
        }

        private void btn_cstConnect_Click(object sender, EventArgs e)
        {
            //    if (!serialPort1.IsOpen)
            //    {
            //        try
            //        {
            //            serialPort1.Open();
            //            cmb_COM.Items.Add(serialPort1.PortName);
            //            lbCOMconecStatus.ForeColor = Color.Lime;
            //            lbCOMconecStatus.Text = "已连接！";
            //            ReadParam();
            //        }
            //        catch (Exception ex)
            //        {
            //            lbCOMconecStatus.ForeColor = Color.Red;
            //            lbCOMconecStatus.Text = "未连接！";
            //            labelCOMerr.Text = ex.Message;
            //            MessageBox.Show(ex.Message);
            //        }
            //        SerialPortSettingButton();
            //    }
        }

        private void btn_cstDisConnect_Click(object sender, EventArgs e)
        {
            //if (serialPort1.IsOpen)
            //{
            //    try
            //    {
            //        serialPort1.Close();
            //        cmb_COM.Items.Clear();
            //        lbCOMconecStatus.ForeColor = Color.Red;
            //        lbCOMconecStatus.Text = "已断开！";
            //    }
            //    catch (Exception ex)
            //    {
            //        lbCOMconecStatus.ForeColor = Color.Red;
            //        lbCOMconecStatus.Text = "断开失败！";
            //        labelCOMerr.Text = ex.Message;
            //        MessageBox.Show(ex.Message);
            //    }
            //    SerialPortSettingButton();
            //}
        }

        private void btn_Set2Ctrl_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    serialPort1.Write(string.Format("SA{0}#SB{1}#SC{2}#SD{3}#", cst_pWidth1.Text, cst_pWidth2.Text, cst_pWidth3.Text, cst_pWidth4.Text));
            //}
            //catch (Exception ex)
            //{
            //    COMSettingmsg.ForeColor = Color.Red;
            //    COMSettingmsg.Text = "设置失败：";
            //    labelCOMerr.Text += ex.Message;
            //}
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                currProductInfo.CSTlights.CH1.pulseWidth = cst_pWidth1.Text;
                currProductInfo.CSTlights.CH2.pulseWidth = cst_pWidth2.Text;
                currProductInfo.CSTlights.CH3.pulseWidth = cst_pWidth3.Text;
                currProductInfo.CSTlights.CH4.pulseWidth = cst_pWidth4.Text;

                XmlSerializerHelper.WriteXML((Object)ALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
            }
            catch (Exception ex)
            {
                COMSettingmsg.Text = "保存失败：" + ex.Message;
            }
        }

        private void btn_readParm_Click(object sender, EventArgs e)
        {
            ReadParam();
        }

        private void ReadParam()
        {
            try
            {
                cst_pWidth1.Text = currProductInfo.CSTlights.CH1.pulseWidth;
                cst_pWidth2.Text = currProductInfo.CSTlights.CH2.pulseWidth;
                cst_pWidth3.Text = currProductInfo.CSTlights.CH3.pulseWidth;
                cst_pWidth4.Text = currProductInfo.CSTlights.CH4.pulseWidth;
                COMSettingmsg.Text = "读取参数成功";
            }
            catch (Exception ex)
            {
                COMSettingmsg.Text = ex.Message;
            }
        }

        private void SerialPortSettingButton()
        {
            //if (serialPort1.IsOpen == true)
            //{
            //    btn_cstConnect.Enabled = false;
            //    btn_cstDisConnect.Enabled = true;
            //}
            //else
            //{
            //    btn_cstConnect.Enabled = true;
            //    btn_cstDisConnect.Enabled = false;
            //}
        }
        #endregion
    }
}

