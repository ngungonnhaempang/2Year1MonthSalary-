namespace LIMS.Views
{
    partial class SPC1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SPC1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearch = new DevComponents.DotNetBar.ButtonItem();
            this.btAdd = new DevComponents.DotNetBar.ButtonItem();
            this.btDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btIn = new DevComponents.DotNetBar.ButtonItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.Workspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.txtMsg = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Workspace, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMsg, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(679, 473);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearch,
            this.btAdd,
            this.btDelete,
            this.btIn,
            this.btReturn});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(673, 26);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 21;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearch
            // 
            this.btSearch.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearch.Image = ((System.Drawing.Image)(resources.GetObject("btSearch.Image")));
            this.btSearch.ImagePaddingHorizontal = 8;
            this.btSearch.Name = "btSearch";
            this.btSearch.Text = "Search";
            // 
            // btAdd
            // 
            this.btAdd.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAdd.Image = ((System.Drawing.Image)(resources.GetObject("btAdd.Image")));
            this.btAdd.ImagePaddingHorizontal = 8;
            this.btAdd.Name = "btAdd";
            this.btAdd.Text = "Add";
            // 
            // btDelete
            // 
            this.btDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImagePaddingHorizontal = 8;
            this.btDelete.Name = "btDelete";
            this.btDelete.Text = "Delete";
            // 
            // btIn
            // 
            this.btIn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btIn.Image = ((System.Drawing.Image)(resources.GetObject("btIn.Image")));
            this.btIn.ImagePaddingHorizontal = 8;
            this.btIn.Name = "btIn";
            this.btIn.Text = "Import From Excel";
            // 
            // btReturn
            // 
            this.btReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReturn.Image = ((System.Drawing.Image)(resources.GetObject("btReturn.Image")));
            this.btReturn.ImagePaddingHorizontal = 8;
            this.btReturn.Name = "btReturn";
            this.btReturn.Text = "Exit";
            // 
            // Workspace
            // 
            this.Workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Workspace.Location = new System.Drawing.Point(3, 33);
            this.Workspace.Name = "Workspace";
            this.Workspace.Size = new System.Drawing.Size(673, 417);
            this.Workspace.TabIndex = 22;
            this.Workspace.Text = "deckWorkspace1";
            // 
            // txtMsg
            // 
            this.txtMsg.AutoSize = true;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg.Location = new System.Drawing.Point(3, 453);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(15, 16);
            this.txtMsg.TabIndex = 0;
            this.txtMsg.Text = ":)";
            // 
            // SPC1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 473);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SPC1";
            this.Text = "SPC1属性维护";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btSearch;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        public DevComponents.DotNetBar.ButtonItem btIn;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace Workspace;
        private DevComponents.DotNetBar.ButtonItem btAdd;
        private DevComponents.DotNetBar.ButtonItem btDelete;
    }
}