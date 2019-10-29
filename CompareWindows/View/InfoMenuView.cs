using CompareWindows.Modle;
using CompareWindows.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.View {
    public class InfoMenuView {
        private InfoView mainView;
        private InfoView otherView;
        private TreeModle mainModle;
        private TreeModle otherModle;
        private ContextMenuStrip contextMenu;
        public string mainRootPath { get; set; }
        public string otherRootPath { get; set; }

        public InfoMenuView(InfoView mainView, InfoView otherView, ContextMenuStrip contextMenu) {
            this.mainView = mainView;
            this.otherView = otherView;
            this.contextMenu = contextMenu;
            contextMenu.Items[0].Click += new EventHandler(CopyItemOnClick);
            contextMenu.Items[1].Click += new EventHandler(DeleteItemOnClick);
            contextMenu.Items[2].Click += new EventHandler(CopyToItemOnClick);
            contextMenu.Items[3].Click += new EventHandler(MoveToItemOnClick);
            contextMenu.Items[4].Click += new EventHandler(CompareItemOnClick);
        } // end InfoMenuView

        public void ResetPath(TreeModle mainModle, TreeModle otherModle) {
            mainRootPath = mainModle == null ? string.Empty : mainModle.rootPath;
            otherRootPath = otherModle == null ? string.Empty : otherModle.rootPath;
            this.mainModle = mainModle;
            this.otherModle = otherModle;
        } // end ResetPath

        private void CopyItemOnClick(object sender, EventArgs e) {
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                string[] file = new string[1];
                file[0] = info.FullPath;
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.FileDrop, file);
                Clipboard.SetDataObject(dataObject, true);
            } else {
                throw new Exception();
            } // end if
        } // end CopyItemOnClick

        private void DeleteItemOnClick(object sender, EventArgs e) {
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                if (info.fileSystemInfo == null) return;
                // end if
                InfoNode otherInfo;
                if (otherModle != null && otherModle.pathToInfoMap.TryGetValue(info.RelativePath, out otherInfo)) {
                    if (otherInfo.fileSystemInfo != null) {
                        info.SetEmpty();
                        otherInfo.IsSpecial = true;
                    } // end if
                } // end if
                info.fileSystemInfo.Delete();
            } // end if
        } // end DeleteItemOnClick

        private void CopyToItemOnClick(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mainRootPath) || string.IsNullOrEmpty(otherRootPath)) return;
            // end if
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                if (info.fileSystemInfo == null) return;
                // end if
                string path = info.RelativePath;
                if (info is FileNode) {
                    FileInfo file = info.fileSystemInfo as FileInfo;
                    InfoNode otherInfo;
                    if (otherModle != null && otherModle.pathToInfoMap.TryGetValue(path, out otherInfo)) {
                        string targetPath = otherRootPath + path;
                        string parentPath = otherRootPath + otherInfo.ParentRelativePath;
                        if (!Directory.Exists(parentPath)) {
                            Directory.CreateDirectory(parentPath);
                        } // end if
                        file.CopyTo(targetPath, true);
                        info.IsSame = true;
                        otherInfo.SetFileSystemInfo(otherRootPath, new FileInfo(targetPath));
                        otherInfo.IsSame = true;
                    } // end if
                    mainView.SetSameNode(path);
                    otherView.SetSameNode(path);
                } // end if
                if (info is DirectoryNode) {
                    MessageBox.Show("暂不能拷贝文件夹");
                    //DirectoryInfo directory = info.fileSystemInfo as DirectoryInfo;
                    //CopyDirectory(directory.FullName, otherRootPath + path);
                    //mainView.SetSameNodes(path);
                    //otherView.SetSameNodes(path);
                } // end if
            } else {
                throw new Exception();
            } // end if
        } // end CopyToItemOnClick

        private void CompareItemOnClick(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mainRootPath) || string.IsNullOrEmpty(otherRootPath)) return;
            // end if
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                if (info is FileNode) {
                    if (Utility.IsNullOrImage(info.FullPath)) {
                        InfoNode otherInfo;
                        string otherPath = string.Empty;
                        if (otherModle != null && otherModle.pathToInfoMap.TryGetValue(info.RelativePath, out otherInfo)) {
                            otherPath = otherInfo.FullPath;
                        } // end if
                        PictureForm form = new PictureForm();
                        form.ShowPricture(info.FullPath, otherPath);
                        form.ShowDialog();
                    } // end if
                } // end if
                if (info is DirectoryNode) {
                    MessageBox.Show("不能比较文件夹");
                } // end if
            } else {
                throw new Exception();
            }// end if
        } // end CompareItemOnClick

        private void MoveToItemOnClick(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mainRootPath) || string.IsNullOrEmpty(otherRootPath)) return;
            // end if
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                string path = info.RelativePath;
                if (info is FileNode) {
                    if (info.fileSystemInfo == null) return;
                    // end if
                    InfoNode otherInfo;
                    if (otherModle != null && otherModle.pathToInfoMap.TryGetValue(path, out otherInfo)) {
                        FileInfo file = info.fileSystemInfo as FileInfo;
                        string targetPath = otherRootPath + path;
                        string parentPath = otherRootPath + info.ParentRelativePath;
                        if (!Directory.Exists(parentPath)) {
                            Directory.CreateDirectory(parentPath);
                        } else if (File.Exists(targetPath)) {
                            File.Delete(targetPath);
                        } // end if
                        mainView.SetEmptyNode(path);
                        otherView.SetSpecialNode(path, info.Name);
                        otherInfo.SetFileSystemInfo(otherRootPath, new FileInfo(targetPath));
                        otherInfo.IsSpecial = true;
                        info.SetEmpty();
                        file.MoveTo(targetPath);
                    } // end if
                } // end if
                if (info is DirectoryNode) {
                    MessageBox.Show("暂不能移动文件夹");
                    //DirectoryInfo directory = info.fileSystemInfo as DirectoryInfo;
                    //directory.MoveTo(otherRootPath + path);
                } // end if
            } else {
                throw new Exception();
            } // end if
        } // end MoveToItemOnClick

        private void CopyDirectory(string source, string target) {
            try {
                if (target[target.Length - 1] != Path.DirectorySeparatorChar) {
                    target += Path.DirectorySeparatorChar;
                } // end if
                if (!Directory.Exists(target)) {
                    Directory.CreateDirectory(target);
                } // end if
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                string[] fileList = Directory.GetFileSystemEntries(source);
                foreach (string file in fileList) {
                    if (Directory.Exists(file)) {
                        CopyDirectory(file, target + Path.GetFileName(file));
                    } else {
                        File.Copy(file, target + Path.GetFileName(file), true);
                    } // end if
                } // end foreachg
            } catch (Exception) {
                throw;
            } // end try
        } // end CopyDirectory
    } // end class InfoMenuView
} // end namespace CompareWindows.View
