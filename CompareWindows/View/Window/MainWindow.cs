using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompareWindows.View.Window {
    public partial class MainWindow : Form {
        public MainWindow() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            TreeWindow win = new TreeWindow();
            win.Show();
            Hide();
        }

        private void button2_Click(object sender, EventArgs e) {
            SvnWindow win = new SvnWindow();
            win.Show();
            Hide();
        }

        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            Application.Exit();
        }
    }
}
