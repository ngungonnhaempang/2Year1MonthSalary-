using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Contract;
using Shawoo.Core;
using LIMS.Model;

namespace LIMS.Service
{
    public class DOCReceiveBiz
    {
        private readonly IDOCReceive proxy = ServiceFactory.Create<IDOCReceive>();


        public DataTable GetReceive(string voucherID, string tableName) 
        {
            return proxy.GetReceive(voucherID, tableName);
        }


        public bool InsertReceive(DOCReceive receive, ReceiveGeneral general, out string msg)
        {

            return proxy.InsertReceive(receive, general, out msg);
        }
        public bool DeleteReceive(string voucherID, string reason, out string msg)
        {
            return proxy.DeleteReceive(voucherID, reason, out msg);
        }
        /// <summary>
        ///  根据现在的申请单接收 并创建 原辅料的检验批
        /// </summary>
        /// <param name="wrgs"></param>
        /// <param name="general"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CreateWRGGProfile(ReceiveMIGODraf[] wrgs, ReceiveGeneral general, out string msg)
        {
            return proxy.CreateWRGGProfile(wrgs, general, out msg);
        }

        /// <summary>
        /// 内销交货单创建 申请
        /// </summary>
        /// <param name="wrgg"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CreateMIGO(ReceiveWRGG wrgg, out string msg)
        {
            return proxy.CreateMIGO(wrgg, out msg);
        }


        /// <summary>
        /// 删除内销交货单创建 申请
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool DeleteMIGO(string voucherID, out string msg)
        {
            return proxy.DeleteMIGO(voucherID, out msg);
        }
       
    }
}