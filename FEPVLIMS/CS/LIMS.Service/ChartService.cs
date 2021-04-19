using LIMS.Contract;
using LIMS.Model;
using Shawoo.Common;
using Shawoo.Core;
using System;

namespace LIMS.Service
{
    public class ChartService : MarshalByRefObject, IChart
    {
        public ChartService()
        {
           // acQC.RegisterSqlLogger(new NBear.Common.LogHandler());
        }

        //FEPV LIMS
        protected static NBear.Data.Gateway acQC = new NBear.Data.Gateway("LIMS");

        private DB db = new DB("LIMS");

        public bool AddChart(QCChart Chart)
        {
            Console.WriteLine("ChartDAL - AddChart():" + DateTime.Now.ToString());
            try
            {
                return db.Insert(Chart);
            }
            catch (Exception e)
            {
               
              
                throw new Exception(e.Message);
            }
        }

        public bool UpdateChart(QCChart chart)
        {
            Console.WriteLine("ChartDAL-UpdateChart():" + DateTime.Now.ToString());

            try
            {
                QCChart selectChart = db.Select<QCChart>(new QCChart
                {
                    SampleName = chart.SampleName,
                    MaterialNo = chart.MaterialNo,
                    LineNo = chart.LineNo,
                    Property = chart.Property,
                    Date = chart.Date
                });

                selectChart.LastModify = DateTime.Now;

                return db.Update(selectChart);
            }
            catch (Exception e)
            {
               
               
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public bool DeleteChart(QCChart chart)
        {
            Console.WriteLine("ChartDAL - DeleteChartUCL2LCL():" + DateTime.Now.ToString());
            try
            {
                QCChart selectchart = db.Select<QCChart>(new QCChart { SampleName = chart.SampleName, Property = chart.Property });
                return db.Delete(selectchart);
            }
            catch (Exception e)
            {
               
              
                throw new Exception(e.Message);
            }
        }
    }
}