namespace LIMS.Views
{
    partial class QC21ViewBarCode
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
            this.dateProdDate = new DevExpress.XtraEditors.DateEdit();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblTypeName = new System.Windows.Forms.Label();
            this.txtBarcode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lbKeyWord = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // dateProdDate
            // 
            this.dateProdDate.EditValue = new System.DateTime(2019, 7, 15, 0, 0, 0, 0);
            this.dateProdDate.Location = new System.Drawing.Point(208, 98);
            this.dateProdDate.Margin = new System.Windows.Forms.Padding(4);
            this.dateProdDate.Name = "dateProdDate";
            this.dateProdDate.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.dateProdDate.Properties.Appearance.Options.UseFont = true;
            this.dateProdDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateProdDate.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.dateProdDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateProdDate.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            this.dateProdDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dateProdDate.Properties.Mask.EditMask = "yyyy-MM-dd";
            this.dateProdDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dateProdDate.Size = new System.Drawing.Size(289, 24);
            this.dateProdDate.TabIndex = 424;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Location = new System.Drawing.Point(18, 122);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(227, 1);
            this.panel5.TabIndex = 423;
            // 
            // lblTypeName
            // 
            this.lblTypeName.AutoSize = true;
            this.lblTypeName.BackColor = System.Drawing.Color.Transparent;
            this.lblTypeName.Font = new System.Drawing.Font("Georgia", 12F);
            this.lblTypeName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTypeName.Location = new System.Drawing.Point(19, 94);
            this.lblTypeName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTypeName.Name = "lblTypeName";
            this.lblTypeName.Size = new System.Drawing.Size(133, 24);
            this.lblTypeName.TabIndex = 422;
            this.lblTypeName.Text = "Product Date:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtBarcode.Border.BackColorGradientType = DevComponents.DotNetBar.eGradientType.Radial;
            this.txtBarcode.Border.Class = "RibbonGalleryContainer";
            this.txtBarcode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.txtBarcode.Border.PaddingTop = 2;
            this.txtBarcode.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.txtBarcode.Location = new System.Drawing.Point(208, 145);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(289, 26);
            this.txtBarcode.TabIndex = 425;
            this.txtBarcode.WatermarkColor = System.Drawing.Color.Silver;
            this.txtBarcode.WatermarkFont = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            // 
            // lbKeyWord
            // 
            this.lbKeyWord.AutoSize = true;
            this.lbKeyWord.BackColor = System.Drawing.Color.Transparent;
            this.lbKeyWord.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbKeyWord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbKeyWord.Location = new System.Drawing.Point(19, 145);
            this.lbKeyWord.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbKeyWord.Name = "lbKeyWord";
            this.lbKeyWord.Size = new System.Drawing.Size(125, 30);
            this.lbKeyWord.TabIndex = 427;
            this.lbKeyWord.Text = "KeyWord:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Location = new System.Drawing.Point(43, 171);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 1);
            this.panel2.TabIndex = 426;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(338, 233);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 31);
            this.btnClose.TabIndex = 429;
            this.btnClose.Text = "Cancel";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(190, 233);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 31);
            this.btnSearch.TabIndex = 428;
            this.btnSearch.Text = "Search";
            // 
            // QC21ViewBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 333);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.lbKeyWord);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dateProdDate);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.lblTypeName);
            this.Name = "QC21ViewBarCode";
            this.Text = "Barcode Query Parameter";
            this.Controls.SetChildIndex(this.lblTypeName, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.dateProdDate, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.lbKeyWord, 0);
            this.Controls.SetChildIndex(this.txtBarcode, 0);
            this.Controls.SetChildIndex(this.btnSearch, 0);
            this.Controls.SetChildIndex(this.btnClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateProdDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.DateEdit dateProdDate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblTypeName;
        public DevComponents.DotNetBar.Controls.TextBoxX txtBarcode;
        private System.Windows.Forms.Label lbKeyWord;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}
