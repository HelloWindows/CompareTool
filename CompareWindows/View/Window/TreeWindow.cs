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
using CompareWindows.Tool;

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

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Application.Exit();
        }

        private void browerBtn1_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                if (leftRoot == path) return;
                // end if
                comboBox1.Text = path;
                comboBox1.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath1(path);
                leftRoot = path;
                if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                    ResetModle(leftRoot, rightRoot);
                } // end if
            } // end if
        }

        private void browerBtn2_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                if (rightRoot == path) return;
                // end if
                comboBox2.Text = path;
                comboBox2.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath2(path);
                rightRoot = path;
                if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                    ResetModle(leftRoot, rightRoot);
                } // end if
            } // end if
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            if (leftRoot == comboBox1.Text) return;
            // end if
            leftRoot = comboBox1.Text;
            if (!string.IsNullOrEmpty(leftRoot) && !string.IsNullOrEmpty(rightRoot)) {
                ResetModle(leftRoot, rightRoot);
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            if (rightRoot == comboBox2.Text) return;
            // end if
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
            statusStrip1.Items[9].Visible = false;
            statusStrip1.Items[10].Visible = false;
            UnBindProgressEvent();
            if (treeModle != null) treeModle.Dispose();
            // end if
            if (svnListModle != null) svnListModle.Dispose();
            // end if
            treeModle = new TreeModel(leftRoot, rightRoot);
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

                treeModle.moveProgress.ProgressStart += OnMoveProgressStart;
                treeModle.moveProgress.ProgressChanged += OnMoveProgressChanged;
                treeModle.moveProgress.ProgressCompleted += OnMoveProgressCompleted;
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

                treeModle.moveProgress.ProgressStart -= OnMoveProgressStart;
                treeModle.moveProgress.ProgressChanged -= OnMoveProgressChanged;
                treeModle.moveProgress.ProgressCompleted -= OnMoveProgressCompleted;
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

        private void OnMoveProgressChanged(object sender, ProgressEventArgs e) {
            moveProgressBar.Maximum = e.MaxiMum;
            moveProgressBar.Value = e.Current;
            moveProgressLabel.Text = e.Percentagep;
        } // end OnSvnProgressChanged

        private void OnMoveProgressCompleted(object sender, ProgressEventArgs e) {
            moveStatusLabel.Text = "复制完成";
            statusStrip1.Items[9].Visible = false;
            statusStrip1.Items[10].Visible = false;
        } // end OnSvnProgressCompleted

        private void OnMoveProgressStart(object sender, ProgressEventArgs e) {
            moveStatusLabel.Text = "正在复制";
            moveProgressBar.Maximum = e.MaxiMum;
            moveProgressBar.Value = e.Current;
            moveProgressLabel.Text = e.Percentagep;
            statusStrip1.Items[9].Visible = true;
            statusStrip1.Items[10].Visible = true;
        } // end OnSvnProgressStart

        private void SetSvnStatus(bool toLoad) {
            if (toLoad) {
                svnStatusLabel.Text = "等待加载Svn";
            } else {
                svnStatusLabel.Text = "停止加载Svn";
            } // end if
        }

        private void showSameMenuItem_Click(object sender, EventArgs e) {
            if (treeModle == null) {
                MessageBox.Show("请先选择对比的文件夹");
                return;
            } // end if
            if (treeModle.IsCompared == false) {
                MessageBox.Show("请开启比较文件，并等待文件对比完成");
                return;
            } // end if
            Global.ShowSame = !Global.ShowSame;
            showSameMenuItem.Checked = Global.ShowSame;
            treeViewAdv1.Model = null;
            treeModle.RefreshModle();
            treeViewAdv1.Model = treeModle;
        }

        private void CompareMenuItem_Click(object sender, EventArgs e) {
            if (treeModle == null) {
                MessageBox.Show("请先选择对比的文件夹");
                return;
            } // end if
            treeModle.StartCompare();
        }

        private void FilterMenuItem_Click(object sender, EventArgs e) {
            FilterWindow form = new FilterWindow();
            AddOwnedForm(form);
            form.FormClosed += OnFilterWindowClose;
            form.ShowDialog();
        }


        private void OnFilterWindowClose(object sender, FormClosedEventArgs e) {
            FilterWindow form = sender as FilterWindow;
            if (form != null) {
                form.FormClosed -= OnFilterWindowClose;
            } // end if
            if (treeModle == null) return;
            treeViewAdv1.Model = null;
            treeModle.RefreshModle();
            treeViewAdv1.Model = treeModle;
        } // end OnFilterWindowClose

        private void CopyToRightMenuItem_Click(object sender, EventArgs e) {
            MoveFiles(MoveType.ToRight);
        }

        private void CopyToLeftMenuItem_Click(object sender, EventArgs e) {
            MoveFiles(MoveType.ToLeft);
        }

        private void MoveFiles(MoveType type) {
            if (MessageBox.Show("确定拷贝文件？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;
            // end if
            List<string> list = new List<string>();
            foreach (var node in treeViewAdv1.SelectedNodes) {
                BaseItem item = node.Tag as BaseItem;
                if (item == null) continue;
                // end if
                if (item is FileItem) {
                    list.Add(item.ItemPath);
                } // end if
            } // end foreach
            if (list.Count <= 0) return;
            // end if 
            if (treeModle.MoveFiles(type, list)) return;
            // end if
            MessageBox.Show("正在复制其他文件，请等待复制其他文件完毕");
        } // end MoveFiles

        private void CompareFileMenuItem_Click(object sender, EventArgs e) {
            if (treeViewAdv1.SelectedNode == null) return;
            // end if
            BaseItem item = treeViewAdv1.SelectedNode.Tag as BaseItem;
            if (item == null) return;
            // end if
            string leftPicture = leftRoot + item.ItemPath;
            string rightPicture = rightRoot + item.ItemPath;
            if (Utility.IsNullOrImage(leftPicture) && Utility.IsNullOrImage(rightPicture)) {
                WatchWindow win = new WatchWindow();
                AddOwnedForm(win);
                win.Show();
                win.ShowPricture(leftPicture, rightPicture);
            } else {
                MessageBox.Show("只能对比图片");
            }// end if
        }

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            float[] widthArr = new float[6];
            widthArr[0] = widthArr[3] = 230f / 760f;
            widthArr[1] = widthArr[4] = 60f / 760f;
            widthArr[2] = widthArr[5] = 90f / 760f;
            for (int i = 0; i < widthArr.Length; ++i) {
                treeViewAdv1.Columns[i].Width = (int)(widthArr[i] * treeViewAdv1.Width);
            } // end for
        }
    }
}
