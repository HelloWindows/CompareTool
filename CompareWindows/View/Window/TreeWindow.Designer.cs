namespace CompareWindows.View.Window {
    partial class TreeWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Aga.Controls.Tree.TreeColumn treeColumn7 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn8 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn9 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn10 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn11 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn12 = new Aga.Controls.Tree.TreeColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.过滤ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpandAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.过滤ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.compareProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this._icon1 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this._name1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._size1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._date1 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._icon2 = new Aga.Controls.Tree.NodeControls.NodeStateIcon();
            this._name2 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._size2 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this._date2 = new Aga.Controls.Tree.NodeControls.NodeTextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel3 = new System.Windows.Forms.Panel();
            this.browerBtn1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.browerBtn2 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.过滤ToolStripMenuItem,
            this.ExpandAllMenuItem,
            this.CollapseAllMenuItem,
            this.过滤ToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(896, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 过滤ToolStripMenuItem
            // 
            this.过滤ToolStripMenuItem.Name = "过滤ToolStripMenuItem";
            this.过滤ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.过滤ToolStripMenuItem.Text = "对比界面";
            // 
            // ExpandAllMenuItem
            // 
            this.ExpandAllMenuItem.Name = "ExpandAllMenuItem";
            this.ExpandAllMenuItem.Size = new System.Drawing.Size(44, 21);
            this.ExpandAllMenuItem.Text = "展开";
            this.ExpandAllMenuItem.Click += new System.EventHandler(this.ExpandAllMenuItem_Click);
            // 
            // CollapseAllMenuItem
            // 
            this.CollapseAllMenuItem.Name = "CollapseAllMenuItem";
            this.CollapseAllMenuItem.Size = new System.Drawing.Size(44, 21);
            this.CollapseAllMenuItem.Text = "折叠";
            this.CollapseAllMenuItem.Click += new System.EventHandler(this.CollapseAllMenuItem_Click);
            // 
            // 过滤ToolStripMenuItem1
            // 
            this.过滤ToolStripMenuItem1.Name = "过滤ToolStripMenuItem1";
            this.过滤ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.过滤ToolStripMenuItem1.Text = "过滤";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.compareProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(896, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(32, 17);
            this.statusLabel.Text = "信息";
            // 
            // compareProgressBar
            // 
            this.compareProgressBar.Name = "compareProgressBar";
            this.compareProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Location = new System.Drawing.Point(12, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 499);
            this.panel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewAdv1);
            this.splitContainer1.Panel1.Controls.Add(this.splitter1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(872, 499);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            treeColumn7.Header = "Name";
            treeColumn7.Width = 280;
            treeColumn8.Header = "Size";
            treeColumn8.Width = 60;
            treeColumn9.Header = "Date";
            treeColumn9.Width = 90;
            treeColumn10.Header = "Name";
            treeColumn10.Width = 280;
            treeColumn11.Header = "Size";
            treeColumn11.Width = 60;
            treeColumn12.Header = "Date";
            treeColumn12.Width = 90;
            this.treeViewAdv1.Columns.Add(treeColumn7);
            this.treeViewAdv1.Columns.Add(treeColumn8);
            this.treeViewAdv1.Columns.Add(treeColumn9);
            this.treeViewAdv1.Columns.Add(treeColumn10);
            this.treeViewAdv1.Columns.Add(treeColumn11);
            this.treeViewAdv1.Columns.Add(treeColumn12);
            this.treeViewAdv1.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeViewAdv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.FullRowSelect = true;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.LoadOnDemand = true;
            this.treeViewAdv1.Location = new System.Drawing.Point(0, 33);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.NodeControls.Add(this._icon1);
            this.treeViewAdv1.NodeControls.Add(this._name1);
            this.treeViewAdv1.NodeControls.Add(this._size1);
            this.treeViewAdv1.NodeControls.Add(this._date1);
            this.treeViewAdv1.NodeControls.Add(this._icon2);
            this.treeViewAdv1.NodeControls.Add(this._name2);
            this.treeViewAdv1.NodeControls.Add(this._size2);
            this.treeViewAdv1.NodeControls.Add(this._date2);
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(872, 272);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            // 
            // _icon1
            // 
            this._icon1.DataPropertyName = "Icon1";
            // 
            // _name1
            // 
            this._name1.DataPropertyName = "Name";
            // 
            // _size1
            // 
            this._size1.Column = 1;
            this._size1.DataPropertyName = "Size1";
            // 
            // _date1
            // 
            this._date1.Column = 2;
            this._date1.DataPropertyName = "Date1";
            // 
            // _icon2
            // 
            this._icon2.Column = 3;
            this._icon2.DataPropertyName = "Icon2";
            // 
            // _name2
            // 
            this._name2.Column = 3;
            this._name2.DataPropertyName = "Name";
            // 
            // _size2
            // 
            this._size2.Column = 4;
            this._size2.DataPropertyName = "Size2";
            // 
            // _date2
            // 
            this._date2.Column = 5;
            this._date2.DataPropertyName = "Date2";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 30);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(872, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(872, 30);
            this.panel2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.browerBtn2);
            this.splitContainer3.Panel2.Controls.Add(this.comboBox2);
            this.splitContainer3.Size = new System.Drawing.Size(872, 30);
            this.splitContainer3.SplitterDistance = 433;
            this.splitContainer3.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.browerBtn1);
            this.panel3.Controls.Add(this.comboBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(433, 30);
            this.panel3.TabIndex = 0;
            // 
            // browerBtn1
            // 
            this.browerBtn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browerBtn1.Location = new System.Drawing.Point(355, 5);
            this.browerBtn1.Name = "browerBtn1";
            this.browerBtn1.Size = new System.Drawing.Size(75, 20);
            this.browerBtn1.TabIndex = 1;
            this.browerBtn1.Text = "Brower";
            this.browerBtn1.UseVisualStyleBackColor = true;
            this.browerBtn1.Click += new System.EventHandler(this.browerBtn1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(3, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(346, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // browerBtn2
            // 
            this.browerBtn2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browerBtn2.Location = new System.Drawing.Point(357, 5);
            this.browerBtn2.Name = "browerBtn2";
            this.browerBtn2.Size = new System.Drawing.Size(75, 20);
            this.browerBtn2.TabIndex = 2;
            this.browerBtn2.Text = "Brower";
            this.browerBtn2.UseVisualStyleBackColor = true;
            this.browerBtn2.Click += new System.EventHandler(this.browerBtn2_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(3, 5);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(348, 20);
            this.comboBox2.TabIndex = 1;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Size = new System.Drawing.Size(872, 190);
            this.splitContainer2.SplitterDistance = 433;
            this.splitContainer2.TabIndex = 0;
            // 
            // TreeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 552);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TreeWindow";
            this.Text = "MainWindow";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 过滤ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExpandAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CollapseAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 过滤ToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar compareProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button browerBtn1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button browerBtn2;
        private System.Windows.Forms.Splitter splitter1;
        private Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon _icon1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _name1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _size1;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _date1;
        private Aga.Controls.Tree.NodeControls.NodeStateIcon _icon2;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _name2;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _size2;
        private Aga.Controls.Tree.NodeControls.NodeTextBox _date2;
    }
}