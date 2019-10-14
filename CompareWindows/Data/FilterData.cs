using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.Data {
    public class FilterData {
        private const string saveFile = "filter_data.data";

        public string inFileText { get; private set; }
        public string inDirectoryText { get; private set; }
        public string exFileText { get; private set; }
        public string exDirectoryText { get; private set; }

        public List<string> InFileList { get; private set; }
        public List<string> InDirectoryList { get; private set; }
        public List<string> ExFileList { get; private set; }
        public List<string> ExDirectoryList { get; private set; }

        public FilterData() {
            InFileList = new List<string>();
            InDirectoryList = new List<string>();
            ExFileList = new List<string>();
            ExDirectoryList = new List<string>();
            Clear();
        } // end FilterData

        private void Read() {
            string path = Path.Combine(Application.LocalUserAppDataPath, saveFile);
            if (File.Exists(path)) {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate))) {
                    inFileText = reader.ReadString();
                    inDirectoryText = reader.ReadString();
                    exFileText = reader.ReadString();
                    exDirectoryText = reader.ReadString();
                } // end using
            } // end if
        } // end Read

        public void Save() {
            string path = Path.Combine(Application.LocalUserAppDataPath, saveFile);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
                writer.Write(inFileText);
                writer.Write(inDirectoryText);
                writer.Write(exFileText);
                writer.Write(exDirectoryText);
            } // end using
        } // end Save

        public void Clear() {
            InFileList.Clear();
            InDirectoryList.Clear();
            ExFileList.Clear();
            ExDirectoryList.Clear();
            inFileText = "";
            inDirectoryText = "";
            exFileText = "";
            exDirectoryText = "";
        } // end Clear

        public bool SetData(string inFileText, string inDirectoryText, string exFileText, string exDirectoryText) {
            HashSet<string> set = new HashSet<string>();
            string[] inFileSections = inFileText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < inFileSections.Length; ++i) {
                inFileSections[i] = inFileSections[i].Trim();
                if (!set.Add(inFileSections[i])) return false;
                // end if
            } // end for
            string[] exFileSections = exFileText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < exFileSections.Length; ++i) {
                exFileSections[i] = exFileSections[i].Trim();
                if (!set.Add(exFileSections[i])) return false;
                // end if
            } // end for
            set.Clear();
            string[] inDirectorySections = inDirectoryText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < inDirectorySections.Length; ++i) {
                inDirectorySections[i] = inDirectorySections[i].Trim();
                if (!set.Add(inDirectorySections[i])) return false;
                // end if
            } // end for
            string[] exDirectorySections = exDirectoryText.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < exDirectorySections.Length; ++i) {
                exDirectorySections[i] = exDirectorySections[i].Trim();
                if (!set.Add(exDirectorySections[i])) return false;
                // end if
            } // end for
            Clear();
            this.inFileText = inFileText;
            this.inDirectoryText = inDirectoryText;
            this.exFileText = exFileText;
            this.exDirectoryText = exDirectoryText;
            InFileList.AddRange(inFileSections);
            InDirectoryList.AddRange(inDirectorySections);
            ExFileList.AddRange(exFileSections);
            ExDirectoryList.AddRange(exDirectorySections);
            return true;
        } // end SetData
    } // end class FilterData
} // end CompareWindows.Data
