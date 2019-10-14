using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using CompareWindows.Data;
using CompareWindows.View;

namespace CompareWindows {
    public partial class MainForm : Form {
        private InfoView leftInfoView;
        private InfoView rightInfoView;

        public MainForm() {
            InitializeComponent();
            Text = GetType().ToString();
            leftInfoView = new InfoView(treeView1, LeftNodeMenuStrip);
            rightInfoView = new InfoView(treeView2, null);
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
        }

        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox1.Text = path;
                comboBox1.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath1(path);
                leftInfoView.RefreshDisplay(path);
            } // end if
        }

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

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) {
            //TreeNode currentNode = treeView1.SelectedNode;
            //FileSystemInfo info;
            //if (nodeToInfoDict1.TryGetValue(currentNode, out info)) {
            //    string[] file = new string[1];
            //    file[0] = info.FullName;
            //    DataObject dataObject = new DataObject();
            //    dataObject.SetData(DataFormats.FileDrop, file);
            //    Clipboard.SetDataObject(dataObject, true);
            //} else {
            //    throw new Exception();
            //} // end if
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            leftInfoView.RefreshDisplay(comboBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog2.SelectedPath;
                comboBox2.Text = path;
                comboBox2.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath2(path);
                rightInfoView.RefreshDisplay(path);
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            rightInfoView.RefreshDisplay(comboBox2.Text);
        }

        private string GetPath(string fullName, string rootName) {
            string path = fullName.Substring(rootName.Length);
            return path;
        } // end GetPath

        private void ToDetectDifferences() {
            //foreach (var item in pathToNodeDict2) {
            //    TreeNode node1;
            //    if (pathToNodeDict1.TryGetValue(item.Key, out node1)) {
            //        FileSystemInfo info1 = nodeToInfoDict1[node1];
            //        FileSystemInfo info2 = nodeToInfoDict2[item.Value];
            //        if (info1 is FileInfo && info2 is FileInfo) {
            //            if (GetMD5HashFromFile(info1.FullName) != GetMD5HashFromFile(info2.FullName)) {
            //                item.Value.ForeColor = Color.Red;
            //            } else {
            //                item.Value.ForeColor = Color.Black;
            //            }// end if
            //        } // end if
            //    } else {
            //        item.Value.ForeColor = Color.Red;
            //    } // end if
            //} // end foreach
        } // end ToDetectDifferences

        private void CopyToRightToolStripMenuItem_Click(object sender, EventArgs e) {
            //if (string.IsNullOrEmpty(rootName2)) return;
            //// end if
            //TreeNode currentNode = treeView1.SelectedNode;
            //FileSystemInfo info;
            //if (nodeToInfoDict1.TryGetValue(currentNode, out info)) {
            //    if (info is FileInfo) {
            //        FileInfo file = info as FileInfo;
            //        string path = GetPath(file.FullName, rootName1);
            //        file.CopyTo(rootName2 + path, true);
            //    } // end if
            //    if (info is DirectoryInfo) {
            //        DirectoryInfo directory = info as DirectoryInfo;
            //        string path = GetPath(directory.FullName, rootName1);
            //        CopyDir(directory.FullName, rootName2 + path);
            //    } // end if
            //    ListDirectory2(comboBox2.Text);
            //} else {
            //    throw new Exception();
            //} // end if
        }

        private void CopyDir(string srcPath, string aimPath) {
            try {
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar) {
                    aimPath += Path.DirectorySeparatorChar;
                } // end if
                if (!Directory.Exists(aimPath)) {
                    Directory.CreateDirectory(aimPath);
                } // end if
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                foreach (string file in fileList) {
                    if (Directory.Exists(file)) {
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    } else {
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                    } // end if
                } // end foreachg
            } catch (Exception) {
                throw;
            } // end try
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) {
            FilterForm form = new FilterForm();
            AddOwnedForm(form);
            form.ShowDialog();
        }
    }
}
