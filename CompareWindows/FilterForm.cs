using CompareWindows.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CompareWindows {
    public partial class FilterForm : Form {

        private MainForm2 mainForm;

        public FilterForm(MainForm2 mainForm) {
            this.mainForm = mainForm;
            InitializeComponent();
            Text = GetType().ToString();
            var data = DataManager.Instance.filterData;
            inFileText.Text = data.inFileText;
            inDirectoryText.Text = data.inDirectoryText;
            exFileText.Text = data.exFileText;
            exDirectoryText.Text = data.exDirectoryText;
        }

        private void clearBtn_Click(object sender, EventArgs e) {
            inFileText.Text = "";
            inDirectoryText.Text = "";
            exFileText.Text = "";
            exDirectoryText.Text = "";
            DataManager.Instance.filterData.Clear();
        }

        private void comfirmBtn_Click(object sender, EventArgs e) {
            var data = DataManager.Instance.filterData;
            bool isChanged = inFileText.Text != data.inFileText ||
                inDirectoryText.Text != data.inDirectoryText ||
                exFileText.Text != data.exFileText ||
                exDirectoryText.Text != data.exDirectoryText;
            if (data.SetData(inFileText.Text, inDirectoryText.Text, exFileText.Text, exDirectoryText.Text)) {
                if (isChanged) {
                    if (null != mainForm) {
                        mainForm.DoDetectFilter();
                    } // end if
                } // end if
                Close();
            } else {
                MessageBox.Show("过滤信息重复或冲突!", "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end if
        }
        /// <summary>
        /// 检查输入是否重复或冲突
        /// </summary>
        /// <returns> 重复或冲突返回false,否之，返回true </returns>
        private bool CheakInput() {
            HashSet<string> set = new HashSet<string>();
            string[] sections = inFileText.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sections.Length; ++i) {
                if (!set.Add(sections[i])) return false;
                // end if
            } // end for
            sections = exFileText.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sections.Length; ++i) {
                if (!set.Add(sections[i])) return false;
                // end if
            } // end for
            set.Clear();
            sections = inDirectoryText.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sections.Length; ++i) {
                if (!set.Add(sections[i])) return false;
                // end if
            } // end for
            sections = exDirectoryText.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < sections.Length; ++i) {
                if (!set.Add(sections[i])) return false;
                // end if
            } // end for
            return true;
        } // end CheakInput
    }
}
