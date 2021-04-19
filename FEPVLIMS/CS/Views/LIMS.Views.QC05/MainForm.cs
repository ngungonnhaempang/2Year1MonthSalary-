using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MIS.Utility;
using LIMS.BLL;
using System.Collections;
using BasicLanuage;
namespace LIMS.Views.QC05
{
    public partial class MainForm : Infrastructure.BaseForm
    {
        QCReporting rep = new QCReporting();
        DOCBiz _docBiz = new DOCBiz();
        DataSet dsgrid = new DataSet();
        public MainForm()
        {
           
            InitializeComponent();          
            this.Load += new EventHandler(MainForm_Load);
            #region Language

            try
            {
                
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QE41", "MainForm");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView1, "gridView1", dsgrid);
                    this.gridView1.BestFitColumns();
                }

                gridData = CultureLanuage.GridHeader(dsgrid, "gridView2");
                if (gridData != null)
                {
                    this.gridControl2.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView2, "gridView2", dsgrid);
                    this.gridView2.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }

            #endregion

        }

        void MainForm_Load(object sender, EventArgs e)
        {
            btSearchSure.Click += new EventHandler(btSearchSure_Click);
            btVouSubmit.Click += new EventHandler(btVouSubmit_Click);
            btItemSubmit.Click += new EventHandler(btItemSubmit_Click);
            btItemRelease.Click += new EventHandler(btItemRelease_Click);
            btSearchback.Click += new EventHandler(btSearchback_Click);
            btVouRelease.Click += new EventHandler(btVouRelease_Click);           
            //txtVoucherID, txtLotNo, txtSampleName, txtSampleID
            txtVoucherID.KeyDown += new KeyEventHandler(txtVoucherID_KeyDown);
            txtLotNo.KeyDown += new KeyEventHandler(txtLotNo_KeyDown);
            txtSampleName.KeyDown += new KeyEventHandler(txtSampleName_KeyDown);
            txtSampleID.KeyDown += new KeyEventHandler(txtSampleID_KeyDown);

            gridView1.Click += new EventHandler(gridView1_Click);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
            gridView2.Click += new EventHandler(gridView2_Click);
            gridView2.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(gridView2_RowCellStyle);

            cbFromDate.DateTime = DateTime.Now.AddMonths(-1); //Set Default value
            cbEndDate.DateTime = DateTime.Now;

            #region Languages
            if (IsLanguage)
            {
                CultureLanuage.ApplyResourcesFrom(this, "QE41", "MainForm");

                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QE41", "MainForm");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                DataTable gridData2 = CultureLanuage.GridHeader(dsgrid, "gridView2");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView1, "gridView1", dsgrid);
                    this.gridView1.BestFitColumns();
                }
                if (gridData2 != null)
                {
                    this.gridControl2.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView2, "gridView2", dsgrid);
                    this.gridView2.BestFitColumns();
                }
            }
            #endregion
        }
   
        public bool IsLanguage { set; get; }
        public string VoucherID { get; set; }
        public string SampleName { get; set; }
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0)
                return;

            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null)
                return;

            if (!string.IsNullOrEmpty(dr["PlanAddID"].ToString()))
            {
                //e.Appearance.ForeColor = Color.LightYellow;// 改变行字体颜色
                e.Appearance.BackColor = Color.Orange;// 改变行背景颜色
                //e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色                
            }
        }
        void gridView2_Click(object sender, EventArgs e)
        {
            int rowCount = gridView2.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView2.GetDataRow(gridView2.GetSelectedRows()[0]);
            VoucherID = row["VoucherID"].ToString();
        }
        string GradeVersion { get; set; }
        void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            VoucherID = row["VoucherID"].ToString();
            SampleName = row["SampleName"].ToString();
            GradeVersion = row["GradeVersion"].ToString();

            txtRemark.Text = row["Remark"].ToString();
            //Items
            SearchPlansItem();

            //Remark
            //DataTable tableVoucher = new DataTable();
            //tableVoucher = rep.Reporting("QC04_QueryDocByVoucherId", new string[] { "voucherId", "Lang" }, new object[] { VoucherID,MyLanguage.Language }).Tables[0];
            //if (tableVoucher.Rows.Count > 0)
            //{
            //    DataRow rowVoucher = tableVoucher.Rows[0];
            //    txtRemark.Text = rowVoucher["Remark"].ToString();
            //}
            //else
            //{
            //    txtRemark.Text = "";
            //}

            //Grade
            this.cmbGrade.DataSource = rep.Reporting("QC05_Query_SampleGradesList", new string[] { "SampleName" }, new object[] { SampleName }).Tables[0];
            this.cmbGrade.DisplayMember = "Grade";
            this.cmbGrade.ValueMember = "Grade";

            txtSysGrade.Text = GetAutoGrade(VoucherID);
            cmbGrade.Text = txtSysGrade.Text;
        }
        /// <summary>
        /// 自动判等
        /// </summary>
        /// <param name="voucher"></param>
        /// <returns></returns>
        private string GetAutoGrade(string voucherId)
        {
            DataTable dt = rep.Reporting("QC05_Query_Grade", new string[] { "VoucherID" }, new object[] { voucherId }).Tables[0];
            if (dt.Rows.Count <= 0)
                return "";
            else
                return dt.Rows[0][0].ToString();
        }
        private void Input()
        {
            btVouSubmit.Enabled = true;
            btItemSubmit.Enabled = true;
            btItemRelease.Enabled = true;
            btVouRelease.Enabled = false;
            bar1.Refresh();
        }
      void txtSampleName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSampleName.Text.Trim()))
                {
                    WriteTips(8, "input SampleName keyword", false);
                    return;
                }
                TxtClick();
            }
        }
        void txtVoucherID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtVoucherID.Text.Trim()))
                {
                    WriteTips(8,"Please input  voucher keyword",false);
                    return;
                }
                TxtClick();
            }
        }
        void txtLotNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtLotNo.Text.Trim()))
                {
                    WriteTips(8, "Please input  LotNo keyword", false);
                    return;
                }
                TxtClick();
            }
        }
        public Color ValueOutRange { get { return Color.PaleVioletRed; } }
        public Color ValueInRange { get { return Color.LightBlue; } }
        void txtSampleID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtSampleID.Text.Trim()))
                {
                    WriteTips(8, "Please input the sample number key", false);
                    return;
                }
                TxtClick();
            }
        }
        /// <summary>
        /// 单据释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btVouRelease_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount <= 0)
            {
                WriteTips(8, "Please Select VoucherID", false);
                return;
            }

            if (VoucherIDs.Length <= 0)
                return;

            if (DialogResult.OK == MessageBox.Show("Released, IF Go on?", "Released", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2))
            {
                string msg = "";
                int count = 0;
                foreach (string item in VoucherIDs)
                {
                    if (!_docBiz.DocUnLock(item))
                        msg += "[" + item + "]";
                    else
                    {
                        //_operationHistoryBiz.WriteHistory(item, "Released");
                        count++;
                    }
                }
                if (count != VoucherIDs.Length)
                    MessageBox.Show("VoucherID " + msg + "释放失败!");
                SearchUnLock();
                ClaerPlansItemTable();
            }
        }
        void TxtClick()
        {
            Input();
            SearchConfirm();
            ClaerPlansItemTable();
        }
        /// <summary>
        /// 取消确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btItemRelease_Click(object sender, EventArgs e)
        {
            int rowCount = gridView2.SelectedRowsCount;
            if (rowCount <= 0)
            {
                WriteTips(8, "Please Select VoucherID", false);
                return;
            }

            if (PropertyNames.Length <= 0)
                return;

            string msg = "";
            int count = 0;
            foreach (string item in PropertyNames)
            {
                if (!_docBiz.DocPropertyUnLock(VoucherID, item))
                    msg += "[" + item + "]";
                else
                    count++;
            }
            if (count != PropertyNames.Length)
                MessageBox.Show("VoucherID[" + VoucherID + "]PropertyName" + msg + "Failed!");
            SearchPlansItem();
        }

        void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                DataRow selectrow = gridView2.GetDataRow(e.RowHandle);

                if (e.Column.FieldName == "Result")
                {
                    if (string.IsNullOrEmpty(selectrow["Result"].ToString()))
                    {
                        e.Appearance.BackColor = Color.White;
                        return;
                    }

                    if ((bool)selectrow["OverRange"])
                    {
                        e.Appearance.BackColor = ValueOutRange;
                        e.Appearance.BackColor2 = Color.White;
                    }
                    else
                    {
                        e.Appearance.BackColor = ValueInRange;
                        e.Appearance.BackColor2 = Color.White;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error:" + exc.Message);
            }
        }
        /// <summary>
        /// 头列表
        /// </summary>
        public DataTable PlansTable
        {
            set
            {
                //#region Language
                //DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                //if (gridData != null)
                //{
                //    this.gridControl1.DataSource = gridData;
                //    CultureLanuage.GridResourcesFrom(this.gridView1, "gridView1", dsgrid);
                //    this.gridView1.BestFitColumns();
                //}
                //#endregion
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }
        /// <summary>
        /// 结果列表
        /// </summary>
        public DataTable PlansItemTable
        {
            set
            {
                //#region Language
                //    DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView2");
                //    if (gridData != null)
                //    {
                //        this.gridControl2.DataSource = gridData;
                //        CultureLanuage.GridResourcesFrom(this.gridView2, "gridView2", dsgrid);
                //        this.gridView2.BestFitColumns();
                //    }
                //#endregion
                gridControl2.DataSource = value;
                gridView2.BestFitColumns();
            }
        }
        public string FromDate {           
            get 
            { 
                return cbFromDate.DateTime.ToString("yyyy-MM-dd"); 
            } 
        }
        public string EndDate {          
            get 
            { 
                return cbEndDate.DateTime.ToString("yyyy-MM-dd"); 
            } 
        }
       // public string B
        //{
        //    get
        //    {
        //        if (chkIsUsered.Checked == true)
        //        {
        //            if (string.IsNullOrEmpty(cbFromDate.Text))
        //                return DateTime.Today.ToString("yyyy-MM-dd");
        //            return cbFromDate.DateTime.ToString("yyyy-MM-dd");
        //        }
        //        else
        //            return null;
        //    }
        //}     
        /// <summary>
        /// 单据确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btVouSubmit_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount <= 0)
            {
                WriteTips(8, "Please select voucherID", false);
                return;
            }

            if (VoucherIDs.Length != 1)
            {
                WriteTips(8, "Please select voucherID 1 row", false);
                return;
            }
          
         
           
            if (Infrastructure.ConfirmBox.Show("Confirm", "Do you want to Confirm  Voucher ?"))
            {
                string msg = "";
                int count = 0;
                if (VoucherIDs.Length == 1)
                {
                    string _voucherid = VoucherIDs[0];
                    if (!_docBiz.SetDocGrade(_voucherid, cmbGrade.Text.Trim().ToUpper()))
                        msg += "[" + _voucherid + "]";
                    else
                    {
                       bool _SaveRemark= _docBiz.SaveDocRemark(_voucherid, txtRemark.Text.Trim());
                        
                        bool _Confirm=_docBiz.DocConfirm(_voucherid);
                        WriteTips(8, "DocConfirm", _Confirm);
                    }

                }
                else
                {
                    foreach (string item in VoucherIDs)
                    {
                        if (!_docBiz.SetDocGrade(item, GetAutoGrade(item)))
                            msg += "[" + item + "]";
                        else
                        {
                            _docBiz.DocConfirm(item);
                            count++;
                        }
                    }
                    if (count != VoucherIDs.Length)
                    {
                        MessageBox.Show("Voucher" + msg + "Confirm Failed!");
                    }

                }
                SearchConfirm();
                ClaerPlansItemTable();
            }
        }

        /// <summary>
        /// 释放单据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btSearchback_Click(object sender, EventArgs e)
        {
            Output();

            SearchUnLock();
        }
        private void Output()
        {
            btVouSubmit.Enabled = false;
            btItemSubmit.Enabled = false;
            btItemRelease.Enabled = false;
            btVouRelease.Enabled = true;
            bar1.Refresh();
        }

       /// <summary>
        /// Modified by Isaac 2018-09-05
       /// </summary>
        private void SearchConfirm()
        {
            //MessageBox.Show(B + txtVoucherID.Text.Trim() + txtLotNo.Text.Trim() + txtSampleName.Text.Trim() + txtSampleID.Text.Trim() + MyLanguage.Language);
            PlansTable = rep.Reporting("QC05_Query_Plans", new string[] { "FromDate","EndDate", "VoucherID", "Lot_No", "SampleName", "SampleID", "Lang" },
                  new object[] { FromDate, EndDate, txtVoucherID.Text.Trim(), txtLotNo.Text.Trim(), txtSampleName.Text.Trim(), txtSampleID.Text.Trim(), MyLanguage.Language }).Tables[0];
        }
        /// <summary>
        /// 释放单据查询
        /// </summary>
        private void SearchUnLock()
        {
            PlansTable = rep.Reporting("QC05_Query_Plans4UnLock", new string[] { "FromDate", "EndDate", "VoucherID", "Lot_No", "SampleName", "SampleID", "Lang" },
                  new object[] { FromDate, EndDate, txtVoucherID.Text.Trim(), txtLotNo.Text.Trim(), txtSampleName.Text.Trim(), txtSampleID.Text.Trim(), MyLanguage.Language }).Tables[0];
        }
        /// <summary>
        /// 单据结果查询
        /// </summary>
        private void SearchPlansItem()
        {
            PlansItemTable = rep.Reporting("QC05_Query_PlansItem", new string[] { "VoucherID" },
                  new object[] { VoucherID }).Tables[0];
        }
        /// <summary>
        /// 清空单据结果
        /// </summary>
        private void ClaerPlansItemTable()
        {
            PlansItemTable = rep.Reporting("QC05_Query_PlansItem", new string[] { "VoucherID" },
                  new object[] { "" }).Tables[0];
        }
        /// <summary>
        /// 审核单据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btSearchSure_Click(object sender, EventArgs e)
        {
            Input();

            SearchConfirm();

            ClaerPlansItemTable();
        }
        public string[] PropertyNames
        {
            get
            {
                List<string> propertyNames = new List<string>();

                ArrayList rows = new ArrayList();

                // Add the selected rows to the list.
                int rowCount = gridView2.SelectedRowsCount;
                if (rowCount <= 0)
                    return propertyNames.ToArray();

                for (int i = 0; i < rowCount; i++)
                {
                    if (gridView2.GetSelectedRows()[i] >= 0)
                        rows.Add(gridView2.GetDataRow(gridView2.GetSelectedRows()[i]));
                }
                ///
                foreach (DataRow r in rows)
                {
                    propertyNames.Add((string)r["PropertyName"]);
                }
                return propertyNames.ToArray();
            }
        }
        private void btItemSubmit_Click(object sender, EventArgs e)
        {
            int rowCount = gridView2.SelectedRowsCount;
            if (rowCount <= 0)
            {
                WriteTips(8, "Please select voucherID", false);
                return;
            }

            if (PropertyNames.Length <= 0)
                return;

            string msg = "";
            int count = 0;
            foreach (string item in PropertyNames)
            {
                if (!_docBiz.DocPropertyConfirm(VoucherID, item))
                    msg += "[" + item + "]";
                else
                    count++;
            }
            if (count != PropertyNames.Length)
                MessageBox.Show("Voucher[" + VoucherID + "]PropertyNames" + msg + "Confirm Falied!");
            SearchPlansItem();
        }

        public string[] VoucherIDs
        {
            get
            {
                List<string> voucherIds = new List<string>();

                ArrayList rows = new ArrayList();

                // Add the selected rows to the list.
                int rowCount = gridView1.SelectedRowsCount;
                if (rowCount <= 0)
                    return voucherIds.ToArray();

                for (int i = 0; i < rowCount; i++)
                {
                    if (gridView1.GetSelectedRows()[i] >= 0)
                        rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                }
                ///
                foreach (DataRow r in rows)
                {
                    voucherIds.Add((string)r["VoucherID"]);
                }
                return voucherIds.ToArray();
            }
        }

    }
}
