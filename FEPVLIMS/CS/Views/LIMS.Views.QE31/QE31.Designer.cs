namespace LIMS.Views
{
    partial class QE31
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QE31));
            this.buttonItem34 = new DevComponents.DotNetBar.ButtonItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearchPlan = new DevComponents.DotNetBar.ButtonItem();
            this.btEditMaterial = new DevComponents.DotNetBar.ButtonItem();
            this.btRecFileUpload = new DevComponents.DotNetBar.ButtonItem();
            this.btnDeleteFile = new DevComponents.DotNetBar.ButtonItem();
            this.btnUploadResult = new DevComponents.DotNetBar.ButtonItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.deckWorkspace1 = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonItem34
            // 
            this.buttonItem34.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem34.ImagePaddingHorizontal = 8;
            this.buttonItem34.Name = "buttonItem34";
            this.buttonItem34.Text = "Undo";
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearchPlan,
            this.btEditMaterial,
            this.btRecFileUpload,
            this.btnDeleteFile,
            this.btnUploadResult,
            this.btReturn});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(634, 24);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 24;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearchPlan
            // 
            this.btSearchPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchPlan.Image = ((System.Drawing.Image)(resources.GetObject("btSearchPlan.Image")));
            this.btSearchPlan.ImagePaddingHorizontal = 8;
            this.btSearchPlan.Name = "btSearchPlan";
            this.btSearchPlan.Text = "Search";
            // 
            // btEditMaterial
            // 
            this.btEditMaterial.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btEditMaterial.Image = ((System.Drawing.Image)(resources.GetObject("btEditMaterial.Image")));
            this.btEditMaterial.ImagePaddingHorizontal = 8;
            this.btEditMaterial.Name = "btEditMaterial";
            this.btEditMaterial.Text = "Edit Material";
            // 
            // btRecFileUpload
            // 
            this.btRecFileUpload.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRecFileUpload.Image = ((System.Drawing.Image)(resources.GetObject("btRecFileUpload.Image")));
            this.btRecFileUpload.ImagePaddingHorizontal = 8;
            this.btRecFileUpload.Name = "btRecFileUpload";
            this.btRecFileUpload.Text = "Upload File";
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnDeleteFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteFile.Image")));
            this.btnDeleteFile.ImagePaddingHorizontal = 8;
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Text = "Delete Uploaded File";
            // 
            // btnUploadResult
            // 
            this.btnUploadResult.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnUploadResult.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadResult.Image")));
            this.btnUploadResult.ImagePaddingHorizontal = 8;
            this.btnUploadResult.Name = "btnUploadResult";
            this.btnUploadResult.Text = "Quick Upload Color";
            // 
            // btReturn
            // 
            this.btReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReturn.Image = ((System.Drawing.Image)(resources.GetObject("btReturn.Image")));
            this.btReturn.ImagePaddingHorizontal = 8;
            this.btReturn.Name = "btReturn";
            this.btReturn.Text = "Exit";
            // 
            // deckWorkspace1
            // 
            this.deckWorkspace1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deckWorkspace1.Location = new System.Drawing.Point(0, 24);
            this.deckWorkspace1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.deckWorkspace1.Name = "deckWorkspace1";
            this.deckWorkspace1.Size = new System.Drawing.Size(634, 286);
            this.deckWorkspace1.TabIndex = 25;
            this.deckWorkspace1.Text = "deckWorkspace1";
            // 
            // QE31
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 310);
            this.Controls.Add(this.deckWorkspace1);
            this.Controls.Add(this.bar1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "QE31";
            this.Text = "QE31结果录入";
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.ButtonItem buttonItem34;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btSearchPlan;
        private DevComponents.DotNetBar.ButtonItem btEditMaterial;
        private DevComponents.DotNetBar.ButtonItem btRecFileUpload;
        private DevComponents.DotNetBar.ButtonItem btnUploadResult;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace deckWorkspace1;
		private DevComponents.DotNetBar.ButtonItem btnDeleteFile;
	}
}