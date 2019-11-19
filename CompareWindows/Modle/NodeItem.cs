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
        public int Index { get; set; } = 0;
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
        private bool isDisable1;
        public bool IsDisable1 {
            get { return isDisable1; }
            set {
                isDisable1 = value;
                if (isDisable1) {
                    Brush1 = Define.DisableBrush;
                    if (Parent != null) Parent.CheakDisable1();
                    // end if
                } // end if
            } // end set
        } // end IsDisable1

        private bool isDisable2;
        public bool IsDisable2 {
            get { return isDisable2; }
            set {
                isDisable2 = value;
                if (isDisable2) {
                    Brush2 = Define.DisableBrush;
                    if (Parent != null) Parent.CheakDisable2();
                    // end if
                } // end if
            } // end set
        } // end IsDisable1

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

        public void CheakDisable1() {
            foreach (var item in Childrens) {
                if (!item.IsDisable1) {
                    IsDisable1 = false;
                    return;
                } // end if
            } // end foreach
            IsDisable1 = true;
        } // end CheakDisable1

        public void CheakDisable2() {
            foreach (var item in Childrens) {
                if (!item.IsDisable2) {
                    IsDisable2 = false;
                    return;
                } // end if
            } // end foreach
            IsDisable2 = true;
        } // end CheakDisable2
    }

    public class RootItem : BaseItem {
        public RootItem(string name, int index) {
            ItemPath = name;
            Index = index;
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

        public FolderItem(string name, BaseItem parent, int index) {
            ItemPath = name;
            Parent = parent;
            Index = index;
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

        public FileItem(string name, BaseItem parent, int index) {
            ItemPath = name;
            Parent = parent;
            Index = index;
            if (Parent != null) Parent.AddChildren(this);
            // end if
        }
    }
}
