using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CompareWindows.Data {
    public class DataManager {
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

        public DataManager() {
            comboBoxData = new ComboBoxData();
            filterData = new FilterData();
        } // end DataManager
    }
}
