using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using DataAssemble;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlHelper;

namespace Encap
{
    public partial class AddNewProductForm : Form
    {
        bool applayed = false;
        ALL_PRODUCT_TYPE myALL_PRODUCT_TYPE = new ALL_PRODUCT_TYPE();

        public AddNewProductForm(ALL_PRODUCT_TYPE aLL_PRODUCT_TYPE)
        {
            InitializeComponent();
            myALL_PRODUCT_TYPE = aLL_PRODUCT_TYPE;
        }

        private void AddNewProductForm_Load(object sender, EventArgs e)
        {
            foreach (Control control in gb_Define.Controls)
            {
                control.Visible = false;
            }
            for (int i = 0; i < myALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
            {
                lstBox_productList.Items.Add(myALL_PRODUCT_TYPE.ProductInfoList[i].Product_name);
            }
        }

        public ALL_PRODUCT_TYPE GetNewALL_PRODUCT_TYPE()
        {
            return myALL_PRODUCT_TYPE;
        }

        private void bn_Update_Click(object sender, EventArgs e)
        {
            lstBox_productList.Items.Clear();
            for (int i = 0; i < myALL_PRODUCT_TYPE.ProductInfoList.Count; i++)
            {
                lstBox_productList.Items.Add(myALL_PRODUCT_TYPE.ProductInfoList[i].Product_name);
            }
        }

        private void bn_AddNewOne_Click(object sender, EventArgs e)
        {
            foreach (Control control in gb_Define.Controls)
            {
                control.Visible = true;
            }
            bn_AddNewOne.Enabled = false;
        }
        
        private void bn_Apply_Click(object sender, EventArgs e)
        {
            bn_AddNewOne.Enabled = true;
            if (txtB_Product_name.Text != "" && txtB_Exposure.Text!="" && txtB_Gain.Text != "")
            {
                foreach (ProductInfo item in myALL_PRODUCT_TYPE.ProductInfoList)
                {
                    if (item.Product_name == txtB_Product_name.Text.Trim())
                    {
                        MessageBox.Show("已经存在此产品类型");
                        return;
                    }
                }
                ProductInfo newProductInfo = new ProductInfo(txtB_Product_name.Text.Trim());
                try
                {
                    newProductInfo.cameraInfo.Exposure = Convert.ToDouble(txtB_Exposure.Text);
                    newProductInfo.cameraInfo.Gain = Convert.ToDouble(txtB_Gain.Text);
                }
                catch (Exception ex)
                {
                    texBmsg.Text="数据输入格式有误：" +ex.Message;
                    return;
                }
                try
                {
                    myALL_PRODUCT_TYPE.ProductInfoList.Add(newProductInfo);
                    CreatDir(new FileInfo(newProductInfo.OffsetToolPath).DirectoryName);
                    CreatDir(new FileInfo(newProductInfo.VisionToolsPath).DirectoryName);
                    CogToolBlock cogToolBlock = new CogToolBlock();
                    CogSerializer.SaveObjectToFile(cogToolBlock, newProductInfo.OffsetToolPath);
                    CogSerializer.SaveObjectToFile(cogToolBlock, newProductInfo.VisionToolsPath);
                    cogToolBlock.Dispose();
                    applayed = true;
                    texBmsg.Text = "生成成功";
                    MessageBox.Show($"生成成功");
                    bn_Update_Click(null, null);
                }
                catch (Exception ex)
                {
                    texBmsg.Text = "生成空白视觉任务失败：" + ex.Message;
                }
            }
            else
            {
                texBmsg.Text = "参数输入不能为空";
            }
        }

        private void texBmsg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            texBmsg.Text = "" ;
        }

        public void CreatDir(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            } 
        }

        public string SelecteFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "★请选择一个文件夹★";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                return foldPath;
            }
            else
            {
                return null;
            }
        }

        public void ToolTip(Control control,string tip)
        {
            // 创建the ToolTip 
            ToolTip toolTip1 = new ToolTip();
            // 设置显示样式
            toolTip1.AutoPopDelay = 5000;//提示信息的可见时间
            toolTip1.InitialDelay = 500;//事件触发多久后出现提示
            toolTip1.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            toolTip1.ShowAlways = true;//是否显示提示框
            //  设置伴随的对象.
            toolTip1.SetToolTip(control, tip);//设置提示按钮和提示内容
        }

        private void lstBox_productList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try{propertyGrid1.SelectedObject = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex];}catch{}
            try
            {
                lb_ProductName.Text = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].Product_name;
                txtB_OffsetPath.Text = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].OffsetToolPath;
                txtB_VisionPath.Text = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].VisionToolsPath;
                t_Exposure.Text = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].cameraInfo.Exposure.ToString();
                t_Gain.Text = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].cameraInfo.Gain.ToString();
            } 
            catch{ }
        }

        private void btn_OffsetPathChange_Click(object sender, EventArgs e)
        {
            string path= SelecteFolder();
            if (path!=null)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].OffsetToolPath);
                    string oldName = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].OffsetToolPath;
                    string newName = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].OffsetToolPath = path + "\\" + fileInfo.Name;
                    File.Copy(oldName, newName);
                    txtB_OffsetPath.Text = newName;
                }
                catch (Exception ex)
                {
                    texBmsg.Text = ex.Message;
                }
            }
        }

        private void btn_VisionPathChange_Click(object sender, EventArgs e)
        {
            string path = SelecteFolder();
            if (path!=null)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].VisionToolsPath);
                    string oldName = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].VisionToolsPath;
                    string newName = myALL_PRODUCT_TYPE.ProductInfoList[lstBox_productList.SelectedIndex].VisionToolsPath = path + "\\" + fileInfo.Name;
                    File.Copy(oldName, newName);
                    txtB_VisionPath.Text = newName;
                }
                catch (Exception ex)
                {
                    texBmsg.Text = ex.Message;
                }
            }
        }

        private void txtB_OffsetPath_MouseHover(object sender, EventArgs e)
        {
            ToolTip(txtB_OffsetPath, txtB_OffsetPath.Text);
        }

        private void txtB_VisionPath_MouseHover(object sender, EventArgs e)
        {
            ToolTip(txtB_VisionPath, txtB_VisionPath.Text);
        }

        private void bn_Save_Click(object sender, EventArgs e)
        {
            bn_AddNewOne.Enabled = true;
            if (applayed)
            {
                string ProductInfoPath = @"D:\Lead2DParameter\Config\ALL_PRODUCT_TYPE.xml";
                XmlSerializerHelper.WriteXML((Object)myALL_PRODUCT_TYPE, ProductInfoPath, typeof(ALL_PRODUCT_TYPE));
                foreach (Control control in gb_Define.Controls)
                {
                    control.Visible = false;
                }
                MessageBox.Show("已保存");
            }
            else
            {
                texBmsg.Text = "先生成新的产品类型";
            }
        }
    }
}
