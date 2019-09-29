using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.Data {
    public class ComboBoxData {
        public const string itemlist1 = "itemlist.data";

        public List<string> ComboBoxItemList1 { get; private set; }
        public List<string> ComboBoxItemList2 { get; private set; }

        public ComboBoxData() {
            ComboBoxItemList1 = new List<string>();
            ComboBoxItemList2 = new List<string>();
            string path = Path.Combine(Application.LocalUserAppDataPath, itemlist1);
            if (File.Exists(path)) {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.OpenOrCreate))) {
                    int count = reader.ReadInt32();
                    for (int i = 0; i < count; i++) {
                        string item = reader.ReadString();
                        ComboBoxItemList1.Add(item);
                    } // end for
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++) {
                        string item = reader.ReadString();
                        ComboBoxItemList2.Add(item);
                    } // end for
                } // end using
            } // end if
        } // end ComboBoxData

        public void Save() {
            string path = Path.Combine(Application.LocalUserAppDataPath, itemlist1);
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
                int count = ComboBoxItemList1.Count;
                count = count > 10 ? 10 : count;
                writer.Write(count);
                for (int i = 0; i < count; i++) {
                    writer.Write(ComboBoxItemList1[i]);
                } // end for
                count = ComboBoxItemList2.Count;
                count = count > 10 ? 10 : count;
                writer.Write(count);
                for (int i = 0; i < count; i++) {
                    writer.Write(ComboBoxItemList2[i]);
                } // end for
            } // end using
        } // end Save

        public void SelectPath1(string path) {
            if (ComboBoxItemList1.Contains(path)) {
                ComboBoxItemList1.Remove(path);
                ComboBoxItemList1.Insert(0, path);
            } else {
                ComboBoxItemList1.Insert(0, path);
            } // end if
            Save();
        } // end SelectPath1

        public void SelectPath2(string path) {
            if (ComboBoxItemList2.Contains(path)) {
                ComboBoxItemList2.Remove(path);
                ComboBoxItemList2.Insert(0, path);
            } else {
                ComboBoxItemList2.Insert(0, path);
            } // end if
            Save();
        } // end SelectPath2
    } // end class ComboBoxData
} // end namespace CompareWindows.Data
