using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CompareWindows.Data;

namespace CompareWindows.View.Window {
    public partial class SvnInputWindow : Form {

        private static string prefix = string.Empty;
        private static string inputStr = string.Empty;

        public SvnInputWindow() {
            InitializeComponent();
            textBox1.Text = prefix;
            richTextBox1.Text = inputStr;
            DataManager data = DataManager.Instance;
            if (string.IsNullOrEmpty(data.filterData.SvnExtensionStr)) {
                StringBuilder str = new StringBuilder();
                str.AppendLine(".meta");
                str.AppendLine(".prefab");
                str.AppendLine(".png");
                data.filterData.SvnExtensionStr = str.ToString();
            } // end if
            richTextBox2.Text = data.filterData.SvnExtensionStr;
        }

        public delegate void OnSvnInputConfirm(IEnumerable<string> list);
        public event OnSvnInputConfirm OnConfirm;

        private void button1_Click(object sender, EventArgs e) {
            prefix = textBox1.Text;
            inputStr = richTextBox1.Text;
            string extensionStr = richTextBox2.Text;
            DataManager.Instance.filterData.SvnExtensionStr = extensionStr;
            int count = prefix.Length;
            HashSet<string> pathList = new HashSet<string>();
            string[] list = inputStr.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] exList = extensionStr.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < list.Length; ++i) {
                if (!list[i].Contains(prefix)) {
                    continue;
                } // end if
                string path = list[i].Remove(0, count);
                path = path.Replace("/", "\\");
                for (int j = 0; j < exList.Length; j++) {
                    if (path.Contains(exList[j])) {
                        pathList.Add(path);
                        continue;
                    } // end if
                } // end for
            } // end for
            if (OnConfirm != null) OnConfirm(pathList);
            // end if
            OnConfirm = null;
            Close();
        }
    }
}
