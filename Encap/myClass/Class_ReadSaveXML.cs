using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SaveManger;

namespace MuSiTest
{
    class Class_ReadSaveXML
    {
        public SaveMangerClass saveMgeClass = new SaveMangerClass();        //文件的一些参数    
        public string parmXML = Environment.CurrentDirectory + "\\config\\parm.xml"; //XML的保存路径

        internal bool readXMLAndsetParm()
        {
            return readXML();
        }

        private bool readXML()
        {
            string str = string.Format("{0}\\config", Environment.CurrentDirectory);
            if (!Directory.Exists(str))
            {
                Directory.CreateDirectory(str);
            }


            if (File.Exists(parmXML))
            {
                FileStream sf = new FileStream(parmXML, FileMode.Open);
                saveMgeClass = (SaveMangerClass)XmlUtil.Deserialize(typeof(SaveMangerClass), sf);
                sf.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void saveXML()
        {
            string str = XmlUtil.Serializer(parmXML, typeof(SaveMangerClass), saveMgeClass);
        }



        public SaveMangerClass2 saveMgeClass2 = new SaveMangerClass2();        //文件的一些参数    

        internal bool readXML(string parmXMLFullName)
        {

            if (File.Exists(parmXMLFullName))
            {
                FileStream sf = new FileStream(parmXMLFullName, FileMode.Open);
                saveMgeClass2 = (SaveMangerClass2)XmlUtil.Deserialize(typeof(SaveMangerClass2), sf);
                sf.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void saveXML(string parmXMLFullName)
        {
            string str = XmlUtil.Serializer(parmXMLFullName, typeof(SaveMangerClass2), saveMgeClass2);
        }



    }
}
