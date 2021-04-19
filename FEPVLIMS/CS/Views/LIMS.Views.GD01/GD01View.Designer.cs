using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace LIMS.Views
{
    partial class GD01View
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GD01View));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._Result = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkIsALL = new DevExpress.XtraEditors.CheckEdit();
            this.cmbMaterial = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbSampleName = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbTypeName = new System.Windows.Forms.ComboBox();
            this.cmbLabName = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.lblSampleName = new System.Windows.Forms.Label();
            this.lblLabName = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this._Result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this._Parameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsALL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMaterial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).BeginInit();
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(846, 513);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _Result
            // 
            this._Result.BackColor = System.Drawing.Color.Transparent;
            this._Result.CanvasColor = System.Drawing.SystemColors.Control;
            this._Result.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Result.Controls.Add(this.gridControl1);
            this._Result.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Result.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Result.Location = new System.Drawing.Point(3, 183);
            this._Result.Name = "_Result";
            this._Result.Size = new System.Drawing.Size(840, 327);
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
            this.gridControl1.Size = new System.Drawing.Size(834, 305);
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
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.chkIsALL);
            this._Parameter.Controls.Add(this.cmbMaterial);
            this._Parameter.Controls.Add(this.cmbSampleName);
            this._Parameter.Controls.Add(this.cmbTypeName);
            this._Parameter.Controls.Add(this.cmbLabName);
            this._Parameter.Controls.Add(this.panel6);
            this._Parameter.Controls.Add(this.panel5);
            this._Parameter.Controls.Add(this.panel2);
            this._Parameter.Controls.Add(this.panel1);
            this._Parameter.Controls.Add(this.lblTypeName);
            this._Parameter.Controls.Add(this.lblMaterial);
            this._Parameter.Controls.Add(this.lblSampleName);
            this._Parameter.Controls.Add(this.lblLabName);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(3, 3);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(840, 174);
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
            // chkIsALL
            // 
            this.chkIsALL.Location = new System.Drawing.Point(191, 122);
            this.chkIsALL.Name = "chkIsALL";
            this.chkIsALL.Properties.Appearance.Font = new System.Drawing.Font("Georgia", 12F);
            this.chkIsALL.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.chkIsALL.Properties.Appearance.Options.UseFont = true;
            this.chkIsALL.Properties.Appearance.Options.UseForeColor = true;
            this.chkIsALL.Properties.Caption = "IsALL";
            this.chkIsALL.Size = new System.Drawing.Size(192, 23);
            this.chkIsALL.TabIndex = 414;
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.Location = new System.Drawing.Point(193, 92);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbMaterial.Properties.Appearance.Options.UseFont = true;
            this.cmbMaterial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMaterial.Properties.NullText = "";
            this.cmbMaterial.Properties.PopupWidth = 450;
            this.cmbMaterial.Properties.ShowFooter = false;
            this.cmbMaterial.Size = new System.Drawing.Size(190, 24);
            this.cmbMaterial.TabIndex = 413;
            // 
            // cmbSampleName
            // 
            this.cmbSampleName.Location = new System.Drawing.Point(193, 64);
            this.cmbSampleName.Name = "cmbSampleName";
            this.cmbSampleName.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSampleName.Properties.Appearance.Options.UseFont = true;
            this.cmbSampleName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbSampleName.Properties.NullText = "";
            this.cmbSampleName.Properties.PopupWidth = 450;
            this.cmbSampleName.Properties.ShowFooter = false;
            this.cmbSampleName.Size = new System.Drawing.Size(190, 24);
            this.cmbSampleName.TabIndex = 412;
            // 
            // cmbTypeName
            // 
            this.cmbTypeName.BackColor = System.Drawing.Color.White;
            this.cmbTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeName.FormattingEnabled = true;
            this.cmbTypeName.Location = new System.Drawing.Point(193, 33);
            this.cmbTypeName.Name = "cmbTypeName";
            this.cmbTypeName.Size = new System.Drawing.Size(190, 25);
            this.cmbTypeName.TabIndex = 322;
            // 
            // cmbLabName
            // 
            this.cmbLabName.BackColor = System.Drawing.Color.White;
            this.cmbLabName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLabName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLabName.FormattingEnabled = true;
            this.cmbLabName.Location = new System.Drawing.Point(193, 4);
            this.cmbLabName.Name = "cmbLabName";
            this.cmbLabName.Size = new System.Drawing.Size(190, 25);
            this.cmbLabName.TabIndex = 188;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.Window;
            this.panel6.Location = new System.Drawing.Point(36, 114);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(170, 1);
            this.panel6.TabIndex = 363;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Location = new System.Drawing.Point(32, 55);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(170, 1);
            this.panel5.TabIndex = 363;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(32, 85);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 1);
            this.panel2.TabIndex = 363;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Location = new System.Drawing.Point(32, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 1);
            this.panel1.TabIndex = 363;
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblTypeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTypeName.Location = new System.Drawing.Point(29, 38);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(87, 18);
            this.lblTypeName.TabIndex = 321;
            this.lblTypeName.Text = "TypeName";
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.BackColor = System.Drawing.Color.Transparent;
            this.lblMaterial.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblMaterial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMaterial.Location = new System.Drawing.Point(29, 97);
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
            this.lblSampleName.Location = new System.Drawing.Point(29, 67);
            this.lblSampleName.Name = "lblSampleName";
            this.lblSampleName.Size = new System.Drawing.Size(102, 18);
            this.lblSampleName.TabIndex = 1;
            this.lblSampleName.Text = "SampleName";
            // 
            // lblLabName
            // 
            this.lblLabName.AutoSize = true;
            this.lblLabName.BackColor = System.Drawing.Color.Transparent;
            this.lblLabName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblLabName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLabName.Location = new System.Drawing.Point(29, 8);
            this.lblLabName.Name = "lblLabName";
            this.lblLabName.Size = new System.Drawing.Size(77, 18);
            this.lblLabName.TabIndex = 0;
            this.lblLabName.Text = "LabName";
            // 
            // GD01View
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GD01View";
            this.Size = new System.Drawing.Size(846, 513);
            this.tableLayoutPanel1.ResumeLayout(false);
            this._Result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsALL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMaterial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSampleName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private GroupPanel _Parameter;
        private GroupPanel _Result;
        public GridControl gridControl1;
        public GridView gridView1;
        private Label lblMaterial;
        private Label lblSampleName;
        private Label lblLabName;
        private ComboBox cmbLabName;
        private ComboBox cmbTypeName;
        private Label lblTypeName;
        private Panel panel6;
        private Panel panel5;
        private Panel panel2;
        private Panel panel1;
        public DevExpress.XtraEditors.LookUpEdit cmbSampleName;
        public DevExpress.XtraEditors.LookUpEdit cmbMaterial;
        private DevExpress.XtraEditors.CheckEdit chkIsALL;
    }
}
