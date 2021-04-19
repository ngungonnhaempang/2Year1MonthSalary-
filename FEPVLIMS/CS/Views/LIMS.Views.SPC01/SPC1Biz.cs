using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using LIMS.BLL;
using LIMS.Model;
using MIS.Utility;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class SPC1
    {

        QCReporting report = new QCReporting();
        ChartBiz biz = new ChartBiz();
        OpenFileDialog OpenDialog = new OpenFileDialog();

        void Init_Load()
        {
            _SPC1View.LabNameLoad = report.GetQCReport("QCW1_QueryLabName", new string[] { "Lang" }, new object[] { _Language }).Tables[0];
            _SPC1View.TypeNameLoad = report.GetQCReport("QCW1_QueryTypeName", new string[] { "Lang" }, new object[] { _Language }).Tables[0];          
            Workspace.Show(_SPC1View);
        }

        /// <summary>
        /// Get Sample List When LabName or TypeName change value
        /// </summary>
        public void GetSampleList()
        {
            try
            {
                _SPC1View.SampleNameLoad = report.GetQCReport("QCW1_QuerySample",
                         new string[] { "LabID", "TypeID" },
                         new object[] {  _SPC1View.LabID, _SPC1View.TypeID}).Tables[0];
            }
            catch (Exception ex)
            {
                WriteTips(5, ex.Message);
            }
        }

        private void GetLottoNoAndLineNo()
        {
            try
            {
                DataSet ds = report.GetQCReport("SPC1_QueryLottoLine",
                         new string[] { "SampleName", "LOT_NO", "LINE" },
                         new object[] { _SPC1View.Sample, _SPC1View.LOT_NO, _SPC1View.LINE });
                _SPC1View.LottoNoLoad = ds.Tables[0];

                _SPC1View.LineNoLoad = ds.Tables[1];
            }
            catch (Exception ex)
            {
                WriteTips(5, ex.Message);
            }
        }

        public void Query()
        {
            string Sample = "";
            string LOT_NO = "";
            string LINE = "";
            if (_SPC1View.Sample == null)
            {
            }
            else
            {
                Sample = _SPC1View.Sample;
                LOT_NO = _SPC1View.LOT_NO;
                LINE = _SPC1View.LINE;
            }
            DataTable dt = report.GetQCReport("SPC1_QueryGetAll",
                                              new string[] { "SampleName", "LOT_NO", "LINE" },
                                              new object[] { Sample, LOT_NO, LINE }).Tables[0];
            _SPC1View.tableMList = dt;
            
                   
        }

        public void AddCL()
        {
            _SPC1Dialog.Clear();
            _SPC1Dialog._STEP = STEP.Add;
            if (string.IsNullOrEmpty(_SPC1View.Sample) || string.IsNullOrEmpty(_SPC1View.LOT_NO) || string.IsNullOrEmpty(_SPC1View.LINE))
            {
                MessageBox.Show(CultureLanuage.Translator(this.Name, 1, "Please pick SampleName, LOT_NO and LINE!"));
                return;
            }

            _SPC1Dialog.Chart = new QCChart { SampleName = _SPC1View.Sample, MaterialNo = _SPC1View.LOT_NO, LineNo = _SPC1View.LINE, Date = DateTime.Now };
            _SPC1Dialog.ShowDialog();
            if (_SPC1Dialog.rValue)
            {
                string msg = _SPC1Dialog.ReadyWork;
                if (string.IsNullOrEmpty(msg))
                {
                    if (!biz.AddChartUCL2LCL(_SPC1Dialog.Chart))
                        WriteTips(5, "Error", Color.Red);
                    else
                    {
                        WriteTips(5, CultureLanuage.Translator(this.Name, 2, "Added Control Line Successful!"));
                        Query();
                    }
                }
                else
                {
                    MessageBox.Show(msg);
                    WriteTips(5, msg, Color.Red);
                }
            }

        }

        public void DeleteCL(QCChart Chart)
        {

            if (string.IsNullOrEmpty(Chart.SampleName) || string.IsNullOrEmpty(Chart.Property))
            {
                WriteTips(5, CultureLanuage.Translator(this.Name, 3, "Please select both SampleName and Property"), Color.Red);
                return;
            }
            else
            {
                if (Infrastructure.ConfirmBox.Show(CultureLanuage.Translator(this.Name, 5, "Confirm Delete"), CultureLanuage.Translator(this.Name, 4, "Confirm to Delete Property : {0} - {1} ?", Chart.SampleName, Chart.Property)))
                {
                    if (biz.DeleteChartUCL2LCL(Chart))
                    {
                        Query();
                        WriteTips(5, CultureLanuage.Translator(this.Name, 6, "Property : {0} - {1}, has been deleted!", Chart.SampleName, Chart.Property));
                    }
                    else
                    {
                        WriteTips(5, CultureLanuage.Translator(this.Name, 7, "Error! Can not delete Property : {0} - {1}!", Chart.SampleName, Chart.Property),Color.Red);
                    }
                }
            }

        }

        void _SPC1View_eventShowDetails(object sender, EventArgs e)
        {
            EgateArgs args = (EgateArgs)e;

            _SPC1Dialog.Clear();

            _SPC1Dialog.Chart = new QCChart
            {
                SampleName = (string)args.EgateDictionary["SampleName"],
                MaterialNo = (string)args.EgateDictionary["LOT_NO"],
                LineNo = (string)args.EgateDictionary["LINE"],
                Property = (string)args.EgateDictionary["PropertyName"],
                UCL = (decimal)args.EgateDictionary["UCL"],
                LCL = (decimal)args.EgateDictionary["LCL"],
                Date = (DateTime)args.EgateDictionary["CreateDate"],
                Remark = (string)args.EgateDictionary["Remark"]
            };
            _SPC1Dialog._STEP = STEP.Edit;

            _SPC1Dialog.ShowDialog();

            if (_SPC1Dialog.rValue)
            {
                string msg = _SPC1Dialog.ReadyWork;
                if (string.IsNullOrEmpty(msg))
                {
                    if (!biz.UpdateChartUCL2LCL(_SPC1Dialog.Chart))
                        WriteTips(5, CultureLanuage.Translator(this.Name, 8, "Update ChartUCL2LCL Error!"));
                    else
                    {
                        WriteTips(5, CultureLanuage.Translator(this.Name, 9, "Update Control Line: {0} - {1} succesfull!", _SPC1Dialog.Chart.SampleName, _SPC1Dialog.Chart.Property));
                        Query();
                    }
                }
                else
                {
                    MessageBox.Show(msg);
                    WriteTips(5, msg);
                }
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (count == table.Rows.Count)
                MessageBox.Show(CultureLanuage.Translator(this.Name, 10, "Successfully import all the data.."));
            else
                MessageBox.Show(CultureLanuage.Translator(this.Name, 11, "Data import faild: ") + strBuilder.ToString());
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            AddContractor();
        }

        public bool GetData()
        {
            try
            {
                OpenDialog.Filter = "Excel(*.xls)|*.xls";
                OpenDialog.Title = CultureLanuage.Translator(this.Name, 12, "Import From Excel");
                if (OpenDialog.ShowDialog() == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(OpenDialog.FileName))
                        return false;

                    GetDataFromExcel();

                    if (bw.IsBusy)
                        return false;

                    bw.RunWorkerAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        int count = 0;
        StringBuilder strBuilder = new StringBuilder();
        public void AddContractor()
        {
            count = 0;
            strBuilder.Length = 0;
            foreach (DataRow row in table.Rows)
            {
                string msg = "";
                QCChart _QCChart = new QCChart
                {
                    SampleName = row["SampleName"].ToString(),
                    MaterialNo = row["LOT_NO"].ToString(),
                    LineNo = row["LINE"].ToString(),
                    Property = row["PropertyName"].ToString(),
                    Date = Convert.ToDateTime(row["CreateDate"]),
                    UCL = Convert.ToDecimal(row["UCL"]),
                    LCL = Convert.ToDecimal(row["LCL"])
                  };
                try
                {
                    biz.AddChartUCL2LCL(_QCChart);
                    count++;
                }
                catch (Exception e){
                    strBuilder.Append(row["SampleName"].ToString() + " " + row["PropertyName"].ToString() + ":" + msg + "\n");
                }
                          
            }
        }

        DataTable table = new DataTable();
        private void GetDataFromExcel()
        {
            DataSet MyDataSet = new DataSet();
            string strCon = " Provider = Microsoft.Jet.OLEDB.4.0 ; Data Source = " + OpenDialog.FileName + ";Extended Properties=Excel 8.0";
            OleDbConnection myConn = new OleDbConnection(strCon);
            string strCom = " SELECT * FROM [Sheet1$] ";
            myConn.Open();
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);

            myCommand.Fill(MyDataSet, "[Sheet1$]");

            myConn.Close();
            table = MyDataSet.Tables[0];
            _SPC1View.gridControl1.DataSource = table;
        }
    }
}
