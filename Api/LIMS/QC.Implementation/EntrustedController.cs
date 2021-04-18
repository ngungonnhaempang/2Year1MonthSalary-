using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using log4net;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Collections;
using System.Reflection;
using QC.Model;
using QC.Utility;



namespace QC.Implementation
{
    [FilterIP]
    [RoutePrefix("api/LIMS/EntrustedController")]
    public class EntrustedController : ApiController
    {
        private NBear.Data.Gateway gate = new NBear.Data.Gateway("LIMS");
        protected readonly ILog Loger = LogManager.GetLogger("HSSELogger");
        HelperBiz _helper = new HelperBiz();

        [Route("getSample")]
        [HttpGet]
        public IHttpActionResult getSample(string UserID, string Query)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_GetSamplesByDepartment",
               new string[] { "UserID", "Query" },
               new object[] { UserID, "0" }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getMaterial")]
        [HttpGet]
        public IHttpActionResult getMaterial(string UserID, string SampleName, string Query)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_i_GetLotNoByDepartment",
               new string[] { "UserID", "SampleName", "Query" },
               new object[] { UserID, SampleName, "0" }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getAttribute")]
        [HttpGet]
        public IHttpActionResult getAttribute(string SampleName)
        {
            try
            {

                DataTable dt = gate.DbHelper.Select("Select * From Attribute WHERE SampleName = @sampleName ",
                    new object[] { SampleName }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getSpec")]
        [HttpGet]
        public IHttpActionResult getSpec(string SampleName, string lang)
        {
            try
            {

                Samples sample = GetSample(SampleName.Trim(), lang);
                WorkCenter workcenter = GetWorkCenter(sample.LabID, lang);

                var Result = new { Msg = string.Format("({0}){1}-{2}", workcenter.Address, workcenter.LabName, sample.TypeName) };
                return Ok(Result);
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getReq4Delegate")]
        [HttpGet]
        public IHttpActionResult getReq4Delegate(string UserID, string DateF, string DateT, string Owner, string state)
        {
            try
            {
                DataTable dt = gate.DbHelper.ExecuteStoredProcedure("aspnet_CC_Q_GetReq4Delegate",
                  new string[] { "UserID", "SendB", "SendE", "Owner", "VoucherState" },
                  new object[] { UserID, DateF, DateT, Owner, state }).Tables[0];

                return Ok(_helper.ConvertJson(dt));
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }

        }

        [Route("getStatus")]
        [HttpGet]
        public IHttpActionResult getStatus()
        {

            DataTable dt = gate.DbHelper.Select("SELECT TOP (1000) [Status],[Spec],[Note]  FROM [LIMS].[dbo].[vStatePlan]",
                                                  new object[] { }).Tables[0];


            return Ok(_helper.ConvertJson(dt));

        }
        [Route("CreateVoucher")]
        [HttpPost]
        public IHttpActionResult CreateVoucher(object note)
        {
            try
            {
                string js = Newtonsoft.Json.JsonConvert.SerializeObject(note);
                Entrusted _Entrusted = Newtonsoft.Json.JsonConvert.DeserializeObject<Entrusted>(js);
                _Entrusted.VoucherID = GetDocID(_Entrusted.Spec.Substring(1, 1));

                if (InsertRequesition(_Entrusted))
                {
                    DataTable dt = GetDraft(_Entrusted.DraftID);
                    return Ok(_helper.ConvertJson(dt));
                }
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
            }
            return Ok();

        }

        [Route("getPropByVoucherID")]
        [HttpGet]
        public IHttpActionResult getPropByVoucherID(string voucherID)
        {
            try
            {
                //string js = Newtonsoft.Json.JsonConvert.SerializeObject(voucherID);
                //DataTable dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(voucherID.ToString());
                //Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(voucherID.ToString());
                
                //ArrayList ABC = new ArrayList();
                
                //foreach (var item in jObject["List"])
                //{
                //    ABC.Add(item["VoucherID"]);
                //}

                DataTable dt = gate.DbHelper.Select("SELECT [VoucherID] ,[SampleName], [PropertyName] from Profile WHERE VoucherID = @VoucherID",
                                                new object[] { voucherID }).Tables[0];
                return Ok(_helper.ConvertJson(dt));
                
            }
            catch {
            
            }
            return Ok();
        }

        public DataTable getProp(string voucherID)
        
        {
            DataTable dt = gate.DbHelper.Select("SELECT [VoucherID] ,[SampleName], [PropertyName] from Profile WHERE VoucherID = @VoucherID",
                                                new object[] { voucherID }).Tables[0];

            return dt;   
        }

                        
        public bool InsertRequesition(Entrusted _Entrusted)
        {
            bool NeedDraft = false;
           
          //  if (_Entrusted.DraftID.Trim().Length < 11)
                if(_Entrusted.DraftID == null || _Entrusted.DraftID == "")
            {
                int count = draftCount(_Entrusted.VoucherID.Trim());
                if (count < 0)
                {
                    return false;
                }
                else
                {
                    // draftID = voucherID.Substring(1, 8) + count.ToString().PadLeft(3, '0');
                   _Entrusted.DraftID = _Entrusted.VoucherID.Substring(1, 8) + count.ToString().PadLeft(4, '0');
                  //  = Draft;
 
                }
                NeedDraft = true;
            }
            //
            bool result = false;
            string createDraft = @"INSERT INTO Draft VALUES(@DraftID,@Owner,GETDATE())";
            //
            string createDraftCommand = @"INSERT INTO ReqInDraft VALUES(@DraftID,@VoucherID,@Stamp)";
            //
            string createRequCommand = @"INSERT INTO Requisition
                                        (VoucherID,SampleName,LOT_NO,Purpose,SampleFrom,RequireDate,SendDate,GetDate,State,Remark)
                                        VALUES(@VoucherID,@SampleName,@LOT_NO,@Purpose,@SampleFrom,@RequireDate,@SendDate,@GetDate,@State,@Remark)";
            System.Data.Common.DbTransaction tran = gate.BeginTransaction();
            //

            try
            {
                if (NeedDraft)
                    gate.ExecuteNonQuery(createDraft, new object[] { _Entrusted.DraftID, _Entrusted.UserID });

                gate.ExecuteNonQuery(createRequCommand, 
                    new object[] { _Entrusted.VoucherID, _Entrusted.SampleName, _Entrusted.Material, _Entrusted.purpose, _Entrusted.sampleFrom,
                        _Entrusted.needDate, _Entrusted.sendDate, _Entrusted.pickTime,"D","D" });
                gate.ExecuteNonQuery(createDraftCommand, 
                    new object[] { _Entrusted.DraftID, _Entrusted.VoucherID, System.DateTime.Now });

                //
                int i = 1;
                foreach (string propertyName in _Entrusted.Properties) 
                {
                    string sqlProfile = @"INSERT INTO 
                                        Profile(VoucherID,Item,SampleName,PropertyName,Result,State,OverRange)
                                        VALUES(@VoucherID,@Item,@SampleName,@PropertyName,@Result,@State,@OverRange)";
                    gate.ExecuteNonQuery(sqlProfile, 
                        new object[] { _Entrusted.VoucherID, i, _Entrusted.SampleName, propertyName, DBNull.Value, "N","0"});
                    i++;
                }
                tran.Commit();
                result = true;
            }
            catch (Exception e)
            {
                tran.Rollback();

                throw e;
            }
            return result;
        }


        //[Route("GetDraft")]
        //[HttpGet]
        public DataTable  GetDraft(string draftID)
        {

            DataTable dt = gate.DbHelper.Select("SELECT [VoucherID],[DraftID]  FROM [ReqInDraft] WHERE ([DraftID] = @DraftID)",
                                                  new object[] { draftID }).Tables[0];

                
                return dt;                   
           
        }
        /// <summary>
        /// 得到委托的ID 
        /// </summary>
        /// <param name="lab"></param>
        /// <returns></returns>
        public string GetDocID(string lab)
        {
            DateTime date = DateTime.Now;
            int count = monthCount(lab);
            if (count == -1) return "";
            return string.Format("RMXQ{0}{1}{2}{3}", date.Year.ToString().Substring(2, 2),
                                                date.Month.ToString().PadLeft(2, '0'),
                                                lab,
                                                count.ToString().PadLeft(4, '0'));
        }

        public string GetDraftID(string lab)
        {
            DateTime date = DateTime.Now;
            string pre = string.Format("MXQ{0}{1}{2}", date.Year.ToString().Substring(2, 2),
                                                  date.Month.ToString().PadLeft(2, '0'),
                                                  lab);


            int flow = gate.SelectScalar<Int32>(string.Format(@"
                                                 SELECT  isnull(max(Convert(int, SUBSTRING(DraftID,9,4))) +1,1)
                                                  FROM Draft
                                                 WHERE DraftID LIKE @Pre"),
                                              new object[] { pre + "%" });


            return pre + flow.ToString().PadLeft(4, '0');

        }

        public int draftCount(string voucherID)
        {
            try
            {
                //                string sqlCommand = @"SELECT isnull(max(Convert(int, SUBSTRING(DraftID,9,3))) +1,1)  FROM Draft 
                //                                  WHERE LEFT(DraftID,8) = SUBSTRING(@VoucherID,2,8)";

                string sqlCommand = @"SELECT isnull(max(Convert(int, SUBSTRING(DraftID,9,4))) +1,1)  FROM Draft 
                                                 WHERE LEFT(DraftID,8) = SUBSTRING(@VoucherID,2,8)";
                return gate.SelectScalar<int>(sqlCommand, new object[] { voucherID });
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
                return -1;
            }
        }

        public int monthCount(string lab)
        {
            if (lab.Trim().Length != 1) return -1;
            try
            {
                string sqlCommand = "SELECT isnull( max(Convert(int, SUBSTRING(ReqInDraft.VoucherID,10,4)))  +1 ,1) FROM Draft INNER JOIN  ReqInDraft ON Draft.DraftID = ReqInDraft.DraftID   WHERE (YEAR(Draft.CreateDate) = YEAR(GETDATE())) AND (SUBSTRING(ReqInDraft.VoucherID, 9, 1) =  @Lab) AND (MONTH(Draft.CreateDate) = MONTH(GETDATE())) ";
                return gate.SelectScalar<int>(sqlCommand, new object[] { lab });
            }
            catch (Exception e)
            {
                MSG_Error = e;
                throw new Exception(e.Message);
                return -1;
            }

        }

        public Samples GetSample(string sampleName, string Lang)
        {


            string sqlCommand = "Select * From vSamples Where SampleName =@sampleName AND Lang = @Lang";
            DataTable dt = gate.DbHelper.Select(sqlCommand, new object[] { sampleName, Lang }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                Samples sample = new Samples();
                sample.SampleName = (string)dt.Rows[0]["SampleName"];
                sample.LabID = Convert.ToInt32(dt.Rows[0]["LabID"]);
                sample.TypeID = Convert.ToInt32(dt.Rows[0]["TypeID"]);
                sample.TypeName = (string)dt.Rows[0]["TypeName"];
                //  sample.Description = (string)dt.Rows[0]["Description"];
                return sample;
            }

            return null;
        }

        public WorkCenter GetWorkCenter(int labID, string lang)
        {

            string sqlCommand = "Select * From WorkCenter Where LabID = '"
                              + labID + "'" + "AND Lang = '" + lang + "'";

            DataTable dt = gate.DbHelper.Select(sqlCommand, new object[] { labID }).Tables[0];
            if (dt.Rows.Count > 0)
            {
                WorkCenter workCenter = new WorkCenter();

                workCenter.LabID = Convert.ToInt32(dt.Rows[0]["LabID"]);
                workCenter.Manager = (string)dt.Rows[0]["Manager"];
                workCenter.Address = (string)dt.Rows[0]["Address"];
                workCenter.Tel = (string)dt.Rows[0]["Tel"];
                workCenter.LabName = (string)dt.Rows[0]["LabName"];
                return workCenter;
            }


            return null;
        }


        Exception MSG_Error
        {
            set
            {
                Console.WriteLine(value.Message);
                Loger.Error(value);
            }
        }

        string MSG_Info
        {
            set
            {
                Console.WriteLine(value);
                Loger.Info(value);
            }
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
