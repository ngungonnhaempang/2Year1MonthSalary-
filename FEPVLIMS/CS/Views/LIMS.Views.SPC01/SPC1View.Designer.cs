namespace LIMS.Views
{
    partial class SPC1View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPC1View));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._Result = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbLottoNo = new System.Windows.Forms.ComboBox();
            this.cmbTypeName = new System.Windows.Forms.ComboBox();
            this.cmbLabName = new System.Windows.Forms.ComboBox();
            this.cmbLineNo = new System.Windows.Forms.ComboBox();
            this.cmbSample = new System.Windows.Forms.ComboBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLineNo = new System.Windows.Forms.Label();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.lblLOTTO = new System.Windows.Forms.Label();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.lblLabName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this._Result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this._Parameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._Result, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._Parameter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 138F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(735, 405);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // _Result
            // 
            this._Result.BackColor = System.Drawing.Color.Transparent;
            this._Result.CanvasColor = System.Drawing.SystemColors.Control;
            this._Result.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Result.Controls.Add(this.gridControl1);
            this._Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Result.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Result.Location = new System.Drawing.Point(3, 141);
            this._Result.Name = "_Result";
            this._Result.Size = new System.Drawing.Size(729, 261);
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
            this._Result.TabIndex = 198;
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
            this.gridControl1.Size = new System.Drawing.Size(723, 239);
            this.gridControl1.TabIndex = 186;
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.cmbLottoNo);
            this._Parameter.Controls.Add(this.cmbTypeName);
            this._Parameter.Controls.Add(this.cmbLabName);
            this._Parameter.Controls.Add(this.cmbLineNo);
            this._Parameter.Controls.Add(this.cmbSample);
            this._Parameter.Controls.Add(this.panel5);
            this._Parameter.Controls.Add(this.panel3);
            this._Parameter.Controls.Add(this.panel4);
            this._Parameter.Controls.Add(this.panel1);
            this._Parameter.Controls.Add(this.panel2);
            this._Parameter.Controls.Add(this.lblLineNo);
            this._Parameter.Controls.Add(this.lblSampleName);
            this._Parameter.Controls.Add(this.lblLOTTO);
            this._Parameter.Controls.Add(this.lblTypeName);
            this._Parameter.Controls.Add(this.lblLabName);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(3, 3);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(729, 132);
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
            this._Parameter.TabIndex = 197;
            this._Parameter.Text = "[Parameter]";
            this._Parameter.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Parameter.TitleImage")));
            // 
            // cmbLottoNo
            // 
            this.cmbLottoNo.BackColor = System.Drawing.Color.White;
            this.cmbLottoNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLottoNo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbLottoNo.FormattingEnabled = true;
            this.cmbLottoNo.Location = new System.Drawing.Point(482, 14);
            this.cmbLottoNo.Name = "cmbLottoNo";
            this.cmbLottoNo.Size = new System.Drawing.Size(169, 25);
            this.cmbLottoNo.TabIndex = 215;
            // 
            // cmbTypeName
            // 
            this.cmbTypeName.BackColor = System.Drawing.Color.White;
            this.cmbTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeName.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbTypeName.FormattingEnabled = true;
            this.cmbTypeName.Location = new System.Drawing.Point(156, 42);
            this.cmbTypeName.Name = "cmbTypeName";
            this.cmbTypeName.Size = new System.Drawing.Size(169, 25);
            this.cmbTypeName.TabIndex = 215;
            // 
            // cmbLabName
            // 
            this.cmbLabName.BackColor = System.Drawing.Color.White;
            this.cmbLabName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLabName.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbLabName.FormattingEnabled = true;
            this.cmbLabName.Location = new System.Drawing.Point(156, 12);
            this.cmbLabName.Name = "cmbLabName";
            this.cmbLabName.Size = new System.Drawing.Size(169, 25);
            this.cmbLabName.TabIndex = 214;
            // 
            // cmbLineNo
            // 
            this.cmbLineNo.BackColor = System.Drawing.Color.White;
            this.cmbLineNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineNo.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbLineNo.FormattingEnabled = true;
            this.cmbLineNo.Location = new System.Drawing.Point(482, 45);
            this.cmbLineNo.Name = "cmbLineNo";
            this.cmbLineNo.Size = new System.Drawing.Size(169, 25);
            this.cmbLineNo.TabIndex = 216;
            // 
            // cmbSample
            // 
            this.cmbSample.BackColor = System.Drawing.Color.White;
            this.cmbSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSample.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSample.FormattingEnabled = true;
            this.cmbSample.Location = new System.Drawing.Point(156, 72);
            this.cmbSample.Name = "cmbSample";
            this.cmbSample.Size = new System.Drawing.Size(169, 25);
            this.cmbSample.TabIndex = 216;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(353, 68);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(130, 1);
            this.panel5.TabIndex = 211;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(16, 96);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 1);
            this.panel3.TabIndex = 211;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(353, 38);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(130, 1);
            this.panel4.TabIndex = 212;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(16, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 1);
            this.panel1.TabIndex = 212;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(16, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 1);
            this.panel2.TabIndex = 213;
            // 
            // lblLineNo
            // 
            this.lblLineNo.AutoSize = true;
            this.lblLineNo.BackColor = System.Drawing.Color.Transparent;
            this.lblLineNo.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLineNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLineNo.Location = new System.Drawing.Point(350, 47);
            this.lblLineNo.Name = "lblLineNo";
            this.lblLineNo.Size = new System.Drawing.Size(98, 18);
            this.lblLineNo.TabIndex = 208;
            this.lblLineNo.Text = "Line Numer:";
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblSampleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleName.Location = new System.Drawing.Point(13, 75);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(111, 18);
            this.lblSampleName.TabIndex = 208;
            this.lblSampleName.Text = "Sample Name:";
            // 
            // lblLOTTO
            // 
            this.lblLOTTO.AutoSize = true;
            this.lblLOTTO.BackColor = System.Drawing.Color.Transparent;
            this.lblLOTTO.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLOTTO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLOTTO.Location = new System.Drawing.Point(350, 17);
            this.lblLOTTO.Name = "lblLOTTO";
            this.lblLOTTO.Size = new System.Drawing.Size(97, 18);
            this.lblLOTTO.TabIndex = 209;
            this.lblLOTTO.Text = "Material No:";
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblTypeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTypeName.Location = new System.Drawing.Point(13, 44);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(96, 18);
            this.lblTypeName.TabIndex = 209;
            this.lblTypeName.Text = "Type Name:";
            // 
            // lblLabName
            // 
            this.lblLabName.AutoSize = true;
            this.lblLabName.BackColor = System.Drawing.Color.Transparent;
            this.lblLabName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLabName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLabName.Location = new System.Drawing.Point(13, 14);
            this.lblLabName.Name = "lblLabName";
            this.lblLabName.Size = new System.Drawing.Size(86, 18);
            this.lblLabName.TabIndex = 210;
            this.lblLabName.Text = "Lab Name:";
            // 
            // SPC1View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SPC1View";
            this.Size = new System.Drawing.Size(735, 405);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._Result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel _Result;
        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        private System.Windows.Forms.ComboBox cmbTypeName;
        private System.Windows.Forms.ComboBox cmbLabName;
        private System.Windows.Forms.ComboBox cmbSample;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblLineNo;
        private System.Windows.Forms.Label lblSampleName;
        private System.Windows.Forms.Label lblLOTTO;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.Label lblLabName;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ComboBox cmbLottoNo;
        private System.Windows.Forms.ComboBox cmbLineNo;
    }
}
