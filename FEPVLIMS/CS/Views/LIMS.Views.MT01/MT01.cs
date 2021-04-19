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
    public partial class MT01 : Infrastructure.BaseForm
    {
        public MT01()
        {
            InitializeComponent();           
            RegisterCommand();
            Workspace.Show(_MT01View);
            // why do you rewrite Infrastructure class  
            #region Languages
            CultureLanuage.ApplyResourcesFrom(this, "MT01", "MT01");
            CultureLanuage.ApplyResourcesFrom(_MT01Dialog, "MT01", "MT01Dialog");

            DataSet dsgrid = CultureLanuage.ApplyResourcesFrom(_MT01View, "MT01", "MT01View");
            DataTable gridData = CultureLanuage.GridHeader(dsgrid, "gridView1");
            if (gridData != null)
            {
                _MT01View.gridControl1.DataSource = gridData;
                CultureLanuage.GridResourcesFrom(_MT01View.gridView1, "gridView1", dsgrid);
                _MT01View.gridView1.BestFitColumns();
            }
            #endregion
        }

        MT01Dialog _MT01Dialog = new MT01Dialog();
        MT01View _MT01View = new MT01View();

        void RegisterCommand()
        {
            btSearch.Click += btSearch_Click;
            btAdd.Click += btAdd_Click;
            btDelete.Click += btDelete_Click;
            btReturn.Click += btReturn_Click;

            _MT01View.eventShowDetails += _MT01View_eventShowDetails;
            _MT01View.eventClickDetails+= _MT01View_eventClickDetails;
            _MT01View.eventcmbSample_SelectedIndexChanged += btSearch_Click;
        }


        void btSearch_Click(object sender, EventArgs e)
        {
            Query();
        }

        void btAdd_Click(object sender, EventArgs e)
        {
            AddMaterial();
        }

        void btDelete_Click(object sender, EventArgs e)
        {
            DeleteMaterial();
        }

        private void btReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        string MainMsg
        {
            set
            {
                txtMsg.Text = value;
                txtMsg.Refresh();
            }
        }

        
    }
    public class EgateArgs : EventArgs
    {
        public Dictionary<string, object> EgateDictionary { get; set; }
    }
      
}
