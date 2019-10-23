using System;
using System.Windows.Forms;

namespace CompareWindows {
    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {
            SVNTest test = new SVNTest();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SVNForm());
        }
    }
}
