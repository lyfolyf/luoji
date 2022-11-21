namespace Encap
{
    partial class AddNewProductForm
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
            this.lstBox_productList = new System.Windows.Forms.ListBox();
            this.bn_AddNewOne = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bn_Update = new System.Windows.Forms.Button();
            this.gb_Define = new System.Windows.Forms.GroupBox();
            this.bn_Save = new System.Windows.Forms.Button();
            this.bn_Apply = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtB_Gain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtB_Exposure = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB_Product_name = new System.Windows.Forms.TextBox();
            this.texBmsg = new System.Windows.Forms.TextBox();
            this.groupBoxImgPath = new System.Windows.Forms.GroupBox();
            this.lb_NGPic = new System.Windows.Forms.Label();
            this.lb_AllPic = new System.Windows.Forms.Label();
            this.btn_VisionPathChange = new System.Windows.Forms.Button();
            this.btn_OffsetPathChange = new System.Windows.Forms.Button();
            this.txtB_VisionPath = new System.Windows.Forms.TextBox();
            this.txtB_OffsetPath = new System.Windows.Forms.TextBox();
            this.lb_ProductName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.t_Gain = new System.Windows.Forms.TextBox();
            this.t_Exposure = new System.Windows.Forms.TextBox();
            this.gb_Define.SuspendLayout();
            this.groupBoxImgPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstBox_productList
            // 
            this.lstBox_productList.BackColor = System.Drawing.SystemColors.Info;
            this.lstBox_productList.FormattingEnabled = true;
            this.lstBox_productList.HorizontalScrollbar = true;
            this.lstBox_productList.ItemHeight = 12;
            this.lstBox_productList.Location = new System.Drawing.Point(323, 41);
            this.lstBox_productList.Name = "lstBox_productList";
            this.lstBox_productList.Size = new System.Drawing.Size(253, 508);
            this.lstBox_productList.TabIndex = 0;
            this.lstBox_productList.SelectedIndexChanged += new System.EventHandler(this.lstBox_productList_SelectedIndexChanged);
            // 
            // bn_AddNewOne
            // 
            this.bn_AddNewOne.Location = new System.Drawing.Point(12, 12);
            this.bn_AddNewOne.Name = "bn_AddNewOne";
            this.bn_AddNewOne.Size = new System.Drawing.Size(86, 23);
            this.bn_AddNewOne.TabIndex = 1;
            this.bn_AddNewOne.Text = "添加新产品";
            this.bn_AddNewOne.UseVisualStyleBackColor = true;
            this.bn_AddNewOne.Click += new System.EventHandler(this.bn_AddNewOne_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(321, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "所有类型";
            // 
            // bn_Update
            // 
            this.bn_Update.Location = new System.Drawing.Point(501, 15);
            this.bn_Update.Name = "bn_Update";
            this.bn_Update.Size = new System.Drawing.Size(75, 23);
            this.bn_Update.TabIndex = 3;
            this.bn_Update.Text = "刷新";
            this.bn_Update.UseVisualStyleBackColor = true;
            this.bn_Update.Click += new System.EventHandler(this.bn_Update_Click);
            // 
            // gb_Define
            // 
            this.gb_Define.Controls.Add(this.bn_Save);
            this.gb_Define.Controls.Add(this.bn_Apply);
            this.gb_Define.Controls.Add(this.label4);
            this.gb_Define.Controls.Add(this.txtB_Gain);
            this.gb_Define.Controls.Add(this.label3);
            this.gb_Define.Controls.Add(this.txtB_Exposure);
            this.gb_Define.Controls.Add(this.label2);
            this.gb_Define.Controls.Add(this.txtB_Product_name);
            this.gb_Define.ForeColor = System.Drawing.Color.Black;
            this.gb_Define.Location = new System.Drawing.Point(12, 42);
            this.gb_Define.Name = "gb_Define";
            this.gb_Define.Size = new System.Drawing.Size(277, 507);
            this.gb_Define.TabIndex = 4;
            this.gb_Define.TabStop = false;
            this.gb_Define.Text = "新产品参数定义";
            // 
            // bn_Save
            // 
            this.bn_Save.AccessibleDescription = "";
            this.bn_Save.Location = new System.Drawing.Point(169, 162);
            this.bn_Save.Name = "bn_Save";
            this.bn_Save.Size = new System.Drawing.Size(71, 23);
            this.bn_Save.TabIndex = 7;
            this.bn_Save.Text = "保存";
            this.bn_Save.UseVisualStyleBackColor = true;
            this.bn_Save.Click += new System.EventHandler(this.bn_Save_Click);
            // 
            // bn_Apply
            // 
            this.bn_Apply.AccessibleDescription = "";
            this.bn_Apply.Location = new System.Drawing.Point(38, 162);
            this.bn_Apply.Name = "bn_Apply";
            this.bn_Apply.Size = new System.Drawing.Size(71, 23);
            this.bn_Apply.TabIndex = 6;
            this.bn_Apply.Text = "生成";
            this.bn_Apply.UseVisualStyleBackColor = true;
            this.bn_Apply.Click += new System.EventHandler(this.bn_Apply_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "增益值：";
            // 
            // txtB_Gain
            // 
            this.txtB_Gain.Location = new System.Drawing.Point(113, 90);
            this.txtB_Gain.Name = "txtB_Gain";
            this.txtB_Gain.Size = new System.Drawing.Size(108, 21);
            this.txtB_Gain.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "曝光值：";
            // 
            // txtB_Exposure
            // 
            this.txtB_Exposure.Location = new System.Drawing.Point(113, 63);
            this.txtB_Exposure.Name = "txtB_Exposure";
            this.txtB_Exposure.Size = new System.Drawing.Size(108, 21);
            this.txtB_Exposure.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "产品名字：";
            // 
            // txtB_Product_name
            // 
            this.txtB_Product_name.Location = new System.Drawing.Point(113, 36);
            this.txtB_Product_name.Name = "txtB_Product_name";
            this.txtB_Product_name.Size = new System.Drawing.Size(108, 21);
            this.txtB_Product_name.TabIndex = 0;
            // 
            // texBmsg
            // 
            this.texBmsg.BackColor = System.Drawing.SystemColors.Info;
            this.texBmsg.Location = new System.Drawing.Point(12, 562);
            this.texBmsg.Multiline = true;
            this.texBmsg.Name = "texBmsg";
            this.texBmsg.ReadOnly = true;
            this.texBmsg.Size = new System.Drawing.Size(960, 37);
            this.texBmsg.TabIndex = 5;
            this.texBmsg.Text = "msg";
            this.texBmsg.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.texBmsg_MouseDoubleClick);
            // 
            // groupBoxImgPath
            // 
            this.groupBoxImgPath.Controls.Add(this.label5);
            this.groupBoxImgPath.Controls.Add(this.label6);
            this.groupBoxImgPath.Controls.Add(this.t_Gain);
            this.groupBoxImgPath.Controls.Add(this.t_Exposure);
            this.groupBoxImgPath.Controls.Add(this.lb_ProductName);
            this.groupBoxImgPath.Controls.Add(this.lb_NGPic);
            this.groupBoxImgPath.Controls.Add(this.lb_AllPic);
            this.groupBoxImgPath.Controls.Add(this.btn_VisionPathChange);
            this.groupBoxImgPath.Controls.Add(this.btn_OffsetPathChange);
            this.groupBoxImgPath.Controls.Add(this.txtB_VisionPath);
            this.groupBoxImgPath.Controls.Add(this.txtB_OffsetPath);
            this.groupBoxImgPath.Location = new System.Drawing.Point(597, 41);
            this.groupBoxImgPath.Name = "groupBoxImgPath";
            this.groupBoxImgPath.Size = new System.Drawing.Size(375, 508);
            this.groupBoxImgPath.TabIndex = 26;
            this.groupBoxImgPath.TabStop = false;
            this.groupBoxImgPath.Text = "产品配置参数";
            // 
            // lb_NGPic
            // 
            this.lb_NGPic.AutoSize = true;
            this.lb_NGPic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb_NGPic.Location = new System.Drawing.Point(12, 126);
            this.lb_NGPic.Name = "lb_NGPic";
            this.lb_NGPic.Size = new System.Drawing.Size(83, 12);
            this.lb_NGPic.TabIndex = 25;
            this.lb_NGPic.Text = "纠偏任务路径:";
            // 
            // lb_AllPic
            // 
            this.lb_AllPic.AutoSize = true;
            this.lb_AllPic.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lb_AllPic.Location = new System.Drawing.Point(12, 154);
            this.lb_AllPic.Name = "lb_AllPic";
            this.lb_AllPic.Size = new System.Drawing.Size(83, 12);
            this.lb_AllPic.TabIndex = 24;
            this.lb_AllPic.Text = "视觉任务路径:";
            // 
            // btn_VisionPathChange
            // 
            this.btn_VisionPathChange.Enabled = false;
            this.btn_VisionPathChange.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_VisionPathChange.Location = new System.Drawing.Point(319, 149);
            this.btn_VisionPathChange.Name = "btn_VisionPathChange";
            this.btn_VisionPathChange.Size = new System.Drawing.Size(32, 21);
            this.btn_VisionPathChange.TabIndex = 21;
            this.btn_VisionPathChange.Text = "...";
            this.btn_VisionPathChange.UseVisualStyleBackColor = true;
            this.btn_VisionPathChange.Click += new System.EventHandler(this.btn_VisionPathChange_Click);
            // 
            // btn_OffsetPathChange
            // 
            this.btn_OffsetPathChange.Enabled = false;
            this.btn_OffsetPathChange.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_OffsetPathChange.Location = new System.Drawing.Point(319, 122);
            this.btn_OffsetPathChange.Name = "btn_OffsetPathChange";
            this.btn_OffsetPathChange.Size = new System.Drawing.Size(32, 21);
            this.btn_OffsetPathChange.TabIndex = 20;
            this.btn_OffsetPathChange.Text = "...";
            this.btn_OffsetPathChange.UseVisualStyleBackColor = true;
            this.btn_OffsetPathChange.Click += new System.EventHandler(this.btn_OffsetPathChange_Click);
            // 
            // txtB_VisionPath
            // 
            this.txtB_VisionPath.Location = new System.Drawing.Point(104, 149);
            this.txtB_VisionPath.Name = "txtB_VisionPath";
            this.txtB_VisionPath.ReadOnly = true;
            this.txtB_VisionPath.Size = new System.Drawing.Size(201, 21);
            this.txtB_VisionPath.TabIndex = 17;
            this.txtB_VisionPath.MouseHover += new System.EventHandler(this.txtB_VisionPath_MouseHover);
            // 
            // txtB_OffsetPath
            // 
            this.txtB_OffsetPath.Location = new System.Drawing.Point(104, 122);
            this.txtB_OffsetPath.Name = "txtB_OffsetPath";
            this.txtB_OffsetPath.ReadOnly = true;
            this.txtB_OffsetPath.Size = new System.Drawing.Size(201, 21);
            this.txtB_OffsetPath.TabIndex = 16;
            this.txtB_OffsetPath.MouseHover += new System.EventHandler(this.txtB_OffsetPath_MouseHover);
            // 
            // lb_ProductName
            // 
            this.lb_ProductName.AutoSize = true;
            this.lb_ProductName.Location = new System.Drawing.Point(14, 33);
            this.lb_ProductName.Name = "lb_ProductName";
            this.lb_ProductName.Size = new System.Drawing.Size(71, 12);
            this.lb_ProductName.TabIndex = 31;
            this.lb_ProductName.Text = "ProductName";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(12, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 35;
            this.label5.Text = "曝光值:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(12, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "增益:";
            // 
            // t_Gain
            // 
            this.t_Gain.Location = new System.Drawing.Point(104, 92);
            this.t_Gain.Name = "t_Gain";
            this.t_Gain.ReadOnly = true;
            this.t_Gain.Size = new System.Drawing.Size(201, 21);
            this.t_Gain.TabIndex = 33;
            // 
            // t_Exposure
            // 
            this.t_Exposure.Location = new System.Drawing.Point(104, 65);
            this.t_Exposure.Name = "t_Exposure";
            this.t_Exposure.ReadOnly = true;
            this.t_Exposure.Size = new System.Drawing.Size(201, 21);
            this.t_Exposure.TabIndex = 32;
            // 
            // AddNewProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.groupBoxImgPath);
            this.Controls.Add(this.texBmsg);
            this.Controls.Add(this.gb_Define);
            this.Controls.Add(this.bn_Update);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bn_AddNewOne);
            this.Controls.Add(this.lstBox_productList);
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNewProductForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "增加新产品类型";
            this.Load += new System.EventHandler(this.AddNewProductForm_Load);
            this.gb_Define.ResumeLayout(false);
            this.gb_Define.PerformLayout();
            this.groupBoxImgPath.ResumeLayout(false);
            this.groupBoxImgPath.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstBox_productList;
        private System.Windows.Forms.Button bn_AddNewOne;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bn_Update;
        private System.Windows.Forms.GroupBox gb_Define;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtB_Product_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtB_Exposure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtB_Gain;
        private System.Windows.Forms.Button bn_Apply;
        private System.Windows.Forms.TextBox texBmsg;
        private System.Windows.Forms.Button bn_Save;
        private System.Windows.Forms.GroupBox groupBoxImgPath;
        private System.Windows.Forms.Label lb_ProductName;
        private System.Windows.Forms.Label lb_NGPic;
        private System.Windows.Forms.Label lb_AllPic;
        private System.Windows.Forms.Button btn_VisionPathChange;
        private System.Windows.Forms.Button btn_OffsetPathChange;
        private System.Windows.Forms.TextBox txtB_VisionPath;
        private System.Windows.Forms.TextBox txtB_OffsetPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox t_Gain;
        private System.Windows.Forms.TextBox t_Exposure;
    }
}