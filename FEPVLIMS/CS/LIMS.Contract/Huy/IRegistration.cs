using LIMS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMS.Contract.Huy
{
    public interface IRegistration
    {
        bool AddRegistration(Registration _regist);

        bool DeleteRegistration(string _regist);

        Registration FindRegistbyID(string idRegist);

        string createNewID();

        DataTable SearchRegister(Registration _regist);

        List<Location> GetLocation();

        DataTable GetUniversity(string idLoc);

       
        string GetNameLocation(string idLoc);

        int BallEvent();

      //  string WhoPickBall(int position);


    }
}
