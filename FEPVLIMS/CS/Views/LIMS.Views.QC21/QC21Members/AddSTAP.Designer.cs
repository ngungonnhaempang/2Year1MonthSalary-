namespace LIMS.Views.QC21Members
{
    partial class AddSTAP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSTAP));
            this.cbHasChart = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dateProdDate = new DevExpress.XtraEditors.DateEdit();
            this.lblRemark = new System.Windows.Forms.Label();
            this.txtRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblProdDate = new System.Windows.Forms.Label();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbReason = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.lblCreateType = new System.Windows.Forms.Label();
            this.cmbCreateType = new System.Windows.Forms.ComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbHasChart
            // 
            this.cbHasChart.AutoSize = true;
            this.cbHasChart.Checked = true;
            this.cbHasChart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHasChart.Font = new System.Drawing.Font("Georgia", 12F);
            this.cbHasChart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cbHasChart.Location = new System.Drawing.Point(265, 162);
            this.cbHasChart.Margin = new System.Windows.Forms.Padding(4);
            this.cbHasChart.Name = "cbHasChart";
            this.cbHasChart.Size = new System.Drawing.Size(68, 28);
            this.cbHasChart.TabIndex = 459;
            this.cbHasChart.Text = "YES";
            this.cbHasChart.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(54, 163);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 24);
            this.label3.TabIndex = 457;
            this.label3.Text = "HasChart:";
            // 
            // dateProdDate
            // 
            this.dateProdDate.EditValue = new System.DateTime(2017, 11, 24, 0, 0, 0, 0);
            this.dateProdDate.Location = new System.Drawing.Point(719, 115);
            this.dateProdDate.Margin = new System.Windows.Forms.Padding(4);
            this.dateProdDate.Name = "dateProdDate";
            this.dateProdDate.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dateProdDate.Properties.Appearance.Options.UseFont = true;
            this.dateProdDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateProdDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dateProdDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateProdDate.Properties.EditFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.dateProdDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateProdDate.Properties.Mask.EditMask = "yyyy-MM-dd HH:mm:ss";
            this.dateProdDate.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
            this.dateProdDate.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
            this.dateProdDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateProdDate.Size = new System.Drawing.Size(223, 24);
            this.dateProdDate.TabIndex = 451;
            // 
            // lblRemark
            // 
            this.lblRemark.AutoSize = true;
            this.lblRemark.BackColor = System.Drawing.Color.Transparent;
            this.lblRemark.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblRemark.Location = new System.Drawing.Point(54, 201);
            this.lblRemark.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRemark.Name = "lblRemark";
            this.lblRemark.Size = new System.Drawing.Size(87, 24);
            this.lblRemark.TabIndex = 450;
            this.lblRemark.Text = "Remark:";
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtRemark.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtRemark.Border.Class = "RibbonGalleryContainer";
            this.txtRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtRemark.Border.PaddingTop = 2;
            this.txtRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtRemark.Location = new System.Drawing.Point(56, 229);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(4);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(899, 40);
            this.txtRemark.TabIndex = 449;
            this.txtRemark.WatermarkColor = System.Drawing.Color.Silver;
            this.txtRemark.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Location = new System.Drawing.Point(511, 143);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(213, 1);
            this.panel4.TabIndex = 441;
            // 
            // lblProdDate
            // 
            this.lblProdDate.AutoSize = true;
            this.lblProdDate.BackColor = System.Drawing.Color.Transparent;
            this.lblProdDate.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblProdDate.Location = new System.Drawing.Point(514, 115);
            this.lblProdDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProdDate.Name = "lblProdDate";
            this.lblProdDate.Size = new System.Drawing.Size(100, 24);
            this.lblProdDate.TabIndex = 439;
            this.lblProdDate.Text = "ProdDate:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(786, 307);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 31);
            this.btnCancel.TabIndex = 428;
            this.btnCancel.Text = "Cancel";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(652, 307);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(80, 31);
            this.btnOk.TabIndex = 427;
            this.btnOk.Text = "Confirm";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(43, 63);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 429;
            this.pictureBox1.TabStop = false;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReason.ForeColor = System.Drawing.Color.Black;
            this.lbReason.Location = new System.Drawing.Point(72, 62);
            this.lbReason.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(299, 23);
            this.lbReason.TabIndex = 430;
            this.lbReason.Text = "Please enter the information:";
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.Window;
            this.panel10.Location = new System.Drawing.Point(54, 149);
            this.panel10.Margin = new System.Windows.Forms.Padding(4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(187, 1);
            this.panel10.TabIndex = 455;
            // 
            // lblCreateType
            // 
            this.lblCreateType.AutoSize = true;
            this.lblCreateType.BackColor = System.Drawing.Color.Transparent;
            this.lblCreateType.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblCreateType.Location = new System.Drawing.Point(50, 124);
            this.lblCreateType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreateType.Name = "lblCreateType";
            this.lblCreateType.Size = new System.Drawing.Size(117, 24);
            this.lblCreateType.TabIndex = 454;
            this.lblCreateType.Text = "CreateType:";
            // 
            // cmbCreateType
            // 
            this.cmbCreateType.BackColor = System.Drawing.Color.White;
            this.cmbCreateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateType.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbCreateType.FormattingEnabled = true;
            this.cmbCreateType.Location = new System.Drawing.Point(265, 118);
            this.cmbCreateType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCreateType.Name = "cmbCreateType";
            this.cmbCreateType.Size = new System.Drawing.Size(221, 25);
            this.cmbCreateType.TabIndex = 453;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.Window;
            this.panel11.Location = new System.Drawing.Point(58, 186);
            this.panel11.Margin = new System.Windows.Forms.Padding(4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(213, 1);
            this.panel11.TabIndex = 458;
            // 
            // AddSTAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(966, 384);
            this.Controls.Add(this.cbHasChart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.cmbCreateType);
            this.Controls.Add(this.lblCreateType);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.dateProdDate);
            this.Controls.Add(this.lblRemark);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.lblProdDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbReason);
            this.Name = "AddSTAP";
            this.Text = "AddSTAP";
            this.Controls.SetChildIndex(this.lbReason, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.btnOk, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.lblProdDate, 0);
            this.Controls.SetChildIndex(this.panel4, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            this.Controls.SetChildIndex(this.lblRemark, 0);
            this.Controls.SetChildIndex(this.dateProdDate, 0);
            this.Controls.SetChildIndex(this.panel10, 0);
            this.Controls.SetChildIndex(this.lblCreateType, 0);
            this.Controls.SetChildIndex(this.cmbCreateType, 0);
            this.Controls.SetChildIndex(this.panel11, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbHasChart, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbHasChart;
        private System.Windows.Forms.Label label3;
        public DevExpress.XtraEditors.DateEdit dateProdDate;
        private System.Windows.Forms.Label lblRemark;
        public DevComponents.DotNetBar.Controls.TextBoxX txtRemark;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblProdDate;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbReason;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label lblCreateType;
        private System.Windows.Forms.ComboBox cmbCreateType;
        private System.Windows.Forms.Panel panel11;
    }
}