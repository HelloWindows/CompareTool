using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Algorithm.LinkList {
    public class LinkList<T> : IEnumerable {
        public Node<T> Head { set; get; } //单链表头

        //构造
        public LinkList() {
            Clear();
        } // end OrderLinkList

        /// <summary>
        /// 求单链表的长度
        /// </summary>
        /// <returns></returns>
        public int GetLength() {
            Node<T> p = Head;
            int length = 0;
            while (p != null) {
                p = p.Next;
                length++;
            } // end while
            return length;
        } // end GetLength

        /// <summary>
        /// 判断单键表是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() {
            if (Head == null) return true;
            // end if
            return false;
        } // end IsEmpty

        /// <summary>
        /// 清空单链表
        /// </summary>
        public void Clear() {
            Head = null;
        } // end CLear

        /// <summary>
        /// 获得当前位置单链表中结点的值
        /// </summary>
        /// <param name="i">结点位置</param>
        /// <returns></returns>
        public T GetNodeValue(int i) {
            if (IsEmpty() || i < 1 || i > GetLength()) {
                Console.WriteLine("单链表为空或结点位置有误！");
                return default(T);
            } // end if
            Node<T> A = new Node<T>();
            A = Head;
            int j = 1;
            while (A.Next != null && j < i) {
                A = A.Next;
                j++;
            } // end while
            return A.Data;
        } // end GetNodeValue

        /// <summary>
        /// 增加新元素到单链表末尾
        /// </summary>
        public void Append(T item) {
            Node<T> foot = new Node<T>(item);
            Node<T> A = new Node<T>();
            if (Head == null) {
                Head = foot;
                return;
            } // end if
            A = Head;
            while (A.Next != null) {
                A = A.Next;
            } // end while
            A.Next = foot;
        } // end Append

        /// <summary>
        /// 增加单链表插入的位置
        /// </summary>
        /// <param name="item">结点内容</param>
        /// <param name="n">结点插入的位置</param>
        public void Insert(T item, int n) {
            if (IsEmpty() || n < 1 || n > GetLength()) {
                Console.WriteLine("单链表为空或结点位置有误！");
                return;
            } // end if
            if (n == 1) { //增加到头部
                Node<T> H = new Node<T>(item);
                H.Next = Head;
                Head = H;
                return;
            } // end if
            Node<T> A = new Node<T>();
            Node<T> B = new Node<T>();
            B = Head;
            int j = 1;
            while (B.Next != null && j < n) {
                A = B;
                B = B.Next;
                j++;
            } // end while
            if (j == n) {
                Node<T> C = new Node<T>(item);
                A.Next = C;
                C.Next = B;
            } // end if
        } // end Insert

        /// <summary>
        /// 删除单链表结点
        /// </summary>
        /// <param name="i">删除结点位置</param>
        /// <returns></returns>
        public void Delete(int i) {
            if (IsEmpty() || i < 1 || i > GetLength()) {
                Console.WriteLine("单链表为空或结点位置有误！");
                return;
            } // end if
            Node<T> A = new Node<T>();
            if (i == 1) { //删除头
                A = Head;
                Head = Head.Next;
                return;
            } // end if
            Node<T> B = new Node<T>();
            B = Head;
            int j = 1;
            while (B.Next != null && j < i) {
                A = B;
                B = B.Next;
                j++;
            } // end while
            if (j == i) {
                A.Next = B.Next;
            } // end if
        } // end Delete

        /// <summary>
        /// 单链表反转
        /// </summary>
        public void Reverse() {
            if (GetLength() == 1 || Head == null) return;
            // end if
            Node<T> NewNode = null;
            Node<T> CurrentNode = Head;
            Node<T> TempNode = new Node<T>();
            while (CurrentNode != null) {
                TempNode = CurrentNode.Next;
                CurrentNode.Next = NewNode;
                NewNode = CurrentNode;
                CurrentNode = TempNode;
            } // end while
            Head = NewNode;
        } // end Reverse

        /// <summary>
        /// 获得单链表中间值
        /// 思路：使用两个指针，第一个每次走一步，第二个每次走两步：
        /// </summary>
        public void GetMiddleValue() {
            Node<T> A = Head;
            Node<T> B = Head;
            while (B != null && B.Next != null) {
                A = A.Next;
                B = B.Next.Next;
            } // end while
            if (B != null) { //奇数
                Console.WriteLine("奇数:中间值为：{0}", A.Data);
            }
            else { //偶数
                Console.WriteLine("偶数:中间值为：{0}和{1}", A.Data, A.Next.Data);
            } // end if
        } // end GetMiddleValue

        /// <summary>
        /// 链表合并
        /// </summary>
        /// <param name="nodeA"></param>
        /// <param name="nodeB"></param>
        /// <returns></returns>
        public static Node<int> Merge(Node<int> nodeA, Node<int> nodeB) {
            if (nodeA == null) {
                return nodeB;
            } else if (nodeB == null) {
                return nodeA;
            } // end if
            Node<int> newHead = null;
            if (nodeA.Data <= nodeB.Data) {
                newHead = nodeA;
                newHead.Next = Merge(nodeA.Next, nodeB);
            } else {
                newHead = nodeB;
                newHead.Next = Merge(nodeA, nodeB.Next);
            } // end if
            return newHead;
        } // end Merge

        public IEnumerator GetEnumerator() {
            return new LinkListEnumerator(Head);
        } // end GetEnumerator

        private class LinkListEnumerator : IEnumerator<T> {
            private Node<T> node;
            public T Current {
                get {
                    if (null != node) return node.Data;
                    // end if
                    return default(T);
                } // end get
            } // end Current
            object IEnumerator.Current { get { return Current; } }

            public LinkListEnumerator(Node<T> head) {
                node = new Node<T>();
                node.Next = head;
            } // end LinkListEnumerator

            public void Dispose() {
                node = null;
            } // end Dispose

            public bool MoveNext() {
                if (null != node) {
                    node = node.Next;
                    return true;
                } // end if
                return false;
            } // end MoveNext

            public void Reset() {
                node = null;
            } // end Reset
        } // end class LinkListEnumerator
    } // end class LinkList
} // end namespace CompareWindows.Algorithm.LinkList
