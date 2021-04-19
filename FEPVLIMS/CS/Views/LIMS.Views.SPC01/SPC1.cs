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
using BasicLanuage;

namespace LIMS.Views
{
    public partial class SPC1 : Infrastructure.BaseForm
    {
        SPC1Dialog _SPC1Dialog = new SPC1Dialog();
        SPC1View _SPC1View = new SPC1View();
        public SPC1()
        {
            InitializeComponent();
            Init_Load();
            RegisterEvent();

            #region Languages
            CultureLanuage.ApplyResourcesFrom(this, "SPC1", "SPC1");  
            #endregion
        }

        #region SP01 Members
        
        

        string _Language { get { return MyLanguage.Language; } }

        BackgroundWorker bw = new BackgroundWorker();
        #endregion

        #region Events
        void RegisterEvent()
        {
            btSearch.Click += btSearch_Click;
            btDelete.Click += btDelete_Click;
            btAdd.Click += btAdd_Click;
            btIn.Click += btTolead_Click;
            btReturn.Click += btReturn_Click;
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            _SPC1View.eventShowDetails += _SPC1View_eventShowDetails;
            _SPC1View.eventcmbLabName_SelectedIndexChanged += _SPC1View_eventcmbLabName_SelectedIndexChanged;
            _SPC1View.eventcmbSample_SelectedIndexChanged +=_SPC1View_eventcmbSample_SelectedIndexChanged;
            _SPC1View.eventcmbTypeName_SelectedIndexChanged += _SPC1View_eventcmbTypeName_SelectedIndexChanged;
        }

        void _SPC1View_eventcmbLabName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSampleList();
        }

        void _SPC1View_eventcmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSampleList();
        }

        void _SPC1View_eventcmbSample_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLottoNoAndLineNo();
            Query();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            AddCL();
        }

        void btDelete_Click(object sender, EventArgs e)
        {
            DeleteCL(_SPC1View.SelectedChart);
        }

        private void btTolead_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void btReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
    public class EgateArgs : EventArgs
    {
        public Dictionary<string, object> EgateDictionary { get; set; }
    }
}
