using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LIMS.BLL;
using MIS.Utility;
using LIMS.Model;
using BasicLanuage;

namespace LIMS.Views.QC21Members
{
    public partial class AddSTAP : Infrastructure.StyleForm
    {
        public AddSTAP()
        {
            InitializeComponent();
            dateProdDate.DateTime = DateTime.Now;
            btnCancel.Click += BtnCancel_Click;
            btnOk.Click += BtnOk_Click;
        }     
        public bool rValue { set; get; }
        public string Grades { set; get; }
        public string isHasChart
        {

            get
            {
                return cbHasChart.Checked ? "1" : "0";
            }
        }
        public DateTime SheetDate { get { return DateTime.Now; } }

        public string Remark { get { return txtRemark.Text; } }

        public DateTime ProdDate { get { return dateProdDate.DateTime; } }

        public string CreateType { get { return cmbCreateType.SelectedValue.ToString(); } }
       
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (ReadyWork == "")
            {
                rValue = true;
                this.Close();
            }
            else
                rValue = false;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            rValue = false;
            this.Close();
        }

        public string BarCode { set; get; }
        public string ReadyWork
        {
            get
            {
                string msg = "";
                //if (string.IsNullOrEmpty(cmbLine.Text.Trim()))
                //    msg += "- Line is Empty\n";

                //if (string.IsNullOrEmpty(BarCode))
                //    msg += "- BarCode is Empty\n";
                return msg;
            }
        }
        public DataTable CreateTypeLoad
        {
            set
            {
                cmbCreateType.DataSource = value;
                cmbCreateType.DisplayMember = "Type";
                cmbCreateType.ValueMember = "ID";
            }
        }   
    }
}
