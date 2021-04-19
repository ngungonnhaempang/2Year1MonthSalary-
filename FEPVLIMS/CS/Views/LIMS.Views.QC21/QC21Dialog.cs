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
    public partial class QC21Dialog : Infrastructure.StyleForm
    {
        public QC21Dialog()
        {
            InitializeComponent();
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

        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
                    msg += "Reason is Empty";
                return msg;
            }
        }

        public string Reason 
        {
            set
            {
                txtRemark.Text = value;
                txtRemark.Enabled = false;
            } 
        }

        public void Clear()
        {
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
    }
}
