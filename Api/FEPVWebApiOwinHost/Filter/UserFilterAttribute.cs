using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Configuration;
using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using log4net;
using System.Net;
using System.Text;
namespace FEPVWebApiOwinHost.Filter
{
    public class UserFilterAttribute : AuthorizationFilterAttribute
    {
        protected readonly ILog log = log4net.LogManager.GetLogger("HSSELogger");
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果用户方位的Action带有AllowAnonymousAttribute，则不进行授权验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
              
            if (actionContext.Request.Headers.Authorization != null) //并且Authorization参数为123456则验证通过
            {
                string authenticationToken = actionContext.Request.Headers
                                           .Authorization.Parameter;
                log.Info(authenticationToken);
                string decodedAuthenticationToken = Encoding.UTF8.GetString(
                    Convert.FromBase64String(authenticationToken));
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];
                log.Info("OnAuthorization:" + username + password);
                if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
                {
                    //如果验证不通过，则返回401错误，并且Body中写入错误原因
                    //   actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new System.Web.Http.HttpError("Token 不正确"));
                    log.Info("OnAuthorization:" + username + password);

                    actionContext.Response =
                        new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                        {
                            Content = new StringContent("Unauthorized User ")
                        };
                    log.Info(string.Format("Unauthorized User：{0}", actionContext.Request.Headers.Authorization.Parameter));

                    return;
                }
                else
                {
                    //write config
                    if (username == "MIS" && password == "fepv123456")
                    {
                        return;
                    }
                    else {
                        actionContext.Response =
                           new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                           {
                               Content = new StringContent("Unauthorized User ")
                           };
                       
                        return;
                    }
                    
                }
            }

            actionContext.Response =
                      new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                      {
                          Content = new StringContent("Unauthorized User,请定义Header Authorization 参数 username and password ")
                      };

            return;

        }
    }
}