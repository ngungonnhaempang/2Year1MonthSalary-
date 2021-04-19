using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLanuage;
using LIMS.BLL;
using LIMS.Model;

namespace LIMS.Views.QC03
{
    public partial class FormEditer :  Infrastructure.StyleForm
    {
        DOCPlanAddBiz _planAddBiz = new DOCPlanAddBiz();
        QCReporting reports = new QCReporting();
           DOCReceiveBiz _docReceiveBiz = new DOCReceiveBiz();
        public FormEditer()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormEditer_Load);
            btAccept.Click += new EventHandler(btAccept_Click);
            btReject.Click += new EventHandler(btReject_Click);
            btCancel.Click += new EventHandler(btCancel_Click);
            gridView1.Click += new EventHandler(gridView1_Click);

            #region language
            try
            {
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QA23", this.Name);
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    gridView1.BestFitColumns();
                    CultureLanuage.GridResourcesFrom(gridView1, "gridView2", dsgrid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            ClearPlanAddSelect();
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            PlanAddID = (int)row["VoucherID"];
            SampleName = (string)row["SampleName"];
            LOT_NO = (string)row["LOT_NO"];
            LINE = (string)row["LINE"];
            ProdDate = Convert.ToDateTime(row["ProdDate"]);
            SheetDate = Convert.ToDateTime(row["SheetDate"]);
            SampleID = (string)row["SampleID"];
            CreateType = Convert.ToInt32(row["CreateType"].ToString());
            CompanyOffer = row["CompanyOfferSample"].ToString();
            CompanyProd = row["CompanyProduce"].ToString();
            GradeVersion = row["GradeVersion"].ToString();
        }

        void FormEditer_Load(object sender, EventArgs e)
        {
            QueryAddPlan();
            ClearPlanAddSelect();
        }

     
        void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btReject_Click(object sender, EventArgs e)
        {
            if (PlanAddID == -1)
            {
                WriteTips(8, "NO Select Row", false);
                return;
            }
            Console.WriteLine("btReject_Click");
            Console.WriteLine(PlanAddID.ToString());

            if (_planAddBiz.RejectPlandAdd(PlanAddID.ToString()))
            {
                QueryAddPlan();
         
                WriteTips(8, "ADD VoucherID Reject:" + PlanAddID.ToString(), false);
            }
            else
            {
                WriteTips(8, "Failure to reject test documents", false);
            }
        }

        void btAccept_Click(object sender, EventArgs e)
        {
            if (PlanAddID == -1)
            {
                WriteTips(8, "NO SELECT ROW", false);
                return;
            }
            Console.WriteLine("btAccept_Click");
            Console.WriteLine(PlanAddID.ToString() + "|" + SampleName + "|" + LOT_NO + "|" + LINE + "|" + ProdDate.ToString() + "|" + SampleID);
            string voucherid = "";

            DOCPlan panAdd = new DOCPlan();
            panAdd.SampleName = SampleName;
            
            panAdd.LOT_NO = LOT_NO;
            panAdd.LINE = LINE;
            panAdd.ProdDate = ProdDate;
            panAdd.SheetDate = SheetDate;
            panAdd.State = "1";
            panAdd.Grade = "A";
            panAdd.SampleID = SampleID;
            panAdd.createType = CreateType;
            panAdd.companyProd = CompanyProd;
            panAdd.companyOffer = CompanyOffer;
            panAdd.GradeVersion = GradeVersion;
            if (_planAddBiz.InsertPlanByAdd(panAdd, PlanAddID, out voucherid))
            {
                QueryAddPlan();

                WriteTips(8, "add VoucherID:" + voucherid, true);
            }
            else
            {
                WriteTips(8, "Receipt of test documents failed", false);
            }
        }

      

        void QueryAddPlan()
        {
            tableDraftList = reports.Reporting("QC03_QueryAddPlan", new string[] { }, new object[] { }).Tables[0];
        }

        private string mes = "";
        public string FormEditMes
        {
            set
            {
                mes = value;
            }
            get
            {
                return mes;
            }
        }

        /// <summary>
        /// 委托单
        /// </summary>
        public DataTable tableDraftList
        {
            set
            {
               
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }

        public int PlanAddID { get; set; }
        public string SampleName { get; set; }
        public string LOT_NO { get; set; }
        public string LINE { get; set; }
        public DateTime ProdDate { get; set; }

        public DateTime SheetDate { get; set; }
        public string SampleID { get; set; }

        public int CreateType { get; set; }
        public string CompanyOffer { get; set; }
        public string CompanyProd { get; set; }
        public string PoyLotNo { get; set; }

        public string GradeVersion { get; set; }

        /// <summary>
        /// 清空选择项
        /// </summary>
        void ClearPlanAddSelect()
        {
            PlanAddID = -1;
        }
    }
}
