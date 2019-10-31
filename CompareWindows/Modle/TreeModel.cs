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

namespace CompareWindows.Modle {
    public class TreeModel : ITreeModel {

        private string leftRoot;
        private string rightRoot;
        private BackgroundWorker _worker;
        private List<BaseItem> _itemsToRead;
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;

        public ProgressModle progress { get; private set; }


        public TreeModel(string leftRoot, string rightRoot) {
            this.leftRoot = leftRoot;
            this.rightRoot = rightRoot;
            DirectoryModle.Reset(leftRoot, rightRoot);
            _itemsToRead = new List<BaseItem>();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
            _worker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnProgressCompleted);
            progress = new ProgressModle();
        }

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
                            Icon icon = new Icon(item.ItemPath);
                            item.Icon1 = icon.ToBitmap();
                        } else if (info.Extension.ToLower() == ".bmp") {
                            item.Icon1 = new Bitmap(item.ItemPath);
                        } // end if
                    } // end if
                    string rightPath = rightRoot + item.ItemPath;
                    bool isExistRight = File.Exists(rightPath);
                    if (isExistRight) {
                        FileInfo info = new FileInfo(rightPath);
                        item.Size2 = info.Length.ToString();
                        item.Date2 = info.CreationTime.ToString();
                        if (info.Extension.ToLower() == ".ico") {
                            Icon icon = new Icon(item.ItemPath);
                            item.Icon2 = icon.ToBitmap();
                        }
                        else if (info.Extension.ToLower() == ".bmp") {
                            item.Icon2 = new Bitmap(item.ItemPath);
                        } // end if
                    } // end if
                    if (isExistLeft && isExistRight) {
                        if (Utility.GetMD5HashFromFile(leftPath) == Utility.GetMD5HashFromFile(rightPath)) {
                            item.IsSame = true;
                        } else {
                            item.IsSame = false;
                        } // end if
                    } else if(isExistLeft && !isExistRight) {
                        item.IsSame = false;
                        item.Brush2 = Define.DisableBrush;
                    } else if(!isExistLeft && isExistRight) {
                        item.IsSame = false;
                        item.Brush1 = Define.DisableBrush;
                    } else {
                        item.Brush1 = item.Brush2 = Define.DefaultBrush;
                    }// end if
                } // end if
                _worker.ReportProgress(0, item);
            } // end while
        } // end ReadFilesProperties

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e) {
            BaseItem item = e.UserState as BaseItem;
            if (NodesChanged != null) {
                TreePath path = GetPath(item.Parent);
                NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
            }
            progress.Current = progress.Current + 1;
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
                DirectoryNode node;
                if (DirectoryModle.DirectoryMap.TryGetValue("", out node)) {
                    List<BaseItem> items = new List<BaseItem>();
                    foreach (var data in node.GetDirectorys()) {
                        FolderItem item = new FolderItem(data, null);
                        items.Add(item);
                    } // foreach
                    foreach (var data in node.GetFiles()) {
                        FileItem item = new FileItem(data, null);
                        items.Add(item);
                    } // foreach
                    _itemsToRead.AddRange(items);
                    RunWorkerAsync();
                    foreach (BaseItem item in items)
                        yield return item;
                    // end foreach
                } // end if
            } else {
                BaseItem parent = treePath.LastNode as BaseItem;
                if (parent != null) {
                    DirectoryNode node;
                    if (DirectoryModle.DirectoryMap.TryGetValue(parent.ItemPath, out node)) {
                        List<BaseItem> items = new List<BaseItem>();
                        foreach (var data in node.GetDirectorys()) {
                            FolderItem item = new FolderItem(data, parent);
                            items.Add(item);
                        } // foreach
                        foreach (var data in node.GetFiles()) {
                            FileItem item = new FileItem(data, parent);
                            items.Add(item);
                        } // foreach
                        _itemsToRead.AddRange(items);
                        RunWorkerAsync();
                        foreach (BaseItem item in items)
                            yield return item;
                        // end foreach
                    } // end if
                } else {
                    yield break;
                } // end if
            } // end if
        }

        public bool IsLeaf(TreePath treePath) {
            return treePath.LastNode is FileItem;
        }

        private void RunWorkerAsync() {
            progress.Restart(0, _itemsToRead.Count);
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
            // end if
        } // end RunWorkerAsync
    }
}
