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
    public class ChartBiz
    {
        private readonly IChart proxy = ServiceFactory.Create<IChart>();

        public bool AddChartUCL2LCL(QCChart Chart)
        {
            return proxy.AddChart(Chart);
        }

        public bool UpdateChartUCL2LCL(QCChart Chart)
        {
            return proxy.UpdateChart(Chart);
        }

        public bool DeleteChartUCL2LCL(QCChart Chart)
        {
            return proxy.DeleteChart(Chart);
        }
    }
}