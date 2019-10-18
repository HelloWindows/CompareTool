using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using CompareWindows.Data;
using CompareWindows.Modle;
using CompareWindows.View;
using System.Threading;

namespace CompareWindows {
    public partial class MainForm : Form {
        private TreeModle leftTreeModle;
        private TreeModle rightTreeModle;
        private InfoView leftInfoView;
        private InfoView rightInfoView;
        private InfoMenuView leftMenuView;
        private InfoMenuView rightMenuView;
        private Thread displayThread;

        public MainForm() {
            InitializeComponent();
            Text = GetType().ToString();
            ShowSameMenuItem.Checked = Global.ShowSame;
            LoadSVNLogMenuItem.Checked = Global.LoadSvnLog;
            leftTreeModle = null;
            rightTreeModle = null;
            leftInfoView = new InfoView(treeView1, listBox1, LeftNodeMenuStrip);
            rightInfoView = new InfoView(treeView2, listBox2, RightNodeMenuStrip);
            leftInfoView.otherView = rightInfoView;
            rightInfoView.otherView = leftInfoView;
            leftMenuView = new InfoMenuView(leftInfoView, rightInfoView, LeftNodeMenuStrip);
            rightMenuView = new InfoMenuView(rightInfoView, leftInfoView, RightNodeMenuStrip);
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
            progressTime.Tick += new EventHandler(ShowProgressBar);
            progressTime.Interval = 1000;
        }

        private void ShowProgressBar(object myObject, EventArgs myEventArgs) {
            int current = DataManager.Instance.compareProgressData.current;
            int maximum = DataManager.Instance.compareProgressData.maxProgress;
            compareProgressBar.Value = current;
            if (maximum > 0) {
                float progress = (float)current / maximum * 100f;
                progressLabel.Text = progress.ToString("f2") + "%";
            } // end if
        } // end ShowProgressBar
        /// <summary>
        /// 检测过滤
        /// </summary>
        public void DoDetectFilter() {
            if (null != leftTreeModle) {
                leftTreeModle.DoDetectFilter();
            } // end if
            if (null != rightTreeModle) {
                rightTreeModle.DoDetectFilter();
            } // end if
            leftInfoView.RefreshDisplay(leftTreeModle);
            rightInfoView.RefreshDisplay(rightTreeModle);
        } // end DoDetectFilter

        private List<TreeNode> checkedNodes = new List<TreeNode>();
        private void RemoveCheckedNodes(TreeNodeCollection nodes) {
            foreach (TreeNode node in nodes) {
                if (node.Checked) {
                    checkedNodes.Add(node);
                } else {
                    RemoveCheckedNodes(node.Nodes);
                } // end if
            } // end foreach
            foreach (TreeNode checkedNode in checkedNodes) {
                nodes.Remove(checkedNode);
            } // end foreach
        } // end RemoveCheckedNodes

        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox1.Text = path;
                comboBox1.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath1(path);
                StartAsynRefeashDisplay(path, null);
            } // end if
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            StartAsynRefeashDisplay(comboBox1.Text, null);
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog2.SelectedPath;
                comboBox2.Text = path;
                comboBox2.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath2(path);
                StartAsynRefeashDisplay(null, path);
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            StartAsynRefeashDisplay(null, comboBox2.Text);
        }

        private string GetPath(string fullName, string rootName) {
            string path = fullName.Substring(rootName.Length);
            return path;
        } // end GetPath

        private void ExpandAllMenuItem_Click(object sender, EventArgs e) {
            treeView1.ExpandAll();
            treeView2.ExpandAll();
        }

        private void CollapseAllMenuItem_Click(object sender, EventArgs e) {
            treeView1.CollapseAll();
            treeView2.CollapseAll();
        }

        private void ShowSameMenuItem_Click(object sender, EventArgs e) {
            Global.ShowSame = !Global.ShowSame;
            ShowSameMenuItem.Checked = Global.ShowSame;
            leftInfoView.RefreshDisplay(leftTreeModle);
            rightInfoView.RefreshDisplay(rightTreeModle);
        }

        private void ShowFilterFormMenuItem_Click(object sender, EventArgs e) {
            FilterForm form = new FilterForm(this);
            AddOwnedForm(form);
            form.ShowDialog();
        }

        private void LoadSVNLogMenuItem_Click(object sender, EventArgs e) {
            Global.LoadSvnLog = !Global.LoadSvnLog;
            LoadSVNLogMenuItem.Checked = Global.LoadSvnLog;
            string log = Global.LoadSvnLog ? "..." : "停止加载Svn";
            listBox1.Items.Clear();
            listBox1.Items.Add(log);
            listBox2.Items.Clear();
            listBox2.Items.Add(log);
        }

        private void StartAsynRefeashDisplay(string leftPath, string rightPath) {
            if (null != displayThread) displayThread.Abort();
            // end if
            if (!string.IsNullOrEmpty(leftPath)) {
                leftTreeModle = new TreeModle(leftPath);
            } // end if
            if (!string.IsNullOrEmpty(rightPath)) {
                rightTreeModle = new TreeModle(rightPath);
            } // end if
            leftMenuView.ResetPath(leftTreeModle, rightTreeModle);
            rightMenuView.ResetPath(rightTreeModle, leftTreeModle);
            int maxProgress = 0;
            if (null != leftTreeModle) {
                maxProgress = leftTreeModle.InfoCount;
            } // end if
            progressTime.Stop();
            DataManager.Instance.compareProgressData.Reset();
            DataManager.Instance.compareProgressData.SetMaxProgress(maxProgress);
            compareProgressBar.Maximum = maxProgress;
            if (maxProgress == 0) {
                progressLabel.Text = "100%";
            } else {
                progressLabel.Text = "0%";
            } // end if
            DisplayTreeView();
            progressTime.Start();
            compareStateLabel.Text = "正在比对...";
            displayThread = new Thread(() => AsynRefeashDispaly(DisplayTreeView));
            displayThread.IsBackground = true;
            displayThread.Start();
        } // end StartAsynRefeashDisplay

        private void AsynRefeashDispaly(Action backCall) {
            TreeModle.CompareModle(leftTreeModle, rightTreeModle);
            if (null != backCall) backCall();
            // end if
        } // end RefeashDispaly

        private delegate void DisplayCallBack();
        private void DisplayTreeView() {
            if (InvokeRequired) {
                Invoke(new DisplayCallBack(DisplayTreeView));
            } else {
                leftInfoView.RefreshDisplay(leftTreeModle);
                rightInfoView.RefreshDisplay(rightTreeModle);
                progressTime.Stop();
                progressLabel.Text = "100%";
                compareStateLabel.Text = "比对完成";
                compareProgressBar.Value = compareProgressBar.Maximum;
            } // end if
        } // end DisplayTreeView
    }
}
