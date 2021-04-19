using LIMS.Service;
using LIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using NBear.Data;
using LIMS.Contract.Huy;
using System.Data.SqlClient;
using Shawoo.Common;

namespace LIMS.Service
{
    public class RegisterService : MarshalByRefObject, IRegistration
    {
        MyContext _mycontext = new MyContext();

        Gateway _gate = new Gateway("UniversityNbear");

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public bool AddRegistration(Registration _regist)
        {
            try
            {
                var _resultFindRegis = _mycontext.Registrations.Find(_regist.ID);
                if (_resultFindRegis == null)
                {
                    _mycontext.Registrations.Add(_regist);
                }
                else
                {
                    _resultFindRegis.Loc_ID = _regist.Loc_ID;
                    _resultFindRegis.Uni_ID = _regist.Uni_ID;
                    _resultFindRegis.Name = _regist.Name;
                    _resultFindRegis.Note = _regist.Note;
                    _mycontext.Entry(_resultFindRegis).State = EntityState.Modified;
                }

                var result = _mycontext.SaveChanges();
                return Convert.ToBoolean(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool DeleteRegistration(string _regist)
        {
            try
            {
                //c1// _mycontext.Entry(_regist).State = EntityState.Deleted;   
                //c2// _mycontext.Registrations.Remove(_regist);
                //  var result = _mycontext.SaveChanges();

                //c3// var result = _gate.DbHelper.ExecuteNonQuery("Delete from Registration where ID = @id", new object[] { _regist });
                var result = _mycontext.Database.ExecuteSqlCommand("Delete from Registration where ID = @id", new SqlParameter("@id", _regist));

                return Convert.ToBoolean(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Registration FindRegistbyID(string idRegist)
        {
            var _rs = _mycontext.Registrations.Find(idRegist);
            return _rs;
        }

        public string createNewID()
        {
            var _newID = _gate.SelectScalar<string>("SELECT dbo.fnCreateRegistID()", new object[] { });
            return _newID;
        }

        public DataTable SearchRegister(Registration _regist)
        {
            var _dtSearch = _gate.DbHelper.ExecuteStoredProcedure("SearchRegister", new string[] { "RegistID", "LocID", "UniID", "name" }, new object[] { _regist.ID, _regist.Loc_ID, _regist.Uni_ID, _regist.Name }).Tables[0];
            return _dtSearch;
        }

        public List<Location> GetLocation()
        {
            var _dtLocation = _mycontext.Locations.SqlQuery(@"select '' as Loc_ID, '' as Loc_Name, '' as Description 
	                                                            union all
	                                                            select *  from [Location] ").ToList();
            return _dtLocation;
        }

        public DataTable GetUniversity(string idLoc)
        {
            try
            {
                var _dtUniversity = _gate.DbHelper.ExecuteStoredProcedure("GetUniversityByLoc", new string[] { "Loc_id" }, new object[] { idLoc }).Tables[0];
                return _dtUniversity;
            }
            catch (Exception e)
            {
                log.Error(e);
                Logger.Warnning(e);
                throw new Exception(e.Message);
            }
          
        }

        public string GetNameLocation(string idLoc)
        {

            var nameLoc = _gate.SelectScalar<string>("Select Loc_Name from Location where Loc_ID = @LocID",new object[]{idLoc});
            return nameLoc;           
        }

        public int BallEvent()
        {
            Random _random = new Random();

            return _random.Next(100);
 
        }

        //public string WhoPickBall(int position)
        //{
        //    var msg = "";
        //    if (position < 40) 
        //    {
        //        msg = "The ball is kicked towards the goal !!\n The goalkeeper has picked up the ball";

        //    }
        //    else if(position < 99)
        //    {
        //        msg = "The ball is on the ground\n The players are trying to catch the ball ";

        //    }
        //    else
        //    {
        //        msg = " The ball flies towards the stand! I’m going for the ball!";
        //    }
        //    return msg;
        //}
  
    }
}
