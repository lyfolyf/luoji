using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using 高精度数字电源;


namespace SaveManger
{
    public class SaveMangerClass
    {
        public SaveMangerClass()
        {

        }

        public double dcoff_time = 0.5;
        public string strWinName = "高精度数字电源";
        public string strIP = "192.168.1.7";
        public string strPort = "";
        public List<cls_Channel> list_cls_Channel = new List<cls_Channel>(1);

    
    }


    public class SaveMangerClass2
    {
        public List<cls_Channel> list_cls_Channel = new List<cls_Channel>();
    }

}
