using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LIMS.Model;

namespace LIMS.Contract
{
    public interface IChart
    {
        bool AddChart(QCChart chart);

        bool UpdateChart(QCChart chart);

        bool DeleteChart(QCChart chart);
    }
}
