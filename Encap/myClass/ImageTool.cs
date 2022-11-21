using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Encap
{

    public  class ImageTools
    {
        /// <summary>
        /// 图片剪切
        /// </summary>
        /// <param name="SrcImage"></param>
        /// <param name="pos"></param>
        /// <param name="cutWidth"></param>
        /// <param name="cutHeight"></param>
        /// <returns></returns>
        public  Image cutImage(Image SrcImage, System.Drawing.Point pos, int cutWidth, int cutHeight)
        {
            Image cutedImage = null;
            Bitmap bmpDest = new Bitmap(cutWidth, cutHeight);
            Graphics g = Graphics.FromImage(bmpDest);
            Rectangle rectSource = new Rectangle(pos.X, pos.Y, cutWidth, cutHeight);
            Rectangle rectDest = new Rectangle(0, 0, cutWidth, cutHeight);
            g.DrawImage(SrcImage, rectDest, rectSource, GraphicsUnit.Pixel);
            cutedImage = (Image)bmpDest;
            g.Dispose();
            return cutedImage;
        }



        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        public static Image JoinImage(Image Img1, Image Img2)
        {
            int targetHeight = 0, targetWidth = 0;
            targetWidth = Img1.Width + Img2.Width;
            targetHeight = Math.Max(Img1.Height, Img2.Height);
            Bitmap joinedBitmap = new Bitmap(targetWidth, targetHeight);
            Graphics graph = Graphics.FromImage(joinedBitmap);
            graph.DrawImage(Img1, 0, 0, Img1.Width, Img1.Height);
            graph.DrawImage(Img2, Img1.Width, 0, Img2.Width, Img2.Height);
            return joinedBitmap;
        }
    }
    
}
