namespace LIMS.Views.QC07
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bar1 = new DevComponents.DotNetBar.Bar();
			this.btQuery = new DevComponents.DotNetBar.ButtonItem();
			this.butReceive = new DevComponents.DotNetBar.ButtonItem();
			this.butReturn = new DevComponents.DotNetBar.ButtonItem();
			this.dWorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtMsg = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.dWorkSpace, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 466);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bar1
			// 
			this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
			this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
			this.bar1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btQuery,
            this.butReceive,
            this.butReturn});
			this.bar1.Location = new System.Drawing.Point(3, 3);
			this.bar1.Name = "bar1";
			this.bar1.Size = new System.Drawing.Size(803, 33);
			this.bar1.Stretch = true;
			this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
			this.bar1.TabIndex = 3;
			this.bar1.TabStop = false;
			this.bar1.Text = "bar1";
			// 
			// btQuery
			// 
			this.btQuery.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btQuery.Image = ((System.Drawing.Image)(resources.GetObject("btQuery.Image")));
			this.btQuery.ImagePaddingHorizontal = 8;
			this.btQuery.Name = "btQuery";
			this.btQuery.Text = "查询";
			// 
			// butReceive
			// 
			this.butReceive.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.butReceive.Image = ((System.Drawing.Image)(resources.GetObject("butReceive.Image")));
			this.butReceive.ImagePaddingHorizontal = 8;
			this.butReceive.Name = "butReceive";
			this.butReceive.Text = "检验单生成";
			// 
			// butReturn
			// 
			this.butReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.butReturn.Image = ((System.Drawing.Image)(resources.GetObject("butReturn.Image")));
			this.butReturn.ImagePaddingHorizontal = 8;
			this.butReturn.Name = "butReturn";
			this.butReturn.Text = "检验退回";
			// 
			// dWorkSpace
			// 
			this.dWorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dWorkSpace.Location = new System.Drawing.Point(3, 31);
			this.dWorkSpace.Name = "dWorkSpace";
			this.dWorkSpace.Size = new System.Drawing.Size(803, 412);
			this.dWorkSpace.TabIndex = 2;
			this.dWorkSpace.Text = "deckWorkspace1";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtMsg);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 449);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(803, 14);
			this.panel1.TabIndex = 0;
			// 
			// txtMsg
			// 
			this.txtMsg.AutoSize = true;
			this.txtMsg.Location = new System.Drawing.Point(24, 1);
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.Size = new System.Drawing.Size(35, 18);
			this.txtMsg.TabIndex = 0;
			this.txtMsg.Text = "^_^";
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(809, 466);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace dWorkSpace;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btQuery;
        private DevComponents.DotNetBar.ButtonItem butReceive;
        private DevComponents.DotNetBar.ButtonItem butReturn;

    }
}