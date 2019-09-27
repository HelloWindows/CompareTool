using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CompareWindows {
    public partial class MainForm : Form {

        private Dictionary<TreeNode, FileSystemInfo> nodeToInfoDict;

        public MainForm() {
            nodeToInfoDict = new Dictionary<TreeNode, FileSystemInfo>();
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox1.Text = path;
                comboBox1.Items.Add(path);
                ListDirectory(treeView1, path);
            } // end if
        }

        private void button4_Click(object sender, System.EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox2.Text = path;
                comboBox2.Items.Add(path);
                ListDirectory(treeView2, path);
            } // end if
        }

        private void ListDirectory(TreeView treeView, string path) {
            treeView.Nodes.Clear();
            nodeToInfoDict.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, LeftNodeMenuStrip));
        } // end ListDirectory

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, ContextMenuStrip contextMenuStrip) {
            if (null == directoryInfo) throw new System.Exception();
            // end if
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories()) {
                directoryNode.Nodes.Add(CreateDirectoryNode(directory, contextMenuStrip));
            } // end foreach
            foreach (var file in directoryInfo.GetFiles()) {
                TreeNode node = new TreeNode(file.Name);
                node.ContextMenuStrip = contextMenuStrip;
                directoryNode.Nodes.Add(node);
                nodeToInfoDict.Add(node, file);
            } // end foreach
            directoryNode.ContextMenuStrip = contextMenuStrip;
            nodeToInfoDict.Add(directoryNode, directoryInfo);
            return directoryNode;
        } // end CreateDirectoryNode

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

        private void CopyToolStripMenuItem_Click(object sender, System.EventArgs e) {
            TreeNode currentNode = treeView1.SelectedNode;
            FileSystemInfo info;
            if (nodeToInfoDict.TryGetValue(currentNode, out info)) {
                string[] file = new string[1];
                file[0] = info.FullName;
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.FileDrop, file);
                Clipboard.SetDataObject(dataObject, true);
            } else {
                throw new System.Exception();
            } // end if
        }

        private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e) {

        }
    }
}
