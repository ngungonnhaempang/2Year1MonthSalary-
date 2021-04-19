namespace LIMS.Views.QC03
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
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.btDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btAddPlan = new DevComponents.DotNetBar.ButtonItem();
            this.btnAcceptDraft = new DevComponents.DotNetBar.ButtonItem();
            this.btSearchPlan = new DevComponents.DotNetBar.ButtonItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearch = new DevComponents.DotNetBar.ButtonItem();
            this.btnADraft = new DevComponents.DotNetBar.ButtonItem();
            this.btnAcceptVoucher = new DevComponents.DotNetBar.ButtonItem();
            this.btnRejectDraft = new DevComponents.DotNetBar.ButtonItem();
            this.btnRejectVoucher = new DevComponents.DotNetBar.ButtonItem();
            this.btnExit = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonClientPanel2 = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._ParameterPanel = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dateTo = new DevExpress.XtraEditors.DateEdit();
            this.dateFrom = new DevExpress.XtraEditors.DateEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.txtReason = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVoucherID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDraftID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.butReqQuery = new DevComponents.DotNetBar.ButtonItem();
            this.btAcceptDraft = new DevComponents.DotNetBar.ButtonItem();
            this.btAcceptVoucher = new DevComponents.DotNetBar.ButtonItem();
            this.btPlanAddConfirm = new DevComponents.DotNetBar.ButtonItem();
            this.btRejectDraft = new DevComponents.DotNetBar.ButtonItem();
            this.btRejectVoucher = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.ribbonClientPanel2.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this._ParameterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.SuspendLayout();
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
            // btReturn
            // 
            this.btReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReturn.Image = ((System.Drawing.Image)(resources.GetObject("btReturn.Image")));
            this.btReturn.ImagePaddingHorizontal = 8;
            this.btReturn.Name = "btReturn";
            this.btReturn.Text = "Exit";
            // 
            // btDelete
            // 
            this.btDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImagePaddingHorizontal = 8;
            this.btDelete.Name = "btDelete";
            this.btDelete.Text = "RejectDraft";
            // 
            // btAddPlan
            // 
            this.btAddPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAddPlan.Image = ((System.Drawing.Image)(resources.GetObject("btAddPlan.Image")));
            this.btAddPlan.ImagePaddingHorizontal = 8;
            this.btAddPlan.Name = "btAddPlan";
            this.btAddPlan.Text = "AcceptVoucher";
            // 
            // btnAcceptDraft
            // 
            this.btnAcceptDraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAcceptDraft.Image = ((System.Drawing.Image)(resources.GetObject("btnAcceptDraft.Image")));
            this.btnAcceptDraft.ImagePaddingHorizontal = 8;
            this.btnAcceptDraft.Name = "btnAcceptDraft";
            this.btnAcceptDraft.Text = "AcceptDraft";
            // 
            // btSearchPlan
            // 
            this.btSearchPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchPlan.Image = ((System.Drawing.Image)(resources.GetObject("btSearchPlan.Image")));
            this.btSearchPlan.ImagePaddingHorizontal = 8;
            this.btSearchPlan.Name = "btSearchPlan";
            this.btSearchPlan.Text = "Search Plan";
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(588, 24);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 20;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearch
            // 
            this.btSearch.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearch.Image = ((System.Drawing.Image)(resources.GetObject("btSearch.Image")));
            this.btSearch.ImagePaddingHorizontal = 8;
            this.btSearch.Name = "btSearch";
            this.btSearch.Text = "Search";
            // 
            // btnADraft
            // 
            this.btnADraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnADraft.Image = ((System.Drawing.Image)(resources.GetObject("btnADraft.Image")));
            this.btnADraft.ImagePaddingHorizontal = 8;
            this.btnADraft.Name = "btnADraft";
            this.btnADraft.Text = "AcceptDraft";
            // 
            // btnAcceptVoucher
            // 
            this.btnAcceptVoucher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAcceptVoucher.Image = ((System.Drawing.Image)(resources.GetObject("btnAcceptVoucher.Image")));
            this.btnAcceptVoucher.ImagePaddingHorizontal = 8;
            this.btnAcceptVoucher.Name = "btnAcceptVoucher";
            this.btnAcceptVoucher.Text = "AcceptVoucher";
            // 
            // btnRejectDraft
            // 
            this.btnRejectDraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRejectDraft.Image = ((System.Drawing.Image)(resources.GetObject("btnRejectDraft.Image")));
            this.btnRejectDraft.ImagePaddingHorizontal = 8;
            this.btnRejectDraft.Name = "btnRejectDraft";
            this.btnRejectDraft.Text = "RejectDraft";
            // 
            // btnRejectVoucher
            // 
            this.btnRejectVoucher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRejectVoucher.Image = ((System.Drawing.Image)(resources.GetObject("btnRejectVoucher.Image")));
            this.btnRejectVoucher.ImagePaddingHorizontal = 8;
            this.btnRejectVoucher.Name = "btnRejectVoucher";
            this.btnRejectVoucher.Text = "RejectVoucher";
            // 
            // btnExit
            // 
            this.btnExit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImagePaddingHorizontal = 8;
            this.btnExit.Name = "btnExit";
            this.btnExit.Text = "Exit";
            // 
            // ribbonClientPanel2
            // 
            this.ribbonClientPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.ribbonClientPanel2.Controls.Add(this.groupPanel2);
            this.ribbonClientPanel2.Controls.Add(this.groupPanel1);
            this.ribbonClientPanel2.Controls.Add(this._ParameterPanel);
            this.ribbonClientPanel2.Controls.Add(this.bar2);
            this.ribbonClientPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonClientPanel2.Location = new System.Drawing.Point(0, 0);
            this.ribbonClientPanel2.Name = "ribbonClientPanel2";
            this.ribbonClientPanel2.ShowFocusRectangle = true;
            this.ribbonClientPanel2.Size = new System.Drawing.Size(1091, 629);
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
            this.ribbonClientPanel2.TabIndex = 23;
            // 
            // groupPanel2
            // 
            this.groupPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.groupPanel2.Controls.Add(this.gridControl2);
            this.groupPanel2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel2.Location = new System.Drawing.Point(3, 367);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(1076, 259);
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
            this.groupPanel2.Text = "[样品属性]";
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
            this.gridControl2.Size = new System.Drawing.Size(1070, 235);
            this.gridControl2.TabIndex = 184;
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
            this.gridView2.OptionsView.ColumnAutoWidth = false;
            this.gridView2.OptionsView.RowAutoHeight = true;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.groupPanel1.Controls.Add(this.gridControl1);
            this.groupPanel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupPanel1.Location = new System.Drawing.Point(3, 162);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(1076, 199);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.groupPanel1.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this.groupPanel1.TabIndex = 185;
            this.groupPanel1.Text = "[委托数据]";
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
            this.gridControl1.Size = new System.Drawing.Size(1070, 175);
            this.gridControl1.TabIndex = 184;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView3});
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl1;
            this.gridView3.Name = "gridView3";
            // 
            // _ParameterPanel
            // 
            this._ParameterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ParameterPanel.BackColor = System.Drawing.Color.Transparent;
            this._ParameterPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this._ParameterPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._ParameterPanel.Controls.Add(this.dateTo);
            this._ParameterPanel.Controls.Add(this.dateFrom);
            this._ParameterPanel.Controls.Add(this.panel4);
            this._ParameterPanel.Controls.Add(this.lblDateFrom);
            this._ParameterPanel.Controls.Add(this.panel6);
            this._ParameterPanel.Controls.Add(this.lblDateTo);
            this._ParameterPanel.Controls.Add(this.txtReason);
            this._ParameterPanel.Controls.Add(this.panel3);
            this._ParameterPanel.Controls.Add(this.label9);
            this._ParameterPanel.Controls.Add(this.label7);
            this._ParameterPanel.Controls.Add(this.label6);
            this._ParameterPanel.Controls.Add(this.label5);
            this._ParameterPanel.Controls.Add(this.label3);
            this._ParameterPanel.Controls.Add(this.txtVoucherID);
            this._ParameterPanel.Controls.Add(this.panel1);
            this._ParameterPanel.Controls.Add(this.label1);
            this._ParameterPanel.Controls.Add(this.txtDraftID);
            this._ParameterPanel.Controls.Add(this.panel2);
            this._ParameterPanel.Controls.Add(this.label2);
            this._ParameterPanel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._ParameterPanel.Location = new System.Drawing.Point(3, 32);
            this._ParameterPanel.Name = "_ParameterPanel";
            this._ParameterPanel.Size = new System.Drawing.Size(1076, 124);
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
            this._ParameterPanel.Text = "[参数]";
            // 
            // dateTo
            // 
            this.dateTo.EditValue = null;
            this.dateTo.Location = new System.Drawing.Point(491, 3);
            this.dateTo.Name = "dateTo";
            this.dateTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTo.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTo.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateTo.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.dateTo.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateTo.Size = new System.Drawing.Size(127, 20);
            this.dateTo.TabIndex = 389;
            // 
            // dateFrom
            // 
            this.dateFrom.EditValue = new System.DateTime(2012, 2, 7, 10, 35, 9, 61);
            this.dateFrom.Location = new System.Drawing.Point(198, 3);
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
            this.dateFrom.Size = new System.Drawing.Size(147, 24);
            this.dateFrom.TabIndex = 387;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Location = new System.Drawing.Point(51, 26);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 1);
            this.panel4.TabIndex = 386;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblDateFrom.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDateFrom.Location = new System.Drawing.Point(51, 7);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(89, 18);
            this.lblDateFrom.TabIndex = 385;
            this.lblDateFrom.Text = "Date From:";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.Location = new System.Drawing.Point(391, 21);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(120, 1);
            this.panel6.TabIndex = 384;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.BackColor = System.Drawing.Color.Transparent;
            this.lblDateTo.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDateTo.Location = new System.Drawing.Point(388, 4);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(32, 18);
            this.lblDateTo.TabIndex = 383;
            this.lblDateTo.Text = "To:";
            // 
            // txtReason
            // 
            this.txtReason.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtReason.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtReason.Border.Class = "RibbonGalleryContainer";
            this.txtReason.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtReason.Border.PaddingTop = 2;
            this.txtReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtReason.Location = new System.Drawing.Point(574, 58);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(193, 23);
            this.txtReason.TabIndex = 308;
            this.txtReason.WatermarkColor = System.Drawing.Color.Silver;
            this.txtReason.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(478, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(205, 1);
            this.panel3.TabIndex = 307;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(97, -19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.TabIndex = 303;
            this.label8.Text = "MXQ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(475, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 20);
            this.label9.TabIndex = 306;
            this.label9.Text = "样品拒收原因";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(355, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 20);
            this.label7.TabIndex = 305;
            this.label7.Text = "(输入后请按回车)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(355, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 20);
            this.label6.TabIndex = 304;
            this.label6.Text = "(输入后请按回车)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(147, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 20);
            this.label5.TabIndex = 303;
            this.label5.Text = "RMXQ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(156, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 302;
            this.label3.Text = "MXQ";
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
            this.txtVoucherID.Location = new System.Drawing.Point(199, 61);
            this.txtVoucherID.Name = "txtVoucherID";
            this.txtVoucherID.Size = new System.Drawing.Size(150, 23);
            this.txtVoucherID.TabIndex = 301;
            this.txtVoucherID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtVoucherID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(53, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(205, 1);
            this.panel1.TabIndex = 300;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(97, -19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 303;
            this.label4.Text = "MXQ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(50, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.TabIndex = 299;
            this.label1.Text = "样品编号";
            // 
            // txtDraftID
            // 
            this.txtDraftID.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtDraftID.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtDraftID.Border.Class = "RibbonGalleryContainer";
            this.txtDraftID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtDraftID.Border.PaddingTop = 2;
            this.txtDraftID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtDraftID.Location = new System.Drawing.Point(199, 32);
            this.txtDraftID.Name = "txtDraftID";
            this.txtDraftID.Size = new System.Drawing.Size(150, 23);
            this.txtDraftID.TabIndex = 298;
            this.txtDraftID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtDraftID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(53, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 1);
            this.panel2.TabIndex = 297;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(50, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 296;
            this.label2.Text = "委托编号";
            // 
            // bar2
            // 
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.butReqQuery,
            this.btAcceptDraft,
            this.btAcceptVoucher,
            this.btPlanAddConfirm,
            this.btRejectDraft,
            this.btRejectVoucher});
            this.bar2.Location = new System.Drawing.Point(0, 0);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(1091, 26);
            this.bar2.Stretch = true;
            this.bar2.TabIndex = 15;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // butReqQuery
            // 
            this.butReqQuery.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.butReqQuery.Image = ((System.Drawing.Image)(resources.GetObject("butReqQuery.Image")));
            this.butReqQuery.ImagePaddingHorizontal = 8;
            this.butReqQuery.Name = "butReqQuery";
            this.butReqQuery.Text = "委托单据查询";
            // 
            // btAcceptDraft
            // 
            this.btAcceptDraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAcceptDraft.Image = ((System.Drawing.Image)(resources.GetObject("btAcceptDraft.Image")));
            this.btAcceptDraft.ImagePaddingHorizontal = 8;
            this.btAcceptDraft.Name = "btAcceptDraft";
            this.btAcceptDraft.Text = "按委托单接收";
            // 
            // btAcceptVoucher
            // 
            this.btAcceptVoucher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAcceptVoucher.Image = ((System.Drawing.Image)(resources.GetObject("btAcceptVoucher.Image")));
            this.btAcceptVoucher.ImagePaddingHorizontal = 8;
            this.btAcceptVoucher.Name = "btAcceptVoucher";
            this.btAcceptVoucher.Text = "按样品接收";
            // 
            // btPlanAddConfirm
            // 
            this.btPlanAddConfirm.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btPlanAddConfirm.Image = ((System.Drawing.Image)(resources.GetObject("btPlanAddConfirm.Image")));
            this.btPlanAddConfirm.ImagePaddingHorizontal = 8;
            this.btPlanAddConfirm.Name = "btPlanAddConfirm";
            this.btPlanAddConfirm.Text = "在线加测确认";
            // 
            // btRejectDraft
            // 
            this.btRejectDraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRejectDraft.Image = ((System.Drawing.Image)(resources.GetObject("btRejectDraft.Image")));
            this.btRejectDraft.ImagePaddingHorizontal = 8;
            this.btRejectDraft.Name = "btRejectDraft";
            this.btRejectDraft.Text = "按委托单删除";
            // 
            // btRejectVoucher
            // 
            this.btRejectVoucher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRejectVoucher.Image = ((System.Drawing.Image)(resources.GetObject("btRejectVoucher.Image")));
            this.btRejectVoucher.ImagePaddingHorizontal = 8;
            this.btRejectVoucher.Name = "btRejectVoucher";
            this.btRejectVoucher.Text = "按样品删除";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 629);
            this.Controls.Add(this.ribbonClientPanel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "QA23";
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ribbonClientPanel2.ResumeLayout(false);
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this._ParameterPanel.ResumeLayout(false);
            this._ParameterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateFrom.Properties)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        private DevComponents.DotNetBar.ButtonItem btDelete;
        private DevComponents.DotNetBar.ButtonItem btAddPlan;
        private DevComponents.DotNetBar.ButtonItem btnAcceptDraft;
        private DevComponents.DotNetBar.ButtonItem btSearchPlan;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btSearch;
        private DevComponents.DotNetBar.ButtonItem btnADraft;
        private DevComponents.DotNetBar.ButtonItem btnAcceptVoucher;
        private DevComponents.DotNetBar.ButtonItem btnRejectDraft;
        private DevComponents.DotNetBar.ButtonItem btnRejectVoucher;
        private DevComponents.DotNetBar.ButtonItem btnExit;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel ribbonClientPanel2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        public DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        private DevComponents.DotNetBar.Controls.GroupPanel _ParameterPanel;
        private DevComponents.DotNetBar.Controls.TextBoxX txtReason;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVoucherID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDraftID;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btAcceptDraft;
        private DevComponents.DotNetBar.ButtonItem btAcceptVoucher;
        private DevComponents.DotNetBar.ButtonItem btPlanAddConfirm;
        private DevComponents.DotNetBar.ButtonItem btRejectDraft;
        private DevComponents.DotNetBar.ButtonItem btRejectVoucher;
        private DevComponents.DotNetBar.ButtonItem butReqQuery;
        private DevExpress.XtraEditors.DateEdit dateFrom;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblDateTo;
        private DevExpress.XtraEditors.DateEdit dateTo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
    }
}