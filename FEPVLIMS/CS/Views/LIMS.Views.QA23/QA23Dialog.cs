using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIMS.BLL;
using LIMS.Model;
using LIMS.Service;

namespace LIMS.Views
{
    public partial class QA23Dialog : Infrastructure.StyleForm
    {
        public QA23Dialog()
        {
            InitializeComponent();
            
         //    ClearPlanAddSelect();
            btnAccept.Click += btnAccept_Click;
            btnClose.Click += btnClose_Click;
            btnReject.Click += btnReject_Click;
        }

        void btnReject_Click(object sender, EventArgs e)
        {
            if (AcceptRejectEvent != null)
                AcceptRejectEvent(false, e);
           // this.Close();
            //if (planaddbiz.RejectPlandAdd(PlanADD.AddID))
            //{

            //    MessageBox.Show("加测单据[" + PlanADD.AddID + "]已否决");
            //}
            //else
            //{
            //    MessageBox.Show("加测单据否决失败");
            //}
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public EventHandler AcceptRejectEvent;
        void btnAccept_Click(object sender, EventArgs e)
        {
            if(AcceptRejectEvent !=null)
                AcceptRejectEvent(true, e);
            this.Close();
            //Rvalue = false;
            //string voucherid = "";
            //if (planaddbiz.InsertPlanByAdd(doc, PlanADD.AddID, out voucherid))
            //{

            //    MessageBox.Show("加测单据[" + voucherid + "]已接收");
            //}
            //else
            //{
            //    MessageBox.Show("加测单据接收失败");
            //}
        }
        
        DOCPlanAdd docplan = new DOCPlanAdd();
        DOCPlanAddBiz planaddbiz = new DOCPlanAddBiz();
        DOCPlan doc = new DOCPlan();
        QCReporting reports = new QCReporting();
        AttributeBiz biz = new AttributeBiz();


        public DOCPlan Plan
        {
            get
            {
                return new DOCPlan
                {
                    SampleName = txtSample.Text.Trim(),
                    VoucherID = txtVoucherID.Text.Trim(),
                    ProdDate = Convert.ToDateTime(txtProdDate.Text.Trim()),
                    SampleID = txtSample.Text.Trim(),
                    LINE = txtLine.Text.Trim(),
                    LOT_NO = txtLot_No.Text.Trim(),
                    //CompanyOffer = txtCOffer.Text.Trim(),
                    //CompanyProd = txtCProduce.Text.Trim(),
                    //Purpose = txtPurpose.Text.Trim()
                };
            }
            set
            {
                txtSample.Text = value.SampleName;
                txtVoucherID.Text = value.VoucherID;
                txtSampleID.Text = value.SampleID;
                txtLine.Text = value.LINE;
                txtLot_No.Text = value.LOT_NO;
                txtProdDate.Text = value.ProdDate.ToString();
                //txtCOffer.Text = value.CompanyOffer;
                //txtCProduce.Text = value.CompanyProd;
                //txtPurpose.Text = value.Purpose;
            }
        }
  
        //void ClearPlanAddSelect()
        //{
        //    PlanADD.AddID = "-1";
        //}
            
    }
}
