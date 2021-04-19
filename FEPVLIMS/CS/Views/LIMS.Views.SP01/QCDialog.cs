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

namespace LIMS.Views
{
    public partial class QCDialog : Infrastructure.StyleForm
    {
        public QCDialog()
        {
            InitializeComponent();
            btYes.Click += btYes_Click;
            btNO.Click += btNO_Click;
            cmLine.Items.AddRange(new object[] {"",
                        "C",
                        "L","S"});
          
            
        }
        public void SetLine(bool vi)
        {

            laLine.Visible = vi;
            cmLine.Visible = vi;
            if (vi == false)
            {
                cmLine.SelectedValue = "";
            }
        
        }

        private bool rValue = false;

        private void btYes_Click(object sender, EventArgs e)
        {
            rValue = true;
            this.Close();
        }

        private void btNO_Click(object sender, EventArgs e)
        {
            rValue = false;
            this.Close();
        }

        private int LabID = 0;
        public int TypeID { set; get; }
        public string LabName {

            set { txtLabName.Text = value; }
        }
        public string TypeName
        {

            set { this.txtTypeName.Text = value; }
        }
        public QCSample Sample
        {
            get
            {
                return new QCSample
                {
                    SampleName = txtSampleName.Text.Trim(),
                   // LabName = txtLabName.Text.Trim(),
                    LabID = this.LabID,
                   // TypeName = txtTypeName.Text.Trim(),
                    TypeID = this.TypeID,
                    Description_EN = txtSampleSpecEN.Text.Trim(),
                    Description_TW = txtSampleSpecTW.Text.Trim(),
                    Description_CN = txtSampleSpecCN.Text.Trim(),
                    Description_VN = txtSampleSpecVN.Text.Trim(),
                    AB = cmLine.Text
                };
            }
            set
            {
                txtSampleName.Text = value.SampleName;
             
                txtSampleSpecEN.Text = value.Description_EN;
                txtSampleSpecTW.Text = value.Description_TW;
                txtSampleSpecCN.Text = value.Description_CN;
                txtSampleSpecVN.Text = value.Description_VN;
                LabID = value.LabID;
                TypeID = value.TypeID;
                cmLine.Text = value.AB;
            }
        }

        public string ReadyWork
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (string.IsNullOrEmpty(txtSampleSpecEN.Text) || string.IsNullOrEmpty(txtSampleSpecCN.Text) ||
                    string.IsNullOrEmpty(txtSampleSpecTW.Text) || string.IsNullOrEmpty(txtSampleSpecVN.Text))
                    msg .Append( "Please Fill all languages!");

                if (TypeID == 2)
                { 
                    if(string.IsNullOrEmpty(cmLine.Text))
                    {
                        msg.Append("AB can not null");
                    }
                }
                return msg.ToString();
            }
        }

        public bool RValue
        {
            get
            {
                return rValue;
            }
        }
        public DataTable _cmLineData
        {
            set
            {

                cmLine.DataSource = value.DefaultView;
                cmLine.DisplayMember = "LINE";
                cmLine.ValueMember = "LINE";
            }
        }

        public STEP _STEP
        {
            set
            {
                if (value == STEP.Add)
                {
                    txtSampleName.Enabled = true;
                    txtTypeName.Enabled = false;
                    txtLabName.Enabled = false;
                }

                if (value == STEP.Edit)
                {
                    txtSampleName.Enabled = false;
                    txtTypeName.Enabled = false;
                    txtLabName.Enabled = false;
                }
            }
        }
    }

    public enum STEP { Add, Edit }
}