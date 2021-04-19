using System;
using System.Collections.Generic;
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
    public partial class MT01
    {
        private QCReporting report = new QCReporting();
        private MaterialBiz biz = new MaterialBiz();

        public void Query()
        {
            DataTable dt = report.GetQCReport("MT01_Q_GetMaterial",
                new string[] { "SampleName", "LOT_NO" },
                     new object[] { _MT01View.Sample, _MT01View.LOT_NO }).Tables[0];
            _MT01View.tableMList = dt;
        }

        public void AddMaterial()
        {
            if (_MT01View.cmbSample.Text.Trim() == "")
            {
                WriteTips(5, "Sample name can not for empty");
            }
            else
            {
                _MT01Dialog.SetNull = "";
                _MT01Dialog.SampleName = _MT01View.Sample;
                if (_MT01Dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(_MT01Dialog.ReadyWork))
                        WriteTips(5000, _MT01Dialog.ReadyWork);
                    else
                    {
                        Query();
                        WriteTips(5000, "Add Material successful!: " + _MT01View.LOT_NO);
                    }
                }
            }
        }

        private void _MT01View_eventShowDetails(object sender, EventArgs e)
        {
            EgateArgs args = (EgateArgs)e;

            if (string.IsNullOrEmpty((string)args.EgateDictionary["LOT_NO"].ToString()))
            {
                WriteTips(5, "No Material selected");
            }
            else
            {
                QCMaterial s = new QCMaterial
                {
                    SampleName = args.EgateDictionary["SampleName"].ToString(),
                    LOT_NO =  args.EgateDictionary["LOT_NO"].ToString(),
                    Description_EN = args.EgateDictionary["Description_EN"].ToString(),
                    Description_TW = args.EgateDictionary["Description_TW"].ToString(),
                    Description_CN = args.EgateDictionary["Description_CN"].ToString(),
                    Description_VN = args.EgateDictionary["Description_VN"].ToString(),
                    IsGrade = Convert.ToBoolean(args.EgateDictionary["IsGrade"])
                };

                _MT01Dialog.Material = s;

                if (_MT01Dialog.ShowDialog() == DialogResult.OK)
                {
                    Query();
                    WriteTips(5, "Save Successful!");
                }
            }
        }

        private void _MT01View_eventClickDetails(object sender, EventArgs e)
        {
            EgateArgs args = (EgateArgs)e;

            if (string.IsNullOrEmpty((string)args.EgateDictionary["LOT_NO"].ToString()))
            {
                WriteTips(5,  "No Material selected");
            }
            else
            {
                QCMaterial s = new QCMaterial
                {
                    SampleName = args.EgateDictionary["SampleName"].ToString(),
                    LOT_NO = args.EgateDictionary["LOT_NO"].ToString(),
                    Description_EN = args.EgateDictionary["Description_EN"].ToString(),
                    Description_TW = args.EgateDictionary["Description_TW"].ToString(),
                    Description_CN = args.EgateDictionary["Description_CN"].ToString(),
                    Description_VN = args.EgateDictionary["Description_VN"].ToString(),
                    IsGrade = Convert.ToBoolean(args.EgateDictionary["IsGrade"])
                };

                _MT01Dialog.Material = s;
            }
        }

        public void DeleteMaterial()
        {
            if (!string.IsNullOrEmpty(_MT01Dialog.Material.LOT_NO))
            {
                if (Infrastructure.ConfirmBox.Show("Confirm", "Do you want to delete this Material: " + _MT01Dialog.Material.LOT_NO))
                
                    if (biz.DeleteMaterial(_MT01View.SampleTemp))
                    {
                        Query();
                        WriteTips(5,"Delete Material {0} successful!" + _MT01Dialog.Material.LOT_NO);
                    }
                    else
                       MainMsg = "Delete Material Error!";
                }
            }
          
        }
    }
