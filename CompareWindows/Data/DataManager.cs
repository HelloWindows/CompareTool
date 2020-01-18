using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CompareWindows.Data {
    public class DataManager {
        private const string dataPath = "dataManager.data";
        private static DataManager instance;
        public static DataManager Instance {
            get {
                if (null == instance) instance = new DataManager();
                // end if
                return instance;
            } // end get
        } // end Instance

        public ComboBoxData comboBoxData { get; private set; }
        public FilterData filterData { get; private set; }
        public CompareProgressData compareProgressData { get; private set; }
        public string BeyondComparePath { get; private set; }

        private DataManager() {
            comboBoxData = new ComboBoxData();
            filterData = new FilterData();
            compareProgressData = new CompareProgressData();
            string path = Path.Combine(Application.LocalUserAppDataPath, dataPath);
            if (File.Exists(path)) {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate))) {
                    BeyondComparePath = reader.ReadString();
                } // end using
            } // end if
        } // end DataManager

        public void SetBeyondComparePath(string path) {
            if (string.IsNullOrEmpty(path)) return;
            // end if
            BeyondComparePath = path;
            string savePath = Path.Combine(Application.LocalUserAppDataPath, dataPath);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
                writer.Write(path);
            } // end using
        }
    }
}
