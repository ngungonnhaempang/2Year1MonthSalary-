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
    public partial class QP01Dialog_Weekly : UserControl
    {
        public QP01Dialog_Weekly()
        {
            InitializeComponent();
        }

        public int Weeks
        {
            get
            {
                return (int)seWeeks.Value;
            }
        }
        public int GetWeeklyInterval
        {
            get 
            {
                return (cbSunday.Checked ? 1 : 0) +
                    (cbMonday.Checked ? 2 : 0) +
                    (cbTuesday.Checked ? 4 : 0) +
                    (cbWednesday.Checked ? 8 : 0) +
                    (cbThursday.Checked ? 16 : 0) +
                    (cbFriday.Checked ? 32 : 0) +
                    (cbSaturday.Checked ? 64 : 0);
            }
        }
    }
}
