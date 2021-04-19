using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LIMS.Model;
using System.Data;

namespace LIMS.Contract
{
    public interface IQCReport
    {
        byte[] GetQCReportByPage(string procedureName, string[] ps, object[] vs, out int count);

        byte[] GetQCReport(string procedureName, string[] paramenters, object[] values);

        byte[] SearchDataByPage(string tableName, string select, string orderBy, int size, int index, bool asc, string where, out int count);
    }

}
