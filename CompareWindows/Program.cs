using CompareWindows.View.Window;
using System;
using System.Windows.Forms;

namespace CompareWindows {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWindow win = new MainWindow();
            win.Show();
            Application.Run();
        }
    }
}
