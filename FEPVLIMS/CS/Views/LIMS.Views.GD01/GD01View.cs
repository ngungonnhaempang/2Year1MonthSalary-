using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using LIMS.BLL;
using LIMS.Model;
using MIS.Utility;
using System.Text;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class GD01View : UserControl
    {
        public GD01View()
        {
            InitializeComponent();

            #region language
            DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "GD01", this.Name);
            DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
            if (gridData != null)
            {
                this.gridControl1.DataSource = gridData;
                gridView1.BestFitColumns();
                CultureLanuage.GridResourcesFrom(gridView1, "gridView1", dsgrid);
            }
            #endregion

            RegisterCommand();
        }

        private void RegisterCommand()
        {
            cmbTypeName.SelectedIndexChanged += cmbTypeName_SelectedIndexChanged;
            cmbSampleName.EditValueChanged += cmbSampleName_EditValueChanged;
            cmbMaterial.EditValueChanged += cmbMaterial_EditValueChanged;
        }
        
        private QCReporting report = new QCReporting();
        private void cmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtListSampleName = report.GetQCReport("GD01_N_GetSampleName",
                new[] { "LabID", "TypeID", "Language" },
                new object[] { LabID, TypeID, MyLanguage.Language }).Tables[0];
        }

        private void cmbSampleName_EditValueChanged(object sender, EventArgs e)
        {
            dtListMaterial = report.GetQCReport("GD01_N_GetMaterial",
                new[] { "SampleName", "Language" },
                new object[] { SampleName, MyLanguage.Language }).Tables[0];
        }

        private void cmbMaterial_EditValueChanged(object sender, EventArgs e)
        {
            QueryGrade();
        }

        public DataTable dtListLabName
        {
            set
            {
                cmbLabName.DataSource = value;
                cmbLabName.DisplayMember = "LabName";
                cmbLabName.ValueMember = "LabID";
            }
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
        public DataTable dtListSampleName
        {
            set
            {
                cmbSampleName.Properties.DataSource = value;
                cmbSampleName.Properties.DisplayMember = "Description";
                cmbSampleName.Properties.ValueMember = "SampleName";
            }
        }
        public DataTable dtListMaterial
        {
            set
            {
                cmbMaterial.Properties.DataSource = value;
                cmbMaterial.Properties.DisplayMember = "Description";
                cmbMaterial.Properties.ValueMember = "LOT_NO";
            }
        }
        public DataTable dtGradeData
        {
            set
            {
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }        

        public void QueryGrade()
        {
            if (IsReady)
            {
                dtGradeData = report.GetQCReport("GD01_N_QueryGradeList", new[] { "SampleName", "LOT_NO", "IsALL" },
                    new object[] { SampleName, MaterialName, chkIsALL.Checked }).Tables[0];
            }
            else
                MessageBox.Show(_msg, "Information");
        }

        string _msg = "";
        public bool IsReady
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (string.IsNullOrEmpty(cmbSampleName.Text.ToString()))
                    msg.Append("SampleName is null");
                if (string.IsNullOrEmpty(cmbMaterial.Text.ToString()))
                    msg.Append("/Material is null");

                _msg = "" + msg;

                if (_msg == "")
                    return true;
                else
                    return false;
            }  
        }
     
        public int LabID
        {
            get
            {
                return Convert.ToInt32(cmbLabName.SelectedValue);
            }
        }

        public int TypeID
        {
            get
            {
                return Convert.ToInt32(cmbTypeName.SelectedValue);
            }
        }

        public string SampleName
        {
            get
            {
                return cmbSampleName.EditValue.ToString();
            }
        }

        public string MaterialName
        {
            get
            {
                return cmbMaterial.EditValue.ToString();
            }
        }    
    }
}