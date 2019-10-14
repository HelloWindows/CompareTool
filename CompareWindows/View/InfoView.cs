using CompareWindows.Modle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.View {
    public class InfoView {
        private TreeView treeView;
        private ContextMenuStrip contextMenuStrip;

        private string rootPath;
        private DirectoryNode directoryNode;
        private Dictionary<string, InfoNode> pathToInfoMap;
        private Dictionary<TreeNode, InfoNode> nodeToInfoMap;

        public InfoView(TreeView treeView, ContextMenuStrip contextMenuStrip) {
            this.treeView = treeView;
            this.contextMenuStrip = contextMenuStrip;
            pathToInfoMap = new Dictionary<string, InfoNode>();
            nodeToInfoMap = new Dictionary<TreeNode, InfoNode>();
        } // end InfoView

        public void RefreshDisplay(string path) {
            treeView.Nodes.Clear();
            pathToInfoMap.Clear();
            nodeToInfoMap.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            rootPath = rootDirectoryInfo.FullName;
            directoryNode = DirectoryNode.CreateDirectoryNode(rootPath, rootDirectoryInfo);
            treeView.Nodes.Add(CreateTreeNode(rootPath, directoryNode));
            DirectoryNode.FilterDirectory(directoryNode);
            foreach (var pair in nodeToInfoMap) {
                if (!pair.Value.IsShow) {
                    pair.Key.Remove();
                } // end if
            } // end forach
        } // end RefreshDisplay

        private TreeNode CreateTreeNode(string rootName, DirectoryNode directoryNode) {
            TreeNode treeNode = new TreeNode(directoryNode.Name);
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                treeNode.Nodes.Add(CreateTreeNode(rootName, directory));
            } // end foreach
            foreach (var file in directoryNode.GetFileNodes()) {
                TreeNode node = new TreeNode(file.Name);
                node.ContextMenuStrip = contextMenuStrip;
                treeNode.Nodes.Add(node);
                nodeToInfoMap.Add(node, file);
                pathToInfoMap.Add(file.RelativePath, file);
            } // end foreach
            treeNode.ContextMenuStrip = contextMenuStrip;
            nodeToInfoMap.Add(treeNode, directoryNode);
            pathToInfoMap.Add(directoryNode.RelativePath, directoryNode);
            return treeNode;
        } // end CreateTreeNode
    } // end class InfoView
} // end CompareWindows.View
