namespace LIMS.Views.CU18
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.btSearch = new DevComponents.DotNetBar.ButtonItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.ribbonClientPanel1 = new DevComponents.DotNetBar.Ribbon.RibbonClientPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.gridCenter = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btAssign = new DevExpress.XtraEditors.SimpleButton();
            this.txtUser = new DevExpress.XtraEditors.TextEdit();
            this.lbUser = new DevExpress.XtraEditors.LabelControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbCreatBarCode = new System.Windows.Forms.ProgressBar();
            this.txtMsg = new System.Windows.Forms.Label();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.bindingSourceMaterial = new System.Windows.Forms.BindingSource(this.components);
            this.bgw = new System.ComponentModel.BackgroundWorker();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.ribbonClientPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMaterial)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btSearch});
            this.bar1.Location = new System.Drawing.Point(3, 3);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(722, 25);
            this.bar1.TabIndex = 193;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // btSearch
            // 
            this.btSearch.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btSearch.Image = ((System.Drawing.Image)(resources.GetObject("btSearch.Image")));
            this.btSearch.ImagePaddingHorizontal = 8;
            this.btSearch.Name = "btSearch";
            this.btSearch.Text = "Query";
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.bar1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(809, 28);
            this.tableLayoutPanel2.TabIndex = 193;
            // 
            // ribbonClientPanel1
            // 
            this.ribbonClientPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.ribbonClientPanel1.Controls.Add(this.tableLayoutPanel3);
            this.ribbonClientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonClientPanel1.Location = new System.Drawing.Point(3, 37);
            this.ribbonClientPanel1.Name = "ribbonClientPanel1";
            this.ribbonClientPanel1.ShowFocusRectangle = true;
            this.ribbonClientPanel1.Size = new System.Drawing.Size(809, 437);
            // 
            // 
            // 
            this.ribbonClientPanel1.Style.BackColor = System.Drawing.Color.Transparent;
            this.ribbonClientPanel1.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.ribbonClientPanel1.Style.BackColorGradientAngle = 90;
            this.ribbonClientPanel1.Style.BackgroundImagePosition = DevComponents.DotNetBar.eStyleBackgroundImage.Tile;
            this.ribbonClientPanel1.Style.BorderBottomWidth = 1;
            this.ribbonClientPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.ribbonClientPanel1.Style.BorderLeftWidth = 1;
            this.ribbonClientPanel1.Style.BorderRightWidth = 1;
            this.ribbonClientPanel1.Style.BorderTopWidth = 1;
            this.ribbonClientPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ribbonClientPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.ribbonClientPanel1.TabIndex = 189;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.gridCenter, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupPanel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5286F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.4714F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(809, 437);
            this.tableLayoutPanel3.TabIndex = 193;
            // 
            // gridCenter
            // 
            this.gridCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCenter.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridCenter.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridCenter.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridCenter.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridCenter.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridCenter.Location = new System.Drawing.Point(3, 166);
            this.gridCenter.MainView = this.gridView1;
            this.gridCenter.Name = "gridCenter";
            this.gridCenter.Size = new System.Drawing.Size(803, 268);
            this.gridCenter.TabIndex = 192;
            this.gridCenter.TabStop = false;
            this.gridCenter.UseEmbeddedNavigator = true;
            this.gridCenter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridCenter.Click += new System.EventHandler(this.gridCenter_Click);
            // 
            // gridView1
            // 
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView1.GridControl = this.gridCenter;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // groupPanel1
            // 
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.groupPanel1.Controls.Add(this.panel3);
            this.groupPanel1.Controls.Add(this.btAssign);
            this.groupPanel1.Controls.Add(this.txtUser);
            this.groupPanel1.Controls.Add(this.lbUser);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Font = new System.Drawing.Font("Georgia", 8.25F);
            this.groupPanel1.Location = new System.Drawing.Point(3, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(803, 157);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.groupPanel1.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(215)))), ((int)(((byte)(198)))));
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132)))));
            this.groupPanel1.TabIndex = 191;
            this.groupPanel1.Text = "[Query Information]";
            this.groupPanel1.TitleImage = ((System.Drawing.Image)(resources.GetObject("groupPanel1.TitleImage")));
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(26, 54);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(148, 1);
            this.panel3.TabIndex = 230;
            // 
            // btAssign
            // 
            this.btAssign.Location = new System.Drawing.Point(280, 30);
            this.btAssign.Name = "btAssign";
            this.btAssign.Size = new System.Drawing.Size(75, 23);
            this.btAssign.TabIndex = 228;
            this.btAssign.Text = "Assign";
            this.btAssign.Click += new System.EventHandler(this.btAssign_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(106, 37);
            this.txtUser.Name = "txtUser";
            this.txtUser.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.txtUser.Size = new System.Drawing.Size(143, 18);
            this.txtUser.TabIndex = 223;
            // 
            // lbUser
            // 
            this.lbUser.Location = new System.Drawing.Point(22, 39);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(55, 14);
            this.lbUser.TabIndex = 224;
            this.lbUser.Text = "UserName";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ribbonClientPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(815, 497);
            this.tableLayoutPanel1.TabIndex = 192;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbCreatBarCode);
            this.panel1.Controls.Add(this.txtMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 480);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 14);
            this.panel1.TabIndex = 192;
            // 
            // pbCreatBarCode
            // 
            this.pbCreatBarCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCreatBarCode.Location = new System.Drawing.Point(630, 0);
            this.pbCreatBarCode.Name = "pbCreatBarCode";
            this.pbCreatBarCode.Size = new System.Drawing.Size(135, 13);
            this.pbCreatBarCode.Step = 1;
            this.pbCreatBarCode.TabIndex = 1;
            this.pbCreatBarCode.Visible = false;
            // 
            // txtMsg
            // 
            this.txtMsg.AutoSize = true;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtMsg.Font = new System.Drawing.Font("宋体", 9.25F);
            this.txtMsg.ForeColor = System.Drawing.Color.Black;
            this.txtMsg.Location = new System.Drawing.Point(0, 0);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(0, 13);
            this.txtMsg.TabIndex = 0;
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.ImagePaddingHorizontal = 8;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "生成包号";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(815, 497);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.CU16_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ribbonClientPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUser.Properties)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceMaterial)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.Ribbon.RibbonClientPanel ribbonClientPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar pbCreatBarCode;
        private System.Windows.Forms.Label txtMsg;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btSearch;
        private System.Windows.Forms.BindingSource bindingSourceMaterial;
        private System.ComponentModel.BackgroundWorker bgw;
        private DevExpress.XtraGrid.GridControl gridCenter;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        public System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton btAssign;
        private DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.LabelControl lbUser;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}