using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LIMS.Model;
using LIMS.BLL;
using Shawoo.Core;
using BasicLanuage;

namespace LIMS.Views
{
    public partial class FormEditer : Infrastructure.StyleForm
    {
        public FormEditer()
        {
            InitializeComponent();
            #region Language

            try
            {
                //this.TabText = CultureLanuage.Translator(this.Name, 0, "Result Input [QE31]");
                CultureLanuage.ApplyResourcesFrom(this, "QC06", "FormEditer");
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show(ex.Message);
            }

            #endregion
            this.cmbCreateType.DataSource = rep.GetQCReport("Q_GetCreatePlanType", new string[] { "Lang" }, new object[] { MIS.Utility.MyLanguage.Language }).Tables[0];
            this.cmbCreateType.DisplayMember = "Type";
            this.cmbCreateType.ValueMember = "ID";

        }

        QCReporting rep = new QCReporting();
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSearch_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        public string CreateType
        {
            get { return cmbCreateType.SelectedValue.ToString(); }
        }
    }
}
