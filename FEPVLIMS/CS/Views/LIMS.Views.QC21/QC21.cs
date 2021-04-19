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
using DevComponents.DotNetBar;
using LIMS.BLL;
using LIMS.Views.QC21Members;
using MIS.Utility;

namespace LIMS.Views
{
    public partial class QC21 : Infrastructure.BaseForm
    {
        public QC21()
        {
            InitializeComponent();
            RegisterCommand();
            Init_Load();
            this.btnPlanBarcode.Visible = false;

            this.btnQueryGoods.Visible = false;
        }
        SampleBiz samplebiz = new SampleBiz();
        QC21View _QC21View = new QC21View();
        QC21Dialog _QC21Dialog = new QC21Dialog();

        QC21ViewBarCode barcodeQuery = new QC21ViewBarCode();
        AddPlan _AddPlan = new AddPlan();

        AddSTAP addSTAP = new AddSTAP(); // Create By Isaac 2019-08-21
        QCReporting report = new QCReporting();
        DOCPlanBiz biz = new DOCPlanBiz();
        DOCPlanAddBiz addbiz = new DOCPlanAddBiz();
       
        private void Init_Load()
        {
         
            #region Language
            try
            {
                CultureLanuage.ApplyResourcesFrom(this, "QC21", "QC21");
                CultureLanuage.ApplyResourcesFrom(_QC21Dialog, "QC21", "QC21Dialog");
                CultureLanuage.ApplyResourcesFrom(_AddPlan, "QC21", "AddPlan");

                ChangeGridViewLanguage("gridView1");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Translate error:"+ex.Message);
            }
            #endregion
            _AddPlan.AddPlan_cmbSample_SelectedIndexChanged += AddPlan_cmbSample_SelectedIndexChanged;
          
           // _AddPlan.MaterialSource = GetMaterials(_AddPlan.SampleName);
            _QC21View.eventSampleMaterial += new EventHandler(qC21View_eventSampleMaterial);
            _QC21View.eventMaterialGetPlan += new EventHandler(qC21View_eventMaterialGetPlan);

            //成品 工序
            _QC21View.InitForm();
            _AddPlan.dtListTypeName = report.GetQCReport("QCW1_QueryTypeName", new[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];

            _QC21View.dtListTypeName = report.GetQCReport("QCW1_QueryTypeName", new[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];
            WorkSpace.Show(_QC21View);
        }       
        #region Event
        private void RegisterCommand()
        {
            btSearchPlan.Click += btSearchPlan_Click;
            btAdd.Click += btAdd_Click;
            btAddPlan.Click += btAddPlan_Click;
            btDelete.Click += btDelete_Click;
            btReturn.Click += btReturn_Click;

            btnQueryGoods.Click += BtnQueryGoods_Click;
            btnPlanBarcode.Click += BtnPlanBarcode_Click;
          
        }

        private void BtnPlanBarcode_Click(object sender, EventArgs e)
        {
            AddStaplePlan();
        }      

        private void BtnQueryGoods_Click(object sender, EventArgs e)
        {           
           
            barcodeQuery.ShowDialog();
           

                ChangeGridViewLanguage("gridView2");
          

                bar1.Refresh();
                _QC21View.dtGrid = GetQueryGoods(barcodeQuery.ProductDate, _QC21View.LotNO, barcodeQuery.BarCode);    
          
           

        }
   
        private void btSearchPlan_Click(object sender, EventArgs e)
        {
       

            ChangeGridViewLanguage("gridView1");
            _QC21View.dtGrid = GetPlan();
        }
        private void ChangeGridViewLanguage(string gridViewName)
        {
            try
            {
                _QC21View.gridView1.Columns.Clear();
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(_QC21View, "QC21", "QC21View");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, gridViewName);
                if (gridData != null)
                {
                    _QC21View.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(_QC21View.gridView1, gridViewName, dsgrid);
                    _QC21View.gridView1.BestFitColumns();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btAdd_Click(object sender, EventArgs e)
        {
            Add();
        }
        private void btAddPlan_Click(object sender, EventArgs e)
        {
            AddPlan();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DeletePlan();
        }

        private void btReturn_Click(object sender, EventArgs e)
        {
            this.Close();          
            WorkSpace.Show(_QC21View);
        }

        //string MainMsg
        //{
        //    set
        //    {
        //        txtMsg.Text = value;
        //        txtMsg.Refresh();
        //    }
        //}

        //private void NotReadyEvent(object msg, EventArgs e)
        //{
        //    WriteTips(5, msg.ToString());
        //}
        #endregion

    
    }
}
