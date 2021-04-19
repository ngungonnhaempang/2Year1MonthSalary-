using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LIMS.Model;
using LIMS.BLL;
using MIS.Utility;

namespace LIMS.Views
{
    public partial class AT01Dialog : Infrastructure.StyleForm
    {
        public AT01Dialog()
        {
            InitializeComponent();
            this.txtSampleName.Enabled = false;
            btCancel.Click += new System.EventHandler(this.btCancel_Click);
            btSave.Click += new System.EventHandler(this.btSave_Click);
        }

    
        AttributeBiz biz = new AttributeBiz();

        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(txtPropertyName.Text.Trim()))
                    msg +=  "Property Name Empty\n";

                if (string.IsNullOrEmpty(txtSpec_EN.Text.Trim()))
                    msg +=  "Description (EN) Empty\n";

                if (string.IsNullOrEmpty(txtSpec_TW.Text.Trim()))
                    msg +=  "Description (TW) Empty\n";

                if (string.IsNullOrEmpty(txtSpec_CN.Text.Trim()))
                    msg +="Description (CN) Empty\n";

                if (string.IsNullOrEmpty(txtSpec_VN.Text.Trim()))
                    msg +="Description (VN) Empty\n";

                if (string.IsNullOrEmpty(msg.ToString()))
                    return "";
                else
                    return msg.ToString();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            string msg = ReadyWork;
            string _msg = "";
             var attribut=  GetAttribute();
            if (string.IsNullOrEmpty(msg))
            {               
                if (txtPropertyName.Enabled == true)
                {
                    if (!biz.AddAttribute(attribut, out mes))
                        MessageBox.Show(_msg);                      
                        
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        mes = "";                      
                       
                    }
                }
                else
                {
                    Console.WriteLine(attribut.Description_EN);
                    if (!biz.UpdateAttribute(attribut))
                        MessageBox.Show("Save Fail!");
                    else
                        mes = "";
                    this.DialogResult = DialogResult.OK;                   
                }
            }
            else
            {
                MessageBox.Show(msg);              
            } 
            
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        public string SetNull
        {
            set
            {
                txtPropertyName.Text = value;
                txtSpec_EN.Text = value;
                txtSpec_TW.Text = value;
                txtSpec_CN.Text = value;
                txtSpec_VN.Text = value;
                txtUnit.Text = value;
                _Prec.Text = "0";
                txtRemark.Text = value;
                txtEnRemark.Text = value;
                txtJPNRemark.Text = value;               
                txtPropertyName.Enabled = true;
                txtOrderBy.Text = value;// Added by Isaac for get OrderBy
            }
        }

        public QCAttribute GetAttribute()
        {
            QCAttribute attr = new QCAttribute();

            attr.SampleName = this.txtSampleName.Text.Trim();
            attr.PropertyName = txtPropertyName.Text.Trim();
            attr.Description_EN = txtSpec_EN.Text.Trim();
            attr.Description_TW = txtSpec_TW.Text.Trim();
            attr.Description_CN = txtSpec_CN.Text.Trim();
            attr.Description_VN = txtSpec_VN.Text.Trim();
            attr.Unit = txtUnit.Text.Trim();
            attr.Prec = Convert.ToInt32(_Prec.Text.Trim());
            attr.Remark = txtRemark.Text;
            attr.EnRemark = txtEnRemark.Text;
            attr.JPNRemark = txtJPNRemark.Text;
            attr.OrderBy = Convert.ToInt32(txtOrderBy.Text.Trim());//Added By Isaac 05-29-2018 03:20 PM
            return attr;
        }

        public QCAttribute SetAttribute
        {
            set
            {
              
                txtSampleName.Text = value.SampleName;
                txtPropertyName.Text = value.PropertyName;
                txtSpec_EN.Text = value.Description_EN;
                txtSpec_TW.Text = value.Description_TW;
                txtSpec_CN.Text = value.Description_CN;
                txtSpec_VN.Text = value.Description_VN;
                txtUnit.Text = value.Unit;
                _Prec.Text = value.Prec.ToString();
                txtRemark.Text = value.Remark.ToString();
                txtEnRemark.Text = value.EnRemark.ToString();
                txtJPNRemark.Text = value.JPNRemark.ToString();
                txtOrderBy.Text = value.OrderBy.ToString();
                string _PropertyName = this.txtPropertyName.Text;
                if (string.IsNullOrEmpty(_PropertyName))
                {
                    txtPropertyName.Enabled = true;
                }
                else
                {
                    txtPropertyName.Enabled = false;
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
    }
}
