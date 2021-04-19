namespace LIMS.Views
{
    partial class QCDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QCDialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbReason = new System.Windows.Forms.Label();
            this.btNO = new DevExpress.XtraEditors.SimpleButton();
            this.btYes = new DevExpress.XtraEditors.SimpleButton();
            this.txtSampleName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.txtSampleSpecEN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDesEN = new System.Windows.Forms.Label();
            this.lblDesTW = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtSampleSpecTW = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblDesCN = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSampleSpecCN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblDesVN = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtSampleSpecVN = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblLabName = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtLabName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtTypeName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.laLine = new System.Windows.Forms.Label();
            this.cmLine = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 280;
            this.pictureBox1.TabStop = false;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReason.ForeColor = System.Drawing.Color.Black;
            this.lbReason.Location = new System.Drawing.Point(54, 51);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(230, 18);
            this.lbReason.TabIndex = 281;
            this.lbReason.Text = "Please enter the information:";
            // 
            // btNO
            // 
            this.btNO.Location = new System.Drawing.Point(390, 354);
            this.btNO.Name = "btNO";
            this.btNO.Size = new System.Drawing.Size(67, 23);
            this.btNO.TabIndex = 8;
            this.btNO.Text = "Cancel";
            // 
            // btYes
            // 
            this.btYes.Location = new System.Drawing.Point(324, 354);
            this.btYes.Name = "btYes";
            this.btYes.Size = new System.Drawing.Size(60, 23);
            this.btYes.TabIndex = 7;
            this.btYes.Text = "Confirm";
            // 
            // txtSampleName
            // 
            this.txtSampleName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSampleName.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSampleName.Border.Class = "RibbonGalleryContainer";
            this.txtSampleName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSampleName.Border.PaddingTop = 2;
            this.txtSampleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSampleName.Location = new System.Drawing.Point(221, 74);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.ReadOnly = true;
            this.txtSampleName.Size = new System.Drawing.Size(243, 21);
            this.txtSampleName.TabIndex = 0;
            this.txtSampleName.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleName.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(25, 95);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 1);
            this.panel4.TabIndex = 285;
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblSampleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleName.Location = new System.Drawing.Point(22, 77);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(111, 18);
            this.lblSampleName.TabIndex = 284;
            this.lblSampleName.Text = "Sample Name:";
            // 
            // txtSampleSpecEN
            // 
            this.txtSampleSpecEN.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSampleSpecEN.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSampleSpecEN.Border.Class = "RibbonGalleryContainer";
            this.txtSampleSpecEN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSampleSpecEN.Border.PaddingTop = 2;
            this.txtSampleSpecEN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSampleSpecEN.Location = new System.Drawing.Point(221, 155);
            this.txtSampleSpecEN.Name = "txtSampleSpecEN";
            this.txtSampleSpecEN.Size = new System.Drawing.Size(243, 21);
            this.txtSampleSpecEN.TabIndex = 3;
            this.txtSampleSpecEN.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleSpecEN.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(25, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 1);
            this.panel1.TabIndex = 288;
            // 
            // lblDesEN
            // 
            this.lblDesEN.AutoSize = true;
            this.lblDesEN.BackColor = System.Drawing.Color.Transparent;
            this.lblDesEN.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblDesEN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDesEN.Location = new System.Drawing.Point(22, 158);
            this.lblDesEN.Name = "lblDesEN";
            this.lblDesEN.Size = new System.Drawing.Size(131, 18);
            this.lblDesEN.TabIndex = 287;
            this.lblDesEN.Text = "Description (EN):";
            // 
            // lblDesTW
            // 
            this.lblDesTW.AutoSize = true;
            this.lblDesTW.BackColor = System.Drawing.Color.Transparent;
            this.lblDesTW.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblDesTW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDesTW.Location = new System.Drawing.Point(22, 185);
            this.lblDesTW.Name = "lblDesTW";
            this.lblDesTW.Size = new System.Drawing.Size(135, 18);
            this.lblDesTW.TabIndex = 287;
            this.lblDesTW.Text = "Description (TW):";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(25, 203);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 1);
            this.panel2.TabIndex = 288;
            // 
            // txtSampleSpecTW
            // 
            this.txtSampleSpecTW.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSampleSpecTW.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSampleSpecTW.Border.Class = "RibbonGalleryContainer";
            this.txtSampleSpecTW.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSampleSpecTW.Border.PaddingTop = 2;
            this.txtSampleSpecTW.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSampleSpecTW.Location = new System.Drawing.Point(221, 184);
            this.txtSampleSpecTW.Name = "txtSampleSpecTW";
            this.txtSampleSpecTW.Size = new System.Drawing.Size(243, 21);
            this.txtSampleSpecTW.TabIndex = 4;
            this.txtSampleSpecTW.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleSpecTW.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // lblDesCN
            // 
            this.lblDesCN.AutoSize = true;
            this.lblDesCN.BackColor = System.Drawing.Color.Transparent;
            this.lblDesCN.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblDesCN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDesCN.Location = new System.Drawing.Point(22, 211);
            this.lblDesCN.Name = "lblDesCN";
            this.lblDesCN.Size = new System.Drawing.Size(131, 18);
            this.lblDesCN.TabIndex = 287;
            this.lblDesCN.Text = "Description (CN):";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(25, 230);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 1);
            this.panel3.TabIndex = 288;
            // 
            // txtSampleSpecCN
            // 
            this.txtSampleSpecCN.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSampleSpecCN.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSampleSpecCN.Border.Class = "RibbonGalleryContainer";
            this.txtSampleSpecCN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSampleSpecCN.Border.PaddingTop = 2;
            this.txtSampleSpecCN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSampleSpecCN.Location = new System.Drawing.Point(221, 211);
            this.txtSampleSpecCN.Name = "txtSampleSpecCN";
            this.txtSampleSpecCN.Size = new System.Drawing.Size(243, 21);
            this.txtSampleSpecCN.TabIndex = 5;
            this.txtSampleSpecCN.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleSpecCN.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // lblDesVN
            // 
            this.lblDesVN.AutoSize = true;
            this.lblDesVN.BackColor = System.Drawing.Color.Transparent;
            this.lblDesVN.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblDesVN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDesVN.Location = new System.Drawing.Point(22, 238);
            this.lblDesVN.Name = "lblDesVN";
            this.lblDesVN.Size = new System.Drawing.Size(132, 18);
            this.lblDesVN.TabIndex = 287;
            this.lblDesVN.Text = "Description (VN):";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(25, 257);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 1);
            this.panel5.TabIndex = 288;
            // 
            // txtSampleSpecVN
            // 
            this.txtSampleSpecVN.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSampleSpecVN.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSampleSpecVN.Border.Class = "RibbonGalleryContainer";
            this.txtSampleSpecVN.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSampleSpecVN.Border.PaddingTop = 2;
            this.txtSampleSpecVN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSampleSpecVN.Location = new System.Drawing.Point(221, 237);
            this.txtSampleSpecVN.Name = "txtSampleSpecVN";
            this.txtSampleSpecVN.Size = new System.Drawing.Size(243, 21);
            this.txtSampleSpecVN.TabIndex = 6;
            this.txtSampleSpecVN.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleSpecVN.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // lblLabName
            // 
            this.lblLabName.AutoSize = true;
            this.lblLabName.BackColor = System.Drawing.Color.Transparent;
            this.lblLabName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLabName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLabName.Location = new System.Drawing.Point(22, 103);
            this.lblLabName.Name = "lblLabName";
            this.lblLabName.Size = new System.Drawing.Size(86, 18);
            this.lblLabName.TabIndex = 284;
            this.lblLabName.Text = "Lab Name:";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Location = new System.Drawing.Point(25, 122);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 1);
            this.panel6.TabIndex = 285;
            // 
            // txtLabName
            // 
            this.txtLabName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLabName.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtLabName.Border.Class = "RibbonGalleryContainer";
            this.txtLabName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtLabName.Border.PaddingTop = 2;
            this.txtLabName.Enabled = false;
            this.txtLabName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtLabName.Location = new System.Drawing.Point(221, 101);
            this.txtLabName.Name = "txtLabName";
            this.txtLabName.ReadOnly = true;
            this.txtLabName.Size = new System.Drawing.Size(243, 21);
            this.txtLabName.TabIndex = 1;
            this.txtLabName.WatermarkColor = System.Drawing.Color.Silver;
            this.txtLabName.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblTypeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTypeName.Location = new System.Drawing.Point(22, 130);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(96, 18);
            this.lblTypeName.TabIndex = 284;
            this.lblTypeName.Text = "Type Name:";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.Location = new System.Drawing.Point(25, 149);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 1);
            this.panel7.TabIndex = 285;
            // 
            // txtTypeName
            // 
            this.txtTypeName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtTypeName.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtTypeName.Border.Class = "RibbonGalleryContainer";
            this.txtTypeName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtTypeName.Border.PaddingTop = 2;
            this.txtTypeName.Enabled = false;
            this.txtTypeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtTypeName.Location = new System.Drawing.Point(221, 127);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.ReadOnly = true;
            this.txtTypeName.Size = new System.Drawing.Size(243, 21);
            this.txtTypeName.TabIndex = 2;
            this.txtTypeName.WatermarkColor = System.Drawing.Color.Silver;
            this.txtTypeName.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // laLine
            // 
            this.laLine.AutoSize = true;
            this.laLine.BackColor = System.Drawing.Color.Transparent;
            this.laLine.Font = new System.Drawing.Font("Georgia", 12F);
            this.laLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.laLine.Location = new System.Drawing.Point(22, 265);
            this.laLine.Name = "laLine";
            this.laLine.Size = new System.Drawing.Size(61, 18);
            this.laLine.TabIndex = 290;
            this.laLine.Text = "产品别:";
            // 
            // cmLine
            // 
            this.cmLine.BackColor = System.Drawing.Color.White;
            this.cmLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmLine.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmLine.FormattingEnabled = true;
            this.cmLine.Location = new System.Drawing.Point(221, 261);
            this.cmLine.Name = "cmLine";
            this.cmLine.Size = new System.Drawing.Size(217, 25);
            this.cmLine.TabIndex = 292;
            // 
            // QCDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.ClientSize = new System.Drawing.Size(519, 398);
            this.Controls.Add(this.cmLine);
            this.Controls.Add(this.laLine);
            this.Controls.Add(this.txtSampleSpecVN);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.txtSampleSpecCN);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.txtSampleSpecTW);
            this.Controls.Add(this.lblDesVN);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblDesCN);
            this.Controls.Add(this.txtSampleSpecEN);
            this.Controls.Add(this.lblDesTW);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblDesEN);
            this.Controls.Add(this.txtTypeName);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.txtLabName);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.lblTypeName);
            this.Controls.Add(this.txtSampleName);
            this.Controls.Add(this.lblLabName);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.lblSampleName);
            this.Controls.Add(this.btNO);
            this.Controls.Add(this.btYes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbReason);
            this.MinimumSize = new System.Drawing.Size(400, 166);
            this.Name = "QCDialog";
            this.Text = "FEPV IT";
            this.Controls.SetChildIndex(this.lbReason, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.btYes, 0);
            this.Controls.SetChildIndex(this.btNO, 0);
            this.Controls.SetChildIndex(this.lblSampleName, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.lblLabName, 0);
            this.Controls.SetChildIndex(this.txtSampleName, 0);
            this.Controls.SetChildIndex(this.lblTypeName, 0);
            this.Controls.SetChildIndex(this.panel6, 0);
            this.Controls.SetChildIndex(this.txtLabName, 0);
            this.Controls.SetChildIndex(this.panel7, 0);
            this.Controls.SetChildIndex(this.txtTypeName, 0);
            this.Controls.SetChildIndex(this.lblDesEN, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lblDesTW, 0);
            this.Controls.SetChildIndex(this.txtSampleSpecEN, 0);
            this.Controls.SetChildIndex(this.lblDesCN, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.lblDesVN, 0);
            this.Controls.SetChildIndex(this.txtSampleSpecTW, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.txtSampleSpecCN, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.txtSampleSpecVN, 0);
            this.Controls.SetChildIndex(this.laLine, 0);
            this.Controls.SetChildIndex(this.cmLine, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbReason;
        private DevExpress.XtraEditors.SimpleButton btNO;
        private DevExpress.XtraEditors.SimpleButton btYes;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleName;
        public System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblSampleName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleSpecEN;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDesEN;
        private System.Windows.Forms.Label lblDesTW;
        public System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleSpecTW;
        private System.Windows.Forms.Label lblDesCN;
        public System.Windows.Forms.Panel panel3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleSpecCN;
        private System.Windows.Forms.Label lblDesVN;
        public System.Windows.Forms.Panel panel5;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleSpecVN;
        private System.Windows.Forms.Label lblLabName;
        public System.Windows.Forms.Panel panel6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLabName;
        private System.Windows.Forms.Label lblTypeName;
        public System.Windows.Forms.Panel panel7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTypeName;
        private System.Windows.Forms.Label laLine;
        private System.Windows.Forms.ComboBox cmLine;
    }
}