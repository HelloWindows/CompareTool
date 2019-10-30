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

namespace CompareWindows.Modle {
    public class TreeModle : ITreeModel {

        private string leftRoot;
        private string rightRoot;
        private BackgroundWorker _worker;
        private List<BaseItem> _itemsToRead;
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;


        public TreeModle(string leftRoot, string rightRoot) {
            this.leftRoot = leftRoot;
            this.rightRoot = rightRoot;
            DirectoryModle.Reset(leftRoot, rightRoot);
            _itemsToRead = new List<BaseItem>();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
            _worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
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
                    string path = leftRoot + item.ItemPath;
                    if (File.Exists(path)) {
                        FileInfo info = new FileInfo(path);
                        item.Size1 = info.Length.ToString();
                        item.Date1 = info.CreationTime.ToString();
                        if (info.Extension.ToLower() == ".ico") {
                            Icon icon = new Icon(item.ItemPath);
                            item.Icon1 = icon.ToBitmap();
                        } else if (info.Extension.ToLower() == ".bmp") {
                            item.Icon1 = new Bitmap(item.ItemPath);
                        } // end if
                    } // end if
                    path = rightRoot + item.ItemPath;
                    if (File.Exists(path)) {
                        FileInfo info = new FileInfo(path);
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
                } // end if
                _worker.ReportProgress(0, item);
            } // end while
        } // end ReadFilesProperties

        void ProgressChanged(object sender, ProgressChangedEventArgs e) {
            BaseItem item = e.UserState as BaseItem;
            if (NodesChanged != null) {
                TreePath path = GetPath(item.Parent);
                NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
            }
        }

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
                    foreach (var data in node.GetDirectorys()) {
                        FolderItem item = new FolderItem(data, null);
                        yield return item;
                    } // foreach
                    foreach (var data in node.GetFiles()) {
                        FileItem item = new FileItem(data, null);
                        yield return item;
                    } // foreach
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
                        if (!_worker.IsBusy)
                            _worker.RunWorkerAsync();
                        // end if
                        foreach (BaseItem item in items)
                            yield return item;
                    } // end if
                }
                else
                    yield break;
            }
        }

        public bool IsLeaf(TreePath treePath) {
            return treePath.LastNode is FileItem;
        }
    }
}
