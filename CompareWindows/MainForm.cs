using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using CompareWindows.Data;
using CompareWindows.Modle;
using CompareWindows.View;

namespace CompareWindows {
    public partial class MainForm : Form {
        private TreeModle leftTreeModle;
        private TreeModle rightTreeModle;
        private InfoView leftInfoView;
        private InfoView rightInfoView;
        private InfoMenuView leftMenuView;
        private InfoMenuView rightMenuView;

        public MainForm() {
            InitializeComponent();
            Text = GetType().ToString();
            ShowSameMenuItem.Checked = Global.ShowSame;
            leftTreeModle = null;
            rightTreeModle = null;
            leftInfoView = new InfoView(treeView1, LeftNodeMenuStrip);
            rightInfoView = new InfoView(treeView2, null);
            leftMenuView = new InfoMenuView(leftInfoView, rightInfoView, LeftNodeMenuStrip);
            DataManager data = DataManager.Instance;
            foreach (string item in data.comboBoxData.ComboBoxItemList1) {
                comboBox1.Items.Add(item);
            } // end foreach
            foreach (string item in data.comboBoxData.ComboBoxItemList2) {
                comboBox2.Items.Add(item);
            } // end foreach
        }
        /// <summary>
        /// 检测过滤
        /// </summary>
        public void DoDetectFilter() {
            leftTreeModle.DoDetectFilter();
            rightTreeModle.DoDetectFilter();
            leftInfoView.RefreshDisplay(leftTreeModle);
            rightInfoView.RefreshDisplay(rightTreeModle);
        } // end DoDetectFilter

        private List<TreeNode> checkedNodes = new List<TreeNode>();
        private void RemoveCheckedNodes(TreeNodeCollection nodes) {
            foreach (TreeNode node in nodes) {
                if (node.Checked) {
                    checkedNodes.Add(node);
                } else {
                    RemoveCheckedNodes(node.Nodes);
                } // end if
            } // end foreach
            foreach (TreeNode checkedNode in checkedNodes) {
                nodes.Remove(checkedNode);
            } // end foreach
        } // end RemoveCheckedNodes

        private void button1_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog1.SelectedPath;
                comboBox1.Text = path;
                comboBox1.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath1(path);
                RefeashDispaly(path, null);
            } // end if
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath1(comboBox1.Text);
            RefeashDispaly(comboBox1.Text, null);
        }

        private void button2_Click(object sender, EventArgs e) {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK) {
                string path = folderBrowserDialog2.SelectedPath;
                comboBox2.Text = path;
                comboBox2.Items.Insert(0, path);
                DataManager.Instance.comboBoxData.SelectPath2(path);
                RefeashDispaly(null, path);
            } // end if
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            DataManager.Instance.comboBoxData.SelectPath2(comboBox2.Text);
            RefeashDispaly(null, comboBox2.Text);
        }

        private void RefeashDispaly(string leftPath, string rightPath) {
            if (!string.IsNullOrEmpty(leftPath)) {
                leftTreeModle = new TreeModle(leftPath);
            } // end if
            if (!string.IsNullOrEmpty(rightPath)) {
                rightTreeModle = new TreeModle(rightPath);
            } // end if
            leftMenuView.ResetPath(leftTreeModle, rightTreeModle);
            TreeModle.CompareModle(leftTreeModle, rightTreeModle);
            leftInfoView.RefreshDisplay(leftTreeModle);
            rightInfoView.RefreshDisplay(rightTreeModle);
        } // end RefeashDispaly

        private string GetPath(string fullName, string rootName) {
            string path = fullName.Substring(rootName.Length);
            return path;
        } // end GetPath

        private void ExpandAllMenuItem_Click(object sender, EventArgs e) {
            treeView1.ExpandAll();
            treeView2.ExpandAll();
        }

        private void CollapseAllMenuItem_Click(object sender, EventArgs e) {
            treeView1.CollapseAll();
            treeView2.CollapseAll();
        }

        private void ShowSameMenuItem_Click(object sender, EventArgs e) {
            Global.ShowSame = !Global.ShowSame;
            ShowSameMenuItem.Checked = Global.ShowSame;
            leftInfoView.RefreshDisplay(leftTreeModle);
            rightInfoView.RefreshDisplay(rightTreeModle);
        }

        private void ShowFilterFormMenuItem_Click(object sender, EventArgs e) {
            FilterForm form = new FilterForm(this);
            AddOwnedForm(form);
            form.ShowDialog();
        }
    }
}
