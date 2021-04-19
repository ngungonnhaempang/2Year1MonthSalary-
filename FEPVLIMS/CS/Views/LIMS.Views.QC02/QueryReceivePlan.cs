using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using BasicLanuage;
using MIS.Utility;
using LIMS.BLL;

namespace LIMS.Views.QC02
{
    public partial class QueryReceivePlan : UserControl
    {
        public QueryReceivePlan()
        {
            InitializeComponent();

            dateB.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cmbSampleName.EditValueChanged += cmbSampleName_EditValueChanged;
            #region language
            try
            {
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QC22", this.Name);
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    gridView1.BestFitColumns();
                    CultureLanuage.GridResourcesFrom(gridView1, "gridView1", dsgrid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
            #endregion

        }

        public string Material {

            get {
                try
                {
                    return cmbGateMaterial.EditValue.ToString();
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
        }
        public DataTable MaterialSource
        {
            set
            {
                cmbGateMaterial.Properties.DataSource = value;
                cmbGateMaterial.Properties.DisplayMember = "LOT_NO";
                cmbGateMaterial.Properties.ValueMember = "LOT_NO";

            }
        }
        private void cmbSampleName_EditValueChanged(object sender, EventArgs e)
        {
            //QC02_QueryMaterialList
            QCReporting report = new QCReporting();
            DataTable dt = report.GetQCReport("QC02_QueryMaterialList", new string[] { "SampleName" },
                                             new object[] { SelectSampleName }).Tables[0];
            MaterialSource = dt;
        }
        public string SelectSampleName
        {

            get { return cmbSampleName.EditValue.ToString(); }
        }
        public DataRow GetVoucherID
        {
            get
            {

                ArrayList rows = new ArrayList();
                // Add the selected rows to the list.
                int rowCount = gridView1.SelectedRowsCount;
                if (rowCount != 1)
                    return null;

                if (gridView1.GetSelectedRows()[0] >= 0)
                    rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[0]));

                return (DataRow)rows[0];//((DataRow)rows[0])["VoucherID"].ToString();
            }
        }
        public DateTime? B
        {
            get
            {
                if (this.dateB.Text == "")
                    return null;
                else
                    return Convert.ToDateTime(dateB.Text);
            }

        }

        public DateTime? E
        {
            get
            {
                if (this.dateE.Text == "")
                    return null;
                else
                    return Convert.ToDateTime(dateE.Text);
            }

        }
        public string VoucherID
        {
            get { return txtVoucherID.Text.Trim(); }
        }
      

      
       
        public DataTable SetSamples
        {
            set
            {
                cmbSampleName.Properties.DataSource = value;
                cmbSampleName.Properties.DisplayMember = "Description";
                cmbSampleName.Properties.ValueMember = "SampleName";

            }
        }
        public DataTable dtVoucherReceive
        {
            set
            {
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }
    }
}
