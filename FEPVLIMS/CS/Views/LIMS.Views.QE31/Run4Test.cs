using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace LIMS.Views
{
    static class Program
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

            Shawoo.Common.Token.URL = "gtcp://10.20.46.23:1314";
            Shawoo.Common.Token.UID = "cassie";
            Shawoo.Common.Token.PWD = "123456";
            MIS.Utility.MyLanguage.Language = "CN";
            RemotingConfiguration.Configure("Remoting.xml", false);

            var c = new QE31();
          //  c.IsLanguage = false;
            //c.ProdType = "C";
            //c.SerialSettings = "COM2,9600,0,7,2";
            //c.TabText = "Create Goods SSP [CPA01]";
            Application.Run(c);
        }
    }
}