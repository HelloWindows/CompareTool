using CompareWindows.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class DirectoryModle {
        public static Dictionary<string, DirectoryNode> DirectoryMap { get; } = new Dictionary<string, DirectoryNode>();

        public static int TotalFileCount {
            get {
                int total = 0;
                foreach (var item in DirectoryMap) {
                    total += item.Value.FileCount;
                } // end foreach
                return total;
            } // end get
        } // end TotalFileCount

        public static void Reset(string leftRoot, string rightRoot) {
            DirectoryMap.Clear();
            AddDirectory(leftRoot, new DirectoryInfo(leftRoot));
            AddDirectory(rightRoot, new DirectoryInfo(rightRoot));
            foreach (var item in DirectoryMap) {
                item.Value.Sort();
            } // end foreach
        } // end Reset

        private static void AddDirectory(string rootPath, DirectoryInfo directory) {
            try {
                string path = Utility.GetRelativePath(rootPath, directory.FullName);
                DirectoryNode node;
                if (DirectoryMap.ContainsKey(path)) {
                    node = DirectoryMap[path];
                } else {
                    node = new DirectoryNode();
                    DirectoryMap.Add(path, node);
                } // end if
                foreach (var info in directory.GetDirectories()) {
                    AddDirectory(rootPath, info);
                    string relativePath = Utility.GetRelativePath(rootPath, info.FullName);
                    node.AddDirectory(relativePath);
                } // end foreach
                foreach (var info in directory.GetFiles()) {
                    string relativePath = Utility.GetRelativePath(rootPath, info.FullName);
                    node.AddFile(relativePath);
                } // end foreach
            } catch (PathTooLongException) {
            } // end try
        } // end AddDirectory
    } // end class DirectoryModle

    public class DirectoryNode {
        private bool isChangedFile = false;
        private bool isChangedDirectory = false;
        private List<string> directoryList = new List<string>();
        private List<string> fileList = new List<string>();
        public int FileCount { get { return fileList.Count; } }

        public DirectoryNode() {
        } // end DirectoryNode

        public IEnumerable<string> GetDirectorys() { return directoryList; }
        public IEnumerable<string> GetFiles() { return fileList; }

        public void AddDirectory(string path) {
            if (directoryList.Contains(path)) return;
            // end if
            directoryList.Add(path);
            isChangedDirectory = true;
        } // end AddDirectory

        public void AddFile(string path) {
            if (fileList.Contains(path)) return;
            // end if
            fileList.Add(path);
            isChangedFile = true;
        } // end AddFile

        public void Sort() {
            if (isChangedDirectory) {
                isChangedDirectory = false;
                directoryList.Sort(string.Compare);
            } // end if
            if (isChangedFile) {
                isChangedFile = false;
                fileList.Sort(string.Compare);
            } // end if
        } // end Sort
    } // end DirectoryNode
}
