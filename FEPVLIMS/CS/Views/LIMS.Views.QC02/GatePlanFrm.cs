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
using DevExpress.XtraEditors;
using BasicLanuage;
using MIS.Utility;
using LIMS.BLL;

namespace LIMS.Views.QC02
{
    public partial class GatePlanFrm : UserControl
    {

        QCReporting report = new QCReporting();
        public GatePlanFrm()
        {
            InitializeComponent();
            gridView1.Click+=gridView1_Click;
            cmbSampleName.EditValueChanged+=cmbSampleName_EditValueChanged;
         
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
        private void cmbSampleName_EditValueChanged(object sender, EventArgs e)
        {
            //QC02_QueryMaterialList
            DataTable dt = report.GetQCReport("QC02_QueryMaterialList", new string[] { "SampleName" },
                                             new object[] { SelectSampleName}).Tables[0];
            MaterialSource = dt;
        }
        public string SelectSampleName {

            get { return cmbSampleName.EditValue.ToString(); }
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
        public void gridView1_Click(object sender, EventArgs e)
        {

            //int rowCount = gridView1.SelectedRowsCount;
            //if (rowCount != 1)
            //    return;

            //DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            //VoucherID = row["VoucherID"].ToString();
            //SampleName = row["SampleName"].ToString();
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
        public string PlanID
        {
            get { return txtGatePlanID.Text.Trim(); }
        }
        public string LotNo
        {
            get
            {
                return cmbGateMaterial.Text.Trim();

            }

        }
        public string SampleName { set; get; }

        public DataTable dtGatePlans
        {
            set
            {
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }
        public DataRow GetSelectPlan
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

        private void cmbGateMaterial_EditValueChanged(object sender, EventArgs e)
       {
           LookUpEdit edit = sender as LookUpEdit;
           if (edit.EditValue != null)
           {
               //取资料行，数据源为DataTable, 资料行是DataRowView对象。   
               object o = edit.Properties.GetDataSourceRowByKeyValue(edit.EditValue);
               if (o is DataRowView)
               {
                   DataRowView rv = o as DataRowView;
                   SampleName = rv.Row["SampleName"].ToString();

               }
           }  

          
        }
    }
}
