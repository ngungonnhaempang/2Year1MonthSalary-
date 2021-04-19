namespace LIMS.Views
{
    partial class QP01Dialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QP01Dialog));
            this.btCancel = new System.Windows.Forms.Button();
            this.btSave = new System.Windows.Forms.Button();
            this.gbFrequency = new System.Windows.Forms.GroupBox();
            this.rbOnce = new System.Windows.Forms.RadioButton();
            this.lblTo = new System.Windows.Forms.Label();
            this.EndTime = new DevExpress.XtraEditors.TimeEdit();
            this.StartTime = new DevExpress.XtraEditors.TimeEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbUnitOcccur = new System.Windows.Forms.ComboBox();
            this.rbOccurEvery = new System.Windows.Forms.RadioButton();
            this.numOccur = new DevExpress.XtraEditors.SpinEdit();
            this.gbDuration = new System.Windows.Forms.GroupBox();
            this.rbEndDate = new System.Windows.Forms.RadioButton();
            this.rbNoenddate = new System.Windows.Forms.RadioButton();
            this.DurationEnd = new DevExpress.XtraEditors.DateEdit();
            this.DurationStart = new DevExpress.XtraEditors.DateEdit();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbOccurs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblLOT_NO = new System.Windows.Forms.Label();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbReason = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Workspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.ckbEnable = new System.Windows.Forms.CheckBox();
            this.clbProperty = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.cmbSampleName = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbLine = new System.Windows.Forms.ComboBox();
            this.gbFrequency.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOccur.Properties)).BeginInit();
            this.gbDuration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DurationEnd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationEnd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationStart.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationStart.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbProperty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMaterial.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.btCancel.Location = new System.Drawing.Point(543, 512);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(93, 27);
            this.btCancel.TabIndex = 12;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btSave
            // 
            this.btSave.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.btSave.Location = new System.Drawing.Point(431, 512);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(95, 27);
            this.btSave.TabIndex = 11;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            // 
            // gbFrequency
            // 
            this.gbFrequency.Controls.Add(this.rbOnce);
            this.gbFrequency.Controls.Add(this.lblTo);
            this.gbFrequency.Controls.Add(this.EndTime);
            this.gbFrequency.Controls.Add(this.StartTime);
            this.gbFrequency.Controls.Add(this.label3);
            this.gbFrequency.Controls.Add(this.cmbUnitOcccur);
            this.gbFrequency.Controls.Add(this.rbOccurEvery);
            this.gbFrequency.Controls.Add(this.numOccur);
            this.gbFrequency.Location = new System.Drawing.Point(48, 315);
            this.gbFrequency.Name = "gbFrequency";
            this.gbFrequency.Size = new System.Drawing.Size(652, 92);
            this.gbFrequency.TabIndex = 426;
            this.gbFrequency.TabStop = false;
            this.gbFrequency.Text = "Daily Frequency";
            // 
            // rbOnce
            // 
            this.rbOnce.AutoSize = true;
            this.rbOnce.Location = new System.Drawing.Point(12, 26);
            this.rbOnce.Name = "rbOnce";
            this.rbOnce.Size = new System.Drawing.Size(89, 16);
            this.rbOnce.TabIndex = 466;
            this.rbOnce.Text = "Once Occurs";
            this.rbOnce.UseVisualStyleBackColor = true;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Font = new System.Drawing.Font("宋体", 9F);
            this.lblTo.Location = new System.Drawing.Point(374, 61);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(23, 12);
            this.lblTo.TabIndex = 464;
            this.lblTo.Text = "To:";
            // 
            // EndTime
            // 
            this.EndTime.EditValue = new System.DateTime(2018, 1, 17, 23, 59, 59, 0);
            this.EndTime.Location = new System.Drawing.Point(414, 57);
            this.EndTime.Name = "EndTime";
            this.EndTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.EndTime.Properties.EditFormat.FormatString = "HH:mm:ss";
            this.EndTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.EndTime.Properties.Mask.EditMask = "HH:mm:ss";
            this.EndTime.Size = new System.Drawing.Size(101, 20);
            this.EndTime.TabIndex = 463;
            // 
            // StartTime
            // 
            this.StartTime.EditValue = new System.DateTime(2018, 1, 17, 0, 0, 0, 0);
            this.StartTime.Location = new System.Drawing.Point(414, 24);
            this.StartTime.Name = "StartTime";
            this.StartTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.StartTime.Properties.EditFormat.FormatString = "HH:mm:ss";
            this.StartTime.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.StartTime.Properties.Mask.EditMask = "HH:mm:ss";
            this.StartTime.Size = new System.Drawing.Size(101, 20);
            this.StartTime.TabIndex = 463;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 9F);
            this.label3.Location = new System.Drawing.Point(324, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 433;
            this.label3.Text = "Starting At:";
            // 
            // cmbUnitOcccur
            // 
            this.cmbUnitOcccur.BackColor = System.Drawing.Color.White;
            this.cmbUnitOcccur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitOcccur.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnitOcccur.FormattingEnabled = true;
            this.cmbUnitOcccur.Items.AddRange(new object[] {
            "Hour",
            "Minute",
            "Second"});
            this.cmbUnitOcccur.Location = new System.Drawing.Point(192, 55);
            this.cmbUnitOcccur.Name = "cmbUnitOcccur";
            this.cmbUnitOcccur.Size = new System.Drawing.Size(80, 25);
            this.cmbUnitOcccur.TabIndex = 458;
            // 
            // rbOccurEvery
            // 
            this.rbOccurEvery.AutoSize = true;
            this.rbOccurEvery.Checked = true;
            this.rbOccurEvery.Location = new System.Drawing.Point(12, 59);
            this.rbOccurEvery.Name = "rbOccurEvery";
            this.rbOccurEvery.Size = new System.Drawing.Size(101, 16);
            this.rbOccurEvery.TabIndex = 419;
            this.rbOccurEvery.TabStop = true;
            this.rbOccurEvery.Text = "Occurs Every:";
            this.rbOccurEvery.UseVisualStyleBackColor = true;
            // 
            // numOccur
            // 
            this.numOccur.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOccur.Location = new System.Drawing.Point(132, 55);
            this.numOccur.Name = "numOccur";
            this.numOccur.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numOccur.Properties.Appearance.Options.UseFont = true;
            this.numOccur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numOccur.Properties.Mask.EditMask = "n0";
            this.numOccur.Properties.MaxValue = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numOccur.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOccur.Size = new System.Drawing.Size(54, 24);
            this.numOccur.TabIndex = 417;
            // 
            // gbDuration
            // 
            this.gbDuration.Controls.Add(this.rbEndDate);
            this.gbDuration.Controls.Add(this.rbNoenddate);
            this.gbDuration.Controls.Add(this.DurationEnd);
            this.gbDuration.Controls.Add(this.DurationStart);
            this.gbDuration.Controls.Add(this.label8);
            this.gbDuration.Location = new System.Drawing.Point(48, 413);
            this.gbDuration.Name = "gbDuration";
            this.gbDuration.Size = new System.Drawing.Size(652, 82);
            this.gbDuration.TabIndex = 431;
            this.gbDuration.TabStop = false;
            this.gbDuration.Text = "Duration";
            // 
            // rbEndDate
            // 
            this.rbEndDate.AutoSize = true;
            this.rbEndDate.Checked = true;
            this.rbEndDate.Location = new System.Drawing.Point(326, 26);
            this.rbEndDate.Name = "rbEndDate";
            this.rbEndDate.Size = new System.Drawing.Size(71, 16);
            this.rbEndDate.TabIndex = 432;
            this.rbEndDate.TabStop = true;
            this.rbEndDate.Text = "End Date";
            this.rbEndDate.UseVisualStyleBackColor = true;
            // 
            // rbNoenddate
            // 
            this.rbNoenddate.AutoSize = true;
            this.rbNoenddate.Location = new System.Drawing.Point(326, 49);
            this.rbNoenddate.Name = "rbNoenddate";
            this.rbNoenddate.Size = new System.Drawing.Size(89, 16);
            this.rbNoenddate.TabIndex = 431;
            this.rbNoenddate.Text = "No End Date";
            this.rbNoenddate.UseVisualStyleBackColor = true;
            // 
            // DurationEnd
            // 
            this.DurationEnd.EditValue = new System.DateTime(9999, 1, 1, 0, 0, 0, 0);
            this.DurationEnd.Location = new System.Drawing.Point(414, 24);
            this.DurationEnd.Name = "DurationEnd";
            this.DurationEnd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DurationEnd.Properties.DisplayFormat.FormatString = "dd-MM-yyyy";
            this.DurationEnd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DurationEnd.Properties.EditFormat.FormatString = "dd-MM-yyyy";
            this.DurationEnd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DurationEnd.Properties.Mask.EditMask = "dd-MM-yyyy";
            this.DurationEnd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DurationEnd.Size = new System.Drawing.Size(101, 20);
            this.DurationEnd.TabIndex = 430;
            // 
            // DurationStart
            // 
            this.DurationStart.EditValue = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
            this.DurationStart.Location = new System.Drawing.Point(132, 24);
            this.DurationStart.Name = "DurationStart";
            this.DurationStart.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DurationStart.Properties.DisplayFormat.FormatString = "dd-MM-yyyy";
            this.DurationStart.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DurationStart.Properties.EditFormat.FormatString = "dd-MM-yyyy";
            this.DurationStart.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DurationStart.Properties.Mask.EditMask = "dd-MM-yyyy";
            this.DurationStart.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DurationStart.Size = new System.Drawing.Size(101, 20);
            this.DurationStart.TabIndex = 429;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("宋体", 9F);
            this.label8.Location = new System.Drawing.Point(10, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 12);
            this.label8.TabIndex = 427;
            this.label8.Text = "Start Date:";
            // 
            // cmbOccurs
            // 
            this.cmbOccurs.BackColor = System.Drawing.Color.White;
            this.cmbOccurs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOccurs.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOccurs.FormattingEnabled = true;
            this.cmbOccurs.Location = new System.Drawing.Point(229, 180);
            this.cmbOccurs.Name = "cmbOccurs";
            this.cmbOccurs.Size = new System.Drawing.Size(169, 25);
            this.cmbOccurs.TabIndex = 457;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label2.Location = new System.Drawing.Point(45, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 18);
            this.label2.TabIndex = 455;
            this.label2.Text = "Occurs:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(48, 202);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 1);
            this.panel3.TabIndex = 456;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.BackColor = System.Drawing.Color.Transparent;
            this.lblLine.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.lblLine.Location = new System.Drawing.Point(44, 126);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(42, 18);
            this.lblLine.TabIndex = 448;
            this.lblLine.Text = "Line:";
            // 
            // lblLOT_NO
            // 
            this.lblLOT_NO.AutoSize = true;
            this.lblLOT_NO.BackColor = System.Drawing.Color.Transparent;
            this.lblLOT_NO.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.lblLOT_NO.Location = new System.Drawing.Point(44, 101);
            this.lblLOT_NO.Name = "lblLOT_NO";
            this.lblLOT_NO.Size = new System.Drawing.Size(77, 18);
            this.lblLOT_NO.TabIndex = 449;
            this.lblLOT_NO.Text = "LOT_NO:";
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.lblSampleName.Location = new System.Drawing.Point(44, 74);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(107, 18);
            this.lblSampleName.TabIndex = 447;
            this.lblSampleName.Text = "Sample Name:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 445;
            this.pictureBox1.TabStop = false;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReason.Location = new System.Drawing.Point(54, 40);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(230, 18);
            this.lbReason.TabIndex = 446;
            this.lbReason.Text = "Please enter the information:";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.White;
            this.panel12.Location = new System.Drawing.Point(47, 117);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(200, 1);
            this.panel12.TabIndex = 450;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(47, 143);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 1);
            this.panel1.TabIndex = 452;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(47, 90);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 1);
            this.panel5.TabIndex = 451;
            // 
            // Workspace
            // 
            this.Workspace.Location = new System.Drawing.Point(48, 217);
            this.Workspace.Name = "Workspace";
            this.Workspace.Size = new System.Drawing.Size(652, 92);
            this.Workspace.TabIndex = 458;
            this.Workspace.Text = "deckWorkspace1";
            // 
            // ckbEnable
            // 
            this.ckbEnable.AutoSize = true;
            this.ckbEnable.Checked = true;
            this.ckbEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEnable.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.ckbEnable.Location = new System.Drawing.Point(48, 159);
            this.ckbEnable.Name = "ckbEnable";
            this.ckbEnable.Size = new System.Drawing.Size(73, 22);
            this.ckbEnable.TabIndex = 459;
            this.ckbEnable.Text = "Enable";
            this.ckbEnable.UseVisualStyleBackColor = true;
            // 
            // clbProperty
            // 
            this.clbProperty.CheckOnClick = true;
            this.clbProperty.HorizontalScrollbar = true;
            this.clbProperty.Location = new System.Drawing.Point(438, 70);
            this.clbProperty.MultiColumn = true;
            this.clbProperty.Name = "clbProperty";
            this.clbProperty.Size = new System.Drawing.Size(262, 132);
            this.clbProperty.TabIndex = 461;
            // 
            // cmbSampleName
            // 
            this.cmbSampleName.Location = new System.Drawing.Point(229, 67);
            this.cmbSampleName.Name = "cmbSampleName";
            this.cmbSampleName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSampleName.Properties.Appearance.Options.UseFont = true;
            this.cmbSampleName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSampleName.Properties.NullText = "";
            this.cmbSampleName.Properties.PopupWidth = 450;
            this.cmbSampleName.Properties.ShowFooter = false;
            this.cmbSampleName.Size = new System.Drawing.Size(169, 24);
            this.cmbSampleName.TabIndex = 464;
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.Location = new System.Drawing.Point(229, 94);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbMaterial.Properties.Appearance.Options.UseFont = true;
            this.cmbMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMaterial.Properties.NullText = "";
            this.cmbMaterial.Properties.PopupWidth = 450;
            this.cmbMaterial.Properties.ShowFooter = false;
            this.cmbMaterial.Size = new System.Drawing.Size(169, 24);
            this.cmbMaterial.TabIndex = 463;
            // 
            // cmbLine
            // 
            this.cmbLine.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbLine.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbLine.BackColor = System.Drawing.Color.White;
            this.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLine.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLine.FormattingEnabled = true;
            this.cmbLine.Location = new System.Drawing.Point(229, 121);
            this.cmbLine.Name = "cmbLine";
            this.cmbLine.Size = new System.Drawing.Size(169, 25);
            this.cmbLine.TabIndex = 462;
            // 
            // QP01Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.ClientSize = new System.Drawing.Size(731, 545);
            this.Controls.Add(this.cmbSampleName);
            this.Controls.Add(this.cmbMaterial);
            this.Controls.Add(this.cmbLine);
            this.Controls.Add(this.clbProperty);
            this.Controls.Add(this.ckbEnable);
            this.Controls.Add(this.Workspace);
            this.Controls.Add(this.cmbOccurs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lblLine);
            this.Controls.Add(this.lblLOT_NO);
            this.Controls.Add(this.lblSampleName);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbReason);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.gbDuration);
            this.Controls.Add(this.gbFrequency);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btSave);
            this.MinimumSize = new System.Drawing.Size(400, 166);
            this.Name = "QP01Dialog";
            this.Text = "FEPV IT";
            this.Controls.SetChildIndex(this.btSave, 0);
            this.Controls.SetChildIndex(this.btCancel, 0);
            this.Controls.SetChildIndex(this.gbFrequency, 0);
            this.Controls.SetChildIndex(this.gbDuration, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel12, 0);
            this.Controls.SetChildIndex(this.lbReason, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblSampleName, 0);
            this.Controls.SetChildIndex(this.lblLOT_NO, 0);
            this.Controls.SetChildIndex(this.lblLine, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.cmbOccurs, 0);
            this.Controls.SetChildIndex(this.Workspace, 0);
            this.Controls.SetChildIndex(this.ckbEnable, 0);
            this.Controls.SetChildIndex(this.clbProperty, 0);
            this.Controls.SetChildIndex(this.cmbLine, 0);
            this.Controls.SetChildIndex(this.cmbMaterial, 0);
            this.Controls.SetChildIndex(this.cmbSampleName, 0);
            this.gbFrequency.ResumeLayout(false);
            this.gbFrequency.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOccur.Properties)).EndInit();
            this.gbDuration.ResumeLayout(false);
            this.gbDuration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DurationEnd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationEnd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationStart.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DurationStart.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clbProperty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMaterial.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.GroupBox gbFrequency;
        private System.Windows.Forms.RadioButton rbOccurEvery;
        public DevExpress.XtraEditors.SpinEdit numOccur;
        private System.Windows.Forms.GroupBox gbDuration;
        private System.Windows.Forms.RadioButton rbNoenddate;
        private DevExpress.XtraEditors.DateEdit DurationEnd;
        private DevExpress.XtraEditors.DateEdit DurationStart;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox cmbOccurs;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblLOT_NO;
        private System.Windows.Forms.Label lblSampleName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbReason;
        public System.Windows.Forms.Panel panel12;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel5;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace Workspace;
        private System.Windows.Forms.RadioButton rbEndDate;
        private System.Windows.Forms.CheckBox ckbEnable;
        public System.Windows.Forms.ComboBox cmbUnitOcccur;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.CheckedListBoxControl clbProperty;
        private DevExpress.XtraEditors.TimeEdit EndTime;
        private DevExpress.XtraEditors.TimeEdit StartTime;
        public DevExpress.XtraEditors.LookUpEdit cmbSampleName;
        public DevExpress.XtraEditors.LookUpEdit cmbMaterial;
        private System.Windows.Forms.ComboBox cmbLine;
        private System.Windows.Forms.RadioButton rbOnce;
        private System.Windows.Forms.Label lblTo;
    }
}