using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encap
{
    public partial class form_confirm : Form
    {
        public form_confirm(string s)
        {
            InitializeComponent();
            lb_describe.Text = s;
        }

        public form_confirm()
        {
            InitializeComponent();
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            label2.Text = "";
            string Time = DateTime.Now.ToString("MMddHH");
            //string Pass = (Convert.ToInt32(Time.Substring(0, 2)) * 2).ToString() + (Convert.ToInt32(Time.Substring(2, 2)) + 5).ToString();// + Time.Substring(5, 1) + Time.Substring(4, 1)；
            string Pass = (Convert.ToInt32(Time.Substring(0, 2)) * 2).ToString();
            if (txtb_confirm.Text.Trim() == Pass)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            else
            {
                txtb_confirm.Text = "";
                label2.ForeColor = Color.Tomato;
                label2.Text="密码错误,请重新输入";
            }
        }

        private void txtb_confirm_Click(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void txtb_confirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                btn_Confirm_Click(null,null);
            }
        }
    }
}
