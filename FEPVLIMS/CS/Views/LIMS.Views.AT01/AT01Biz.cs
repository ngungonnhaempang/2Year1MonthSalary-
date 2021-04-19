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
using BasicLanuage;

namespace LIMS.Views
{
    public partial class AT01
    {
        QCReporting report = new QCReporting();
        AttributeBiz biz = new AttributeBiz();

        public void Query()
        {
            DataTable dt = report.GetQCReport("AT01_QueryGetAll",
                new string[] { "LabID", "TypeID", "SampleName", "Lang" },
                     new object[] { _AT01View.LabID, _AT01View.TypeID, _AT01View.Sample, MIS.Utility.MyLanguage.Language }).Tables[0];
            _AT01View.tableMList = dt;
        }

        public void AddSample()
        {
            if (_AT01View.cmbSample.Text.Trim() == "")
            {
                MessageBox.Show("Sample name is empty, please select the sample name!");
            }
            else
            {
                //_AT01Dialog = new AT01Dialog();
                _AT01Dialog.SetNull = "";
                _AT01Dialog.SetAttribute = new QCAttribute { SampleName = _AT01View.Sample, SampleDesc = _AT01View.SampleDesc };
                if (_AT01Dialog.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(_AT01Dialog.ReadyWork))
                        WriteTips(5,_AT01Dialog.ReadyWork);
                    else
                    {
                        Query();
                        WriteTips(5, "Add Sample successful!: "+ _AT01View.Sample);
                    }
                }
            }
        }

        void _AT01View_eventShowDetails(object sender, EventArgs e)
        {
            EgateArgs args = (EgateArgs)e;
            //_AT01Dialog = new AT01Dialog();

            if (string.IsNullOrEmpty((string)args.EgateDictionary["SampleName"].ToString()))
            {
               // WriteTips(5, CultureLanguage.Translator(this.Name, 3, "No sample name selected"));
                WriteTips(5, "No sample name selected");
            }
            else
            {
                QCAttribute s = new QCAttribute();
                s.SampleName = (string)args.EgateDictionary["SampleName"].ToString();
             
                s.PropertyName = (string)args.EgateDictionary["PropertyName"].ToString();
                s.Description_EN = (string)args.EgateDictionary["Description_EN"].ToString();
                s.Description_TW = (string)args.EgateDictionary["Description_TW"].ToString();
                s.Description_CN = (string)args.EgateDictionary["Description_CN"].ToString();
                s.Description_VN = (string)args.EgateDictionary["Description_VN"].ToString();
                s.Unit = (string)args.EgateDictionary["Unit"].ToString();
                s.Prec = Convert.ToInt32(args.EgateDictionary["Prec"].ToString());
                s.Remark = (string)args.EgateDictionary["Remark"].ToString();
                s.EnRemark = (string)args.EgateDictionary["EnRemark"].ToString();
                s.JPNRemark = (string)args.EgateDictionary["JPNRemark"].ToString();
                s.OrderBy =Convert.ToInt32(args.EgateDictionary["OrderBy"].ToString());
                _AT01Dialog.SetAttribute = s;

                if (_AT01Dialog.ShowDialog() == DialogResult.OK)
                {
                    Query();
                    WriteTips(5,"Save Successful!");
                }
            }
        }


        public void DelectSample()
        {
            string msg = "";
            if (!string.IsNullOrEmpty(_AT01View.SampleTemp.SampleName))
            {
                 if (Infrastructure.ConfirmBox.Show("Delete PropertyName ", string.Format("Do you want to delete this Sample: {0} -  {1}", _AT01View.SampleTemp.SampleName, _AT01View.SampleTemp.PropertyName)))
                 {

                     if (biz.DelAttribute(_AT01View.SampleTemp.SampleName, _AT01View.SampleTemp.PropertyName))
                    {
                        Query();
                        WriteTips(5, "Delete Sample successful!"+ _AT01View.SampleTemp.SampleName);
                    }
                    else
                        WriteTips(5,  msg);
                }
            }
            else
            {
               WriteTips(5,  "No sample selected!");
            }
        }   
    }
}
