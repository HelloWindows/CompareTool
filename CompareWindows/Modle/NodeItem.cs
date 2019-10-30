using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public abstract class BaseItem {
        public abstract string Name { get; set; }
        public string ItemPath { get; set; } = "";

        public Image Icon1 { get; set; }
        public string Size1 { get; set; } = "";
        public string Date1 { get; set; } = "";
        public Brush Brush1 { get; set; } = SystemBrushes.ControlText;
        public Image Icon2 { get; set; }
        public string Size2 { get; set; } = "";
        public string Date2 { get; set; } = "";
        public Brush Bursh2 { get; set; } = SystemBrushes.ControlText;
        public BaseItem Parent { get; set; }
    }

    public class RootItem : BaseItem {
        public RootItem(string name) {
            ItemPath = name;
        }

        public override string Name {
            get {
                return Path.GetFileName(ItemPath);
            }
            set {
                ItemPath = value;
            }
        }
    }

    public class FolderItem : BaseItem {
        public override string Name {
            get {
                return Path.GetFileName(ItemPath);
            }
            set {
                ItemPath = value;
            }
        }

        public FolderItem(string name, BaseItem parent) {
            ItemPath = name;
            Parent = parent;
        }
    }

    public class FileItem : BaseItem {
        public override string Name {
            get {
                return Path.GetFileName(ItemPath);
            }
            set {
                ItemPath = value;
            }
        }

        public FileItem(string name, BaseItem parent) {
            ItemPath = name;
            Parent = parent;
        }
    }
}
