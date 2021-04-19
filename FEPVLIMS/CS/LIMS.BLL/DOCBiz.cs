using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Contract;
using Shawoo.Core;
using LIMS.Model;
using MIS.Utility;

namespace LIMS.Service
{
    public class DOCBiz
    {
        private readonly IDOC proxy = ServiceFactory.Create<IDOC>();


        public DataTable DocAssistSearch(DateTime B, DateTime E, string par)
        {
          
               return DataFormatter.RetrieveDataSetDecompress(proxy.DocAssistSearch(B, E, par,DB.User)).Tables[0];
          
        }

        public DataTable GetvProperties(string voucherID)
        {
           byte[] b =  proxy.GetvProperties(voucherID);

           return DataFormatter.RetrieveDataSetDecompress(b).Tables[0];
        }

        public bool SaveDocRemark(string voucherID, string remark)
        {
          
              return proxy.SaveDocRemark(voucherID, remark);
          
        }

        public bool DocConfirm(string voucherID)
        {
           
             return proxy.DocConfirm(voucherID);
          
        }
        /// <summary>
        /// Create by Isaac 2018-11-12
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="lotno_new"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public bool DocUpdateMaterialNo(string voucherID, string lotno_new, string reason)
        {
            return proxy.DocUpdateMaterialNo(voucherID, lotno_new, reason);
        }
        public bool DocUnLock(string voucherID)
        {
            return proxy.DocUnLock(voucherID);
            
        }

        public bool DocPropertyConfirm(string voucherID, string PropertyName)
        {
          
               return proxy.DocPropertyConfirm(voucherID, PropertyName);
           
        }

        public bool DocPropertyUnLock(string voucherID, string PropertyName)
        {
           
             return proxy.DocPropertyUnLock(voucherID, PropertyName);
          
        }

        public bool SetDocGrade(string voucherID, string Grade)
        {
           
               return proxy.SetDocGrade(voucherID, Grade);
           
        }

        public bool ChangeCreateType(string voucherID, string createType)
        {
           
               return proxy.ChangeCreateType(voucherID, createType);
           
        }

        public bool DocDelete(string voucherID)
        {
           
              return proxy.DocDelete(voucherID);
         
        }

        public bool UpLoadFile(string voucherID, byte[] fileData, string fileType, string DocType, string PropertyName, out string filePathServer)
        {
            return proxy.UpLoadFile(voucherID, fileData, fileType, DocType,PropertyName, out filePathServer);
        }

		public bool DeleteFile(string voucherID, string PropertyName)
		{

            return proxy.DeleteFile(voucherID, PropertyName);

		}


	}
}