namespace LIMS.Views.QC07
{
    partial class RawFrm
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
			this.lbSampleName = new System.Windows.Forms.Label();
			this.txtSampleName = new System.Windows.Forms.TextBox();
			this.txtMaterialNO = new System.Windows.Forms.TextBox();
			this.lbMaterial = new System.Windows.Forms.Label();
			this.txtReceiveNum = new DevExpress.XtraEditors.SpinEdit();
			this.lbRecieveNum = new System.Windows.Forms.Label();
			this.txtGradeVersion = new DevExpress.XtraEditors.TextEdit();
			this.lblGradeVersion = new System.Windows.Forms.Label();
			this.btCancel = new System.Windows.Forms.Button();
			this.btSave = new System.Windows.Forms.Button();
			this.txtGR_NO = new DevExpress.XtraEditors.TextEdit();
			this.lblGR_NO = new System.Windows.Forms.Label();
			this.vSampleName = new System.Windows.Forms.Label();
			this.lblSheet_Date = new System.Windows.Forms.Label();
			this.lbUnit = new System.Windows.Forms.Label();
			this.txtUnit = new DevExpress.XtraEditors.TextEdit();
			this.label1 = new System.Windows.Forms.Label();
			this.txtQuantity = new DevExpress.XtraEditors.TextEdit();
			this.txtDate = new DevExpress.XtraEditors.DateEdit();
			this.txtDateofSample = new DevExpress.XtraEditors.DateEdit();
			this.lblDateofSample = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtVendor = new DevExpress.XtraEditors.TextEdit();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.txtReceiveNum.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGradeVersion.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGR_NO.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDateofSample.Properties.VistaTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDateofSample.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVendor.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// lbSampleName
			// 
			this.lbSampleName.AutoSize = true;
			this.lbSampleName.BackColor = System.Drawing.Color.Transparent;
			this.lbSampleName.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.lbSampleName.Location = new System.Drawing.Point(39, 90);
			this.lbSampleName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbSampleName.Name = "lbSampleName";
			this.lbSampleName.Size = new System.Drawing.Size(156, 27);
			this.lbSampleName.TabIndex = 294;
			this.lbSampleName.Text = "SampleName:";
			// 
			// txtSampleName
			// 
			this.txtSampleName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.txtSampleName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSampleName.Location = new System.Drawing.Point(210, 88);
			this.txtSampleName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtSampleName.Name = "txtSampleName";
			this.txtSampleName.ReadOnly = true;
			this.txtSampleName.Size = new System.Drawing.Size(254, 32);
			this.txtSampleName.TabIndex = 295;
			this.txtSampleName.Tag = "";
			// 
			// txtMaterialNO
			// 
			this.txtMaterialNO.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.txtMaterialNO.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtMaterialNO.Location = new System.Drawing.Point(210, 134);
			this.txtMaterialNO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtMaterialNO.Name = "txtMaterialNO";
			this.txtMaterialNO.ReadOnly = true;
			this.txtMaterialNO.Size = new System.Drawing.Size(254, 32);
			this.txtMaterialNO.TabIndex = 297;
			this.txtMaterialNO.Tag = "";
			// 
			// lbMaterial
			// 
			this.lbMaterial.AutoSize = true;
			this.lbMaterial.BackColor = System.Drawing.Color.Transparent;
			this.lbMaterial.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.lbMaterial.Location = new System.Drawing.Point(39, 134);
			this.lbMaterial.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbMaterial.Name = "lbMaterial";
			this.lbMaterial.Size = new System.Drawing.Size(106, 27);
			this.lbMaterial.TabIndex = 296;
			this.lbMaterial.Text = "Material:";
			// 
			// txtReceiveNum
			// 
			this.txtReceiveNum.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.txtReceiveNum.EnterMoveNextControl = true;
			this.txtReceiveNum.ImeMode = System.Windows.Forms.ImeMode.Close;
			this.txtReceiveNum.Location = new System.Drawing.Point(213, 187);
			this.txtReceiveNum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtReceiveNum.Name = "txtReceiveNum";
			this.txtReceiveNum.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtReceiveNum.Properties.Appearance.Options.UseFont = true;
			this.txtReceiveNum.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.txtReceiveNum.Properties.IsFloatValue = false;
			this.txtReceiveNum.Properties.Mask.EditMask = "N00";
			this.txtReceiveNum.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
			this.txtReceiveNum.Properties.MaxValue = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
			this.txtReceiveNum.Size = new System.Drawing.Size(256, 30);
			this.txtReceiveNum.TabIndex = 318;
			this.txtReceiveNum.Tag = "";
			// 
			// lbRecieveNum
			// 
			this.lbRecieveNum.AutoSize = true;
			this.lbRecieveNum.BackColor = System.Drawing.Color.Transparent;
			this.lbRecieveNum.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.lbRecieveNum.Location = new System.Drawing.Point(38, 189);
			this.lbRecieveNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbRecieveNum.Name = "lbRecieveNum";
			this.lbRecieveNum.Size = new System.Drawing.Size(147, 27);
			this.lbRecieveNum.TabIndex = 317;
			this.lbRecieveNum.Text = "ReceiveNum:";
			// 
			// txtGradeVersion
			// 
			this.txtGradeVersion.Location = new System.Drawing.Point(715, 282);
			this.txtGradeVersion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtGradeVersion.Name = "txtGradeVersion";
			this.txtGradeVersion.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGradeVersion.Properties.Appearance.Options.UseFont = true;
			this.txtGradeVersion.Properties.ReadOnly = true;
			this.txtGradeVersion.Size = new System.Drawing.Size(261, 30);
			this.txtGradeVersion.TabIndex = 320;
			this.txtGradeVersion.Tag = "";
			// 
			// lblGradeVersion
			// 
			this.lblGradeVersion.AutoSize = true;
			this.lblGradeVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblGradeVersion.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.lblGradeVersion.Location = new System.Drawing.Point(543, 286);
			this.lblGradeVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblGradeVersion.Name = "lblGradeVersion";
			this.lblGradeVersion.Size = new System.Drawing.Size(159, 27);
			this.lblGradeVersion.TabIndex = 319;
			this.lblGradeVersion.Text = "GradeVersion:";
			// 
			// btCancel
			// 
			this.btCancel.Location = new System.Drawing.Point(397, 408);
			this.btCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(112, 38);
			this.btCancel.TabIndex = 322;
			this.btCancel.Text = "Cancel";
			this.btCancel.UseVisualStyleBackColor = true;
			// 
			// btSave
			// 
			this.btSave.Location = new System.Drawing.Point(210, 408);
			this.btSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.btSave.Name = "btSave";
			this.btSave.Size = new System.Drawing.Size(112, 38);
			this.btSave.TabIndex = 321;
			this.btSave.Text = "Save";
			this.btSave.UseVisualStyleBackColor = true;
			// 
			// txtGR_NO
			// 
			this.txtGR_NO.Location = new System.Drawing.Point(213, 284);
			this.txtGR_NO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtGR_NO.Name = "txtGR_NO";
			this.txtGR_NO.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtGR_NO.Properties.Appearance.Options.UseFont = true;
			this.txtGR_NO.Properties.ReadOnly = true;
			this.txtGR_NO.Size = new System.Drawing.Size(261, 30);
			this.txtGR_NO.TabIndex = 324;
			this.txtGR_NO.Tag = "";
			// 
			// lblGR_NO
			// 
			this.lblGR_NO.AutoSize = true;
			this.lblGR_NO.BackColor = System.Drawing.Color.Transparent;
			this.lblGR_NO.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.lblGR_NO.Location = new System.Drawing.Point(41, 288);
			this.lblGR_NO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblGR_NO.Name = "lblGR_NO";
			this.lblGR_NO.Size = new System.Drawing.Size(90, 27);
			this.lblGR_NO.TabIndex = 323;
			this.lblGR_NO.Text = "Gr_No:";
			// 
			// vSampleName
			// 
			this.vSampleName.AutoSize = true;
			this.vSampleName.Location = new System.Drawing.Point(709, 100);
			this.vSampleName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.vSampleName.Name = "vSampleName";
			this.vSampleName.Size = new System.Drawing.Size(31, 20);
			this.vSampleName.TabIndex = 325;
			this.vSampleName.Text = "XX";
			// 
			// lblSheet_Date
			// 
			this.lblSheet_Date.AutoSize = true;
			this.lblSheet_Date.BackColor = System.Drawing.Color.Transparent;
			this.lblSheet_Date.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSheet_Date.Location = new System.Drawing.Point(538, 139);
			this.lblSheet_Date.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSheet_Date.Name = "lblSheet_Date";
			this.lblSheet_Date.Size = new System.Drawing.Size(144, 27);
			this.lblSheet_Date.TabIndex = 326;
			this.lblSheet_Date.Text = "RecieveDate:";
			// 
			// lbUnit
			// 
			this.lbUnit.AutoSize = true;
			this.lbUnit.BackColor = System.Drawing.Color.Transparent;
			this.lbUnit.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.lbUnit.Location = new System.Drawing.Point(39, 233);
			this.lbUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbUnit.Name = "lbUnit";
			this.lbUnit.Size = new System.Drawing.Size(65, 27);
			this.lbUnit.TabIndex = 323;
			this.lbUnit.Text = "Unit:";
			// 
			// txtUnit
			// 
			this.txtUnit.EnterMoveNextControl = true;
			this.txtUnit.Location = new System.Drawing.Point(210, 231);
			this.txtUnit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtUnit.Name = "txtUnit";
			this.txtUnit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUnit.Properties.Appearance.Options.UseFont = true;
			this.txtUnit.Size = new System.Drawing.Size(256, 30);
			this.txtUnit.TabIndex = 329;
			this.txtUnit.Tag = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.label1.Location = new System.Drawing.Point(41, 339);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(109, 27);
			this.label1.TabIndex = 319;
			this.label1.Text = "Quantity:";
			// 
			// txtQuantity
			// 
			this.txtQuantity.Location = new System.Drawing.Point(213, 335);
			this.txtQuantity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtQuantity.Name = "txtQuantity";
			this.txtQuantity.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtQuantity.Properties.Appearance.Options.UseFont = true;
			this.txtQuantity.Properties.ReadOnly = true;
			this.txtQuantity.Size = new System.Drawing.Size(261, 30);
			this.txtQuantity.TabIndex = 320;
			this.txtQuantity.Tag = "";
			// 
			// txtDate
			// 
			this.txtDate.EditValue = null;
			this.txtDate.Location = new System.Drawing.Point(713, 141);
			this.txtDate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtDate.Name = "txtDate";
			this.txtDate.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDate.Properties.Appearance.Options.UseFont = true;
			this.txtDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.txtDate.Properties.Mask.EditMask = "yyyy-MM-dd";
			this.txtDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.txtDate.Size = new System.Drawing.Size(256, 30);
			this.txtDate.TabIndex = 330;
			this.txtDate.Tag = "";
			// 
			// txtDateofSample
			// 
			this.txtDateofSample.EditValue = null;
			this.txtDateofSample.Location = new System.Drawing.Point(715, 186);
			this.txtDateofSample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtDateofSample.Name = "txtDateofSample";
			this.txtDateofSample.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDateofSample.Properties.Appearance.Options.UseFont = true;
			this.txtDateofSample.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.txtDateofSample.Properties.Mask.EditMask = "yyyy-MM-dd";
			this.txtDateofSample.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.txtDateofSample.Size = new System.Drawing.Size(256, 30);
			this.txtDateofSample.TabIndex = 330;
			this.txtDateofSample.Tag = "";
			// 
			// lblDateofSample
			// 
			this.lblDateofSample.AutoSize = true;
			this.lblDateofSample.BackColor = System.Drawing.Color.Transparent;
			this.lblDateofSample.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDateofSample.Location = new System.Drawing.Point(540, 184);
			this.lblDateofSample.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDateofSample.Name = "lblDateofSample";
			this.lblDateofSample.Size = new System.Drawing.Size(174, 27);
			this.lblDateofSample.TabIndex = 326;
			this.lblDateofSample.Text = "Date of Sample:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Georgia", 11.25F);
			this.label2.Location = new System.Drawing.Point(543, 235);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(93, 27);
			this.label2.TabIndex = 319;
			this.label2.Text = "Vendor:";
			// 
			// txtVendor
			// 
			this.txtVendor.Location = new System.Drawing.Point(715, 231);
			this.txtVendor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.txtVendor.Name = "txtVendor";
			this.txtVendor.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtVendor.Properties.Appearance.Options.UseFont = true;
			this.txtVendor.Properties.ReadOnly = true;
			this.txtVendor.Size = new System.Drawing.Size(261, 30);
			this.txtVendor.TabIndex = 320;
			this.txtVendor.Tag = "";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(543, 93);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(121, 27);
			this.label3.TabIndex = 326;
			this.label3.Text = "SampleID:";
			// 
			// RawFrm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1009, 492);
			this.Controls.Add(this.txtUnit);
			this.Controls.Add(this.lblDateofSample);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lblSheet_Date);
			this.Controls.Add(this.vSampleName);
			this.Controls.Add(this.txtGR_NO);
			this.Controls.Add(this.lbUnit);
			this.Controls.Add(this.lblGR_NO);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btSave);
			this.Controls.Add(this.txtVendor);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtQuantity);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtGradeVersion);
			this.Controls.Add(this.lblGradeVersion);
			this.Controls.Add(this.txtReceiveNum);
			this.Controls.Add(this.lbRecieveNum);
			this.Controls.Add(this.txtMaterialNO);
			this.Controls.Add(this.lbMaterial);
			this.Controls.Add(this.txtSampleName);
			this.Controls.Add(this.lbSampleName);
			this.Controls.Add(this.txtDateofSample);
			this.Controls.Add(this.txtDate);
			this.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
			this.MinimumSize = new System.Drawing.Size(600, 452);
			this.Name = "RawFrm";
			this.Text = "RawFrm";
			this.Controls.SetChildIndex(this.txtDate, 0);
			this.Controls.SetChildIndex(this.txtDateofSample, 0);
			this.Controls.SetChildIndex(this.lbSampleName, 0);
			this.Controls.SetChildIndex(this.txtSampleName, 0);
			this.Controls.SetChildIndex(this.lbMaterial, 0);
			this.Controls.SetChildIndex(this.txtMaterialNO, 0);
			this.Controls.SetChildIndex(this.lbRecieveNum, 0);
			this.Controls.SetChildIndex(this.txtReceiveNum, 0);
			this.Controls.SetChildIndex(this.lblGradeVersion, 0);
			this.Controls.SetChildIndex(this.txtGradeVersion, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.txtQuantity, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.txtVendor, 0);
			this.Controls.SetChildIndex(this.btSave, 0);
			this.Controls.SetChildIndex(this.btCancel, 0);
			this.Controls.SetChildIndex(this.lblGR_NO, 0);
			this.Controls.SetChildIndex(this.lbUnit, 0);
			this.Controls.SetChildIndex(this.txtGR_NO, 0);
			this.Controls.SetChildIndex(this.vSampleName, 0);
			this.Controls.SetChildIndex(this.lblSheet_Date, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.lblDateofSample, 0);
			this.Controls.SetChildIndex(this.txtUnit, 0);
			((System.ComponentModel.ISupportInitialize)(this.txtReceiveNum.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGradeVersion.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtGR_NO.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtUnit.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtQuantity.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDateofSample.Properties.VistaTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtDateofSample.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.txtVendor.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSampleName;
        private System.Windows.Forms.TextBox txtSampleName;
        private System.Windows.Forms.TextBox txtMaterialNO;
        private System.Windows.Forms.Label lbMaterial;
        private DevExpress.XtraEditors.SpinEdit txtReceiveNum;
        private System.Windows.Forms.Label lbRecieveNum;
        private DevExpress.XtraEditors.TextEdit txtGradeVersion;
        private System.Windows.Forms.Label lblGradeVersion;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private DevExpress.XtraEditors.TextEdit txtGR_NO;
        private System.Windows.Forms.Label lblGR_NO;
        private System.Windows.Forms.Label vSampleName;
        private System.Windows.Forms.Label lblSheet_Date;
        private System.Windows.Forms.Label lbUnit;
        private DevExpress.XtraEditors.TextEdit txtUnit;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TextEdit txtQuantity;
        private DevExpress.XtraEditors.DateEdit txtDate;
        private DevExpress.XtraEditors.DateEdit txtDateofSample;
        private System.Windows.Forms.Label lblDateofSample;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.TextEdit txtVendor;
		private System.Windows.Forms.Label label3;
	}
}