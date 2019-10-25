using CompareWindows.Data;
using CompareWindows.Modle;
using CompareWindows.Tool;
using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CompareWindows {
    public partial class SVNForm : Form {
        private SvnClient client;
        private SvnLogArgs logArgs;
        private string leftRootPath { get; set; }
        private string rightRootPath { get; set; }
        private FileNodeList leftNodeList { get; set; }
        private FileNodeList rightNodeList { get; set; }
        private Thread svnThread;

        public SVNForm() {
            InitializeComponent();
            client = new SvnClient();
            logArgs = new SvnLogArgs();
            logArgs.RetrieveAllProperties = false; //不检索所有属性
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
            listView1.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(OnListViewItemSelectionChanged);
            listView2.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(OnListViewItemSelectionChanged);
            listView1.DoubleClick += new EventHandler(OnDoubleClick);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            leftRootPath = comboBox1.Text;
            Reset();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            rightRootPath = comboBox2.Text;
            Reset();
        }

        private void comfirmBtn_Click(object sender, EventArgs e) {
            Reset();
        }

        private void OnListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs args) {
            if (args == null || !args.IsSelected) return;
            // end if
            string path = args.Item.SubItems[1].Text;
            ShowSvnLog(path, leftNodeList, listView3);
            ShowSvnLog(path, rightNodeList, listView4);
        } // end OnListView1ItemChanged

        private void OnDoubleClick(object sender, EventArgs args) {
            if (sender == null) return;
            ListView listView = sender as ListView;
            if (null == listView) return;
            // end if
            if (listView.SelectedItems.Count <= 0) return;
            // end if
            if (listView.SelectedItems[0].SubItems.Count <= 0) return;
            // end if
            string path = listView.SelectedItems[0].SubItems[1].Text;
            FileNode node;
            string leftFile = string.Empty;
            string rightFile = string.Empty;
            if (leftNodeList != null) {
                if (leftNodeList.pathToNodeMap.TryGetValue(path, out node)) {
                    leftFile = node.FullPath;
                } // end if
            } // end if
            if (rightNodeList != null) {
                if (rightNodeList.pathToNodeMap.TryGetValue(path, out node)) {
                    rightFile = node.FullPath;
                } // end if
            } // end if
            if (!Utility.IsNullOrImage(leftFile) || !Utility.IsNullOrImage(rightFile)) {
                MessageBox.Show("只能对比图片！");
                return;
            } // end if
            PictureForm form = new PictureForm();
            form.ShowPricture(leftFile, rightFile);
            form.ShowDialog();
        } // end OnDoubleClick

        private void ShowSvnLog(string path, FileNodeList nodeList, ListView listView) {
            if (string.IsNullOrEmpty(path) || nodeList == null || listView == null) return;
            // end if
            FileNode node;
            listView.BeginUpdate();
            listView.Items.Clear();
            if (nodeList.pathToNodeMap.TryGetValue(path, out node)) {
                if (node.fileSystemInfo == null || node.svnLogStatus == null) {
                    listView.EndUpdate();
                    return;
                } // end if
                foreach (var info in node.svnLogStatus) {
                    var item = listView.Items.Add(info.Revision.ToString());
                    item.SubItems.Add(info.Author);
                    item.SubItems.Add(info.Time.ToString());
                    item.SubItems.Add(info.LogMessage);
                } // end foreac
            } // end if
            listView.EndUpdate();
        } // end ShowSvnLog

        private void Reset() {
            string prefix = prefixInput.Text;
            int count = prefix.Length;
            HashSet<string> pathList = new HashSet<string>();
            string[] pathArr = pathListInput.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < pathArr.Length; ++i) {
                if (!pathArr[i].Contains(prefix)) {
                    MessageBox.Show("不包含前缀:" + pathArr[i]);
                    continue;
                } // end if
                string path = pathArr[i].Remove(0, count);
                path = path.Replace("/", "\\");
                if (!pathList.Add(path)) {
                    MessageBox.Show("重复输入：" + pathArr[i]);
                } // end if 
            } // end for
            if (string.IsNullOrEmpty(leftRootPath)) {
                leftNodeList = null;
            } else {
                leftNodeList = new FileNodeList(leftRootPath, pathList);
            } // end if
            if (string.IsNullOrEmpty(rightRootPath)) {
                rightNodeList = null;
            } else {
                rightNodeList = new FileNodeList(rightRootPath, pathList);
            } // end if
            if (leftNodeList != null && rightNodeList != null) {
                FileNodeList.CompareFileNodeList(leftNodeList, rightNodeList);
            } // end if
            ResetProgressBar();
            if (svnThread != null) svnThread.Abort();
            // end if
            svnThread = new Thread(() => AsynLoadSvnLog());
            svnThread.IsBackground = true;
            svnThread.Start();
            ResetShow(listView1, leftNodeList);
            ResetShow(listView2, rightNodeList);
        } // end Reset

        private void ResetShow(ListView listView, FileNodeList nodeList) {
            listView.BeginUpdate();
            listView.Items.Clear();
            if (null == nodeList) {
                listView.EndUpdate();
                return;
            } // end if
            // end if
            int index = 1;
            foreach (var pair in nodeList.pathToNodeMap) {
                ListViewItem item = new ListViewItem();
                item.Text = index.ToString();
                item.ForeColor = pair.Value.color;
                item.SubItems.Add(pair.Key);
                listView.Items.Add(item);
                ++index;
            } // end foreach
            listView.EndUpdate();
        } // end ResetShow

        private void button3_Click(object sender, EventArgs e) {
            if (leftNodeList == null || string.IsNullOrEmpty(leftRootPath) ||
                rightRootPath == null || string.IsNullOrEmpty(rightRootPath)) return;
            // end if
            if (MessageBox.Show("确定拷贝?", "拷贝", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) {
                return;
            } // end if
            StringBuilder pathStr = new StringBuilder(rightRootPath);
            foreach (var node in leftNodeList.pathToNodeMap) {
                if (node.Value.fileSystemInfo == null) continue;
                // end if
                pathStr.Length = rightRootPath.Length;
                pathStr.Append(node.Value.ParentRelativePath);
                if (!Directory.Exists(pathStr.ToString())) {
                    Directory.CreateDirectory(pathStr.ToString());
                } // end if
                pathStr.Length = rightRootPath.Length;
                pathStr.Append(node.Key);
                File.Copy(node.Value.FullPath, pathStr.ToString(), true);
            } // end foreach
            MessageBox.Show("拷贝完成!");
        }

        private void AsynLoadSvnLog() {
            LoadSvnLog(leftNodeList);
            LoadSvnLog(rightNodeList);
        } // end AsynLoadSvnLog

        private void LoadSvnLog(FileNodeList nodeList) {
            if (null == nodeList) return;
            // end if
            foreach (var pair in nodeList.pathToNodeMap) {
                Invoke(new BarDelegate(UpdateBar));
                if (pair.Value.fileSystemInfo == null || pair.Value.svnLogStatus != null) continue;
                // end if
                Collection<SvnLogEventArgs> status = null;
                try {
                    if (!client.GetLog(pair.Value.FullPath, logArgs, out status)) {
                        status = null;
                    } // end if
                    pair.Value.svnLogStatus = status;
                } catch (Exception) {
                } // end try
            } // end foreach
        } // end LoadSvnLog

        private delegate void BarDelegate();
        private void UpdateBar() {
            progressBar1.Value++;
            label2.Text = progressBar1.Value + "/" + progressBar1.Maximum;
            if (progressBar1.Value == progressBar1.Maximum) {
                label2.Text = "Svn加载完毕";
                progressBar1.Value = 0;
            } // end if
        } // end UpdateBar

        private void ResetProgressBar() {
            int maximum = 0;
            if (leftNodeList != null) maximum += leftNodeList.pathToNodeMap.Count;
            // end if
            if (rightNodeList != null) maximum += rightNodeList.pathToNodeMap.Count;
            // end if
            progressBar1.Value = 0;
            progressBar1.Maximum = maximum;
            label2.Text = progressBar1.Value + "/" + progressBar1.Maximum;
        }
    }
}
