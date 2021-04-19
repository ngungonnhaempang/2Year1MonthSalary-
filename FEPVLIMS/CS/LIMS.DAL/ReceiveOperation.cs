using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMS.Contract;
using LIMS.Model;

namespace LIMS.DAL
{
    public class ReceiveOperation
    {

        private IReceiveOperation m_ReceiveOperation;
        public ReceiveOperation(IReceiveOperation receiveOperation)
        {
            this.m_ReceiveOperation = receiveOperation;
        
        }
        
        public bool InsertRecieve(DOCReceive recieve, ReceiveGeneral general, out string msg)
        {
            return m_ReceiveOperation.InsertRecieve(recieve, general, out msg);
        }

        public DataTable GetReceive(string voucherID)
        {
            return m_ReceiveOperation.GetRecieve(voucherID);
        }
    }
}
