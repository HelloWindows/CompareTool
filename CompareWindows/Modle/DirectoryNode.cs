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
            fileNodes.Add(node);
        } // end AddFileNode
        /// <summary>
        /// 创角文件夹节点
        /// </summary>
        /// <param name="rootPath"> 根路径 </param>
        /// <param name="directoryInfo"> 文件夹 </param>
        /// <returns> 文件夹节点 </returns>
        public static DirectoryNode CreateDirectoryNode(string rootPath, DirectoryInfo directoryInfo) {
            DirectoryNode directoryNode = new DirectoryNode(rootPath, directoryInfo);
            foreach (var directory in directoryInfo.GetDirectories()) {
                directoryNode.AddDirectoryNode(CreateDirectoryNode(rootPath, directory));
            } // end foreach
            foreach (var file in directoryInfo.GetFiles()) {
                directoryNode.AddFileNode(new FileNode(rootPath, file));
            } // end foreach
            return directoryNode;
        } // end CreateDirectoryNode
    }
}
