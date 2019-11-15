using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.Data {
    public class FilterData {
        private const string saveFile = "filter_data.data";

        private string svnExtensionStr;
        public string SvnExtensionStr {
            get {
                return svnExtensionStr;
            }
            set {
                if (value == svnExtensionStr) return;
                // end if
                svnExtensionStr = value;
                Save();
            }
        }

        public FilterData() {
            Read();
        } // end FilterData

        private void Read() {
            string path = Path.Combine(Application.LocalUserAppDataPath, saveFile);
            if (File.Exists(path)) {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate))) {
                    svnExtensionStr = reader.ReadString();
                } // end using
            } // end if
        } // end Read

        public void Save() {
            string path = Path.Combine(Application.LocalUserAppDataPath, saveFile);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
                writer.Write(svnExtensionStr);
            } // end using
        } // end Save
    } // end class FilterData
} // end CompareWindows.Data
