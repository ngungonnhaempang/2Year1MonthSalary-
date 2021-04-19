using System;
using System.Data;
using LIMS.BLL;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace LIMS.Views
{
    public partial class QE31CU : Infrastructure.StyleForm
    {
        public QE31CU()
        {
            InitializeComponent();
            btYes.Click += btYes_Click;
            btNo.Click += btNO_Click;
            ceMaterial.CheckedChanged += ceMaterial_CheckedChanged;
            cmbMaterialList.KeyDown += cmbMaterialList_KeyDown;
            txtRemark.TextChanged += txtRemark_TextChanged;
            this.FormClosed += QE31CU_FormClosed;
            txtcl.TextChanged += correctValue_changed;
            txtca.TextChanged += correctValue_changed;
            txtcb.TextChanged += correctValue_changed;
            btnX.Click += btnX_Click;
            btnAdd.Click += btnAdd_Click;
        }

        
        #region init list
        string cl_code = ""; //FIRST PARAM FROM THE INPUT STRING. example: "ls-06"
        bool _ischangedList = false; // = true if any kind of interacting with correctDataList
        private DataTable correctDataList = new DataTable();

        public DataTable MaterialList
        {
            set
            {
                ToolTip tip = new ToolTip();
                tip.SetToolTip(cmbMaterialList, "Press hotkey Ctrl + Del to delete the selected item");
                cmbMaterialList.DataSource = value;
                cmbMaterialList.DisplayMember = "LOT_NO";
                cmbMaterialList.ValueMember = "LOT_NO";
                cmbMaterialList.SelectedItem = null;
                cmbMaterialList.SelectedIndexChanged += cmbMaterialList_SelectedIndexChanged;

            }
        }

        public DataTable MaterialListPrepare()
        {

            string path = @"resources\\QE31_QE31CU.xml";
            if (!File.Exists(path))  //if file is not existed then create it
            {
                #region QE31_QE31CU.xml detail
                string str = @"<?xml version=""1.0""?>
                <CVALUE>
                    <COLUMN id=""1"" name=""SSP"">
		                <LOT_NO>SSP</LOT_NO>
		                <L>-0.8</L>
                        <a>-2.4</a>
                        <b>0.4</b>
                    </COLUMN>
                    <COLUMN id=""2"" name=""CP-A30"">
		                <LOT_NO>CP-A30</LOT_NO>
                        <L>-1.0</L>
                        <a>-0.3</a>
                        <b>2</b>
                    </COLUMN>
                    <COLUMN id=""3"" name=""CP-A36"">
		                <LOT_NO>CP-A36</LOT_NO>
                        <L>-1.0</L>
                        <a>-0.3</a>
                        <b>2</b>
                    </COLUMN>
                    <COLUMN id=""4"" name=""CP-A20A"">
		                <LOT_NO>CP-A20A</LOT_NO>
                        <L>-1.0</L>
                        <a>-1.1</a>
                        <b>2</b>
                    </COLUMN>
                </CVALUE>";
                XDocument mydoc = XDocument.Parse(str);
                mydoc.Save(path);
                #endregion
            }
            XDocument doc = XDocument.Load(path);
            if (correctDataList.Columns.Count == 0) //if correctDataList is not load then add it's column
            {
                correctDataList.Columns.Add("LOT_NO");
                correctDataList.Columns.Add("L");
                correctDataList.Columns.Add("a");
                correctDataList.Columns.Add("b");
            }

            if (correctDataList.Rows.Count == 0) //if correctDataList is not load then add it's records
            {
                foreach (System.Xml.Linq.XElement el in doc.Root.Elements())
                {
                    DataRow row = correctDataList.NewRow();
                    foreach (System.Xml.Linq.XElement element in el.Elements())
                        row[element.Name.ToString()] = element.Value;
                    correctDataList.Rows.Add(row);
                }
                correctDataList.Rows.Add(correctDataList.NewRow()["LOT_NO"] = "Add new item...");
            }

            return correctDataList;
        }

        public EventHandler eventShowMessage;
        public string[] Paras //the result to upload to itemlist
        {
            get
            {
                return new string[] { cl_code, txtfl.Text, txtfa.Text, txtfb.Text };
            }
        }
        public void Clear()
        {
            _ischangedList = false;
            _rValue = false;
            txtRemark.Text = string.Empty;
            ceMaterial.Checked = false;
            txtcl.Text = string.Empty;
            txtca.Text = string.Empty;
            txtcb.Text = string.Empty;
            txtfl.Text = string.Empty;
            txtfa.Text = string.Empty;
            txtfb.Text = string.Empty;


        }
        #endregion
        private void QE31CU_Load(object sender, EventArgs e)
        {
            addValuePanel.Height = 0;
        }

        public string msg { set { if (eventShowMessage != null) eventShowMessage(value, EventArgs.Empty); } }

        #region ValueChange events

        private void txtRemark_TextChanged(object sender, EventArgs e)
        {
            addFixedResult();
        }
        private void correctValue_changed(object sender, EventArgs e)
        {
            if (txtcl.Focused) //if any of correct value is forcus edit then update list
                updateMaterialList("L", txtcl.Text);
            else if (txtca.Focused)
                updateMaterialList("a", txtca.Text);
            else if (txtcb.Focused)
                updateMaterialList("b", txtcb.Text);
        }
        private void cmbMaterialList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if form is inited or data are just pasted to remark then return, because the txtRemark_TextChanged has calculated fixed value
            if (cmbMaterialList.Text == "" || (txtRemark.Focused && cmbMaterialList.SelectedIndex == -1)) return;
            // if add item then show the addnewitem panel
            if (cmbMaterialList.SelectedValue.ToString() == "Add new item...")
            {
                addValuePanel.Height = 35;
                txtnl.Text = string.Empty;
                txtna.Text = string.Empty;
                txtnb.Text = string.Empty;
                txtnewLOT.Text = string.Empty;
            }
            //show the value and calculate
            var dt = correctDataList.Select("LOT_NO='" + cmbMaterialList.Text + "'").FirstOrDefault();
            txtcl.Text = dt["L"].ToString();
            txtca.Text = dt["a"].ToString();
            txtcb.Text = dt["b"].ToString();
            addFixedResult();
        }
        private void ceMaterial_CheckedChanged(object sender, EventArgs e)
        {
            bool enable = ceMaterial.Checked;
            txtcl.Enabled = enable;
            txtca.Enabled = enable;
            txtcb.Enabled = enable;
        }
        /// <summary>
        /// CALCULATE AND UPDATE FIXED RESULT
        /// </summary>
        private void addFixedResult()
        {
            string[] strarray = txtRemark.Text.Split('\t');
            cl_code = strarray[0].ToString();
            if ((cl_code.ToLower().Contains("s")) && txtRemark.Focused)
            {
                cmbMaterialList.SelectedValue = "SSP";
            }
            if (strarray.Length == 4 && txtcl.Text != "" && txtca.Text != "" && txtcb.Text != "")
            {
                try
                {
                    txtfl.Text = (Convert.ToDouble(strarray[1].ToString()) + Convert.ToDouble(txtcl.Text)).ToString();
                    txtfa.Text = (Convert.ToDouble(strarray[2].ToString()) + Convert.ToDouble(txtca.Text)).ToString();
                    txtfb.Text = (Convert.ToDouble(strarray[3].ToString()) + Convert.ToDouble(txtcb.Text)).ToString();
                    btYes.Enabled = true;
                }
                catch
                {
                    //user try adding wrong format value?
                    btYes.Enabled = false;
                }
            }


        }

        /// <summary>
        /// Update correctDataList
        /// </summary>
        /// <param name="materiallistname"></param>
        /// <param name="propname"></param>
        /// <param name="value"></param>
        private void updateMaterialList(string propname, string value)
        {
            
            correctDataList.Rows[cmbMaterialList.SelectedIndex][propname] = value;
            correctDataList.AcceptChanges();
            _ischangedList = true;
            addFixedResult();
            

        }
        #endregion

        #region Button events
        //DELETE ITEM FROM LIST BY PRESS KEY F9
        void cmbMaterialList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Delete)
            {
                var dialogresult = MessageBox.Show("You want to delete this item?", "Delete item confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogresult == DialogResult.Yes)
                {
                    var dt = correctDataList.Select("LOT_NO='" + cmbMaterialList.Text + "'").FirstOrDefault();
                    _ischangedList = true;
                    cmbMaterialList.Focus();
                    correctDataList.Rows.Remove(dt);
                    cmbMaterialList.SelectedIndex = 0;
                }
            }

        }
        void btnAdd_Click(object sender, EventArgs e)
        {
            var clonetb = correctDataList.Clone();
            var rowadd = clonetb.NewRow();
            rowadd["LOT_NO"] = txtnewLOT.Text;
            rowadd["L"] = txtnl.Text;
            rowadd["a"] = txtna.Text;
            rowadd["b"] = txtnb.Text;
            clonetb.Rows.Add(rowadd);
            clonetb.Merge(correctDataList);
            correctDataList = clonetb;
            correctDataList.AcceptChanges();
            cmbMaterialList.DataSource = correctDataList;
            _ischangedList = true;
            btnX_Click(sender, e);

        }

        void btnX_Click(object sender, EventArgs e)
        {
            addValuePanel.Height = 0;
        }
        private void btYes_Click(object sender, EventArgs e)
        {
            var dialogresult = MessageBox.Show("This upload will overwrite the old value (if existed), click Yes to confirm.", "Upload confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogresult == DialogResult.Yes)
            {
                _rValue = true;
                this.Close();
            }

        }
        public string ReadyWork
        {
            get
            {
                string msg = "";
                if (string.IsNullOrEmpty(txtRemark.Text.Trim()))
                    msg += "Reason is Empty";
                if (!ceMaterial.Checked || false || false)
                    msg += "/nNo any items is pick";
                //msg += Translator("QC01_MSG_10");
                return msg;
            }
        }
        private void btNO_Click(object sender, EventArgs e)
        {
            _rValue = false;
            this.Close();
        }
        bool _rValue = false;

        public bool rValue
        {
            get
            {
                return _rValue;
            }
            set { }
        }
        #endregion Button events


        void QE31CU_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {

            if (_ischangedList)
            {
                string path = @"resources\\QE31_QE31CU.xml";
                string str = @"<?xml version=""1.0""?><CVALUE>";
                for (int i = 0; i < correctDataList.Rows.Count - 1; i++)
                {
                    //lưu vào file xml
                    DataRow dr = correctDataList.Rows[i];
                    str += @"<COLUMN><LOT_NO>" + dr["LOT_NO"].ToString()
                        + "</LOT_NO><L>" + dr["L"].ToString()
                        + "</L><a>" + dr["a"].ToString()
                        + "</a><b>" + dr["b"].ToString()
                        + "</b></COLUMN>";
                }
                str += "</CVALUE>";
                Console.WriteLine("Create new QE31_QE31CU.xml:");
                Console.WriteLine(str);
                XDocument xdoc = XDocument.Load(path);
                xdoc = XDocument.Parse(str);
                xdoc.Save(path);
            }

            //}
            return;
        }




    }
}
