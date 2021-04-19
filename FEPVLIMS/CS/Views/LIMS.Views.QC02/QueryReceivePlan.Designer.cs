namespace LIMS.Views.QC02
{
    partial class QueryReceivePlan
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryReceivePlan));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._ResultVoucher = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbSampleName = new DevExpress.XtraEditors.LookUpEdit();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtVoucherID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dateE = new DevExpress.XtraEditors.DateEdit();
            this.dateB = new DevExpress.XtraEditors.DateEdit();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pMaterial = new System.Windows.Forms.Panel();
            this.lblVoucherID = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cmbGateMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this._ResultVoucher.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this._Parameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGateMaterial.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._ResultVoucher, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._Parameter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 89F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(889, 465);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // _ResultVoucher
            // 
            this._ResultVoucher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ResultVoucher.BackColor = System.Drawing.Color.Transparent;
            this._ResultVoucher.CanvasColor = System.Drawing.SystemColors.Control;
            this._ResultVoucher.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._ResultVoucher.Controls.Add(this.gridControl1);
            this._ResultVoucher.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._ResultVoucher.Location = new System.Drawing.Point(3, 92);
            this._ResultVoucher.Name = "_ResultVoucher";
            this._ResultVoucher.Size = new System.Drawing.Size(883, 370);
            // 
            // 
            // 
            this._ResultVoucher.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._ResultVoucher.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this._ResultVoucher.Style.BackColorGradientAngle = 90;
            this._ResultVoucher.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ResultVoucher.Style.BorderBottomWidth = 1;
            this._ResultVoucher.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this._ResultVoucher.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ResultVoucher.Style.BorderLeftWidth = 1;
            this._ResultVoucher.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ResultVoucher.Style.BorderRightWidth = 1;
            this._ResultVoucher.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this._ResultVoucher.Style.BorderTopWidth = 1;
            this._ResultVoucher.Style.CornerDiameter = 4;
            this._ResultVoucher.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this._ResultVoucher.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this._ResultVoucher.TabIndex = 203;
            this._ResultVoucher.Text = "[原辅料检验计划]";
            this._ResultVoucher.TitleImage = ((System.Drawing.Image)(resources.GetObject("_ResultVoucher.TitleImage")));
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
            this.gridControl1.Size = new System.Drawing.Size(877, 348);
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
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.cmbGateMaterial);
            this._Parameter.Controls.Add(this.panel2);
            this._Parameter.Controls.Add(this.lblMaterial);
            this._Parameter.Controls.Add(this.cmbSampleName);
            this._Parameter.Controls.Add(this.lblSampleName);
            this._Parameter.Controls.Add(this.panel5);
            this._Parameter.Controls.Add(this.txtVoucherID);
            this._Parameter.Controls.Add(this.dateE);
            this._Parameter.Controls.Add(this.dateB);
            this._Parameter.Controls.Add(this.panel4);
            this._Parameter.Controls.Add(this.panel1);
            this._Parameter.Controls.Add(this.pMaterial);
            this._Parameter.Controls.Add(this.lblVoucherID);
            this._Parameter.Controls.Add(this.lblTo);
            this._Parameter.Controls.Add(this.lblFrom);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(3, 3);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(883, 83);
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
            this._Parameter.TabIndex = 199;
            this._Parameter.Text = "[Parameter]";
            this._Parameter.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Parameter.TitleImage")));
            // 
            // cmbSampleName
            // 
            this.cmbSampleName.Location = new System.Drawing.Point(164, 35);
            this.cmbSampleName.Name = "cmbSampleName";
            this.cmbSampleName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSampleName.Properties.Appearance.Options.UseFont = true;
            this.cmbSampleName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSampleName.Properties.NullText = "";
            this.cmbSampleName.Properties.PopupWidth = 450;
            this.cmbSampleName.Properties.ShowFooter = false;
            this.cmbSampleName.Size = new System.Drawing.Size(200, 24);
            this.cmbSampleName.TabIndex = 417;
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleName.Location = new System.Drawing.Point(21, 36);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(107, 18);
            this.lblSampleName.TabIndex = 415;
            this.lblSampleName.Text = "SampleName:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Location = new System.Drawing.Point(30, 55);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(140, 1);
            this.panel5.TabIndex = 416;
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
            this.txtVoucherID.Location = new System.Drawing.Point(780, 32);
            this.txtVoucherID.Name = "txtVoucherID";
            this.txtVoucherID.Size = new System.Drawing.Size(147, 23);
            this.txtVoucherID.TabIndex = 406;
            this.txtVoucherID.WatermarkColor = System.Drawing.Color.Silver;
            this.txtVoucherID.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // dateE
            // 
            this.dateE.EditValue = null;
            this.dateE.Location = new System.Drawing.Point(513, 2);
            this.dateE.Name = "dateE";
            this.dateE.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dateE.Properties.Appearance.Options.UseFont = true;
            this.dateE.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateE.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateE.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateE.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateE.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateE.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateE.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateE.Size = new System.Drawing.Size(147, 24);
            this.dateE.TabIndex = 323;
            // 
            // dateB
            // 
            this.dateB.EditValue = null;
            this.dateB.Location = new System.Drawing.Point(164, 5);
            this.dateB.Name = "dateB";
            this.dateB.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dateB.Properties.Appearance.Options.UseFont = true;
            this.dateB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateB.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateB.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateB.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateB.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateB.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateB.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateB.Size = new System.Drawing.Size(200, 24);
            this.dateB.TabIndex = 322;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Location = new System.Drawing.Point(682, 53);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(105, 1);
            this.panel4.TabIndex = 321;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(415, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(105, 1);
            this.panel1.TabIndex = 321;
            // 
            // pMaterial
            // 
            this.pMaterial.BackColor = System.Drawing.SystemColors.Window;
            this.pMaterial.Location = new System.Drawing.Point(28, 25);
            this.pMaterial.Name = "pMaterial";
            this.pMaterial.Size = new System.Drawing.Size(150, 1);
            this.pMaterial.TabIndex = 321;
            // 
            // lblVoucherID
            // 
            this.lblVoucherID.AutoSize = true;
            this.lblVoucherID.BackColor = System.Drawing.Color.Transparent;
            this.lblVoucherID.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoucherID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblVoucherID.Location = new System.Drawing.Point(679, 36);
            this.lblVoucherID.Name = "lblVoucherID";
            this.lblVoucherID.Size = new System.Drawing.Size(90, 18);
            this.lblVoucherID.TabIndex = 320;
            this.lblVoucherID.Text = "VoucherID:";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.BackColor = System.Drawing.Color.Transparent;
            this.lblTo.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTo.Location = new System.Drawing.Point(412, 7);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(32, 18);
            this.lblTo.TabIndex = 320;
            this.lblTo.Text = "To:";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblFrom.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblFrom.Location = new System.Drawing.Point(25, 7);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(52, 18);
            this.lblFrom.TabIndex = 320;
            this.lblFrom.Text = "From:";
            // 
            // cmbGateMaterial
            // 
            this.cmbGateMaterial.Location = new System.Drawing.Point(513, 35);
            this.cmbGateMaterial.Name = "cmbGateMaterial";
            this.cmbGateMaterial.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGateMaterial.Properties.Appearance.Options.UseFont = true;
            this.cmbGateMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbGateMaterial.Properties.NullText = "";
            this.cmbGateMaterial.Properties.PopupWidth = 450;
            this.cmbGateMaterial.Properties.ShowFooter = false;
            this.cmbGateMaterial.Size = new System.Drawing.Size(156, 24);
            this.cmbGateMaterial.TabIndex = 420;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(406, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(63, 1);
            this.panel2.TabIndex = 419;
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.BackColor = System.Drawing.Color.Transparent;
            this.lblMaterial.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMaterial.Location = new System.Drawing.Point(410, 38);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(73, 18);
            this.lblMaterial.TabIndex = 418;
            this.lblMaterial.Text = "Material:";
            // 
            // QueryReceivePlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "QueryReceivePlan";
            this.Size = new System.Drawing.Size(889, 465);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._ResultVoucher.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGateMaterial.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        private DevComponents.DotNetBar.Controls.TextBoxX txtVoucherID;
        public DevExpress.XtraEditors.DateEdit dateE;
        public DevExpress.XtraEditors.DateEdit dateB;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pMaterial;
        private System.Windows.Forms.Label lblVoucherID;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private DevComponents.DotNetBar.Controls.GroupPanel _ResultVoucher;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraEditors.LookUpEdit cmbSampleName;
        private System.Windows.Forms.Label lblSampleName;
        private System.Windows.Forms.Panel panel5;
        private DevExpress.XtraEditors.LookUpEdit cmbGateMaterial;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblMaterial;

    }
}
