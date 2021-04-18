using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace FEPVWebApiOwinHost.Models.Gate
{
    [Table("GuestItem")]
    public class GuestItem
    {
        /// <summary>
        /// 单据号
        /// </summary>
        [Required, Key, Column(Order = 0)]
        public Guid ID { get; set; }

        /// <summary>
        /// 访客姓名
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public byte[] Image { get; set; }

        ///// <summary>
        /// 感应卡号
        /// </summary>
        public string CardNO { get; set; }

        /// <summary>
        /// 访客进入厂单据号
        /// </summary>
        public string VoucherID { get; set; }
    }
}
