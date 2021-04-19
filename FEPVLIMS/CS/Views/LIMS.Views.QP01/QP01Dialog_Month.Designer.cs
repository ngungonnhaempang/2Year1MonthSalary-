namespace LIMS.Views
{
    partial class QP01Dialog_Month
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QP01Dialog_Month));
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.seMonths = new DevExpress.XtraEditors.SpinEdit();
            this.seMonthlyDay = new DevExpress.XtraEditors.SpinEdit();
            this.label3 = new System.Windows.Forms.Label();
            this._Parameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seMonths.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMonthlyDay.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.label3);
            this._Parameter.Controls.Add(this.label2);
            this._Parameter.Controls.Add(this.label1);
            this._Parameter.Controls.Add(this.seMonths);
            this._Parameter.Controls.Add(this.seMonthlyDay);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(0, 0);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(506, 92);
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
            this._Parameter.TabIndex = 466;
            this._Parameter.Text = "[Parameter]";
            this._Parameter.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Parameter.TitleImage")));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label2.Location = new System.Drawing.Point(278, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 18);
            this.label2.TabIndex = 465;
            this.label2.Text = "month(s)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label1.Location = new System.Drawing.Point(136, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 464;
            this.label1.Text = "day of";
            // 
            // seMonths
            // 
            this.seMonths.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seMonths.Location = new System.Drawing.Point(205, 18);
            this.seMonths.Name = "seMonths";
            this.seMonths.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.seMonths.Properties.Appearance.Options.UseFont = true;
            this.seMonths.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seMonths.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seMonths.Properties.Mask.EditMask = "n0";
            this.seMonths.Properties.MaxValue = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.seMonths.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seMonths.Size = new System.Drawing.Size(72, 26);
            this.seMonths.TabIndex = 463;
            // 
            // seMonthlyDay
            // 
            this.seMonthlyDay.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seMonthlyDay.Location = new System.Drawing.Point(59, 18);
            this.seMonthlyDay.Name = "seMonthlyDay";
            this.seMonthlyDay.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.seMonthlyDay.Properties.Appearance.Options.UseFont = true;
            this.seMonthlyDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seMonthlyDay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seMonthlyDay.Properties.Mask.EditMask = "n0";
            this.seMonthlyDay.Properties.MaxValue = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.seMonthlyDay.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seMonthlyDay.Size = new System.Drawing.Size(72, 26);
            this.seMonthlyDay.TabIndex = 462;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label3.Location = new System.Drawing.Point(23, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 18);
            this.label3.TabIndex = 466;
            this.label3.Text = "The";
            // 
            // QP01Dialog_Month
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Parameter);
            this.Name = "QP01Dialog_Month";
            this.Size = new System.Drawing.Size(506, 92);
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seMonths.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seMonthlyDay.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        public DevExpress.XtraEditors.SpinEdit seMonths;
        public DevExpress.XtraEditors.SpinEdit seMonthlyDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;


    }
}
