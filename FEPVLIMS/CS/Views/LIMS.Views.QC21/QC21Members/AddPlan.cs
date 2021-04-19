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
using DevExpress.XtraEditors.Controls;
using BasicLanuage;

namespace LIMS.Views
{
    /// <summary>
    /// 加测
    /// </summary>
    public partial class AddPlan : Infrastructure.StyleForm
    {
        public AddPlan()
        {
            InitializeComponent();
            btYes.Click += btYes_Click;
            btNO.Click += btNO_Click;
            cmbSample.SelectedIndexChanged += cmbSample_SelectedIndexChanged;          
            cmbLine.SelectedValueChanged += eventSetSampleID;
            dteProdDate.DateTimeChanged += eventSetSampleID;
            cmbTypeName.SelectionChangeCommitted+=cmbTypeName_SelectionChangeCommitted;
        }

        QCReporting report = new QCReporting();
        public string Line {
            get { return cmbLine.SelectedValue.ToString(); }
        }
        private void eventSetSampleID(object sender, EventArgs e)
        {
            txtSampleID.Text =dteProdDate.DateTime.ToString("yyMMddHHmm");
        }

        public void Clear()
        {
            dteProdDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            txtSampleID.Text = DateTime.Now.ToString("yyMMddHHmm");
            txtCompanyOffer.Text = string.Empty;
            txtCompanyProd.Text = string.Empty;
            txtRemark.Text = string.Empty;
        }
        public int Catelag
        {

            get { return int.Parse(cmbTypeName.SelectedValue.ToString()); }
        }
        public DataTable dtListTypeName
        {
            set
            {
                cmbTypeName.DataSource = value;
                cmbTypeName.DisplayMember = "TypeName";
                cmbTypeName.ValueMember = "TypeID";
            }
        }
        private void cmbTypeName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Console.WriteLine("cmbTypeName");
            SampleSource = report.GetQCReport("Q_SamplesByCategory", new[] { "TypeID" }, new object[] { Catelag }).Tables[0];
        }
        public EventHandler AddPlan_cmbSample_SelectedIndexChanged;

        private void cmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(cmbSample.SelectedIndex<0)
            {
                return;
            }
            if (AddPlan_cmbSample_SelectedIndexChanged != null)
                AddPlan_cmbSample_SelectedIndexChanged(sender, new ShowDetailsArg { ID = cmbSample.SelectedValue.ToString() });
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
        public DataTable CreateTypeLoad
        {
            set
            {
                cmbCreateType.DataSource = value;
                cmbCreateType.DisplayMember = "Type";
                cmbCreateType.ValueMember = "ID";
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
        public DataTable SampleSource
        {
            set
            {
              
                DataRow row = value.NewRow();
                value.Rows.Add(row);
              
                cmbSample.DataSource = value;
                cmbSample.DisplayMember = string.Format("{0}_{1}", "Description", MyLanguage.Language);
                cmbSample.ValueMember = "SampleName";
            }
        }

      
        public DataTable MaterialSource
        {
            set
            {
                cmbMaterial.DataSource = value;
                cmbMaterial.DisplayMember = string.Format("{0}_{1}", "Description", MyLanguage.Language);
                cmbMaterial.ValueMember = "LOT_NO";
            }
        }

        public DOCPlanAdd _DOC
        {
            get 
            {
                return new DOCPlanAdd
                {
                    SampleName = cmbSample.SelectedValue.ToString(),
                    LOT_NO = cmbMaterial.SelectedValue.ToString(),
                    LINE = Line,
              //      HasChart=isHasChar,
                    SheetDate = dteProdDate.DateTime,
                    SampleID = txtSampleID.Text,
                    CompanyOfferSample = txtCompanyOffer.Text,
                    CompanyProduce = txtCompanyProd.Text,
                    CreateType=int.Parse(cmbCreateType.SelectedValue.ToString()),
                    Purpose=txtRemark.Text.Trim()  
                 
                };

            }
        }

        public string SampleName
        {
            get
            {
                if (cmbSample.SelectedIndex < 0)
                {
                    return "";
                }
                return this.cmbSample.SelectedValue.ToString();
            }
        
        }
        public EventHandler NotReadyEvent;
        private void btYes_Click(object sender, EventArgs e)
        {

            if (ReadyWork == "")
            {
                _rValue = true;
                this.Close();
            }
            else
            {
                WriteTips(5, ReadyWork, false);
            }
        }

        private void btNO_Click(object sender, EventArgs e)
        {
            _rValue = false;
            this.Close();
        }

        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(Line))
                    msg +=  "- Line is Empty\n";
            
                if (string.IsNullOrEmpty(txtSampleID.Text.Trim()))
                    msg +="- SampleID is Empty\n";
                return msg;
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
