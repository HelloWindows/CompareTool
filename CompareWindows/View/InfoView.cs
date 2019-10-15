using CompareWindows.Data;
using CompareWindows.Modle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.View {
    public class InfoView {
        public TreeView treeView { get; private set; }
        public ContextMenuStrip contextMenuStrip { get; private set; }
        public Dictionary<TreeNode, InfoNode> nodeToInfoMap { get; private set; }

        public InfoView(TreeView treeView, ContextMenuStrip contextMenuStrip) {
            this.treeView = treeView;
            this.contextMenuStrip = contextMenuStrip;
            nodeToInfoMap = new Dictionary<TreeNode, InfoNode>();
        } // end InfoView
        /// <summary>
        /// 刷新展示树视图
        /// </summary>
        /// <param name="treeModle"> 树模型 </param>
        public void RefreshDisplay(TreeModle treeModle) {
            treeView.Nodes.Clear();
            nodeToInfoMap.Clear();
            if (null == treeModle) return;
            // end if
            if (null != treeModle.rootDirectoryNode && !treeModle.rootDirectoryNode.IsFilter) {
                treeView.Nodes.Add(CreateTreeNode(treeModle.rootDirectoryNode));
            } // end if
            treeView.ExpandAll();
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
            } // end foreach
            treeNode.ContextMenuStrip = contextMenuStrip;
            treeNode.ForeColor = directoryNode.color;
            nodeToInfoMap.Add(treeNode, directoryNode);
            return treeNode;
        } // end CreateTreeNode
    } // end class InfoView
} // end CompareWindows.View
