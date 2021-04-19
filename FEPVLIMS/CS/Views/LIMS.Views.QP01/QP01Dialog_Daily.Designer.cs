namespace LIMS.Views
{
    partial class QP01Dialog_Daily
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QP01Dialog_Daily));
            this.seDays = new DevExpress.XtraEditors.SpinEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            ((System.ComponentModel.ISupportInitialize)(this.seDays.Properties)).BeginInit();
            this._Parameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // seDays
            // 
            this.seDays.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seDays.Location = new System.Drawing.Point(150, 19);
            this.seDays.Name = "seDays";
            this.seDays.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.seDays.Properties.Appearance.Options.UseFont = true;
            this.seDays.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seDays.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seDays.Properties.Mask.EditMask = "n0";
            this.seDays.Properties.ReadOnly = true;
            this.seDays.Size = new System.Drawing.Size(169, 26);
            this.seDays.TabIndex = 461;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 18);
            this.label1.TabIndex = 460;
            this.label1.Text = "Recur every:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label4.Location = new System.Drawing.Point(351, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 18);
            this.label4.TabIndex = 462;
            this.label4.Text = "Days";
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.seDays);
            this._Parameter.Controls.Add(this.label4);
            this._Parameter.Controls.Add(this.label1);
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
            this._Parameter.TabIndex = 467;
            this._Parameter.Text = "Parameter";
            this._Parameter.TitleImage = ((System.Drawing.Image)(resources.GetObject("_Parameter.TitleImage")));
            // 
            // QP01Dialog_Daily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Parameter);
            this.Name = "QP01Dialog_Daily";
            this.Size = new System.Drawing.Size(506, 92);
            ((System.ComponentModel.ISupportInitialize)(this.seDays.Properties)).EndInit();
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SpinEdit seDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;

    }
}
