using CompareWindows.Data;
using CompareWindows.Event;
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
    public partial class TreeWindow : Form {

        private string leftRoot;
        private string rightRoot;
        private TreeModle treeModle;

        public TreeWindow() {
            InitializeComponent();
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
            treeViewAdv1.SelectionChanged += OnSelectionChanged;
        }

        private void browerBtn1_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox1.Text = path;
                comboBox1.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath1(path);
                leftRoot = path;
            } // end if
        }

        private void browerBtn2_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox2.Text = path;
                comboBox2.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath2(path);
                rightRoot = path;
            } // end if
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            leftRoot = comboBox1.Text;
            if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                ResetModle(leftRoot, rightRoot);
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            rightRoot = comboBox2.Text;
            if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                ResetModle(leftRoot, rightRoot);
            } // end if
        }

        private void ExpandAllMenuItem_Click(object sender, EventArgs e) {
            treeViewAdv1.ExpandAll();
        }

        private void CollapseAllMenuItem_Click(object sender, EventArgs e) {
            treeViewAdv1.CollapseAll();
        }

        private void OnProgressChanged(object sender, ProgressEventArgs e) {
            if (!statusStrip1.Items[1].Visible) {
                statusLabel.Text = "正在对比";
                statusStrip1.Items[1].Visible = true;
                statusStrip1.Items[2].Visible = true;
            } // end if
            compareProgressBar.Maximum = e.MaxiMum;
            compareProgressBar.Value = e.Current;
            progressLabel.Text = e.Percentagep;
            if (e.Current >= e.MaxiMum) {
                statusLabel.Text = "对比完成";
                statusStrip1.Items[1].Visible = false;
                statusStrip1.Items[2].Visible = false;
            } // end if
        } // end OnProgressChanged

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (treeModle != null) {
                treeModle.progress.ProgressChanged -= OnProgressChanged;
            } // end if
            base.OnFormClosing(e);
        }

        private void ResetModle(string leftRoot, string rightRoot) {
            if (treeModle != null) {
                treeModle.progress.ProgressChanged -= OnProgressChanged;
            } // end if
            treeModle = new TreeModle(leftRoot, rightRoot);
            treeModle.progress.ProgressChanged += OnProgressChanged;
            treeViewAdv1.Model = treeModle;
            statusStrip1.Items[1].Visible = false;
            statusStrip1.Items[2].Visible = false;
        } // end ResetModle

        private void OnSelectionChanged(object sender, EventArgs e) {
            BaseItem item = treeViewAdv1.SelectedNode.Tag as BaseItem;
            if (item != null) {
            } // end 
        } // end OnSelectionChanged
    }
}
