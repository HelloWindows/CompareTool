using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public abstract class BaseItem {
        public abstract string Name { get; set; }

        public string ItemPath { get; set; } = "";
        public Image Icon { get; set; }
        public string Size { get; set; } = "";
        public string Date { get; set; } = "";
        public Brush Brush { get; set; } = SystemBrushes.ControlText;
        public BaseItem Parent { get; set; }
    }
}
