namespace LIMS.Views
{
    partial class QC21Dialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QC21Dialog));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbReason = new System.Windows.Forms.Label();
            this.btNO = new DevExpress.XtraEditors.SimpleButton();
            this.btYes = new DevExpress.XtraEditors.SimpleButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtRemark = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(32, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 280;
            this.pictureBox1.TabStop = false;
            // 
            // lbReason
            // 
            this.lbReason.AutoSize = true;
            this.lbReason.Font = new System.Drawing.Font("Georgia", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReason.ForeColor = System.Drawing.Color.Black;
            this.lbReason.Location = new System.Drawing.Point(54, 51);
            this.lbReason.Name = "lbReason";
            this.lbReason.Size = new System.Drawing.Size(230, 18);
            this.lbReason.TabIndex = 281;
            this.lbReason.Text = "Please enter the information:";
            // 
            // btNO
            // 
            this.btNO.Location = new System.Drawing.Point(303, 178);
            this.btNO.Name = "btNO";
            this.btNO.Size = new System.Drawing.Size(67, 25);
            this.btNO.TabIndex = 8;
            this.btNO.Text = "Cancel";
            // 
            // btYes
            // 
            this.btYes.Location = new System.Drawing.Point(237, 178);
            this.btYes.Name = "btYes";
            this.btYes.Size = new System.Drawing.Size(60, 25);
            this.btYes.TabIndex = 7;
            this.btYes.Text = "Confirm";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtRemark
            // 
            this.txtRemark.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtRemark.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtRemark.Border.Class = "RibbonGalleryContainer";
            this.txtRemark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtRemark.Border.PaddingTop = 2;
            this.txtRemark.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtRemark.Location = new System.Drawing.Point(44, 83);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(326, 89);
            this.txtRemark.TabIndex = 303;
            this.txtRemark.WatermarkColor = System.Drawing.Color.Silver;
            this.txtRemark.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // QC21Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.ClientSize = new System.Drawing.Size(400, 219);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.btNO);
            this.Controls.Add(this.btYes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbReason);
            this.MinimumSize = new System.Drawing.Size(400, 180);
            this.Name = "QC21Dialog";
            this.Text = "FEPV IT";
            this.Controls.SetChildIndex(this.lbReason, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.btYes, 0);
            this.Controls.SetChildIndex(this.btNO, 0);
            this.Controls.SetChildIndex(this.txtRemark, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbReason;
        private DevExpress.XtraEditors.SimpleButton btNO;
        private DevExpress.XtraEditors.SimpleButton btYes;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtRemark;
    }
}