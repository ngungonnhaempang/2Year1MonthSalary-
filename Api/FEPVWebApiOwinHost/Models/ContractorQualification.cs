using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEPVWebApiOwinHost.Models
{
    /// <summary>
    /// 承揽商资质
    /// </summary>
    [Table("ContractorQualification")]
    public class ContractorQualification
    {
        /// <summary>
        /// 承揽商GUID
        /// </summary>
        [Required, StringLength(50), Key, Column(Order = 0)]
        public string ContractorID { get; set; }

        /// <summary>
        /// 承揽商名称
        /// </summary>
        [Required]
        public string ContractorName { get; set; }

        /// <summary>
        /// 承揽商类型 kind
        /// </summary>
        public string CType { get; set; }


        /// <summary>
        /// 备案号
        /// </summary>
        public string Rcode { get; set; }
        /// <summary>
        /// 长期 还是短期
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public int isvalid { get; set; }

     

      
        public string DepartmentID { get; set; }
        public string Remark { get; set; }


        public string UserID { get; set; }

        public DateTime Stamp { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? AccDate { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string Status { get; set; }

         

        public string InvalidRemark { get;set; }

        /// <summary>
        /// Mới thêm vô nhe
        /// </summary>
        public string Region { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string   ContractorByEmloyee { get; set; }
        public string    InternalNumber { get; set; }
        public string    Email { get; set; }

        public DateTime? StartdateCancel { get; set; }
        public DateTime? EnddateCancel { get; set; }
        public string ContractorFile { get; set; }

        public string EmailContractor { get; set; }

        public string EmailSafetyContractor { get; set; }

        public virtual List<Contractor> Contractors { get; set; }
    }

    
}

