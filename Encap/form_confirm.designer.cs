namespace Encap
{
    partial class form_confirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.txtb_confirm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_describe = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.Location = new System.Drawing.Point(363, 42);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(62, 23);
            this.btn_Confirm.TabIndex = 5;
            this.btn_Confirm.Text = "确定";
            this.btn_Confirm.UseVisualStyleBackColor = true;
            this.btn_Confirm.Click += new System.EventHandler(this.btn_Confirm_Click);
            // 
            // txtb_confirm
            // 
            this.txtb_confirm.Location = new System.Drawing.Point(152, 43);
            this.txtb_confirm.Name = "txtb_confirm";
            this.txtb_confirm.PasswordChar = '*';
            this.txtb_confirm.Size = new System.Drawing.Size(183, 21);
            this.txtb_confirm.TabIndex = 4;
            this.txtb_confirm.Click += new System.EventHandler(this.txtb_confirm_Click);
            this.txtb_confirm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtb_confirm_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "请输入密码：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 6;
            // 
            // lb_describe
            // 
            this.lb_describe.AutoSize = true;
            this.lb_describe.Location = new System.Drawing.Point(150, 12);
            this.lb_describe.Name = "lb_describe";
            this.lb_describe.Size = new System.Drawing.Size(0, 12);
            this.lb_describe.TabIndex = 7;
            // 
            // form_confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(496, 102);
            this.Controls.Add(this.lb_describe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.txtb_confirm);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(512, 141);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 141);
            this.Name = "form_confirm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "权限限制";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.TextBox txtb_confirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_describe;
    }
}