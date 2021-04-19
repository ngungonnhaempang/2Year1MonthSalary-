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
using BasicLanuage;
namespace LIMS.Views
{
    public partial class SPC1View : UserControl
    {
        public SPC1View()
        {
            InitializeComponent();
            gridView1.Click+=gridView1_Click;
            gridView1.DoubleClick += new EventHandler(gridView1_DoubleClick);
            cmbTypeName.SelectedIndexChanged += cmbTypeName_SelectedIndexChanged;
            cmbLabName.SelectedIndexChanged += cmbLabName_SelectedIndexChanged;
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;
            try
            {
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "SPC1", "SPC1View");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView1, "gridView1", dsgrid);
                    this.gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public event EventHandler eventShowDetails;
        QCReporting reports = new QCReporting();

        void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            SelectedChart = new QCChart{SampleName=row["SampleName"].ToString(), Property = row["PropertyName"].ToString()};
        }

        void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            Dictionary<string, object> paramenters = new Dictionary<string, object>();
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName]);
            }

            if (eventShowDetails != null)
            {
                eventShowDetails(this, new EgateArgs { EgateDictionary = paramenters });
            }
        }
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
                if (cmbTypeName.SelectedItem != null)
                    return Convert.ToInt32(((DataRowView)cmbTypeName.SelectedItem).Row["TypeID"].ToString());
                return 0;
            }
        }

        public string Sample
        {
            get
            {
                if (cmbSample.SelectedValue != null)
                {
                    return cmbSample.SelectedValue.ToString();
                }
                else {
                    return "";
                }
               
            }
        }

        public string LOT_NO
        {
            get
            {
                return cmbLottoNo.Text;
            }
        }

        public string LINE
        {
            get
            {
                return cmbLineNo.Text;
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
                cmbLabName.DataSource = value;
                cmbLabName.DisplayMember = "LabName";
                cmbLabName.ValueMember = "LabName";              
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

                cmbTypeName.DataSource = value;
                cmbTypeName.DisplayMember = "TypeName";
                cmbTypeName.ValueMember = "TypeID";
            }
        }

        public DataTable SampleNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["SampleName"] = "";
                value.Rows.InsertAt(dr, 0);

                cmbSample.DataSource = value;
                switch (MyLanguage.Language)
                {
                    case "CN":
                        cmbSample.DisplayMember = "Description_CN";
                        break;
                    case "EN":
                        cmbSample.DisplayMember = "Description_EN";
                        break;
                    case "TW":
                        cmbSample.DisplayMember = "Description_TW";
                        break;
                    case "VN":
                        cmbSample.DisplayMember = "Description_VN";
                        break;

                }
                cmbSample.ValueMember = "SampleName";
            }
        }

        public DataTable LottoNoLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["LOT_NO"] = "";
                value.Rows.InsertAt(dr, 0);

                cmbLottoNo.DataSource = value;
                cmbLottoNo.DisplayMember = "LOT_NO";
                cmbLottoNo.ValueMember = "LOT_NO";
            }
        }

        public DataTable LineNoLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["LINE"] = "";
                value.Rows.InsertAt(dr, 0);

                cmbLineNo.DataSource = value;
                cmbLineNo.DisplayMember = "LINE";
                cmbLineNo.ValueMember = "LINE";
            }
        }

        public DataTable tableMList
        {
            set
            {
                gridControl1.DataSource = value;
                //gridView1.Columns["LastModify"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                //gridView1.Columns["LastModify"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                gridView1.BestFitColumns();
            }
        }

        public QCChart SelectedChart { get; set; }

        public event EventHandler eventcmbLabName_SelectedIndexChanged;
        private void cmbLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbLabName_SelectedIndexChanged != null)
                eventcmbLabName_SelectedIndexChanged(sender, e);
        }

        public event EventHandler eventcmbTypeName_SelectedIndexChanged;
        private void cmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbTypeName_SelectedIndexChanged != null)
                eventcmbTypeName_SelectedIndexChanged(sender, e);
        }

        public event EventHandler eventcmbSample_SelectedIndexChanged;
        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbSample_SelectedIndexChanged != null)
                eventcmbSample_SelectedIndexChanged(sender, e);
        }
    }
}
