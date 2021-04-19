using System;
using System.Data;
using LIMS.BLL;
using MIS.Utility;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class QE31 : Infrastructure.BaseForm
    {
        QCReporting report = new QCReporting();
        QE31View _QE31View = new QE31View();
        QE31CU _QE31CU = new QE31CU();
        QE31Edit _QE31Edit = new QE31Edit();
        ProfileBiz biz = new ProfileBiz();
        DOCBiz doc = new DOCBiz();
   
        public QE31()
        {
            InitializeComponent();
            Init();
            //CultureLanuage.ApplyResourcesFrom(this, "QE31", "QE31");
          
        }


        public bool IsLanguage { set; get; }
        void Init()
        {
            btSearchPlan.Click += btSearchPlan_Click;
            btRecFileUpload.Click += btRecFileUpload_Click;
			btnDeleteFile.Click += BtnDeleteFile_Click;
            this.Load+=QE31_Load;
            btEditMaterial.Click += btEditMaterial_Click;
            btnUploadResult.Click += BtnUploadResult_Click;
            _QE31Edit.eventShowMessage += (o, s) => { WriteTips(5, (string)o); };
            _QE31View.DOCTypeSource = report.GetQCReport("QE31_GetDOCType", new string[] { "Lan" }, new object[] { MIS.Utility.MyLanguage.Language}).Tables[0];
            deckWorkspace1.Show(_QE31View);
        }

		private void BtnDeleteFile_Click(object sender, EventArgs e)
		{
			
            DeleteFileUpdate();
		}

		private void BtnUploadResult_Click(object sender, EventArgs e)
        {
            UploadResult();
        }

        private void QE31_Load(object sender, EventArgs e)
        {
           
            if (IsLanguage)
            {
                #region Language

                this.TabText = CultureLanuage.Translator(this.Name, 0, "Result Input [QE31]");
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(_QE31View, "QE31", "QE31View");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    _QE31View.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(_QE31View.gridView1, "gridView1", dsgrid);
                    _QE31View.gridView1.BestFitColumns();
                }

                gridData = CultureLanuage.GridHeader(dsgrid, "gridView2");
                if (gridData != null)
                {
                    _QE31View.gridControl2.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(_QE31View.gridView2, "gridView2", dsgrid);
                    _QE31View.gridView2.BestFitColumns();
                }

                #endregion
            }
        }
        private void btEditMaterial_Click(object sender, EventArgs e)
        {
            EditMaterial();
        }

        private void btSearchPlan_Click(object sender, EventArgs e)
        {
            SearchPlan();
        }

       

       

        void btRecFileUpload_Click(object sender, EventArgs e)
        {
            RecFileUpload();
        }

      

    }
}
