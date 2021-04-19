using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLanuage;
using LIMS.BLL;
using LIMS.Model;
using MIS.Utility;

namespace LIMS.Views
{
    public partial class QC21
    {
     

        private DataTable GetMaterials(string sampleName)
        {
            return report.GetQCReport("Q_Materials",
                                             new string[] { "SampleName", "Query" },
                                             new object[] { sampleName, "0" }).Tables[0];
        }

        public string AB { set; get; }
        private string GetShowUI(string sampleName)
        {
            string ab = report.GetQCReport("QC21_ShowUI", new string[] { "SampleName" },
                                               new object[] { sampleName }).Tables[0].Rows[0]["AB"].ToString();

            return ab;

        }
        private DataTable GetPlanType()
        {
            return report.GetQCReport("QC21_QueryCreatePlanType", new string[] { "Lang" }, new object[] { MIS.Utility.MyLanguage.Language }).Tables[0];


        }
        /// <summary>
        /// query plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void qC21View_eventMaterialGetPlan(object sender, EventArgs e)
        {           
            _QC21View.dtGrid = GetPlan();

        }
        // get materials
        void qC21View_eventSampleMaterial(object sender, EventArgs e)
        {


         
            var arg = (ShowDetailsArg)e;
            if (!string.IsNullOrEmpty(arg.ID))
            {
                _QC21View.SetMaterial = GetMaterials(arg.ID);
                AB = GetShowUI(arg.ID);
                //STAP Physical  
                if (AB == "STAPPY")
                {
                    btnPlanBarcode.Visible = true;
                    btnQueryGoods.Visible = true;
                    bar1.Refresh();

                }
                else {
                    btnPlanBarcode.Visible = false;
                    btnQueryGoods.Visible = false;
                    bar1.Refresh();
                }
            }
            else
            {
                _QC21View.SetMaterial = GetMaterials("");
                AB = "";
            }


        }
  
        /// <summary>
        /// create plan
        /// </summary>
        private void Add()
        {
            if (string.IsNullOrEmpty(_QC21View.LotNO))
            {
                WriteTips(5, "Material is Empty", false);
                return;
            }
            if (string.IsNullOrEmpty(AB))
            {
                WriteTips(5, "Don't define UI ,link to IT", false);
           
                return;
            }
                AddPOLYSSP(_QC21View.SampleName, _QC21View.LotNO, true);
           

        }
        //get material and line
        private void AddPlan_cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            var arg = (ShowDetailsArg)e;
            if (!string.IsNullOrEmpty(arg.ID))
            {
                _AddPlan.MaterialSource = GetMaterials(arg.ID);
                _AddPlan.LineList = GetLines(arg.ID);

            }
        }

        private DataTable GetLines(string sampleName)
        {
            return report.GetQCReport("Q_GetLinesBySampleName", new string[] { "SampleName" }, new object[] { sampleName }).Tables[0];
        }

        private DataTable GetPlan()
        {
            return report.GetQCReport("QC21_QueryCreatePlan", new string[] { "VoucherID", "SampleName", "MaterialNo", "Lang" }, new object[] { _QC21View.VoucherID, _QC21View.SampleName, _QC21View.LotNO, MIS.Utility.MyLanguage.Language }).Tables[0];
        }

        private void AddPOLYSSP(string sampleName, string materialNo, bool isSampleId)
        {
            AddPOLYSSP _AddPOLYSSP = new AddPOLYSSP(sampleName, materialNo, isSampleId);
            _AddPOLYSSP.LineList = GetLines(sampleName);
            _AddPOLYSSP.CreateTypeLoad = GetPlanType();
            CultureLanuage.ApplyResourcesFrom(_AddPOLYSSP, "QC21", "AddPOLYSSP");
            _AddPOLYSSP.ShowDialog();
            if (_AddPOLYSSP.rValue)
            {
                string msg = _AddPOLYSSP.ReadyWork;
                if (string.IsNullOrEmpty(msg))
                {
                    DOCPlan _DOC = _AddPOLYSSP.GetDocPlan;
                    string VoucherID = "";
                    if (biz.InsertPlan(_DOC, ref VoucherID, out msg))
                    {
                        WriteTips(5,  "Add VoucherID DOC :successful!");
                        _QC21View.dtGrid = GetPlan();
                    }
                    else
                    {
                      
                           WriteTips(5,  msg,false);
                    }
                }
                else
                {
                    WriteTips(5, msg);
                }
            }
        }

        /// <summary>
        /// create add test plan
        /// </summary>
        private void AddPlan()
        {
            //成品
           
          
            _AddPlan.CreateTypeLoad = GetPlanType();
            _AddPlan.Clear();
            _AddPlan.ShowDialog();
            if (_AddPlan.rValue)
            {
                string VoucherID = "";
                string msg = "";
                if (addbiz.InsertPlanAdd(_AddPlan._DOC))
                {
                    WriteTips(5,"Add successful! VoucherID: " + VoucherID,true);
                   // WriteTips(5, CultureLanuage.Translator(this.Name, 6, string.Format("Add VoucherID : {0} successful!", VoucherID), VoucherID));
                    _QC21View.dtGrid = GetPlan();
                }
                else
                {
                    WriteTips(5, msg, false);
                }
            }
        }

        /*
         * Create By Isaac 2019-08-21
         * Create New Form for Staple Test
         */

        private void AddStaplePlan()
        {
            addSTAP.CreateTypeLoad = GetPlanType();
            addSTAP.ShowDialog();
            var Products = _QC21View.GetMultipleSelectionProducts();
            foreach(var product in Products)
            {
                if (addSTAP.rValue)
                {
                    string msg = addSTAP.ReadyWork;
                    if (string.IsNullOrEmpty(msg))
                    {                      
                        string VoucherID = "";
                        product.SheetDate = addSTAP.SheetDate;
                        product.ProdDate = addSTAP.ProdDate;
                        product.Remark = addSTAP.Remark;
                        product.HasChart = addSTAP.isHasChart;
                        product.createType = int.Parse(addSTAP.CreateType);
                        if (biz.InsertPlan(product, ref VoucherID, out msg))
                            WriteTips(5, "Add VoucherID DOC :successful!");
                        else
                            WriteTips(5, msg, false);
                    }
                    else
                    {
                        WriteTips(5, msg);
                    }
                }
            }
            _QC21View.gridView1.Columns.Clear();
            _QC21View.dtGrid = GetPlan();
          
            bar1.Refresh();
        }

        private void DeletePlan()
        {
            string VoucherID = _QC21View.SelectVoucherID;
            if (VoucherID == "")
                return;

            _QC21Dialog.Reason = string.Format("Do you want to delete VoucherID: {0} ?", VoucherID);
            _QC21Dialog.ShowDialog();
            if (_QC21Dialog.rValue)
            {
                string msg = "";
                DOCBiz docBiz = new DOCBiz();
                if (docBiz.DocDelete(VoucherID))
                {
                    WriteTips(8, "Delete successfull, VoucherID: " + VoucherID, false);
                   
                    _QC21View.dtGrid = GetPlan();
                }
                else
                    WriteTips(5, "Delete fail", false);

                    //WriteTips(5, CultureLanuage.Translator(this.Name, 9, "Delete VoucherID : {0} Fail. Error : {1}", VoucherID, msg));
            }
        }

        #region Create By Isaac 2019-09-09      
        private DataTable GetQueryGoods(DateTime prodDate, string lot_no, string barcode)
        {
            return this.report.GetQCReport("QC01_QueryMesBagsList", new string[] { "Date", "Lot_No","Barcode","AB" }, 
                new object[] { prodDate, lot_no, barcode,"S" }).Tables[0];
        }
        #endregion
    }
}