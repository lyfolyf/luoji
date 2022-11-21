using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MuSiTest;

namespace 高精度数字电源
{
    class Class_mainParm
    {
        internal Class_ReadSaveXML cls_readsaveXML = new Class_ReadSaveXML();
        internal int nSelCh = 0;     //当前是第几个通道

    }


    public class cls_Channel
    {
        public string strChannel;
        public int nTrig;
        public int nLight;
        public int nTG;
        public int nLightValue;

        public BX_struct_space._stuPulse stuLight;
        public BX_struct_space._stuPulse stuOut;

        public cls_Channel MyCopyTo()
        {
            cls_Channel cls_ChTemp = new cls_Channel();
            cls_ChTemp.strChannel = strChannel;
            cls_ChTemp.nTrig = nTrig;
            cls_ChTemp.nLight = nLight;
            cls_ChTemp.nTG = nTG;
            cls_ChTemp.nLightValue = nLightValue;

            cls_ChTemp.stuLight = stuLight;
            cls_ChTemp.stuOut = stuOut;

            return cls_ChTemp;
        }
    }



}
