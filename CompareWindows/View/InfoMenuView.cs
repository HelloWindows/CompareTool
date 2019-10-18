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
        } // end InfoMenuView

        public void ResetPath(TreeModle mainModle, TreeModle otherModle) {
            mainRootPath = mainModle == null ? string.Empty : mainModle.rootPath;
            otherRootPath = otherModle == null ? string.Empty : otherModle.rootPath;
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
                info.fileSystemInfo.Delete();
            } // end if
        } // end DeleteItemOnClick

        private void CopyToItemOnClick(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mainRootPath) || string.IsNullOrEmpty(otherRootPath)) return;
            // end if
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                if (info is FileNode) {
                    FileInfo file = info.fileSystemInfo as FileInfo;
                    string path = Utility.GetRelativePath(mainRootPath, info.FullPath);
                    file.CopyTo(otherRootPath + path, true);
                } // end if
                if (info is DirectoryNode) {
                    DirectoryInfo directory = info.fileSystemInfo as DirectoryInfo;
                    string path = Utility.GetRelativePath(mainRootPath, directory.FullName);
                    CopyDirectory(directory.FullName, otherRootPath + path);
                } // end if
            } else {
                throw new Exception();
            } // end if
        } // end CopyToItemOnClick

        private void MoveToItemOnClick(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(mainRootPath) || string.IsNullOrEmpty(otherRootPath)) return;
            // end if
            TreeNode currentNode = mainView.treeView.SelectedNode;
            InfoNode info;
            if (mainView.nodeToInfoMap.TryGetValue(currentNode, out info)) {
                if (info is FileNode) {
                    FileInfo file = info.fileSystemInfo as FileInfo;
                    string path = Utility.GetRelativePath(mainRootPath, info.FullPath);
                    file.MoveTo(otherRootPath + path);
                } // end if
                if (info is DirectoryNode) {
                    DirectoryInfo directory = info.fileSystemInfo as DirectoryInfo;
                    string path = Utility.GetRelativePath(mainRootPath, directory.FullName);
                    directory.MoveTo(otherRootPath + path);
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
