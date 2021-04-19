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
using LIMS.Service;

namespace LIMS.Views
{
    public partial class QA23 : Infrastructure.BaseForm
    {
        public QA23()
        {

            InitializeComponent();
            Workspace.Show(_QA23View);

            btSearch.Click += btSearch_Click;

            _QA23View.event_cmbMaterial += _QA23Viewevent_cmbMaterial;
            _QA23View.event_SampleName += _QA23View_event_SampleName;
            _QA23View.eventShowDetails += _QA23View_eventShowDetails;
            _QA23Dialog.AcceptRejectEvent += _QA23DialogAcceptRejectEvent;
            _QA23View.eventVoucherIDKeyDown += _QA23View_eventVoucherIDKeyDown;
            _QA23View.eventDraftKeyDown += _QA23View_eventDraftKeyDown;
            _QA23View.eventgvdClick2 += _QA23View_eventgvdClick2;
            btnExit.Click += btnExit_Click;

            btnADraft.Enabled = false;
            btnAcceptVoucher.Enabled = false;
            btnRejectDraft.Enabled = false;
            btnRejectVoucher.Enabled = false;
         
            btnADraft.Click += btnADraft_Click;
            btnRejectDraft.Click += btnRejectDraft_Click;
            btnAcceptVoucher.Click += btnAcceptVoucher_Click;
            btnRejectVoucher.Click += btnRejectVoucher_Click;
            //#region Languages
            CultureLanuage.ApplyResourcesFrom(this, "QA23", "QA23");
          ///  CultureLanuage.ApplyResourcesFrom(_QA23View, "QA23", "QA23View");
            CultureLanuage.ApplyResourcesFrom(_QA23Dialog, "QA23", "QA23Dialog");
            DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(_QA23View, "QA23", "QA23View");
            DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
            if (gridData != null)
            {
                _QA23View.gridControl1.DataSource = gridData;
                CultureLanuage.GridResourcesFrom(_QA23View.gridView1, "gridView1", dsgrid);
                _QA23View.gridView1.BestFitColumns();
            }

            gridData = CultureLanuage.GridHeader(dsgrid, "gridView2");
            if (gridData != null)
            {
                _QA23View.gridControl2.DataSource = gridData;
                CultureLanuage.GridResourcesFrom(_QA23View.gridView2, "gridView2", dsgrid);
                _QA23View.gridView2.BestFitColumns();
            }

            gridData = CultureLanuage.GridHeader(dsgrid, "gridView3");
            if (gridData != null)
            {
                _QA23View.gridControl3.DataSource = gridData;
                CultureLanuage.GridResourcesFrom(_QA23View.gridView3, "gridView3", dsgrid);
                _QA23View.gridView3.BestFitColumns();
            }

            //#endregion
        }

        void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
        DOCRequisitionBiz Requibiz = new DOCRequisitionBiz();
        DOCPlanAddBiz biz = new DOCPlanAddBiz();
        QA23View _QA23View = new QA23View();
        QA23Dialog _QA23Dialog = new QA23Dialog();

        void btSearch_Click(object sender, EventArgs e)
        {
            if (_QA23View.tabControl1.SelectedTab == _QA23View.tabControl1.TabPages["tabPage1"])//your specific tabname
               
            {
               
                Query();
            }

            else
                // _QA23View.tableDraftList = report.GetQCReport("QA23_QueryDraftPlan", new string[] { "DraftID", "VoucherID", "DateB", "DateE" }, new object[] { _QA23View.TDraftID, _QA23View.TVoucherID, _QA23View.DateB, _QA23View.DateB }).Tables[0];
                QueryDraft();
                //  _QA23View.QueryDraftPlan(_QA23View.TDraftID, _QA23View.TVoucherID, _QA23View.DateB, _QA23View.DateB);
        }

        void _QA23View_event_SampleName(object sender, EventArgs e)
        {
            _QA23View.FillMaterial();
            Query();
        }


        private void _QA23Viewevent_cmbMaterial(object s, EventArgs e)
        {
            Query();
        }
        string MainMsg
        {
            set
            {
                txtMsg.Text = value;
                txtMsg.Refresh();
            }
        }
    }
}
