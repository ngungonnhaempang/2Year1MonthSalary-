using MIS.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class AT01 : Infrastructure.BaseForm
    {
        public AT01()
        {
            InitializeComponent();
            RegisterCommand();
            Workspace.Show(_AT01View);

            #region Languages
            try
            {
                CultureLanuage.ApplyResourcesFrom(this, "AT01", "AT01");

                CultureLanuage.ApplyResourcesFrom(_AT01Dialog, "AT01", "AT01Dialog");

                DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(_AT01View, "AT01", "AT01View");

                DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
                if (gridData != null)
                {
                    _AT01View.gridControl1.DataSource = gridData;
                    CultureLanuage.GridResourcesFrom(_AT01View.gridView1, "gridView1", dsgrid);
                    _AT01View.gridView1.BestFitColumns();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }

        AT01Dialog _AT01Dialog = new AT01Dialog();
        AT01View _AT01View = new AT01View();

        void RegisterCommand()
        {
            btSearch.Click += btSearch_Click;
            btAdd.Click += btAdd_Click;
            btDelete.Click += btDelete_Click;
            btReturn.Click += btReturn_Click;
            _AT01View.eventShowDetails += _AT01View_eventShowDetails;
            _AT01View.eventcmbSample_SelectedIndexChanged += _AT01View_eventcmbSample_SelectedIndexChanged;
        }

        void _AT01View_eventcmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            Query();
        }

        void btSearch_Click(object sender, EventArgs e)
        {
            Query();
        }

        void btAdd_Click(object sender, EventArgs e)
        {
            AddSample();
        }

        void btDelete_Click(object sender, EventArgs e)
        {
            DelectSample();
        }

        private void btReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
    public class EgateArgs : EventArgs
    {
        public Dictionary<string, object> EgateDictionary { get; set; }
    }
}
