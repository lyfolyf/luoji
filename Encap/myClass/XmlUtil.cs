using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Xml序列化与反序列化
/// </summary>
public class XmlUtil
{
    #region 反序列化
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="xml">XML字符串</param>
    /// <returns></returns>
    public static object Deserialize(Type type, string xml)
    {
        try
        {
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer xmldes = new XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }
        catch (Exception e)
        {

            return null;
        }
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="type"></param>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static object Deserialize(Type type, Stream stream)
    {
        XmlSerializer xmldes = new XmlSerializer(type);
        return xmldes.Deserialize(stream);
    }
    #endregion

    #region 序列化
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="type">类型</param>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    public static string Serializer(string fileName, Type type, object obj)
    {
        string str = ""; ;
        try
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            TextWriter tw = new StreamWriter(fileName);

            xml.Serialize(tw, obj);
            //序列化对象
            xml.Serialize(Stream, obj);
            tw.Close();


            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

        }
        //catch (InvalidOperationException)
        catch(Exception ex)
        {
           // System.Windows.Forms.MessageBox.Show(ex.ToString());
        }

    


  
        return str;
    }

    #endregion
}