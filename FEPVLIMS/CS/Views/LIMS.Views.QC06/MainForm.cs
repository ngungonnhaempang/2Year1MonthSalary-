using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LIMS.BLL;
using LIMS.Model;
using System.Collections;
using BasicLanuage;
using MIS.Utility;

namespace LIMS.Views
{
    public partial class MainForm : Infrastructure.BaseForm
    {
        public MainForm()
        {
            InitializeComponent();
            dateFrom.DateTime = DateTime.Now;
            dateTo.DateTime = DateTime.Now;
            this.Load += new EventHandler(MainForm_Load);
            #region Language

            try
            {
                //this.TabText = CultureLanuage.Translator(this.Name, 0, "Result Input [QE31]");
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QC06", "MainForm");
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(this.gridView1, "gridView1", dsgrid);
                    this.gridView1.BestFitColumns();
                }

              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        QCReporting report = new QCReporting();
        void MainForm_Load(object sender, EventArgs e)
        {
            btSearchSure.Click += new EventHandler(btSearchSure_Click);
            btVouSubmit.Click += new EventHandler(btVouSubmit_Click);
            btVouRelease.Click += new EventHandler(btVouRelease_Click);
            cmbTypeName.SelectionChangeCommitted += cmbTypeName_SelectionChangeCommitted;
            cmbSampleName.EditValueChanged += cmbSampleName_EditValueChanged;
            gridView1.Click += new EventHandler(gridView1_Click);
            dtListTypeName=report.GetQCReport("QCW1_QueryTypeName", new[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];


        }
        private DataTable dtListTypeName
        {
            set
            {
                cmbTypeName.DataSource = value;
                cmbTypeName.DisplayMember = "TypeName";
                cmbTypeName.ValueMember = "TypeID";
            }
        }
        private int Catelag
        {
            get { return int.Parse(cmbTypeName.SelectedValue.ToString()); }
        }
        private string SampleNameCode
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
        private void cmbSampleName_EditValueChanged(object sender, EventArgs e)
        {

          MaterialSource=  report.GetQCReport("Q_Materials",
                                             new string[] { "SampleName", "Query" },
                                             new object[] { SampleNameCode, "S" }).Tables[0];
        }
        private void cmbTypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Console.WriteLine("select type");
         
            SetSamples = report.GetQCReport("Q_SamplesByCategory", new[] { "TypeID" }, new object[] { Catelag }).Tables[0];
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
        void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            VoucherID = row["VoucherID"].ToString();
            SampleName = row["SampleName"].ToString();
        }

        FormEditer f = new FormEditer();
        QCReporting rep = new QCReporting();
        DOCBiz _docBiz = new DOCBiz();

        #region button click
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btSearchSure_Click(object sender, EventArgs e)
        {
            SearchConfirm();
        }

        /// <summary>
        /// 单据区隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btVouSubmit_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount <= 0)
            {
                MessageBox.Show("Please Choose Item!");
                return;
            }

            if (VoucherIDs.Length <= 0)
                return;

            if (f.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in VoucherIDs)
                {
                    if (item.Substring(0, 1) == "P")
                        _docBiz.ChangeCreateType(item, f.CreateType);
                    else
                        MessageBox.Show("Voucher" + item + "is not plan");
                }

                SearchConfirm();
            }
        }

        /// <summary>
        /// 单据删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btVouRelease_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount <= 0)
            {
                MessageBox.Show("Please Choose Item!");
                return;
            }

            if (VoucherIDs.Length <= 0)
                return;

            if (DialogResult.OK == MessageBox.Show("WILL DELETE,CONTINUE?", "ITEM DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2))
            {
                string msg = "";
                int count = 0;
                foreach (string item in VoucherIDs)
                {
                    if (!_docBiz.DocDelete(item))
                        msg += "[" + item + "]";
                    else
                        count++;
                }
                if (count != VoucherIDs.Length)
                    MessageBox.Show("ITEM " + msg + "DELETE FAILED!");
                SearchConfirm();
            }
        }
        #endregion

        /// <summary>
        /// 单据查询
        /// </summary>
        private void SearchConfirm()
        {
            PlansTable = rep.GetQCReport("QC06_Query_Plans", new string[] { "deWadat", "deTo", "Catalog", "VoucherID", "Lot_No", "SampleName", "lang" },
                  new object[] { B, E, Catelag, txtVoucherID.Text.Trim(), this.txtVoucherID.Text.Trim(), this.cmbMaterial.Text.Trim(), this.SampleNameCode,MyLanguage.Language }).Tables[0];
        }

        /// <summary>
        /// 头列表
        /// </summary>
        public DataTable PlansTable
        {
            set
            {
                gridView1.Columns.Clear();
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }

        public DateTime?  B
        {
            get
            {
                if (string.IsNullOrEmpty(this.dateFrom.Text))
                {
                    return null;
                }
                return dateFrom.DateTime;
            }
        }
        public DateTime? E
        {
            get
            {
                if (string.IsNullOrEmpty(this.dateTo.Text))
                {
                    return null;
                }
                return dateTo.DateTime;
            }
        }
        public string VoucherID { get; set; }
        public string SampleName { get; set; }

        public string[] VoucherIDs
        {
            get
            {
                List<string> voucherIds = new List<string>();

                ArrayList rows = new ArrayList();

                // Add the selected rows to the list.
                int rowCount = gridView1.SelectedRowsCount;
                if (rowCount <= 0)
                    return voucherIds.ToArray();

                for (int i = 0; i < rowCount; i++)
                {
                    if (gridView1.GetSelectedRows()[i] >= 0)
                        rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                }
                ///
                foreach (DataRow r in rows)
                {
                    voucherIds.Add((string)r["VoucherID"]);
                }
                return voucherIds.ToArray();
            }
        }
    }
}
