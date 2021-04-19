using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LIMS.DAL
{
   public static class Recieve_Facotry
    {

       public static IReceiveOperation CreateRecive(string typeName)
       {
           switch (typeName)
           {

               case "UnJointTruck":
                   return new GatePlanReceive();

               case "PtaEgTruck":
                   return new GatePlanReceive();

               case "MIGO":
                   return new MIGOReceive();


               default:
                   throw new Exception("NO Fond Type" + typeName.ToString());
           }
       }
    }
}
