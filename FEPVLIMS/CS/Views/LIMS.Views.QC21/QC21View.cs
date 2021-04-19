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
using System.Collections;
using DevExpress.XtraEditors.Controls;
using MIS.Utility;
using LIMS.Model;
using LIMS.Views.QC21Members;

namespace LIMS.Views
{
    public partial class QC21View : UserControl
    {
        public QC21View()
        {
            InitializeComponent();

            gridView1.RowCellClick += gridView1_RowCellClick;
            cmbSampleName.EditValueChanged += cmbSampleName_EditValueChanged;
            cmbMaterial.EditValueChanged += cmbMaterial_EditValueChanged;
            cmbTypeName.SelectionChangeCommitted+=cmbTypeName_SelectionChangeCommitted;

        }

        public event EventHandler eventSampleMaterial;
        public event EventHandler eventMaterialGetPlan;



        public int Catelag {

            get { return int.Parse(cmbTypeName.SelectedValue.ToString()); }
        }


        public DataTable dtListTypeName
        {
            set
            {
                cmbTypeName.DataSource = value;
                cmbTypeName.DisplayMember = "TypeName";
                cmbTypeName.ValueMember = "TypeID";
            }
        }
        public DataTable SetSamples
        {
            set
            {
                cmbSampleName.Properties.DataSource = value;
                cmbSampleName.Properties.DisplayMember = string.Format("{0}_{1}", "Description", MyLanguage.Language);
                cmbSampleName.Properties.ValueMember = "SampleName";
             
            }
        }
        public DataTable SetMaterial
        {
            set
            {
                cmbMaterial.Properties.DataSource = value;
                cmbMaterial.Properties.DisplayMember = string.Format("{0}_{1}", "Description", MyLanguage.Language);
                cmbMaterial.Properties.ValueMember = "LOT_NO";
   
            }
        }
        public void InitForm()
        {
          
            LookUpColumnInfoCollection cols = cmbSampleName.Properties.Columns;
            cols.Add(new LookUpColumnInfo("SampleName", 0));
            cols.Add(new LookUpColumnInfo("Description_" + MIS.Utility.MyLanguage.Language, 0));
            cmbSampleName.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            cmbSampleName.Properties.SearchMode = SearchMode.AutoComplete;
            cmbSampleName.Properties.AutoSearchColumnIndex = 1;

            LookUpColumnInfoCollection coll = cmbMaterial.Properties.Columns;
            coll.Add(new LookUpColumnInfo("LOT_NO", 0));
            coll.Add(new LookUpColumnInfo("Description_" + MIS.Utility.MyLanguage.Language, 0));
            cmbMaterial.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            cmbMaterial.Properties.SearchMode = SearchMode.AutoComplete;
            cmbMaterial.Properties.AutoSearchColumnIndex = 1;
        }


        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CellValue.ToString())) Clipboard.SetText(e.CellValue.ToString());
        }
        private void cmbTypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Console.WriteLine("select type");
            QCReporting report = new QCReporting();

            SetSamples = report.GetQCReport("Q_SamplesByCategory", new[] { "TypeID" }, new object[] { Catelag }).Tables[0];
        }
        private void cmbMaterial_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbSampleName.EditValue.ToString()))
            {
                return;
            }

            this.eventMaterialGetPlan(this, new ShowDetailsArg { ID = cmbMaterial.EditValue.ToString() });
        }

        private void cmbSampleName_EditValueChanged(object sender, EventArgs e)
        {
             if(string.IsNullOrEmpty(cmbSampleName.EditValue.ToString()))
               return ;

            if (eventSampleMaterial != null)
                 eventSampleMaterial(this, new ShowDetailsArg { ID = cmbSampleName.EditValue.ToString() });
        }

        public string SampleName
        {
            get
            {
                try
                {
                    return this.cmbSampleName.EditValue.ToString();
                }
                catch
                {
                    return "";
                }

            }
        }
        public string LotNO
        {
            get
            {
                try
                {
                    return cmbMaterial.EditValue.ToString();
                }
                catch
                {
                    return "";
                }


            }
        }

      
        public string SelectVoucherID
        {
            get
            {
                if (gridView1.SelectedRowsCount != 1)
                    return "";
                else
                    return gridView1.GetDataRow(gridView1.GetSelectedRows()[0])["VoucherID"].ToString();
            }
        }
        //public string BarCode
        //{
        //    get
        //    {
        //        if (gridView1.SelectedRowsCount != 1)
        //            return "";
        //        else
        //            return gridView1.GetDataRow(gridView1.GetSelectedRows()[0])["BarCode"].ToString();
        //    }
        //}


        DataTable dtMaterial = new DataTable();
        public DataTable MaterialSource
        {
            set
            {
                dtMaterial.Clear();
                DataRow row = dtMaterial.NewRow();
                dtMaterial.Rows.Add(row);
                dtMaterial.Merge(value);
                cmbMaterial.Properties.DataSource = dtMaterial;
                cmbMaterial.Properties.DisplayMember = "LOT_NO";
                cmbMaterial.Properties.ValueMember = "ID";
                cmbMaterial.ItemIndex = 0;
            }
        }
        public DataTable dtGrid
        {
            set
            {
                gridControl1.DataSource = value;
              
                gridView1.BestFitColumns();
            }
        }

        public string VoucherID {

            get { return this.txtVoucherID.Text.Trim(); }
        }      

        public List<DOCPlan> GetMultipleSelectionProducts()
        {

            List<DOCPlan> dOCPlans = new List<DOCPlan>();
            // Add the selected rows to the list. 
            Int32[] selectedRowHandles = gridView1.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                int selectedRowHandle = selectedRowHandles[i];
                if (selectedRowHandle >= 0)
                {
                    var currentDoc = gridView1.GetDataRow(selectedRowHandle);
                    DOCPlan doc = new DOCPlan();
                    doc.SampleName = SampleName;
                    doc.LOT_NO = LotNO;
                    doc.LINE = currentDoc["Line"].ToString();
                    doc.Grade = "A";
                    doc.grades = currentDoc["Grades"].ToString();
                    doc.SampleID= currentDoc["BarCode"].ToString() ;                
                    dOCPlans.Add(doc);
                }
                  
            }
            return dOCPlans;
           
        }
    }

    public class ShowDetailsArg : EventArgs
    {
        public string ID { get; set; }
    }
}
