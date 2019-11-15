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
            this.components = new System.ComponentModel.Container();
            Aga.Controls.Tree.TreeColumn treeColumn1 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn2 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn3 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn4 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn5 = new Aga.Controls.Tree.TreeColumn();
            Aga.Controls.Tree.TreeColumn treeColumn6 = new Aga.Controls.Tree.TreeColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ExpandAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompareMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.过滤ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showSameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.svn设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toLoadSvnMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.compareProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.svnStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.svnProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.svnProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.moveStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.moveProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.moveProgressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewAdv1 = new Aga.Controls.Tree.TreeViewAdv();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyToRightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToLeftMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompareFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.svnListBox1 = new CompareWindows.View.Control.SvnListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandAllMenuItem,
            this.CollapseAllMenuItem,
            this.CompareMenuItem,
            this.过滤ToolStripMenuItem1,
            this.svn设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(896, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
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
            // CompareMenuItem
            // 
            this.CompareMenuItem.Name = "CompareMenuItem";
            this.CompareMenuItem.Size = new System.Drawing.Size(44, 21);
            this.CompareMenuItem.Text = "比较";
            this.CompareMenuItem.Click += new System.EventHandler(this.CompareMenuItem_Click);
            // 
            // 过滤ToolStripMenuItem1
            // 
            this.过滤ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showSameMenuItem,
            this.FilterMenuItem});
            this.过滤ToolStripMenuItem1.Name = "过滤ToolStripMenuItem1";
            this.过滤ToolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.过滤ToolStripMenuItem1.Text = "过滤";
            // 
            // showSameMenuItem
            // 
            this.showSameMenuItem.Name = "showSameMenuItem";
            this.showSameMenuItem.Size = new System.Drawing.Size(124, 22);
            this.showSameMenuItem.Text = "显示相同";
            this.showSameMenuItem.Click += new System.EventHandler(this.showSameMenuItem_Click);
            // 
            // FilterMenuItem
            // 
            this.FilterMenuItem.Name = "FilterMenuItem";
            this.FilterMenuItem.Size = new System.Drawing.Size(124, 22);
            this.FilterMenuItem.Text = "过滤器";
            this.FilterMenuItem.Click += new System.EventHandler(this.FilterMenuItem_Click);
            // 
            // svn设置ToolStripMenuItem
            // 
            this.svn设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toLoadSvnMenuItem});
            this.svn设置ToolStripMenuItem.Name = "svn设置ToolStripMenuItem";
            this.svn设置ToolStripMenuItem.Size = new System.Drawing.Size(64, 21);
            this.svn设置ToolStripMenuItem.Text = "Svn设置";
            // 
            // toLoadSvnMenuItem
            // 
            this.toLoadSvnMenuItem.Name = "toLoadSvnMenuItem";
            this.toLoadSvnMenuItem.Size = new System.Drawing.Size(124, 22);
            this.toLoadSvnMenuItem.Text = "开启加载";
            this.toLoadSvnMenuItem.Click += new System.EventHandler(this.toLoadSvnMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.compareProgressBar,
            this.progressLabel,
            this.toolStripStatusLabel1,
            this.svnStatusLabel,
            this.svnProgressBar,
            this.svnProgressLabel,
            this.toolStripStatusLabel2,
            this.moveStatusLabel,
            this.moveProgressBar,
            this.moveProgressLabel});
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
            this.statusLabel.Text = "就绪";
            // 
            // compareProgressBar
            // 
            this.compareProgressBar.Name = "compareProgressBar";
            this.compareProgressBar.Size = new System.Drawing.Size(100, 16);
            this.compareProgressBar.Visible = false;
            // 
            // progressLabel
            // 
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(40, 17);
            this.progressLabel.Text = "100%";
            this.progressLabel.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(100, 17);
            // 
            // svnStatusLabel
            // 
            this.svnStatusLabel.Name = "svnStatusLabel";
            this.svnStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.svnStatusLabel.Text = "就绪";
            // 
            // svnProgressBar
            // 
            this.svnProgressBar.Name = "svnProgressBar";
            this.svnProgressBar.Size = new System.Drawing.Size(100, 16);
            this.svnProgressBar.Visible = false;
            // 
            // svnProgressLabel
            // 
            this.svnProgressLabel.Name = "svnProgressLabel";
            this.svnProgressLabel.Size = new System.Drawing.Size(40, 17);
            this.svnProgressLabel.Text = "100%";
            this.svnProgressLabel.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(84, 17);
            this.toolStripStatusLabel2.Text = "                   ";
            // 
            // moveStatusLabel
            // 
            this.moveStatusLabel.Name = "moveStatusLabel";
            this.moveStatusLabel.Size = new System.Drawing.Size(32, 17);
            this.moveStatusLabel.Text = "就绪";
            // 
            // moveProgressBar
            // 
            this.moveProgressBar.Name = "moveProgressBar";
            this.moveProgressBar.Size = new System.Drawing.Size(100, 16);
            this.moveProgressBar.Visible = false;
            // 
            // moveProgressLabel
            // 
            this.moveProgressLabel.Name = "moveProgressLabel";
            this.moveProgressLabel.Size = new System.Drawing.Size(40, 17);
            this.moveProgressLabel.Text = "100%";
            this.moveProgressLabel.Visible = false;
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
            this.splitContainer1.Panel2.Controls.Add(this.svnListBox1);
            this.splitContainer1.Size = new System.Drawing.Size(872, 499);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            treeColumn1.Header = "Name";
            treeColumn1.Width = 280;
            treeColumn2.Header = "Size";
            treeColumn2.Width = 60;
            treeColumn3.Header = "Date";
            treeColumn3.Width = 90;
            treeColumn4.Header = "Name";
            treeColumn4.Width = 280;
            treeColumn5.Header = "Size";
            treeColumn5.Width = 60;
            treeColumn6.Header = "Date";
            treeColumn6.Width = 90;
            this.treeViewAdv1.Columns.Add(treeColumn1);
            this.treeViewAdv1.Columns.Add(treeColumn2);
            this.treeViewAdv1.Columns.Add(treeColumn3);
            this.treeViewAdv1.Columns.Add(treeColumn4);
            this.treeViewAdv1.Columns.Add(treeColumn5);
            this.treeViewAdv1.Columns.Add(treeColumn6);
            this.treeViewAdv1.ContextMenuStrip = this.contextMenuStrip1;
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
            this.treeViewAdv1.RowHeight = 18;
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.SelectionMode = Aga.Controls.Tree.TreeSelectionMode.Multi;
            this.treeViewAdv1.Size = new System.Drawing.Size(872, 216);
            this.treeViewAdv1.TabIndex = 0;
            this.treeViewAdv1.Text = "treeViewAdv1";
            this.treeViewAdv1.UseColumns = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToRightMenuItem,
            this.CopyToLeftMenuItem,
            this.CompareFileMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 70);
            // 
            // CopyToRightMenuItem
            // 
            this.CopyToRightMenuItem.Name = "CopyToRightMenuItem";
            this.CopyToRightMenuItem.Size = new System.Drawing.Size(150, 22);
            this.CopyToRightMenuItem.Text = "复制到右边->";
            this.CopyToRightMenuItem.Click += new System.EventHandler(this.CopyToRightMenuItem_Click);
            // 
            // CopyToLeftMenuItem
            // 
            this.CopyToLeftMenuItem.Name = "CopyToLeftMenuItem";
            this.CopyToLeftMenuItem.Size = new System.Drawing.Size(150, 22);
            this.CopyToLeftMenuItem.Text = "<-复制到左边";
            this.CopyToLeftMenuItem.Click += new System.EventHandler(this.CopyToLeftMenuItem_Click);
            // 
            // CompareFileMenuItem
            // 
            this.CompareFileMenuItem.Name = "CompareFileMenuItem";
            this.CompareFileMenuItem.Size = new System.Drawing.Size(150, 22);
            this.CompareFileMenuItem.Text = "比较";
            this.CompareFileMenuItem.Click += new System.EventHandler(this.CompareFileMenuItem_Click);
            // 
            // _icon1
            // 
            this._icon1.DataPropertyName = "Icon1";
            // 
            // _name1
            // 
            this._name1.BrushPropertyName = "Brush1";
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
            this._name2.BrushPropertyName = "Brush2";
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
            // svnListBox1
            // 
            this.svnListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.svnListBox1.Location = new System.Drawing.Point(0, 0);
            this.svnListBox1.Model = null;
            this.svnListBox1.Name = "svnListBox1";
            this.svnListBox1.Size = new System.Drawing.Size(872, 246);
            this.svnListBox1.TabIndex = 0;
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar compareProgressBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
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
        private System.Windows.Forms.ToolStripStatusLabel progressLabel;
        private Control.SvnListBox svnListBox1;
        private System.Windows.Forms.ToolStripMenuItem svn设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toLoadSvnMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel svnStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar svnProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel svnProgressLabel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem CopyToRightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToLeftMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CompareFileMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel moveStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar moveProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel moveProgressLabel;
        private System.Windows.Forms.ToolStripMenuItem ExpandAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CollapseAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CompareMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 过滤ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showSameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FilterMenuItem;
    }
}