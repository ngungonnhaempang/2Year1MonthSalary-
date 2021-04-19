namespace LIMS.Views.QC07
{
    partial class SerchPlanView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SerchPlanView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gcList = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this._ParameterPanel = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cbMySelf = new System.Windows.Forms.CheckBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.cmbTypeName = new System.Windows.Forms.ComboBox();
            this.cmbSampleName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEBLEN = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVBELN = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.deVoutd = new DevExpress.XtraEditors.DateEdit();
            this.deVoufd = new DevExpress.XtraEditors.DateEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this._ParameterPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEBLEN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVBELN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoutd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoutd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoufd.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoufd.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gcList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this._ParameterPanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 194F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(684, 419);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gcList
            // 
            this.gcList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcList.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcList.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcList.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcList.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcList.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcList.Location = new System.Drawing.Point(3, 197);
            this.gcList.MainView = this.gridView1;
            this.gcList.Name = "gcList";
            this.gcList.Size = new System.Drawing.Size(678, 219);
            this.gcList.TabIndex = 201;
            this.gcList.UseEmbeddedNavigator = true;
            this.gcList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView1.GridControl = this.gcList;
            this.gridView1.GroupFormat = "[#image]{1} {2}";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // _ParameterPanel
            // 
            this._ParameterPanel.BackColor = System.Drawing.Color.Transparent;
            this._ParameterPanel.CanvasColor = System.Drawing.SystemColors.Control;
            this._ParameterPanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._ParameterPanel.Controls.Add(this.cbMySelf);
            this._ParameterPanel.Controls.Add(this.chkAll);
            this._ParameterPanel.Controls.Add(this.lblTypeName);
            this._ParameterPanel.Controls.Add(this.cmbTypeName);
            this._ParameterPanel.Controls.Add(this.cmbSampleName);
            this._ParameterPanel.Controls.Add(this.label5);
            this._ParameterPanel.Controls.Add(this.txtEBLEN);
            this._ParameterPanel.Controls.Add(this.label4);
            this._ParameterPanel.Controls.Add(this.txtVBELN);
            this._ParameterPanel.Controls.Add(this.label6);
            this._ParameterPanel.Controls.Add(this.deVoutd);
            this._ParameterPanel.Controls.Add(this.deVoufd);
            this._ParameterPanel.Controls.Add(this.label2);
            this._ParameterPanel.Controls.Add(this.label1);
            this._ParameterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ParameterPanel.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._ParameterPanel.Location = new System.Drawing.Point(3, 3);
            this._ParameterPanel.Name = "_ParameterPanel";
            this._ParameterPanel.Size = new System.Drawing.Size(678, 188);
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
            this._ParameterPanel.TabIndex = 200;
            this._ParameterPanel.Text = "[Parameters]";
            this._ParameterPanel.TitleImage = ((System.Drawing.Image)(resources.GetObject("_ParameterPanel.TitleImage")));
            // 
            // cbMySelf
            // 
            this.cbMySelf.AutoSize = true;
            this.cbMySelf.ForeColor = System.Drawing.Color.DodgerBlue;
            this.cbMySelf.Location = new System.Drawing.Point(141, 132);
            this.cbMySelf.Name = "cbMySelf";
            this.cbMySelf.Size = new System.Drawing.Size(98, 18);
            this.cbMySelf.TabIndex = 306;
            this.cbMySelf.Text = "查询自己申请";
            this.cbMySelf.UseVisualStyleBackColor = true;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.ForeColor = System.Drawing.Color.DodgerBlue;
            this.chkAll.Location = new System.Drawing.Point(337, 132);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(43, 18);
            this.chkAll.TabIndex = 305;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblTypeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTypeName.Location = new System.Drawing.Point(17, 78);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(96, 18);
            this.lblTypeName.TabIndex = 209;
            this.lblTypeName.Text = "Type Name:";
            // 
            // cmbTypeName
            // 
            this.cmbTypeName.BackColor = System.Drawing.Color.White;
            this.cmbTypeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypeName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbTypeName.FormattingEnabled = true;
            this.cmbTypeName.Location = new System.Drawing.Point(141, 73);
            this.cmbTypeName.Name = "cmbTypeName";
            this.cmbTypeName.Size = new System.Drawing.Size(172, 25);
            this.cmbTypeName.TabIndex = 208;
            // 
            // cmbSampleName
            // 
            this.cmbSampleName.BackColor = System.Drawing.Color.White;
            this.cmbSampleName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbSampleName.FormattingEnabled = true;
            this.cmbSampleName.Location = new System.Drawing.Point(141, 101);
            this.cmbSampleName.Name = "cmbSampleName";
            this.cmbSampleName.Size = new System.Drawing.Size(172, 25);
            this.cmbSampleName.TabIndex = 70;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 18);
            this.label5.TabIndex = 69;
            this.label5.Text = "laEBLEN:";
            // 
            // txtEBLEN
            // 
            this.txtEBLEN.Location = new System.Drawing.Point(141, 49);
            this.txtEBLEN.Name = "txtEBLEN";
            this.txtEBLEN.Size = new System.Drawing.Size(172, 20);
            this.txtEBLEN.TabIndex = 67;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 18);
            this.label4.TabIndex = 64;
            this.label4.Text = "SampleName:";
            // 
            // txtVBELN
            // 
            this.txtVBELN.Location = new System.Drawing.Point(141, 26);
            this.txtVBELN.Name = "txtVBELN";
            this.txtVBELN.Size = new System.Drawing.Size(172, 20);
            this.txtVBELN.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 18);
            this.label6.TabIndex = 60;
            this.label6.Text = "PO No:";
            // 
            // deVoutd
            // 
            this.deVoutd.EditValue = null;
            this.deVoutd.Location = new System.Drawing.Point(387, 3);
            this.deVoutd.Name = "deVoutd";
            this.deVoutd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deVoutd.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deVoutd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deVoutd.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deVoutd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deVoutd.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.deVoutd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.deVoutd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deVoutd.Size = new System.Drawing.Size(202, 20);
            this.deVoutd.TabIndex = 59;
            // 
            // deVoufd
            // 
            this.deVoufd.EditValue = null;
            this.deVoufd.Location = new System.Drawing.Point(141, 3);
            this.deVoufd.Name = "deVoufd";
            this.deVoufd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deVoufd.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.deVoufd.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deVoufd.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.deVoufd.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.deVoufd.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.deVoufd.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deVoufd.Size = new System.Drawing.Size(172, 20);
            this.deVoufd.TabIndex = 58;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(350, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 18);
            this.label2.TabIndex = 57;
            this.label2.Text = "To:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 18);
            this.label1.TabIndex = 56;
            this.label1.Text = "Date From:";
            // 
            // SerchPlanView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SerchPlanView";
            this.Size = new System.Drawing.Size(684, 419);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this._ParameterPanel.ResumeLayout(false);
            this._ParameterPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtEBLEN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVBELN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoutd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoutd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoufd.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deVoufd.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel _ParameterPanel;
        private DevExpress.XtraEditors.TextEdit txtEBLEN;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.DateEdit deVoutd;
        private DevExpress.XtraEditors.DateEdit deVoufd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        public DevExpress.XtraGrid.GridControl gcList;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtVBELN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbSampleName;
        private System.Windows.Forms.Label lblTypeName;
        private System.Windows.Forms.ComboBox cmbTypeName;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox cbMySelf;
    }
}
