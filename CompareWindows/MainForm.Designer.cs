namespace CompareWindows {
    partial class MainForm {
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.LeftNodeMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowFilterFormMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowSameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExpandAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CollapseAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.splitter6 = new System.Windows.Forms.Splitter();
            this.panel9 = new System.Windows.Forms.Panel();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LeftNodeMenuStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftNodeMenuStrip
            // 
            this.LeftNodeMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyMenuItem,
            this.DeleteMenuItem,
            this.CopyToMenuItem,
            this.MoveToMenuItem});
            this.LeftNodeMenuStrip.Name = "contextMenuStrip1";
            this.LeftNodeMenuStrip.Size = new System.Drawing.Size(181, 114);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Name = "CopyMenuItem";
            this.CopyMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CopyMenuItem.Text = "复制";
            // 
            // CopyToMenuItem
            // 
            this.CopyToMenuItem.Name = "CopyToMenuItem";
            this.CopyToMenuItem.Size = new System.Drawing.Size(180, 22);
            this.CopyToMenuItem.Text = "复制到右边>>";
            // 
            // MoveToMenuItem
            // 
            this.MoveToMenuItem.Name = "MoveToMenuItem";
            this.MoveToMenuItem.Size = new System.Drawing.Size(180, 22);
            this.MoveToMenuItem.Text = "移动到右边>>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ExpandAllMenuItem,
            this.CollapseAllMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1384, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowFilterFormMenuItem,
            this.ShowSameMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(68, 21);
            this.toolStripMenuItem1.Text = "添加过滤";
            // 
            // ShowFilterFormMenuItem
            // 
            this.ShowFilterFormMenuItem.Name = "ShowFilterFormMenuItem";
            this.ShowFilterFormMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ShowFilterFormMenuItem.Text = "过滤文件及文件夹";
            this.ShowFilterFormMenuItem.Click += new System.EventHandler(this.ShowFilterFormMenuItem_Click);
            // 
            // ShowSameMenuItem
            // 
            this.ShowSameMenuItem.Name = "ShowSameMenuItem";
            this.ShowSameMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ShowSameMenuItem.Text = "显示相同文件或文件夹";
            this.ShowSameMenuItem.Click += new System.EventHandler(this.ShowSameMenuItem_Click);
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 609);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1384, 52);
            this.panel1.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 30);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1384, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 606);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1384, 3);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            this.splitter1.Visible = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter2.Location = new System.Drawing.Point(660, 25);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 581);
            this.splitter2.TabIndex = 5;
            this.splitter2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.splitter3);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(660, 581);
            this.panel2.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.treeView1);
            this.panel4.Controls.Add(this.splitter4);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(660, 478);
            this.panel4.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 28);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(660, 450);
            this.treeView1.TabIndex = 2;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter4.Location = new System.Drawing.Point(0, 25);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(660, 3);
            this.splitter4.TabIndex = 1;
            this.splitter4.TabStop = false;
            this.splitter4.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.comboBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(660, 25);
            this.panel5.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(440, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Brower";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(10, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(420, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // splitter3
            // 
            this.splitter3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 478);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(660, 3);
            this.splitter3.TabIndex = 1;
            this.splitter3.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.listBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 481);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(660, 100);
            this.panel3.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(660, 100);
            this.listBox1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Controls.Add(this.splitter6);
            this.panel6.Controls.Add(this.panel9);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(663, 25);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(721, 581);
            this.panel6.TabIndex = 6;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.treeView2);
            this.panel7.Controls.Add(this.splitter5);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(721, 478);
            this.panel7.TabIndex = 2;
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Location = new System.Drawing.Point(0, 28);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(721, 450);
            this.treeView2.TabIndex = 2;
            // 
            // splitter5
            // 
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter5.Location = new System.Drawing.Point(0, 25);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(721, 3);
            this.splitter5.TabIndex = 1;
            this.splitter5.TabStop = false;
            this.splitter5.Visible = false;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.button2);
            this.panel8.Controls.Add(this.comboBox2);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(721, 25);
            this.panel8.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(440, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Brower";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(10, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(420, 20);
            this.comboBox2.TabIndex = 0;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // splitter6
            // 
            this.splitter6.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitter6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter6.Location = new System.Drawing.Point(0, 478);
            this.splitter6.Name = "splitter6";
            this.splitter6.Size = new System.Drawing.Size(721, 3);
            this.splitter6.TabIndex = 1;
            this.splitter6.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.listBox2);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 481);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(721, 100);
            this.panel9.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(0, 0);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(721, 100);
            this.listBox2.TabIndex = 0;
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(180, 22);
            this.DeleteMenuItem.Text = "删除";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 661);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 500);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.LeftNodeMenuStrip.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ContextMenuStrip LeftNodeMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyToMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MoveToMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Splitter splitter4;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Splitter splitter5;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Splitter splitter6;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem ExpandAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CollapseAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowFilterFormMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowSameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
    }
}