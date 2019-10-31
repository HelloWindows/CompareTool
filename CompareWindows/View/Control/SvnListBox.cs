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

        public SvnListBox() {
            InitializeComponent();
        }

        public void Clear() {
            BeginShow();
            listView1.Items.Clear();
            listView2.Items.Clear();
            EndShow();
        } // end Clear

        private void ShowItem(ListView listView, Collection<SvnLogEventArgs> status) {
            listView.Items.Clear();
            if (status == null) return;
            // end if
            foreach (var info in status) {
                var item = listView.Items.Add(info.Revision.ToString());
                item.SubItems.Add(info.Author);
                item.SubItems.Add(info.Time.ToString());
                item.SubItems.Add(info.LogMessage);
            } // end foreach
        } // end ShowItem

        private void OnStopSvnLoad(object sender, EventArgs e) {
            Clear();
        } // end OnStopSvnLoad

        private void OnSvnInfoReady(object sender, SvnDataEventArgs e) {
            BeginShow();
            ShowItem(listView1, e.LeftStatus);
            ShowItem(listView2, e.RightStatus);
            EndShow();
        } // end OnSvnInfoReady

        private void BeginShow() {
            listView1.BeginUpdate();
            listView2.BeginUpdate();
        } // end BeginShow

        private void EndShow() {
            listView1.EndUpdate();
            listView2.EndUpdate();
        } // end EndShow

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
    }
}
