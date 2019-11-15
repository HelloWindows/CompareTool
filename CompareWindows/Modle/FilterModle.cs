using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public static class FilterModle {
        public static string fileStr { get; private set; } = string.Empty;
        public static string folderStr { get; private set; } = string.Empty;
        private static HashSet<string> fileList = new HashSet<string>();
        private static HashSet<string> folderList = new HashSet<string>();

        public static bool IsFilterFile(string file) {
            return fileList.Contains(file);
        } // end IsFilterFile

        public static bool IsFilterFolder(string folder) {
            return folderList.Contains(folder);
        } // end IsFilterFolder

        public static void Clear() {
            fileStr = string.Empty;
            folderStr = string.Empty;
            fileList.Clear();
            folderList.Clear();
        } // end Clear

        public static void SetFileStr(string str) {
            if (fileStr == str) return;
            // end if
            fileStr = str;
            fileList.Clear();
            if (string.IsNullOrEmpty(fileStr)) return;
            // end if
            string[] list = fileStr.Split('\n');
            foreach (string file in list) {
                if (string.IsNullOrEmpty(file)) continue;
                // end if
                fileList.Add(file);
            } // end foreach
        } // end SetFileStr

        public static void SetFolderStr(string str) {
            if (folderStr == str) return;
            // end if
            folderStr = str;
            folderList.Clear();
            if (string.IsNullOrEmpty(folderStr)) return;
            // end if
            string[] list = folderStr.Split('\n');
            foreach (string folder in list) {
                if (string.IsNullOrEmpty(folder)) continue;
                // end if
                folderList.Add(folder);
            } // end foreach
        } // end SetFolderStr

    } // end class FilterModle
} // end namespace CompareWindows.Modle
