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

        public DirectoryNode(string rootPath, DirectoryInfo directoryInfo) : base(rootPath, directoryInfo) {
            this.directoryInfo = directoryInfo;
            fileNodes = new List<FileNode>();
            directoryNodes = new List<DirectoryNode>();
        }

        public DirectoryNode(string relativePath) : base(relativePath) {
            fileNodes = new List<FileNode>();
            directoryNodes = new List<DirectoryNode>();
        } // end DirectoryNode
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

        public void AddEmptyDirectory(string relativePath) {
            DirectoryNode node = new DirectoryNode(relativePath);
            directoryNodes.Add(node);
            directoryNodes.Sort((DirectoryNode a, DirectoryNode b) => {
                int value = string.Compare(a.RelativePath, b.RelativePath);
                if (value < 0) {
                    return -1;
                } else if (value > 0) {
                    return 1;
                } // end if
                return 0;
            });
        } // end AddEmptyDirectory

        public void AddEmptyFile(string relativePath) {
            FileNode node = new FileNode(relativePath);
            fileNodes.Add(node);
            fileNodes.Sort((FileNode a, FileNode b) => {
                int value = string.Compare(a.RelativePath, b.RelativePath);
                if (value < 0) {
                    return -1;
                } else if (value > 0) {
                    return 1;
                } // end if
                return 0;
            });
        } // end AddEmptyDirectory
    }
}
