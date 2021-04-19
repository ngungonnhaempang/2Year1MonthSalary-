using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Model;

namespace LIMS.DAL
{
    public interface IReceiveOperation
    {

        bool InsertRecieve(DOCReceive recieve, ReceiveGeneral general, out string msg);

        DataTable GetRecieve(string voucherID);

    }
}
