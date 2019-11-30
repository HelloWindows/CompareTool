using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SharpSvn;
using CompareWindows.Modle;
using CompareWindows.Event;

namespace CompareWindows.View.Control {
    public partial class SvnListBox : UserControl {

        private bool gridLines;
        [DefaultValue(false), Category("Appearance")]
        public bool GridLines {
            get { return gridLines; }
            set {
                gridLines = value;
                listView1.GridLines = gridLines;
                listView2.GridLines = gridLines;
            }
        } // end ShowGrid
        private SvnListModel model;
        public SvnListModel Model {
            get { return model; }
            set {
                UnBindSvnEvent();
                model = value;
                BindSvnEvent();
            }
        }
        private List<ListViewItem> cacheItems1;
        private List<ListViewItem> cacheItems2;

        public SvnListBox() {
            InitializeComponent();
            cacheItems1 = new List<ListViewItem>();
            cacheItems2 = new List<ListViewItem>();
            listView1.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView1_RetrieveVirtualItem);
            listView2.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView2_RetrieveVirtualItem);
        }

        public void Clear() {
            listView1.Items.Clear();
            listView2.Items.Clear();
        } // end Clear

        private void ShowItem(ListView listView, List<ListViewItem> itemList, Collection<SvnLogEventArgs> status) {
            listView.Items.Clear();
            listView.VirtualListSize = 0;
            if (status == null) return;
            // end if
            SvnLogEventArgs data;
            ListViewItem item;
            for (int i = 0; i < status.Count; ++i) {
                data = status[i];
                if (itemList.Count <= i) {
                    item = new ListViewItem();
                    itemList.Add(item);
                } else {
                    item = itemList[i];
                } // end if
                item.Name = data.Revision.ToString();
                item.SubItems.Add(data.Author);
                item.SubItems.Add(data.Time.ToString());
                item.SubItems.Add(data.LogMessage);
            } // end for
            listView.VirtualListSize = status.Count;
        } // end ShowItem

        private void OnStopSvnLoad(object sender, EventArgs e) {
            Clear();
        } // end OnStopSvnLoad

        private void OnSvnInfoReady(object sender, SvnDataEventArgs e) {
            ShowItem(listView1, cacheItems1, e.LeftStatus);
            ShowItem(listView2, cacheItems2, e.RightStatus);
        } // end OnSvnInfoReady

        private void BindSvnEvent() {
            if (model == null) return;
            // end if
            model.SvnDataReady += OnSvnInfoReady;
            model.StopSvnLoad += OnStopSvnLoad;
        } // end BindSvnEvent

        private void UnBindSvnEvent() {
            if (model == null) return;
            // end if
            model.SvnDataReady -= OnSvnInfoReady;
            model.StopSvnLoad -= OnStopSvnLoad;
        } // end UnBindSvnEvent

        private void listView1_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            if (cacheItems1 == null || e.ItemIndex < 0 || cacheItems1.Count < e.ItemIndex) {
                return;
            } // end if
            e.Item = cacheItems1[e.ItemIndex];
        } // end listView1_RetrieveVirtualItem

        private void listView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e) {
            if (cacheItems2 == null || e.ItemIndex < 0 || cacheItems2.Count < e.ItemIndex) {
                return;
            } // end if
            e.Item = cacheItems2[e.ItemIndex];
        } // end listView2_RetrieveVirtualItem

        protected override void OnSizeChanged(EventArgs e) {
            base.OnSizeChanged(e);
            float[] widthArr = new float[4];
            widthArr[0] = 60f / 434f;
            widthArr[1] = 80f / 434f;
            widthArr[2] = 120f / 434f;
            widthArr[3] = 170f / 434f;
            for (int i = 0; i < widthArr.Length; ++i) {
                listView1.Columns[i].Width = (int)(widthArr[i] * listView1.Width);
                listView2.Columns[i].Width = (int)(widthArr[i] * listView2.Width);
            } // end for
        }
    }
}
