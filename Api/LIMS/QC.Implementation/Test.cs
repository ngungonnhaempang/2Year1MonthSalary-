using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data;
using System.Data.Common;


namespace QC.Implementation
{
    [QC.Utility.FilterIP]
    [RoutePrefix("api/LIMS/Leo")]
    public class LIMS111Controller : ApiController
    {
        [Route("leo")]
        [HttpGet]
        public IHttpActionResult leo()
        {
            return Ok(new { a = "123", b = "123" });
        }
    }
}
