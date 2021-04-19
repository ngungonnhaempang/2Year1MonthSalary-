using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MIS.Utility;
using LIMS.Model;
using LIMS.BLL;
using DevExpress.XtraEditors.Controls;
using BasicLanuage;
using System.Text;

namespace LIMS.Views
{
    public partial class QP01Dialog : Infrastructure.StyleForm
    {
        public QP01Dialog()
        {
            InitializeComponent();
            RegisterEvent();
            Init_Load();
            InitLookUpEdit();
        }
        public void Init_Load()
        {
            //每天，每月
            cmbOccurs.DataSource = report.GetQCReport("QP01_GetFreqType", new string[] { }, new object[] { }).Tables[0];
            cmbOccurs.DisplayMember = "Spec";
            cmbOccurs.ValueMember = "Type";
            //分钟，小时
            cmbUnitOcccur.DataSource = report.GetQCReport("QP01_GetUnitType", new string[] { }, new object[] { }).Tables[0];
            cmbUnitOcccur.DisplayMember = "Spec";
            cmbUnitOcccur.ValueMember = "Type";
            //初始形态
            ckbEnable.Checked = true;
            //初始值
            StartTime.Time = DateTime.Now.Date;
            EndTime.Time = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            DurationStart.DateTime = DateTime.Now;
            DurationEnd.DateTime = DateTime.Now.AddMonths(1);
            clbProperty.Items.Clear();
            _Type = Type.Daily;
            Workspace.Show(_QP01Dialog_Daily);
        }
        private void InitLookUpEdit()
        {
            SampleLoad = report.GetQCReport("Q_Samples", new string[] { "Query" }, new object[] { "" }).Tables[0];

            LookUpColumnInfoCollection cols = cmbMaterial.Properties.Columns;
            cols.Add(new LookUpColumnInfo("LOT_NO", 0));
            cols.Add(new LookUpColumnInfo("Description_" + MIS.Utility.MyLanguage.Language, 0));
            cmbMaterial.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            cmbMaterial.Properties.SearchMode = SearchMode.AutoComplete;
            cmbMaterial.Properties.AutoSearchColumnIndex = 1;

            cols = cmbSampleName.Properties.Columns;
            cols.Add(new LookUpColumnInfo("SampleName", 0));
            cols.Add(new LookUpColumnInfo("Description_" + MIS.Utility.MyLanguage.Language, 0));
            cmbSampleName.Properties.BestFitMode = BestFitMode.BestFitResizePopup;
            cmbSampleName.Properties.SearchMode = SearchMode.AutoComplete;
            cmbSampleName.Properties.AutoSearchColumnIndex = 1;
        }
        
        private void cmbSampleName_EditValueChanged(object sender, EventArgs e)
        {
            MaterialLoad = report.GetQCReport("Q_Materials", 
                new string[] { "SampleName", "Query" }, new object[] { SampleName, "" }).Tables[0];
        }

        private void cmbMaterial_EditValueChanged(object sender, EventArgs e)
        {
            LineLoad = report.GetQCReport("Q_GetLinesBySampleName",
                new string[] { "SampleName" }, new object[] { SampleName }).Tables[0];
        }

        void cmbLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSetPlanTimeJob = new PlanTimeJob() 
            {
                SampleName = SampleName,
                LOT_NO = Material,
                LINE = Line
            };
        }

        public string SampleName
        {
            get
            {
                return cmbSampleName.EditValue == null ? "" : cmbSampleName.EditValue.ToString();
            }
        }

        public string Material
        {
            get
            {
                return cmbMaterial.EditValue == null ? "" : cmbMaterial.EditValue.ToString();
            }
        }

        public string Line
        {
            get
            {
                return cmbLine.SelectedIndex < 0 ? "" : cmbLine.SelectedValue.ToString();
            }
        }

        public DataTable SampleLoad
        {
            set
            {
                cmbSampleName.Properties.DataSource = value;
                cmbSampleName.Properties.DisplayMember = "Description_" + MyLanguage.Language;
                cmbSampleName.Properties.ValueMember = "SampleName";
            }
        }
        public DataTable MaterialLoad
        {
            set
            {
                cmbMaterial.Properties.DataSource = value;
                cmbMaterial.Properties.DisplayMember = "LOT_NO";
                cmbMaterial.Properties.ValueMember = "LOT_NO";
            }
        }

        public DataTable LineLoad
        {
            set
            {
                cmbLine.DataSource = value;
                cmbLine.DisplayMember = "Line";
                cmbLine.ValueMember = "Line";
            }
        }

        #region QP01Dialog Members
        QCReporting report = new QCReporting();
        QP01Dialog_Daily _QP01Dialog_Daily = new QP01Dialog_Daily();
        QP01Dialog_Month _QP01Dialog_Month = new QP01Dialog_Month();
        #endregion

        string _msg = "";
        /// <summary>
        /// 数据准备
        /// </summary>
        public bool IsReady
        {
            get
            {
                StringBuilder msg = new StringBuilder();
                if (string.IsNullOrEmpty(SampleName))
                    msg.Append("SampleName Empty.");
                if (string.IsNullOrEmpty(Material))
                    msg.Append("|Material Empty");
                if (string.IsNullOrEmpty(Line))
                    msg.Append("|Line Empty");
                if (cmbOccurs.Text.ToString() == "")
                    msg.Append("|Please choose Occurs Type");
                if (clbProperty.CheckedItemsCount < 1)
                    msg.Append("|Please Choose Attribute");
                if (_Type == Type.Daily)
                {
                    if (_QP01Dialog_Daily.Days < 1)
                        msg.Append("|Please Select Days.");
                }
                if (_Type == Type.Monthly)
                {
                    if (_QP01Dialog_Month.TypeMonth == 16)
                    {
                        if (_QP01Dialog_Month.FreqInterval < 1)
                        {
                            msg.Append("|Please Select Day in Month");
                        };
                        if (_QP01Dialog_Month.RecurrenceFactor < 1)
                        {
                            msg.Append("|Please Select Months");
                        };

                    }
                }
                if (StartTime.Time >= EndTime.Time)
                    msg.Append("|EndTime Must Be Higher");
                if (DurationStart.DateTime > DurationEnd.DateTime)
                    msg.Append("|EndDate Must Be Higher");

                _msg = "" + msg;

                if (_msg == "")
                    return true;
                else
                    return false;
            }
        }

        #region Event
        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegisterEvent()
        {
            btSave.Click += btSave_Click;
            btCancel.Click += btCancel_Click;

            cmbSampleName.EditValueChanged += cmbSampleName_EditValueChanged;
            cmbMaterial.EditValueChanged += cmbMaterial_EditValueChanged;
            cmbLine.SelectedIndexChanged += cmbLine_SelectedIndexChanged;
            cmbOccurs.SelectedIndexChanged += cmbOccurs_SelectedIndexChanged;

            rbOnce.CheckedChanged += rbOnce_CheckedChanged;
            rbOccurEvery.CheckedChanged += rbOccurEvery_CheckedChanged;
            rbEndDate.CheckedChanged += rbEndDate_CheckedChanged;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (IsReady)
            {
                _Type = _TempType;
                DialogResult = DialogResult.Yes;
            }
            else
            {
                WriteTips(5, _msg, false);
            }           
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void cmbOccurs_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmbOccurs.Text)
            {
                case "Daily":
                    _Type = Type.Daily;
                    //
                    numOccur.Visible = true;
                    cmbUnitOcccur.Visible = true;
                    EndTime.Visible = true;
                    rbOccurEvery.Visible = true;
                    lblTo.Visible = true;
                    //
                    Workspace.Show(_QP01Dialog_Daily);
                    break;
                case "Monthly":
                    _Type = Type.Monthly;
                    //
                    rbOnce.Checked = true;
                    numOccur.Visible = false;
                    cmbUnitOcccur.Visible = false;
                    EndTime.Visible = false;
                    rbOccurEvery.Visible = false;
                    lblTo.Visible = false;
                    //
                    Workspace.Show(_QP01Dialog_Month);
                    break;
            }
        }
        void rbOnce_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOnce.Checked)
            {
                numOccur.Enabled = false;
                cmbUnitOcccur.Enabled = false;
                EndTime.Enabled = false;
            }
        }
        private void rbOccurEvery_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOccurEvery.Checked)
            {
                numOccur.Enabled = true;
                cmbUnitOcccur.Enabled = true;
                EndTime.Enabled = true;
            }
        }

        private void rbEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEndDate.Checked)
            {
                DurationEnd.Enabled = true;
            }
            else
            {
                DurationEnd.Enabled = false;
            }
        }

        #endregion

        PlanTimeJob _TempPlanTimeJob { get; set; }
        public PlanTimeJob GetSetPlanTimeJob
        {
            get
            {
                _Type = _TempType;

                return new PlanTimeJob
                {
                    SampleName = cmbSampleName.EditValue.ToString(),
                    LOT_NO = cmbMaterial.EditValue.ToString(),
                    LINE = cmbLine.SelectedValue.ToString(),
                    Type = _TempPlanTimeJob.Type,
                    Interval = _TempPlanTimeJob.Interval,
                    DayUnit = _TempPlanTimeJob.DayUnit,
                    DayInterval = _TempPlanTimeJob.DayInterval,
                    RelativeInterval = _TempPlanTimeJob.RelativeInterval,
                    RecurrenceFactor = _TempPlanTimeJob.RecurrenceFactor,
                    StartDate = _TempPlanTimeJob.StartDate,
                    EndDate = _TempPlanTimeJob.EndDate,
                    StartTime = _TempPlanTimeJob.StartTime,
                    EndTime = _TempPlanTimeJob.EndTime,
                    CreateDate = DateTime.Now,
                    AccDate = DateTime.Now,
                    Enabled = ckbEnable.Checked
                };
            }
            set
            {
                DataTable dt = report.GetQCReport("QP01_GetProperty", new string[] { "SampleName", "LOT_NO", "LINE" }, new object[] { value.SampleName, value.LOT_NO, value.LINE }).Tables[0];

                clbProperty.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    var item = new CheckedListBoxItem
                    {
                        Value = row["PropertyName"].ToString(),
                        CheckState = CheckState.Unchecked,
                        Enabled = !((bool)row["IsExist"])
                    };
                    clbProperty.Items.Add(item);
                }
            }
        }

        public List<string> GetAttributeList
        {
            get
            {
                var list = new List<string>();
                foreach (CheckedListBoxItem item in clbProperty.CheckedItems)
                {
                    list.Add(item.Value.ToString());
                }
                return list;
            }
        }

        private Type _TempType { get; set; }
        Type _Type
        {
            set
            {
                _TempType = value;
                _TempPlanTimeJob = new PlanTimeJob();
                if (value == Type.Daily)
                {
                   _TempPlanTimeJob.Type = 4;
                   _TempPlanTimeJob.Interval = _QP01Dialog_Daily.Days;//workspace
                }
                if (value == Type.Monthly)
                {
                    _TempPlanTimeJob.Type = _QP01Dialog_Month.TypeMonth;//workspace 16
                    _TempPlanTimeJob.Interval = _QP01Dialog_Month.RecurrenceFactor;//workspace 1~12
                    _TempPlanTimeJob.RecurrenceFactor = _QP01Dialog_Month.FreqInterval;//workspace 1~31
                    _TempPlanTimeJob.RelativeInterval = _QP01Dialog_Month.RelativeInterval;//workspace 0
                }

                _TempPlanTimeJob.StartDate = DurationStart.DateTime;
                _TempPlanTimeJob.EndDate = rbEndDate.Checked ? DurationEnd.DateTime : DateTime.MaxValue;
                _TempPlanTimeJob.StartTime = (DateTime)StartTime.EditValue;
                _TempPlanTimeJob.EndTime = EndTime.Time;
                _TempPlanTimeJob.DayUnit = rbOnce.Checked ? 1 : int.Parse(cmbUnitOcccur.SelectedValue.ToString());
                _TempPlanTimeJob.DayInterval = (int)numOccur.Value;
            }

            get
            {
                return _TempType;
            }
        }
    }
}
