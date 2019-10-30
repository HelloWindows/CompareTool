using CompareWindows.Data;
using CompareWindows.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class TreeModle2 {
        public string rootPath { get; private set; }
        public DirectoryNode2 rootDirectoryNode { get; private set; }
        public Dictionary<string, InfoNode> pathToInfoMap { get; private set; }
        public int InfoCount { get { return pathToInfoMap.Count; } }

        public TreeModle2(string path) {
            pathToInfoMap = new Dictionary<string, InfoNode>();
            var rootDirectoryInfo = new DirectoryInfo(path);
            rootPath = rootDirectoryInfo.FullName;
            rootDirectoryNode = CreateDirectoryNode(rootPath, rootDirectoryInfo);
            MapPathToNode(rootDirectoryNode);
            DoDetectFilter();
        } // end TreeModle
        /// <summary>
        /// 检测过滤
        /// </summary>
        public void DoDetectFilter() {
            ResetFilter(rootDirectoryNode);
            FilterDirectory(rootDirectoryNode);
            CheckFilter(rootDirectoryNode);
        } // end DoDetectFilter


        private void CheckFilter(DirectoryNode2 directoryNode) {
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                CheckFilter(directory);
                if (!directory.IsFilter) {
                    directoryNode.IsFilter = false;
                } // end if
            } // end foreach
        } // end CheckFilter

        /// <summary>
        /// 比较树模型
        /// </summary>
        /// <param name="treeModle1"> 树模型1 </param>
        /// <param name="treeModle2"> 树模型2 </param>
        public static void CompareModle(TreeModle2 treeModle1, TreeModle2 treeModle2) {
            if (null == treeModle1 || null == treeModle2) return;
            // end if
            ResetCompare(treeModle1.rootDirectoryNode);
            ResetCompare(treeModle2.rootDirectoryNode);
            foreach (var pair in treeModle1.pathToInfoMap) {
                DataManager.Instance.compareProgressData.Increment();
                if (!treeModle2.pathToInfoMap.ContainsKey(pair.Key)) {
                    pair.Value.IsSpecial = true;
                } else {
                    var node = treeModle2.pathToInfoMap[pair.Key];
                    if (pair.Value is DirectoryNode2 || node is DirectoryNode2 ||
                        pair.Value.IsFilter || node.IsFilter) continue;
                    // end if
                    if (Utility.GetMD5HashFromFile(pair.Value.FullPath) == Utility.GetMD5HashFromFile(node.FullPath)) {
                        node.IsSame = true;
                        pair.Value.IsSame = true;
                    } else {
                        node.IsSame = false;
                        pair.Value.IsSame = false;
                    } // end if
                }// end if
            } // end foreach
            foreach (var pair in treeModle2.pathToInfoMap) {
                if (!treeModle1.pathToInfoMap.ContainsKey(pair.Key)) {
                    pair.Value.IsSpecial = true;
                } // end if
            } // end foreach
            MergeModle(treeModle1, treeModle2);
            CompareDirectory(treeModle1.rootDirectoryNode);
            CompareDirectory(treeModle2.rootDirectoryNode);
        } // end CompareModle

        private static void MergeModle(TreeModle2 treeModle1, TreeModle2 treeModle2) {
            foreach (var pair in treeModle1.pathToInfoMap) {
                if (!treeModle2.pathToInfoMap.ContainsKey(pair.Key)) {
                    if (pair.Value is DirectoryNode2) {
                        InsertEmptyDirectoryNode(treeModle2, pair.Key);
                    } else {
                        InsertEmptyFileNode(treeModle2, pair.Key);
                    } // end if
                } // end if
            } // end foreach
            foreach (var pair in treeModle2.pathToInfoMap) {
                if (!treeModle1.pathToInfoMap.ContainsKey(pair.Key)) {
                    if (pair.Value is DirectoryNode2) {
                        InsertEmptyDirectoryNode(treeModle1, pair.Key);
                    } else {
                        InsertEmptyFileNode(treeModle1, pair.Key);
                    } // end if
                } // end if
            } // end foreach
        } // end MergeModle

        private static DirectoryNode2 InsertEmptyDirectoryNode(TreeModle2 treeModle, string relativePath) {
            if(treeModle.pathToInfoMap.ContainsKey(relativePath)) throw new Exception();
            // end if
            int index = relativePath.LastIndexOf('\\');
            if (index < 0) {
                throw new Exception();
            } else {
                string parentPath = relativePath.Substring(0, index);
                InfoNode node;
                DirectoryNode2 directory;
                if (treeModle.pathToInfoMap.TryGetValue(parentPath, out node)) {
                    directory = node as DirectoryNode2;
                } else {
                    directory = InsertEmptyDirectoryNode(treeModle, parentPath);
                } // end if
                if (directory == null) {
                    throw new Exception();
                } // end if
                DirectoryNode2 empty = new DirectoryNode2(relativePath);
                directory.AddEmptyDirectory(empty);
                treeModle.pathToInfoMap.Add(empty.RelativePath, empty);
                return empty;
            } // end if
        } // end InsertEmptyDirectoryNode

        private static FileNode InsertEmptyFileNode(TreeModle2 treeModle, string relativePath) {
            if(treeModle.pathToInfoMap.ContainsKey(relativePath)) throw new Exception();
            // end if
            int index = relativePath.LastIndexOf('\\');
            if (index < 0) {
                throw new Exception();
            } else {
                string parentPath = relativePath.Substring(0, index);
                InfoNode node;
                DirectoryNode2 directory;
                if (treeModle.pathToInfoMap.TryGetValue(parentPath, out node)) {
                    directory = node as DirectoryNode2;
                } else {
                    directory = InsertEmptyDirectoryNode(treeModle, parentPath);
                } // end if
                if (directory == null) {
                    throw new Exception();
                } // end if
                FileNode empty = new FileNode(relativePath);
                directory.AddEmptyFile(empty);
                treeModle.pathToInfoMap.Add(empty.RelativePath, empty);
                return empty;
            } // end if
        } // end InsertEmptyFileNode

        /// <summary>
        /// 比较文件夹
        /// </summary>
        /// <param name="directoryNode"></param>
        private static void CompareDirectory(DirectoryNode2 directoryNode) {
            bool isSpecial = false;
            bool isSame = true;
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                CompareDirectory(directory);
                if (directory.fileSystemInfo == null || directory.IsSpecial) {
                    isSpecial = true;
                    isSame = false;
                } else if (!directory.IsSame) {
                    isSame = false;
                } // end if
            } // end foreach
            if (isSpecial == false && isSame == true) {
                foreach (var file in directoryNode.GetFileNodes()) {
                    if (file.IsSpecial || file.fileSystemInfo == null) {
                        isSpecial = true;
                        isSame = false;
                        break;
                    } else if (!file.IsSame) {
                        isSame = false;
                    } // end if
                } // end foreach
            } // end if
            directoryNode.IsSame = isSame;
            directoryNode.IsSpecial = isSpecial;
        } // end CompareDirectory
        /// <summary>
        /// 重置节点比较信息
        /// </summary>
        /// <param name="directoryNode"> 文件夹节点 </param>
        private static void ResetCompare(DirectoryNode2 directoryNode) {
            directoryNode.ResetCompare();
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                ResetCompare(directory);
            } // end foreach
            foreach (var file in directoryNode.GetFileNodes()) {
                file.ResetCompare();
            } // end foreach
        } // end ResetCompare
        /// <summary>
        /// 创角文件夹节点
        /// </summary>
        /// <param name="rootPath"> 根路径 </param>
        /// <param name="directoryInfo"> 文件夹 </param>
        /// <returns> 文件夹节点 </returns>
        private static DirectoryNode2 CreateDirectoryNode(string rootPath, DirectoryInfo directoryInfo) {
            try {
                DirectoryNode2 directoryNode = new DirectoryNode2(rootPath, directoryInfo);
                foreach (var directory in directoryInfo.GetDirectories()) {
                    directoryNode.AddDirectoryNode(CreateDirectoryNode(rootPath, directory));
                } // end foreach
                foreach (var file in directoryInfo.GetFiles()) {
                    directoryNode.AddFileNode(new FileNode(rootPath, file));
                } // end foreach
                return directoryNode;
            } catch (PathTooLongException) {
                return null;
            } // end try
        } // end CreateDirectoryNode
        /// <summary>
        /// 建立映射
        /// </summary>
        /// <param name="directoryNode"> 文件夹节点 </param>
        private void MapPathToNode(DirectoryNode2 directoryNode) {
            pathToInfoMap.Add(directoryNode.RelativePath, directoryNode);
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                MapPathToNode(directory);
            } // end foreach
            foreach (var file in directoryNode.GetFileNodes()) {
                pathToInfoMap.Add(file.RelativePath, file);
            } // end foreach
        } // end MapPathToNode
        /// <summary>
        /// 重置过滤
        /// </summary>
        /// <param name="directoryNode"> 文件夹节点 </param>
        private void ResetFilter(DirectoryNode2 directoryNode) {
            directoryNode.IsFilter = false;
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                ResetFilter(directory);
            } // end foreach
            foreach (var file in directoryNode.GetFileNodes()) {
                file.IsFilter = false;
            } // end foreach
        } // end ResetFilter
        /// <summary>
        /// 过滤文件夹节点
        /// </summary>
        /// <param name="directoryNode"> 文件夹节点 </param>
        private static void FilterDirectory(DirectoryNode2 directoryNode) {
            FilterInfoNode(directoryNode);
            foreach (var directory in directoryNode.GetDirectoryNodes()) {
                FilterDirectory(directory);
            } // end foreach
            foreach (var file in directoryNode.GetFileNodes()) {
                FilterInfoNode(file);
            } // end foreach
        } // end FilterDirectory
        /// <summary>
        /// 过滤节点
        /// </summary>
        /// <param name="infoNode"> 节点 </param>
        private static void FilterInfoNode(InfoNode infoNode) {
            if (infoNode is DirectoryNode2) {
                var inList = DataManager.Instance.filterData.InDirectoryList;
                if (inList.Count > 0) {
                    infoNode.IsFilter = true;
                    for (int i = 0; i < inList.Count; ++i) {
                        if (infoNode.Name == inList[i]) {
                            infoNode.IsFilter = false;
                            break;
                        } // end if
                    } // end for
                } // end if
                var exList = DataManager.Instance.filterData.ExDirectoryList;
                for (int i = 0; i < exList.Count; ++i) {
                    if (infoNode.Name == exList[i]) {
                        infoNode.IsFilter = true;
                        return;
                    } // end if
                } // end for
            } else if (infoNode is FileNode) {
                var inList = DataManager.Instance.filterData.InFileList;
                if (inList.Count > 0) {
                    infoNode.IsFilter = true;
                    for (int i = 0; i < inList.Count; ++i) {
                        if (infoNode.Name == inList[i]) {
                            infoNode.IsFilter = false;
                            break;
                        } // end if
                    } // end for
                } // end if
                var exList = DataManager.Instance.filterData.ExFileList;
                for (int i = 0; i < exList.Count; ++i) {
                    if (infoNode.Name == exList[i]) {
                        infoNode.IsFilter = true;
                        return;
                    } // end if
                } // end for
            } // end if
        } // end FilterDirectory
    } // end class TreeModle
} // end namespace CompareWindows.Modle
