using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Contract;
using LIMS.Model;
using Shawoo.Core;
using MIS.Utility;


namespace LIMS.Service
{
    public class QCReporting
    {
        private readonly IQCReport proxy = ServiceFactory.Create<IQCReport>();

        public DataSet GetQCReportByPage(string procedureName, string[] paramenters, object[] values, out int count)
        {
            DataSet result = null;
            byte[] b = proxy.GetQCReportByPage(procedureName, paramenters, values, out count);
            result = DataFormatter.RetrieveDataSetDecompress(b);
            return result;
        }
        public DataSet Reporting(string procdureName, string[] paramenters, object[] values)
        {

            DataSet result = null;
            byte[] b = proxy.GetQCReport(procdureName, paramenters, values);
            result = DataFormatter.RetrieveDataSetDecompress(b);

            return result;
        }
        public DataSet GetQCReport(string procedureName, string[] paramenters, object[] values)
        {
            DataSet result = null;
            byte[] b = proxy.GetQCReport(procedureName, paramenters, values);
            result = DataFormatter.RetrieveDataSetDecompress(b);

            return result;
        }

        public DataSet SearchDataByPage(string TableName, string Select, string OrderBy, int Size, int Index, bool ASC, string Where, out int Count)
        {
            DataSet result = null;
            byte[] b = proxy.SearchDataByPage(TableName, Select, OrderBy, Size, Index, ASC, Where, out Count);
            result = DataFormatter.RetrieveDataSetDecompress(b);

            return result;
        }


    }
}