using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using BasicLanuage;
using MIS.Utility;
using LIMS.BLL;
using LIMS.Model;

namespace LIMS.Views.QC07
{
    public partial class SerchPlanView : UserControl
    {
        QCReporting rep = new QCReporting();
        public SerchPlanView()
        {
            InitializeComponent();
            #region language
            try
            {
                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(this, "QC07", this.Name);
                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    this.gcList.DataSource = gridData;
                    gridView1.BestFitColumns();
                    CultureLanuage.GridResourcesFrom(gridView1, "gridView1", dsgrid);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            #endregion

            deVoufd.Text = DateTime.Now.Date.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
          //  deVoufd.Text = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
         //   this.gcSahList.DoubleClick += new System.EventHandler(this.gcSahList_DoubleClick);

            cmbTypeName.SelectedIndexChanged += cmbTypeName_SelectedIndexChanged;
    
        }
        public event EventHandler eventcmbTypeName_SelectedIndexChanged;
        private void cmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbTypeName_SelectedIndexChanged != null)
                eventcmbTypeName_SelectedIndexChanged(sender, e);
        }

      
        DateTime? B
        {
            get
            {
                if (deVoufd.Text == "")
                    return null;
                else
                    return Convert.ToDateTime(deVoufd.Text);
            }
            
        }

        DateTime? E
        {
            get
            {
                if (deVoutd.Text == "")
                    return null;
                else
                    return Convert.ToDateTime(deVoutd.Text);
            }
            
        }
        public int TypeID
        {
            get
            {
                if (cmbTypeName.SelectedItem != null)
                    return Convert.ToInt32(((DataRowView)cmbTypeName.SelectedItem).Row["TypeID"].ToString());
                return 0;
            }
        }
        public DataTable TypeNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["TypeName"] = "";
                dr["TypeID"] = "0";
                value.Rows.InsertAt(dr, 0);

                cmbTypeName.DataSource = value;
                cmbTypeName.DisplayMember = "TypeName";
                cmbTypeName.ValueMember = "TypeID";
            }
        }
        public string TypeName
        {
            get
            {
                return cmbTypeName.Text.Trim();
            }
        }

        public string SampleName
        {
            get { return cmbSampleName.SelectedValue.ToString(); }

            set { cmbSampleName.Text = value; }
        }
  
        public DataTable SampleNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["SampleName"] = "";
                value.Rows.InsertAt(dr, 0);

                cmbSampleName.DataSource = value;
                cmbSampleName.DisplayMember = "Description_" + MyLanguage.Language;
                cmbSampleName.ValueMember = "SampleName";
            }
        }
        public string SampleDes { get; set; }
        public string RSampleName { get; set; }
        public string RLotNO { get; set; }
        public bool ALL
        {
            get
            {
                return chkAll.Checked;
            }
            set { chkAll.Checked = value; }
        }

        public bool MySelf
        {
            get { return cbMySelf.Checked; }

            set { cbMySelf.Checked = value; }
        }
        #region Create by Isaac 2018-11-29
        /// <summary>
        /// Create by Isaac 2018-11-29
        /// EBELN, EBELP,GR_NO,Quantity
        /// </summary>
        public string GR_NO { get; set; }

        public string GR_Unit { set; get; }
		public string Vendor { set; get; }
		public decimal Quantity { set; get; }     

        public string EBELP { set; get; }
        public string EBELN { set; get; }

        public Decimal ReceiveNum { set; get; }
        public DateTime SheetDate { set;  get; }
		public DateTime DateOfSample { set; get; }

        #endregion


        public string[] VoucherIDs
        {
            get
            {
                List<string> voucherIds = new List<string>();

                ArrayList rows = new ArrayList();

                // Add the selected rows to the list.
                int rowCount = gridView1.SelectedRowsCount;
                if (rowCount <= 0)
                    return voucherIds.ToArray();

                for (int i = 0; i < rowCount; i++)
                {
                    if (gridView1.GetSelectedRows()[i] >= 0)
                        rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));
                }

                ///
                foreach (DataRow r in rows)
                {                  
                    if(string.IsNullOrEmpty(r["ReceiveID"].ToString()))
                    {
                        MessageBox.Show("This VoucherID didn't exists in database! Please check again.","VoucherID not exists.",MessageBoxButtons.OK,MessageBoxIcon.Error);

                    }
                    else{
                        
                        voucherIds.Add((string)r["ReceiveID"]);
                    }
                    
                }
                return voucherIds.ToArray();
            }
        }
        public string[] dtSelectBarCodes4Query
        {
            get
            {
             
               
                List<string> barcodes = new List<string>();
                ArrayList rows = new ArrayList();

                // Add the selected rows to the list.
                int rowCount = gridView1.SelectedRowsCount;
                if (rowCount <= 0)
                    return barcodes.ToArray();


             
                for (int i = 0; i < rowCount; i++)
                {
                   
                    if (gridView1.GetSelectedRows()[i] >= 0)
                    {
                        rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[i]));

                    }
                }
                SampleDes = ((DataRow)rows[0])["LOTNOSPc"].ToString();
                 RSampleName = ((DataRow)rows[0])["SampleName"].ToString();
                 RLotNO = ((DataRow)rows[0])["LOT_NO"].ToString();
                #region Create By Isaac 2018-11-29
                 GR_NO = ((DataRow)rows[0])["GR_NO"].ToString();
                 GR_Unit = ((DataRow)rows[0])["GR_Unit"].ToString();//Create by Isaac 2018-08-28
                 Quantity = decimal.Parse(((DataRow)rows[0])["Quatity"].ToString());
                 EBELN = ((DataRow)rows[0])["EBELN"].ToString();
                 EBELP = ((DataRow)rows[0])["EBELP"].ToString();
				 Vendor = ((DataRow)rows[0])["Name1"].ToString();
				//ReceiveNum = decimal.Parse(((DataRow)rows[0])["ReceiveNum"].ToString());


				#endregion
				foreach (DataRow r in rows)
                {
                    if (RSampleName != r["SampleName"].ToString())
                    {
                        MessageBox.Show("SampleName is not the same ");
                        return new string[]{};
                    }
                    if (RLotNO != r["LOT_NO"].ToString())
                    {
                        MessageBox.Show("LOT_NO is not the same ");
                        return new string[] { };
                    }
                    if (GR_NO != r["GR_NO"].ToString())
                    {
                        MessageBox.Show("GR_NO is not the same ");
                        return new string[] { };
                    }
                  
                    if (!string.IsNullOrEmpty(r["ReceiveID"].ToString()))
                    {
                        MessageBox.Show("this Row already exsit ");
                        return new string[] { };
                    }
                    barcodes.Add((string)r["VoucherID"]);
                }

            
                return barcodes.ToArray();
            }
        }

        public string[] Parameters
        {
            get
            {
                return new string[] { "B", "E", "EBELN", "PONO", "SampleName","IsMySelf", "ISALL", "Lang" };
            }
        }

      
        public object[] Values
        {
            get { return new object[] { B, E, txtVBELN.Text.Trim(), txtEBLEN.Text.Trim().ToUpper(), SampleName,MySelf, ALL, MyLanguage.Language }; }
        }

        public DataTable dtSah
        {
            set
            {
                //gridView3.Columns.Clear(); Not use here because it auto clear all language firstly load data
                this.gcList.DataSource = value;
                gridView1.BestFitColumns();
            }
        }

       

        //string SelectVbeln
        //{
        //    get
        //    {
        //        ArrayList rows = new ArrayList();

        //        // Add the selected rows to the list.
        //        int rowCount = gridView1.SelectedRowsCount;
        //        if (rowCount != 1)
        //            return "";

        //        if (gridView1.GetSelectedRows()[0] >= 0)
        //            rows.Add(gridView1.GetDataRow(gridView1.GetSelectedRows()[0]));

        //        return ((DataRow)rows[0])["VBELN"].ToString();
        //    }
        //}

     

       
      
    }
}
