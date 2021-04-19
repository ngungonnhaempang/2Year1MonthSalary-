using LIMS.Model;
using MIS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using BasicLanuage;


namespace LIMS.Views
{
    public partial class QE31
    {
        private void SearchPlan()
        {
            _QE31View.VoucherIDSource = report.GetQCReport("QE31_GetPlansByVoucherID",
               new string[] { "VoucherID", "DOCType", "B", "E", "Lang" },
               _QE31View.Values4Plan).Tables[0];

        }

        private void EditMaterial()
        {
            _QE31Edit.Clear();
            _QE31Edit.Paras = new string[]
            {
                _QE31View.VoucherID,
                _QE31View.SampleName,
                _QE31View.LOT_NO,
            };
            _QE31Edit.MaterialList = report.GetQCReport("QE31_GetAnotherMaterials",
                new string[] { "LOT_NO" },
                new object[] { _QE31View.LOT_NO }).Tables[0];
            _QE31Edit.ShowDialog();
            SearchPlan();
        }

        private void DSCFileUpload()
        {
        }

        private void RecFileUpload()
        {
            if (string.IsNullOrEmpty(_QE31View.VoucherID))
            {
                WriteTips(5, "No VoucherID is selected!");
                return;
            }
            string VoucherID = _QE31View.VoucherID;
            
            //if (VoucherID.Substring(0, 1) != "S" && VoucherID.Substring(0, 1) != "P")
            //{
                
            //    WriteTips(5, "Only upload routine files that has VoucherID start with 'S' OR  'P'!");
            //    return;
            //}
            
            if (string.IsNullOrEmpty(_QE31View.PropertyName))
            {
                WriteTips(5, "No PropertyName is selected!");
                return;
            }
            string PropertyName = _QE31View.PropertyName;
            DialogResult dialog = MessageBox.Show("Do you want to Upload document for VoucherID :" + VoucherID + " with PropertyName of " + PropertyName,"Confirm" , MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) //(Infrastructure.ConfirmBox.Show("Confirm", "Do you want to Upload DCS document for VoucherID :"+ VoucherID))

            //if (Infrastructure.ConfirmBox.Show("Confirm", "Do you want to Upload Receive document for VoucherID :" + VoucherID+ " with PropertyName of "+ PropertyName ))
            {
                string fileUrl = "";
                OpenFileDialog openFileDialog = new OpenFileDialog()
                {
                    Filter = @"PDF File(*.pdf)|*.pdf;)
                                                     |DOC File(*.doc;*.docx;*.txt)|*.doc;*.docx;*.txt;)
                                                     |XLS File(*.xls;*.xlsx;*.csv)|*.xls;*.xlsx;*.csv;)
                                                     |Images File(*.jpg;*.jpeg;*.bmp;*png)|*.jpg;*.jpeg;*.bmp;*png;)"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileUrl = openFileDialog.FileName;

                    Stream fileStream = File.Open(fileUrl, FileMode.Open);
                    fileStream.Position = 0;
                    byte[] Content = new byte[((int)fileStream.Length) + 1];
                    Console.WriteLine("Byte");
                    Console.WriteLine(Content.ToString());
                    fileStream.Read(Content, 0, Content.Length);

                    int pos = fileUrl.LastIndexOf(".") + 1;
                    string fileType = fileUrl.Substring(pos, fileUrl.Length - pos);
                    string filePathServer = "";

                    if (doc.UpLoadFile(VoucherID, Content, fileType, VoucherID.Substring(0, 1), PropertyName, out filePathServer))
                    {
                        filePathServer = filePathServer.Split('\\')[filePathServer.Split('\\').Length - 1];
                        WriteTips(5, "Uploaded!", true);
                        _QE31View.FillProfile();
                    }
                    fileStream.Close();
                }
            }
            _QE31View.FillProfile();
        }
		/// <summary>
		/// FAST UPLOAD COLOR TEST RESULT (t.version1)
		/// </summary>
		private void UploadResult()
		{
			_QE31CU.Clear();
			_QE31CU.MaterialList = _QE31CU.MaterialListPrepare();
			_QE31CU.ShowDialog();

			var data = _QE31CU.Paras;
			if (data.Length > 0 && _QE31CU.rValue)
			{
				if (string.IsNullOrEmpty(_QE31View.VoucherID))
				{
					WriteTips(5, "No VoucherID selected!");
					return;
    }
				_QE31View.UpdateValue("L", data[1].ToString());
				_QE31View.UpdateValue("a", data[2].ToString());
				_QE31View.UpdateValue("b", data[3].ToString());

}

		}

		private void DeleteFileUpdate()
		{
            if (string.IsNullOrEmpty(_QE31View.VoucherID))
            {
                WriteTips(5, "No VoucherID is selected!");
                return;
            }
            string VoucherID = _QE31View.VoucherID;

            //if (VoucherID.Substring(0, 1) != "S" && VoucherID.Substring(0, 1) != "P")
            //{

            //    WriteTips(5, "Only delete routine files that has VoucherID start with 'S' OR 'P'!");
            //    return;
            //}

            if (string.IsNullOrEmpty(_QE31View.PropertyName))
            {
                WriteTips(5, "No PropertyName is selected!");
                return;
            }
            string PropertyName = _QE31View.PropertyName;
            DialogResult dialog = MessageBox.Show("Do you want to Delete document for VoucherID :" + VoucherID + " with PropertyName of " + PropertyName, "Confirm", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes) //(Infrastructure.ConfirmBox.Show("Confirm", "Do you want to Upload DCS document for VoucherID :"+ VoucherID))
            //if (Infrastructure.ConfirmBox.Show("Confirm", "Do you want to Delete File Uploaded for VoucherID :" + VoucherID))
			{
                bool result = doc.DeleteFile(VoucherID, PropertyName);
				if (result){
                    WriteTips(5, VoucherID+ "File is removed!", true);
                    _QE31View.FillProfile();
                }
					
				else
					WriteTips(5, PropertyName+"doen't exist any file!",false);
			}
		}
	}
}
