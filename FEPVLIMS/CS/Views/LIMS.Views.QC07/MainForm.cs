using BasicLanuage;
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
using MIS.Utility;
using System.Collections;
using LIMS.Model;

namespace LIMS.Views.QC07
{
    public partial class MainForm : Infrastructure.BaseForm
    {
        SerchPlanView _SerchPlanView = new SerchPlanView();

        DOCReceiveBiz biz = new DOCReceiveBiz();
        QCReporting rep = new QCReporting();
        RawFrm rawFrm = new RawFrm();

        public string _RoleName
        {
            set;
            get;
        }
        public MainForm()
        {
            InitializeComponent();

            #region language
            CultureLanuage.ApplyResourcesFrom(this, "QC07", this.Name);
            #endregion


            butReceive.Click += butReceive_Click;
            this.Load += new System.EventHandler(this.Main_Load);
            btQuery.Click += btQuery_Click;
            this.butReturn.Click += butReturn_Click;
            _SerchPlanView.eventcmbTypeName_SelectedIndexChanged += _SerchPlanView_eventcmbTypeName_SelectedIndexChanged;
            DataTable DtTypeName = rep.GetQCReport("QCW1_QueryTypeName", new string[] { "Lang" }, new object[] { MyLanguage.Language }).Tables[0];
            _SerchPlanView.TypeNameLoad = DtTypeName;
            dWorkSpace.Show(_SerchPlanView);

        }
        private void Main_Load(object sender, EventArgs e)
        {
            if (_RoleName == "QC")
            {
                butReceive.Visible = true;
                butReturn.Visible = true;
                _SerchPlanView.ALL = false;
                _SerchPlanView.MySelf = false;
            }
            else
            {
                butReceive.Visible = false;
                butReturn.Visible = false;
                _SerchPlanView.ALL = true;
                _SerchPlanView.MySelf = true;
            }
        }
        void _SerchPlanView_eventcmbTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _SerchPlanView.SampleNameLoad = rep.GetQCReport("QCW1_QuerySample",
                         new string[] { "LabID", "TypeID" },
                         new object[] { "1", _SerchPlanView.TypeID }).Tables[0];
            }
            catch (Exception ex)
            {
                WriteTips(5, ex.Message);
            }
        }
        public string GetGradeVersion(string lot_no, string sampleName)
        {
            string _GradeVersion = "";
            string msg = "";
            DataTable dt = rep.Reporting("MMJY_GetVersionID", new string[] { "LotNo", "SampleName" },
                new object[] { lot_no, sampleName }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                msg = dt.Rows[0]["MSG"].ToString();
                Console.WriteLine(msg);
                if (string.IsNullOrEmpty(msg))
                {
                    _GradeVersion = dt.Rows[0]["VersionID"].ToString();
                }
                else
                {
                    WriteTips(5, msg, false);
                }


            }
            return _GradeVersion;
        }
        void butReturn_Click(object sender, EventArgs e)
        {

            string[] VoucherIDs = _SerchPlanView.VoucherIDs;
            if (VoucherIDs.Length <= 0)
                return;

            if (DialogResult.OK == MessageBox.Show("WILL DELETE,CONTINUE?", "ITEM DELETE", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2))
            {
                string msg = "";
                int count = 0;
                foreach (string item in VoucherIDs)
                {
                    DOCBiz _docBiz = new DOCBiz();
                    if (!_docBiz.DocDelete(item))
                        msg += "[" + item + "]";
                    else
                        count++;
                }
                if (count != VoucherIDs.Length)
                    MessageBox.Show("ITEM " + msg + "DELETE FAILED!");

                Query();

            }
        }
        void butReceive_Click(object sender, EventArgs e)
        {

            string[] strs = _SerchPlanView.dtSelectBarCodes4Query;
            rawFrm.SampleDes = _SerchPlanView.SampleDes;
            rawFrm.SampleName = _SerchPlanView.RSampleName;
            rawFrm.LotNO = _SerchPlanView.RLotNO;
            rawFrm.GradeVersion = GetGradeVersion(rawFrm.LotNO, rawFrm.SampleName);

            #region Recive by Isaac 2018-11-29
            //Create by Isaac EBELN, EBELP, Quantity 2018-11-29
            rawFrm.Quantity = _SerchPlanView.Quantity;
            rawFrm.EBELN = _SerchPlanView.EBELN;
            rawFrm.EBELP = _SerchPlanView.EBELP;
            rawFrm.GR_NO = _SerchPlanView.GR_NO;
            rawFrm.ReceiveNum = _SerchPlanView.ReceiveNum;
            rawFrm.GR_Unit = _SerchPlanView.GR_Unit;
			rawFrm.Vendor = _SerchPlanView.Vendor;			
			#endregion
			rawFrm.ShowDialog();
            if (rawFrm.RValue)
            {
                ReceiveGeneral general = new ReceiveGeneral();
                general.SampleName = rawFrm.SampleName;
                general.LOT_NO = rawFrm.LotNO;
                #region Create By Isaac
                //Creat by IS 2018-11-29

                general.SampleQuantity = rawFrm.ReceiveNum;
                general.GradeVersion = rawFrm.GradeVersion;
                general.GR_NO = rawFrm.GR_NO;
                general.SheetDate = rawFrm.SheetDate;
                general.GR_Unit = rawFrm.GR_Unit;
				general.Vendor = rawFrm.Vendor;
				general.Quatity = rawFrm.Quantity;
                general.EBELN = rawFrm.EBELN;
                general.EBELP = rawFrm.EBELP;
                general.DateOfSample = rawFrm.DateOfSample;
                #endregion 
                general.State = "1";

                general.Grade = "";

                ReceiveMIGODraf[] drfs = new ReceiveMIGODraf[strs.Length];
                int i = 0;
                foreach (string str in strs)
                {

                    ReceiveMIGODraf draf = new ReceiveMIGODraf();
                    draf.VoucherID = str;

                    drfs[i] = draf;
                    i++;

                }
                string msg = string.Empty;
                if (!biz.CreateWRGGProfile(drfs, general, out msg))
                {
                    WriteTips(8, msg, false);

                }
                Query();
                rawFrm.ResetForm();

            }
        }
        void btQuery_Click(object sender, EventArgs e)
        {
            Query();
        }
        void Query()
        {

            var dt =  rep.Reporting("QC07_QueryWRGG", _SerchPlanView.Parameters, _SerchPlanView.Values).Tables[0];
            _SerchPlanView.dtSah = dt;
        }



    }
}
