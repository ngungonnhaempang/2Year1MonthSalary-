using LIMS.Contract.Huy;
using LIMS.Model;
using Shawoo.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.BLL.Huy
{
    public class RegisterBiz
    {
        public readonly IRegistration proxy = ServiceFactory.Create<IRegistration>();
     
        public bool AddRegistration(Registration _regist)
        {
            return proxy.AddRegistration(_regist);
        }

        public bool DeleteRegistration(string _regist)
        {
            return proxy.DeleteRegistration(_regist);
        }

        public Registration FindRegistbyID(string idRegist)
        {
            return proxy.FindRegistbyID(idRegist);
        }

        public string createNewID()
        {
            return proxy.createNewID();
        }

        public DataTable SearchRegister(Registration _regist)
        {
            return proxy.SearchRegister(_regist);
        }

        public List<Location> GetLocation()
        {
            return proxy.GetLocation();
        }

        public DataTable GetUniversity(string idLoc)
        {
            return proxy.GetUniversity(idLoc);
        }

        public string GetNameLocation(string idLoc)
        {
            return proxy.GetNameLocation(idLoc);
        }

        public int BallEvent()
        {
            return proxy.BallEvent();
        }

        //public string WhoPickBall(int position)
        //{
        //     return proxy.WhoPickBall(position);
        //}
        
    }
}
