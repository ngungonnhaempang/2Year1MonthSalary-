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

namespace LIMS.Views
{
    public partial class AddPOLYSSP : Infrastructure.StyleForm
    {
      
        public AddPOLYSSP(string sampleName,string materialNo,bool IsShowSampleID)
        {
            InitializeComponent();


            btYes.Click += btYes_Click;
            btNO.Click += btNO_Click;
            dateProdDate.DateTime = DateTime.Now;
            this.cbHasChart.Checked = true;
            //if (!IsShowSampleID)
            //{
               // this.txtSampleID.Enabled = false;
                cmbLine.SelectedValueChanged += eventSetSampleID;
                dateProdDate.DateTimeChanged += eventSetSampleID;
          //  }
           // else
           // {
                this.txtSampleID.Enabled = true;
          //  }
            SampleName = sampleName;
            MaterialNo = materialNo;

            #region Language
            try
            {
                CultureLanuage.ApplyResourcesFrom(this, "QC21", "AddPOLYSSP");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Translate error:" + ex.Message);
            }
            #endregion
        }
        private void eventSetSampleID(object sender, EventArgs e)
        {
            txtSampleID.Text = Line + dateProdDate.DateTime.ToString("yyMMddHHmm");
        }
        public string SampleName { get; set; }
        public string MaterialNo { get; set; }
   
        private void btYes_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(ReadyWork))
            {
                _rValue = true;
                this.Close();
            }
            else
            {
                WriteTips(5000, ReadyWork,false); 
            }
              
        }


        public string SampleID {

            get { return this.txtSampleID.Text.Trim(); }
            set { txtSampleID.Text = value; }
        }

        private void btNO_Click(object sender, EventArgs e)
        {

            _rValue = false;
            this.Close();
        }
        public DOCPlan GetDocPlan {

            get {

                if (txtSampleID.Enabled)
                {
                    SampleID = txtSampleID.Text.Trim();
                }
                else
                {
                    SampleID = Line + dateProdDate.DateTime.ToString("yyMMddHHmm");
                }
                DOCPlan _DOC = new DOCPlan
                {
                    SampleName = SampleName,
                    LOT_NO = MaterialNo,
                    LINE = Line,
                    HasChart=isHasChar,
                    SheetDate = dateProdDate.DateTime,
                    ProdDate = dateProdDate.DateTime,
                    SampleID = this.SampleID,
                    State = "1",
                    grades = "0",
                    Grade = "A",
                    Remark = txtRemark.Text,
                    createType =int.Parse( cmbCreateType.SelectedValue.ToString())
                };

                return _DOC;
            }
        }

        public string isHasChar
        {
            get
            {
                if (cbHasChart.Checked)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }
        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(Line))
                    msg +="- Line is Empty";


                if (txtSampleID.Enabled)
                {
                    if (string.IsNullOrEmpty(SampleID))
                    {
                        msg += "SampleID is Empty";
                    }
                }
               

                return msg;
            }
        }

        public void Clear()
        {
           // Line = string.Empty;
            txtRemark.Text = string.Empty;
            SampleID = string.Empty;
        }

        public string Line {
            get { return cmbLine.SelectedValue.ToString(); }
            set { cmbLine.SelectedValue = value; }
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
        public DataTable LineList
        {
            set
            {
                this.cmbLine.DataSource = value;
                cmbLine.DisplayMember = "Line";
                cmbLine.ValueMember = "Line";
            }
        }
        bool _rValue = false;

        public bool rValue
        {
            get
            {
                return _rValue;
            }
        }
    }
}
