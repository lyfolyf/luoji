using Cognex.VisionPro.ToolBlock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BX_SetIPParm_Dll_space;
using BX_struct_space;
using 多通道数字电源_Dll_Space;
using System.IO;
using System.ComponentModel;

namespace DataAssemble
{
    #region ---------产品数据结构信息
    public class SingleResult
    {
        public string SN { get; set; }
        public bool Result { get; set; }
        public SingleResult(string sn,bool rt)
        {
            SN = sn;
            Result = rt;
        }
    }
    public class RowResult
    {
        public List<SingleResult> SingleRowResult { get; set; }
        public RowResult(List<SingleResult> singleResults)
        {
            SingleRowResult = singleResults;
        }
        public RowResult() { }
    }
    public class AllResult
    {
        public List<RowResult> AllRowsResult { get; set; }
        public string PicPath { get; set; }
        public string NGPath { get; set; }
        public AllResult(List<RowResult> rowResults,string picPath,string ngPath)
        {
            AllRowsResult = rowResults;
            PicPath = picPath;
            NGPath = ngPath;
        }
    }

    public class Fai
    {
        public string Name;
        public double Value;
        public Fai() { }
        public Fai(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }

    public class SingleProduct
    {
        public List<Fai> Fais = new List<Fai>();
        public string ErrCode = "";
    }

    public class Limitness
    {
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double pixelScale { get; set; }
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double WidthUSL { get; set; }
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double WidthLSL { get; set; }
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double X_Nomial { get; set; } = 500;
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double Y_Nominal { get; set; } = 500;
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double X_USL { get; set; } = 500;
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double X_LSL { get; set; }
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double Y_USL { get; set; } = 500;
        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double Y_LSL { get; set; }

        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double X_Tol_Plus { get; set; } = 0.8;

        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double X_Tol_Minus { get; set; } = -0.8;

        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double Y_Tol_Plus { get; set; } = 0.8;

        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public double Y_Tol_Minus { get; set; } = -0.8;

    }

    [DefaultPropertyAttribute("产品参数")]
    public class ProductInfo
    {
        //此类产品基本属性

        /// <summary>
        /// 此类产品型号或名字
        /// </summary>
        public string Product_name { get; set; }

        /// <summary>
        /// 是否像素标定
        /// </summary>
        public bool ByScale { get; set; }

        /// <summary>
        /// 像素标定值
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// 偏移任务路径
        /// </summary>
        public string OffsetToolPath { get; set; }

        /// <summary>
        /// 视觉任务路径
        /// </summary>
        public string VisionToolsPath { get; set; }

        /// <summary>
        /// 当前产品的SN
        /// </summary>
        public string Code_value { get; set; }

        /// <summary>
        /// 拍照位置索引
        /// </summary>
        public int captureIndex { get; set; }



        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public CameraInfo cameraInfo { get; set; }

        public HGS HGSlights { get; set; }
        public CST CSTlights { get; set; }

        [CategoryAttribute("SPEC"), DescriptionAttribute("")]
        public Limitness Limitness { get; set; }

        public ProductInfo(string product_name = "EncapBig")
        {
            Product_name = product_name;
            OffsetToolPath = "D:\\Lead2DParameter\\VisionTask\\" + product_name + "\\OffsetTool.vpp";
            VisionToolsPath = "D:\\Lead2DParameter\\VisionTask\\" + product_name + "\\Vision.vpp";
            cameraInfo = new CameraInfo("CameraName",01,"SN123456789");
            HGSlights = new HGS("200");
            CSTlights = new CST("0555");
            Limitness = new Limitness();
        }
        public ProductInfo() { }
    }

    public class ALL_PRODUCT_TYPE
    {
        public string SelectedProduct;
        public List< ProductInfo> ProductInfoList;
        public ALL_PRODUCT_TYPE() { }
        public ALL_PRODUCT_TYPE(string selectedProduct) 
        {
            SelectedProduct = selectedProduct;
        }
    }

    #endregion

    #region ---------相机信息

    public class CameraInfo
    {
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public int Id { get; set; }
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public string Name { get; set; }
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public string Description { get; set; } = "";
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public string SN { get; set; } = "00234736204";
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public double Exposure { get; set; } = 500;
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public double Gain { get; set; } = 0;
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public InterfaceType InterfaceType { get; set; } = InterfaceType.GigE;
        [CategoryAttribute("相机属性"), DescriptionAttribute("")]
        public TriggerMode TriggerMode { get; set; } = TriggerMode.On;

        public CameraInfo() { }
        public CameraInfo(string name, int id, string sn)
        {
            Name = name;
            Id = id;
            SN = sn;
        }
    }

    public enum InterfaceType
    {
        GigE,
        USB3
    }

    public enum TriggerMode
    {
        On,
        Off
    }

    #endregion

    #region ---------光源控制信息

    public class CH 
    {
        public string pulseWidth;
        public CH() { }
        public CH( string wid = "200")
        {
            pulseWidth = wid;
        }
    }

    public class Lights
    {
        public CH CH1;
        public CH CH2;
        public CH CH3;
        public CH CH4;

        public Lights() { }
        public Lights(string pulse="200")
        {
            CH1 = new CH(pulse);
            CH2 = new CH(pulse);
            CH3 = new CH(pulse);
            CH4 = new CH(pulse);
        }
    }

    public class HGS: Lights
    {
        public bool Used;
        public int MinValue;
        public int MaxValue;
        public string IP;
        public HGS() { }
        public HGS(string pulse = "30")
        {
            Used = false;
            MinValue = 0;
            MaxValue = 30;
            CH1 = new CH(pulse);
            CH2 = new CH(pulse);
            CH3 = new CH(pulse);
            CH4 = new CH(pulse);
        }
    }

    public class CST : Lights
    {
        public bool Used;
        public int MinValue;
        public int MaxValue;
        public CST() { }
        public CST(string pulse = "0255")
        {
            Used = false;
            MinValue = 0;
            MaxValue = 255;
            CH1 = new CH(pulse);
            CH2 = new CH(pulse);
            CH3 = new CH(pulse);
            CH4 = new CH(pulse);
        }
    }

    #endregion

    #region ---------图片保存信息

    public class ImageSaveSetting
    {
        public string IPAdr;
        public string Port;
        public bool SaveRawImg;
        public bool SaveNGImg;
        public bool SaveOKPicture;
        public bool SaveNGPicture;
        public bool SaveStitchPicture;

        public int PicQuality;

        public int RawImgDays;
        public int NGImgDays;
        public int OKPictureDays;
        public int NGPictureDays;
        public int StitchPictureDays;

        public string AllRawImageDir;
        public string NGRawImageDir;
        public string AllPicturesDir;
        public string NGPicturesDir;
        public string StitchImageDir;

        public string DebugImgDir;
        public ImageSaveSetting(bool save_raw_img=true, bool save_ng_img=true, bool save_ok_picture =true, bool save_ng_picture=true, int raw_img_days=5, int ng_img_days=30,int ok_pic_days=10, int ng_pic_days=60,string  allRawImageDir= @"E:\Image\AllRawImage", string  ngRawImageDir= @"E:\Image\NGRawIamge", string allPicturesDir= @"E:\Image\AllPictures", string ngPicturesPath= @"E:\Image\NGPictures",int pic_quality=1)
        {
            IPAdr = "127.0.0.1";
            SaveRawImg = save_raw_img;
            SaveNGImg = save_ng_img;
            SaveOKPicture = save_ok_picture;
            SaveNGPicture = save_ng_picture;
            SaveStitchPicture = true;

            PicQuality = pic_quality;

            RawImgDays = raw_img_days;
            NGImgDays = ng_img_days;
            OKPictureDays = ok_pic_days;
            NGPictureDays = ng_pic_days;
            StitchPictureDays = 90;

            AllRawImageDir = allRawImageDir;
            NGRawImageDir = ngRawImageDir;
            AllPicturesDir = allPicturesDir;
            NGPicturesDir = ngPicturesPath;
            StitchImageDir = @"E:\Image\StitchImage";

            DebugImgDir = @"F:\Image";
        }

        public ImageSaveSetting() { }
    }

    #endregion

    #region ---------生产统计

    public class SystemParam
    {
        public int TotalNum;
        public int OKNums;
        public SystemParam() { }
    }

    #endregion

    #region ---------标定信息


    #endregion

    #region 


    public class Output
    {
        /// <summary>
        /// 第几个输出项
        /// </summary>
        public string OutputName;

        /// <summary>
        /// 第几个输出项
        /// </summary>
        public int OutputIndex;

        /// <summary>
        /// 输出项类型：int,double,string ,string[](逗号分隔string[,,,])
        /// </summary>
        public string OutputType;

        /// <summary>
        /// 数组中的长度
        /// </summary>
        public int ArrayCount;

        public Output() { }

        public Output(int outputIndex=0,string outPutType="int")
        {
            OutputName = "FAI";
            OutputIndex = outputIndex;
            OutputType = outPutType;
            ArrayCount = 5;
        }

    }

    public class OutputConfig
    {
        /// <summary>
        /// 此类产品型号或名字
        /// </summary>
        public string Product_name;

        /// <summary>
        /// 输出项数量
        /// </summary>
        public int OutputCount;

        public List<Output> Outputs = new List<Output>();
        public OutputConfig(){ }
        public OutputConfig(string product_name = "EncapNA", int outputCount = 3)
        {
            Product_name = product_name;
            OutputCount = outputCount;
            for (int i = 0; i < outputCount; i++)
            {
                Outputs.Add(new Output(i,"int"));
            }
        }
    }

    public class AllProductOutputConfig
    {
        /// <summary>
        /// 产品类型数量
        /// </summary>
        public int Product_count;

        public List<OutputConfig> outputConfigs = new List<OutputConfig>();
        public AllProductOutputConfig() { }
        public AllProductOutputConfig(int product_count=6)
        {
            Product_count = product_count;
            for (int i = 0; i < product_count; i++)
            {
                outputConfigs.Add(new OutputConfig("EncapNA",3));
            }
        }
    }

    #endregion

    #region ---------其他

    public class Point
    {
        public double X;
        public double Y;
        public double Angle;

        public Point( double x=0,double y=0,double r=0)
        {
            X = x;
            Y = y;
            Angle = r;
        }

        public Point(){}
    }

    #endregion
}
