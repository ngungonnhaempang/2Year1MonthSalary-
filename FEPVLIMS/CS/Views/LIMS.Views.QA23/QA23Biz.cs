using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.BLL;
using LIMS.Model;
using System.Windows.Forms;
using System.Data;
using MIS.Utility;
using BasicLanuage;
using LIMS.Service;

namespace LIMS.Views
{
    public partial class QA23 : Infrastructure.BaseForm
    {
        QCReporting report = new QCReporting();
        string msg;
        public void Query()

        {
            MainMsg = "";
            _QA23View.tablePlanADD = report.GetQCReport("QA23_QueryAddPlan",
                   new string[] { "SampleName", "Material" },
                   new object[] { _QA23View.Sample, _QA23View.Materials }).Tables[0];
        }
        public void QueryDraft()
        {
             MainMsg = "";
           _QA23View.tableDraftList = report.GetQCReport("QA23_QueryDraftPlan", 
               new string[] { "DraftID", "VoucherID", "DateB", "DateE","State" },
               new object[] { _QA23View.TDraftID, _QA23View.TVoucherID, _QA23View.DateB,_QA23View.DateE,_QA23View.State }).Tables[0];

        }
     
        private void BarShow(string _DraftState)
        {
            switch (_DraftState)
            {
                case "D":
                    btnADraft.Enabled = true;
                    btnRejectDraft.Enabled = true;
                    btnAcceptVoucher.Enabled = true;
                    btnRejectVoucher.Enabled = true;
                    bar1.Refresh();
                    break;
                case "Q":
                    btnADraft.Enabled = false;
                    btnRejectDraft.Enabled = false;
                    btnAcceptVoucher.Enabled = false;
                    btnRejectVoucher.Enabled = false;
                    bar1.Refresh();
                    break;
                case "U":
                    btnADraft.Enabled = false;
                    btnRejectDraft.Enabled = false;
                    btnAcceptVoucher.Enabled = false;
                    btnRejectVoucher.Enabled = false;
                    bar1.Refresh();
                    break;
                default:
                    btnADraft.Enabled = false;
                    btnRejectDraft.Enabled = false;
                    btnAcceptVoucher.Enabled = false;
                    btnRejectVoucher.Enabled = false;
                    bar1.Refresh();
                    break;
            }
        }
        void _QA23View_eventVoucherIDKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
            {
                return;
            }
            if (string.IsNullOrEmpty(_QA23View.TVoucherID))
            {

                MessageBox.Show("请输入委托编号");
            }
            btnADraft.Enabled = false;
            btnRejectDraft.Enabled = false;
            btnAcceptVoucher.Enabled = false;
            btnRejectVoucher.Enabled = false;
            bar1.Refresh();
            _QA23View.TVoucherID = "";
            _QA23View.QueryDraftPlan("" , _QA23View.TVoucherID,_QA23View.DateB,_QA23View.DateE,_QA23View.State);    
            _QA23View.ReflashSampleAttribute();
          
        }
         void _QA23View_eventDraftKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter) {
                return;
            }
            if (string.IsNullOrEmpty(_QA23View.TDraftID)) {

                MessageBox.Show("请输入委托编号");
            }
            btnADraft.Enabled = false;
            btnRejectDraft.Enabled = false;
            btnAcceptVoucher.Enabled = false;
            btnRejectVoucher.Enabled = false;
            bar1.Refresh();

            _QA23View.QueryDraftPlan("MXQ" + _QA23View.TDraftID, "", _QA23View.DateB, _QA23View.DateE, _QA23View.State);
            _QA23View.TDraftID = "";
            _QA23View.ReflashSampleAttribute();
          
        }


         void _QA23View_eventgvdClick2(object sender, EventArgs e)
         {
             int rowCount = _QA23View.gridView2.SelectedRowsCount;
             if (rowCount != 1)
                 return;

             DataRow row = _QA23View.gridView2.GetDataRow(_QA23View.gridView2.GetSelectedRows()[0]);
             _QA23View.DraftID = row["DraftID"].ToString();
             _QA23View.VoucherID = row["VoucherID"].ToString();
             _QA23View.DraftState = row["State"].ToString();

             BarShow(_QA23View.DraftState);
             _QA23View.QuerySampleAttribute(_QA23View.VoucherID);
         }

        // Accept Draft
         void btnADraft_Click(object sender, EventArgs e)
         {
             string msg;
             if (DialogResult.OK == MessageBox.Show("According to the order receipt, whether to continue?", "Receipt of documents", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2))
             {
                 Console.WriteLine("DraftID:" +_QA23View.DraftID);
                 if (string.IsNullOrEmpty(_QA23View.DraftID))
                 {
                     MessageBox.Show("请选择委托单据");
                     return;
                 }
                 if (Requibiz.Accept(_QA23View.DraftID,out msg))
                 {

                     _QA23View.QueryDraftPlan("MXQ" + _QA23View.TDraftID, "", _QA23View.DateB, _QA23View.DateE,_QA23View.State);
                     MainMsg = "Accept " + _QA23View.TDraftID + " success ! ";

                    // WriteTips(1, CultureLanuage.Translator(this.Name, 3, "Add VoucherID DOC:  {0} successful!", _QA23View.DraftID));
                 }
                 else
                 {
                     MessageBox.Show(msg);
                 }
                 _QA23View.DraftID = "";
             }
         }
         void btnRejectDraft_Click(object sender, EventArgs e)
         {

             if (DialogResult.OK == MessageBox.Show("According to the order receipt, whether to continue?", "Receipt of documents", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2))
             {
                 Console.WriteLine("DraftID:" + _QA23View.DraftID);
                 if (string.IsNullOrEmpty(_QA23View.DraftID))
                 {
                     MessageBox.Show("请选择委托单据");
                     return;
                 }
                 if (Requibiz.Reject(_QA23View.DraftID, out msg))
                 {
                    // WriteTips(1, CultureLanuage.Translator(this.Name, 3, "Reject {0} successfull", _QA23View.DraftID));
                     MainMsg = "Reject successfull : DraftID: "+ _QA23View.DraftID;
                     _QA23View.QueryDraftPlan("MXQ" + _QA23View.TDraftID, "", _QA23View.DateB, _QA23View.DateE, _QA23View.State);
                 }
                 else
                 {
                     MessageBox.Show(msg);
                 }
                 _QA23View.DraftID = "";
             }
         }
         private void _QA23DialogAcceptRejectEvent(object s, EventArgs e)
         {
             string voucherid = "";
             if ((bool)s)
             {
                 //Accept VoucherID
                 if (biz.InsertPlanByAdd(_QA23View.GetPlan, _QA23View.GetPlan.VoucherID, out voucherid))
                 {
                 //  WriteTips(1, CultureLanguage.Translator(this.Name, 3, "Add VoucherID DOC:  {0} successful!", voucherid));
                     MainMsg = "Add VoucherID successful: " + _QA23View.GetPlan.VoucherID;
                     Query();
                 }
                 else
                 {
               
                   // WriteTips(1, CultureLanguage.Translator(this.Name, 3, "Add test document receipt failed", voucherid));
                     MainMsg = "Add test document receipt failed: " + _QA23View.GetPlan.VoucherID;
                 }

             }
             else
             {
                 //Reject VoucherID
                 if (biz.RejectPlandAdd(_QA23View.GetPlan.VoucherID))
                 {

                     //MSG_Info = "The application [" + _QA23View.GetPlan.VoucherID + "] Rejected";
                    // 

                     MainMsg = "Reject VoucherID successfull: " + _QA23View.GetPlan.VoucherID;
                     Query();
                     _QA23Dialog.Close();
                 }
                 else
                 {
                      
                     MainMsg = "Reject VoucherID fail: ";
                 }
             }
         }
        // Accept By Voucher
         void btnAcceptVoucher_Click(object sender, EventArgs e)
         {
             Console.WriteLine("VoucherID:" + _QA23View.VoucherID);
             if (string.IsNullOrEmpty(_QA23View.VoucherID))
             {
                 
                  //WriteTips(1, CultureLanguage.Translator(this.Name, 3, "Please select the sample"));

                 return;
             }
             if (Requibiz.Accept(_QA23View.VoucherID,out msg))
             {
                 MainMsg = "Accept " + _QA23View.VoucherID + " success ! ";
                // WriteTips(1, CultureLanguage.Translator(this.Name, 3, "Accept VoucherID DOC:  {0} successful!!", _QA23View.VoucherID));

                 _QA23View.QueryDraftPlan("MXQ" + _QA23View.TDraftID, "RMXQ" + _QA23View.TVoucherID, _QA23View.DateB, _QA23View.DateE, _QA23View.State);
             }
             else
             {

                 MainMsg= "Add test document receipt failed";
             }
             _QA23View.VoucherID = "";
         }

         void btnRejectVoucher_Click(object sender, EventArgs e)
         {
             if (string.IsNullOrEmpty(_QA23View.VoucherID))
             {


                 MainMsg = "Add test document receipt failed";
                 return;
             }
             if (Requibiz.Reject(_QA23View.VoucherID, out msg))
             {
                   MainMsg = "Reject " + _QA23View.VoucherID + " success ! ";
                // WriteTips(1, CultureLanguage.Translator(this.Name, 3, "Reject VoucherID DOC:  {0} successful!", _QA23View.VoucherID));

                 _QA23View.QueryDraftPlan("MXQ" + _QA23View.TDraftID, "RMXQ" + _QA23View.TVoucherID, _QA23View.DateB, _QA23View.DateE, _QA23View.State);
             }
             else
             {
              
                  MainMsg = "Sample document acceptance failed";
             }
             _QA23View.VoucherID = "";
         }
        void _QA23View_eventShowDetails(object sender, EventArgs e)
        {
            EditPlanADD(_QA23View.GetPlan);         
        }

        public void EditPlanADD(DOCPlan PlanADD)
        {
            _QA23Dialog.Plan = PlanADD;
            _QA23Dialog.ShowDialog(); 
        }

   
    }
}
