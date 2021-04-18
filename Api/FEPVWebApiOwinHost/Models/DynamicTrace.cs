using System;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;

namespace FEPVWebApiOwinHost.Models
{
    public class DynamicTrace : SignalRBase<2>, ITraceWriter
    {
        //private static readonly ILog log = log4net.LogManager.GetLogger(typeof(DynamicTrace));
        //protected readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level == TraceLevel.Off)
                return;

            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            WriteLog(rec);
        }
        public void WriteLog(TraceRecord record)
        {
            if (!string.IsNullOrWhiteSpace(record.Message))
            {
                String strLog = string.Format("{0}: {1} - {2}", DateTime.Now, record.Status, record.Message);
                //log.Info(strLog);
                Hub.Clients.All.logMessage(strLog);
            }

            if (record.Exception != null)
            {
                String strLog = string.Format("{0}:{1} ::::====>>>> 出现异常，异常信息: {2}", DateTime.Now,record.Operation, record.Exception.Message);
                //log.Error(strLog);
                Hub.Clients.All.logMessage(strLog);
            }

            //var message = new StringBuilder();
            //message.AppendMessage(record.Level.ToString().ToUpper());
            //message.AppendMessage(DateTime.Now.ToString());

            //if (record.Request != null)
            //    message.AppendMessage(record.Request.Method.ToString(), notEmpty).AppendMessage(record.Request.RequestUri.ToString(), notEmpty);

            //message.AppendMessage(record.Category, notEmpty).AppendMessage(record.Operator, notEmpty)
            //    .AppendMessage(record.Operation).AppendMessage(record.Message, notEmpty);

            //if (record.Exception != null)
            //    message.AppendMessage(record.Exception.GetBaseException().Message, notEmpty);

            //Hub.Clients.All.logMessage(message.ToString());



            //String strLog = string.Format("{0};{1};{2};{3}", rec.Category, rec.Operator, rec.Operation, rec.Message);
            //log.Info(strLog);
        }

        //Func<string, bool> notEmpty = (text) => !string.IsNullOrWhiteSpace(text);
    }

    public static class MyExtensions
    {
        public static StringBuilder AppendMessage(this StringBuilder sb, string text, Func<string, bool> predicate = null)
        {
            if (predicate != null)
            {
                if (predicate(text))
                {
                    sb.Append(" ");
                    sb.Append(text);
                }
            }
            else
            {
                sb.Append(" ");
                sb.Append(text);
            }

            return sb;
        }
    }   
}
