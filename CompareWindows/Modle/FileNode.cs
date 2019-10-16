using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class FileNode : InfoNode {
        public FileInfo fileInfo { get; private set; }

        public FileNode(string rootPath, FileInfo fileInfo) : base(rootPath, fileInfo) {
            this.fileInfo = fileInfo;
        } // end FileNode
    } // end class FileNode
} // end CompareWindows.Modle
