using Aga.Controls.Tree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class TreeModle : ITreeModel {
        public event EventHandler<TreeModelEventArgs> NodesChanged;
        public event EventHandler<TreeModelEventArgs> NodesInserted;
        public event EventHandler<TreeModelEventArgs> NodesRemoved;
        public event EventHandler<TreePathEventArgs> StructureChanged;



        public IEnumerable GetChildren(TreePath treePath) {
            throw new NotImplementedException();
        }

        public bool IsLeaf(TreePath treePath) {
            throw new NotImplementedException();
        }
    }
}
