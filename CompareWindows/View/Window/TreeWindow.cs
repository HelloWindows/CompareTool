using CompareWindows.Data;
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
        private BackgroundWorker _compare;

        public TreeWindow() {
            InitializeComponent();
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
            _compare = new BackgroundWorker();
            _compare.WorkerReportsProgress = true;
            _compare.DoWork += new DoWorkEventHandler(CompareFiles);
            _compare.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            _compare.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompareCompleted);
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
            StopConpare();
            if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                TreeModle treeModle = new TreeModle(leftRoot, rightRoot);
                treeViewAdv1.Model = treeModle;
                ResetCompare();
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            rightRoot = comboBox2.Text;
            StopConpare();
            if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                TreeModle treeModle = new TreeModle(leftRoot, rightRoot);
                treeViewAdv1.Model = treeModle;
                ResetCompare();
            } // end if
        }

        private void ExpandAllMenuItem_Click(object sender, EventArgs e) {
            treeViewAdv1.ExpandAll();
        }

        private void CollapseAllMenuItem_Click(object sender, EventArgs e) {
            treeViewAdv1.CollapseAll();
        }

        private void ResetCompare() {
            _compare.RunWorkerAsync();
            compareProgressBar.Maximum = DirectoryModle.TotalFileCount;
            compareProgressBar.Value = 0;
        } // end ResetCompare

        private void StopConpare() {
            _compare.CancelAsync();
            compareProgressBar.Maximum = 0;
            compareProgressBar.Value = 0;
        } // end StopConpare

        private void CompareFiles(object sender, DoWorkEventArgs e) {
            foreach(var pair in DirectoryModle.DirectoryMap) {

            } // end foreach
        } // end CompareFiles

        private void ProgressChanged(object sender, ProgressChangedEventArgs e) {
        } // end ProgressChanged

        private void CompareCompleted(object sender, RunWorkerCompletedEventArgs e) {
        } // end CompareCompleted

        protected override void OnFormClosing(FormClosingEventArgs e) {
            base.OnFormClosing(e);
            _compare.Dispose();
        }
    }
}
