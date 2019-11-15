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

    public enum MoveType {
        /// <summary>
        /// 移向左边
        /// </summary>
        ToLeft = 0, 
        /// <summary>
        /// 移向右边
        /// </summary>
        ToRight = 1,
    } // end enum MoveType

    public class TreeModel : ITreeModel, IDisposable {

        private class ItemNode {
            public bool IsSame { get; private set; } = true;
            public bool IsDisable1 { get; private set; } = true;
            public bool IsDisable2 { get; private set; } = true;
            private List<string> parentList = new List<string>();

            public ItemNode(string name) {
                while (!string.IsNullOrEmpty(name)) {
                    name = name.Remove(name.LastIndexOf('\\'));
                    parentList.Add(name);
                } // end while
            } // end ItemNode

            public void SetProperty(bool isSame, bool isDisable1, bool isDisable2, Dictionary<string, ItemNode> map) {
                IsSame = isSame;
                IsDisable1 = isDisable1;
                IsDisable2 = isDisable2;
                ItemNode parent;
                foreach (var path in parentList) {
                    if (map.TryGetValue(path, out parent)) {
                        if (!isSame) parent.IsSame = isSame;
                        // end if
                        if (!isDisable1) parent.IsDisable1 = isDisable1;
                        // end if
                        if (!isDisable2) parent.IsDisable2 = isDisable2;
                        // end if
                    } // end if
                } // end foreach
            } // end SetIsSame
        } // end class ItemNode

        private string leftRoot;
        private string rightRoot;
        private BackgroundWorker _worker;
        private List<BaseItem> _itemsToRead;
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;

        public ProgressModle progress { get; private set; }

        private BackgroundWorker _compare;
        private Dictionary<string, ItemNode> _itemMap;
        private Dictionary<string, BaseItem> _nodeMap;

        private MoveType moveType;
        private BackgroundWorker _moveWorker;
        private List<string> moveList;
        public ProgressModle moveProgress { get; private set; }

        public bool IsCompared {
            get {
                return _itemMap.Count >= DirectoryModle.TotalFileCount;
            } // end get
        }

        public TreeModel(string leftRoot, string rightRoot) {
            this.leftRoot = leftRoot;
            this.rightRoot = rightRoot;
            DirectoryModle.Reset(leftRoot, rightRoot);
            _itemMap = new Dictionary<string, ItemNode>();
            _nodeMap = new Dictionary<string, BaseItem>();
            _itemsToRead = new List<BaseItem>();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
            _worker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);
         
            progress = new ProgressModle();
            _compare = new BackgroundWorker();
            _compare.WorkerReportsProgress = true;
            _compare.DoWork += new DoWorkEventHandler(CompareWork);
            _compare.ProgressChanged += new ProgressChangedEventHandler(OnCompareChanged);
            _compare.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompareCompleted);
            if (!_itemMap.ContainsKey("")) _itemMap.Add("", new ItemNode(""));
            // end if
            foreach (var item in DirectoryModle.DirectoryMap) {
                foreach (var folder in item.Value.GetDirectorys()) {
                    lock (_itemMap) {
                        if (!_itemMap.ContainsKey(folder)) _itemMap.Add(folder, new ItemNode(folder));
                        // end if
                    } // end lock
                    _compare.ReportProgress(0);
                } // end foreach
            } // end foreach
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
            _compare.Dispose();
            _moveWorker.Dispose();
        } // end Dispose

        public void RefreshModle() {
            _nodeMap.Clear();
            _itemsToRead.Clear();
        } // end RefreshModle

        public void StartCompare() {
            if (IsCompared) return;
            // end if
            if (!_compare.IsBusy) {
                progress.Restart(_itemMap.Count, DirectoryModle.TotalItemCount);
                _compare.RunWorkerAsync();
            } // end if
        } // end StartCompare

        public bool MoveFiles(MoveType type, IEnumerable<string> list) {
            if (_moveWorker.IsBusy) return false;
            moveType = type;
            moveList.Clear();
            moveList.AddRange(list);
            moveProgress.Restart(0, moveList.Count);
            _moveWorker.RunWorkerAsync();
            return true;
        } // end MoveFiles

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
                    ItemNode node;
                    lock (_itemMap) {
                        if (_itemMap.TryGetValue(item.ItemPath, out node)) {
                            item.IsSame = node.IsSame;
                            item.IsDisable1 = node.IsDisable1;
                            item.IsDisable2 = node.IsDisable2;
                        } // end if
                    } // end lock
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
                        }
                        else if (info.Extension.ToLower() == ".bmp") {
                            item.Icon2 = new Bitmap(info.FullName);
                        } // end if
                    } // end if
                    ItemNode node;
                    lock (_itemMap) {
                        if (!_itemMap.TryGetValue(item.ItemPath, out node)) {
                            node = CompareFile(item.ItemPath, leftPath, rightPath);
                        } // end if
                    } // end lock
                    item.IsSame = node.IsSame;
                    item.IsDisable1 = node.IsDisable1;
                    item.IsDisable2 = node.IsDisable2;
                } // end if
                _worker.ReportProgress(0, item);
            } // end while
        } // end ReadFilesProperties

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e) {
            BaseItem item = e.UserState as BaseItem;
            progress.Current = _itemMap.Count;
            if (NodesChanged != null) {
                TreePath path = GetPath(item.Parent);
                NodesChanged(this, new TreeModelEventArgs(path, new object[] { item }));
            } // end if
        } // end OnProgressChanged

        private void OnCompareChanged(object sender, ProgressChangedEventArgs e) {
            progress.Current = _itemMap.Count;
        } // end OnCompareChanged

        private void OnCompareCompleted(object sender, RunWorkerCompletedEventArgs e) {
            progress.Completed();
        } // end OnCompareCompleted

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
                if (!AskShowWithPath("")) yield break;
                // end if
                DirectoryNode node;
                if (DirectoryModle.DirectoryMap.TryGetValue("", out node)) {
                    List<BaseItem> items = new List<BaseItem>();
                    int index = 0;
                    foreach (var data in node.GetDirectorys()) {
                        if (!AskShowWithPath(data)) continue;
                        // end if
                        BaseItem item = new FolderItem(data, null, index);
                        if (FilterModle.IsFilterFolder(item.Name)) continue;
                        // end if
                        items.Add(item);
                        _nodeMap.Add(item.ItemPath, item);
                        ++index;
                    } // foreach
                    foreach (var data in node.GetFiles()) {
                        if (!AskShowWithPath(data)) continue;
                        // end if
                        BaseItem item = new FileItem(data, null, index);
                        if (FilterModle.IsFilterFile(item.Name)) continue;
                        // end if
                        items.Add(item);
                        _nodeMap.Add(item.ItemPath, item);
                        ++index;
                    } // foreach
                    _itemsToRead.AddRange(items);
                    RunWorkerAsync();
                    foreach (BaseItem item in items) {
                        yield return item;
                    } // end foreach
                } // end if
            } else {
                BaseItem parent = treePath.LastNode as BaseItem;
                if (parent != null) {
                    if (!AskShowWithPath(parent.ItemPath)) yield break;
                    // end if
                    DirectoryNode node;
                    if (DirectoryModle.DirectoryMap.TryGetValue(parent.ItemPath, out node)) {
                        List<BaseItem> items = new List<BaseItem>();
                        int index = 0;
                        foreach (var data in node.GetDirectorys()) {
                            if (!AskShowWithPath(data)) continue;
                            // end if
                            BaseItem item = new FolderItem(data, parent, index);
                            if (FilterModle.IsFilterFolder(item.Name)) continue;
                            // end if
                            items.Add(item);
                            _nodeMap.Add(item.ItemPath, item);
                            ++index;
                        } // foreach
                        foreach (var data in node.GetFiles()) {
                            if (!AskShowWithPath(data)) continue;
                            // end if
                            BaseItem item = new FileItem(data, parent, index);
                            if (FilterModle.IsFilterFile(item.Name)) continue;
                            // end if
                            items.Add(item);
                            _nodeMap.Add(item.ItemPath, item);
                            ++index;
                        } // foreach
                        _itemsToRead.AddRange(items);
                        RunWorkerAsync();
                        foreach (BaseItem item in items) {
                            yield return item;
                        } // end foreach
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
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
            // end if
        } // end RunWorkerAsync

        private void CompareWork(object sender, DoWorkEventArgs e) {
            foreach (var item in DirectoryModle.DirectoryMap) {
                foreach (var file in item.Value.GetFiles()) {
                    lock (_itemMap) {
                        if (_itemMap.ContainsKey(file)) continue;
                        // end if
                        string leftPath = leftRoot + file;
                        string rightPath = rightRoot + file;
                        CompareFile(file, leftPath, rightPath);
                    } // end lock
                    _compare.ReportProgress(0);
                } // end foreach
            } // end foreach
        } // end CompareWork

        private ItemNode CompareFile(string file, string leftPath, string rightPath) {
            bool isExistLeft = File.Exists(leftPath);
            bool isExistRight = File.Exists(rightPath);
            var node = new ItemNode(file);
            if (isExistLeft && isExistRight) {
                bool isSame = false;
                if (Utility.GetMD5HashFromFile(leftPath) == Utility.GetMD5HashFromFile(rightPath)) {
                    isSame = true;
                } // end if
                node.SetProperty(isSame, false, false, _itemMap);
            } else if (isExistLeft && !isExistRight) {
                node.SetProperty(false, false, true, _itemMap);
            } else if (!isExistLeft && isExistRight) {
                node.SetProperty(false, true, false, _itemMap);
            } else {
                node.SetProperty(false, true, true, _itemMap);
            } // end if
            _itemMap.Add(file, node);
            return node;
        } // end CompareFile

        private bool AskShowWithPath(string path) {
            if (Global.ShowSame) return true;
            // end if
            ItemNode node;
            if (_itemMap.TryGetValue(path, out node)) {
                return node.IsSame == false;
            } else {
                MessageBox.Show(string.Format("{0}没有对比记录", path));
                return true;
            }// end if
        } // end AskShowWithPath

        private void MoveFile(object sender, DoWorkEventArgs e) {
            string source = leftRoot;
            string target = rightRoot;
            if (moveType == MoveType.ToLeft) {
                source = rightRoot;
                target = leftRoot;
            } // end if
            foreach (string path in moveList) {
                BaseItem node;
                ItemNode item;
                if (_nodeMap.TryGetValue(path, out node)) {
                    string sourcePath = source + path;
                    string targetPath = target + path;
                    if (!File.Exists(sourcePath)) continue;
                    // end if
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
                    if (_itemMap.TryGetValue(path, out item)) {
                        item.SetProperty(true, false, false, _itemMap);
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
