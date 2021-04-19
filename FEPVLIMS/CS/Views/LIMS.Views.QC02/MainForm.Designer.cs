namespace LIMS.Views.QC02
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
            this.btSearchPlan = new DevComponents.DotNetBar.ButtonItem();
            this.btCreateByGate = new DevComponents.DotNetBar.ButtonItem();
            this.butTransportPlan = new DevComponents.DotNetBar.ButtonItem();
            this.butAddGatePlan = new DevComponents.DotNetBar.ButtonItem();
            this.butManualAdd = new DevComponents.DotNetBar.ButtonItem();
            this.btDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.WorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtMsg = new System.Windows.Forms.Label();
            this.btCreateManual = new DevComponents.DotNetBar.ButtonItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.WorkSpace, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(808, 484);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearchPlan,
            this.btCreateByGate,
            this.butTransportPlan,
            this.butAddGatePlan,
            this.butManualAdd,
            this.btDelete,
            this.btReturn});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(802, 26);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 23;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar2";
            // 
            // btSearchPlan
            // 
            this.btSearchPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchPlan.Image = ((System.Drawing.Image)(resources.GetObject("btSearchPlan.Image")));
            this.btSearchPlan.ImagePaddingHorizontal = 8;
            this.btSearchPlan.Name = "btSearchPlan";
            this.btSearchPlan.Text = "Search";
            // 
            // btCreateByGate
            // 
            this.btCreateByGate.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btCreateByGate.Image = ((System.Drawing.Image)(resources.GetObject("btCreateByGate.Image")));
            this.btCreateByGate.ImagePaddingHorizontal = 8;
            this.btCreateByGate.Name = "btCreateByGate";
            this.btCreateByGate.Text = "收货计划";
            // 
            // butTransportPlan
            // 
            this.butTransportPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.butTransportPlan.Image = ((System.Drawing.Image)(resources.GetObject("butTransportPlan.Image")));
            this.butTransportPlan.ImagePaddingHorizontal = 8;
            this.butTransportPlan.Name = "butTransportPlan";
            this.butTransportPlan.Text = "查询收货计划";
            // 
            // butAddGatePlan
            // 
            this.butAddGatePlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.butAddGatePlan.Image = ((System.Drawing.Image)(resources.GetObject("butAddGatePlan.Image")));
            this.butAddGatePlan.ImagePaddingHorizontal = 8;
            this.butAddGatePlan.Name = "butAddGatePlan";
            this.butAddGatePlan.Text = "新增检验批";
            // 
            // butManualAdd
            // 
            this.butManualAdd.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.butManualAdd.Image = ((System.Drawing.Image)(resources.GetObject("butManualAdd.Image")));
            this.butManualAdd.ImagePaddingHorizontal = 8;
            this.butManualAdd.Name = "butManualAdd";
            this.butManualAdd.Text = "手工创建";
            this.butManualAdd.Visible = false;
            // 
            // btDelete
            // 
            this.btDelete.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btDelete.Image = ((System.Drawing.Image)(resources.GetObject("btDelete.Image")));
            this.btDelete.ImagePaddingHorizontal = 8;
            this.btDelete.Name = "btDelete";
            this.btDelete.Text = "Delete";
            // 
            // btReturn
            // 
            this.btReturn.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btReturn.Image = ((System.Drawing.Image)(resources.GetObject("btReturn.Image")));
            this.btReturn.ImagePaddingHorizontal = 8;
            this.btReturn.Name = "btReturn";
            this.btReturn.Text = "Return";
            // 
            // WorkSpace
            // 
            this.WorkSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(178)))), ((int)(((byte)(195)))));
            this.WorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkSpace.Location = new System.Drawing.Point(0, 28);
            this.WorkSpace.Margin = new System.Windows.Forms.Padding(0);
            this.WorkSpace.Name = "WorkSpace";
            this.WorkSpace.Size = new System.Drawing.Size(808, 432);
            this.WorkSpace.TabIndex = 21;
            this.WorkSpace.Text = "Search";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 463);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtMsg);
            this.splitContainer1.Size = new System.Drawing.Size(802, 18);
            this.splitContainer1.SplitterDistance = 617;
            this.splitContainer1.TabIndex = 22;
            // 
            // txtMsg
            // 
            this.txtMsg.AutoSize = true;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(17, 12);
            this.txtMsg.TabIndex = 23;
            this.txtMsg.Text = ":)";
            // 
            // btCreateManual
            // 
            this.btCreateManual.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btCreateManual.Image = ((System.Drawing.Image)(resources.GetObject("btCreateManual.Image")));
            this.btCreateManual.ImagePaddingHorizontal = 8;
            this.btCreateManual.Name = "btCreateManual";
            this.btCreateManual.Text = "Manual Add";
            this.btCreateManual.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 484);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainForm";
            this.Text = "MainForm 原料";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace WorkSpace;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.DotNetBar.ButtonItem btCreateManual;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        private DevComponents.DotNetBar.ButtonItem btSearchPlan;
        private DevComponents.DotNetBar.ButtonItem btCreateByGate;
        private DevComponents.DotNetBar.ButtonItem butAddGatePlan;
        private DevComponents.DotNetBar.ButtonItem butManualAdd;
        private DevComponents.DotNetBar.ButtonItem btDelete;
        private DevComponents.DotNetBar.ButtonItem butTransportPlan;
    }
}