using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace CompareWindows {
    static class Utilite {
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
        }
    }
}
