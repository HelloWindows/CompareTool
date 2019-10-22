using CompareWindows.Algorithm.LinkList;
using CompareWindows.Tool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class NodeList {
        private HashSet<string> pathSet;
        private LinkList<string> pathList;
        private LinkList<string> otherList;
        public string leftRootPath { get; private set; }
        public string rightRootPath { get; private set; }

        public NodeList(string leftRootPath, string rightRootPath) {
            pathSet = new HashSet<string>();
            pathList = new LinkList<string>();
            otherList = new LinkList<string>();
            this.leftRootPath = leftRootPath;
            this.rightRootPath = rightRootPath;
            if (string.IsNullOrEmpty(leftRootPath) && string.IsNullOrEmpty(rightRootPath)) return;
            // end if
            if (string.IsNullOrEmpty(leftRootPath)) {
                DirectoryInfo info = new DirectoryInfo(rightRootPath);
                InsertPath(rightRootPath, info);
                return;
            } // end if 
            if (string.IsNullOrEmpty(rightRootPath)) {
                DirectoryInfo info = new DirectoryInfo(leftRootPath);
                InsertPath(leftRootPath, info);
                return;
            } // end if
            DirectoryInfo leftInfo = new DirectoryInfo(leftRootPath);
            InsertPath(leftRootPath, leftInfo);
            DirectoryInfo rightInfo = new DirectoryInfo(rightRootPath);
            InsertOther(rightRootPath, rightInfo);
            pathList.Head = LinkList<string>.Merge(pathList.Head, otherList.Head);
            foreach (string item in pathList) {
                continue;
            } // emd 
        } // end NodeList


        /// <summary>
        /// 创角文件夹节点
        /// </summary>
        /// <param name="rootPath"> 根路径 </param>
        /// <param name="directoryInfo"> 文件夹 </param>
        /// <returns> 文件夹节点 </returns>
        private void InsertPath(string rootPath, DirectoryInfo directoryInfo) {
            try {
                string path = Utility.GetRelativePath(rootPath, directoryInfo.FullName);
                if (!pathSet.Add(path)) {
                    throw new Exception("重复路径：" + path);
                } // end if
                pathList.Append(path);
                foreach (var directory in directoryInfo.GetDirectories()) {
                    InsertPath(rootPath, directory);
                } // end foreach
                foreach (var file in directoryInfo.GetFiles()) {
                    string filePath = Utility.GetRelativePath(rootPath, file.FullName);
                    if (!pathSet.Add(filePath)) {
                        throw new Exception("重复路径：" + filePath);
                    } // end if
                    pathList.Append(filePath);
                } // end foreach
            } catch (PathTooLongException) {
                throw;
            } // end try
        } // end CreateDirectoryNode

        private void InsertOther(string rootPath, DirectoryInfo directoryInfo) {
            try {
                string path = Utility.GetRelativePath(rootPath, directoryInfo.FullName);
                if (pathSet.Add(path)) {
                    otherList.Append(path);
                } // end if
                foreach (var directory in directoryInfo.GetDirectories()) {
                    InsertOther(rootPath, directory);
                } // end foreach
                foreach (var file in directoryInfo.GetFiles()) {
                    string filePath = Utility.GetRelativePath(rootPath, directoryInfo.FullName);
                    if (pathSet.Add(filePath)) {
                        otherList.Append(path);
                    } // end if
                } // end foreach
            } catch (PathTooLongException) {
                throw;
            } // end try
        } // end InsertOther
    } // end class NodeList
} // end namespace CompareWindows.Modle