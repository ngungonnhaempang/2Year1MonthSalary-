using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SAP.Middleware.Connector;

namespace LIMS.Service
{
    public class RfcHelp
    {

        public static DataTable ToAdoTable(IRfcTable rfcTable)
        {
            Console.WriteLine("ToAdoTable------");
            DataTable loTable = new DataTable();
            //
            for (int liElement = 0; liElement < rfcTable.ElementCount; liElement++)
            {
                RfcElementMetadata metadata = rfcTable.GetElementMetadata(liElement);
                loTable.Columns.Add(metadata.Name);
            }
            //
            foreach (IRfcStructure row in rfcTable)
            {
                DataRow ldr = loTable.NewRow();
                for (int liElement = 0; liElement < rfcTable.ElementCount; liElement++)
                {
                    RfcElementMetadata metadata = rfcTable.GetElementMetadata(liElement);
                    ldr[metadata.Name] = row.GetString(metadata.Name);
                }
                loTable.Rows.Add(ldr);
            }
            return loTable;
        }
    }
}
