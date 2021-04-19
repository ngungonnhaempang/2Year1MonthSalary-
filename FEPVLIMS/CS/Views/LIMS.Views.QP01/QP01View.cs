using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Linq;
using DevExpress.Utils;
using LIMS.BLL;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using LIMS.Model;
using MIS.Utility;


namespace LIMS.Views
{
    public partial class QP01View : UserControl
    {
        public QP01View()
        {
            InitializeComponent();
            RegisterCommand();
        }

        QCReporting report = new QCReporting();

        public void RegisterCommand()
        {
            gridView1.Click += gridView1_Click;
            gridView1.RowStyle += gridView1_RowStyle;
            cStartEnable.CheckedChanged += cStartEnable_CheckedChanged;
        }

        void cStartEnable_CheckedChanged(object sender, EventArgs e)
        {
            QueryPlanJob();

            if (eventgridView1_Click != null)
                eventgridView1_Click(cStartEnable.Checked, EventArgs.Empty);
        }

        public void QueryPlanJob()
        {
            PlanTimeJobData = report.GetQCReport("QP01_QueryGetPlanJob",
                new string[] { "Enable", "Language" },
                new object[] { StartEnable, MyLanguage.Language }).Tables[0];
        }

        void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                DataRow selectrow = gridView1.GetDataRow(e.RowHandle);
                if ((bool)selectrow["Enabled"])
                    e.Appearance.ForeColor = Color.CornflowerBlue;
            }
            catch { }
        }

        public PlanTimeJob planTemp { get; set; }
        public EventHandler eventgridView1_Click;
        void gridView1_Click(object sender, EventArgs e)
        {
            Dictionary<string, object> paramenters = new Dictionary<string, object>();

            int rowCount = gridView1.SelectedRowsCount;
            if (rowCount != 1)
                return;

            DataRow row = gridView1.GetDataRow(gridView1.GetSelectedRows()[0]);

            foreach (DataColumn c in row.Table.Columns)
            {
                paramenters.Add(c.ColumnName, row[c.ColumnName]);
            }

            PlanTimeJob _planjob = new PlanTimeJob
            {
                JobID = new Guid(paramenters["JobID"].ToString()),
                ScheduleUID = new Guid(paramenters["ScheduleUID"].ToString()),
                ScheduleName = paramenters["ScheduleName"].ToString(),
                Enabled = (bool)paramenters["Enabled"]
            };
            planTemp = _planjob;
            if (eventgridView1_Click != null)
                eventgridView1_Click(_planjob.Enabled, EventArgs.Empty);
        }        

        public DataTable PlanTimeJobData
        {
            set
            {
                gridControl1.DataSource = value;
                gridView1.Columns["JobID"].Visible = false;
                gridView1.Columns["ScheduleUID"].Visible = false;
                gridView1.Columns["CreateDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["CreateDate"].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
                gridView1.Columns["AccDate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridView1.Columns["AccDate"].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";

                gridView1.BestFitColumns();
            }
        }

        public bool StartEnable
        {
            get { return cStartEnable.Checked; }
        }
    }
}
