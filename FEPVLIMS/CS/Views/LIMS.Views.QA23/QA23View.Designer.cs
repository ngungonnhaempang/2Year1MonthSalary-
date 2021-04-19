namespace LIMS.Views
{
    partial class QA23View
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QA23View));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._Result = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.cmbSampleName = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbState = new System.Windows.Forms.Label();
            this.txtDraftID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblLabName = new System.Windows.Forms.Label();
            this.dateE = new DevExpress.XtraEditors.DateEdit();
            this.dateB = new DevExpress.XtraEditors.DateEdit();
            this.txtVoucherID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this._Result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this._Parameter.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(914, 441);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(906, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "线上加测";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._Result, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._Parameter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(900, 409);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // _Result
            // 
            this._Result.BackColor = System.Drawing.Color.Transparent;
            this._Result.CanvasColor = System.Drawing.SystemColors.Control;
            this._Result.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Result.Controls.Add(this.gridControl1);
            this._Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Result.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Result.Location = new System.Drawing.Point(3, 95);
            this._Result.Name = "_Result";
            this._Result.Size = new System.Drawing.Size(894, 311);
            // 
            // 
            // 
            this._Result.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._Result.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._Result.Style.BackColorGradientAngle = 90;
            this._Result.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Result.Style.BorderBottomWidth = 1;
            this._Result.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._Result.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Result.Style.BorderLeftWidth = 1;
            this._Result.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Result.Style.BorderRightWidth = 1;
            this._Result.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._Result.Style.BorderTopWidth = 1;
            this._Result.Style.CornerDiameter = 4;
            this._Result.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._Result.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this._Result.TabIndex = 199;
            this._Result.Text = "[Result]";
            this._Result.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Result.TitleImage")));
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
            this.gridControl1.Size = new System.Drawing.Size(888, 289);
            this.gridControl1.TabIndex = 185;
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
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.cmbMaterial);
            this._Parameter.Controls.Add(this.cmbSampleName);
            this._Parameter.Controls.Add(this.panel6);
            this._Parameter.Controls.Add(this.panel2);
            this._Parameter.Controls.Add(this.lblMaterial);
            this._Parameter.Controls.Add(this.lblSampleName);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(3, 3);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(894, 86);
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
            this._Parameter.TabIndex = 198;
            this._Parameter.Text = "[Parameter]";
            this._Parameter.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Parameter.TitleImage")));
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.BackColor = System.Drawing.Color.White;
            this.cmbMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMaterial.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Location = new System.Drawing.Point(197, 36);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(190, 25);
            this.cmbMaterial.TabIndex = 190;
            // 
            // cmbSampleName
            // 
            this.cmbSampleName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSampleName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSampleName.BackColor = System.Drawing.Color.White;
            this.cmbSampleName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSampleName.FormattingEnabled = true;
            this.cmbSampleName.Location = new System.Drawing.Point(197, 7);
            this.cmbSampleName.Name = "cmbSampleName";
            this.cmbSampleName.Size = new System.Drawing.Size(190, 25);
            this.cmbSampleName.TabIndex = 189;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.Location = new System.Drawing.Point(36, 58);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(170, 1);
            this.panel6.TabIndex = 363;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(36, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 1);
            this.panel2.TabIndex = 363;
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.BackColor = System.Drawing.Color.Transparent;
            this.lblMaterial.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblMaterial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMaterial.Location = new System.Drawing.Point(33, 41);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(68, 18);
            this.lblMaterial.TabIndex = 2;
            this.lblMaterial.Text = "Material";
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblSampleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleName.Location = new System.Drawing.Point(33, 12);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(102, 18);
            this.lblSampleName.TabIndex = 1;
            this.lblSampleName.Text = "SampleName";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(906, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "委托";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.gridControl3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupPanel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupPanel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(900, 409);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl3.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl3.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl3.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl3.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl3.Location = new System.Drawing.Point(3, 302);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(894, 104);
            this.gridControl3.TabIndex = 200;
            this.gridControl3.UseEmbeddedNavigator = true;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.FixedLineWidth = 1;
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.GroupFormat = " [#image]{1} {2}";
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsSelection.MultiSelect = true;
            this.gridView3.OptionsView.ColumnAutoWidth = false;
            this.gridView3.OptionsView.RowAutoHeight = true;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.groupPanel1.Controls.Add(this.gridControl2);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Font = new System.Drawing.Font("Georgia", 8.25F);
            this.groupPanel1.Location = new System.Drawing.Point(3, 103);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(894, 193);
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
            this.groupPanel1.TabIndex = 199;
            this.groupPanel1.Text = "[Result]";
            this.groupPanel1.TitleImage = ((System.Drawing.Image)(resources.GetObject("groupPanel1.TitleImage")));
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
            this.gridControl2.Size = new System.Drawing.Size(888, 171);
            this.gridControl2.TabIndex = 185;
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
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.groupPanel2.Controls.Add(this.cbState);
            this.groupPanel2.Controls.Add(this.panel7);
            this.groupPanel2.Controls.Add(this.lbState);
            this.groupPanel2.Controls.Add(this.txtDraftID);
            this.groupPanel2.Controls.Add(this.panel3);
            this.groupPanel2.Controls.Add(this.lblLabName);
            this.groupPanel2.Controls.Add(this.dateE);
            this.groupPanel2.Controls.Add(this.dateB);
            this.groupPanel2.Controls.Add(this.txtVoucherID);
            this.groupPanel2.Controls.Add(this.label2);
            this.groupPanel2.Controls.Add(this.label1);
            this.groupPanel2.Controls.Add(this.lblTypeName);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel2.Font = new System.Drawing.Font("Georgia", 8.25F);
            this.groupPanel2.Location = new System.Drawing.Point(3, 3);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(894, 94);
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
            this.groupPanel2.TabIndex = 198;
            this.groupPanel2.Text = "[Parameter]";
            this.groupPanel2.TitleImage = ((System.Drawing.Image)(resources.GetObject("groupPanel2.TitleImage")));
            // 
            // cbState
            // 
            this.cbState.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbState.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbState.BackColor = System.Drawing.Color.White;
            this.cbState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(692, 7);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(182, 25);
            this.cbState.TabIndex = 365;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.Window;
            this.panel7.Location = new System.Drawing.Point(16, 72);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(170, 1);
            this.panel7.TabIndex = 366;
            // 
            // lbState
            // 
            this.lbState.AutoSize = true;
            this.lbState.BackColor = System.Drawing.Color.Transparent;
            this.lbState.Font = new System.Drawing.Font("Georgia", 12F);
            this.lbState.ForeColor = System.Drawing.Color.Black;
            this.lbState.Location = new System.Drawing.Point(624, 12);
            this.lbState.Name = "lbState";
            this.lbState.Size = new System.Drawing.Size(50, 18);
            this.lbState.TabIndex = 364;
            this.lbState.Text = "State:";
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
            this.txtDraftID.Font = new System.Drawing.Font("Georgia", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDraftID.Location = new System.Drawing.Point(125, 42);
            this.txtDraftID.Name = "txtDraftID";
            this.txtDraftID.Size = new System.Drawing.Size(158, 25);
            this.txtDraftID.TabIndex = 301;
            this.txtDraftID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtDraftID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(292, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(137, 1);
            this.panel3.TabIndex = 300;
            // 
            // lblLabName
            // 
            this.lblLabName.AutoSize = true;
            this.lblLabName.BackColor = System.Drawing.Color.Transparent;
            this.lblLabName.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.lblLabName.Location = new System.Drawing.Point(14, 47);
            this.lblLabName.Name = "lblLabName";
            this.lblLabName.Size = new System.Drawing.Size(114, 18);
            this.lblLabName.TabIndex = 299;
            this.lblLabName.Text = "DraftID委托号:";
            // 
            // dateE
            // 
            this.dateE.EditValue = new System.DateTime(2017, 11, 27, 0, 0, 0, 0);
            this.dateE.Location = new System.Drawing.Point(435, 8);
            this.dateE.Margin = new System.Windows.Forms.Padding(2);
            this.dateE.Name = "dateE";
            this.dateE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateE.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateE.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateE.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateE.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateE.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateE.Size = new System.Drawing.Size(184, 20);
            this.dateE.TabIndex = 298;
            // 
            // dateB
            // 
            this.dateB.EditValue = new System.DateTime(((long)(0)));
            this.dateB.Location = new System.Drawing.Point(103, 8);
            this.dateB.Margin = new System.Windows.Forms.Padding(2);
            this.dateB.Name = "dateB";
            this.dateB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateB.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateB.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateB.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateB.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateB.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateB.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateB.Size = new System.Drawing.Size(180, 20);
            this.dateB.TabIndex = 298;
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
            this.txtVoucherID.Font = new System.Drawing.Font("Georgia", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVoucherID.Location = new System.Drawing.Point(435, 43);
            this.txtVoucherID.Name = "txtVoucherID";
            this.txtVoucherID.Size = new System.Drawing.Size(184, 24);
            this.txtVoucherID.TabIndex = 290;
            this.txtVoucherID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtVoucherID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label2.Location = new System.Drawing.Point(294, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 18);
            this.label2.TabIndex = 285;
            this.label2.Text = "Date To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 285;
            this.label1.Text = "Date From:";
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.lblTypeName.Location = new System.Drawing.Point(294, 48);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(135, 18);
            this.lblTypeName.TabIndex = 285;
            this.lblTypeName.Text = "VoucherID样品号:";
            // 
            // QA23View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "QA23View";
            this.Size = new System.Drawing.Size(914, 441);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._Result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel _Result;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.ComboBox cmbSampleName;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.Label lblSampleName;
        public System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        public DevExpress.XtraGrid.GridControl gridControl3;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        public DevExpress.XtraGrid.GridControl gridControl2;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        public DevComponents.DotNetBar.Controls.TextBoxX txtVoucherID;
        private System.Windows.Forms.Label lblTypeName;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public DevComponents.DotNetBar.Controls.TextBoxX txtDraftID;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblLabName;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lbState;
        private DevExpress.XtraEditors.DateEdit dateE;
        private DevExpress.XtraEditors.DateEdit dateB;
    }
}
