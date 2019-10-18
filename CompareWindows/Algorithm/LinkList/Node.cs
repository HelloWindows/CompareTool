using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Algorithm.LinkList {
    public class Node<T> {
        public T Data { set; get; }          //数据域,当前结点数据
        public Node<T> Next { set; get; }    //位置域,下一个结点地址

        public Node(T item) {
            Data = item;
            Next = null;
        } // end Node

        public Node() {
            Data = default(T);
            Next = null;
        } // end Node
    } // end class Node
} // end namespace CompareWindows.Algorithm.LinkList
