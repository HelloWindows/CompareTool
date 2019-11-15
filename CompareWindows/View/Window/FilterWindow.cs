using CompareWindows.Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.View.Window {
    public partial class FilterWindow : Form {
        public FilterWindow() {
            InitializeComponent();
            richTextBox1.Text = FilterModle.fileStr;
            richTextBox2.Text = FilterModle.folderStr;
        }

        private void clearBtn_Click(object sender, EventArgs e) {
            FilterModle.Clear();
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
        }

        private void confirmBtn_Click(object sender, EventArgs e) {
            string fileStr = richTextBox1.Text;
            FilterModle.SetFileStr(fileStr);
            string folderStr = richTextBox2.Text;
            FilterModle.SetFolderStr(folderStr);
            Close();
        }
    }
}
