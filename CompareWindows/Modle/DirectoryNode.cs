using CompareWindows.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class DirectoryNode : InfoNode {
        public DirectoryInfo directoryInfo { get; private set; }
        private List<FileNode> fileNodes;
        private List<DirectoryNode> directoryNodes;

        private DirectoryNode(string rootPath, DirectoryInfo directoryInfo) : base(rootPath, directoryInfo) {
            this.directoryInfo = directoryInfo;
            fileNodes = new List<FileNode>();
            directoryNodes = new List<DirectoryNode>();
        }
        /// <summary>
        /// 获取子文件夹节点列表
        /// </summary>
        /// <returns> 子文件夹节点列表 </returns>
        public List<DirectoryNode> GetDirectoryNodes() { return directoryNodes; }
        /// <summary>
        /// 添加子文件夹节点
        /// </summary>
        /// <param name="node"> 子文件夹节点 </param>
        public void AddDirectoryNode(DirectoryNode node) {
            if (null == node) return;
            // end if
            directoryNodes.Add(node);
        } // end AddDirectoryNode
        /// <summary>
        /// 获取子文件节点列表
        /// </summary>
        /// <returns> 子文件节点列表 </returns>
        public List<FileNode> GetFileNodes() { return fileNodes; }
        /// <summary>
        /// 添加子文件节点
        /// </summary>
        /// <param name="node"> 子文件节点 </param>
        public void AddFileNode(FileNode node) {
            if (null == node) return;
            // end if
            fileNodes.Add(node);
        } // end AddFileNode
        /// <summary>
        /// 创角文件夹节点
        /// </summary>
        /// <param name="rootPath"> 根路径 </param>
        /// <param name="directoryInfo"> 文件夹 </param>
        /// <returns> 文件夹节点 </returns>
        public static DirectoryNode CreateDirectoryNode(string rootPath, DirectoryInfo directoryInfo) {
            try {
                DirectoryNode directoryNode = new DirectoryNode(rootPath, directoryInfo);
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
        /// 过滤文件夹节点
        /// </summary>
        /// <param name="directoryNode"> 文件夹节点 </param>
        public static void FilterDirectory(DirectoryNode directoryNode) {
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
        public static void FilterInfoNode(InfoNode infoNode) {
            if (infoNode is DirectoryNode) {
                var list = DataManager.Instance.filterData.ExDirectoryList;
                for (int i = 0; i < list.Count; ++i) {
                    if (infoNode.FullPath.Contains(list[i])) {
                        infoNode.SetShow(false);
                        return;
                    } // end if
                } // end for
            } else if(infoNode is FileNode) {
                var list = DataManager.Instance.filterData.ExFileList;
                for (int i = 0; i < list.Count; ++i) {
                    if (infoNode.Name == list[i]) {
                        infoNode.SetShow(false);
                        return;
                    } // end if
                } // end for
            } // end if
            infoNode.SetShow(true);
        } // end FilterDirectory
    }
}
