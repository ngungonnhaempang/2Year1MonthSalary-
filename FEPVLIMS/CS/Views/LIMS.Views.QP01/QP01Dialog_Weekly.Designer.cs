namespace LIMS.Views
{
    partial class QP01Dialog_Weekly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QP01Dialog_Weekly));
            this._Parameter = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.seWeeks = new DevExpress.XtraEditors.SpinEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMonday = new System.Windows.Forms.CheckBox();
            this.cbSaturday = new System.Windows.Forms.CheckBox();
            this.cbSunday = new System.Windows.Forms.CheckBox();
            this.cbFriday = new System.Windows.Forms.CheckBox();
            this.cbTuesday = new System.Windows.Forms.CheckBox();
            this.cbThursday = new System.Windows.Forms.CheckBox();
            this.cbWednesday = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this._Parameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seWeeks.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // _Parameter
            // 
            this._Parameter.BackColor = System.Drawing.Color.Transparent;
            this._Parameter.CanvasColor = System.Drawing.SystemColors.Control;
            this._Parameter.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this._Parameter.Controls.Add(this.seWeeks);
            this._Parameter.Controls.Add(this.label4);
            this._Parameter.Controls.Add(this.cbMonday);
            this._Parameter.Controls.Add(this.cbSaturday);
            this._Parameter.Controls.Add(this.cbSunday);
            this._Parameter.Controls.Add(this.cbFriday);
            this._Parameter.Controls.Add(this.cbTuesday);
            this._Parameter.Controls.Add(this.cbThursday);
            this._Parameter.Controls.Add(this.cbWednesday);
            this._Parameter.Controls.Add(this.label3);
            this._Parameter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._Parameter.Font = new System.Drawing.Font("Georgia", 8.25F);
            this._Parameter.Location = new System.Drawing.Point(0, 0);
            this._Parameter.Name = "_Parameter";
            this._Parameter.Size = new System.Drawing.Size(506, 116);
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
            // seWeeks
            // 
            this.seWeeks.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.seWeeks.Location = new System.Drawing.Point(155, 1);
            this.seWeeks.Name = "seWeeks";
            this.seWeeks.Properties.Appearance.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F);
            this.seWeeks.Properties.Appearance.Options.UseFont = true;
            this.seWeeks.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seWeeks.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.seWeeks.Properties.Mask.EditMask = "n0";
            this.seWeeks.Size = new System.Drawing.Size(169, 26);
            this.seWeeks.TabIndex = 478;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label4.Location = new System.Drawing.Point(341, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 18);
            this.label4.TabIndex = 479;
            this.label4.Text = "Week(s) on";
            // 
            // cbMonday
            // 
            this.cbMonday.AutoSize = true;
            this.cbMonday.Location = new System.Drawing.Point(66, 36);
            this.cbMonday.Name = "cbMonday";
            this.cbMonday.Size = new System.Drawing.Size(72, 18);
            this.cbMonday.TabIndex = 477;
            this.cbMonday.Text = "Monday";
            this.cbMonday.UseVisualStyleBackColor = true;
            // 
            // cbSaturday
            // 
            this.cbSaturday.AutoSize = true;
            this.cbSaturday.Location = new System.Drawing.Point(264, 65);
            this.cbSaturday.Name = "cbSaturday";
            this.cbSaturday.Size = new System.Drawing.Size(81, 18);
            this.cbSaturday.TabIndex = 476;
            this.cbSaturday.Text = "Saturday";
            this.cbSaturday.UseVisualStyleBackColor = true;
            // 
            // cbSunday
            // 
            this.cbSunday.AutoSize = true;
            this.cbSunday.Location = new System.Drawing.Point(366, 36);
            this.cbSunday.Name = "cbSunday";
            this.cbSunday.Size = new System.Drawing.Size(71, 18);
            this.cbSunday.TabIndex = 476;
            this.cbSunday.Text = "Sunday";
            this.cbSunday.UseVisualStyleBackColor = true;
            // 
            // cbFriday
            // 
            this.cbFriday.AutoSize = true;
            this.cbFriday.Location = new System.Drawing.Point(264, 36);
            this.cbFriday.Name = "cbFriday";
            this.cbFriday.Size = new System.Drawing.Size(65, 18);
            this.cbFriday.TabIndex = 476;
            this.cbFriday.Text = "Friday";
            this.cbFriday.UseVisualStyleBackColor = true;
            // 
            // cbTuesday
            // 
            this.cbTuesday.AutoSize = true;
            this.cbTuesday.Location = new System.Drawing.Point(66, 65);
            this.cbTuesday.Name = "cbTuesday";
            this.cbTuesday.Size = new System.Drawing.Size(75, 18);
            this.cbTuesday.TabIndex = 476;
            this.cbTuesday.Text = "Tuesday";
            this.cbTuesday.UseVisualStyleBackColor = true;
            // 
            // cbThursday
            // 
            this.cbThursday.AutoSize = true;
            this.cbThursday.Location = new System.Drawing.Point(155, 65);
            this.cbThursday.Name = "cbThursday";
            this.cbThursday.Size = new System.Drawing.Size(83, 18);
            this.cbThursday.TabIndex = 476;
            this.cbThursday.Text = "Thursday";
            this.cbThursday.UseVisualStyleBackColor = true;
            // 
            // cbWednesday
            // 
            this.cbWednesday.AutoSize = true;
            this.cbWednesday.Location = new System.Drawing.Point(155, 36);
            this.cbWednesday.Name = "cbWednesday";
            this.cbWednesday.Size = new System.Drawing.Size(92, 18);
            this.cbWednesday.TabIndex = 476;
            this.cbWednesday.Text = "Wednesday";
            this.cbWednesday.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Georgia", 11.25F);
            this.label3.Location = new System.Drawing.Point(31, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 18);
            this.label3.TabIndex = 473;
            this.label3.Text = "Recurs Every:";
            // 
            // QP01Dialog_Weekly
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Parameter);
            this.Name = "QP01Dialog_Weekly";
            this.Size = new System.Drawing.Size(506, 116);
            this._Parameter.ResumeLayout(false);
            this._Parameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seWeeks.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel _Parameter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbSaturday;
        private System.Windows.Forms.CheckBox cbSunday;
        private System.Windows.Forms.CheckBox cbFriday;
        private System.Windows.Forms.CheckBox cbTuesday;
        private System.Windows.Forms.CheckBox cbThursday;
        private System.Windows.Forms.CheckBox cbWednesday;
        private System.Windows.Forms.CheckBox cbMonday;
        public DevExpress.XtraEditors.SpinEdit seWeeks;
        private System.Windows.Forms.Label label4;


    }
}
