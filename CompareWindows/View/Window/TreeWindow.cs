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
        private TreeModel treeModle;
        private SvnListModel svnListModle;

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
            toLoadSvnMenuItem.Checked = Global.LoadSvnLog;
            showSameMenuItem.Checked = Global.ShowSame;
            SetSvnStatus(Global.LoadSvnLog);
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
            compareProgressBar.Maximum = e.MaxiMum;
            compareProgressBar.Value = e.Current;
            progressLabel.Text = e.Percentagep;
        } // end OnProgressChanged

        private void OnProgressCompleted(object sender, ProgressEventArgs e) {
            statusLabel.Text = "对比完成";
            statusStrip1.Items[1].Visible = false;
            statusStrip1.Items[2].Visible = false;
        } // end OnProgressCompleted

        private void OnProgressStart(object sender, ProgressEventArgs e) {
            statusLabel.Text = "正在对比";
            compareProgressBar.Maximum = e.MaxiMum;
            compareProgressBar.Value = e.Current;
            progressLabel.Text = e.Percentagep;
            statusStrip1.Items[1].Visible = true;
            statusStrip1.Items[2].Visible = true;
        } // end OnProgressStart

        protected override void OnFormClosing(FormClosingEventArgs e) {
            if (treeModle != null) {
                UnBindProgressEvent();
            } // end if
            base.OnFormClosing(e);
        }

        private void ResetModle(string leftRoot, string rightRoot) {
            statusLabel.Text = "就绪";
            SetSvnStatus(Global.LoadSvnLog);
            statusStrip1.Items[1].Visible = false;
            statusStrip1.Items[2].Visible = false;
            statusStrip1.Items[5].Visible = false;
            statusStrip1.Items[6].Visible = false;
            UnBindProgressEvent();
            treeModle = new TreeModel(leftRoot, rightRoot, Global.ShowSame);
            svnListModle = new SvnListModel(leftRoot, rightRoot);
            svnListModle.ToLoad = Global.LoadSvnLog;
            BindProgressEvent();
            treeViewAdv1.Model = treeModle;
            svnListBox1.Model = svnListModle;
        } // end ResetModle

        private void OnSelectionChanged(object sender, EventArgs e) {
            svnListBox1.Clear();
            if (treeViewAdv1.SelectedNode == null) return;
            // end if
            BaseItem item = treeViewAdv1.SelectedNode.Tag as BaseItem;
            if (item == null) return;
            // end if
            svnListModle.CurrentPaht = item.ItemPath;
        } // end OnSelectionChanged

        private void BindProgressEvent() {
            if (treeModle != null) {
                treeModle.progress.ProgressStart += OnProgressStart;
                treeModle.progress.ProgressChanged += OnProgressChanged;
                treeModle.progress.ProgressCompleted += OnProgressCompleted;
            } // end if
            if (svnListModle != null) {
                svnListModle.progress.ProgressStart += OnSvnProgressStart;
                svnListModle.progress.ProgressChanged += OnSvnProgressChanged;
                svnListModle.progress.ProgressCompleted += OnSvnProgressCompleted;
            } // end if
        } // end BindProgressEvent

        private void UnBindProgressEvent() {
            if (treeModle != null) {
                treeModle.progress.ProgressStart -= OnProgressStart;
                treeModle.progress.ProgressChanged -= OnProgressChanged;
                treeModle.progress.ProgressCompleted -= OnProgressCompleted;
            } // end if
            if (svnListModle != null) {
                svnListModle.progress.ProgressStart -= OnSvnProgressStart;
                svnListModle.progress.ProgressChanged -= OnSvnProgressChanged;
                svnListModle.progress.ProgressCompleted -= OnSvnProgressCompleted;
            } // end if
        } // end UnBindProgressEvent

        private void toLoadSvnMenuItem_Click(object sender, EventArgs e) {
            Global.LoadSvnLog = !Global.LoadSvnLog;
            SetSvnStatus(Global.LoadSvnLog);
            toLoadSvnMenuItem.Checked = Global.LoadSvnLog;
            if (svnListModle != null) svnListModle.ToLoad = Global.LoadSvnLog;
            // end if
        }

        private void OnSvnProgressChanged(object sender, ProgressEventArgs e) {
            svnProgressBar.Maximum = e.MaxiMum;
            svnProgressBar.Value = e.Current;
            svnProgressLabel.Text = e.Percentagep;
        } // end OnSvnProgressChanged

        private void OnSvnProgressCompleted(object sender, ProgressEventArgs e) {
            svnStatusLabel.Text = "Svn加载完成";
            statusStrip1.Items[5].Visible = false;
            statusStrip1.Items[6].Visible = false;
        } // end OnSvnProgressCompleted

        private void OnSvnProgressStart(object sender, ProgressEventArgs e) {
            svnStatusLabel.Text = "正在加载Svn";
            svnProgressBar.Maximum = e.MaxiMum;
            svnProgressBar.Value = e.Current;
            svnProgressLabel.Text = e.Percentagep;
            statusStrip1.Items[5].Visible = true;
            statusStrip1.Items[6].Visible = true;
        } // end OnSvnProgressStart

        private void SetSvnStatus(bool toLoad) {
            if (toLoad) {
                svnStatusLabel.Text = "等待加载Svn";
            } else {
                svnStatusLabel.Text = "停止加载Svn";
            } // end if
        }

        private void showSameMenuItem_Click(object sender, EventArgs e) {
            Global.ShowSame = !Global.ShowSame;
            ResetModle(leftRoot, rightRoot);
        }
    }
}
