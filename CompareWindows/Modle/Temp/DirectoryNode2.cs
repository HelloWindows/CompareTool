using CompareWindows.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class DirectoryNode2 : InfoNode {
        public DirectoryInfo directoryInfo { get; private set; }
        private SortedList<string, FileNode> fileNodes;
        private SortedList<string, DirectoryNode2> directoryNodes;

        public DirectoryNode2(string rootPath, DirectoryInfo directoryInfo) : base(rootPath, directoryInfo) {
            this.directoryInfo = directoryInfo;
            fileNodes = new SortedList<string, FileNode>();
            directoryNodes = new SortedList<string, DirectoryNode2>();
        }

        public DirectoryNode2(string relativePath) : base(relativePath) {
            fileNodes = new SortedList<string, FileNode>();
            directoryNodes = new SortedList<string, DirectoryNode2>();
        } // end DirectoryNode
        /// <summary>
        /// 获取子文件夹节点列表
        /// </summary>
        /// <returns> 子文件夹节点列表 </returns>
        public IList<DirectoryNode2> GetDirectoryNodes() { return directoryNodes.Values; }
        /// <summary>
        /// 添加子文件夹节点
        /// </summary>
        /// <param name="node"> 子文件夹节点 </param>
        public void AddDirectoryNode(DirectoryNode2 node) {
            if (null == node) return;
            // end if
            directoryNodes.Add(node.RelativePath, node);
        } // end AddDirectoryNode
        /// <summary>
        /// 获取子文件节点列表
        /// </summary>
        /// <returns> 子文件节点列表 </returns>
        public IList<FileNode> GetFileNodes() { return fileNodes.Values; }
        /// <summary>
        /// 添加子文件节点
        /// </summary>
        /// <param name="node"> 子文件节点 </param>
        public void AddFileNode(FileNode node) {
            if (null == node) return;
            // end if
            fileNodes.Add(node.RelativePath, node);
        } // end AddFileNode

        public void AddEmptyDirectory(DirectoryNode2 directory) {
            if (string.IsNullOrEmpty(directory.RelativePath)) throw new Exception();
            // end if
            directoryNodes.Add(directory.RelativePath, directory);
        } // end AddEmptyDirectory

        public void AddEmptyFile(FileNode file) {
            if (string.IsNullOrEmpty(file.RelativePath)) throw new Exception();
            // end if
            fileNodes.Add(file.RelativePath, file);
        } // end AddEmptyFile
    }
}
