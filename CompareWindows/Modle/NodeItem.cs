using CompareWindows.Config;
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
        public Brush Brush2 { get; set; } = SystemBrushes.ControlText;
        public BaseItem Parent { get; set; }
        private List<BaseItem> Childrens = new List<BaseItem>();
        private bool isSame;
        public bool IsSame {
            get { return isSame; }
            set {
                isSame = value;
                if (isSame) {
                    Brush1 = Brush2 = Define.SameBrush;
                    if (Parent != null) Parent.CheakSame();
                    // end if
                } else {
                    Brush1 = Brush2 = Define.DefferentBrush;
                    if (Parent != null) Parent.CheakSame();
                    // end if
                }// end if
            }
        }

        public void AddChildren(BaseItem item) {
            Childrens.Add(item);
        } // end AddChildrens

        public void RemoveChildren(BaseItem item) {
            Childrens.Remove(item);
        } // end RemoveChildren

        public void CheakSame() {
            foreach (var item in Childrens) {
                if (!item.IsSame) {
                    IsSame = false;
                    return;
                } // end if
            } // end foreach
            IsSame = true;
        } // end CheakSame
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
            if (Parent != null) Parent.AddChildren(this);
            // end if
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
            if (Parent != null) Parent.AddChildren(this);
            // end if
        }
    }
}
