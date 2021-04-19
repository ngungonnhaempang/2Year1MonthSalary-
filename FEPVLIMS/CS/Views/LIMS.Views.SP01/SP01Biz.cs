using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using LIMS.Model;
using MIS.Utility;
using LIMS.BLL;

namespace LIMS.Views
{
    public partial class SP01
    {
        DataTable DtLabName = new DataTable();
        DataTable DtTypeName = new DataTable();
        private void Init_Load()
        {

            DtLabName = report.GetQCReport("QCW1_QueryLabName", new string[] { "Lang" }, new object[] { _Language }).Tables[0];
            _SP01View.LabNameLoad = DtLabName;
            DtTypeName = report.GetQCReport("QCW1_QueryTypeName", new string[] { "Lang" }, new object[] { _Language }).Tables[0];
            _QCDialog._cmLineData = report.GetQCReport("SP01_GetLine", new string[] {  }, new object[] {  }).Tables[0];
            _SP01View.TypeNameLoad = DtTypeName;
            WorkSpace.Show(_SP01View);
        }

        public void GetSampleList()
        {
            try
            {
                _SP01View.SampleNameLoad = report.GetQCReport("QCW1_QuerySample",
                         new string[] { "LabID", "TypeID" },
                         new object[] { _SP01View.LabID, _SP01View.TypeID }).Tables[0];
            }
            catch (Exception ex)
            {
                WriteTips(5, ex.Message);
            }
        }

        public void Query()
        {
            try
            {
                _SP01View.tableMList = report.GetQCReport("QCW1_QuerySamplesList",
                         new string[] { "SampleName", "LabID", "TypeID", "Lang" },
                         new object[] { _SP01View.SampleName, _SP01View.LabID, _SP01View.TypeID, _Language }).Tables[0];
            }
            catch (Exception ex)
            {
                WriteTips(5, ex.Message);
            }
        }

        public void AddSample()
        {
            _QCDialog._STEP = STEP.Add;
            if (string.IsNullOrEmpty(_SP01View.LabName) || string.IsNullOrEmpty(_SP01View.TypeName))
            {
                WriteTips(5, "Please pick Labname and TypeName!");
                return;
            }
            _QCDialog.TypeID = _SP01View.TypeID;
            bool isAb = false;
            if (_SP01View.TypeID == 2)
            {
                isAb = true;
            }
            _QCDialog.SetLine(isAb);
            _QCDialog.LabName = _SP01View.LabName;
            _QCDialog.TypeName = _SP01View.TypeName;
            _QCDialog.Sample = new QCSample { LabID = _SP01View.LabID,  TypeID = _SP01View.TypeID};
            _QCDialog.ShowDialog();
            if (_QCDialog.RValue)
            {
                string msg = _QCDialog.ReadyWork;
                string _outmsg = "";
                if (string.IsNullOrEmpty(msg))
                {
                    if (!biz.CreateSample(_QCDialog.Sample))
                        WriteTips(5, _outmsg, false);
                    else
                    {
                        WriteTips(1, "Add Sample: {0} Successful!");
                        GetSampleList();
                        Query();
                    }
                }
                else
                {
                    WriteTips(5, msg);
                }
            }
        }

        public void EditSample(QCSample Sample)
        {
            _QCDialog._STEP = STEP.Edit;
            _QCDialog.Sample = Sample;
            bool isAb = false;
            if (_SP01View.TypeID == 2)
            {
                isAb = true;
            }
            Console.WriteLine(_SP01View.TypeID);
            _QCDialog.SetLine(isAb);
            _QCDialog.LabName = _SP01View.LabName;
             _QCDialog.TypeName = _SP01View.TypeName;
            _QCDialog.ShowDialog();
            if (_QCDialog.RValue)
            {
                string msg = _QCDialog.ReadyWork;
                if (string.IsNullOrEmpty(msg))
                {
                    if (!biz.UpdateSample(_QCDialog.Sample))
                        WriteTips(5, "Edit Sample Failed!");
                    else
                    {
                        WriteTips(5,"Edit Sample Successful!"+ Sample.SampleName);
                        Query();
                    }
                }
                else
                {
                    WriteTips(5, msg, false);
                }
            }
        }

        public void DeleteSample(string SampleName)
        {
            string outmsg = "";
            if (!string.IsNullOrEmpty(SampleName))
            {
                if (Infrastructure.ConfirmBox.Show( "Confirm Delete", string.Format("Confirm to delete this Sample: {0}?", SampleName)))
                {
                    string msg = biz.DeleteSample(SampleName);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        WriteTips(5, "Sample has been deleted!" + SampleName);
                        GetSampleList();
                        Query();
                    }
                    else
                    {
                        WriteTips(5, outmsg, true);
                    }
                }
            }
            else
            {
                WriteTips(5,  "No Sample Selected!");
            }
        }

        public string ReadyWork()
        {
            string msg = "";
            if (string.IsNullOrEmpty(_SP01View.SampleName))
                msg += "The Sample Name is empty/";
            if (string.IsNullOrEmpty(_SP01View.LabName))
                msg += "Labname is empty/";
            if (string.IsNullOrEmpty(_SP01View.TypeName))
                msg += "Type is empty/";
            return msg;
        }
    }
}