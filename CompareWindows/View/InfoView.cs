using CompareWindows.Config;
using CompareWindows.Data;
using CompareWindows.Modle;
using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CompareWindows.View {
    public class InfoView {
        public TreeView treeView { get; private set; }
        public ListBox listBox { get; private set; }
        public ContextMenuStrip contextMenuStrip { get; private set; }
        public Dictionary<TreeNode, InfoNode> nodeToInfoMap { get; private set; }
        public Dictionary<string, TreeNode> pathToNodeMap { get; private set; }
        private SvnClient client;
        private SvnLogArgs logArgs;
        private Thread showSvnThread;
        public InfoView otherView { get; set; }
        private TreeNode lastOtherNode;
        public Action<string> OnTopNodeChanged;
        private TreeNode topNode;
        public TreeNode TopNode {
            get { return topNode; }
            set {
                if (topNode == null) {
                    topNode = value;
                } else if (topNode != value) {
                    topNode = value;
                    InfoNode info;
                    if (nodeToInfoMap.TryGetValue(topNode, out info)) {
                        if (OnTopNodeChanged != null) {
                            OnTopNodeChanged(info.RelativePath);
                        } // end if
                    } // end if
                } // end if
            } // end set
        } // end TopNode

        public InfoView(TreeView treeView, ListBox listBox, ContextMenuStrip contextMenuStrip) {
            this.treeView = treeView;
            this.listBox = listBox;
            this.contextMenuStrip = contextMenuStrip;
            client = new SvnClient();
            logArgs = new SvnLogArgs();
            logArgs.RetrieveAllProperties = false; //不检索所有属性
            nodeToInfoMap = new Dictionary<TreeNode, InfoNode>();
            pathToNodeMap = new Dictionary<string, TreeNode>();
            this.treeView.AfterSelect += new TreeViewEventHandler(OnAfterSelect);
            this.treeView.LostFocus += new EventHandler(OnLostFocus);
            this.treeView.AfterExpand += new TreeViewEventHandler(OnAfterExpand);
            this.treeView.AfterCollapse += new TreeViewEventHandler(OnAfterCollapse);
            this.treeView.DrawNode += new DrawTreeNodeEventHandler(OnDrawNode);
            treeView.ShowLines = false;
            treeView.FullRowSelect = true;
        } // end InfoView
        /// <summary>
        /// 刷新展示树视图
        /// </summary>
        /// <param name="treeModle"> 树模型 </param>
        public void RefreshDisplay(TreeModle2 treeModle) {
            treeView.Nodes.Clear();
            nodeToInfoMap.Clear();
            pathToNodeMap.Clear();
            if (null == treeModle) return;
            // end if
            if (null != treeModle.rootDirectoryNode && !treeModle.rootDirectoryNode.IsFilter) {
                treeView.Nodes.Add(CreateTreeNode(treeModle.rootDirectoryNode));
            } // end if
        } // end RefreshDisplay
        /// <summary>
        /// 创建树节点
        /// </summary>
        /// <param name="directoryNode"> 文件夹节点 </param>
        /// <returns> 树节点 </returns>
        private TreeNode CreateTreeNode(DirectoryNode directoryNode) {
            TreeNode treeNode = new TreeNode(directoryNode.Name);
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                if (directory.IsFilter || !Global.ShowSame && directory.IsSame) continue;
                // end if
                treeNode.Nodes.Add(CreateTreeNode(directory));
            } // end foreach
            foreach (var file in directoryNode.GetFileNodes()) {
                if (file.IsFilter || !Global.ShowSame && file.IsSame) continue;
                // end if
                TreeNode node = new TreeNode(file.Name);
                node.ContextMenuStrip = contextMenuStrip;
                node.ForeColor = file.color;
                treeNode.Nodes.Add(node);
                nodeToInfoMap.Add(node, file);
                pathToNodeMap.Add(file.RelativePath, node);
            } // end foreach
            treeNode.ContextMenuStrip = contextMenuStrip;
            treeNode.ForeColor = directoryNode.color;
            nodeToInfoMap.Add(treeNode, directoryNode);
            pathToNodeMap.Add(directoryNode.RelativePath, treeNode);
            return treeNode;
        } // end CreateTreeNode

        public void SetTopNode(string path) {
            TreeNode topNode;
            if (pathToNodeMap.TryGetValue(path, out topNode)) {
                treeView.TopNode = topNode;
            } // end if
        } // end SetTopNode

        private void OnAfterSelect(object sender, TreeViewEventArgs e) {
            TreeNode node = treeView.SelectedNode;
            InfoNode info;
            if (nodeToInfoMap.TryGetValue(node, out info)) {
                if (null != otherView) {
                    TreeNode otherNode;
                    if (otherView.pathToNodeMap.TryGetValue(info.RelativePath, out otherNode)) {
                        if(lastOtherNode != null) lastOtherNode.BackColor = Define.NotSelectedColor;
                        // end if
                        lastOtherNode = otherNode;
                        otherNode.BackColor = Define.SelectedColor;
                        otherView.ShowSvnLog(otherNode);
                    } else {
                        if (lastOtherNode != null) lastOtherNode.BackColor = Define.NotSelectedColor;
                        // end if
                    } // end if
                } // end if
                ShowSvnLog(info);
            } // end if
        } // end OnAfterSelect

        private void OnLostFocus(object sender, EventArgs e) {
            if (lastOtherNode != null) lastOtherNode.BackColor = Define.NotSelectedColor;
        } // end OnLostFocus

        private void OnAfterExpand(object sender, TreeViewEventArgs e) {
            if (e == null) return;
            // end if
            InfoNode info;
            if (nodeToInfoMap.TryGetValue(e.Node, out info)) {
                if (null != otherView) {
                    TreeNode otherNode;
                    if (otherView.pathToNodeMap.TryGetValue(info.RelativePath, out otherNode)) {
                        otherNode.Expand();
                    } // end if
                } // end if
            } // end if
        } // end OnAfterExpand

        private void OnAfterCollapse(object sender, TreeViewEventArgs e) {
            if (e == null) return;
            // end if
            InfoNode info;
            if (nodeToInfoMap.TryGetValue(e.Node, out info)) {
                if (null != otherView) {
                    TreeNode otherNode;
                    if (otherView.pathToNodeMap.TryGetValue(info.RelativePath, out otherNode)) {
                        otherNode.Collapse();
                    } // end if
                } // end if
            } // end if
        } // end OnAfterCollapse

        private void OnDrawNode(object sender, DrawTreeNodeEventArgs e) {
            TopNode = treeView.TopNode;
        } // end OnDrawNode

        public void ShowSvnLog(TreeNode node) {
            InfoNode info;
            if (nodeToInfoMap.TryGetValue(node, out info)) {
                ShowSvnLog(info);
            } // end if
        } // end ShowSvnLog

        private void ShowSvnLog(InfoNode info) {
            if (!Global.LoadSvnLog) return;
            // end if
            if (null != info.svnLogStatus) {
                Collection<SvnLogEventArgs> status = info.svnLogStatus;
                DisplaySvnLog(status);
            } else {
                StartAsynLoadSvnLog(info);
            } // end if
        } // end ShowSvnLog


        private void StartAsynLoadSvnLog(InfoNode info) {
            if (null != showSvnThread) showSvnThread.Abort();
            // end if
            if (null != client) {
                client.Dispose();
            } // end if
            client = new SvnClient();
            listBox.Items.Clear();
            listBox.Items.Add("加载中...");
            showSvnThread = new Thread(() => AsynLoadSvnLog(info, LoadSvnLogBackCall));
            showSvnThread.IsBackground = true;
            showSvnThread.Start();
        } // end StartAsynLoadSvnLog

        private void AsynLoadSvnLog(InfoNode info, Action<InfoNode, Collection<SvnLogEventArgs>> backCall) {
            Collection<SvnLogEventArgs> status = null;
            try {
                if (!client.GetLog(info.FullPath, logArgs, out status)) {
                    status = null;
                } // end if
            } catch(Exception) {
                //MessageBox.Show(ex.Message);
            } // end try
            backCall(info, status);
        } // end AsynLoadSvnLog

        private delegate void SvnLogCallBack(Collection<SvnLogEventArgs> status);
        private void LoadSvnLogBackCall(InfoNode info, Collection<SvnLogEventArgs> status) {
            if (null != info) info.svnLogStatus = status;
            // end if
            DisplaySvnLog(status);
        } // end LoadSvnLogBackCall

        private void DisplaySvnLog(Collection<SvnLogEventArgs> status) {
            if (listBox.InvokeRequired) {
                listBox.Invoke(new SvnLogCallBack(DisplaySvnLog), status);
            } else {
                listBox.Items.Clear();
                if (null == status) {
                    listBox.Items.Add("加载失败");
                } else {
                    StringBuilder str = new StringBuilder();
                    foreach (var item in status) {
                        str.Length = 0;
                        str.Append(item.Time);
                        str.Append("  ");
                        str.Append(item.Author);
                        str.Append("  ");
                        str.Append(item.LogMessage);
                        listBox.Items.Add(str.ToString());
                    } // end foreac
                } // end if
            } // end if
        } // end DisplayTreeView

        public void SetSameNode(string path) {
            TreeNode node;
            if (pathToNodeMap.TryGetValue(path, out node)) {
                if (!Global.ShowSame) {
                    node.Remove();
                } else {
                    SetSameNodesColor(node);
                }// end if
            } // end if
        } // end SetSameNode

        public void SetEmptyNode(string path) {
            TreeNode node;
            if (pathToNodeMap.TryGetValue(path, out node)) {
                node.Text = string.Empty;
            } // end if
        } // end SetEmptyNode

        public void SetSpecialNode(string path, string name) {
            TreeNode node;
            if (pathToNodeMap.TryGetValue(path, out node)) {
                node.Text = name;
                node.ForeColor = Define.SpecialColor;
            } // end if
        } // end SetSpecialNode

        private void SetSameNodesColor(TreeNode node) {
            InfoNode info;
            if (nodeToInfoMap.TryGetValue(node, out info)) {
                node.Text = info.Name;
            } // end if
            node.ForeColor = Define.SameColor;
            foreach (TreeNode item in node.Nodes) {
                SetSameNodesColor(item);
            } // end foreach
        } // end SetSameNodes
    } // end class InfoView
} // end CompareWindows.View
