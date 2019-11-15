using Aga.Controls.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Drawing;
using CompareWindows.Tool;
using CompareWindows.Config;
using CompareWindows.Event;
using CompareWindows.Data;
using System.Windows.Forms;

namespace CompareWindows.Modle {

    public class SvnViewModel : ITreeModel, IDisposable {
        private string leftRoot;
        private string rightRoot;
        private BackgroundWorker _worker;
        private List<BaseItem> _itemsToRead;
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;
        public ProgressModle progress { get; private set; }
        private Dictionary<string, BaseItem> _nodeMap;
        private MoveType moveType;
        private BackgroundWorker _moveWorker;
        private List<string> moveList;
        public ProgressModle moveProgress { get; private set; }
        private List<string> pathList = new List<string>(); 

        public SvnViewModel(string leftRoot, string rightRoot) {
            this.leftRoot = leftRoot;
            this.rightRoot = rightRoot;
            DirectoryModle.Reset(leftRoot, rightRoot);
            _nodeMap = new Dictionary<string, BaseItem>();
            _itemsToRead = new List<BaseItem>();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
            _worker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnProgressCompleted);
            progress = new ProgressModle();
            _moveWorker = new BackgroundWorker();
            _moveWorker.WorkerReportsProgress = true;
            _moveWorker.DoWork += new DoWorkEventHandler(MoveFile);
            _moveWorker.ProgressChanged += new ProgressChangedEventHandler(OnMoveProgressChanged);
            _moveWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnMoveCompleted);
            moveList = new List<string>();
            moveProgress = new ProgressModle();
        }

        public void Dispose() {
            _worker.Dispose();
            _moveWorker.Dispose();
        } // end Dispose

        public void RefreshModle() {
            _nodeMap.Clear();
            _itemsToRead.Clear();
        } // end RefreshModle

        public bool MoveFiles(MoveType type, IEnumerable<string> list) {
            if (_moveWorker.IsBusy) return false;
            moveType = type;
            moveList.Clear();
            moveList.AddRange(list);
            moveProgress.Restart(0, moveList.Count);
            _moveWorker.RunWorkerAsync();
            return true;
        } // end MoveFiles

        public void RefreshListView(IEnumerable<string> list) {
            pathList.Clear();
            pathList.AddRange(list);
        } // end RefreshListView

        void ReadFilesProperties(object sender, DoWorkEventArgs e) {
            while (_itemsToRead.Count > 0) {
                BaseItem item = _itemsToRead[0];
                _itemsToRead.RemoveAt(0);

                if (item is FolderItem) {
                    DirectoryInfo info = new DirectoryInfo(leftRoot + item.ItemPath);
                    if (info != null) item.Date1 = info.CreationTime.ToString();
                    // end if
                    info = new DirectoryInfo(rightRoot + item.ItemPath);
                    if (info != null) item.Date2 = info.CreationTime.ToString();
                    // end if
                } else if (item is FileItem) {
                    string leftPath = leftRoot + item.ItemPath;
                    bool isExistLeft = File.Exists(leftPath);
                    if (isExistLeft) {
                        FileInfo info = new FileInfo(leftPath);
                        item.Size1 = info.Length.ToString();
                        item.Date1 = info.CreationTime.ToString();
                        if (info.Extension.ToLower() == ".ico") {
                            Icon icon = new Icon(info.FullName);
                            item.Icon1 = icon.ToBitmap();
                        } else if (info.Extension.ToLower() == ".bmp") {
                            item.Icon1 = new Bitmap(info.FullName);
                        } // end if
                    } // end if
                    string rightPath = rightRoot + item.ItemPath;
                    bool isExistRight = File.Exists(rightPath);
                    if (isExistRight) {
                        FileInfo info = new FileInfo(rightPath);
                        item.Size2 = info.Length.ToString();
                        item.Date2 = info.CreationTime.ToString();
                        if (info.Extension.ToLower() == ".ico") {
                            Icon icon = new Icon(info.FullName);
                            item.Icon2 = icon.ToBitmap();
                        } else if (info.Extension.ToLower() == ".bmp") {
                            item.Icon2 = new Bitmap(info.FullName);
                        } // end if
                    } // end if
                    if (isExistLeft && isExistRight) {
                        if (Utility.GetMD5HashFromFile(leftPath) == Utility.GetMD5HashFromFile(rightPath)) {
                            item.IsSame = true;
                        } else {
                            item.IsSame = false;
                        }// end if
                        item.IsDisable1 = false;
                        item.IsDisable2 = false;
                    } else if (isExistLeft && !isExistRight) {
                        item.IsSame = false;
                        item.IsDisable1 = false;
                        item.IsDisable2 = true;
                    } else if (!isExistLeft && isExistRight) {
                        item.IsSame = false;
                        item.IsDisable1 = true;
                        item.IsDisable2 = false;
                    } else {
                        item.IsSame = false;
                        item.IsDisable1 = true;
                        item.IsDisable2 = true;
                    } // end if
                } // end if
                _worker.ReportProgress(0, item);
            } // end while
        } // end ReadFilesProperties

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e) {
            BaseItem item = e.UserState as BaseItem;
            progress.Current = progress.Current + 1;
            if (NodesChanged != null) {
                TreePath path = GetPath(item.Parent);
                NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
            } // end if
        } // end OnProgressChanged

        private void OnProgressCompleted(object sender, RunWorkerCompletedEventArgs e) {
            progress.Completed();
        } // end OnProgressCompleted

        private TreePath GetPath(BaseItem item) {
            Stack<object> stack = new Stack<object>();
            while (item != null && !(item is RootItem)) {
                stack.Push(item);
                item = item.Parent;
            }
            return new TreePath(stack.ToArray());
        }

        public IEnumerable GetChildren(TreePath treePath) {
            if (treePath.IsEmpty()) {
                List<BaseItem> items = new List<BaseItem>();
                int index = 0;
                foreach (var data in pathList) {
                    BaseItem item = new FileItem(data, null, index);
                    items.Add(item);
                    _nodeMap.Add(item.ItemPath, item);
                    ++index;
                } // foreach
                _itemsToRead.AddRange(items);
                RunWorkerAsync();
                foreach (BaseItem item in items) {
                    yield return item;
                } // end foreach
            } else {
                yield break;
            } // end if
        }

        public bool IsLeaf(TreePath treePath) {
            return treePath.LastNode is FileItem;
        }

        private void RunWorkerAsync() {
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
            // end if
            progress.Restart(0, _itemsToRead.Count);
        } // end RunWorkerAsync

        private void MoveFile(object sender, DoWorkEventArgs e) {
            string source = leftRoot;
            string target = rightRoot;
            if (moveType == MoveType.ToLeft) {
                source = rightRoot;
                target = leftRoot;
            } // end if
            foreach (string path in moveList) {
                BaseItem node;
                if (_nodeMap.TryGetValue(path, out node)) {
                    string sourcePath = source + path;
                    string targetPath = target + path;
                    if (!File.Exists(sourcePath)) continue;
                    // end if
                    string directory = Path.GetDirectoryName(targetPath);
                    if (!Directory.Exists(directory)) {
                        Directory.CreateDirectory(directory);
                    } // end if
                    File.Copy(sourcePath, targetPath, true);
                    FileInfo sourceInfo = new FileInfo(sourcePath);
                    FileInfo targetInfo = new FileInfo(targetPath);
                    node.IsSame = true;
                    node.IsDisable1 = false;
                    node.IsDisable2 = false;
                    if (moveType == MoveType.ToLeft) {
                        node.Size1 = targetInfo.Length.ToString();
                        node.Date1 = targetInfo.CreationTime.ToString();
                    } else if (moveType == MoveType.ToRight) {
                        node.Size2 = targetInfo.Length.ToString();
                        node.Date2 = targetInfo.CreationTime.ToString();
                    } // end if
                    _moveWorker.ReportProgress(0, node);
                } // end if
            } // end foreach
        } // end MoveFile

        private void OnMoveProgressChanged(object sender, ProgressChangedEventArgs e) {
            BaseItem item = e.UserState as BaseItem;
            moveProgress.Current = moveProgress.Current + 1;
            if (NodesChanged != null) {
                TreePath path = GetPath(item.Parent);
                NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
            } // end if
        } // end OnMoveProgressChanged

        private void OnMoveCompleted(object sender, RunWorkerCompletedEventArgs e) {
            moveProgress.Completed();
        } // end OnCompareCompleted
    }
}
