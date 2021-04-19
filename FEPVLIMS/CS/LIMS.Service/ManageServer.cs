using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LIMS.Contract;
using MIS.Utility;
using System.Data;
using Shawoo.Core;
using Shawoo.Common;
using System.ServiceModel;
using System.Data.Common;
using LIMS.Model;

namespace LIMS.Service
{
    public class ManageServer : MarshalByRefObject, IQCManage
    {
        public ManageServer()
        {
           // acQC.RegisterSqlLogger(new NBear.Common.LogHandler());
        }

        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        public bool AddUser(string DepartmentID, string UserID, out string msg)
        {
            Console.WriteLine("ManageDAL - AddUser():" + DateTime.Now.ToString());
            msg = "";
            try
            {
                if (acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM IM.dbo.vUsers WHERE UserID = @ID", new object[] { UserID }) == 0)
                {
                    msg = string.Format("The User : {0} do not Exist!", UserID);
                    return false;
                }

                if (acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM UsersInDepart WHERE UserID = @ID AND DepartmentID = @DepartmentID", new object[] { UserID, DepartmentID }) == 0)
                {
                    acQC.ExecuteNonQuery(@"INSERT INTO UsersInDepart SELECT @UserID,@DepartmentID, @DepartmentSpec", new object[] { UserID, DepartmentID, DepartmentID });
                    return true;
                }
                else
                {
                    msg = string.Format("Already Exist User: {0} with DepartmentID : {1}", UserID, DepartmentID);
                    return false;
                }
            }
            catch (Exception e)
            {
              
             
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool DeleteUser(string DepartmentID, string UserID, out string msg)
        {
            Console.WriteLine("ManageDAL - DeleteUser():" + DateTime.Now.ToString());
            msg = "";
            try
            {
                if (acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM UsersInDepart WHERE UserID = @ID", new object[] { UserID }) > 0)
                {
                    acQC.ExecuteNonQuery(@"DELETE UsersInDepart WHERE UserID = @UserID AND DepartmentID = @DepartmentID", new object[] { UserID, DepartmentID });
                    return true;
                }
                else
                {
                    msg = string.Format("Do not Exist User: {0} with DepartmentID : {1}", UserID, DepartmentID);
                    return false;
                }
            }
            catch (Exception e)
            {
               
                
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool AddSample(string DepartmentID, string SampleName, string Material, out string msg)
        {
            Console.WriteLine("ManageDAL - AddSample():" + DateTime.Now.ToString());
            msg = "";
            try
            {
                if (acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM SamplesInDepart 
	                WHERE Department = @DepartmentID AND SampleName = @SampleName AND (LOT_NO = @Material OR LOT_NO ='%')",
                    new object[] { DepartmentID, SampleName, Material }) > 0)
                {
                    msg = string.Format("'Already Exist SampleName: {0} - {1} with DepartmentID : {2}", SampleName, Material, DepartmentID);
                    return false;
                }
                else
                {
                    acQC.ExecuteNonQuery(@"INSERT INTO SamplesInDepart SELECT @DepartmentID,@SampleName, @Material",
                                         new object[] { DepartmentID, SampleName, Material });
                    return true;
                }
            }
            catch (Exception e)
            {
              
               
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool DeleteSample(string DepartmentID, string SampleName, string Material, out string msg)
        {
            Console.WriteLine("ManageDAL - DeleteSample():" + DateTime.Now.ToString());
            msg = "";
            try
            {
                if (acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM SamplesInDepart 
	                WHERE Department = @DepartmentID AND SampleName = @SampleName AND LOT_NO = @Material",
                    new object[] { DepartmentID, SampleName, Material }) > 0)
                {
                    acQC.ExecuteNonQuery(@"DELETE SamplesInDepart 
		                                   WHERE Department = @DepartmentID 
		                                   AND SampleName = @SampleName 
		                                   AND (LOT_NO = @Material OR LOT_NO ='%')",
                     new object[] { DepartmentID, SampleName, Material });
                    return true;
                }

                else if (acQC.SelectScalar<int>(@"SELECT COUNT(*) FROM SamplesInDepart 
		WHERE Department = @DepartmentID AND SampleName = @SampleName AND LOT_NO = '%'",
                    new object[] { DepartmentID, SampleName }) > 0)
                {
                    msg = string.Format("SampleName: {0} - {1} belong to %, can not delete!' ", SampleName, Material);
                    return false;
                }

                else
                {
                    msg = string.Format("Do not Exist SampleName: {0} - {1} with DepartmentID : {2} ", SampleName, Material, DepartmentID);
                    return false;
                }
            }
            catch (Exception e)
            {
               
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
