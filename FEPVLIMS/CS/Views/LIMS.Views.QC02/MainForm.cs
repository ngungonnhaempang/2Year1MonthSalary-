using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLanuage;
using FEPV.Views;
using LIMS.BLL;
using LIMS.Model;
using MIS.Utility;

namespace LIMS.Views.QC02
{
    public partial class MainForm :  Infrastructure.BaseForm
    {
        QueryReceivePlan _QueryReceivePlan = new QueryReceivePlan();
        QCReporting report = new QCReporting();
        GatePlanFrm _GatePlanFrm = new GatePlanFrm();
        DOCReceiveBiz biz = new DOCReceiveBiz();
        Remark remark = new Remark();
      
        public MainForm()
        {
            InitializeComponent();
            btSearchPlan.Click += btSearchPlan_Click;
            btCreateByGate.Click+=btCreateByGate_Click;
            butTransportPlan.Click+=butTransportPlan_Click;
           this.btDelete.Click+=btDelete_Click;
            btReturn.Click+=btReturn_Click;
            butAddGatePlan.Click+=butAddGatePlan_Click;
            WorkSpace.Show(_QueryReceivePlan);
           // SearchVoucherReceive();
            step = Step.QueryPlanStep;
          
         //   _QueryReceivePlan.MaterialSource = GetMaterialList;
            DataTable dtSample = report.GetQCReport("QC22_SamplesByCategory", new[] { "TypeID", "Lang" }, new object[] { "3",MyLanguage.Language }).Tables[0]; ;
            _QueryReceivePlan.SetSamples = dtSample;
            _GatePlanFrm.SetSamples = dtSample;
            //QC02_QueryMaterialList
            //GetMaterialList = report.GetQCReport("Q_Materials", new string[] { "SampleName", "Query" },
            //                                 new object[] { _GatePlanFrm.SelectSampleName, "S" }).Tables[0];
           // _GatePlanFrm.MaterialSource = GetMaterialList;
            #region language
            try
            {
                CultureLanuage.ApplyResourcesFrom(this, "QC22", this.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            #endregion
        }

    
          Step _step = Step.QueryPlanStep;
          public Step step
          {
              get
              {
                  return _step;
              }
              set
              {
                  _step = value;
                  //if (_step > Step.DownPlanStep)
                  //    _step = Step.DownPlanStep;
                  //if (_step < Step.QueryPlanStep)
                  //    _step = Step.QueryPlanStep;

                  switch (_step)
                  {
                      case Step.QueryPlanStep:
                          ButtonSetting(true);                       
                          WorkSpace.Show(_QueryReceivePlan);
                          bar1.Refresh();
                          break;
                      case Step.GatePlanStep:
                          btReturn.Visible = true;
                          ButtonSetting(false);
                          WorkSpace.Show(_GatePlanFrm);
                          bar1.Refresh();
                          break;

                  }
              }

          }
        public void ButtonSetting(bool isVisible)
        {
            btSearchPlan.Visible = isVisible;
            btCreateByGate.Visible = isVisible;
            btCreateManual.Visible = isVisible;
            btDelete.Visible = isVisible;
            butTransportPlan.Visible = !isVisible;
            btReturn.Visible = !isVisible;
            butAddGatePlan.Visible = !isVisible;
        }
        public void butAddGatePlan_Click(object sender, EventArgs e)
        {
            DOCReceive receive = new DOCReceive();
            ReceiveGeneral receiveGeneral = new ReceiveGeneral();
            string msg = string.Empty;
            if (string.IsNullOrEmpty(_GatePlanFrm.SampleName))
            {
                WriteTips(6, "SampleName is null", false);
                return;
            }
            DataRow row = _GatePlanFrm.GetSelectPlan;
            if (row != null)
            {
                receive.GatePlanID = row["VoucherID"].ToString();
                receive.Grade = "--";
                receive.State = "1";
                receive.SampleName = _GatePlanFrm.SampleName;
                receive.LOT_NO = _GatePlanFrm.LotNo;
                receive.TableName = row["TableName"].ToString();
                if(string.IsNullOrEmpty(row["FirstTime"].ToString()))
                {
                    WriteTips(6, "The Truck didn't come in", false);
                     return;
                }
                receive.SheetDate = Convert.ToDateTime(row["FirstTime"].ToString());
                receive.Remark = string.Format("{0}|{1}", row["VehicleNO"].ToString(), row["StorageNO"].ToString());
                
                if (biz.InsertReceive(receive, receiveGeneral, out msg))
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        WriteTips(6, msg, false);
                    }
                    else
                    {
                        QueryGatePlan();
                    }
                }
                else
                {
                    WriteTips(6, msg, false);
                }
            }
            else {
                WriteTips(6, "Please select row", false);
            }
        
        }
        public void btReturn_Click(object sender, EventArgs e)
        {
            SearchVoucherReceive();
            if (step != Step.QueryPlanStep)
            {
                step = Step.QueryPlanStep;
            }

           
        }
        public void butTransportPlan_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(_GatePlanFrm.LotNo))
            {
                WriteTips(6, "MaterialNo can not null!", false);
                return;
            }
            if (_GatePlanFrm.B == null)
            {
                WriteTips(6, "Start Date can not null!", false);
                return;
            }
            QueryGatePlan();
        }
        private void QueryGatePlan() {
            DataTable dt = report.GetQCReport("QC02_Gate_Plan", new string[] { "B", "E", "LotNo", "PlanID" }, new object[] { _GatePlanFrm.B, _GatePlanFrm.E, _GatePlanFrm.LotNo, _GatePlanFrm.PlanID }).Tables[0];

            _GatePlanFrm.dtGatePlans = dt;
        }
        private void btCreateByGate_Click(object sender, EventArgs e)
        {
            if (step != Step.GatePlanStep)
            {
                step = Step.GatePlanStep;
            }
        

        }
        public void btDelete_Click(object sender, EventArgs e)
        {

            DataRow row =_QueryReceivePlan.GetVoucherID;
            if (row != null)
            {
                string voucherId = row["VoucherID"].ToString();
                if (string.IsNullOrEmpty(voucherId)) {
                    WriteTips(8, "VoucherId is null", false);
                }
                 DialogTitle = "Enter reason to delte:";
                 if (IsNotarize)
                 {
                     if (string.IsNullOrEmpty(DialogMsg))
                     {
                         WriteTips(8, "reason can  not null", false);
                         return;
                     }
                     string msg = string.Empty;
                     if (biz.DeleteReceive(voucherId, DialogMsg, out msg))
                     {

                         WriteTips(5, "Delete Succuess");
                     }
                     else
                     {
                         WriteTips(8, msg+"DELETE Failed ,Status ERROR", false);
                     }
                 }


            }
            else {
                WriteTips(8, "Select Row", false);
            }

        }
        private void btSearchPlan_Click(object sender, EventArgs e)
        {
            SearchVoucherReceive();
        }
        private void SearchVoucherReceive()
        {
            _QueryReceivePlan.dtVoucherReceive = report.GetQCReport("QC02_ReceivePlans", new string[] { "B", "E", "LotNo", "VoucherID" }, new object[] { _QueryReceivePlan.B, _QueryReceivePlan.E, _QueryReceivePlan.Material, _QueryReceivePlan.VoucherID }).Tables[0];
        }

        public string DialogMsg
        {
            get
            {
                return remark.Msg;
            }
        }

        public string DialogTitle
        {
            set
            {
                remark.Text = value;
            }
        }

        public bool IsNotarize
        {
            get
            {
                remark.ShowDialog();
                return remark.RValue;
            }
        }

    }

    public enum Step
    {
        QueryPlanStep, GatePlanStep
    }
}
