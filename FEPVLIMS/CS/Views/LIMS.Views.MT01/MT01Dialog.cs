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
using LIMS.Service;

namespace LIMS.Views
{
    public partial class MT01Dialog : Infrastructure.StyleForm
    {
        public MT01Dialog()
        {
            InitializeComponent();
            txtSample.Enabled = false;
            btCancle.Click += new System.EventHandler(this.btCancle_Click);
            btSave.Click += new System.EventHandler(this.btSave_Click);
            this.cbisGrade.Checked = true;
        }
        public bool GetisGrade
        {
            get
            {
                if (cbisGrade.Checked)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private QCReporting reports = new QCReporting();
        private MaterialBiz biz = new MaterialBiz();

        private void btSave_Click(object sender, EventArgs e)
        {
            string msg = ReadyWork;
            string _msg = "";
            if (string.IsNullOrEmpty(msg))
            {
                if (txtLOT_NO.Enabled == true)
                {
                    if (!biz.CreateMaterial(Material))
                        WriteTips(5, _msg);
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        mes = "";
                    }
                }
                else
                {
                    if (!biz.UpdateMaterial(Material))
                        MSG_Warning = "Save operation failed";
                    else
                        mes = "";
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MSG_Warning = msg;
            }
        }

        private void btCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(txtLOT_NO.Text.Trim()))
                    msg += "LOT_NO is Empty";
                if (string.IsNullOrEmpty(txtSpec_EN.Text.Trim()) || string.IsNullOrEmpty(txtSpec_TW.Text.Trim()) || string.IsNullOrEmpty(txtSpec_CN.Text.Trim()) || string.IsNullOrEmpty(txtSpec_VN.Text.Trim()))
                    msg += "Spec is Empty/";
                if (string.IsNullOrEmpty(msg.ToString()))
                    return "";
                else
                    return msg.ToString();
            }
        }

        public string SetNull
        {
            set
            {
                txtLOT_NO.Text = "";
                txtSpec_EN.Text = "";
                txtSpec_CN.Text = "";
                txtSpec_TW.Text = "";
                txtSpec_VN.Text = "";
                txtLOT_NO.Enabled = true;
            }
        }

        public string SampleName
        {
            set
            {
                txtSample.Text = value;
            }
        }

        public QCMaterial Material
        {
            get
            {
                return new QCMaterial
                {
                    SampleName = txtSample.Text,
                    LOT_NO = txtLOT_NO.Text,
                    Description_EN = txtSpec_EN.Text,
                    Description_TW = txtSpec_TW.Text,
                    Description_CN = txtSpec_CN.Text,
                    Description_VN = txtSpec_VN.Text,
                    IsGrade = GetisGrade
                };
            }
            set
            {
                txtSample.Text = value.SampleName;
                txtLOT_NO.Text = value.LOT_NO;
                txtSpec_EN.Text = value.Description_EN;
                txtSpec_CN.Text = value.Description_CN;
                txtSpec_TW.Text = value.Description_TW;
                txtSpec_VN.Text = value.Description_VN;
                cbisGrade.Checked = value.IsGrade;
                string _LOT_NO = this.txtLOT_NO.Text;
                if (string.IsNullOrEmpty(_LOT_NO))
                {
                    txtLOT_NO.Enabled = true;
                }
                else
                {
                    txtLOT_NO.Enabled = false;
                }
            }
        }

        private string mes = "";

        public string FormEditMes
        {
            set
            {
                mes = value;
            }
            get
            {
                return mes;
            }
        }

        public string MSG_Info { get; set; }
        public string MSG_Warning { get; set; }
    }
}