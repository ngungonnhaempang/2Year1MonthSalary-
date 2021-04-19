using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using LIMS.BLL;
using LIMS.Model;
using MIS.Utility;

namespace LIMS.Views
{
    public partial class SP01View : UserControl
    {
        public SP01View()
        {
            InitializeComponent();
            RegisterCommand();
            _SelectedSampleName = "";
        }

        private void RegisterCommand()
        {
            gridView1.Click += new EventHandler(gridView1_Click);
            gridView1.DoubleClick += new EventHandler(gridView1_DoubleClick);
            gridView1.CustomDrawCell += gridView1_CustomDrawCell;
            cmbLabName.SelectedIndexChanged += cmbLabName_SelectedIndexChanged;
            cmbTypeName.SelectedIndexChanged += cmbTypeName_SelectedIndexChanged;
            cmbSampleName.SelectedIndexChanged += cmbSampleName_SelectedIndexChanged;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView1.IsRowSelected(e.RowHandle))
            {
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.ForeColor = Color.White;
                e.Appearance.Options.UseBackColor = true;
                e.Appearance.BackColor = Color.DeepSkyBlue;
            }
        }

        public event EventHandler eventcmbTypeName_SelectedIndexChanged;

        private void cmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbTypeName_SelectedIndexChanged != null)
                eventcmbTypeName_SelectedIndexChanged(sender, e);
        }

        public event EventHandler eventcmbLabName_SelectedIndexChanged;

        private void cmbLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbLabName_SelectedIndexChanged != null)
                eventcmbLabName_SelectedIndexChanged(sender, e);
        }

        public event EventHandler eventcmbSampleName_SelectedIndexChanged;

        private void cmbSampleName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eventcmbSampleName_SelectedIndexChanged != null)
                eventcmbSampleName_SelectedIndexChanged(sender, e);
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;
            _SelectedSampleName = gridView1.GetDataRow(gridView1.GetSelectedRows()[0])["SampleName"].ToString();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            Dictionary<string, object> paramenters = new Dictionary<string, object>();
            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);
            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName]);
            }
            if (EditEvent != null)
            {
                GetSample = new QCSample
                {
                    SampleName = paramenters["SampleName"].ToString(),
                    LabID = this.LabID,
                    TypeID = this.TypeID,
                    Description_EN = paramenters["Description_EN"].ToString(),
                    Description_TW = paramenters["Description_TW"].ToString(),
                    Description_CN = paramenters["Description_CN"].ToString(),
                    Description_VN = paramenters["Description_VN"].ToString(),
                    AB =paramenters["AB"].ToString()
                };

              
                EditEvent(sender, e);
            }
        }

        public event EventHandler EditEvent;

        public QCSample GetSample { get; set; }

   

        public string LabName
        {
            get
            {
                return cmbLabName.Text.Trim();
            }
           
        }

     

        public int LabID
        {
            get
            {
                if (cmbLabName.SelectedItem != null)
                    return Convert.ToInt32(((DataRowView)cmbLabName.SelectedItem).Row["LabID"].ToString());
                return 0;
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

        public DataTable LabNameLoad
        {
            set
            {
                DataRow dr = value.NewRow();
                dr["LabName"] = "";
                dr["LabID"] = "0";
                value.Rows.InsertAt(dr, 0);
                cmbLabName.DataSource = value;
                cmbLabName.DisplayMember = "LabName";
                cmbLabName.ValueMember = "LabID";
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

        public DataTable tableMList
        {
            set
            {
                gridControl1.DataSource = value;
                gridView1.BestFitColumns();
            }
        }

        public string _SelectedSampleName { get; set; }
    }
}