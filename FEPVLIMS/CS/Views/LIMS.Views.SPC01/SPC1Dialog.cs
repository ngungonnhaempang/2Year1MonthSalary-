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
using BasicLanuage;
using MIS.Utility;
using LIMS.Model;

namespace LIMS.Views
{
    public partial class SPC1Dialog : Infrastructure.StyleForm
    {
        public SPC1Dialog()
        {
            InitializeComponent();
            CultureLanuage.ApplyResourcesFrom(this, "SPC1", "SPC1Dialog");
            btYes.Click += btYes_Click;
            btNO.Click += btNO_Click;
        }

        private void btYes_Click(object sender, EventArgs e)
        {
            _rValue = true;
            this.Close();
        }

        private void btNO_Click(object sender, EventArgs e)
        {

            _rValue = false;
            this.Close();
        }

        public QCChart Chart
        {
            get
            {
                return new QCChart
                {
                    SampleName = txtSample.Text.Trim(),
                    MaterialNo = txtLOT_NO.Text.Trim(),
                    LineNo = txtLine.Text.Trim(),
                    Property = txtProperty.Text.Trim(),
                    UCL = Convert.ToDecimal(txtUCL.Text.Trim()),
                    LCL = Convert.ToDecimal(txtLCL.Text.Trim()),
                    Date = Convert.ToDateTime(txtDate.Text.Trim()),
                    Remark = txtRemark.Text.Trim()
                };
            }
            set
            {
                txtSample.Text = value.SampleName;
                txtLOT_NO.Text = value.MaterialNo;
                txtLine.Text = value.LineNo;
                txtProperty.Text = value.Property;
                txtUCL.Text = value.UCL.ToString();
                txtLCL.Text = value.LCL.ToString();
                txtDate.Text = value.Date.ToString("yyyy-MM-dd");
                txtRemark.Text = value.Remark;
            }
        }

        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(txtProperty.Text.Trim()))
                    msg += Translator(12, "\n - Property is Empty");

                if (string.IsNullOrEmpty(txtUCL.Text.Trim()))
                      msg += Translator(13, "\n - UCL is Empty");
                if (string.IsNullOrEmpty(txtLCL.Text.Trim()))
                    msg += Translator(14, "\n - LCL is Empty");

                if (msg == "")
                {
                    if (Convert.ToDecimal(txtUCL.Text.Trim()) < Convert.ToDecimal(txtLCL.Text.Trim()))
                        msg += Translator(15, "\n - Upper Control Line value can not less than Lower Control Line");
                }
                else
                    msg = Translator(16, "Can not Modify/Add Control Line cause:") + msg; 
                return msg;
            }
        }

        public void Clear()
        {
            txtSample.Text = string.Empty;
            txtLOT_NO.Text = string.Empty;
            txtLine.Text = string.Empty;
            txtProperty.Text = string.Empty;
            txtUCL.Text = string.Empty;
            txtLCL.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtRemark.Text = string.Empty;

        }

        bool _rValue = false;

        public bool rValue
        {
            get
            {
                return _rValue;
            }
        }

        public STEP _STEP
        {
            set
            {
                if (value == STEP.Add)
                {
                    //txtSample.Enabled = false;
                    //txtLOT_NO.Enabled = false;
                    //txtLine.Enabled = false;
                    txtProperty.Enabled = true;
                    //txtUCL.Enabled = true;
                    //txtLCL.Enabled = true;
                    //txtDate.Enabled = false;
                    //txtRemark.Enabled = true;
                }

                if (value == STEP.Edit)
                {
                    //txtSample.Enabled = false;
                    //txtLOT_NO.Enabled = false;
                    //txtLine.Enabled = false;
                    txtProperty.Enabled = false;
                    //txtUCL.Enabled = true;
                    //txtLCL.Enabled = true;
                    //txtDate.Enabled = false;
                    //txtRemark.Enabled = true;
                }
            }
        }

        string Translator(int ID, string Note)
        {
            return CultureLanuage.Translator("SPC1", ID, Note);
        }
    }

    public enum STEP { Add, Edit }
}
