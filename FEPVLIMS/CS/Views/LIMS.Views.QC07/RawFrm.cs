using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLanuage;

namespace LIMS.Views.QC07
{
    public partial class RawFrm  :  Infrastructure.StyleForm
    {
        public RawFrm()
        {
            InitializeComponent();
            txtDate.EditValue = DateTime.Today.ToString("yyyy-MM-dd");
            txtDate.Properties.EditMask = "yyy-MM-dd";
            txtDateofSample.EditValue = DateTime.Today.ToString("yyyy-MM-dd");
            txtDateofSample.Properties.EditMask = "yyy-MM-dd";

            btSave.Click += btSave_Click;
            btCancel.Click += btCancel_Click;
           
            #region language
            try
            {
                 CultureLanuage.ApplyResourcesFrom(this, "QC07", this.Name);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        void btCancel_Click(object sender, EventArgs e)
        {
            rValue = false;
            this.Close();
            ResetForm();

        }
        public void ResetForm()
        {           
            vSampleName.Text = string.Empty;
            txtMaterialNO.Text = string.Empty;
            txtReceiveNum.Text = string.Empty;
            txtGR_NO.Text = string.Empty;
            txtGradeVersion.Text = string.Empty;
            txtUnit.Text = string.Empty;
           
        }
     
       public DateTime SheetDate
        {
            get
            {
                return DateTime.Parse(txtDate.Text.ToString());
            }
        }
        public DateTime DateOfSample
        {
            get
            {
                return DateTime.Parse(txtDateofSample.Text.ToString());
            }
        }
        void btSave_Click(object sender, EventArgs e)
        {
            string msg = IsReady;
            if (string.IsNullOrEmpty(msg))
            {
                rValue = true;
                this.Close();               
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        public string IsReady
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (string.IsNullOrEmpty(SampleName))
                    msg.Append("SampleName is empty");
                if (string.IsNullOrEmpty(txtGradeVersion.Text))
                    msg.Append("/GradeVersion is empty");
                if (ReceiveNum <= 0)
                    msg.Append("/Sample quantity is zero");

                if (string.IsNullOrEmpty(LotNO))
                {
                    msg.Append("/LotNOcannot be empty");
                }
                if (string.IsNullOrEmpty(GR_Unit))
                {
                    msg.Append("/GR_Unit cannot be empty");
                }
                return msg.ToString();
            }
        }
        bool rValue = false;
        public bool RValue
        {
            get { return rValue; }
        }
        public string SampleDes {


            set
            {
               
                this.txtSampleName.Text = value;
            }
            get { return this.txtSampleName.Text.Trim(); }
        }
        public string LotNO
        {
            set { this.txtMaterialNO.Text = value; }
            get { return this.txtMaterialNO.Text.Trim(); }
        }
        public string SampleName 
        {
            set { this. vSampleName.Text = value; }
            get { return this.vSampleName.Text.Trim(); }
        }

        public decimal ReceiveNum
        {
            get
            {
                return Convert.ToDecimal(this.txtReceiveNum.Text);
            }

            set
            {
              
                txtReceiveNum.Text = (value <=0 ? "1": value.ToString());
            }

        }
        # region Create by Isaac 2018-11-29  For EBELN EBELP
        /// <summary>
        /// Create by Isaac 2018-11-29  For EBELN EBELP
        /// Quantity
        /// </summary>
        public decimal Quantity
        {
            set { txtQuantity.Text = value.ToString(); }
            get
            {
                return Convert.ToDecimal(this.txtQuantity.Text);
            }
        }      
        public string GR_NO
        {
            get { return txtGR_NO.Text.Trim(); }
            set { txtGR_NO.Text = value; }
        }
        public string EBELN { set; get; }
        public string EBELP { set; get; }

        #endregion
        public string GradeVersion
        {
            get { return txtGradeVersion.Text.Trim();} 
            set { txtGradeVersion.Text = value; }
        }
        /// <summary>
        /// Create by Isaac
        /// </summary>
        public string GR_Unit
        {
            get { return txtUnit.Text.Trim(); }
            set { txtUnit.Text = value; }
        }

		public string Vendor
		{
			get { return txtVendor.Text.Trim(); }
			set { txtVendor.Text = value; }
		}
	}
}
