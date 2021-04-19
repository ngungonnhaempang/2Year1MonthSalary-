using System;
using LIMS.BLL;
using MIS.Utility;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class GD01 : Infrastructure.BaseForm
    {
        public GD01()
        {
            InitializeComponent();

            #region language
            CultureLanuage.ApplyResourcesFrom(this, "GD01", this.Name);
            #endregion

            Init_Load();
            btSearch.Click += btSearch_Click;
        }

        private void Init_Load()
        {
            _GD01View.dtListLabName = report.GetQCReport("QCW1_QueryLabName", new[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];
            _GD01View.dtListTypeName = report.GetQCReport("QCW1_QueryTypeName", new[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];
            WorkSpace.Show(_GD01View);
        }       

        private GD01View _GD01View = new GD01View();        
        private GradeBiz biz = new GradeBiz();
        private QCReporting report = new QCReporting();

        private void btSearch_Click(object sender, EventArgs e)
        {
            _GD01View.QueryGrade();
        }
    }
}