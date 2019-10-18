using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using CompareWindows.Tool;
using System.Drawing;
using CompareWindows.Config;
using SharpSvn;
using System.Collections.ObjectModel;

namespace CompareWindows.Modle {
    public class InfoNode : IComparable<InfoNode> {
        /// <summary>
        /// 文件系统信息
        /// </summary>
        public FileSystemInfo fileSystemInfo { get; private set; }
        /// <summary>
        /// 文件名（含后缀）
        /// </summary>
        public string Name { get { return fileSystemInfo == null ? string.Empty : fileSystemInfo.Name; } }
        /// <summary>
        /// 全路径
        /// </summary>
        public string FullPath { get { return fileSystemInfo == null ? string.Empty : fileSystemInfo.FullName; } }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string RelativePath { get; private set; }
        /// <summary>
        /// 是否被过滤
        /// </summary>
        public bool IsFilter { get; set; }
        /// <summary>
        /// 是否相同
        /// </summary>
        public bool IsSame {
            get { return isSame; }
            set {
                isSame = value;
                if (isSame) {
                    color = Define.SameColor;
                } else {
                    color = Define.DefferentColor;
                } // end if
            }  // end set
        } // end IsSame
        private bool isSame;

        public bool IsSpecial {
            get { return isSpecial; }
            set {
                isSpecial = value;
                if (isSpecial) {
                    color = Define.SpecialColor;
                } // end if
            } // end set
        } // end IsSpecial
        private bool isSpecial;
        /// <summary>
        /// 显示颜色
        /// </summary>
        public Color color { get; private set; }
        /// <summary>
        /// SVN 信息
        /// </summary>
        public Collection<SvnLogEventArgs> svnLogStatus { get; set; }

        public InfoNode(string rootPath, FileSystemInfo fileSystemInfo) {
            this.fileSystemInfo = fileSystemInfo;
            RelativePath = Utility.GetRelativePath(rootPath, fileSystemInfo.FullName);
            ResetCompare();
            IsFilter = false;
            svnLogStatus = null;
        } // end InfoNode

        public InfoNode(string relativePath) {
            fileSystemInfo = null;
            RelativePath = relativePath;
            ResetCompare();
            IsFilter = false;
            svnLogStatus = null;
        } // end InfoNode

        public void ResetCompare() {
            IsSame = false;
            IsSpecial = false;
            color = Define.DefaultColor;
        } // end ResetCompare

        public int CompareTo(InfoNode other) {
            int value = string.Compare(RelativePath, other.RelativePath);
            return value;
        } // end CompareTo
    } // end class InfoNode
} // end namespace CompareWindows.Modle