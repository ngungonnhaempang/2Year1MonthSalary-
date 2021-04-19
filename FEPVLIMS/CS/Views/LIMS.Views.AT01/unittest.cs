using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

using Infrastructure;

namespace LIMS.Views
{
   static class unittest
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            MIS.Utility.MyLanguage.currentCultureInfo = new System.Globalization.CultureInfo("en-US");
            MIS.Utility.MyLanguage.rm = new System.Resources.ResourceManager("BasicLanuage.ReadFiles", System.Reflection.Assembly.Load("BasicLanuage"));

            Shawoo.Common.Token.URL = "gtcp://10.20.46.23:1314"; // server cần debug 
            Shawoo.Common.Token.UID = "cassie";// tương ứng với server cần debug
            Shawoo.Common.Token.PWD = "123456";
            MIS.Utility.MyLanguage.Language = "VN";// ngôn ngữ
            RemotingConfiguration.Configure("Remoting.xml", false);// bắt buộc phải có

         

            var c = SingletonFormProvider.GetInstance<AT01>();

            Application.Run(c);
        }
    }
}
