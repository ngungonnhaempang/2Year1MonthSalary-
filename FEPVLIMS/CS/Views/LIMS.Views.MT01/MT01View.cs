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
using MIS.Utility;
using LIMS.Service;

namespace LIMS.Views
{
    public partial class MT01View : UserControl
    {
        public MT01View()
        {
            InitializeComponent();
            RegisterCommand();
        }

        private void RegisterCommand()
        {
            this.Load += MT01View_Load;
            gridView1.Click += gridView1_Click;
            gridView1.DoubleClick += gridView1_DoubleClick;
            cmbTypeName.SelectedIndexChanged += cmbTypeName_SelectedIndexChanged;
            cmbLabName.SelectedIndexChanged += cmbLabName_SelectedIndexChanged;
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;
        }

        void MT01View_Load(object sender, EventArgs e)
        {
            LabNameLoad = reports.GetQCReport("QCW1_QueryLabName", new string[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];

            TypeNameLoad = reports.GetQCReport("QCW1_QueryTypeName", new string[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];
        }

        void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Dictionary<string, object> paramenters = new Dictionary<string, object>();

            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName]);
            }

            if (eventShowDetails != null)
            {
                _sample.SampleName = (string)paramenters["SampleName"].ToString();
                _sample.LOT_NO = (string)paramenters["LOT_NO"].ToString();
                _sample.Description_EN = (string)paramenters["Description_EN"].ToString();
                _sample.Description_TW = (string)paramenters["Description_TW"].ToString();
                _sample.Description_CN = (string)paramenters["Description_CN"].ToString();
                _sample.Description_VN = (string)paramenters["Description_VN"].ToString();

                eventShowDetails(this, new EgateArgs { EgateDictionary = paramenters });
            }
        }

        void gridView1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> paramenters = new Dictionary<string, object>();

            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName]);
            }

            if (eventClickDetails != null)
            {
                _sample.SampleName = (string)paramenters["SampleName"].ToString();
                _sample.LOT_NO = (string)paramenters["LOT_NO"].ToString();
                _sample.Description_EN = (string)paramenters["Description_EN"].ToString();
                _sample.Description_TW = (string)paramenters["Description_TW"].ToString();
                _sample.Description_CN = (string)paramenters["Description_CN"].ToString();
                _sample.Description_VN = (string)paramenters["Description_VN"].ToString();

                eventClickDetails(this, new EgateArgs { EgateDictionary = paramenters });
            }
        }

        private void GetOnClick()
        {
            Dictionary<string, object> paramenters = new Dictionary<string, object>();

            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName]);
            }

            if (eventShowDetails != null)
            {
                _sample.SampleName = (string)paramenters["SampleName"].ToString();
                _sample.LOT_NO = (string)paramenters["LOT_NO"].ToString();
                _sample.Description_EN = (string)paramenters["Description_EN"].ToString();
                _sample.Description_TW = (string)paramenters["Description_TW"].ToString();
                _sample.Description_CN = (string)paramenters["Description_CN"].ToString();
                _sample.Description_VN = (string)paramenters["Description_VN"].ToString();

                eventShowDetails(this, new EgateArgs { EgateDictionary = paramenters });
            }

        }
        
        public void FillSamples()
        {
            DataTable dt = reports.GetQCReport("QCW1_QuerySample", new string[] { "LabID", "TypeID" }, new object[] { LabID, TypeID, MyLanguage.Language }).Tables[0];
            SampleNameLoad = dt;
        }

        QCReporting reports = new QCReporting();

        public int LabID
        {
            get
            {
                if (cmbLabName.SelectedItem != null)
                    return Convert.ToInt32(((DataRowView)cmbLabName.SelectedItem).Row["LabID"].ToString());
                return 0;
            }
        }

        public int TypeID
        {
            get
            {
                if (cmbTypeName.SelectedItem!=null)
                    return Convert.ToInt32(((DataRowView)cmbTypeName.SelectedItem).Row["TypeID"].ToString());
                return 0;
            }
        }

        public string Sample
        {
            get
            {
                return cmbSample.SelectedValue.ToString();
            }
        }

        public string LOT_NO
        {
            get
            {
                return txtLOT_NO.Text.Trim();
            }
        }

        public DataTable tableMList
        {
            get
            {
                return new DataTable();
            }
            set
            {
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }

        public DataTable LabNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["LabName"] = "";
                dr["LabID"] = "0";
                value.Rows.InsertAt(dr, 0);

                this.cmbLabName.DataSource = value;
                this.cmbLabName.DisplayMember = "LabName";
                this.cmbLabName.ValueMember = "LabID";
            }
        }

        public DataTable TypeNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["TypeName"] = "";
                dr["TypeID"] = "0";
                value.Rows.InsertAt(dr, 0);

                this.cmbTypeName.DataSource = value;
                this.cmbTypeName.DisplayMember = "TypeName";
                this.cmbTypeName.ValueMember = "TypeID";

            }
        }

        public DataTable SampleNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["SampleName"] = "";
                value.Rows.InsertAt(dr, 0);

                this.cmbSample.DataSource = value;
                this.cmbSample.DisplayMember = "Description_" + MyLanguage.Language;
                this.cmbSample.ValueMember = "SampleName";
            }
        }

        public event EventHandler eventShowDetails;
        public event EventHandler eventClickDetails;

        private QCMaterial _sample = new QCMaterial();
        public QCMaterial SampleTemp
        {
            get { return _sample; }
            set { _sample = value; }
        }

        private void cmbLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSamples();
        }

        private void cmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillSamples();
        }

        public event EventHandler eventcmbSample_SelectedIndexChanged;

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbSample_SelectedIndexChanged != null)
                eventcmbSample_SelectedIndexChanged(sender, e);
        }

    }
}
