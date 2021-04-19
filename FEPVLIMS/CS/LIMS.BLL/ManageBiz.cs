using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Contract;
using Shawoo.Core;
using LIMS.Model;

namespace LIMS.Service
{
    public class ManageBiz
    {
        private readonly IQCManage proxy = ServiceFactory.Create<IQCManage>();

        public bool AddUser(string DepartmentID, string UserID, out string msg)
        {
            return proxy.AddUser(DepartmentID, UserID, out msg);
        }

        public bool DeleteUser(string DepartmentID, string UserID, out string msg)
        {
            return proxy.DeleteUser(DepartmentID, UserID, out msg);
        }

        public bool AddSample(string DepartmentID, string SampleName, string Material, out string msg)
        {
            return proxy.AddSample(DepartmentID, SampleName, Material, out msg);
        }

        public bool DeleteSample(string DepartmentID, string SampleName, string Material, out string msg)
        {
            return proxy.DeleteSample(DepartmentID, SampleName, Material, out msg);
        }

    }
}