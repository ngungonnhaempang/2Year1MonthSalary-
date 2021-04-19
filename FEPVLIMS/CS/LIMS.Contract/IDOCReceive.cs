using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    /// <summary>
    /// 进料检验建档

    /// </summary>
    public interface IDOCReceive
    {

        DataTable GetReceive(string voucherID,string tableName);



        /// <summary>
        /// 根据不同方式，创建原料检验批
        /// </summary>
        /// <param name="receive"></param>
        /// <param name="general"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool InsertReceive(DOCReceive receive, ReceiveGeneral general, out string msg);

        /// <summary>
        /// 删除原辅料的检验批 单 ,状态为1
        /// </summary>
        /// <param name="VoucherID"></param>
        /// <param name="Reason"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteReceive(string VoucherID, string Reason, out string msg);

        /// <summary>
        /// 根据现在的申请单接收 并创建 原辅料的检验批
        /// </summary>
        /// <param name="wrgs"></param>
        /// <param name="general"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateWRGGProfile(ReceiveMIGODraf[] wrgs, ReceiveGeneral general, out string msg);

        /// <summary>
        /// 内销交货单创建 申请
        /// </summary>
        /// <param name="wrgg"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CreateMIGO(ReceiveWRGG wrgg, out string msg);


        /// <summary>
        /// 删除内销交货单创建 申请
        /// </summary>
        /// <param name="voucherID"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool DeleteMIGO(string voucherID, out string msg);
    }
}
