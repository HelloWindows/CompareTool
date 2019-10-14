using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace CompareWindows.Modle {
    public class InfoNode {
        public FileSystemInfo fileSystemInfo { get; private set; }
        public string Name { get { return fileSystemInfo.Name; } }
        public string FullPath { get { return fileSystemInfo.FullName; } }
        public string RelativePath { get; private set; }
        public bool IsShow { get; private set; }

        public InfoNode(string rootPath, FileSystemInfo fileSystemInfo) {
            this.fileSystemInfo = fileSystemInfo;
            RelativePath = Utilite.GetRelativePath(rootPath, fileSystemInfo.FullName);
            IsShow = true;
        } // end InfoNode

        /// <summary>
        /// 设置是否显示
        /// </summary>
        /// <param name="isShow"> 是否显示 </param>
        public void SetShow(bool isShow) {
            IsShow = isShow;
        } // end SetShow
    } // end class InfoNode
} // end namespace CompareWindows.Modle