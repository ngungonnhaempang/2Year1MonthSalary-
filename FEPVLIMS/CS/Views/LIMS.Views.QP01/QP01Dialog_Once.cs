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
    public partial class QP01Dialog_Once : UserControl
    {
        public QP01Dialog_Once()
        {
            InitializeComponent();
            dateOnce.DateTime = DateTime.Now;
        }
        public DateTime DateOnce
        {
            get { return dateOnce.DateTime; }
            set { dateOnce.DateTime = value; }
        }
    }
}
