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
    public partial class QP01Dialog_Daily : UserControl
    {
        public QP01Dialog_Daily()
        {
            InitializeComponent();
        }

        public int Days
        {
            get
            {
                return (int)seDays.Value;
            }
        }
    }
}
