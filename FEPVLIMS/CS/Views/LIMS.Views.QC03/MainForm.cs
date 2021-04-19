using LIMS.BLL;
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
using BasicLanuage;

namespace LIMS.Views.QC03
{
    public partial class MainForm : Infrastructure.BaseForm
    {
        QCReporting reports = new QCReporting();
        DOCRequisitionBiz _docRequisitionBiz = new DOCRequisitionBiz();
      //  OperationHistoryBiz _operationHistoryBiz = new OperationHistoryBiz();

     
        public MainForm()
        {
            InitializeComponent();
           
            dateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.Load += new EventHandler(MainForm_Load);

        }
        public bool IsLanguage { set; get; }
        void MainForm_Load(object sender, EventArgs e)
        {

            if (IsLanguage)
            {
                #region Language

                //this.TabText = CultureLanuage.Translator(this.Name, 0, "Result Input [QE31]");
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QA23", "MainForm");
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

                #endregion
            }
            btAcceptDraft.Enabled = false;
            btRejectDraft.Enabled = false;
            btAcceptVoucher.Enabled = false;
            btRejectVoucher.Enabled = false;

            bar1.Refresh();
         
            txtDraftID.KeyDown += new KeyEventHandler(txtDraftID_KeyDown);
            txtVoucherID.KeyDown += new KeyEventHandler(txtVoucherID_KeyDown);
            gridView1.Click += new EventHandler(gridView1_Click);
     
            btAcceptVoucher.Click += new EventHandler(btAcceptVoucher_Click);
            btAcceptDraft.Click += new EventHandler(btAcceptDraft_Click);
            btPlanAddConfirm.Click += new EventHandler(btPlanAddConfirm_Click);
            btRejectDraft.Click += new EventHandler(btRejectDraft_Click);
            btRejectVoucher.Click += new EventHandler(btRejectVoucher_Click);
            butReqQuery.Click += new EventHandler(butReqQuery_Click);
          //  gridView1.Columns[0].da = "DateTime";

            QueryDraftPlan("", "");
            QuerySampleAttribute("");
        }

        void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            DraftID = row["DraftID"].ToString();//委托编号
            VoucherID = row["VoucherID"].ToString();//样品编号
            DraftState = row["State"].ToString();//状态

            BarShow(DraftState);
            QuerySampleAttribute(VoucherID);
        }

        /// <summary>
        /// 按钮显示
        /// </summary>
        private void BarShow(string _DraftState)
        {
            switch (_DraftState)
            {
                case "0":
                    btAcceptDraft.Enabled = true;
                    btRejectDraft.Enabled = true;
                    btAcceptVoucher.Enabled = true;
                    btRejectVoucher.Enabled = true;
                    bar1.Refresh();
                    break;
                case "1":
                    btAcceptDraft.Enabled = false;
                    btRejectDraft.Enabled = false;
                    btAcceptVoucher.Enabled = false;
                    btRejectVoucher.Enabled = false;
                    bar1.Refresh();
                    break;
                case "X":
                    btAcceptDraft.Enabled = false;
                    btRejectDraft.Enabled = false;
                    btAcceptVoucher.Enabled = false;
                    btRejectVoucher.Enabled = false;
                    bar1.Refresh();
                    break;
                default:
                    btAcceptDraft.Enabled = false;
                    btRejectDraft.Enabled = false;
                    btAcceptVoucher.Enabled = false;
                    btRejectVoucher.Enabled = false;
                    bar1.Refresh(); 
                    break;
            }
        }

        void txtVoucherID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) 
                return;

            if (string.IsNullOrEmpty(txtVoucherID.Text.Trim()))
            {
           
                WriteTips(8, "input  VoucherID :RMXQ..", false);
                return;
            }
            btAcceptDraft.Enabled = false;
            btRejectDraft.Enabled = false;
            btAcceptVoucher.Enabled = false;
            btRejectVoucher.Enabled = false;
            bar1.Refresh();

            QueryDraftPlan("", "RMXQ" + txtVoucherID.Text.Trim());
            //
            ReflashSampleAttribute();
        }

        void txtDraftID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
                return;

            if (string.IsNullOrEmpty(txtDraftID.Text.Trim()))
            {
                WriteTips(8, "input  DraftID :MXQ..", false);
                return;
            }

            btAcceptDraft.Enabled = false;
            btRejectDraft.Enabled = false;
            btAcceptVoucher.Enabled = false;
            btRejectVoucher.Enabled = false;
            bar1.Refresh();

            QueryDraftPlan("MXQ" + txtDraftID.Text.Trim(), "");
            //
            ReflashSampleAttribute();
        }

        /// <summary>
        /// 刷新属性列表
        /// </summary>
        void ReflashSampleAttribute()
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
            {
                QuerySampleAttribute("");
                return;
            }
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            QuerySampleAttribute(row["VoucherID"].ToString());
        }
        DateTime? B
        {
            get
            {
                if (dateFrom.Text == "")
                    return null;
                else
                    return Convert.ToDateTime(dateFrom.Text);
            }
        }
        DateTime? E
        {
            get
            {
                if (dateTo.Text == "")
                    return null;
                else
                    return Convert.ToDateTime(dateTo.Text);
            }
        }
        /// <summary>
        /// 根据委托单号或样品单号查询委托单
        /// </summary>
        /// <param name="draftid">委托单号</param>
        /// <param name="voucherid">样品单号</param>
        void QueryDraftPlan(string draftid, string voucherid)
        {
            tableDraftList = reports.Reporting("QC03_QueryDraftPlan", new string[] { "DraftID", "VoucherID", "DateForm", "DateTo" }, new object[] { draftid, voucherid, B,E }).Tables[0];
        }
        /// <summary>
        /// 根据样品单号查询属性
        /// </summary>
        /// <param name="voucherid">样品单号</param>
        void QuerySampleAttribute(string voucherid)
        {
            tableSampleAttributeList = reports.Reporting("QC03_QuerySampleAttribute", new string[] { "VoucherID" }, new object[] { voucherid }).Tables[0];
        }

        #region --button event--
        private void btAcceptDraft_Click(object sender, EventArgs e)
        {
         
            if (Infrastructure.ConfirmBox.Show("Accept", string.Format("{0}:Accept,GO ON?", DraftID)))
            
            {
                Console.WriteLine("DraftID:" + DraftID);
                if (string.IsNullOrEmpty(DraftID))
                {
                    WriteTips(8, "Select  DraftID Row", false);
                    return;
                }
                string msg = string.Empty;
                if (_docRequisitionBiz.Accept(DraftID,out msg))
                {
                  
                    QueryDraftPlan("MXQ" + txtDraftID.Text.Trim(), "");
                }
                else
                {

                    WriteTips(8, "document Accept Failure" + msg, false);
                }
                DraftID = "";
            }
        }

        private void btRejectDraft_Click(object sender, EventArgs e)
        {
            Console.WriteLine("DraftID:" + DraftID);
            if (string.IsNullOrEmpty(DraftID))
            {
             
                WriteTips(8, "Select  DraftID Row", false);
                return;
            }
            if (Infrastructure.ConfirmBox.Show("Delete", string.Format("{0}:Delete,GO ON?", DraftID)))
            {
        
                string msg = string.Empty;
                if (_docRequisitionBiz.Reject(DraftID,out msg))
                {
                   
                    QueryDraftPlan("MXQ" + txtDraftID.Text.Trim(), "");
                }
                else
                {
                    WriteTips(8, "document rejection Failure", false);
                }
            }
            
            DraftID = "";
        }

        private void btAcceptVoucher_Click(object sender, EventArgs e)
        {
            Console.WriteLine("VoucherID:" + VoucherID);
            if (string.IsNullOrEmpty(VoucherID))
            {
            
                WriteTips(8, "Select  VoucherID Row", false);
                return;
            }
            string msg = string.Empty;
            if (_docRequisitionBiz.Accept(VoucherID, out msg))
            {
              
                QueryDraftPlan("MXQ" + txtDraftID.Text.Trim(), "RMXQ" + txtVoucherID.Text.Trim());
            }
            else
            {
                WriteTips(8, "Accept  VoucherID Failed", false);
            }
            VoucherID = "";
        }
        private void butReqQuery_Click(object sender, EventArgs e)
        {

            QueryDraftPlan("", "");
        }
        private void btRejectVoucher_Click(object sender, EventArgs e)
        {
            Console.WriteLine("VoucherID:" + VoucherID);
            if (string.IsNullOrEmpty(VoucherID))
            {
                WriteTips(8, "Select  VoucherID Row", false);
                return;
            }
            if (string.IsNullOrEmpty(txtReason.Text.Trim()))
            {
                WriteTips(8, "Reject  VoucherID Failed", false);
                return;
            }
            string msg = string.Empty;
            if (Infrastructure.ConfirmBox.Show("Delete", string.Format("{0}:Delete,GO ON?", VoucherID)))
            {
                if (_docRequisitionBiz.Reject(VoucherID, out msg))
                {

                    QueryDraftPlan("MXQ" + txtDraftID.Text.Trim(), "RMXQ" + txtVoucherID.Text.Trim());
                }
                else
                {
                    WriteTips(8, "Rejected  VoucherID Failed", false);


                }
            }
            VoucherID = "";
        }

        private void btPlanAddConfirm_Click(object sender, EventArgs e)
        {
            FormEditer f = new FormEditer();
            f.ShowDialog();
        }
        #endregion

        #region --属性--
        /// <summary>
        /// 委托单
        /// </summary>
        public DataTable tableDraftList
        {
            set
            {
                gridView1.Columns.Clear();
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }
        /// <summary>
        /// 样品属性
        /// </summary>
        public DataTable tableSampleAttributeList
        {
            set
            {
                gridView2.Columns.Clear();
                gridControl2.DataSource = value;
                gridView2.BestFitColumns();
            }
        }

        public string VoucherID { get; set; }//样品编号

        public string DraftID { get; set; }//委托编号

        public string DraftState { get; set; }//委托单状态

        #endregion
    }
}
