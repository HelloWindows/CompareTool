using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CompareWindows.Data;

namespace CompareWindows {
    public partial class MainForm : Form {

        private Dictionary<TreeNode, FileSystemInfo> nodeToInfoDict1;
        private Dictionary<TreeNode, FileSystemInfo> nodeToInfoDict2;
        private Dictionary<string, FileSystemInfo> pathToInfoDict1;
        private Dictionary<string, FileSystemInfo> pathToInfoDict2;
        private string rootName1 = string.Empty;
        private string rootName2 = string.Empty;

        public MainForm() {
            nodeToInfoDict1 = new Dictionary<TreeNode, FileSystemInfo>();
            nodeToInfoDict2 = new Dictionary<TreeNode, FileSystemInfo>();
            pathToInfoDict1 = new Dictionary<string, FileSystemInfo>();
            pathToInfoDict2 = new Dictionary<string, FileSystemInfo>();
            InitializeComponent();
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
        }

        private void button1_Click(object sender, System.EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox1.Text = path;
                comboBox1.Items.Insert (0, path);
                DataManager.Instance.comboBoxData.SelectPath1(path);
                ListDirectory1(path);
            } // end if
        }

        private void ListDirectory1(string path) {
            treeView1.Nodes.Clear();
            treeView2.Nodes.Clear();
            nodeToInfoDict1.Clear();
            nodeToInfoDict2.Clear();
            pathToInfoDict1.Clear();
            pathToInfoDict2.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            string rootName = rootDirectoryInfo.FullName;
            treeView1.Nodes.Add(CreateDirectoryNode1(rootName, rootDirectoryInfo, LeftNodeMenuStrip));
            string temp = comboBox2.Text;
        } // end ListDirectory1

        private TreeNode CreateDirectoryNode1(string rootName, DirectoryInfo directoryInfo, ContextMenuStrip contextMenuStrip) {
            if (null == directoryInfo) throw new System.Exception();
            // end if
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories()) {
                directoryNode.Nodes.Add(CreateDirectoryNode1(rootName, directory, contextMenuStrip));
            } // end foreach
            foreach (var file in directoryInfo.GetFiles()) {
                TreeNode node = new TreeNode(file.Name);
                node.ContextMenuStrip = contextMenuStrip;
                directoryNode.Nodes.Add(node);
                nodeToInfoDict1.Add(node, file);
                string subPath = GetPath(file.FullName, rootName);
                pathToInfoDict1.Add(subPath, file);
            } // end foreach
            directoryNode.ContextMenuStrip = contextMenuStrip;
            nodeToInfoDict1.Add(directoryNode, directoryInfo);
            string path = GetPath(directoryInfo.FullName, rootName);
            pathToInfoDict1.Add(path, directoryInfo);
            return directoryNode;
        } // end CreateDirectoryNode1

        private void CreateDirectoryNode2(string rootName, DirectoryInfo directoryInfo, ContextMenuStrip contextMenuStrip) {

        } // end CreateDirectoryNode2

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
            if (nodeToInfoDict1.TryGetValue(currentNode, out info)) {
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
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            ListDirectory1(comboBox1.Text);
        }

        private void button2_Click(object sender, System.EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                //string path = folderBrowserDialog1.SelectedPath;
                //comboBox2.Text = path;
                //comboBox2.Items.Add(path);
                //DataManager.Instance.comboBoxData.SelectPath2(path);
                //ListDirectory1(path);
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, System.EventArgs e) {
            //DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            //ListDirectory1(comboBox2.Text);
        }

        private string GetPath(string fullName, string rootName) {
            string path = fullName.Substring(rootName.Length);
            return path;
        } // end GetPath
    }
}
