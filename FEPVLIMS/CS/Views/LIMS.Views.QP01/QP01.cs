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
using LIMS.Model;

namespace LIMS.Views
{public partial class QP01 : Infrastructure.BaseForm
    {
        public QP01()
        {
            InitializeComponent();
            RegisterCommand();
            Step = QP01Step.ShowView;


        }

        #region QP01 Members
        QP01View _QP01View = new QP01View();
        QP01Dialog _QP01Dialog = new QP01Dialog();
        QP01PlanHistory _QP01History = new QP01PlanHistory();

        DOCPlanTimeJobBiz biz = new DOCPlanTimeJobBiz();
        QCReporting report = new QCReporting();

        enum QP01Step { ShowView, ShowHistory }

        private QP01Step Step
        {
            set
            {
                if (value == QP01Step.ShowView)
                {
                    btSearch.Visible = true;
                    btAddSchedule.Visible = true;
                    btDelete.Visible = true;
                    btShowHistory.Visible = true;                 
                    btReturn.Visible = false;
                    btRefresh.Visible = false;
                    btStart.Visible = false;
                    btStop.Visible = false;
                    bar1.Refresh();
                    Workspace.Show(_QP01View);
                }
                if (value == QP01Step.ShowHistory)
                {
                    btSearch.Visible = false;
                    btAddSchedule.Visible = false;
                    btDelete.Visible = false;
                    btShowHistory.Visible = false;
                    btReturn.Visible = true;
                    btRefresh.Visible = true;
                    btStart.Visible = false ;
                    btStop.Visible = false;
                    bar1.Refresh();
                    Workspace.Show(_QP01History);
                }
            }
        }
        #endregion

        #region QP01 Event
        private void RegisterCommand()
        {
            btSearch.Click += btSearch_Click;
            btAddSchedule.Click += btAddSchedule_Click;
            btShowHistory.Click += btShowHistory_Click;
            btDelete.Click += btDelete_Click;
            btReturn.Click += btReturn_Click;
            btStart.Click += btStartStop_Click;
            btStop.Click += btStartStop_Click;
            _QP01View.eventgridView1_Click += _QP01VieweventgridView1_Click;         
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void btAddSchedule_Click(object sender, EventArgs e)
        {
            AddSchedule();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DeletePlanJob();
        }     

        private void _QP01VieweventgridView1_Click(object sender, EventArgs e)
        {
            btStop.Visible = (bool)sender;
            btStart.Visible = !((bool)sender);
            bar1.Refresh();
        }
        
        private void btShowHistory_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void btReturn_Click(object sender, EventArgs e)
        {
            Step = QP01Step.ShowView;
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void btStartStop_Click(object sender, EventArgs e)
        {
            StartStopSchedule();
        }
        #endregion
    }

    public enum Type { Daily, Monthly }
    //public enum FreqStep {  RangeTime }
}
