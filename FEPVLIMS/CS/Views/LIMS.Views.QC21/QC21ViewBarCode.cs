using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIMS.Views
{
    public partial class QC21ViewBarCode : Infrastructure.StyleForm
    {
        public QC21ViewBarCode()
        {
          
            InitializeComponent();
            Initial();

            dateProdDate.DateTime =Convert.ToDateTime( DateTime.Now.ToString("yyyy-MM-dd"));

        }
        void Initial()
        {
            btnClose.Click += BtnClose_Click;
            btnSearch.Click += BtnSearch_Click;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
         
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
        
            this.Close();
        }

     

        public DateTime ProductDate
        {
            set { dateProdDate.DateTime = value; }
            get { return dateProdDate.DateTime; }
        }

        public string BarCode
        {
            set
            {
                 txtBarcode.Text = value;
            }
            get
            {
                return txtBarcode.Text;
            }
        }
    }
}
