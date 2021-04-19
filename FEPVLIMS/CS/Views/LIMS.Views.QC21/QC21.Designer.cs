namespace LIMS.Views
{
    partial class QC21
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QC21));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearchPlan = new DevComponents.DotNetBar.ButtonItem();
            this.btnQueryGoods = new DevComponents.DotNetBar.ButtonItem();
            this.btnPlanBarcode = new DevComponents.DotNetBar.ButtonItem();
            this.btAdd = new DevComponents.DotNetBar.ButtonItem();
            this.btAddPlan = new DevComponents.DotNetBar.ButtonItem();
            this.btDelete = new DevComponents.DotNetBar.ButtonItem();
            this.btReturn = new DevComponents.DotNetBar.ButtonItem();
            this.WorkSpace = new Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace();
            this.txtMsg = new System.Windows.Forms.Label();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.WorkSpace, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMsg, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(899, 473);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearchPlan,
            this.btnQueryGoods,
            this.btnPlanBarcode,
            this.btAdd,
            this.btAddPlan,
            this.btDelete,
            this.btReturn});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(893, 25);
            this.bar1.Stretch = true;
            this.bar1.TabIndex = 20;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearchPlan
            // 
            this.btSearchPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearchPlan.Image = ((System.Drawing.Image)(resources.GetObject("btSearchPlan.Image")));
            this.btSearchPlan.ImagePaddingHorizontal = 8;
            this.btSearchPlan.Name = "btSearchPlan";
            this.btSearchPlan.Text = "Search Plan";
            // 
            // btnQueryGoods
            // 
            this.btnQueryGoods.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnQueryGoods.Image = ((System.Drawing.Image)(resources.GetObject("btnQueryGoods.Image")));
            this.btnQueryGoods.ImagePaddingHorizontal = 8;
            this.btnQueryGoods.Name = "btnQueryGoods";
            this.btnQueryGoods.Text = "Query Goods";
            // 
            // btnPlanBarcode
            // 
            this.btnPlanBarcode.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnPlanBarcode.Image = ((System.Drawing.Image)(resources.GetObject("btnPlanBarcode.Image")));
            this.btnPlanBarcode.ImagePaddingHorizontal = 8;
            this.btnPlanBarcode.Name = "btnPlanBarcode";
            this.btnPlanBarcode.Tag = "g";
            this.btnPlanBarcode.Text = "Plan Barcode";
            // 
            // btAdd
            // 
            this.btAdd.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAdd.Image = ((System.Drawing.Image)(resources.GetObject("btAdd.Image")));
            this.btAdd.ImagePaddingHorizontal = 8;
            this.btAdd.Name = "btAdd";
            this.btAdd.Text = "Create";
            // 
            // btAddPlan
            // 
            this.btAddPlan.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btAddPlan.Image = ((System.Drawing.Image)(resources.GetObject("btAddPlan.Image")));
            this.btAddPlan.ImagePaddingHorizontal = 8;
            this.btAddPlan.Name = "btAddPlan";
            this.btAddPlan.Text = "Add Plan";
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
            this.btReturn.Text = "Exit";
            // 
            // WorkSpace
            // 
            this.WorkSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(178)))), ((int)(((byte)(195)))));
            this.WorkSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkSpace.Location = new System.Drawing.Point(0, 30);
            this.WorkSpace.Margin = new System.Windows.Forms.Padding(0);
            this.WorkSpace.Name = "WorkSpace";
            this.WorkSpace.Size = new System.Drawing.Size(899, 425);
            this.WorkSpace.TabIndex = 21;
            this.WorkSpace.Text = "Search";
            // 
            // txtMsg
            // 
            this.txtMsg.AutoSize = true;
            this.txtMsg.Location = new System.Drawing.Point(3, 455);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(13, 13);
            this.txtMsg.TabIndex = 22;
            this.txtMsg.Text = ":)";
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
            // QC21
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 473);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "QC21";
            this.Text = "QC21";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem btAdd;
        private DevComponents.DotNetBar.ButtonItem btDelete;
        private DevComponents.DotNetBar.ButtonItem btReturn;
        private Microsoft.Practices.CompositeUI.WinForms.DeckWorkspace WorkSpace;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.DotNetBar.ButtonItem btSearchPlan;
        private DevComponents.DotNetBar.ButtonItem btAddPlan;
        private DevComponents.DotNetBar.ButtonItem btnQueryGoods;
        private DevComponents.DotNetBar.ButtonItem btnPlanBarcode;
    }
}