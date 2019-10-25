using CompareWindows.Algorithm.LinkList;
using CompareWindows.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.Modle {
    public class FileNodeList {

        public string rootPath { get; private set; }
        public Dictionary<string, FileNode> pathToNodeMap { get; private set; }

        public FileNodeList(string rootPath, HashSet<string> pathList) {
            this.rootPath = rootPath;
            pathToNodeMap = new Dictionary<string, FileNode>();
            StringBuilder pathStr = new StringBuilder();
            pathStr.Append(rootPath);
            foreach (var path in pathList) {
                pathStr.Length = rootPath.Length;
                pathStr.Append(path);
                string filePath = pathStr.ToString();
                if (Directory.Exists(filePath)) continue;
                // end if
                if (File.Exists(filePath)) {
                    pathToNodeMap.Add(path, new FileNode(rootPath, new FileInfo(filePath)));
                } else {
                    pathToNodeMap.Add(path, new FileNode(path));
                }// end if
            } // end for
        } // end NodeList

        public static void CompareFileNodeList(FileNodeList list1, FileNodeList list2) {
            FileNode node1;
            FileNode node2;
            foreach (var pari in list1.pathToNodeMap) {
                node1 = pari.Value;
                if (list2.pathToNodeMap.TryGetValue(pari.Key, out node2)) {
                    node1.ResetCompare();
                    node2.ResetCompare();
                    if (node1.fileSystemInfo == null) {
                        if (node2.fileSystemInfo == null) {
                            MessageBox.Show("警告", "无效输入：" + pari.Key, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        } else {
                            node2.IsSpecial = true;
                        } // end if
                    } else {
                        if (node2.fileSystemInfo == null) {
                            node1.IsSpecial = true;
                        } else {
                            if (Utility.GetMD5HashFromFile(node1.FullPath) == Utility.GetMD5HashFromFile(node2.FullPath)) {
                                node1.IsSame = true;
                                node2.IsSame = true;
                            } else {
                                node1.IsSame = false;
                                node1.IsSame = false;
                            } // end if
                        } // end if
                    } // end if
                } else {
                    throw new Exception();
                }// end if
            } // end forech
        } // end CompareFileNodeList
    } // end class FileNodeList
} // end namespace CompareWindows.Modle