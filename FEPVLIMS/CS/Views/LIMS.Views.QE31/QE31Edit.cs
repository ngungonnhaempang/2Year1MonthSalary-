using System;
using System.Data;
using LIMS.BLL;

namespace LIMS.Views
{
    public partial class QE31Edit : Infrastructure.StyleForm
    {
        public QE31Edit()
        {
            InitializeComponent();
            btYes.Click += btYes_Click;
            btNo.Click += btNO_Click;
            ceMaterial.CheckedChanged += ceMaterial_CheckedChanged;
        }

        public EventHandler eventShowMessage;

        public string msg { set { if (eventShowMessage != null) eventShowMessage(value, EventArgs.Empty); } }
        private void btYes_Click(object sender, EventArgs e)
        {
            if (ReadyWork == "")
            {
                DOCBiz docbiz = new DOCBiz();
                //WriteTips(5, "NO implementation",false);
                _rValue = docbiz.DocUpdateMaterialNo(txtVoucherID.Text, cmbMaterialList.SelectedValue.ToString(), txtRemark.Text);
                if (_rValue)
                {
                    msg = string.Format("Change Material from {0} --> {1} successful!", txtMaterialNo.Text, cmbMaterialList.SelectedValue.ToString());
                    this.Close();
                }
                else
                    msg = string.Format("Change Material from {0} --> {1} failure!", txtMaterialNo.Text, cmbMaterialList.SelectedValue.ToString());

            }
            else
                msg = ReadyWork;
        }

        private void btNO_Click(object sender, EventArgs e)
        {

            _rValue = false;
            this.Close();
        }

        private void ceMaterial_CheckedChanged(object sender, EventArgs e)
        {
            cmbMaterialList.Enabled = !cmbMaterialList.Enabled;
        }

        public string[] Paras 
        { 
            set 
            { 
                txtVoucherID.Text = value[0];
                txtSampleName.Text = value[1];
                txtMaterialNo.Text = value[2];
            }
            get
            {
                return new string[] { txtMaterialNo.Text, cmbMaterialList.SelectedValue.ToString(), txtRemark.Text };
            }
        }

        public DataTable MaterialList
        {
            set
            {
                cmbMaterialList.DataSource = value;
                cmbMaterialList.DisplayMember = "LOT_NO";
                cmbMaterialList.ValueMember = "LOT_NO";
            }
        }

        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
                    msg += "Reason is Empty";
                if(!ceMaterial.Checked || false || false)
                    msg += "/nNo any items is pick";
                //msg += Translator("QC01_MSG_10");
                return msg;
            }
        }

        public void Clear()
        {
            txtRemark.Text = string.Empty;
            ceMaterial.Checked = false;
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
