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
    public class ProfileBiz
    {
        private readonly IProfile proxy = ServiceFactory.Create<IProfile>();

        public bool ProfileInRange(string voucherID, string sampleName, string lot_no, string perpertyName, decimal result)
        {
            return proxy.ProfileInRange(voucherID, sampleName, lot_no, perpertyName, result);
        }



        public Profile SelectProfile(string voucherID, string propertyName)
        {
            return proxy.SelectProfile(voucherID, propertyName);
        }

     

        /// <summary>
        /// 结果录入
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="msg"></param>
        /// <returns>返回错误信息</returns>
        public string RecodeResult(Profile profile)
        {
            return proxy.RecodeResult(profile);
        }

     

    }
}