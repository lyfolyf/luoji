using System;

namespace Encap
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tp任务编辑 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Y_Nominal = new System.Windows.Forms.TextBox();
            this.X_Nominal = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.X_Tol_Minus = new System.Windows.Forms.TextBox();
            this.X_Tol_Plus = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.DistanceY = new System.Windows.Forms.TextBox();
            this.DistanceX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bn_AddNewProduct = new System.Windows.Forms.Button();
            this.numY_LSL = new System.Windows.Forms.TextBox();
            this.numY_USL = new System.Windows.Forms.TextBox();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.cmb_Record = new System.Windows.Forms.ComboBox();
            this.numX_LSL = new System.Windows.Forms.TextBox();
            this.numX_USL = new System.Windows.Forms.TextBox();
            this.bn_SaveLimit = new System.Windows.Forms.Button();
            this.label50 = new System.Windows.Forms.Label();
            this.labelUL = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.bn_runOnce = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_TBPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_removeTBs = new System.Windows.Forms.Button();
            this.btn_loadVpp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lb_RunStatus = new System.Windows.Forms.Label();
            this.listBoxTBResults = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_edt_rmv_pic = new System.Windows.Forms.Button();
            this.btn_edt_clr_pic = new System.Windows.Forms.Button();
            this.btn_loadNImag = new System.Windows.Forms.Button();
            this.btn_load1Imag = new System.Windows.Forms.Button();
            this.listBoxpics = new System.Windows.Forms.ListBox();
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.panel17 = new System.Windows.Forms.Panel();
            this.btn_saveEditVpp = new System.Windows.Forms.Button();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.tp主页面 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.listBoxRunLog = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.ChangeCancel = new System.Windows.Forms.Button();
            this.cmb_SltPro = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.labelAll = new System.Windows.Forms.Label();
            this.ChangeTypeCf = new System.Windows.Forms.Button();
            this.txt_Type = new System.Windows.Forms.TextBox();
            this.labelType = new System.Windows.Forms.Label();
            this.btn_ChangeProduct = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tip = new System.Windows.Forms.Label();
            this.cogRecordDisplay2 = new Cognex.VisionPro.CogRecordDisplay();
            this.panel12 = new System.Windows.Forms.Panel();
            this.Distance_Y = new System.Windows.Forms.TextBox();
            this.Distance_X = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bn_disConnectCam = new System.Windows.Forms.Button();
            this.bn_ReconnectCam = new System.Windows.Forms.Button();
            this.Main_Tab_Control = new System.Windows.Forms.TabControl();
            this.tp图片记录 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.groupBoxSaveDays = new System.Windows.Forms.GroupBox();
            this.groupBoximgset = new System.Windows.Forms.GroupBox();
            this.btn_savedays = new System.Windows.Forms.Button();
            this.labelSaveday = new System.Windows.Forms.Label();
            this.btn_ModifyDays = new System.Windows.Forms.Button();
            this.groupBoxSaveDay = new System.Windows.Forms.GroupBox();
            this.chkb_Stitch = new System.Windows.Forms.CheckBox();
            this.chkb_Allimg = new System.Windows.Forms.CheckBox();
            this.chkb_NGimg = new System.Windows.Forms.CheckBox();
            this.chkb_Allpic = new System.Windows.Forms.CheckBox();
            this.chkb_NGpic = new System.Windows.Forms.CheckBox();
            this.numericUpDownStitchPicDays = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownNgRawPicDays = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAllRawPicDays = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAllPicDays = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNgPicDays = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBoxImgPath = new System.Windows.Forms.GroupBox();
            this.lb_stitchImg = new System.Windows.Forms.Label();
            this.btn_stitchPic = new System.Windows.Forms.Button();
            this.TXTstitchPicDir = new System.Windows.Forms.TextBox();
            this.lb_AllImg = new System.Windows.Forms.Label();
            this.lb_NGImg = new System.Windows.Forms.Label();
            this.lb_NGPic = new System.Windows.Forms.Label();
            this.lb_AllPic = new System.Windows.Forms.Label();
            this.btn_allRaw = new System.Windows.Forms.Button();
            this.btn_ngRaw = new System.Windows.Forms.Button();
            this.btn_allPicture = new System.Windows.Forms.Button();
            this.btn_ngPicture = new System.Windows.Forms.Button();
            this.TXTrawPicDir = new System.Windows.Forms.TextBox();
            this.TXTrawngPicDir = new System.Windows.Forms.TextBox();
            this.TXTallPicDir = new System.Windows.Forms.TextBox();
            this.TXTngPicDir = new System.Windows.Forms.TextBox();
            this.groupBoxPicQuaility = new System.Windows.Forms.GroupBox();
            this.rbtn_Mid = new System.Windows.Forms.RadioButton();
            this.rbtn_High = new System.Windows.Forms.RadioButton();
            this.rbtn_Low = new System.Windows.Forms.RadioButton();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btn_review_remove_itm = new System.Windows.Forms.Button();
            this.btn_review_clear_itm = new System.Windows.Forms.Button();
            this.btn_review_picFolder = new System.Windows.Forms.Button();
            this.btn_review_pic = new System.Windows.Forms.Button();
            this.listBox_ReviewPic = new System.Windows.Forms.ListBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.DisplayReView = new Cognex.VisionPro.Display.CogDisplay();
            this.tp光学调试 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.groupBoxLight = new System.Windows.Forms.GroupBox();
            this.unmUDCH4 = new System.Windows.Forms.NumericUpDown();
            this.unmUDCH3 = new System.Windows.Forms.NumericUpDown();
            this.unmUDCH2 = new System.Windows.Forms.NumericUpDown();
            this.unmUDCH1 = new System.Windows.Forms.NumericUpDown();
            this.bn_save_lightPar = new System.Windows.Forms.Button();
            this.lb_light_4 = new System.Windows.Forms.Label();
            this.lb_light_3 = new System.Windows.Forms.Label();
            this.lb_light_2 = new System.Windows.Forms.Label();
            this.bn_set_lightPar = new System.Windows.Forms.Button();
            this.lb_light_1 = new System.Windows.Forms.Label();
            this.groupBoxInitCameraParam = new System.Windows.Forms.GroupBox();
            this.bn_SaveCameraPar = new System.Windows.Forms.Button();
            this.bnSetParam = new System.Windows.Forms.Button();
            this.bnGetParam = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelq1 = new System.Windows.Forms.Label();
            this.tbFrameRate = new System.Windows.Forms.TextBox();
            this.tbGain = new System.Windows.Forms.TextBox();
            this.tbExposure = new System.Windows.Forms.TextBox();
            this.groupBoxDebugBrowse = new System.Windows.Forms.GroupBox();
            this.btn_浏览 = new System.Windows.Forms.Button();
            this.bnSaveJpg = new System.Windows.Forms.Button();
            this.bnSaveBmp = new System.Windows.Forms.Button();
            this.groupBoxTriggermode = new System.Windows.Forms.GroupBox();
            this.bn_Triger = new System.Windows.Forms.Button();
            this.bnTriggerExec = new System.Windows.Forms.Button();
            this.cbSoftTrigger = new System.Windows.Forms.CheckBox();
            this.bnStopGrab = new System.Windows.Forms.Button();
            this.bnStartGrab = new System.Windows.Forms.Button();
            this.bnTriggerMode = new System.Windows.Forms.RadioButton();
            this.bnContinuesMode = new System.Windows.Forms.RadioButton();
            this.groupBoxInitCamera = new System.Windows.Forms.GroupBox();
            this.label21 = new System.Windows.Forms.Label();
            this.btn_swth_mode = new System.Windows.Forms.Button();
            this.bnOpen = new System.Windows.Forms.Button();
            this.bnEnum = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.Display_debug = new System.Windows.Forms.PictureBox();
            this.label_debug = new System.Windows.Forms.Label();
            this.tp测试 = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.CST_Group = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.labelCOMerr = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.COMSettingmsg = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.lbCOMconecStatus = new System.Windows.Forms.Label();
            this.cst_pDelay4 = new System.Windows.Forms.TextBox();
            this.cst_pDelay3 = new System.Windows.Forms.TextBox();
            this.cst_pDelay2 = new System.Windows.Forms.TextBox();
            this.cst_pDelay1 = new System.Windows.Forms.TextBox();
            this.cst_pWidth4 = new System.Windows.Forms.TextBox();
            this.cst_pWidth3 = new System.Windows.Forms.TextBox();
            this.cst_pWidth2 = new System.Windows.Forms.TextBox();
            this.btn_Set2Ctrl = new System.Windows.Forms.Button();
            this.cst_pWidth1 = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_readParm = new System.Windows.Forms.Button();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.btn_cstConnect = new System.Windows.Forms.Button();
            this.btn_cstDisConnect = new System.Windows.Forms.Button();
            this.label37 = new System.Windows.Forms.Label();
            this.cmb_COM = new System.Windows.Forms.ComboBox();
            this.HGS_Group = new System.Windows.Forms.GroupBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.tbxIP = new System.Windows.Forms.TextBox();
            this.CH_SendDelay4 = new System.Windows.Forms.TextBox();
            this.btn_TCPConnect = new System.Windows.Forms.Button();
            this.CH_SendDelay3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CH_SendDelay2 = new System.Windows.Forms.TextBox();
            this.tbxPort = new System.Windows.Forms.TextBox();
            this.CH_SendDelay1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_lightTime4 = new System.Windows.Forms.TextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.textBox_lightTime3 = new System.Windows.Forms.TextBox();
            this.btn_TCPClose = new System.Windows.Forms.Button();
            this.textBox_lightTime2 = new System.Windows.Forms.TextBox();
            this.textBox_data = new System.Windows.Forms.TextBox();
            this.btn_setLightvalue = new System.Windows.Forms.Button();
            this.textBox_lightTime1 = new System.Windows.Forms.TextBox();
            this.btn_SaveLightparam = new System.Windows.Forms.Button();
            this.CH1_Read = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.tb_Type = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioBstop = new System.Windows.Forms.RadioButton();
            this.label28 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.txt_Index = new System.Windows.Forms.TextBox();
            this.txt_ChanTime = new System.Windows.Forms.TextBox();
            this.btn_singleTrg = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_simulation_status = new System.Windows.Forms.TextBox();
            this.btn_SimulationStart = new System.Windows.Forms.Button();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txt_SimTotalNum = new System.Windows.Forms.TextBox();
            this.txt_SimTime = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.listBox_TCPIP = new System.Windows.Forms.ListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.IPClient = new System.Windows.Forms.TextBox();
            this.IP_Disconnect = new System.Windows.Forms.Button();
            this.IP_Connect = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.PortClient = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bn_modifyIP = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripLOG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制选中内容ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Y_Tol_Minus = new System.Windows.Forms.TextBox();
            this.Y_Tol_Plus = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.tp任务编辑.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.panel17.SuspendLayout();
            this.tp主页面.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay2)).BeginInit();
            this.panel12.SuspendLayout();
            this.Main_Tab_Control.SuspendLayout();
            this.tp图片记录.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.panel13.SuspendLayout();
            this.groupBoxSaveDays.SuspendLayout();
            this.groupBoximgset.SuspendLayout();
            this.groupBoxSaveDay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStitchPicDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNgRawPicDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllRawPicDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllPicDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNgPicDays)).BeginInit();
            this.groupBoxImgPath.SuspendLayout();
            this.groupBoxPicQuaility.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayReView)).BeginInit();
            this.tp光学调试.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBoxLight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH1)).BeginInit();
            this.groupBoxInitCameraParam.SuspendLayout();
            this.groupBoxDebugBrowse.SuspendLayout();
            this.groupBoxTriggermode.SuspendLayout();
            this.groupBoxInitCamera.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Display_debug)).BeginInit();
            this.tp测试.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.CST_Group.SuspendLayout();
            this.HGS_Group.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStripLOG.SuspendLayout();
            this.SuspendLayout();
            // 
            // tp任务编辑
            // 
            this.tp任务编辑.BackColor = System.Drawing.Color.Silver;
            this.tp任务编辑.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.tp任务编辑, "tp任务编辑");
            this.tp任务编辑.Name = "tp任务编辑";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.cogToolBlockEditV21, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel17, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel3.Controls.Add(this.numY_LSL);
            this.panel3.Controls.Add(this.Y_Tol_Minus);
            this.panel3.Controls.Add(this.Y_Tol_Plus);
            this.panel3.Controls.Add(this.label36);
            this.panel3.Controls.Add(this.label40);
            this.panel3.Controls.Add(this.Y_Nominal);
            this.panel3.Controls.Add(this.X_Nominal);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label33);
            this.panel3.Controls.Add(this.X_Tol_Minus);
            this.panel3.Controls.Add(this.X_Tol_Plus);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.DistanceY);
            this.panel3.Controls.Add(this.DistanceX);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.bn_AddNewProduct);
            this.panel3.Controls.Add(this.numY_USL);
            this.panel3.Controls.Add(this.label52);
            this.panel3.Controls.Add(this.label53);
            this.panel3.Controls.Add(this.cmb_Record);
            this.panel3.Controls.Add(this.numX_LSL);
            this.panel3.Controls.Add(this.numX_USL);
            this.panel3.Controls.Add(this.bn_SaveLimit);
            this.panel3.Controls.Add(this.label50);
            this.panel3.Controls.Add(this.labelUL);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // Y_Nominal
            // 
            resources.ApplyResources(this.Y_Nominal, "Y_Nominal");
            this.Y_Nominal.Name = "Y_Nominal";
            // 
            // X_Nominal
            // 
            resources.ApplyResources(this.X_Nominal, "X_Nominal");
            this.X_Nominal.Name = "X_Nominal";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // X_Tol_Minus
            // 
            resources.ApplyResources(this.X_Tol_Minus, "X_Tol_Minus");
            this.X_Tol_Minus.Name = "X_Tol_Minus";
            // 
            // X_Tol_Plus
            // 
            resources.ApplyResources(this.X_Tol_Plus, "X_Tol_Plus");
            this.X_Tol_Plus.Name = "X_Tol_Plus";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // DistanceY
            // 
            resources.ApplyResources(this.DistanceY, "DistanceY");
            this.DistanceY.Name = "DistanceY";
            // 
            // DistanceX
            // 
            resources.ApplyResources(this.DistanceX, "DistanceX");
            this.DistanceX.Name = "DistanceX";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // bn_AddNewProduct
            // 
            resources.ApplyResources(this.bn_AddNewProduct, "bn_AddNewProduct");
            this.bn_AddNewProduct.ForeColor = System.Drawing.Color.Navy;
            this.bn_AddNewProduct.Name = "bn_AddNewProduct";
            this.bn_AddNewProduct.UseVisualStyleBackColor = true;
            this.bn_AddNewProduct.Click += new System.EventHandler(this.bn_AddNewProduct_Click);
            // 
            // numY_LSL
            // 
            resources.ApplyResources(this.numY_LSL, "numY_LSL");
            this.numY_LSL.Name = "numY_LSL";
            // 
            // numY_USL
            // 
            resources.ApplyResources(this.numY_USL, "numY_USL");
            this.numY_USL.Name = "numY_USL";
            // 
            // label52
            // 
            resources.ApplyResources(this.label52, "label52");
            this.label52.Name = "label52";
            // 
            // label53
            // 
            resources.ApplyResources(this.label53, "label53");
            this.label53.Name = "label53";
            // 
            // cmb_Record
            // 
            this.cmb_Record.FormattingEnabled = true;
            this.cmb_Record.Items.AddRange(new object[] {
            resources.GetString("cmb_Record.Items"),
            resources.GetString("cmb_Record.Items1"),
            resources.GetString("cmb_Record.Items2"),
            resources.GetString("cmb_Record.Items3")});
            resources.ApplyResources(this.cmb_Record, "cmb_Record");
            this.cmb_Record.Name = "cmb_Record";
            // 
            // numX_LSL
            // 
            resources.ApplyResources(this.numX_LSL, "numX_LSL");
            this.numX_LSL.Name = "numX_LSL";
            // 
            // numX_USL
            // 
            resources.ApplyResources(this.numX_USL, "numX_USL");
            this.numX_USL.Name = "numX_USL";
            // 
            // bn_SaveLimit
            // 
            resources.ApplyResources(this.bn_SaveLimit, "bn_SaveLimit");
            this.bn_SaveLimit.ForeColor = System.Drawing.Color.Navy;
            this.bn_SaveLimit.Name = "bn_SaveLimit";
            this.bn_SaveLimit.UseVisualStyleBackColor = true;
            this.bn_SaveLimit.Click += new System.EventHandler(this.bn_SaveLimit_Click);
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // labelUL
            // 
            resources.ApplyResources(this.labelUL, "labelUL");
            this.labelUL.Name = "labelUL";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.bn_runOnce);
            this.panel4.Controls.Add(this.groupBox3);
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Name = "panel4";
            // 
            // bn_runOnce
            // 
            resources.ApplyResources(this.bn_runOnce, "bn_runOnce");
            this.bn_runOnce.ForeColor = System.Drawing.Color.Navy;
            this.bn_runOnce.Name = "bn_runOnce";
            this.bn_runOnce.UseVisualStyleBackColor = true;
            this.bn_runOnce.Click += new System.EventHandler(this.bn_runOnce_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_TBPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.btn_removeTBs);
            this.groupBox3.Controls.Add(this.btn_loadVpp);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // tb_TBPath
            // 
            resources.ApplyResources(this.tb_TBPath, "tb_TBPath");
            this.tb_TBPath.Name = "tb_TBPath";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.label1.DoubleClick += new System.EventHandler(this.label11_DoubleClick);
            // 
            // btn_removeTBs
            // 
            resources.ApplyResources(this.btn_removeTBs, "btn_removeTBs");
            this.btn_removeTBs.Name = "btn_removeTBs";
            this.btn_removeTBs.UseVisualStyleBackColor = true;
            this.btn_removeTBs.Click += new System.EventHandler(this.btn_removeTBs_Click);
            // 
            // btn_loadVpp
            // 
            resources.ApplyResources(this.btn_loadVpp, "btn_loadVpp");
            this.btn_loadVpp.Name = "btn_loadVpp";
            this.btn_loadVpp.UseVisualStyleBackColor = true;
            this.btn_loadVpp.Click += new System.EventHandler(this.btn_loadVpp_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.lb_RunStatus);
            this.groupBox2.Controls.Add(this.listBoxTBResults);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // lb_RunStatus
            // 
            resources.ApplyResources(this.lb_RunStatus, "lb_RunStatus");
            this.lb_RunStatus.Name = "lb_RunStatus";
            this.lb_RunStatus.DoubleClick += new System.EventHandler(this.lb_RunStatus_DoubleClick);
            // 
            // listBoxTBResults
            // 
            resources.ApplyResources(this.listBoxTBResults, "listBoxTBResults");
            this.listBoxTBResults.BackColor = System.Drawing.Color.Silver;
            this.listBoxTBResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxTBResults.FormattingEnabled = true;
            this.listBoxTBResults.Name = "listBoxTBResults";
            this.listBoxTBResults.DoubleClick += new System.EventHandler(this.listBoxTBResults_DoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_edt_rmv_pic);
            this.groupBox1.Controls.Add(this.btn_edt_clr_pic);
            this.groupBox1.Controls.Add(this.btn_loadNImag);
            this.groupBox1.Controls.Add(this.btn_load1Imag);
            this.groupBox1.Controls.Add(this.listBoxpics);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btn_edt_rmv_pic
            // 
            resources.ApplyResources(this.btn_edt_rmv_pic, "btn_edt_rmv_pic");
            this.btn_edt_rmv_pic.Name = "btn_edt_rmv_pic";
            this.btn_edt_rmv_pic.UseVisualStyleBackColor = true;
            this.btn_edt_rmv_pic.Click += new System.EventHandler(this.btn_edt_rmv_pic_Click);
            // 
            // btn_edt_clr_pic
            // 
            resources.ApplyResources(this.btn_edt_clr_pic, "btn_edt_clr_pic");
            this.btn_edt_clr_pic.Name = "btn_edt_clr_pic";
            this.btn_edt_clr_pic.UseVisualStyleBackColor = true;
            this.btn_edt_clr_pic.Click += new System.EventHandler(this.btn_edt_clr_pic_Click);
            // 
            // btn_loadNImag
            // 
            resources.ApplyResources(this.btn_loadNImag, "btn_loadNImag");
            this.btn_loadNImag.Name = "btn_loadNImag";
            this.btn_loadNImag.UseVisualStyleBackColor = true;
            this.btn_loadNImag.Click += new System.EventHandler(this.btn_loadNImag_Click);
            // 
            // btn_load1Imag
            // 
            resources.ApplyResources(this.btn_load1Imag, "btn_load1Imag");
            this.btn_load1Imag.Name = "btn_load1Imag";
            this.btn_load1Imag.UseVisualStyleBackColor = true;
            this.btn_load1Imag.Click += new System.EventHandler(this.btn_load1Imag_Click);
            // 
            // listBoxpics
            // 
            this.listBoxpics.BackColor = System.Drawing.Color.Silver;
            this.listBoxpics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxpics.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxpics, "listBoxpics");
            this.listBoxpics.Name = "listBoxpics";
            this.listBoxpics.DoubleClick += new System.EventHandler(this.listBoxpics_DoubleClick);
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            resources.ApplyResources(this.cogToolBlockEditV21, "cogToolBlockEditV21");
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.btn_saveEditVpp);
            this.panel17.Controls.Add(this.progressBar3);
            resources.ApplyResources(this.panel17, "panel17");
            this.panel17.Name = "panel17";
            // 
            // btn_saveEditVpp
            // 
            resources.ApplyResources(this.btn_saveEditVpp, "btn_saveEditVpp");
            this.btn_saveEditVpp.BackColor = System.Drawing.Color.White;
            this.btn_saveEditVpp.ForeColor = System.Drawing.Color.Navy;
            this.btn_saveEditVpp.Name = "btn_saveEditVpp";
            this.btn_saveEditVpp.UseVisualStyleBackColor = false;
            this.btn_saveEditVpp.Click += new System.EventHandler(this.btn_saveEditVpp_Click);
            // 
            // progressBar3
            // 
            resources.ApplyResources(this.progressBar3, "progressBar3");
            this.progressBar3.Name = "progressBar3";
            // 
            // tp主页面
            // 
            this.tp主页面.BackColor = System.Drawing.Color.Silver;
            this.tp主页面.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.tp主页面, "tp主页面");
            this.tp主页面.Name = "tp主页面";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.listBoxRunLog, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // listBoxRunLog
            // 
            this.listBoxRunLog.BackColor = System.Drawing.Color.Silver;
            this.listBoxRunLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listBoxRunLog, "listBoxRunLog");
            this.listBoxRunLog.FormattingEnabled = true;
            this.listBoxRunLog.Name = "listBoxRunLog";
            this.listBoxRunLog.DoubleClick += new System.EventHandler(this.listBoxRunLog_DoubleClick);
            this.listBoxRunLog.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxRunLog_MouseUp);
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.panel10, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.panel12, 1, 1);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Controls.Add(this.progressBar2);
            this.panel10.Controls.Add(this.ChangeCancel);
            this.panel10.Controls.Add(this.cmb_SltPro);
            this.panel10.Controls.Add(this.label27);
            this.panel10.Controls.Add(this.labelAll);
            this.panel10.Controls.Add(this.ChangeTypeCf);
            this.panel10.Controls.Add(this.txt_Type);
            this.panel10.Controls.Add(this.labelType);
            this.panel10.Controls.Add(this.btn_ChangeProduct);
            this.panel10.Name = "panel10";
            // 
            // progressBar2
            // 
            resources.ApplyResources(this.progressBar2, "progressBar2");
            this.progressBar2.Name = "progressBar2";
            // 
            // ChangeCancel
            // 
            resources.ApplyResources(this.ChangeCancel, "ChangeCancel");
            this.ChangeCancel.Name = "ChangeCancel";
            this.ChangeCancel.UseVisualStyleBackColor = true;
            this.ChangeCancel.Click += new System.EventHandler(this.ChangeCancel_Click);
            // 
            // cmb_SltPro
            // 
            this.cmb_SltPro.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_SltPro, "cmb_SltPro");
            this.cmb_SltPro.Name = "cmb_SltPro";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.BackColor = System.Drawing.Color.Silver;
            this.label27.Name = "label27";
            // 
            // labelAll
            // 
            resources.ApplyResources(this.labelAll, "labelAll");
            this.labelAll.Name = "labelAll";
            // 
            // ChangeTypeCf
            // 
            resources.ApplyResources(this.ChangeTypeCf, "ChangeTypeCf");
            this.ChangeTypeCf.Name = "ChangeTypeCf";
            this.ChangeTypeCf.UseVisualStyleBackColor = true;
            this.ChangeTypeCf.Click += new System.EventHandler(this.ChangeTypeCf_Click);
            // 
            // txt_Type
            // 
            resources.ApplyResources(this.txt_Type, "txt_Type");
            this.txt_Type.Name = "txt_Type";
            this.txt_Type.ReadOnly = true;
            // 
            // labelType
            // 
            resources.ApplyResources(this.labelType, "labelType");
            this.labelType.Name = "labelType";
            // 
            // btn_ChangeProduct
            // 
            resources.ApplyResources(this.btn_ChangeProduct, "btn_ChangeProduct");
            this.btn_ChangeProduct.Name = "btn_ChangeProduct";
            this.btn_ChangeProduct.UseVisualStyleBackColor = true;
            this.btn_ChangeProduct.Click += new System.EventHandler(this.btn_ChangeProduct_Click);
            // 
            // panel2
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.tip);
            this.panel2.Controls.Add(this.cogRecordDisplay2);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // tip
            // 
            resources.ApplyResources(this.tip, "tip");
            this.tip.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tip.Name = "tip";
            // 
            // cogRecordDisplay2
            // 
            this.cogRecordDisplay2.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay2.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay2.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay2.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay2.ColorMapUpperRoiLimit = 1D;
            resources.ApplyResources(this.cogRecordDisplay2, "cogRecordDisplay2");
            this.cogRecordDisplay2.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay2.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay2.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay2.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay2.Name = "cogRecordDisplay2";
            this.cogRecordDisplay2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay2.OcxState")));
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.Distance_Y);
            this.panel12.Controls.Add(this.Distance_X);
            this.panel12.Controls.Add(this.label5);
            this.panel12.Controls.Add(this.label4);
            this.panel12.Controls.Add(this.bn_disConnectCam);
            this.panel12.Controls.Add(this.bn_ReconnectCam);
            resources.ApplyResources(this.panel12, "panel12");
            this.panel12.Name = "panel12";
            // 
            // Distance_Y
            // 
            resources.ApplyResources(this.Distance_Y, "Distance_Y");
            this.Distance_Y.Name = "Distance_Y";
            // 
            // Distance_X
            // 
            resources.ApplyResources(this.Distance_X, "Distance_X");
            this.Distance_X.Name = "Distance_X";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // bn_disConnectCam
            // 
            resources.ApplyResources(this.bn_disConnectCam, "bn_disConnectCam");
            this.bn_disConnectCam.Name = "bn_disConnectCam";
            this.bn_disConnectCam.UseVisualStyleBackColor = true;
            this.bn_disConnectCam.Click += new System.EventHandler(this.bn_disConnectCam_Click);
            // 
            // bn_ReconnectCam
            // 
            resources.ApplyResources(this.bn_ReconnectCam, "bn_ReconnectCam");
            this.bn_ReconnectCam.Name = "bn_ReconnectCam";
            this.bn_ReconnectCam.UseVisualStyleBackColor = true;
            this.bn_ReconnectCam.Click += new System.EventHandler(this.bn_ReconnectCam_Click);
            // 
            // Main_Tab_Control
            // 
            this.Main_Tab_Control.Controls.Add(this.tp主页面);
            this.Main_Tab_Control.Controls.Add(this.tp图片记录);
            this.Main_Tab_Control.Controls.Add(this.tp任务编辑);
            this.Main_Tab_Control.Controls.Add(this.tp光学调试);
            this.Main_Tab_Control.Controls.Add(this.tp测试);
            resources.ApplyResources(this.Main_Tab_Control, "Main_Tab_Control");
            this.Main_Tab_Control.Name = "Main_Tab_Control";
            this.Main_Tab_Control.SelectedIndex = 0;
            this.Main_Tab_Control.SelectedIndexChanged += new System.EventHandler(this.Main_Tab_Control_SelectedIndexChanged);
            // 
            // tp图片记录
            // 
            this.tp图片记录.BackColor = System.Drawing.Color.Silver;
            this.tp图片记录.Controls.Add(this.tableLayoutPanel8);
            resources.ApplyResources(this.tp图片记录, "tp图片记录");
            this.tp图片记录.Name = "tp图片记录";
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.panel13, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.panel14, 0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.groupBoxSaveDays);
            this.panel13.Controls.Add(this.groupBox13);
            resources.ApplyResources(this.panel13, "panel13");
            this.panel13.Name = "panel13";
            // 
            // groupBoxSaveDays
            // 
            resources.ApplyResources(this.groupBoxSaveDays, "groupBoxSaveDays");
            this.groupBoxSaveDays.Controls.Add(this.groupBoximgset);
            this.groupBoxSaveDays.Controls.Add(this.groupBoxSaveDay);
            this.groupBoxSaveDays.Controls.Add(this.groupBoxImgPath);
            this.groupBoxSaveDays.Controls.Add(this.groupBoxPicQuaility);
            this.groupBoxSaveDays.Name = "groupBoxSaveDays";
            this.groupBoxSaveDays.TabStop = false;
            // 
            // groupBoximgset
            // 
            this.groupBoximgset.Controls.Add(this.btn_savedays);
            this.groupBoximgset.Controls.Add(this.labelSaveday);
            this.groupBoximgset.Controls.Add(this.btn_ModifyDays);
            resources.ApplyResources(this.groupBoximgset, "groupBoximgset");
            this.groupBoximgset.Name = "groupBoximgset";
            this.groupBoximgset.TabStop = false;
            // 
            // btn_savedays
            // 
            resources.ApplyResources(this.btn_savedays, "btn_savedays");
            this.btn_savedays.Name = "btn_savedays";
            this.btn_savedays.UseVisualStyleBackColor = true;
            this.btn_savedays.Click += new System.EventHandler(this.btn_savedays_Click);
            // 
            // labelSaveday
            // 
            resources.ApplyResources(this.labelSaveday, "labelSaveday");
            this.labelSaveday.Name = "labelSaveday";
            this.labelSaveday.DoubleClick += new System.EventHandler(this.labelSaveday_DoubleClick);
            // 
            // btn_ModifyDays
            // 
            resources.ApplyResources(this.btn_ModifyDays, "btn_ModifyDays");
            this.btn_ModifyDays.Name = "btn_ModifyDays";
            this.btn_ModifyDays.UseVisualStyleBackColor = true;
            this.btn_ModifyDays.Click += new System.EventHandler(this.btn_ModifyDays_Click);
            // 
            // groupBoxSaveDay
            // 
            this.groupBoxSaveDay.Controls.Add(this.chkb_Stitch);
            this.groupBoxSaveDay.Controls.Add(this.chkb_Allimg);
            this.groupBoxSaveDay.Controls.Add(this.chkb_NGimg);
            this.groupBoxSaveDay.Controls.Add(this.chkb_Allpic);
            this.groupBoxSaveDay.Controls.Add(this.chkb_NGpic);
            this.groupBoxSaveDay.Controls.Add(this.numericUpDownStitchPicDays);
            this.groupBoxSaveDay.Controls.Add(this.label9);
            this.groupBoxSaveDay.Controls.Add(this.numericUpDownNgRawPicDays);
            this.groupBoxSaveDay.Controls.Add(this.numericUpDownAllRawPicDays);
            this.groupBoxSaveDay.Controls.Add(this.numericUpDownAllPicDays);
            this.groupBoxSaveDay.Controls.Add(this.numericUpDownNgPicDays);
            this.groupBoxSaveDay.Controls.Add(this.label17);
            this.groupBoxSaveDay.Controls.Add(this.label18);
            this.groupBoxSaveDay.Controls.Add(this.label16);
            this.groupBoxSaveDay.Controls.Add(this.label15);
            resources.ApplyResources(this.groupBoxSaveDay, "groupBoxSaveDay");
            this.groupBoxSaveDay.Name = "groupBoxSaveDay";
            this.groupBoxSaveDay.TabStop = false;
            // 
            // chkb_Stitch
            // 
            resources.ApplyResources(this.chkb_Stitch, "chkb_Stitch");
            this.chkb_Stitch.Name = "chkb_Stitch";
            this.chkb_Stitch.UseVisualStyleBackColor = true;
            // 
            // chkb_Allimg
            // 
            resources.ApplyResources(this.chkb_Allimg, "chkb_Allimg");
            this.chkb_Allimg.Name = "chkb_Allimg";
            this.chkb_Allimg.UseVisualStyleBackColor = true;
            // 
            // chkb_NGimg
            // 
            resources.ApplyResources(this.chkb_NGimg, "chkb_NGimg");
            this.chkb_NGimg.Name = "chkb_NGimg";
            this.chkb_NGimg.UseVisualStyleBackColor = true;
            // 
            // chkb_Allpic
            // 
            resources.ApplyResources(this.chkb_Allpic, "chkb_Allpic");
            this.chkb_Allpic.Name = "chkb_Allpic";
            this.chkb_Allpic.UseVisualStyleBackColor = true;
            // 
            // chkb_NGpic
            // 
            resources.ApplyResources(this.chkb_NGpic, "chkb_NGpic");
            this.chkb_NGpic.Name = "chkb_NGpic";
            this.chkb_NGpic.UseVisualStyleBackColor = true;
            // 
            // numericUpDownStitchPicDays
            // 
            resources.ApplyResources(this.numericUpDownStitchPicDays, "numericUpDownStitchPicDays");
            this.numericUpDownStitchPicDays.Name = "numericUpDownStitchPicDays";
            this.numericUpDownStitchPicDays.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // numericUpDownNgRawPicDays
            // 
            resources.ApplyResources(this.numericUpDownNgRawPicDays, "numericUpDownNgRawPicDays");
            this.numericUpDownNgRawPicDays.Name = "numericUpDownNgRawPicDays";
            this.numericUpDownNgRawPicDays.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numericUpDownAllRawPicDays
            // 
            resources.ApplyResources(this.numericUpDownAllRawPicDays, "numericUpDownAllRawPicDays");
            this.numericUpDownAllRawPicDays.Name = "numericUpDownAllRawPicDays";
            this.numericUpDownAllRawPicDays.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownAllPicDays
            // 
            resources.ApplyResources(this.numericUpDownAllPicDays, "numericUpDownAllPicDays");
            this.numericUpDownAllPicDays.Name = "numericUpDownAllPicDays";
            this.numericUpDownAllPicDays.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownNgPicDays
            // 
            resources.ApplyResources(this.numericUpDownNgPicDays, "numericUpDownNgPicDays");
            this.numericUpDownNgPicDays.Name = "numericUpDownNgPicDays";
            this.numericUpDownNgPicDays.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // groupBoxImgPath
            // 
            this.groupBoxImgPath.Controls.Add(this.lb_stitchImg);
            this.groupBoxImgPath.Controls.Add(this.btn_stitchPic);
            this.groupBoxImgPath.Controls.Add(this.TXTstitchPicDir);
            this.groupBoxImgPath.Controls.Add(this.lb_AllImg);
            this.groupBoxImgPath.Controls.Add(this.lb_NGImg);
            this.groupBoxImgPath.Controls.Add(this.lb_NGPic);
            this.groupBoxImgPath.Controls.Add(this.lb_AllPic);
            this.groupBoxImgPath.Controls.Add(this.btn_allRaw);
            this.groupBoxImgPath.Controls.Add(this.btn_ngRaw);
            this.groupBoxImgPath.Controls.Add(this.btn_allPicture);
            this.groupBoxImgPath.Controls.Add(this.btn_ngPicture);
            this.groupBoxImgPath.Controls.Add(this.TXTrawPicDir);
            this.groupBoxImgPath.Controls.Add(this.TXTrawngPicDir);
            this.groupBoxImgPath.Controls.Add(this.TXTallPicDir);
            this.groupBoxImgPath.Controls.Add(this.TXTngPicDir);
            resources.ApplyResources(this.groupBoxImgPath, "groupBoxImgPath");
            this.groupBoxImgPath.Name = "groupBoxImgPath";
            this.groupBoxImgPath.TabStop = false;
            // 
            // lb_stitchImg
            // 
            resources.ApplyResources(this.lb_stitchImg, "lb_stitchImg");
            this.lb_stitchImg.Name = "lb_stitchImg";
            this.lb_stitchImg.DoubleClick += new System.EventHandler(this.lb_stitchImg_DoubleClick);
            // 
            // btn_stitchPic
            // 
            resources.ApplyResources(this.btn_stitchPic, "btn_stitchPic");
            this.btn_stitchPic.Name = "btn_stitchPic";
            this.btn_stitchPic.UseVisualStyleBackColor = true;
            this.btn_stitchPic.Click += new System.EventHandler(this.btn_stitchPic_Click);
            // 
            // TXTstitchPicDir
            // 
            resources.ApplyResources(this.TXTstitchPicDir, "TXTstitchPicDir");
            this.TXTstitchPicDir.Name = "TXTstitchPicDir";
            this.TXTstitchPicDir.ReadOnly = true;
            // 
            // lb_AllImg
            // 
            resources.ApplyResources(this.lb_AllImg, "lb_AllImg");
            this.lb_AllImg.Name = "lb_AllImg";
            this.lb_AllImg.DoubleClick += new System.EventHandler(this.lb_AllImg_DoubleClick);
            // 
            // lb_NGImg
            // 
            resources.ApplyResources(this.lb_NGImg, "lb_NGImg");
            this.lb_NGImg.Name = "lb_NGImg";
            this.lb_NGImg.DoubleClick += new System.EventHandler(this.lb_NGImg_DoubleClick);
            // 
            // lb_NGPic
            // 
            resources.ApplyResources(this.lb_NGPic, "lb_NGPic");
            this.lb_NGPic.Name = "lb_NGPic";
            this.lb_NGPic.DoubleClick += new System.EventHandler(this.lb_NGPic_DoubleClick);
            // 
            // lb_AllPic
            // 
            resources.ApplyResources(this.lb_AllPic, "lb_AllPic");
            this.lb_AllPic.Name = "lb_AllPic";
            this.lb_AllPic.DoubleClick += new System.EventHandler(this.lb_AllPic_DoubleClick);
            // 
            // btn_allRaw
            // 
            resources.ApplyResources(this.btn_allRaw, "btn_allRaw");
            this.btn_allRaw.Name = "btn_allRaw";
            this.btn_allRaw.UseVisualStyleBackColor = true;
            this.btn_allRaw.Click += new System.EventHandler(this.btn_allRaw_Click);
            // 
            // btn_ngRaw
            // 
            resources.ApplyResources(this.btn_ngRaw, "btn_ngRaw");
            this.btn_ngRaw.Name = "btn_ngRaw";
            this.btn_ngRaw.UseVisualStyleBackColor = true;
            this.btn_ngRaw.Click += new System.EventHandler(this.btn_ngRaw_Click);
            // 
            // btn_allPicture
            // 
            resources.ApplyResources(this.btn_allPicture, "btn_allPicture");
            this.btn_allPicture.Name = "btn_allPicture";
            this.btn_allPicture.UseVisualStyleBackColor = true;
            this.btn_allPicture.Click += new System.EventHandler(this.btn_allPicture_Click);
            // 
            // btn_ngPicture
            // 
            resources.ApplyResources(this.btn_ngPicture, "btn_ngPicture");
            this.btn_ngPicture.Name = "btn_ngPicture";
            this.btn_ngPicture.UseVisualStyleBackColor = true;
            this.btn_ngPicture.Click += new System.EventHandler(this.btn_ngPicture_Click);
            // 
            // TXTrawPicDir
            // 
            resources.ApplyResources(this.TXTrawPicDir, "TXTrawPicDir");
            this.TXTrawPicDir.Name = "TXTrawPicDir";
            this.TXTrawPicDir.ReadOnly = true;
            // 
            // TXTrawngPicDir
            // 
            resources.ApplyResources(this.TXTrawngPicDir, "TXTrawngPicDir");
            this.TXTrawngPicDir.Name = "TXTrawngPicDir";
            this.TXTrawngPicDir.ReadOnly = true;
            // 
            // TXTallPicDir
            // 
            resources.ApplyResources(this.TXTallPicDir, "TXTallPicDir");
            this.TXTallPicDir.Name = "TXTallPicDir";
            this.TXTallPicDir.ReadOnly = true;
            // 
            // TXTngPicDir
            // 
            resources.ApplyResources(this.TXTngPicDir, "TXTngPicDir");
            this.TXTngPicDir.Name = "TXTngPicDir";
            this.TXTngPicDir.ReadOnly = true;
            // 
            // groupBoxPicQuaility
            // 
            this.groupBoxPicQuaility.Controls.Add(this.rbtn_Mid);
            this.groupBoxPicQuaility.Controls.Add(this.rbtn_High);
            this.groupBoxPicQuaility.Controls.Add(this.rbtn_Low);
            resources.ApplyResources(this.groupBoxPicQuaility, "groupBoxPicQuaility");
            this.groupBoxPicQuaility.Name = "groupBoxPicQuaility";
            this.groupBoxPicQuaility.TabStop = false;
            // 
            // rbtn_Mid
            // 
            resources.ApplyResources(this.rbtn_Mid, "rbtn_Mid");
            this.rbtn_Mid.Name = "rbtn_Mid";
            this.rbtn_Mid.TabStop = true;
            this.rbtn_Mid.UseVisualStyleBackColor = true;
            // 
            // rbtn_High
            // 
            resources.ApplyResources(this.rbtn_High, "rbtn_High");
            this.rbtn_High.Name = "rbtn_High";
            this.rbtn_High.TabStop = true;
            this.rbtn_High.UseVisualStyleBackColor = true;
            // 
            // rbtn_Low
            // 
            resources.ApplyResources(this.rbtn_Low, "rbtn_Low");
            this.rbtn_Low.Name = "rbtn_Low";
            this.rbtn_Low.TabStop = true;
            this.rbtn_Low.UseVisualStyleBackColor = true;
            // 
            // groupBox13
            // 
            resources.ApplyResources(this.groupBox13, "groupBox13");
            this.groupBox13.Controls.Add(this.btn_review_remove_itm);
            this.groupBox13.Controls.Add(this.btn_review_clear_itm);
            this.groupBox13.Controls.Add(this.btn_review_picFolder);
            this.groupBox13.Controls.Add(this.btn_review_pic);
            this.groupBox13.Controls.Add(this.listBox_ReviewPic);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.TabStop = false;
            // 
            // btn_review_remove_itm
            // 
            resources.ApplyResources(this.btn_review_remove_itm, "btn_review_remove_itm");
            this.btn_review_remove_itm.Name = "btn_review_remove_itm";
            this.btn_review_remove_itm.UseVisualStyleBackColor = true;
            this.btn_review_remove_itm.Click += new System.EventHandler(this.btn_review_remove_itm_Click);
            // 
            // btn_review_clear_itm
            // 
            resources.ApplyResources(this.btn_review_clear_itm, "btn_review_clear_itm");
            this.btn_review_clear_itm.Name = "btn_review_clear_itm";
            this.btn_review_clear_itm.UseVisualStyleBackColor = true;
            this.btn_review_clear_itm.Click += new System.EventHandler(this.btn_review_clear_itm_Click);
            // 
            // btn_review_picFolder
            // 
            resources.ApplyResources(this.btn_review_picFolder, "btn_review_picFolder");
            this.btn_review_picFolder.Name = "btn_review_picFolder";
            this.btn_review_picFolder.UseVisualStyleBackColor = true;
            this.btn_review_picFolder.Click += new System.EventHandler(this.btn_review_picFolder_Click);
            // 
            // btn_review_pic
            // 
            resources.ApplyResources(this.btn_review_pic, "btn_review_pic");
            this.btn_review_pic.Name = "btn_review_pic";
            this.btn_review_pic.UseVisualStyleBackColor = true;
            this.btn_review_pic.Click += new System.EventHandler(this.btn_review_pic_Click);
            // 
            // listBox_ReviewPic
            // 
            this.listBox_ReviewPic.BackColor = System.Drawing.Color.Silver;
            this.listBox_ReviewPic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox_ReviewPic.FormattingEnabled = true;
            resources.ApplyResources(this.listBox_ReviewPic, "listBox_ReviewPic");
            this.listBox_ReviewPic.Name = "listBox_ReviewPic";
            this.listBox_ReviewPic.SelectedIndexChanged += new System.EventHandler(this.listBox_ReviewPic_SelectedIndexChanged);
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.DisplayReView);
            resources.ApplyResources(this.panel14, "panel14");
            this.panel14.Name = "panel14";
            // 
            // DisplayReView
            // 
            this.DisplayReView.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.DisplayReView.ColorMapLowerRoiLimit = 0D;
            this.DisplayReView.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.DisplayReView.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.DisplayReView.ColorMapUpperRoiLimit = 1D;
            resources.ApplyResources(this.DisplayReView, "DisplayReView");
            this.DisplayReView.DoubleTapZoomCycleLength = 2;
            this.DisplayReView.DoubleTapZoomSensitivity = 2.5D;
            this.DisplayReView.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.DisplayReView.MouseWheelSensitivity = 1D;
            this.DisplayReView.Name = "DisplayReView";
            this.DisplayReView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("DisplayReView.OcxState")));
            // 
            // tp光学调试
            // 
            this.tp光学调试.BackColor = System.Drawing.Color.Silver;
            this.tp光学调试.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.tp光学调试, "tp光学调试");
            this.tp光学调试.Name = "tp光学调试";
            // 
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.panel5, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.groupBoxLight);
            this.panel5.Controls.Add(this.groupBoxInitCameraParam);
            this.panel5.Controls.Add(this.groupBoxDebugBrowse);
            this.panel5.Controls.Add(this.groupBoxTriggermode);
            this.panel5.Controls.Add(this.groupBoxInitCamera);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // groupBoxLight
            // 
            resources.ApplyResources(this.groupBoxLight, "groupBoxLight");
            this.groupBoxLight.Controls.Add(this.unmUDCH4);
            this.groupBoxLight.Controls.Add(this.unmUDCH3);
            this.groupBoxLight.Controls.Add(this.unmUDCH2);
            this.groupBoxLight.Controls.Add(this.unmUDCH1);
            this.groupBoxLight.Controls.Add(this.bn_save_lightPar);
            this.groupBoxLight.Controls.Add(this.lb_light_4);
            this.groupBoxLight.Controls.Add(this.lb_light_3);
            this.groupBoxLight.Controls.Add(this.lb_light_2);
            this.groupBoxLight.Controls.Add(this.bn_set_lightPar);
            this.groupBoxLight.Controls.Add(this.lb_light_1);
            this.groupBoxLight.Name = "groupBoxLight";
            this.groupBoxLight.TabStop = false;
            // 
            // unmUDCH4
            // 
            resources.ApplyResources(this.unmUDCH4, "unmUDCH4");
            this.unmUDCH4.Name = "unmUDCH4";
            // 
            // unmUDCH3
            // 
            resources.ApplyResources(this.unmUDCH3, "unmUDCH3");
            this.unmUDCH3.Name = "unmUDCH3";
            // 
            // unmUDCH2
            // 
            resources.ApplyResources(this.unmUDCH2, "unmUDCH2");
            this.unmUDCH2.Name = "unmUDCH2";
            // 
            // unmUDCH1
            // 
            resources.ApplyResources(this.unmUDCH1, "unmUDCH1");
            this.unmUDCH1.Name = "unmUDCH1";
            // 
            // bn_save_lightPar
            // 
            resources.ApplyResources(this.bn_save_lightPar, "bn_save_lightPar");
            this.bn_save_lightPar.Name = "bn_save_lightPar";
            this.bn_save_lightPar.UseVisualStyleBackColor = true;
            this.bn_save_lightPar.Click += new System.EventHandler(this.bn_save_lightPar_Click);
            // 
            // lb_light_4
            // 
            resources.ApplyResources(this.lb_light_4, "lb_light_4");
            this.lb_light_4.Name = "lb_light_4";
            // 
            // lb_light_3
            // 
            resources.ApplyResources(this.lb_light_3, "lb_light_3");
            this.lb_light_3.Name = "lb_light_3";
            // 
            // lb_light_2
            // 
            resources.ApplyResources(this.lb_light_2, "lb_light_2");
            this.lb_light_2.Name = "lb_light_2";
            // 
            // bn_set_lightPar
            // 
            resources.ApplyResources(this.bn_set_lightPar, "bn_set_lightPar");
            this.bn_set_lightPar.Name = "bn_set_lightPar";
            this.bn_set_lightPar.UseVisualStyleBackColor = true;
            this.bn_set_lightPar.Click += new System.EventHandler(this.bn_set_lightPar_Click);
            // 
            // lb_light_1
            // 
            resources.ApplyResources(this.lb_light_1, "lb_light_1");
            this.lb_light_1.Name = "lb_light_1";
            // 
            // groupBoxInitCameraParam
            // 
            resources.ApplyResources(this.groupBoxInitCameraParam, "groupBoxInitCameraParam");
            this.groupBoxInitCameraParam.Controls.Add(this.bn_SaveCameraPar);
            this.groupBoxInitCameraParam.Controls.Add(this.bnSetParam);
            this.groupBoxInitCameraParam.Controls.Add(this.bnGetParam);
            this.groupBoxInitCameraParam.Controls.Add(this.label3);
            this.groupBoxInitCameraParam.Controls.Add(this.label2);
            this.groupBoxInitCameraParam.Controls.Add(this.labelq1);
            this.groupBoxInitCameraParam.Controls.Add(this.tbFrameRate);
            this.groupBoxInitCameraParam.Controls.Add(this.tbGain);
            this.groupBoxInitCameraParam.Controls.Add(this.tbExposure);
            this.groupBoxInitCameraParam.Name = "groupBoxInitCameraParam";
            this.groupBoxInitCameraParam.TabStop = false;
            // 
            // bn_SaveCameraPar
            // 
            resources.ApplyResources(this.bn_SaveCameraPar, "bn_SaveCameraPar");
            this.bn_SaveCameraPar.Name = "bn_SaveCameraPar";
            this.bn_SaveCameraPar.UseVisualStyleBackColor = true;
            this.bn_SaveCameraPar.Click += new System.EventHandler(this.bn_SaveCameraPar_Click);
            // 
            // bnSetParam
            // 
            resources.ApplyResources(this.bnSetParam, "bnSetParam");
            this.bnSetParam.Name = "bnSetParam";
            this.bnSetParam.UseVisualStyleBackColor = true;
            this.bnSetParam.Click += new System.EventHandler(this.bnSetParam_Click);
            // 
            // bnGetParam
            // 
            resources.ApplyResources(this.bnGetParam, "bnGetParam");
            this.bnGetParam.Name = "bnGetParam";
            this.bnGetParam.UseVisualStyleBackColor = true;
            this.bnGetParam.Click += new System.EventHandler(this.bnGetParam_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelq1
            // 
            resources.ApplyResources(this.labelq1, "labelq1");
            this.labelq1.Name = "labelq1";
            // 
            // tbFrameRate
            // 
            resources.ApplyResources(this.tbFrameRate, "tbFrameRate");
            this.tbFrameRate.Name = "tbFrameRate";
            // 
            // tbGain
            // 
            resources.ApplyResources(this.tbGain, "tbGain");
            this.tbGain.Name = "tbGain";
            // 
            // tbExposure
            // 
            resources.ApplyResources(this.tbExposure, "tbExposure");
            this.tbExposure.Name = "tbExposure";
            // 
            // groupBoxDebugBrowse
            // 
            resources.ApplyResources(this.groupBoxDebugBrowse, "groupBoxDebugBrowse");
            this.groupBoxDebugBrowse.Controls.Add(this.btn_浏览);
            this.groupBoxDebugBrowse.Controls.Add(this.bnSaveJpg);
            this.groupBoxDebugBrowse.Controls.Add(this.bnSaveBmp);
            this.groupBoxDebugBrowse.Name = "groupBoxDebugBrowse";
            this.groupBoxDebugBrowse.TabStop = false;
            // 
            // btn_浏览
            // 
            resources.ApplyResources(this.btn_浏览, "btn_浏览");
            this.btn_浏览.Name = "btn_浏览";
            this.btn_浏览.UseVisualStyleBackColor = true;
            this.btn_浏览.Click += new System.EventHandler(this.btn_浏览_Click);
            // 
            // bnSaveJpg
            // 
            resources.ApplyResources(this.bnSaveJpg, "bnSaveJpg");
            this.bnSaveJpg.Name = "bnSaveJpg";
            this.bnSaveJpg.UseVisualStyleBackColor = true;
            this.bnSaveJpg.Click += new System.EventHandler(this.bnSaveJpg_Click);
            // 
            // bnSaveBmp
            // 
            resources.ApplyResources(this.bnSaveBmp, "bnSaveBmp");
            this.bnSaveBmp.Name = "bnSaveBmp";
            this.bnSaveBmp.UseVisualStyleBackColor = true;
            this.bnSaveBmp.Click += new System.EventHandler(this.bnSaveBmp_Click);
            // 
            // groupBoxTriggermode
            // 
            resources.ApplyResources(this.groupBoxTriggermode, "groupBoxTriggermode");
            this.groupBoxTriggermode.Controls.Add(this.bn_Triger);
            this.groupBoxTriggermode.Controls.Add(this.bnTriggerExec);
            this.groupBoxTriggermode.Controls.Add(this.cbSoftTrigger);
            this.groupBoxTriggermode.Controls.Add(this.bnStopGrab);
            this.groupBoxTriggermode.Controls.Add(this.bnStartGrab);
            this.groupBoxTriggermode.Controls.Add(this.bnTriggerMode);
            this.groupBoxTriggermode.Controls.Add(this.bnContinuesMode);
            this.groupBoxTriggermode.Name = "groupBoxTriggermode";
            this.groupBoxTriggermode.TabStop = false;
            // 
            // bn_Triger
            // 
            resources.ApplyResources(this.bn_Triger, "bn_Triger");
            this.bn_Triger.ForeColor = System.Drawing.Color.Green;
            this.bn_Triger.Name = "bn_Triger";
            this.bn_Triger.UseVisualStyleBackColor = true;
            this.bn_Triger.Click += new System.EventHandler(this.bn_Triger_Click);
            // 
            // bnTriggerExec
            // 
            resources.ApplyResources(this.bnTriggerExec, "bnTriggerExec");
            this.bnTriggerExec.Name = "bnTriggerExec";
            this.bnTriggerExec.UseVisualStyleBackColor = true;
            this.bnTriggerExec.Click += new System.EventHandler(this.bnTriggerExec_Click);
            // 
            // cbSoftTrigger
            // 
            resources.ApplyResources(this.cbSoftTrigger, "cbSoftTrigger");
            this.cbSoftTrigger.Name = "cbSoftTrigger";
            this.cbSoftTrigger.UseVisualStyleBackColor = true;
            this.cbSoftTrigger.CheckedChanged += new System.EventHandler(this.cbSoftTrigger_CheckedChanged);
            // 
            // bnStopGrab
            // 
            resources.ApplyResources(this.bnStopGrab, "bnStopGrab");
            this.bnStopGrab.Name = "bnStopGrab";
            this.bnStopGrab.UseVisualStyleBackColor = true;
            this.bnStopGrab.Click += new System.EventHandler(this.bnStopGrab_Click);
            // 
            // bnStartGrab
            // 
            resources.ApplyResources(this.bnStartGrab, "bnStartGrab");
            this.bnStartGrab.Name = "bnStartGrab";
            this.bnStartGrab.UseVisualStyleBackColor = true;
            this.bnStartGrab.Click += new System.EventHandler(this.bnStartGrab_Click);
            // 
            // bnTriggerMode
            // 
            resources.ApplyResources(this.bnTriggerMode, "bnTriggerMode");
            this.bnTriggerMode.Name = "bnTriggerMode";
            this.bnTriggerMode.TabStop = true;
            this.bnTriggerMode.UseMnemonic = false;
            this.bnTriggerMode.UseVisualStyleBackColor = true;
            this.bnTriggerMode.CheckedChanged += new System.EventHandler(this.bnTriggerMode_CheckedChanged);
            // 
            // bnContinuesMode
            // 
            resources.ApplyResources(this.bnContinuesMode, "bnContinuesMode");
            this.bnContinuesMode.Name = "bnContinuesMode";
            this.bnContinuesMode.TabStop = true;
            this.bnContinuesMode.UseVisualStyleBackColor = true;
            // 
            // groupBoxInitCamera
            // 
            resources.ApplyResources(this.groupBoxInitCamera, "groupBoxInitCamera");
            this.groupBoxInitCamera.Controls.Add(this.label21);
            this.groupBoxInitCamera.Controls.Add(this.btn_swth_mode);
            this.groupBoxInitCamera.Controls.Add(this.bnOpen);
            this.groupBoxInitCamera.Controls.Add(this.bnEnum);
            this.groupBoxInitCamera.Name = "groupBoxInitCamera";
            this.groupBoxInitCamera.TabStop = false;
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // btn_swth_mode
            // 
            resources.ApplyResources(this.btn_swth_mode, "btn_swth_mode");
            this.btn_swth_mode.Name = "btn_swth_mode";
            this.btn_swth_mode.UseVisualStyleBackColor = true;
            this.btn_swth_mode.Click += new System.EventHandler(this.btn_swth_mode_Click);
            // 
            // bnOpen
            // 
            resources.ApplyResources(this.bnOpen, "bnOpen");
            this.bnOpen.Name = "bnOpen";
            this.bnOpen.UseVisualStyleBackColor = true;
            this.bnOpen.Click += new System.EventHandler(this.bnOpen_Click);
            // 
            // bnEnum
            // 
            resources.ApplyResources(this.bnEnum, "bnEnum");
            this.bnEnum.Name = "bnEnum";
            this.bnEnum.UseVisualStyleBackColor = true;
            this.bnEnum.Click += new System.EventHandler(this.bnEnum_Click);
            // 
            // tableLayoutPanel5
            // 
            resources.ApplyResources(this.tableLayoutPanel5, "tableLayoutPanel5");
            this.tableLayoutPanel5.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.panel9, 0, 1);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.cbDeviceList);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            resources.ApplyResources(this.cbDeviceList, "cbDeviceList");
            this.cbDeviceList.Name = "cbDeviceList";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.Display_debug);
            this.panel9.Controls.Add(this.label_debug);
            resources.ApplyResources(this.panel9, "panel9");
            this.panel9.Name = "panel9";
            // 
            // Display_debug
            // 
            resources.ApplyResources(this.Display_debug, "Display_debug");
            this.Display_debug.Name = "Display_debug";
            this.Display_debug.TabStop = false;
            // 
            // label_debug
            // 
            resources.ApplyResources(this.label_debug, "label_debug");
            this.label_debug.Name = "label_debug";
            // 
            // tp测试
            // 
            this.tp测试.BackColor = System.Drawing.Color.DimGray;
            this.tp测试.Controls.Add(this.groupBox17);
            this.tp测试.Controls.Add(this.groupBox16);
            resources.ApplyResources(this.tp测试, "tp测试");
            this.tp测试.Name = "tp测试";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.CST_Group);
            this.groupBox17.Controls.Add(this.HGS_Group);
            this.groupBox17.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox17.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox17, "groupBox17");
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.TabStop = false;
            // 
            // CST_Group
            // 
            this.CST_Group.Controls.Add(this.label8);
            this.CST_Group.Controls.Add(this.label46);
            this.CST_Group.Controls.Add(this.labelCOMerr);
            this.CST_Group.Controls.Add(this.label47);
            this.CST_Group.Controls.Add(this.COMSettingmsg);
            this.CST_Group.Controls.Add(this.label48);
            this.CST_Group.Controls.Add(this.lbCOMconecStatus);
            this.CST_Group.Controls.Add(this.cst_pDelay4);
            this.CST_Group.Controls.Add(this.cst_pDelay3);
            this.CST_Group.Controls.Add(this.cst_pDelay2);
            this.CST_Group.Controls.Add(this.cst_pDelay1);
            this.CST_Group.Controls.Add(this.cst_pWidth4);
            this.CST_Group.Controls.Add(this.cst_pWidth3);
            this.CST_Group.Controls.Add(this.cst_pWidth2);
            this.CST_Group.Controls.Add(this.btn_Set2Ctrl);
            this.CST_Group.Controls.Add(this.cst_pWidth1);
            this.CST_Group.Controls.Add(this.btn_save);
            this.CST_Group.Controls.Add(this.btn_readParm);
            this.CST_Group.Controls.Add(this.label38);
            this.CST_Group.Controls.Add(this.label39);
            this.CST_Group.Controls.Add(this.btn_cstConnect);
            this.CST_Group.Controls.Add(this.btn_cstDisConnect);
            this.CST_Group.Controls.Add(this.label37);
            this.CST_Group.Controls.Add(this.cmb_COM);
            resources.ApplyResources(this.CST_Group, "CST_Group");
            this.CST_Group.Name = "CST_Group";
            this.CST_Group.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Name = "label8";
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.ForeColor = System.Drawing.Color.Black;
            this.label46.Name = "label46";
            // 
            // labelCOMerr
            // 
            resources.ApplyResources(this.labelCOMerr, "labelCOMerr");
            this.labelCOMerr.ForeColor = System.Drawing.Color.Black;
            this.labelCOMerr.Name = "labelCOMerr";
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.ForeColor = System.Drawing.Color.Black;
            this.label47.Name = "label47";
            // 
            // COMSettingmsg
            // 
            this.COMSettingmsg.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.COMSettingmsg, "COMSettingmsg");
            this.COMSettingmsg.Name = "COMSettingmsg";
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.ForeColor = System.Drawing.Color.Black;
            this.label48.Name = "label48";
            // 
            // lbCOMconecStatus
            // 
            resources.ApplyResources(this.lbCOMconecStatus, "lbCOMconecStatus");
            this.lbCOMconecStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbCOMconecStatus.ForeColor = System.Drawing.Color.Lime;
            this.lbCOMconecStatus.Name = "lbCOMconecStatus";
            // 
            // cst_pDelay4
            // 
            resources.ApplyResources(this.cst_pDelay4, "cst_pDelay4");
            this.cst_pDelay4.Name = "cst_pDelay4";
            this.cst_pDelay4.ReadOnly = true;
            // 
            // cst_pDelay3
            // 
            resources.ApplyResources(this.cst_pDelay3, "cst_pDelay3");
            this.cst_pDelay3.Name = "cst_pDelay3";
            this.cst_pDelay3.ReadOnly = true;
            // 
            // cst_pDelay2
            // 
            resources.ApplyResources(this.cst_pDelay2, "cst_pDelay2");
            this.cst_pDelay2.Name = "cst_pDelay2";
            this.cst_pDelay2.ReadOnly = true;
            // 
            // cst_pDelay1
            // 
            resources.ApplyResources(this.cst_pDelay1, "cst_pDelay1");
            this.cst_pDelay1.Name = "cst_pDelay1";
            this.cst_pDelay1.ReadOnly = true;
            // 
            // cst_pWidth4
            // 
            resources.ApplyResources(this.cst_pWidth4, "cst_pWidth4");
            this.cst_pWidth4.Name = "cst_pWidth4";
            // 
            // cst_pWidth3
            // 
            resources.ApplyResources(this.cst_pWidth3, "cst_pWidth3");
            this.cst_pWidth3.Name = "cst_pWidth3";
            // 
            // cst_pWidth2
            // 
            resources.ApplyResources(this.cst_pWidth2, "cst_pWidth2");
            this.cst_pWidth2.Name = "cst_pWidth2";
            // 
            // btn_Set2Ctrl
            // 
            this.btn_Set2Ctrl.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_Set2Ctrl, "btn_Set2Ctrl");
            this.btn_Set2Ctrl.Name = "btn_Set2Ctrl";
            this.btn_Set2Ctrl.UseVisualStyleBackColor = true;
            this.btn_Set2Ctrl.Click += new System.EventHandler(this.btn_Set2Ctrl_Click);
            // 
            // cst_pWidth1
            // 
            resources.ApplyResources(this.cst_pWidth1, "cst_pWidth1");
            this.cst_pWidth1.Name = "cst_pWidth1";
            // 
            // btn_save
            // 
            this.btn_save.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_readParm
            // 
            this.btn_readParm.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_readParm, "btn_readParm");
            this.btn_readParm.Name = "btn_readParm";
            this.btn_readParm.UseVisualStyleBackColor = true;
            this.btn_readParm.Click += new System.EventHandler(this.btn_readParm_Click);
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Name = "label38";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Name = "label39";
            // 
            // btn_cstConnect
            // 
            this.btn_cstConnect.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_cstConnect, "btn_cstConnect");
            this.btn_cstConnect.Name = "btn_cstConnect";
            this.btn_cstConnect.UseVisualStyleBackColor = true;
            this.btn_cstConnect.Click += new System.EventHandler(this.btn_cstConnect_Click);
            // 
            // btn_cstDisConnect
            // 
            this.btn_cstDisConnect.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_cstDisConnect, "btn_cstDisConnect");
            this.btn_cstDisConnect.Name = "btn_cstDisConnect";
            this.btn_cstDisConnect.UseVisualStyleBackColor = true;
            this.btn_cstDisConnect.Click += new System.EventHandler(this.btn_cstDisConnect_Click);
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Name = "label37";
            // 
            // cmb_COM
            // 
            this.cmb_COM.FormattingEnabled = true;
            resources.ApplyResources(this.cmb_COM, "cmb_COM");
            this.cmb_COM.Name = "cmb_COM";
            // 
            // HGS_Group
            // 
            this.HGS_Group.Controls.Add(this.label45);
            this.HGS_Group.Controls.Add(this.label44);
            this.HGS_Group.Controls.Add(this.label43);
            this.HGS_Group.Controls.Add(this.label42);
            this.HGS_Group.Controls.Add(this.tbxIP);
            this.HGS_Group.Controls.Add(this.CH_SendDelay4);
            this.HGS_Group.Controls.Add(this.btn_TCPConnect);
            this.HGS_Group.Controls.Add(this.CH_SendDelay3);
            this.HGS_Group.Controls.Add(this.label11);
            this.HGS_Group.Controls.Add(this.CH_SendDelay2);
            this.HGS_Group.Controls.Add(this.tbxPort);
            this.HGS_Group.Controls.Add(this.CH_SendDelay1);
            this.HGS_Group.Controls.Add(this.label14);
            this.HGS_Group.Controls.Add(this.textBox_lightTime4);
            this.HGS_Group.Controls.Add(this.label_status);
            this.HGS_Group.Controls.Add(this.textBox_lightTime3);
            this.HGS_Group.Controls.Add(this.btn_TCPClose);
            this.HGS_Group.Controls.Add(this.textBox_lightTime2);
            this.HGS_Group.Controls.Add(this.textBox_data);
            this.HGS_Group.Controls.Add(this.btn_setLightvalue);
            this.HGS_Group.Controls.Add(this.textBox_lightTime1);
            this.HGS_Group.Controls.Add(this.btn_SaveLightparam);
            this.HGS_Group.Controls.Add(this.CH1_Read);
            this.HGS_Group.Controls.Add(this.label19);
            this.HGS_Group.Controls.Add(this.label20);
            this.HGS_Group.Controls.Add(this.labelError);
            resources.ApplyResources(this.HGS_Group, "HGS_Group");
            this.HGS_Group.Name = "HGS_Group";
            this.HGS_Group.TabStop = false;
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.ForeColor = System.Drawing.Color.Black;
            this.label45.Name = "label45";
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.ForeColor = System.Drawing.Color.Black;
            this.label44.Name = "label44";
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.ForeColor = System.Drawing.Color.Black;
            this.label43.Name = "label43";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.ForeColor = System.Drawing.Color.Black;
            this.label42.Name = "label42";
            // 
            // tbxIP
            // 
            resources.ApplyResources(this.tbxIP, "tbxIP");
            this.tbxIP.Name = "tbxIP";
            this.tbxIP.ReadOnly = true;
            // 
            // CH_SendDelay4
            // 
            resources.ApplyResources(this.CH_SendDelay4, "CH_SendDelay4");
            this.CH_SendDelay4.Name = "CH_SendDelay4";
            // 
            // btn_TCPConnect
            // 
            this.btn_TCPConnect.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_TCPConnect, "btn_TCPConnect");
            this.btn_TCPConnect.Name = "btn_TCPConnect";
            this.btn_TCPConnect.UseVisualStyleBackColor = true;
            this.btn_TCPConnect.Click += new System.EventHandler(this.btn_TCPOpen_Click);
            // 
            // CH_SendDelay3
            // 
            resources.ApplyResources(this.CH_SendDelay3, "CH_SendDelay3");
            this.CH_SendDelay3.Name = "CH_SendDelay3";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Name = "label11";
            // 
            // CH_SendDelay2
            // 
            resources.ApplyResources(this.CH_SendDelay2, "CH_SendDelay2");
            this.CH_SendDelay2.Name = "CH_SendDelay2";
            // 
            // tbxPort
            // 
            resources.ApplyResources(this.tbxPort, "tbxPort");
            this.tbxPort.Name = "tbxPort";
            this.tbxPort.ReadOnly = true;
            // 
            // CH_SendDelay1
            // 
            resources.ApplyResources(this.CH_SendDelay1, "CH_SendDelay1");
            this.CH_SendDelay1.Name = "CH_SendDelay1";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Name = "label14";
            // 
            // textBox_lightTime4
            // 
            resources.ApplyResources(this.textBox_lightTime4, "textBox_lightTime4");
            this.textBox_lightTime4.Name = "textBox_lightTime4";
            // 
            // label_status
            // 
            resources.ApplyResources(this.label_status, "label_status");
            this.label_status.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label_status.ForeColor = System.Drawing.Color.Lime;
            this.label_status.Name = "label_status";
            // 
            // textBox_lightTime3
            // 
            resources.ApplyResources(this.textBox_lightTime3, "textBox_lightTime3");
            this.textBox_lightTime3.Name = "textBox_lightTime3";
            // 
            // btn_TCPClose
            // 
            this.btn_TCPClose.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_TCPClose, "btn_TCPClose");
            this.btn_TCPClose.Name = "btn_TCPClose";
            this.btn_TCPClose.UseVisualStyleBackColor = true;
            this.btn_TCPClose.Click += new System.EventHandler(this.btn_TCPClose_Click);
            // 
            // textBox_lightTime2
            // 
            resources.ApplyResources(this.textBox_lightTime2, "textBox_lightTime2");
            this.textBox_lightTime2.Name = "textBox_lightTime2";
            // 
            // textBox_data
            // 
            this.textBox_data.BackColor = System.Drawing.Color.DimGray;
            resources.ApplyResources(this.textBox_data, "textBox_data");
            this.textBox_data.Name = "textBox_data";
            // 
            // btn_setLightvalue
            // 
            this.btn_setLightvalue.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_setLightvalue, "btn_setLightvalue");
            this.btn_setLightvalue.Name = "btn_setLightvalue";
            this.btn_setLightvalue.UseVisualStyleBackColor = true;
            this.btn_setLightvalue.Click += new System.EventHandler(this.btn_setLightvalue_Click);
            // 
            // textBox_lightTime1
            // 
            resources.ApplyResources(this.textBox_lightTime1, "textBox_lightTime1");
            this.textBox_lightTime1.Name = "textBox_lightTime1";
            // 
            // btn_SaveLightparam
            // 
            this.btn_SaveLightparam.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_SaveLightparam, "btn_SaveLightparam");
            this.btn_SaveLightparam.Name = "btn_SaveLightparam";
            this.btn_SaveLightparam.UseVisualStyleBackColor = true;
            this.btn_SaveLightparam.Click += new System.EventHandler(this.btn_SaveLightparam_Click);
            // 
            // CH1_Read
            // 
            this.CH1_Read.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.CH1_Read, "CH1_Read");
            this.CH1_Read.Name = "CH1_Read";
            this.CH1_Read.UseVisualStyleBackColor = true;
            this.CH1_Read.Click += new System.EventHandler(this.CH1_Read_Click);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Name = "label19";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Name = "label20";
            // 
            // labelError
            // 
            resources.ApplyResources(this.labelError, "labelError");
            this.labelError.ForeColor = System.Drawing.Color.Black;
            this.labelError.Name = "labelError";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.groupBox18);
            this.groupBox16.Controls.Add(this.listBox_TCPIP);
            this.groupBox16.Controls.Add(this.groupBox6);
            this.groupBox16.Controls.Add(this.groupBox4);
            this.groupBox16.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox16.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.groupBox16, "groupBox16");
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.TabStop = false;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.tb_Type);
            this.groupBox18.Controls.Add(this.button2);
            this.groupBox18.Controls.Add(this.radioButton1);
            this.groupBox18.Controls.Add(this.radioBstop);
            this.groupBox18.Controls.Add(this.label28);
            this.groupBox18.Controls.Add(this.label35);
            this.groupBox18.Controls.Add(this.label26);
            this.groupBox18.Controls.Add(this.label34);
            this.groupBox18.Controls.Add(this.txt_Index);
            this.groupBox18.Controls.Add(this.txt_ChanTime);
            this.groupBox18.Controls.Add(this.btn_singleTrg);
            this.groupBox18.Controls.Add(this.label32);
            this.groupBox18.Controls.Add(this.txt_simulation_status);
            this.groupBox18.Controls.Add(this.btn_SimulationStart);
            this.groupBox18.Controls.Add(this.label31);
            this.groupBox18.Controls.Add(this.label29);
            this.groupBox18.Controls.Add(this.txt_SimTotalNum);
            this.groupBox18.Controls.Add(this.txt_SimTime);
            this.groupBox18.Controls.Add(this.label30);
            resources.ApplyResources(this.groupBox18, "groupBox18");
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.TabStop = false;
            // 
            // tb_Type
            // 
            resources.ApplyResources(this.tb_Type, "tb_Type");
            this.tb_Type.Name = "tb_Type";
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioBstop
            // 
            resources.ApplyResources(this.radioBstop, "radioBstop");
            this.radioBstop.Name = "radioBstop";
            this.radioBstop.TabStop = true;
            this.radioBstop.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.BackColor = System.Drawing.Color.Gray;
            this.label28.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label28.Name = "label28";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.ForeColor = System.Drawing.Color.Black;
            this.label35.Name = "label35";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.ForeColor = System.Drawing.Color.Black;
            this.label26.Name = "label26";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Name = "label34";
            // 
            // txt_Index
            // 
            this.txt_Index.BackColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.txt_Index, "txt_Index");
            this.txt_Index.Name = "txt_Index";
            // 
            // txt_ChanTime
            // 
            resources.ApplyResources(this.txt_ChanTime, "txt_ChanTime");
            this.txt_ChanTime.Name = "txt_ChanTime";
            // 
            // btn_singleTrg
            // 
            this.btn_singleTrg.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_singleTrg, "btn_singleTrg");
            this.btn_singleTrg.Name = "btn_singleTrg";
            this.btn_singleTrg.UseVisualStyleBackColor = true;
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.ForeColor = System.Drawing.Color.Black;
            this.label32.Name = "label32";
            // 
            // txt_simulation_status
            // 
            this.txt_simulation_status.BackColor = System.Drawing.Color.Black;
            this.txt_simulation_status.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_simulation_status, "txt_simulation_status");
            this.txt_simulation_status.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_simulation_status.Name = "txt_simulation_status";
            // 
            // btn_SimulationStart
            // 
            this.btn_SimulationStart.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.btn_SimulationStart, "btn_SimulationStart");
            this.btn_SimulationStart.Name = "btn_SimulationStart";
            this.btn_SimulationStart.UseVisualStyleBackColor = true;
            this.btn_SimulationStart.Click += new System.EventHandler(this.btn_SimulationStart_Click);
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.ForeColor = System.Drawing.Color.Black;
            this.label31.Name = "label31";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.BackColor = System.Drawing.Color.Gray;
            this.label29.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label29.Name = "label29";
            // 
            // txt_SimTotalNum
            // 
            resources.ApplyResources(this.txt_SimTotalNum, "txt_SimTotalNum");
            this.txt_SimTotalNum.Name = "txt_SimTotalNum";
            // 
            // txt_SimTime
            // 
            resources.ApplyResources(this.txt_SimTime, "txt_SimTime");
            this.txt_SimTime.Name = "txt_SimTime";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.ForeColor = System.Drawing.Color.Black;
            this.label30.Name = "label30";
            // 
            // listBox_TCPIP
            // 
            this.listBox_TCPIP.BackColor = System.Drawing.Color.DimGray;
            this.listBox_TCPIP.FormattingEnabled = true;
            resources.ApplyResources(this.listBox_TCPIP, "listBox_TCPIP");
            this.listBox_TCPIP.Name = "listBox_TCPIP";
            this.listBox_TCPIP.DoubleClick += new System.EventHandler(this.listBox_TCPIP_DoubleClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label24);
            this.groupBox6.Controls.Add(this.IPClient);
            this.groupBox6.Controls.Add(this.IP_Disconnect);
            this.groupBox6.Controls.Add(this.IP_Connect);
            this.groupBox6.Controls.Add(this.label25);
            this.groupBox6.Controls.Add(this.PortClient);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.Name = "label24";
            // 
            // IPClient
            // 
            resources.ApplyResources(this.IPClient, "IPClient");
            this.IPClient.Name = "IPClient";
            // 
            // IP_Disconnect
            // 
            this.IP_Disconnect.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.IP_Disconnect, "IP_Disconnect");
            this.IP_Disconnect.Name = "IP_Disconnect";
            this.IP_Disconnect.UseVisualStyleBackColor = true;
            this.IP_Disconnect.Click += new System.EventHandler(this.IP_Disconnect_Click);
            // 
            // IP_Connect
            // 
            this.IP_Connect.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.IP_Connect, "IP_Connect");
            this.IP_Connect.Name = "IP_Connect";
            this.IP_Connect.UseVisualStyleBackColor = true;
            this.IP_Connect.Click += new System.EventHandler(this.IP_Connect_Click);
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.ForeColor = System.Drawing.Color.Black;
            this.label25.Name = "label25";
            // 
            // PortClient
            // 
            resources.ApplyResources(this.PortClient, "PortClient");
            this.PortClient.Name = "PortClient";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.bn_modifyIP);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.txtIP);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.txtPort);
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // bn_modifyIP
            // 
            resources.ApplyResources(this.bn_modifyIP, "bn_modifyIP");
            this.bn_modifyIP.Name = "bn_modifyIP";
            this.bn_modifyIP.UseVisualStyleBackColor = true;
            this.bn_modifyIP.Click += new System.EventHandler(this.bn_modifyIP_Click);
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Name = "label23";
            // 
            // txtIP
            // 
            resources.ApplyResources(this.txtIP, "txtIP");
            this.txtIP.Name = "txtIP";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Name = "label22";
            // 
            // txtPort
            // 
            resources.ApplyResources(this.txtPort, "txtPort");
            this.txtPort.Name = "txtPort";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStripLOG
            // 
            this.contextMenuStripLOG.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripLOG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制选中内容ToolStripMenuItem});
            this.contextMenuStripLOG.Name = "contextMenuStripLOG";
            resources.ApplyResources(this.contextMenuStripLOG, "contextMenuStripLOG");
            // 
            // 复制选中内容ToolStripMenuItem
            // 
            this.复制选中内容ToolStripMenuItem.Name = "复制选中内容ToolStripMenuItem";
            resources.ApplyResources(this.复制选中内容ToolStripMenuItem, "复制选中内容ToolStripMenuItem");
            this.复制选中内容ToolStripMenuItem.Click += new System.EventHandler(this.复制选中内容ToolStripMenuItem_Click);
            // 
            // Y_Tol_Minus
            // 
            resources.ApplyResources(this.Y_Tol_Minus, "Y_Tol_Minus");
            this.Y_Tol_Minus.Name = "Y_Tol_Minus";
            // 
            // Y_Tol_Plus
            // 
            resources.ApplyResources(this.Y_Tol_Plus, "Y_Tol_Plus");
            this.Y_Tol_Plus.Name = "Y_Tol_Plus";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Main_Tab_Control);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tp任务编辑.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.panel17.ResumeLayout(false);
            this.tp主页面.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay2)).EndInit();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.Main_Tab_Control.ResumeLayout(false);
            this.tp图片记录.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.groupBoxSaveDays.ResumeLayout(false);
            this.groupBoximgset.ResumeLayout(false);
            this.groupBoximgset.PerformLayout();
            this.groupBoxSaveDay.ResumeLayout(false);
            this.groupBoxSaveDay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStitchPicDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNgRawPicDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllRawPicDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAllPicDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNgPicDays)).EndInit();
            this.groupBoxImgPath.ResumeLayout(false);
            this.groupBoxImgPath.PerformLayout();
            this.groupBoxPicQuaility.ResumeLayout(false);
            this.groupBoxPicQuaility.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DisplayReView)).EndInit();
            this.tp光学调试.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBoxLight.ResumeLayout(false);
            this.groupBoxLight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unmUDCH1)).EndInit();
            this.groupBoxInitCameraParam.ResumeLayout(false);
            this.groupBoxInitCameraParam.PerformLayout();
            this.groupBoxDebugBrowse.ResumeLayout(false);
            this.groupBoxTriggermode.ResumeLayout(false);
            this.groupBoxTriggermode.PerformLayout();
            this.groupBoxInitCamera.ResumeLayout(false);
            this.groupBoxInitCamera.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Display_debug)).EndInit();
            this.tp测试.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.CST_Group.ResumeLayout(false);
            this.CST_Group.PerformLayout();
            this.HGS_Group.ResumeLayout(false);
            this.HGS_Group.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.contextMenuStripLOG.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tp任务编辑;
        private System.Windows.Forms.TabPage tp主页面;
        private System.Windows.Forms.TabControl Main_Tab_Control;
        private System.Windows.Forms.TabPage tp图片记录;
        private System.Windows.Forms.TabPage tp光学调试;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxTBResults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBoxpics;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox groupBoxLight;
        private System.Windows.Forms.Label lb_light_4;
        private System.Windows.Forms.Label lb_light_3;
        private System.Windows.Forms.Label lb_light_2;
        private System.Windows.Forms.Button bn_set_lightPar;
        private System.Windows.Forms.Label lb_light_1;
        private System.Windows.Forms.GroupBox groupBoxInitCameraParam;
        private System.Windows.Forms.Button bnSetParam;
        private System.Windows.Forms.Button bnGetParam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelq1;
        private System.Windows.Forms.TextBox tbFrameRate;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.TextBox tbExposure;
        private System.Windows.Forms.GroupBox groupBoxDebugBrowse;
        private System.Windows.Forms.Button bnSaveJpg;
        private System.Windows.Forms.Button bnSaveBmp;
        private System.Windows.Forms.GroupBox groupBoxTriggermode;
        private System.Windows.Forms.Button bnTriggerExec;
        private System.Windows.Forms.CheckBox cbSoftTrigger;
        private System.Windows.Forms.Button bnStopGrab;
        private System.Windows.Forms.Button bnStartGrab;
        private System.Windows.Forms.RadioButton bnTriggerMode;
        private System.Windows.Forms.RadioButton bnContinuesMode;
        private System.Windows.Forms.GroupBox groupBoxInitCamera;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.Button bnEnum;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabPage tp测试;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button btn_loadVpp;
        private System.Windows.Forms.Button btn_load1Imag;
        private System.Windows.Forms.Button btn_loadNImag;
        private System.Windows.Forms.Button btn_removeTBs;
        private System.Windows.Forms.Label lb_RunStatus;
        private System.Windows.Forms.Button btn_edt_rmv_pic;
        private System.Windows.Forms.Button btn_edt_clr_pic;
        private System.Windows.Forms.GroupBox groupBoxSaveDays;
        private System.Windows.Forms.Button btn_review_remove_itm;
        private System.Windows.Forms.Button btn_review_clear_itm;
        private System.Windows.Forms.Button btn_review_picFolder;
        private System.Windows.Forms.Button btn_review_pic;
        private System.Windows.Forms.ListBox listBox_ReviewPic;
        private System.Windows.Forms.GroupBox groupBoxSaveDay;
        private System.Windows.Forms.NumericUpDown numericUpDownNgRawPicDays;
        private System.Windows.Forms.NumericUpDown numericUpDownAllRawPicDays;
        private System.Windows.Forms.NumericUpDown numericUpDownAllPicDays;
        private System.Windows.Forms.NumericUpDown numericUpDownNgPicDays;
        private System.Windows.Forms.GroupBox groupBoxImgPath;
        private System.Windows.Forms.Button btn_allRaw;
        private System.Windows.Forms.Button btn_ngRaw;
        private System.Windows.Forms.Button btn_allPicture;
        private System.Windows.Forms.Button btn_ngPicture;
        private System.Windows.Forms.TextBox TXTrawPicDir;
        private System.Windows.Forms.TextBox TXTrawngPicDir;
        private System.Windows.Forms.TextBox TXTallPicDir;
        private System.Windows.Forms.TextBox TXTngPicDir;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBoxPicQuaility;
        private System.Windows.Forms.RadioButton rbtn_Mid;
        private System.Windows.Forms.RadioButton rbtn_High;
        private System.Windows.Forms.RadioButton rbtn_Low;
        private System.Windows.Forms.Button btn_ModifyDays;
        private System.Windows.Forms.Label labelSaveday;
        private System.Windows.Forms.Button btn_savedays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label_debug;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.Button btn_swth_mode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_TCPConnect;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbxPort;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button btn_TCPClose;
        private System.Windows.Forms.Button btn_SaveLightparam;
        private System.Windows.Forms.TextBox textBox_lightTime1;
        private System.Windows.Forms.Button btn_setLightvalue;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label labelError;
        private System.Windows.Forms.Label label21;
        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ListBox listBoxRunLog;
        private System.Windows.Forms.Button btn_浏览;
        private System.Windows.Forms.Button bn_SaveCameraPar;
        private System.Windows.Forms.Button bn_save_lightPar;
        private System.Windows.Forms.Button IP_Disconnect;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button IP_Connect;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox IPClient;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox PortClient;
        private System.Windows.Forms.ListBox listBox_TCPIP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label labelAll;
        private System.Windows.Forms.Button ChangeTypeCf;
        private System.Windows.Forms.TextBox txt_Type;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Button btn_ChangeProduct;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.ComboBox cmb_SltPro;
        private System.Windows.Forms.Button ChangeCancel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private Cognex.VisionPro.Display.CogDisplay DisplayReView;
        private System.Windows.Forms.Button bn_modifyIP;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripLOG;
        private System.Windows.Forms.ToolStripMenuItem 复制选中内容ToolStripMenuItem;
        private System.Windows.Forms.TextBox tbxIP;
        private System.Windows.Forms.Button CH1_Read;
        private System.Windows.Forms.TextBox textBox_data;
        private System.Windows.Forms.TextBox textBox_lightTime4;
        private System.Windows.Forms.TextBox textBox_lightTime3;
        private System.Windows.Forms.TextBox textBox_lightTime2;
        private System.Windows.Forms.TextBox CH_SendDelay4;
        private System.Windows.Forms.TextBox CH_SendDelay3;
        private System.Windows.Forms.TextBox CH_SendDelay2;
        private System.Windows.Forms.TextBox CH_SendDelay1;
        private System.Windows.Forms.GroupBox CST_Group;
        private System.Windows.Forms.Label labelCOMerr;
        private System.Windows.Forms.TextBox COMSettingmsg;
        private System.Windows.Forms.Label lbCOMconecStatus;
        private System.Windows.Forms.TextBox cst_pDelay4;
        private System.Windows.Forms.TextBox cst_pDelay3;
        private System.Windows.Forms.TextBox cst_pDelay2;
        private System.Windows.Forms.TextBox cst_pDelay1;
        private System.Windows.Forms.TextBox cst_pWidth4;
        private System.Windows.Forms.TextBox cst_pWidth3;
        private System.Windows.Forms.TextBox cst_pWidth2;
        private System.Windows.Forms.Button btn_Set2Ctrl;
        private System.Windows.Forms.TextBox cst_pWidth1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_readParm;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Button btn_cstConnect;
        private System.Windows.Forms.Button btn_cstDisConnect;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ComboBox cmb_COM;
        private System.Windows.Forms.GroupBox HGS_Group;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txt_Index;
        private System.Windows.Forms.TextBox txt_ChanTime;
        private System.Windows.Forms.Button btn_singleTrg;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txt_simulation_status;
        private System.Windows.Forms.Button btn_SimulationStart;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txt_SimTotalNum;
        private System.Windows.Forms.TextBox txt_SimTime;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Button bn_Triger;
        private System.Windows.Forms.NumericUpDown unmUDCH4;
        private System.Windows.Forms.NumericUpDown unmUDCH3;
        private System.Windows.Forms.NumericUpDown unmUDCH2;
        private System.Windows.Forms.NumericUpDown unmUDCH1;
        private System.Windows.Forms.Label lb_AllImg;
        private System.Windows.Forms.Label lb_NGImg;
        private System.Windows.Forms.Label lb_NGPic;
        private System.Windows.Forms.Label lb_AllPic;
        private System.Windows.Forms.GroupBox groupBoximgset;
        private System.Windows.Forms.NumericUpDown numericUpDownStitchPicDays;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lb_stitchImg;
        private System.Windows.Forms.Button btn_stitchPic;
        private System.Windows.Forms.TextBox TXTstitchPicDir;
        private System.Windows.Forms.PictureBox Display_debug;
        private System.Windows.Forms.Button bn_ReconnectCam;
        private System.Windows.Forms.Button bn_disConnectCam;
        private System.Windows.Forms.CheckBox chkb_Stitch;
        private System.Windows.Forms.CheckBox chkb_Allimg;
        private System.Windows.Forms.CheckBox chkb_NGimg;
        private System.Windows.Forms.CheckBox chkb_Allpic;
        private System.Windows.Forms.CheckBox chkb_NGpic;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label labelUL;
        private System.Windows.Forms.Button bn_runOnce;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Button btn_saveEditVpp;
        private System.Windows.Forms.Button bn_SaveLimit;
        private System.Windows.Forms.TextBox numX_LSL;
        private System.Windows.Forms.TextBox numX_USL;
        private System.Windows.Forms.ComboBox cmb_Record;
        private System.Windows.Forms.TextBox tb_TBPath;
        private System.Windows.Forms.RadioButton radioBstop;
        private System.Windows.Forms.TextBox numY_LSL;
        private System.Windows.Forms.TextBox numY_USL;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.RadioButton radioButton1;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay2;
        private System.Windows.Forms.Label tip;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tb_Type;
        private System.Windows.Forms.Button bn_AddNewProduct;
        private System.Windows.Forms.TextBox Distance_Y;
        private System.Windows.Forms.TextBox Distance_X;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DistanceY;
        private System.Windows.Forms.TextBox DistanceX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox X_Tol_Minus;
        private System.Windows.Forms.TextBox X_Tol_Plus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Y_Nominal;
        private System.Windows.Forms.TextBox X_Nominal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox Y_Tol_Minus;
        private System.Windows.Forms.TextBox Y_Tol_Plus;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label40;
    }
}

