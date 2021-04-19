﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LIMS.Contract;
using LIMS.Model;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using Newtonsoft.Json;

namespace LIMS.Service
{
    public class QCReportService : MarshalByRefObject, IQCReport
    {
        public QCReportService()
        {
          //  acQC.RegisterSqlLogger(new NBear.Common.LogHandler());
        }
        //FEPV MIS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        public byte[] GetQCReportByPage(string procedureName, string[] paramenters, object[] values, out int count)
        {
            Console.WriteLine("QCReportDAL - DataTable GetMISReportByPage()" + " - " + DateTime.Now.ToString());

            List<string> ps = paramenters.ToList();
            ps.Add("UserID");
            paramenters = ps.ToArray();
            List<object> vs = values.ToList();
            vs.Add(Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString());
            values = vs.ToArray();

            try
            {
                object[] outParameters;
                DataSet ds = acQC.DbHelper.ExecuteStoredProcedure(procedureName, paramenters, values,
                                           new string[] { "Count" }, new DbType[] { DbType.Int32 },
                                          out outParameters);

                count = (int)outParameters.ElementAt(0);
                return DataFormatter.GetBinaryFormatDataCompress(ds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
              
               
                throw e;
            }
        }
        
        public byte[] GetQCReport(string procdureName, string[] paramenters, object[] values)
        {
            try
            {
                Console.WriteLine("procedureName:" + procdureName);
                List<string> ps = paramenters.ToList();
                ps.Add("UserID");
                paramenters = ps.ToArray();
                List<object> vs = values.ToList();
                //vs.Add(Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString());
                vs.Add(Shawoo.GenuineChannels.GenuineUtility.CurrentSession["UID"].ToString());
                values = vs.ToArray();
                Console.WriteLine("paramenters:" + JsonConvert.SerializeObject(paramenters));
                Console.WriteLine("values:" + JsonConvert.SerializeObject(values));
                DataSet ds = acQC.DbHelper.ExecuteStoredProcedure(procdureName, paramenters, values);
                return DataFormatter.GetBinaryFormatDataCompress(ds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
              
                throw e;
            }
        }

        public byte[] SearchDataByPage(string TableName, string Select, string OrderBy, int Size, int Index, bool ASC, string Where, out int Count)
        {
            object[] outParameters;
            DataSet ds=new DataSet();
            try
            {
                ds = acQC.ExecuteStoredProcedure("_PagerSql2016_out_count",
                                                   new string[] { "tblName", "fldCow", "fldName", "PageSize", "PageIndex", "OrderType", "strWhere" },
                                                   new object[] { TableName, Select, OrderBy, Size, Index, ASC, Where },
                                                   new string[] { "count" },
                                                   new DbType[] { DbType.Int32 },
                                                   out outParameters
                                                   );
                Count = (int)outParameters.ElementAt(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DataFormatter.GetBinaryFormatDataCompress(ds);
        }
    }
}
