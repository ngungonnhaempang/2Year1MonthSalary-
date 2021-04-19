using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLanuage;
using LIMS.BLL;
//using Newtonsoft.Json;

namespace LIMS.Views.CU18
{
    public partial class MainForm :  Infrastructure.BaseForm
    {
  
        public MainForm()
        {
            InitializeComponent();

            CultureLanuage.ApplyResourcesFrom(this, "CU18", "MainForm");
        }

        DataTable dtlist;
        BindingSource bslist;
        public event EventHandler GetGoodsColumnsName;
        DataRow rows;
        SampleBiz report = new SampleBiz();
        public DataTable setGoodsColumnsName
        {
            set { dtlist = value; }
        }


        void GetList(string username)
        {
            listUserSample = null;
            listUserSample = report.QuerySampleUser(UserID);
        }
        public DataTable listUserSample
        {
            set
            {
                if (value != null)
                {

                    dtlist.Merge(value);
                }
                else
                    dtlist.Clear();

                gridView1.BestFitColumns();
            }
            get
            {
                return dtlist;
            }
        }
        public DataRow selectCenter
        {
            get
            {
                List<string> barcodes = new List<string>();
                ArrayList rows = new ArrayList();

                // Add the selected rows to the list.
                int rowCount = gridView1.SelectedRowsCount;

                for (int i = 0; i < rowCount; i++)
                {
                    if (gridView1.GetSelectedRows()[i] >= 0)
                        rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                }
                return ((DataRow)rows[0]);
            }
        }
      
        #region  Event
 
        /// <summary>
        /// 给用户设置成本中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rows["SampleName"].ToString()) || string.IsNullOrEmpty(UserID))
            {
                WriteTips(5, "Please ,Select SampleName");
                return;
            }
            else
            {

                report.AssignUserForSample(txtUser.Text.Trim(), rows["SampleName"].ToString());


                GetList(txtUser.Text.Trim());
            }

        }



        private void CU16_Load(object sender, EventArgs e)
        {
            dtlist = report.QuerySampleUser("ww");

            bslist = new BindingSource();

            bslist.DataSource = dtlist;
            gridCenter.DataSource = bslist;
        }

        public string UserID {

            get { return this.txtUser.Text.Trim(); }
        }
        private void btSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtUser.Text.Trim()))
            {
                WriteTips(4, "UserName cann't NULL");
                return;
            }
            else
            {

                GetList(txtUser.Text.Trim());
            }
        }
        /// <summary>
        /// Assign成本中心
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAssign_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(selectCenter["SampleName"].ToString()) || string.IsNullOrEmpty(this.txtUser.Text.Trim()))
            {
                WriteTips(5, "Please ,Select SampleName");
                return;
            }
            else
            {
                if (btAssign.Text == "Cancel")
                {
                    report.DeleteSampleUser(txtUser.Text.Trim(), selectCenter["SampleName"].ToString());
                }
                else
                {
                    report.AssignUserForSample(txtUser.Text.Trim(), selectCenter["SampleName"].ToString());
                }
                GetList(txtUser.Text.Trim());
            }
        }

        private void gridCenter_Click(object sender, EventArgs e)
        {
            try
            {
                rows = selectCenter;

                if (!string.IsNullOrEmpty(rows["UserID"].ToString()))
                {
                    btAssign.Text = "Cancel";
                }
                else
                {
                    btAssign.Text = "Assign";
                }
            }
            catch (Exception ex)
            {
                return;

            }
        }



        #endregion
       
      
    }
}
