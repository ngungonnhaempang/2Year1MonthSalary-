using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Model;

namespace LIMS.Contract
{
    public interface IQCManage
    {
        bool AddUser(string DepartmentID, string UserID, out string msg);

        bool DeleteUser(string DepartmentID, string UserID, out string msg);

        bool AddSample(string DepartmentID, string SampleName, string Material, out string msg);

        bool DeleteSample(string DepartmentID, string SampleName, string Material, out string msg);
    }
}
