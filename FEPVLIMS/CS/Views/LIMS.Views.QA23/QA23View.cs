using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIMS.BLL;
using LIMS.Model;
using System.Xml;
using LIMS.Service;

namespace LIMS.Views
{
    public partial class QA23View : UserControl
    {
        public QA23View()
        {
            InitializeComponent();
            InitLoad();
            RegisterCommand();
            _SelectedSampleName = "";

           

        }
        void InitLoad()
        {
            StatusLoad = reports.GetQCReport("QA23GetState", new string[] { "Lan", "Ctype" }, new object[] { _Language, "Plan" }).Tables[0];
            SampleNameLoad = reports.GetQCReport("Q_Samples", new string[] { "Query" }, new object[] { "" }).Tables[0];
            dateB.EditValue = DateTime.Now;
            dateE.EditValue = DateTime.Now;
        }
        public void RegisterCommand()
        {
            cmbSampleName.SelectedIndexChanged += cmbSampleName_SelectedIndexChanged;
            cmbMaterial.SelectedIndexChanged += cmbMaterial_SelectedIndexChanged;
            gridView1.Click += gridView1_Click;
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView2.Click += gridView2_Click;
            txtDraftID.KeyDown += txtDraftID_KeyDown;
            txtVoucherID.KeyDown += txtVoucherID_KeyDown;

            
        }
        public EventHandler eventgvdClick2;        
        void gridView2_Click(object sender, EventArgs e)
        {
            if (eventgvdClick2 != null)
                eventgvdClick2(sender, e);
        }
        public event KeyEventHandler eventVoucherIDKeyDown;
        void txtVoucherID_KeyDown(object sender, KeyEventArgs e)
        {
            if (eventVoucherIDKeyDown != null)
                eventVoucherIDKeyDown(sender, e);
        }
        public event KeyEventHandler eventDraftKeyDown;
        void txtDraftID_KeyDown(object sender, KeyEventArgs e)
        {
            if (eventDraftKeyDown != null)
                eventDraftKeyDown(sender, e);
         
        }

        void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            Dictionary<string, string> paramenters = new Dictionary<string, string>();
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName] == null ? "" : row[c.ColumnName].ToString());
            }
             if (eventShowDetails != null) {
                 GetPlan = new DOCPlan
                 {
                     VoucherID = paramenters["VoucherID"],
                     SampleName = paramenters["SampleName"],
                     //Purpose = paramenters["Purpose"],
                     ProdDate = Convert.ToDateTime(paramenters["ProdDate"]),
                     SampleID = paramenters["SampleID"],
                     LOT_NO = paramenters["LOT_NO"],
                     LINE = paramenters["LINE"],
                     //CompanyOffer = paramenters["CompanyOfferSample"] ,
                     //CompanyProd = paramenters["CompanyProduce"]
                 

                 };
                 eventShowDetails(sender, e);
                
             }
       
        }

        void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;
            _SelectedSampleName = gridView1.GetDataRow(gridView1.GetSelectedRows()[0])["VoucherID"].ToString();
        }

        public event EventHandler event_cmbMaterial;
        void cmbMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (event_cmbMaterial != null)
                event_cmbMaterial(sender, e);
        }
        public event EventHandler event_SampleName;
        void cmbSampleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (event_SampleName != null)
                event_SampleName(sender, e);
           
           
        }
        // Get LOT_NO
        public void FillMaterial()
        {
            DataTable dt = reports.GetQCReport("Q_Materials", new string[] { "SampleName", "Query" }, new object[] { Sample,"" }).Tables[0];
            MaterialLoad = dt;
        }

        QCReporting reports = new QCReporting();
        // GridView
        public DataTable tablePlanADD
        {
            set
            {
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }

        public string Sample
        {
            set
            {
                cmbMaterial.SelectedValue = "";
            }
            get
            {
                return cmbSampleName.SelectedValue.ToString();
            }
        }
        public string Materials
        {
          
            get
            {
                return cmbMaterial.Text.ToString();
            }
        }
        public string State {
            get { return cbState.SelectedValue.ToString(); }
        }
        public string TDraftID
        {
            set { txtVoucherID.Text.Trim(); }
            get { return txtDraftID.Text.Trim(); }
        }

        public string TVoucherID
        {
            set {  txtVoucherID.Text.Trim(); }
            get { return txtVoucherID.Text.Trim(); }
            
        }
        public DateTime DateB
        {
            
            get
            {
                if (string.IsNullOrEmpty(dateB.Text))
                {
                    return DateTime.Today;
                }
                return Convert.ToDateTime(dateB.Text);
            }
        }
        public DateTime DateE
        {
            get
            {
                if (string.IsNullOrEmpty(dateE.Text))
                {
                    return DateTime.Today;
                }
                return Convert.ToDateTime(dateE.Text);
            }
        }

        public DataTable SampleNameLoad
        {
            set
            {
                if (this.cmbSampleName.DataSource == value)
                    return;
                DataRow dr = value.NewRow();
                dr["SampleName"] = "";
                value.Rows.InsertAt(dr, 0);

                this.cmbSampleName.DataSource = value;
                this.cmbSampleName.DisplayMember = "Description_" + _Language;
                this.cmbSampleName.ValueMember = "SampleName";
            }
        }
        public DataTable StatusLoad
        {
            set
            {
                if (this.cbState.DataSource == value)
                    return;
                DataRow dr = value.NewRow();
                dr["State"] = "";
                value.Rows.InsertAt(dr, 0);

                this.cbState.DataSource = value;
                this.cbState.DisplayMember = "Remark";
                this.cbState.ValueMember = "State";
            }
        }
        public DataTable MaterialLoad
        {
            set
            {
                if (this.cmbMaterial.DataSource == value)
                    return;
                DataRow dr = value.NewRow();
                dr["LOT_NO"] = "";
                value.Rows.InsertAt(dr, 0);

                this.cmbMaterial.DataSource = value;
                this.cmbMaterial.DisplayMember = "LOT_NO";
                this.cmbMaterial.ValueMember = "LOT_NO";
            }
        }
        public event EventHandler eventcmbSample_SelectedIndexChanged;

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbSample_SelectedIndexChanged != null)
                eventcmbSample_SelectedIndexChanged(sender, e);
        }
        // Query 

        public void ReflashSampleAttribute()
        {
            int rowCount = gridView2.SelectedRowsCount;
            if (rowCount != 1)
            {
                QuerySampleAttribute("");
                return;
            }
            DataRow row = gridView2.GetDataRow(gridView2.GetSelectedRows()[0]);
            QuerySampleAttribute(row["VoucherID"].ToString());
        }
        public void QueryDraftPlan(string draftid, string voucherid, DateTime DateB, DateTime DateE, string State)
        {
            tableDraftList = reports.GetQCReport("QA23_QueryDraftPlan",
                new string[] { "DraftID", "VoucherID", "DateB", "DateE","State" },
                new object[] { draftid, voucherid, DateB, DateE,State }).Tables[0];
        }
       public void QuerySampleAttribute(string voucherid) { 
       tableSampleAttributeList = reports.GetQCReport("QA23_QuerySampleAttribute", 
           new string[]{"VoucherID"} ,new object[]{voucherid }).Tables[0];
       }
        public event EventHandler eventShowDetails;

        public DOCPlan GetPlan { get; set; }
        public string _SelectedSampleName { get; set; }
        #region --属性--
   
        public DataTable tableDraftList
        {
            set
            {
               // gridView2.Columns.Clear();
                gridControl2.DataSource = value;
                gridView2.BestFitColumns();
            }
        }

        public DataTable tableSampleAttributeList
        {
            set
            {
                gridView3.Columns.Clear();
                gridControl3.DataSource = value;
                gridView3.BestFitColumns();
            }
        }

        public string VoucherID { get; set; }

        public string DraftID { get; set; }

        public string DraftState { get; set; }
        string _Language
        {
            get
            {
                return MIS.Utility.MyLanguage.Language;
            }
        }

        #endregion
    }
}
