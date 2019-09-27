﻿using SharpSvn;
using System.Collections.ObjectModel;

namespace CompareWindows {
    class SVNTest {

        public string GetSvnLog() {
            string path = @"G:\Workspace\SVN\ftc_2\trunk\workspace";
            SvnClient client = new SvnClient();
            SvnLogArgs logArgs = new SvnLogArgs();
            logArgs.RetrieveAllProperties = false; //不检索所有属性
            Collection<SvnLogEventArgs> status;
            client.GetLog(path, logArgs, out status);
            int messgNum = 0;
            string logText = "";
            string lastLog = "";
            foreach (var item in status) {
                if (messgNum > 150)
                    break;
                messgNum += 1;
                if (string.IsNullOrEmpty(item.LogMessage) || item.LogMessage == " " || lastLog == item.LogMessage) {
                    continue;
                }
                logText = item.Author + "：" + item.LogMessage + "\n" + logText;
                lastLog = item.LogMessage;
            }
            return logText;
        }
    }
}
