namespace LIMS.Views
{
    partial class QA23
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QA23));
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.btDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btAddPlan = new DevComponents.DotNetBar.ButtonItem();
            this.btnAcceptDraft = new DevComponents.DotNetBar.ButtonItem();
            this.btSearchPlan = new DevComponents.DotNetBar.ButtonItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Workspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.bar2 = new DevComponents.DotNetBar.Bar();
            this.btSearch = new DevComponents.DotNetBar.ButtonItem();
            this.btnADraft = new DevComponents.DotNetBar.ButtonItem();
            this.btnAcceptVoucher = new DevComponents.DotNetBar.ButtonItem();
            this.btnRejectDraft = new DevComponents.DotNetBar.ButtonItem();
            this.btnRejectVoucher = new DevComponents.DotNetBar.ButtonItem();
            this.txtMsg = new System.Windows.Forms.Label();
            this.btnExit = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).BeginInit();
            this.SuspendLayout();
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "POLY";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "SSP";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "POY";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "DTY";
            // 
            // btReturn
            // 
            this.btReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReturn.Image = ((System.Drawing.Image)(resources.GetObject("btReturn.Image")));
            this.btReturn.ImagePaddingHorizontal = 8;
            this.btReturn.Name = "btReturn";
            this.btReturn.Text = "Exit";
            // 
            // btDelete
            // 
            this.btDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImagePaddingHorizontal = 8;
            this.btDelete.Name = "btDelete";
            this.btDelete.Text = "RejectDraft";
            // 
            // btAddPlan
            // 
            this.btAddPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAddPlan.Image = ((System.Drawing.Image)(resources.GetObject("btAddPlan.Image")));
            this.btAddPlan.ImagePaddingHorizontal = 8;
            this.btAddPlan.Name = "btAddPlan";
            this.btAddPlan.Text = "AcceptVoucher";
            // 
            // btnAcceptDraft
            // 
            this.btnAcceptDraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAcceptDraft.Image = ((System.Drawing.Image)(resources.GetObject("btnAcceptDraft.Image")));
            this.btnAcceptDraft.ImagePaddingHorizontal = 8;
            this.btnAcceptDraft.Name = "btnAcceptDraft";
            this.btnAcceptDraft.Text = "AcceptDraft";
            // 
            // btSearchPlan
            // 
            this.btSearchPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchPlan.Image = ((System.Drawing.Image)(resources.GetObject("btSearchPlan.Image")));
            this.btSearchPlan.ImagePaddingHorizontal = 8;
            this.btSearchPlan.Name = "btSearchPlan";
            this.btSearchPlan.Text = "Search Plan";
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Top;
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(588, 24);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 20;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Workspace, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.bar2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtMsg, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1091, 629);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // Workspace
            // 
            this.Workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Workspace.Location = new System.Drawing.Point(3, 43);
            this.Workspace.Name = "Workspace";
            this.Workspace.Size = new System.Drawing.Size(1085, 561);
            this.Workspace.TabIndex = 20;
            this.Workspace.Text = "deckWorkspace1";
            // 
            // bar2
            // 
            this.bar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar2.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar2.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearch,
            this.btnADraft,
            this.btnAcceptVoucher,
            this.btnRejectDraft,
            this.btnRejectVoucher,
            this.btnExit});
            this.bar2.Location = new System.Drawing.Point(3, 3);
            this.bar2.Name = "bar2";
            this.bar2.Size = new System.Drawing.Size(1085, 32);
            this.bar2.Stretch = true;
            this.bar2.TabIndex = 19;
            this.bar2.TabStop = false;
            this.bar2.Text = "bar2";
            // 
            // btSearch
            // 
            this.btSearch.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearch.Image = ((System.Drawing.Image)(resources.GetObject("btSearch.Image")));
            this.btSearch.ImagePaddingHorizontal = 8;
            this.btSearch.Name = "btSearch";
            this.btSearch.Text = "Search";
            // 
            // btnADraft
            // 
            this.btnADraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnADraft.Image = ((System.Drawing.Image)(resources.GetObject("btnADraft.Image")));
            this.btnADraft.ImagePaddingHorizontal = 8;
            this.btnADraft.Name = "btnADraft";
            this.btnADraft.Text = "AcceptDraft";
            // 
            // btnAcceptVoucher
            // 
            this.btnAcceptVoucher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnAcceptVoucher.Image = ((System.Drawing.Image)(resources.GetObject("btnAcceptVoucher.Image")));
            this.btnAcceptVoucher.ImagePaddingHorizontal = 8;
            this.btnAcceptVoucher.Name = "btnAcceptVoucher";
            this.btnAcceptVoucher.Text = "AcceptVoucher";
            // 
            // btnRejectDraft
            // 
            this.btnRejectDraft.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRejectDraft.Image = ((System.Drawing.Image)(resources.GetObject("btnRejectDraft.Image")));
            this.btnRejectDraft.ImagePaddingHorizontal = 8;
            this.btnRejectDraft.Name = "btnRejectDraft";
            this.btnRejectDraft.Text = "RejectDraft";
            // 
            // btnRejectVoucher
            // 
            this.btnRejectVoucher.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnRejectVoucher.Image = ((System.Drawing.Image)(resources.GetObject("btnRejectVoucher.Image")));
            this.btnRejectVoucher.ImagePaddingHorizontal = 8;
            this.btnRejectVoucher.Name = "btnRejectVoucher";
            this.btnRejectVoucher.Text = "RejectVoucher";
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtMsg.Location = new System.Drawing.Point(3, 607);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(1085, 22);
            this.txtMsg.TabIndex = 21;
            this.txtMsg.Text = ":)";
            // 
            // btnExit
            // 
            this.btnExit.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImagePaddingHorizontal = 8;
            this.btnExit.Name = "btnExit";
            this.btnExit.Text = "Exit";
            // 
            // QA23
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 629);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "QA23";
            this.Text = "QA23";
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        private DevComponents.DotNetBar.ButtonItem btDelete;
        private DevComponents.DotNetBar.ButtonItem btAddPlan;
        private DevComponents.DotNetBar.ButtonItem btnAcceptDraft;
        private DevComponents.DotNetBar.ButtonItem btSearchPlan;
        private DevComponents.DotNetBar.Bar bar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace Workspace;
        private DevComponents.DotNetBar.Bar bar2;
        private DevComponents.DotNetBar.ButtonItem btSearch;
        private DevComponents.DotNetBar.ButtonItem btnADraft;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.DotNetBar.ButtonItem btnAcceptVoucher;
        private DevComponents.DotNetBar.ButtonItem btnRejectDraft;
        private DevComponents.DotNetBar.ButtonItem btnRejectVoucher;
        private DevComponents.DotNetBar.ButtonItem btnExit;
    }
}