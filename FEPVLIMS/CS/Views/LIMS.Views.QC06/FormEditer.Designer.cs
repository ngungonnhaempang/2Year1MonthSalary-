namespace LIMS.Views
{
    partial class FormEditer
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
            this.btSearch = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btCancle = new System.Windows.Forms.Button();
            this.cmbCreateType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btSearch
            // 
            this.btSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.btSearch.Location = new System.Drawing.Point(70, 157);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(95, 29);
            this.btSearch.TabIndex = 235;
            this.btSearch.Text = "确  定";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Location = new System.Drawing.Point(70, 102);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(105, 1);
            this.panel5.TabIndex = 234;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(68, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 232;
            this.label5.Text = "单据类型";
            // 
            // btCancle
            // 
            this.btCancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.btCancle.Location = new System.Drawing.Point(213, 157);
            this.btCancle.Name = "btCancle";
            this.btCancle.Size = new System.Drawing.Size(93, 29);
            this.btCancle.TabIndex = 236;
            this.btCancle.Text = "取  消";
            this.btCancle.UseVisualStyleBackColor = true;
            this.btCancle.Click += new System.EventHandler(this.btCancle_Click);
            // 
            // cmbCreateType
            // 
            this.cmbCreateType.BackColor = System.Drawing.Color.White;
            this.cmbCreateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCreateType.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.cmbCreateType.ForeColor = System.Drawing.Color.Black;
            this.cmbCreateType.FormattingEnabled = true;
            this.cmbCreateType.Location = new System.Drawing.Point(158, 78);
            this.cmbCreateType.Name = "cmbCreateType";
            this.cmbCreateType.Size = new System.Drawing.Size(148, 25);
            this.cmbCreateType.TabIndex = 294;
            // 
            // FormEditer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 223);
            this.Controls.Add(this.cmbCreateType);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btCancle);
            this.Controls.Add(this.panel5);
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "FormEditer";
            this.Text = "PlanType";
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.btCancle, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.btSearch, 0);
            this.Controls.SetChildIndex(this.cmbCreateType, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btCancle;
        private System.Windows.Forms.ComboBox cmbCreateType;

    }
}