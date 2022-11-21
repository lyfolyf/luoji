using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encap
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] processes = Process.GetProcessesByName(Application.ProductName);
            int id = Process.GetCurrentProcess().Id;
            if (processes.Length >= 2)
            {
                //MessageBox.Show("请勿重复启动！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
                //return;
                foreach (var item in processes)
                {
                    if (item.Id != id)
                    {
                        item.Kill();
                    }
                }
                //processes = Process.GetProcessesByName(Application.ProductName);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

        }
    }
}
