using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IDOC
    {
        byte[] DocAssistSearch(DateTime B, DateTime E, string par, string userid);

        byte[] GetvProperties(string voucherID);
        /// <summary>
        /// Create by Isaac 2018-11-12
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="lotno_new"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        bool DocUpdateMaterialNo(string voucherID, string lotno_new, string reason);

        bool SaveDocRemark(string voucherID, string remark);

        bool DocConfirm(string voucherID);

        bool DocUnLock(string voucherID);

        bool DocPropertyConfirm(string voucherID, string PropertyName);

        bool DocPropertyUnLock(string voucherID, string PropertyName);

        bool SetDocGrade(string voucherID, string Grade);

        bool ChangeCreateType(string voucherID, string createType);

        bool DocDelete(string voucherID);

        bool UpLoadFile(string voucherID, byte[] fileData, string fileType, string DocType, string PropertyName, out string filePathServer);

		bool DeleteFile(string VoucherID, string PropertyName);
			
    }
}
