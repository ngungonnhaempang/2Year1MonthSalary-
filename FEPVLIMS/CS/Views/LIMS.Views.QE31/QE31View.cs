using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using LIMS.BLL;
using LIMS.Model;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MIS.Utility;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class QE31View : UserControl
    {
        public QE31View()
        {
            InitializeComponent();
            RegisterCommand();
            dateFrom.DateTime = DateTime.Now;
            dateTo.DateTime = DateTime.Now;
        }
        QCReporting report = new QCReporting();
        DOCBiz _DOCBiz = new DOCBiz();
        ProfileBiz _profileBiz = new ProfileBiz();
        DataTable dtVoucherID = new DataTable();


        Regex reg = new Regex(@"^-?([0-9]\d*\.\d*|0?\.\d*|\d*|Y|N)$");

        //  DataTable DtPropertys =new DataTable();
        /// <summary>
        /// 选择计划的ID
        /// </summary>
        public string VoucherID { set; get; }
        public string ProdDate { set; get; }
        public string SampleName { get; set; }
        public string SampleIDLong { get; set; }
        public string LOT_NO { get; set; }
        public string MaxValue { set; get; }
        public string MinValue { set; get; }
        public string PropertyName { set; get; }
        #region Events
        private void RegisterCommand()
        {

            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            gridView2.Click += gridView2_Click;
            gridView1.RowCellClick += gridView1_RowCellClick;
            gridView2.OptionsBehavior.AutoSelectAllInEditor = true;
            gridView2.RowCellStyle += gridView2_RowCellStyle;
            gridView2.CellValueChanged += gridView2_CellValueChanged;
            gridView2.OptionsBehavior.Editable = true;
            gridControl2.ProcessGridKey += gridControl2_ProcessGridKey;

            #region Language

            try
            {
               // this.TabText = CultureLanuage.Translator(this.Name, 0, "Result Input [QE31]");
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QE31", "QE31View");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView1, "gridView1", dsgrid);
                    this.gridView1.BestFitColumns();
                }

                gridData = CultureLanuage.GridHeader(dsgrid, "gridView2");
                if (gridData != null)
                {
                    this.gridControl2.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView2, "gridView2", dsgrid);
                    this.gridView2.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }

            #endregion

            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(gridView1_RowStyle);
        }

        
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            int hand = e.RowHandle;
            if (hand < 0)
                return;

            DataRow dr = this.gridView1.GetDataRow(hand);
            if (dr == null)
                return;

            if (!string.IsNullOrEmpty(dr["PlanAddID"].ToString()))
            {
                //e.Appearance.ForeColor = Color.LightYellow;// 改变行字体颜色
                e.Appearance.BackColor = Color.Orange;// 改变行背景颜色
                //e.Appearance.BackColor2 = Color.Blue;// 添加渐变颜色                
            }
        }
        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

            if (e.Column.FieldName == "RealResult")
            {
                Console.WriteLine("RealResult");
                txtMsg.Text = "";
                DataRow selectrow = gridView2.GetDataRow(e.RowHandle);
                Console.WriteLine("save Result:" + selectrow["RealResult"].ToString());
                string originalResult = selectrow["RealResult"].ToString().ToUpper();
                Profile pf = new Profile();
                pf.VoucherID = VoucherID;
                pf.SampleName = SampleName;
                pf.PropertyName = selectrow["PropertyName"].ToString();

                if (!string.IsNullOrEmpty(originalResult))
                {
                    txtMsg.Text = "Input  null";
                    decimal n;
                    string _result = CheckWord(selectrow, out n);// Check word or not
                    pf.OriginalResult = _result;
                    int prec = int.Parse(selectrow["Prec"].ToString());
                    if (reg.IsMatch((string)originalResult.ToString()))
                    {
                        if (selectrow["Enable"].ToString() == "True" && n != 0)//Modified here
                        {
                            bool IsOutRange = CheckValueOutRange(_result, MinValue, MaxValue);
                            pf.OverRange = IsOutRange;
                        }
                        else
                        {
                            pf.OverRange = (originalResult == "N" ? true : false);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(_result))
                            {
                                if (n != 0)
                                {
                                    pf.Result = Convert.ToDecimal(n.ToString());
                                }
                                //pf.Result = (_result == null ? 0m : Convert.ToDecimal(n.ToString()));//laf
                                SaveProfile(pf, e, _result);
                            }
                            else
                            {
                                pf.Result = null;
                                SaveProfile(pf, e, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
                else
                {
                    pf.Result = null;
                    SaveProfile(pf, e, null);
                }
            }
        }
        /// <summary>
        /// Create by Isaac on 2018-08-31
        /// CheckWord when input value in VoucherID begin With S
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private string CheckWord(DataRow dr, out decimal n)
        {
            string originalResult = dr["RealResult"].ToString();
            string _result;
            MinValue = dr["MinValue"].ToString();
            MaxValue = dr["MaxValue"].ToString();
            bool isCheck = decimal.TryParse(originalResult, out n);
            if (isCheck) //if it is a decimal
            {
                _result = Math.Round(Convert.ToDecimal(n), Convert.ToInt32(dr["Prec"].ToString()), MidpointRounding.AwayFromZero).ToString();
            }
            else
            {
                //If it is a word
                if (VoucherID.StartsWith("S"))// Check VoucherID if Start with S
                {
                    if (!string.IsNullOrEmpty(MinValue) || !string.IsNullOrEmpty(MaxValue))//check if word have Min and Max value
                        _result = null;
                    else
                        _result = originalResult.ToUpper();// Upper string
                }
                else _result = null;
            }
            return _result;
        }

        private void SaveProfile(Profile pf, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e, string result)
        {
            string msg = _profileBiz.RecodeResult(pf);
            if (string.IsNullOrEmpty(msg))
            {
                gridView2.SetRowCellValue(e.RowHandle, "OverRange", pf.OverRange);
                gridView2.SetRowCellValue(e.RowHandle, "Result", result);
                // gridView2.SetRowCellValue(e.RowHandle, "RealResult", pf.OriginalResult);
                gridView2.SetRowCellValue(e.RowHandle, "State", result == null ? "" : "0");
            }
            else
            {
                txtMsg.Text = msg;
                Profile newpf = _profileBiz.SelectProfile(pf.VoucherID, pf.PropertyName);
                gridView2.SetRowCellValue(e.RowHandle, "Result", newpf.Result);
                gridView2.SetRowCellValue(e.RowHandle, "OverRange", newpf.OverRange);
                gridView2.SetRowCellValue(e.RowHandle, "State", result == null ? "" : result);
                // gridView2.SetRowCellValue(e.RowHandle, "RealResult", "");
            }
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try { 
            if (!string.IsNullOrEmpty(e.CellValue.ToString())) Clipboard.SetText(e.CellValue.ToString());
            }
            catch
            {
                //IGNORE: Requested Clipboard operation did not succeed.
            }
        }

        private void gridControl2_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                (gridControl2.FocusedView as DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle++;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                gridView2.SetRowCellValue(gridView2.GetFocusedDataSourceRowIndex(), "RealResult", "");
                e.Handled = true;
            }
        }


        private void gridView1_FocusedRowChanged(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;
            var selectRow = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            VoucherID = selectRow["VoucherID"].ToString();
            SampleName = selectRow["SampleName"].ToString();
            LOT_NO = selectRow["LOT_NO"].ToString();
            PropertyName = null;
            
            FillProfile();
        }
        void gridView2_Click(object sender, EventArgs e)
        {
            int rowCount = gridView2.SelectedRowsCount;
            if (rowCount != 1)
                return;
            var selectRow = gridView2.GetDataRow(gridView2.GetSelectedRows()[0]);
            PropertyName = selectRow["PropertyName"].ToString();

        }

        public Color ValueInRange { get { return Color.LightBlue; } }

        public Color ValueOutRange { get { return Color.PaleVioletRed; } }
        public Color ValueWarning { get { return Color.Orange; } }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                DataRow selectrow = gridView2.GetDataRow(e.RowHandle);

                if (e.Column.FieldName == "RealResult")
                {
                    if (string.IsNullOrEmpty(selectrow["RealResult"].ToString()))
                    {
                        e.Appearance.BackColor = Color.White;
                        return;
                    }

                    //Create by Isaac 2018-08-30
                    #region Set Y/N color

                    string myResult = selectrow["RealResult"].ToString().ToUpper();
                    decimal n;
                    string isCheck = CheckWord(selectrow, out n);
                    switch (isCheck)
                    {
                        case "N":
                            e.Appearance.BackColor = ValueOutRange;
                            e.Appearance.BackColor2 = Color.White;
                            break;
                        case "Y":
                            e.Appearance.BackColor = ValueInRange;
                            e.Appearance.BackColor2 = Color.White;
                            break;
                        default:
                            e.Appearance.BackColor = Color.White;
                            break;
                    }

                    #endregion
                    if ((bool)selectrow["OverRange"])
                    {
                        e.Appearance.BackColor = ValueOutRange;
                        e.Appearance.BackColor2 = Color.White;
                    }
                    else
                    {
                        // selectrow["Result"];
                        decimal? minv = null;
                        decimal? maxv = null;
                        decimal result = Convert.ToDecimal(selectrow["Result"].ToString());
                        if (!string.IsNullOrEmpty(selectrow["MinValue"].ToString()))
                        {
                            minv = Convert.ToDecimal(selectrow["MinValue"].ToString());
                        }
                        if (!string.IsNullOrEmpty(selectrow["MaxValue"].ToString()))
                        {
                            maxv = Convert.ToDecimal(selectrow["MaxValue"].ToString());
                        }
                        // selectrow["MaxValue"];
                        if (minv != null)
                        {
                            if (result == minv)
                            {
                                e.Appearance.BackColor = ValueWarning;
                                e.Appearance.BackColor2 = Color.White;
                                return;
                            }

                        }
                        if (maxv != null)
                        {
                            if (result == maxv)
                            {
                                e.Appearance.BackColor = ValueWarning;
                                e.Appearance.BackColor2 = Color.White;
                                return;
                            }
                        }
                        e.Appearance.BackColor = ValueInRange;
                        e.Appearance.BackColor2 = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                txtMsg.Text = ex.Message;
            }
        }
        //public bool isCheckWord(string myValue, out decimal n)
        //{
        //    //decimal n;
        //     return decimal.TryParse(myValue, out n);
        //}
        public bool CheckValueOutRange(object Value, object Min, object Max)
        {
            if (string.IsNullOrWhiteSpace(Min.ToString())) Min = null;
            if (string.IsNullOrWhiteSpace(Max.ToString())) Max = null;
            if (string.IsNullOrWhiteSpace(Value.ToString())) Value = null;

            if (Min == null && Max == null || Value == null)
                return false;

            decimal value = Convert.ToDecimal(Value);

            if (Min == null)
                return value > Convert.ToDecimal(Max) ? true : false;

            if (Max == null)
                return value < Convert.ToDecimal(Min) ? true : false;

            return value < Convert.ToDecimal(Min) || value > Convert.ToDecimal(Max) ? true : false;
        }

        #endregion



        /// <summary>
        /// 查询参数
        /// </summary>
        public object[] Values4Plan
        {
            get
            {
                return new object[] { this.txtVoucherID.Text.Trim(),
                    cmbDOCType.SelectedValue.ToString(),
                    dateFrom.DateTime.Date, 
                    dateTo.DateTime.Date.AddHours(23).AddMinutes(59).AddSeconds(59), 
                    MyLanguage.Language 
                };
            }
        }


        public DataTable VoucherIDSource
        {
            set
            {

                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
                gridControl1.Refresh();
                dtVoucherID.Clear();
               
            }
        }

        public DataTable DOCTypeSource
        {
            set
            {
                cmbDOCType.DataSource = value;
                cmbDOCType.DisplayMember = "Spec";
                cmbDOCType.ValueMember = "Type";
            }
        }

        /// <summary>
        /// 加载属性
        /// </summary>
        /// <param name="voucherId"></param>
        public void FillProfile()
        {
            DataTable tableVoucher = report.GetQCReport("QE31_QueryDocByVoucherId", new string[] { "voucherId" }, new object[] { VoucherID }).Tables[0];
            if (tableVoucher.Rows.Count > 0)
            {
                DataRow row = tableVoucher.Rows[0];
                var DtPropertys = _DOCBiz.GetvProperties(VoucherID);
                gridControl2.DataSource = DtPropertys;
                foreach (DataRow dr in DtPropertys.Rows)
                {
                    if (dr["MaxValue"].ToString().Length > 0 && dr["MinValue"].ToString().Length > 0 && dr["Prec"].ToString().Length > 0)
                    {
                        dr["MaxValue"] = decimal.Round(decimal.Parse(dr["MaxValue"].ToString()), Convert.ToInt32(dr["Prec"].ToString()));
                        dr["MinValue"] = decimal.Round(decimal.Parse(dr["MinValue"].ToString()), Convert.ToInt32(dr["Prec"].ToString()));
                    }

                }
                DtPropertys.AcceptChanges();
                foreach (DevExpress.XtraGrid.Columns.GridColumn coulun in gridView2.Columns)
                {
                    if (coulun.FieldName != "RealResult")
                    {
                        coulun.AppearanceCell.BackColor = Color.LightGray;
                        coulun.OptionsColumn.AllowEdit = false;
                    }
                }
            }
            else
            {
                gridControl2.DataSource = new DataTable();
            }
            gridView2.BestFitColumns();
        }

        public void UpdateValue(string Propertyname, string value)
        {
            for (int i = 0; i < gridView2.DataRowCount; i++) {
                DataRow row = gridView2.GetDataRow(i);
                if (row["PropertyName"].ToString() == Propertyname)
                {
                    gridView2.SetRowCellValue(i, "RealResult", value);
                    return;
                }
                
            }
        }


    }
}
