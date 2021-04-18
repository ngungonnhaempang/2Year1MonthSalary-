using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEPVWebApiOwinHost.Models
{
    public class ContractorStaticsDto
    {

        public string DepartmentID { set; get; }

        public string Empolyer { set; get; }

        public string DateForm { set; get; }


        public int PeopleCount { set; get; }


        public int FactoryCount { set; get; }

        public int InCount { set; get; }


        public int OutCount { set; get; }

        public int InvialCount { set; get; }
    }



    public class CardLogs
    {

        public int ID { get; set; }


        public string MAC { get; set; }


        public string ContractorID { get; set; }


        public string ContractorName { get; set; }


        public string Company { get; set; }


        public int Operate { get; set; }



        public DateTime? OperateTime { get; set; }


        public string CardType { get; set; }

        public string SiteName { get; set; }

        public string Status { get; set; }


        public string Remark { get; set; }

        public string EmployerId { get; set; }
    }

   
}
