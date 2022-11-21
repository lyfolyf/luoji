using System;
using System.Text;
using System.IO;
using System.Configuration;

namespace Encap
{
    /// <summary>
    /// 作者:NatureSex 创建时间:2011-03-25 17:09:07
    /// 名称:简单日志记录类
    /// 默认使用配置文件APPSetting名称为FilePath节点值
    /// 一个范围内写文件不会马上关闭流,需要手动调用Dispose方法释放,或者使用using实例化该类
    /// </summary>

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 一般
        /// </summary>
        Info = 1,


        /// <summary>
        /// 警告
        /// </summary>
        Warning,

        /// <summary>
        /// 错误
        /// </summary>
        Error,
    }

    public class SimpleLogger : IDisposable
    {
        private static string appSettingPath = ConfigurationManager.AppSettings["FilePath"];

        private static string logPath = null;

        /// <summary>
        /// 存放路径
        /// </summary>
        public string LogPath
        {
            get { return logPath; }
            set { logPath = value; }
        }

        private static int logSize = 2048;
        /// <summary>
        /// 每个日志文件大小(kb)
        /// </summary>
        public static int LogSize
        {
            get { return logSize; }
            set { logSize = value; }
        }

        private static int maxCount = 5;

        /// <summary>
        /// 每天日志文件最大记录数量
        /// </summary>
        public static int MaxCount
        {
            get { return maxCount; }
            set { maxCount = value; }
        }

        private static string logSuffix = "log";

        /// <summary>
        /// 日志文件后缀名
        /// </summary>
        public static string LogSuffix
        {
            get { return logSuffix; }
            set { logSuffix = value; }
        }

        private static int lineCharCount = 60;
        /// <summary>
        /// 行字数,默认60
        /// </summary>
        public static int LineCharCount
        {
            get { return lineCharCount; }
            set { lineCharCount = value; }
        }

        FileStream newfs = null;
        FileInfo openfile = null;
        StreamWriter sw = null;

        static SimpleLogger()
        {

        }

        /// <summary>
        /// 使用配置文件AppSetting节点name为FilePath中value路径实例化
        /// </summary>
        public SimpleLogger()
        {

            if (string.IsNullOrEmpty(logPath) && appSettingPath != "")
            {
                if (logPath == null)
                {
                    logPath = appSettingPath;
                    CreateMatchFolder(logPath);
                }
            }
        }

        /// <summary>
        /// 使用自定义的路径实例化
        /// </summary>
        /// <param name="logPathPrams"></param>
        public SimpleLogger(string logPathPrams)
        {
            if (!Path.HasExtension(logPathPrams))
            {
                if (logPath == null)
                {
                    logPath = logPathPrams;
                    CreateMatchFolder(logPath);
                }
            }
            else
            {
                throw new Exception("路径不能包含文件名称!");
            }
        }

        private static void CreateMatchFolder(string path)
        {
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 写入日常日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="info">内容</param>
        public bool WriteInfoLog(string title, LogLevel loggerLevel, string info)
        {
            return WriteFile(logPath, title, loggerLevel, info);
        }

        /// <summary>
        ///  写入日常日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="info">内容</param>
        /// <param name="autoNewLine">是否自动换行</param>
        /// <returns></returns>
        public bool WriteInfoLog(string title, string info, bool autoNewLine)
        {
            string[] newinfo = SplitString(info);
            return WriteFile(logPath, title, LogLevel.Info, newinfo);
        }

        /// <summary>
        /// 写入错误日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="info">内容</param>
        public bool WriteErrorLog(string title, Exception ex)
        {
            return WriteFile(logPath, title, LogLevel.Error, ex.Message);
        }

        /// <summary>
        /// 写入错误日志信息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="info">内容</param>
        /// <param name="autoNewLine">是否自动换行</param>
        /// <returns></returns>
        public bool WriteErrorLog(string title, Exception ex, bool autoNewLine)
        {
            string[] newinfo = SplitString(ex.Message);
            return WriteFile(logPath, title, LogLevel.Error, newinfo);
        }

        //lineCharCount个字换一行
        private string[] SplitString(string info)
        {
            int charLen = info.Length;
            int line = (int)Math.Ceiling((charLen / (double)lineCharCount));
            string[] splitArray = new string[line];//行数
            for (int i = 0; i < line; i++)
            {
                if (charLen < lineCharCount)
                {
                    splitArray[i] = info;
                }
                else if ((charLen - (i * lineCharCount)) < lineCharCount)
                {
                    splitArray[i] = info.Substring(i * lineCharCount, charLen - (i * lineCharCount));
                }
                else
                {
                    splitArray[i] = info.Substring(i * lineCharCount, lineCharCount);
                }
            }
            return splitArray;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="path"></param>
        /// <param name="title"></param>
        /// <param name="level"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private bool WriteFile(string path, string title, LogLevel level, params string[] info)
        {
            string fileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd"), logSuffix);
            string logFile = string.Format("{0}/{1}", path, fileName);
            if (!File.Exists(logFile))
            {
                CreateNewLog(logFile);
            }

            if (openfile == null || sw == null)
            {
                openfile = new FileInfo(logFile);
                long sizeKB = (openfile.Length / 1024);
                if (sizeKB > logSize)
                {
                    logFile = FindCanWriteFileName(path, fileName);
                    CreateNewLog(logFile);
                    openfile = new FileInfo(logFile);
                }
                newfs = openfile.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                sw = new StreamWriter(newfs);
            }

            if (sw == null) return false;
            //sw.WriteLine("<{0}>", title);
            sw.WriteLine("{0}   Time:{1}   Rate:{2}", title, DateTime.Now.ToString("MM-dd HH:mm:ss.fff"), level == LogLevel.Info ? "info" : "error");
            //sw.WriteLine();
            if (info != null)
            {
                foreach (string str in info)
                {
                    sw.WriteLine(str);
                }
            }
            sw.WriteLine();
            sw.Flush();
            return true;

        }
        /// <summary>
        /// 建立一个新日志文件
        /// </summary>
        /// <param name="logFile"></param>
        private void CreateNewLog(string logFile)
        {
            if (!File.Exists(logFile))
            {
                using (newfs = new FileStream(logFile, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    sw = new StreamWriter(newfs, Encoding.UTF8);
                    sw.WriteLine("文件创建时间:{0}", DateTime.Now.ToString());
                    sw.WriteLine("文件最大记录大小:{0}KB(与实际可能会有出入)", logSize);
                    sw.WriteLine(sw.NewLine);
                    newfs.Flush();
                    sw.Dispose();
                    sw = null;
                }
            }
        }

        /// <summary>
        /// 寻找一个当天可写的文件,并且不大于设置的文件大小
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string FindCanWriteFileName(string path, string name)
        {
            string newName = string.Empty;
            long sizeKB = 0;
            FileInfo tempFi = null;
            for (int i = 1; i <= MaxCount; i++)
            {
                newName = string.Format("{0}/{2}-{1}", path, name, i);
                if (!File.Exists(newName))
                {
                    break;
                }
                else
                {
                    tempFi = new FileInfo(newName);
                    sizeKB = (tempFi.Length / 1024);
                    if (sizeKB < logSize)
                    {
                        break;
                    }
                }
            }
            tempFi = null;
            return newName;
        }

        #region Dispose

        private bool disposed = false;

        /// <summary>
        /// 垃圾回收器执行函数
        /// </summary>
        ~SimpleLogger()
        {
            //如果有就释放非托管
            Dispose(false);
        }

        /// <summary>
        /// 关闭并是否资源
        /// </summary>
        public void Dispose()
        {
            //全部释放
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// 关闭并是否资源
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 关闭并是否资源
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            // 清理托管资源
            if (disposing)
            {

                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
                if (newfs != null)
                {
                    newfs.Close();
                    newfs.Dispose();
                    newfs = null;
                }

                if (openfile != null)
                {
                    openfile = null;
                }
            }
            //非托管
            disposed = true;
        }
        #endregion
    }

    public class ProcessTime
    {
        /// <summary>
        /// 计算时间
        /// </summary>
        /// <param name="function">要被执行的代码</param>
        /// <returns>执行这一段代码耗时，单位：毫秒</returns>
        public static string Stopwatch(Action function)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //开始执行业务代码
            function();

            sw.Stop();
            TimeSpan timeSpan = sw.Elapsed;

            return (timeSpan.TotalMilliseconds) + "ms";
        }
    }
}


