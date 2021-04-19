namespace LIMS.Views
{
    partial class QP01
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QP01));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearch = new DevComponents.DotNetBar.ButtonItem();
            this.btAddSchedule = new DevComponents.DotNetBar.ButtonItem();
            this.btShowHistory = new DevComponents.DotNetBar.ButtonItem();
            this.btDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btStart = new DevComponents.DotNetBar.ButtonItem();
            this.btStop = new DevComponents.DotNetBar.ButtonItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.btRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Workspace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearch,
            this.btAddSchedule,
            this.btShowHistory,
            this.btDelete,
            this.btStart,
            this.btStop,
            this.btReturn,
            this.btRefresh});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(673, 26);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 20;
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
            // btAddSchedule
            // 
            this.btAddSchedule.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAddSchedule.Image = ((System.Drawing.Image)(resources.GetObject("btAddSchedule.Image")));
            this.btAddSchedule.ImagePaddingHorizontal = 8;
            this.btAddSchedule.Name = "btAddSchedule";
            this.btAddSchedule.Text = "Add Schedule";
            // 
            // btShowHistory
            // 
            this.btShowHistory.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btShowHistory.Image = ((System.Drawing.Image)(resources.GetObject("btShowHistory.Image")));
            this.btShowHistory.ImagePaddingHorizontal = 8;
            this.btShowHistory.Name = "btShowHistory";
            this.btShowHistory.Text = "View History";
            // 
            // btDelete
            // 
            this.btDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImagePaddingHorizontal = 8;
            this.btDelete.Name = "btDelete";
            this.btDelete.Text = "Delete";
            // 
            // btStart
            // 
            this.btStart.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btStart.Image = ((System.Drawing.Image)(resources.GetObject("btStart.Image")));
            this.btStart.ImagePaddingHorizontal = 8;
            this.btStart.Name = "btStart";
            this.btStart.Text = "Start";
            this.btStart.Visible = false;
            // 
            // btStop
            // 
            this.btStop.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btStop.Image = ((System.Drawing.Image)(resources.GetObject("btStop.Image")));
            this.btStop.ImagePaddingHorizontal = 8;
            this.btStop.Name = "btStop";
            this.btStop.Text = "Stop";
            this.btStop.Visible = false;
            // 
            // btReturn
            // 
            this.btReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReturn.Image = ((System.Drawing.Image)(resources.GetObject("btReturn.Image")));
            this.btReturn.ImagePaddingHorizontal = 8;
            this.btReturn.Name = "btReturn";
            this.btReturn.Text = "Return";
            // 
            // btRefresh
            // 
            this.btRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btRefresh.Image")));
            this.btRefresh.ImagePaddingHorizontal = 8;
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Text = "Refresh";
            this.btRefresh.Visible = false;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Workspace, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(679, 473);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // Workspace
            // 
            this.Workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Workspace.Location = new System.Drawing.Point(3, 33);
            this.Workspace.Name = "Workspace";
            this.Workspace.Size = new System.Drawing.Size(673, 437);
            this.Workspace.TabIndex = 21;
            this.Workspace.Text = "deckWorkspace1";
            // 
            // QP01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 473);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "QP01";
            this.Text = "QP01";
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btSearch;
        private DevComponents.DotNetBar.ButtonItem btDelete;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace Workspace;
        private DevComponents.DotNetBar.ButtonItem btStart;
        private DevComponents.DotNetBar.ButtonItem btStop;
        private DevComponents.DotNetBar.ButtonItem btShowHistory;
        private DevComponents.DotNetBar.ButtonItem btAddSchedule;
        private DevComponents.DotNetBar.ButtonItem btRefresh;
    }
}