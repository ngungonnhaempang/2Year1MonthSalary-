namespace LIMS.Views
{
    partial class MT01View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MT01View));
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.lblLabName = new System.Windows.Forms.Label();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.lblLOTTONo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmbSample = new System.Windows.Forms.ComboBox();
            this.cmbLabName = new System.Windows.Forms.ComboBox();
            this.cmbTypeName = new System.Windows.Forms.ComboBox();
            this.txtLOT_NO = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._Result = new DevComponents.DotNetBar.Controls.GroupPanel();
            this._Parameter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this._Result.SuspendLayout();
            this.SuspendLayout();
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.txtLOT_NO);
            this._Parameter.Controls.Add(this.cmbTypeName);
            this._Parameter.Controls.Add(this.cmbLabName);
            this._Parameter.Controls.Add(this.cmbSample);
            this._Parameter.Controls.Add(this.panel4);
            this._Parameter.Controls.Add(this.panel3);
            this._Parameter.Controls.Add(this.panel1);
            this._Parameter.Controls.Add(this.panel2);
            this._Parameter.Controls.Add(this.lblLOTTONo);
            this._Parameter.Controls.Add(this.lblSampleName);
            this._Parameter.Controls.Add(this.lblTypeName);
            this._Parameter.Controls.Add(this.lblLabName);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(4, 5);
            this._Parameter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(1039, 232);
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
            // lblLabName
            // 
            this.lblLabName.AutoSize = true;
            this.lblLabName.BackColor = System.Drawing.Color.Transparent;
            this.lblLabName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLabName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLabName.Location = new System.Drawing.Point(51, 12);
            this.lblLabName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLabName.Name = "lblLabName";
            this.lblLabName.Size = new System.Drawing.Size(129, 29);
            this.lblLabName.TabIndex = 278;
            this.lblLabName.Text = "Lab Name:";
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblTypeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTypeName.Location = new System.Drawing.Point(51, 58);
            this.lblTypeName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(74, 29);
            this.lblTypeName.TabIndex = 277;
            this.lblTypeName.Text = "Type:";
            // 
            // lblSampleName
            // 
            this.lblSampleName.AutoSize = true;
            this.lblSampleName.BackColor = System.Drawing.Color.Transparent;
            this.lblSampleName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblSampleName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSampleName.Location = new System.Drawing.Point(51, 105);
            this.lblSampleName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(169, 29);
            this.lblSampleName.TabIndex = 276;
            this.lblSampleName.Text = "Sample Name:";
            // 
            // lblLOTTONo
            // 
            this.lblLOTTONo.AutoSize = true;
            this.lblLOTTONo.BackColor = System.Drawing.Color.Transparent;
            this.lblLOTTONo.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLOTTONo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLOTTONo.Location = new System.Drawing.Point(51, 149);
            this.lblLOTTONo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLOTTONo.Name = "lblLOTTONo";
            this.lblLOTTONo.Size = new System.Drawing.Size(148, 29);
            this.lblLOTTONo.TabIndex = 275;
            this.lblLOTTONo.Text = "Material No:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(56, 42);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 2);
            this.panel2.TabIndex = 282;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(56, 89);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 2);
            this.panel1.TabIndex = 281;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(56, 135);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 2);
            this.panel3.TabIndex = 280;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(56, 177);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(225, 2);
            this.panel4.TabIndex = 279;
            // 
            // cmbSample
            // 
            this.cmbSample.BackColor = System.Drawing.Color.White;
            this.cmbSample.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSample.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSample.FormattingEnabled = true;
            this.cmbSample.Location = new System.Drawing.Point(278, 98);
            this.cmbSample.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbSample.Name = "cmbSample";
            this.cmbSample.Size = new System.Drawing.Size(252, 25);
            this.cmbSample.TabIndex = 2;
            // 
            // cmbLabName
            // 
            this.cmbLabName.BackColor = System.Drawing.Color.White;
            this.cmbLabName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLabName.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbLabName.FormattingEnabled = true;
            this.cmbLabName.Location = new System.Drawing.Point(278, 5);
            this.cmbLabName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbLabName.Name = "cmbLabName";
            this.cmbLabName.Size = new System.Drawing.Size(252, 25);
            this.cmbLabName.TabIndex = 0;
            // 
            // cmbTypeName
            // 
            this.cmbTypeName.BackColor = System.Drawing.Color.White;
            this.cmbTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeName.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbTypeName.FormattingEnabled = true;
            this.cmbTypeName.Location = new System.Drawing.Point(278, 52);
            this.cmbTypeName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTypeName.Name = "cmbTypeName";
            this.cmbTypeName.Size = new System.Drawing.Size(252, 25);
            this.cmbTypeName.TabIndex = 1;
            // 
            // txtLOT_NO
            // 
            this.txtLOT_NO.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtLOT_NO.Location = new System.Drawing.Point(278, 145);
            this.txtLOT_NO.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLOT_NO.Name = "txtLOT_NO";
            this.txtLOT_NO.Size = new System.Drawing.Size(252, 23);
            this.txtLOT_NO.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this._Result, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._Parameter, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 242F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1047, 662);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1033, 383);
            this.gridControl1.TabIndex = 187;
            this.gridControl1.UseEmbeddedNavigator = true;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.gridView2});
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
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl1;
            this.gridView2.Name = "gridView2";
            // 
            // _Result
            // 
            this._Result.BackColor = System.Drawing.Color.Transparent;
            this._Result.CanvasColor = System.Drawing.SystemColors.Control;
            this._Result.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Result.Controls.Add(this.gridControl1);
            this._Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Result.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Result.Location = new System.Drawing.Point(4, 247);
            this._Result.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._Result.Name = "_Result";
            this._Result.Size = new System.Drawing.Size(1039, 410);
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
            // MT01View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MT01View";
            this.Size = new System.Drawing.Size(1047, 662);
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this._Result.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        private System.Windows.Forms.TextBox txtLOT_NO;
        private System.Windows.Forms.ComboBox cmbTypeName;
        private System.Windows.Forms.ComboBox cmbLabName;
        public System.Windows.Forms.ComboBox cmbSample;
        public System.Windows.Forms.Panel panel4;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblLOTTONo;
        private System.Windows.Forms.Label lblSampleName;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.Label lblLabName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel _Result;
        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;

    }
}
