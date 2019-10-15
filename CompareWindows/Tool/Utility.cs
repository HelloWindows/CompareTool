using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace CompareWindows.Tool {
    public static class Utility {
        public static string GetMD5HashFromFile(string fileName) {
            try {
                FileStream file = new FileStream(fileName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
                return BitConverter.ToString(retVal).ToLower().Replace("-", "");
            } catch (Exception) {
                throw;
            } // end try
        } // end GetMD5HashFromFile
        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="rootPath"> 根路径 </param>
        /// <param name="fullPath"> 全路径 </param>
        /// <returns> 相对路径 </returns>
        public static string GetRelativePath(string rootPath, string fullPath) {
            string path = fullPath.Substring(rootPath.Length);
            return path;
        } // end GetRelativePath
    } // end class Utilite
} // end namespace CompareWindows.Tool
