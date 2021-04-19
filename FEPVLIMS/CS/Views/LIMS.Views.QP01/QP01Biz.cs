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
using MIS.Utility;

namespace LIMS.Views
{
    public partial class QP01
    {
        /// <summary>
        /// Query
        /// </summary>
        private void Query()
        {
            _QP01View.QueryPlanJob();
        }

        private void AddSchedule()
        {
            _QP01Dialog.Init_Load();   
            if (_QP01Dialog.ShowDialog() == DialogResult.Yes)
            {
                PlanTimeJob planadd = _QP01Dialog.GetSetPlanTimeJob;

                if (Infrastructure.ConfirmBox.Show("Confirm", "Do you want to Create Plan: " ))
                {
                    if (biz.Create(planadd, _QP01Dialog.GetAttributeList))
                    {
                        WriteTips(5, "Save Success!", true);
                        Query();
                    }
                    else
                        WriteTips(5, "Save Fail!", false);
                }
            }
        }

        private void DeletePlanJob()
        {
            if (Infrastructure.ConfirmBox.Show("Confirm", "Do you want  to delete ?"))
            {
                if (biz.Delete(_QP01View.planTemp))
                {
                    WriteTips(5, "Delete successful!", true);
                    Query();
                }
                else
                {
                    WriteTips(5, "Delete Fail!", false);
                }
            }
        }

        private void ShowHistory()
        {
            _QP01History.LogsSource(_QP01View.planTemp);
            Step = QP01Step.ShowHistory;
        }

        private void StartStopSchedule()
        {
            if (biz.StartStopSchedule(_QP01View.planTemp))
            {
                WriteTips(5, _QP01View.planTemp.Enabled ? "Stop Successful" : "Start Successful", true);
                Query();
                btStop.Visible = false;
                btStart.Visible = false;
                bar1.Refresh();
            }
            else
                WriteTips(5, "Start/Stop fail!", false);            
        }
    }
}
