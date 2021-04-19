using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIMS.Views
{
    public partial class QP01Dialog_Month : UserControl
    {
        public QP01Dialog_Month()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 几号
        /// </summary>
        public int FreqInterval
        {
            get
            {
                try
                {
                    return (int)seMonthlyDay.Value;
                }
                catch { return 0; }
            }
        }
        /// <summary>
        /// 每几个月
        /// </summary>
        public int RecurrenceFactor
        {
            get
            {
                try
                {
                    return (int)seMonths.Value;
                }
                catch { return 0; }
            }
        }

        public int TypeMonth
        {
            get
            {
                return 16;
            }
        }

        public int RelativeInterval
        {
            get
            {
                return 0;
            }
        }
    }
}
