﻿using CompareWindows.Algorithm.LinkList;
using SharpSvn;
using System.Collections.ObjectModel;

namespace CompareWindows {
    class SVNTest {

        public SVNTest() {
        } // end SVNTest

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

        public void GetFileLog() {
            using (SvnClient client = new SvnClient()) {
                string path = @"G:\Workspace\SVN\ftc_2\trunk\workspace";
                SvnStatusArgs args = new SvnStatusArgs();
                args.Depth = SvnDepth.Empty;
                args.RetrieveRemoteStatus = false;
                Collection<SvnStatusEventArgs> list = new Collection<SvnStatusEventArgs>();
                client.GetStatus(path, args, out list);
                foreach (SvnStatusEventArgs eventArgs in list) {
                    //从eventArgs中获取每个变更文件的相关信息
                }
            }
        }
    }
}
