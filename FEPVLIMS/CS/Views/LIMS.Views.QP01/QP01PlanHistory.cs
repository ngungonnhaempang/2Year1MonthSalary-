using System;
using System.Windows.Forms;
using LIMS.BLL;
using System.Collections;
using LIMS.Model;

namespace LIMS.Views
{
    public partial class QP01PlanHistory : UserControl
    {
        public QP01PlanHistory()
        {
            InitializeComponent();
        }

        QCReporting report = new QCReporting();
        public void LogsSource(PlanTimeJob plan)
        {
            gridControl1.DataSource = report.GetQCReport("QP01_QueryGetScheduleLog", new string[] { "ID", "ScheduleUID" }, new object[] { plan.JobID,plan.ScheduleUID }).Tables[0];
            gridView1.Columns["Stamp"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["Stamp"].DisplayFormat.FormatString = "dd-MM-yyyy HH:mm:ss";
            gridView1.BestFitColumns();
        }
    }
}