using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MIS.Utility;
using LIMS.BLL;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class SP01 : Infrastructure.BaseForm
    {
        public SP01()
        {
            InitializeComponent();
            Init_Load();

            RegisterCommand();

            #region Languages
            CultureLanuage.ApplyResourcesFrom(this, "SP01", "SP01");

            CultureLanuage.ApplyResourcesFrom(_QCDialog, "SP01", "QCDialog");

            DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(_SP01View, "SP01", "SP01View");
            DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
            if (gridData != null)
            {
                _SP01View.gridControl1.DataSource = gridData;
                CultureLanuage.GridResourcesFrom(_SP01View.gridView1, "gridView1", dsgrid);
                _SP01View.gridView1.BestFitColumns();
            }
            #endregion
        }

        #region SP01 Members
        QCDialog _QCDialog = new QCDialog();

        SP01View _SP01View = new SP01View();

        QCReporting report = new QCReporting();

        SampleBiz biz = new SampleBiz();

        string _Language { get { return MIS.Utility.MyLanguage.Language; } }
        #endregion

        #region Event
        void RegisterCommand()
        {
            btSearch.Click += eventQuery;
            btAdd.Click += btAdd_Click;
            btDelete.Click += btDelete_Click;
            btReturn.Click += btReturn_Click;

            _SP01View.eventcmbLabName_SelectedIndexChanged += _SP01View_eventcmbLabName_SelectedIndexChanged;
            _SP01View.eventcmbTypeName_SelectedIndexChanged += _SP01View_eventcmbTypeName_SelectedIndexChanged;
            _SP01View.eventcmbSampleName_SelectedIndexChanged += eventQuery;
            _SP01View.EditEvent += _SP01ViewEditEvent;
        }

       void _SP01View_eventcmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSampleList();
        }

        void _SP01View_eventcmbLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSampleList();
        }

        void _SP01ViewEditEvent(object sender, EventArgs e)
        {
            EditSample(_SP01View.GetSample);
        }

        private void eventQuery(object sender, EventArgs e)
        {
            Query();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            AddSample();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            DeleteSample(_SP01View._SelectedSampleName);
        }

        private void btReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
