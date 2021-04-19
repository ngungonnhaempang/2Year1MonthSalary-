namespace LIMS.Views
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
            this.ribbonClientPanel2 = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearchSure = new DevComponents.DotNetBar.ButtonItem();
            this.btVouSubmit = new DevComponents.DotNetBar.ButtonItem();
            this.btVouRelease = new DevComponents.DotNetBar.ButtonItem();
            this._ParameterPanel = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbSampleName = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbTypeName = new System.Windows.Forms.ComboBox();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.txtVoucherID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ribbonClientPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this._ParameterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            this.panel6.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonClientPanel2
            // 
            this.ribbonClientPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.ribbonClientPanel2.Controls.Add(this.tableLayoutPanel1);
            this.ribbonClientPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonClientPanel2.Location = new System.Drawing.Point(0, 0);
            this.ribbonClientPanel2.Name = "ribbonClientPanel2";
            this.ribbonClientPanel2.ShowFocusRectangle = true;
            this.ribbonClientPanel2.Size = new System.Drawing.Size(805, 440);
            // 
            // 
            // 
            this.ribbonClientPanel2.Style.BackColor = System.Drawing.Color.Transparent;
            this.ribbonClientPanel2.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.ribbonClientPanel2.Style.BackColorGradientAngle = 90;
            this.ribbonClientPanel2.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Tile;
            this.ribbonClientPanel2.Style.BorderBottomWidth = 1;
            this.ribbonClientPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.ribbonClientPanel2.Style.BorderLeftWidth = 1;
            this.ribbonClientPanel2.Style.BorderRightWidth = 1;
            this.ribbonClientPanel2.Style.BorderTopWidth = 1;
            this.ribbonClientPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ribbonClientPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.ribbonClientPanel2.TabIndex = 22;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this._ParameterPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel6, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 145F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(805, 440);
            this.tableLayoutPanel1.TabIndex = 16;
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearchSure,
            this.btVouSubmit,
            this.btVouRelease});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(799, 26);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 15;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearchSure
            // 
            this.btSearchSure.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchSure.Image = global::LIMS.Views.Properties.Resources.Modify;
            this.btSearchSure.ImagePaddingHorizontal = 8;
            this.btSearchSure.Name = "btSearchSure";
            this.btSearchSure.Text = "查询单据";
            // 
            // btVouSubmit
            // 
            this.btVouSubmit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btVouSubmit.Image = global::LIMS.Views.Properties.Resources.MenuBar_Copy;
            this.btVouSubmit.ImagePaddingHorizontal = 8;
            this.btVouSubmit.Name = "btVouSubmit";
            this.btVouSubmit.Text = "单据区隔";
            // 
            // btVouRelease
            // 
            this.btVouRelease.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btVouRelease.Image = global::LIMS.Views.Properties.Resources.MenuBar_Close;
            this.btVouRelease.ImagePaddingHorizontal = 8;
            this.btVouRelease.Name = "btVouRelease";
            this.btVouRelease.Text = "单据删除";
            // 
            // _ParameterPanel
            // 
            this._ParameterPanel.BackColor = System.Drawing.Color.Transparent;
            this._ParameterPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this._ParameterPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._ParameterPanel.Controls.Add(this.cmbMaterial);
            this._ParameterPanel.Controls.Add(this.cmbSampleName);
            this._ParameterPanel.Controls.Add(this.cmbTypeName);
            this._ParameterPanel.Controls.Add(this.dateTo);
            this._ParameterPanel.Controls.Add(this.dateFrom);
            this._ParameterPanel.Controls.Add(this.panel7);
            this._ParameterPanel.Controls.Add(this.lblDateFrom);
            this._ParameterPanel.Controls.Add(this.panel8);
            this._ParameterPanel.Controls.Add(this.lblDateTo);
            this._ParameterPanel.Controls.Add(this.txtVoucherID);
            this._ParameterPanel.Controls.Add(this.panel4);
            this._ParameterPanel.Controls.Add(this.label4);
            this._ParameterPanel.Controls.Add(this.panel2);
            this._ParameterPanel.Controls.Add(this.label2);
            this._ParameterPanel.Controls.Add(this.panel1);
            this._ParameterPanel.Controls.Add(this.label1);
            this._ParameterPanel.Controls.Add(this.panel3);
            this._ParameterPanel.Controls.Add(this.label3);
            this._ParameterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ParameterPanel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ParameterPanel.Location = new System.Drawing.Point(3, 31);
            this._ParameterPanel.Name = "_ParameterPanel";
            this._ParameterPanel.Size = new System.Drawing.Size(799, 139);
            // 
            // 
            // 
            this._ParameterPanel.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._ParameterPanel.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._ParameterPanel.Style.BackColorGradientAngle = 90;
            this._ParameterPanel.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ParameterPanel.Style.BorderBottomWidth = 1;
            this._ParameterPanel.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._ParameterPanel.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ParameterPanel.Style.BorderLeftWidth = 1;
            this._ParameterPanel.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ParameterPanel.Style.BorderRightWidth = 1;
            this._ParameterPanel.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ParameterPanel.Style.BorderTopWidth = 1;
            this._ParameterPanel.Style.CornerDiameter = 4;
            this._ParameterPanel.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._ParameterPanel.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this._ParameterPanel.TabIndex = 181;
            this._ParameterPanel.Text = "[查询参数]";
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.Location = new System.Drawing.Point(121, 62);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbMaterial.Properties.Appearance.Options.UseFont = true;
            this.cmbMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMaterial.Properties.NullText = "";
            this.cmbMaterial.Properties.PopupWidth = 450;
            this.cmbMaterial.Properties.ShowFooter = false;
            this.cmbMaterial.Size = new System.Drawing.Size(217, 24);
            this.cmbMaterial.TabIndex = 416;
            // 
            // cmbSampleName
            // 
            this.cmbSampleName.Location = new System.Drawing.Point(469, 35);
            this.cmbSampleName.Name = "cmbSampleName";
            this.cmbSampleName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSampleName.Properties.Appearance.Options.UseFont = true;
            this.cmbSampleName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSampleName.Properties.NullText = "";
            this.cmbSampleName.Properties.PopupWidth = 450;
            this.cmbSampleName.Properties.ShowFooter = false;
            this.cmbSampleName.Size = new System.Drawing.Size(217, 24);
            this.cmbSampleName.TabIndex = 415;
            // 
            // cmbTypeName
            // 
            this.cmbTypeName.BackColor = System.Drawing.Color.White;
            this.cmbTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeName.FormattingEnabled = true;
            this.cmbTypeName.Location = new System.Drawing.Point(121, 31);
            this.cmbTypeName.Name = "cmbTypeName";
            this.cmbTypeName.Size = new System.Drawing.Size(215, 25);
            this.cmbTypeName.TabIndex = 414;
            // 
            // dateTo
            // 
            this.dateTo.EditValue = new System.DateTime(2012, 2, 7, 10, 35, 9, 61);
            this.dateTo.Location = new System.Drawing.Point(469, 3);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dateTo.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dateTo.Properties.Appearance.Options.UseBackColor = true;
            this.dateTo.Properties.Appearance.Options.UseFont = true;
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTo.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTo.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateTo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateTo.Properties.VistaTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTo.Size = new System.Drawing.Size(217, 24);
            this.dateTo.TabIndex = 388;
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = new System.DateTime(2012, 2, 7, 10, 35, 9, 61);
            this.dateFrom.Location = new System.Drawing.Point(121, 3);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.dateFrom.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dateFrom.Properties.Appearance.Options.UseBackColor = true;
            this.dateFrom.Properties.Appearance.Options.UseFont = true;
            this.dateFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateFrom.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateFrom.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateFrom.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateFrom.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateFrom.Properties.VistaTimeProperties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateFrom.Size = new System.Drawing.Size(217, 24);
            this.dateFrom.TabIndex = 387;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.Location = new System.Drawing.Point(14, 24);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(120, 1);
            this.panel7.TabIndex = 386;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblDateFrom.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDateFrom.Location = new System.Drawing.Point(14, 5);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(89, 18);
            this.lblDateFrom.TabIndex = 385;
            this.lblDateFrom.Text = "Date From:";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Window;
            this.panel8.Location = new System.Drawing.Point(373, 24);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(120, 1);
            this.panel8.TabIndex = 384;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTo.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDateTo.Location = new System.Drawing.Point(370, 3);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(32, 18);
            this.lblDateTo.TabIndex = 383;
            this.lblDateTo.Text = "To:";
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
            this.txtVoucherID.Location = new System.Drawing.Point(469, 63);
            this.txtVoucherID.Name = "txtVoucherID";
            this.txtVoucherID.Size = new System.Drawing.Size(217, 21);
            this.txtVoucherID.TabIndex = 346;
            this.txtVoucherID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtVoucherID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Location = new System.Drawing.Point(369, 83);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 1);
            this.panel4.TabIndex = 345;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(367, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 344;
            this.label4.Text = "单据号";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(371, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 1);
            this.panel2.TabIndex = 342;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(369, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 341;
            this.label2.Text = "样品名称";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(14, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 1);
            this.panel1.TabIndex = 339;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(15, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 338;
            this.label1.Text = "物料规格";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Location = new System.Drawing.Point(14, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 1);
            this.panel3.TabIndex = 336;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(15, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 335;
            this.label3.Text = "类型";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupPanel2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 176);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(799, 261);
            this.panel6.TabIndex = 182;
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.groupPanel2.Controls.Add(this.gridControl1);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel2.Location = new System.Drawing.Point(0, 0);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(799, 261);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.groupPanel2.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this.groupPanel2.TabIndex = 186;
            this.groupPanel2.Text = "[单据明细]";
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
            this.gridControl1.Size = new System.Drawing.Size(793, 237);
            this.gridControl1.TabIndex = 184;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupFormat = " [#image]{1} {2}";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 440);
            this.Controls.Add(this.ribbonClientPanel2);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.TabText = "MM00";
            this.Text = "结果审核";
            this.ribbonClientPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this._ParameterPanel.ResumeLayout(false);
            this._ParameterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            this.panel6.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel ribbonClientPanel2;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btSearchSure;
        private DevComponents.DotNetBar.ButtonItem btVouSubmit;
        private DevComponents.DotNetBar.ButtonItem btVouRelease;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel _ParameterPanel;
        public DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVoucherID;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel6;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.ComboBox cmbTypeName;
        public DevExpress.XtraEditors.LookUpEdit cmbSampleName;
        public DevExpress.XtraEditors.LookUpEdit cmbMaterial;
    }
}

