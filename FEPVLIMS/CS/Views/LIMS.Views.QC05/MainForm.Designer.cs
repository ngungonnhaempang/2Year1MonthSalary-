namespace LIMS.Views.QC05
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearchSure = new DevComponents.DotNetBar.ButtonItem();
            this.btItemSubmit = new DevComponents.DotNetBar.ButtonItem();
            this.btSearchback = new DevComponents.DotNetBar.ButtonItem();
            this.btItemRelease = new DevComponents.DotNetBar.ButtonItem();
            this.btVouRelease = new DevComponents.DotNetBar.ButtonItem();
            this.btVouSubmit = new DevComponents.DotNetBar.ButtonItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtMsg = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.cmbGrade = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSysGrade = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.cbEndDate = new DevExpress.XtraEditors.DateEdit();
            this.cbFromDate = new DevExpress.XtraEditors.DateEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMaterialNo = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.lblVoucherID = new System.Windows.Forms.Label();
            this.txtSampleID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSampleName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pMaterial = new System.Windows.Forms.Panel();
            this.lblSampleID = new System.Windows.Forms.Label();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.txtLotNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtVoucherID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this._ItemsList = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._DOCList = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this._Parameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEndDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFromDate.Properties)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this._ItemsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this._DOCList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearchSure,
            this.btItemSubmit,
            this.btSearchback,
            this.btItemRelease,
            this.btVouRelease,
            this.btVouSubmit});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(907, 25);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 20;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearchSure
            // 
            this.btSearchSure.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchSure.Image = ((System.Drawing.Image)(resources.GetObject("btSearchSure.Image")));
            this.btSearchSure.ImagePaddingHorizontal = 8;
            this.btSearchSure.Name = "btSearchSure";
            this.btSearchSure.Text = "查询审核单据";
            // 
            // btItemSubmit
            // 
            this.btItemSubmit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btItemSubmit.Image = ((System.Drawing.Image)(resources.GetObject("btItemSubmit.Image")));
            this.btItemSubmit.ImagePaddingHorizontal = 8;
            this.btItemSubmit.Name = "btItemSubmit";
            this.btItemSubmit.Text = "部分确认";
            // 
            // btSearchback
            // 
            this.btSearchback.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchback.Image = ((System.Drawing.Image)(resources.GetObject("btSearchback.Image")));
            this.btSearchback.ImagePaddingHorizontal = 8;
            this.btSearchback.Name = "btSearchback";
            this.btSearchback.Text = "查询释放单据";
            // 
            // btItemRelease
            // 
            this.btItemRelease.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btItemRelease.Image = ((System.Drawing.Image)(resources.GetObject("btItemRelease.Image")));
            this.btItemRelease.ImagePaddingHorizontal = 8;
            this.btItemRelease.Name = "btItemRelease";
            this.btItemRelease.Text = "取消确认";
            // 
            // btVouRelease
            // 
            this.btVouRelease.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btVouRelease.Image = ((System.Drawing.Image)(resources.GetObject("btVouRelease.Image")));
            this.btVouRelease.ImagePaddingHorizontal = 8;
            this.btVouRelease.Name = "btVouRelease";
            this.btVouRelease.Text = "单据释放";
            // 
            // btVouSubmit
            // 
            this.btVouSubmit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btVouSubmit.Image = ((System.Drawing.Image)(resources.GetObject("btVouSubmit.Image")));
            this.btVouSubmit.ImagePaddingHorizontal = 8;
            this.btVouSubmit.Name = "btVouSubmit";
            this.btVouSubmit.Text = "单据确认";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMsg, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 495);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // txtMsg
            // 
            this.txtMsg.AutoSize = true;
            this.txtMsg.Location = new System.Drawing.Point(3, 477);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(17, 12);
            this.txtMsg.TabIndex = 22;
            this.txtMsg.Text = ":)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this._Parameter, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 33);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.79819F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.20181F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(907, 441);
            this.tableLayoutPanel2.TabIndex = 23;
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.txtRemark);
            this._Parameter.Controls.Add(this.cmbGrade);
            this._Parameter.Controls.Add(this.label7);
            this._Parameter.Controls.Add(this.txtSysGrade);
            this._Parameter.Controls.Add(this.label6);
            this._Parameter.Controls.Add(this.panel7);
            this._Parameter.Controls.Add(this.cbEndDate);
            this._Parameter.Controls.Add(this.cbFromDate);
            this._Parameter.Controls.Add(this.panel1);
            this._Parameter.Controls.Add(this.panel4);
            this._Parameter.Controls.Add(this.panel3);
            this._Parameter.Controls.Add(this.lblMaterialNo);
            this._Parameter.Controls.Add(this.lblToDate);
            this._Parameter.Controls.Add(this.lblFromDate);
            this._Parameter.Controls.Add(this.lblVoucherID);
            this._Parameter.Controls.Add(this.txtSampleID);
            this._Parameter.Controls.Add(this.txtSampleName);
            this._Parameter.Controls.Add(this.panel5);
            this._Parameter.Controls.Add(this.panel2);
            this._Parameter.Controls.Add(this.pMaterial);
            this._Parameter.Controls.Add(this.lblSampleID);
            this._Parameter.Controls.Add(this.lblSampleName);
            this._Parameter.Controls.Add(this.txtLotNo);
            this._Parameter.Controls.Add(this.txtVoucherID);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(3, 3);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(901, 121);
            // 
            // 
            // 
            this._Parameter.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._Parameter.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._Parameter.Style.BackColorGradientAngle = 90;
            this._Parameter.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Parameter.Style.BorderBottomWidth = 1;
            this._Parameter.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._Parameter.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Parameter.Style.BorderLeftWidth = 1;
            this._Parameter.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Parameter.Style.BorderRightWidth = 1;
            this._Parameter.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Parameter.Style.BorderTopWidth = 1;
            this._Parameter.Style.CornerDiameter = 4;
            this._Parameter.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._Parameter.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this._Parameter.TabIndex = 201;
            this._Parameter.Text = "[Parameter]";
            this._Parameter.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Parameter.TitleImage")));
            // 
            // txtRemark
            // 
            this.txtRemark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRemark.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRemark.Location = new System.Drawing.Point(533, 39);
            this.txtRemark.MaxLength = 500;
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(200, 47);
            this.txtRemark.TabIndex = 387;
            // 
            // cmbGrade
            // 
            this.cmbGrade.BackColor = System.Drawing.Color.White;
            this.cmbGrade.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbGrade.ForeColor = System.Drawing.Color.Black;
            this.cmbGrade.FormattingEnabled = true;
            this.cmbGrade.Location = new System.Drawing.Point(597, 8);
            this.cmbGrade.Name = "cmbGrade";
            this.cmbGrade.Size = new System.Drawing.Size(75, 25);
            this.cmbGrade.TabIndex = 386;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label7.Location = new System.Drawing.Point(529, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 20);
            this.label7.TabIndex = 385;
            this.label7.Text = "手动判等";
            // 
            // txtSysGrade
            // 
            this.txtSysGrade.BackColor = System.Drawing.Color.Gainsboro;
            // 
            // 
            // 
            this.txtSysGrade.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSysGrade.Border.Class = "RibbonGalleryContainer";
            this.txtSysGrade.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSysGrade.Border.PaddingTop = 2;
            this.txtSysGrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSysGrade.Location = new System.Drawing.Point(755, 10);
            this.txtSysGrade.Name = "txtSysGrade";
            this.txtSysGrade.ReadOnly = true;
            this.txtSysGrade.Size = new System.Drawing.Size(75, 23);
            this.txtSysGrade.TabIndex = 384;
            this.txtSysGrade.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSysGrade.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(688, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 20);
            this.label6.TabIndex = 383;
            this.label6.Text = "等级";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.Location = new System.Drawing.Point(517, 7);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1, 80);
            this.panel7.TabIndex = 382;
            // 
            // cbEndDate
            // 
            this.cbEndDate.EditValue = new System.DateTime(2018, 8, 23, 0, 0, 0, 0);
            this.cbEndDate.Location = new System.Drawing.Point(387, 12);
            this.cbEndDate.Name = "cbEndDate";
            this.cbEndDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbEndDate.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cbEndDate.Properties.Appearance.Options.UseBackColor = true;
            this.cbEndDate.Properties.Appearance.Options.UseFont = true;
            this.cbEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbEndDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.cbEndDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cbEndDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.cbEndDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cbEndDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.cbEndDate.Properties.VistaTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cbEndDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cbEndDate.Size = new System.Drawing.Size(125, 24);
            this.cbEndDate.TabIndex = 355;
            // 
            // cbFromDate
            // 
            this.cbFromDate.EditValue = new System.DateTime(2018, 7, 25, 0, 0, 0, 0);
            this.cbFromDate.Location = new System.Drawing.Point(136, 12);
            this.cbFromDate.Name = "cbFromDate";
            this.cbFromDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cbFromDate.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cbFromDate.Properties.Appearance.Options.UseBackColor = true;
            this.cbFromDate.Properties.Appearance.Options.UseFont = true;
            this.cbFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbFromDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.cbFromDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cbFromDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.cbFromDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.cbFromDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.cbFromDate.Properties.VistaTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cbFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.cbFromDate.Size = new System.Drawing.Size(125, 24);
            this.cbFromDate.TabIndex = 355;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(29, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 1);
            this.panel1.TabIndex = 379;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Location = new System.Drawing.Point(29, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(120, 1);
            this.panel4.TabIndex = 380;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Location = new System.Drawing.Point(29, 58);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(120, 1);
            this.panel3.TabIndex = 380;
            // 
            // lblMaterialNo
            // 
            this.lblMaterialNo.AutoSize = true;
            this.lblMaterialNo.BackColor = System.Drawing.Color.Transparent;
            this.lblMaterialNo.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMaterialNo.Location = new System.Drawing.Point(26, 66);
            this.lblMaterialNo.Name = "lblMaterialNo";
            this.lblMaterialNo.Size = new System.Drawing.Size(97, 18);
            this.lblMaterialNo.TabIndex = 377;
            this.lblMaterialNo.Text = "Material No:";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.BackColor = System.Drawing.Color.Transparent;
            this.lblToDate.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblToDate.Location = new System.Drawing.Point(270, 13);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(69, 18);
            this.lblToDate.TabIndex = 378;
            this.lblToDate.Text = "To Date:";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.BackColor = System.Drawing.Color.Transparent;
            this.lblFromDate.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblFromDate.Location = new System.Drawing.Point(29, 14);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(89, 18);
            this.lblFromDate.TabIndex = 378;
            this.lblFromDate.Text = "From Date:";
            // 
            // lblVoucherID
            // 
            this.lblVoucherID.AutoSize = true;
            this.lblVoucherID.BackColor = System.Drawing.Color.Transparent;
            this.lblVoucherID.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblVoucherID.Location = new System.Drawing.Point(26, 41);
            this.lblVoucherID.Name = "lblVoucherID";
            this.lblVoucherID.Size = new System.Drawing.Size(94, 18);
            this.lblVoucherID.TabIndex = 378;
            this.lblVoucherID.Text = "Voucher ID:";
            // 
            // txtSampleID
            // 
            this.txtSampleID.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtSampleID.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtSampleID.Border.Class = "RibbonGalleryContainer";
            this.txtSampleID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtSampleID.Border.PaddingTop = 2;
            this.txtSampleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtSampleID.Location = new System.Drawing.Point(387, 40);
            this.txtSampleID.Name = "txtSampleID";
            this.txtSampleID.Size = new System.Drawing.Size(125, 21);
            this.txtSampleID.TabIndex = 367;
            this.txtSampleID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
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
            this.txtSampleName.Location = new System.Drawing.Point(387, 65);
            this.txtSampleName.Name = "txtSampleName";
            this.txtSampleName.Size = new System.Drawing.Size(125, 21);
            this.txtSampleName.TabIndex = 364;
            this.txtSampleName.WatermarkColor = System.Drawing.Color.Silver;
            this.txtSampleName.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Location = new System.Drawing.Point(273, 35);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(120, 1);
            this.panel5.TabIndex = 376;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(273, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 1);
            this.panel2.TabIndex = 376;
            // 
            // pMaterial
            // 
            this.pMaterial.BackColor = System.Drawing.SystemColors.Window;
            this.pMaterial.Location = new System.Drawing.Point(273, 85);
            this.pMaterial.Name = "pMaterial";
            this.pMaterial.Size = new System.Drawing.Size(120, 1);
            this.pMaterial.TabIndex = 376;
            // 
            // lblSampleID
            // 
            this.lblSampleID.AutoSize = true;
            this.lblSampleID.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleID.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleID.Location = new System.Drawing.Point(270, 41);
            this.lblSampleID.Name = "lblSampleID";
            this.lblSampleID.Size = new System.Drawing.Size(87, 18);
            this.lblSampleID.TabIndex = 375;
            this.lblSampleID.Text = "Sample ID:";
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleName.Location = new System.Drawing.Point(270, 67);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(111, 18);
            this.lblSampleName.TabIndex = 375;
            this.lblSampleName.Text = "Sample Name:";
            // 
            // txtLotNo
            // 
            this.txtLotNo.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtLotNo.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtLotNo.Border.Class = "RibbonGalleryContainer";
            this.txtLotNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtLotNo.Border.PaddingTop = 2;
            this.txtLotNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtLotNo.Location = new System.Drawing.Point(136, 66);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Size = new System.Drawing.Size(125, 21);
            this.txtLotNo.TabIndex = 361;
            this.txtLotNo.WatermarkColor = System.Drawing.Color.Silver;
            this.txtLotNo.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // txtVoucherID
            // 
            this.txtVoucherID.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtVoucherID.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtVoucherID.Border.Class = "RibbonGalleryContainer";
            this.txtVoucherID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtVoucherID.Border.PaddingTop = 2;
            this.txtVoucherID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtVoucherID.Location = new System.Drawing.Point(136, 40);
            this.txtVoucherID.Name = "txtVoucherID";
            this.txtVoucherID.Size = new System.Drawing.Size(125, 21);
            this.txtVoucherID.TabIndex = 358;
            this.txtVoucherID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtVoucherID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.32804F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.67196F));
            this.tableLayoutPanel3.Controls.Add(this._ItemsList, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this._DOCList, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 130);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(901, 308);
            this.tableLayoutPanel3.TabIndex = 202;
            // 
            // _ItemsList
            // 
            this._ItemsList.BackColor = System.Drawing.Color.Transparent;
            this._ItemsList.CanvasColor = System.Drawing.SystemColors.Control;
            this._ItemsList.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._ItemsList.Controls.Add(this.gridControl2);
            this._ItemsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ItemsList.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._ItemsList.Location = new System.Drawing.Point(384, 3);
            this._ItemsList.Name = "_ItemsList";
            this._ItemsList.Size = new System.Drawing.Size(514, 302);
            // 
            // 
            // 
            this._ItemsList.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._ItemsList.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._ItemsList.Style.BackColorGradientAngle = 90;
            this._ItemsList.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ItemsList.Style.BorderBottomWidth = 1;
            this._ItemsList.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._ItemsList.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ItemsList.Style.BorderLeftWidth = 1;
            this._ItemsList.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ItemsList.Style.BorderRightWidth = 1;
            this._ItemsList.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ItemsList.Style.BorderTopWidth = 1;
            this._ItemsList.Style.CornerDiameter = 4;
            this._ItemsList.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._ItemsList.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this._ItemsList.TabIndex = 207;
            this._ItemsList.Text = "[Items List]";
            this._ItemsList.TitleImage = ((System.Drawing.Image)(resources.GetObject("_ItemsList.TitleImage")));
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl2.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl2.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl2.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl2.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(508, 280);
            this.gridControl2.TabIndex = 186;
            this.gridControl2.UseEmbeddedNavigator = true;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.FixedLineWidth = 1;
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.GroupFormat = " [#image]{1} {2}";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsSelection.MultiSelect = true;
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // _DOCList
            // 
            this._DOCList.BackColor = System.Drawing.Color.Transparent;
            this._DOCList.CanvasColor = System.Drawing.SystemColors.Control;
            this._DOCList.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._DOCList.Controls.Add(this.gridControl1);
            this._DOCList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._DOCList.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._DOCList.Location = new System.Drawing.Point(3, 3);
            this._DOCList.Name = "_DOCList";
            this._DOCList.Size = new System.Drawing.Size(375, 302);
            // 
            // 
            // 
            this._DOCList.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._DOCList.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._DOCList.Style.BackColorGradientAngle = 90;
            this._DOCList.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._DOCList.Style.BorderBottomWidth = 1;
            this._DOCList.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._DOCList.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._DOCList.Style.BorderLeftWidth = 1;
            this._DOCList.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._DOCList.Style.BorderRightWidth = 1;
            this._DOCList.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._DOCList.Style.BorderTopWidth = 1;
            this._DOCList.Style.CornerDiameter = 4;
            this._DOCList.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._DOCList.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this._DOCList.TabIndex = 206;
            this._DOCList.Text = "[DOC List]";
            this._DOCList.TitleImage = ((System.Drawing.Image)(resources.GetObject("_DOCList.TitleImage")));
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(369, 280);
            this.gridControl1.TabIndex = 188;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.gridView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.SeaShell;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupFormat = " [#image]{1} {2}";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "POLY";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "SSP";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "POY";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "DTY";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 495);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainForm";
            this.Text = "MainForm结果审核";
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEndDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbFromDate.Properties)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this._ItemsList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this._DOCList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btSearchSure;
        private DevComponents.DotNetBar.ButtonItem btSearchback;
        private DevComponents.DotNetBar.ButtonItem btVouRelease;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        private DevExpress.XtraEditors.DateEdit cbFromDate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblMaterialNo;
        private System.Windows.Forms.Label lblVoucherID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pMaterial;
        private System.Windows.Forms.Label lblSampleID;
        private System.Windows.Forms.Label lblSampleName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLotNo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVoucherID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private DevComponents.DotNetBar.Controls.GroupPanel _DOCList;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevComponents.DotNetBar.Controls.GroupPanel _ItemsList;
        public DevExpress.XtraGrid.GridControl gridControl2;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevComponents.DotNetBar.ButtonItem btItemRelease;
        private DevComponents.DotNetBar.ButtonItem btVouSubmit;
        private DevComponents.DotNetBar.ButtonItem btItemSubmit;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.ComboBox cmbGrade;
        private System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSysGrade;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel7;
        private DevExpress.XtraEditors.DateEdit cbEndDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Panel panel5;


    }
}