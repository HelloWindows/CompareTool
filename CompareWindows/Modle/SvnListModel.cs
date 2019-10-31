using CompareWindows.Event;
using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class SvnListModel {
        private string leftRoot;
        private string rightRoot;
        private BackgroundWorker _worker;
        private List<string> _stringToRead;
        private HashSet<string> alreadyPath;
        private Dictionary<string, Collection<SvnLogEventArgs>> leftPathToStatus;
        private Dictionary<string, Collection<SvnLogEventArgs>> rightPathToStatus;
        public ProgressModle progress { get; private set; }
        public event EventHandler<SvnDataEventArgs> SvnDataReady;
        public event EventHandler<EventArgs> StopSvnLoad; 
        private bool toLoad = false;
        public bool ToLoad {
            get { return toLoad; }
            set {
                toLoad = value;
                if (toLoad == false) {
                    if (_worker.IsBusy) _worker.CancelAsync();
                    // end if
                    _stringToRead.Clear();
                    alreadyPath.Clear();
                    leftPathToStatus.Clear();
                    rightPathToStatus.Clear();
                    if (StopSvnLoad != null) StopSvnLoad(this, EventArgs.Empty);
                    // end if
                } // end if
            }
        } // end ToLoad

        private string currentPath;
        public string CurrentPaht {
            get { return currentPath; }
            set {
                currentPath = value;
                if (string.IsNullOrEmpty(currentPath) || !toLoad) return;
                // end if
                if (alreadyPath.Add(currentPath)) {
                    _stringToRead.Add(currentPath);
                    RunWorkerAsync();
                } else {
                    OnSvnInfoReady(currentPath);
                }// end if
            } 
        } 

        public SvnListModel(string leftRoot, string rightRoot) {
            this.leftRoot = leftRoot;
            this.rightRoot = rightRoot;
            _stringToRead = new List<string>();
            alreadyPath = new HashSet<string>();
            leftPathToStatus = new Dictionary<string, Collection<SvnLogEventArgs>>();
            rightPathToStatus = new Dictionary<string, Collection<SvnLogEventArgs>>();
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += new DoWorkEventHandler(ReadFilesProperties);
            _worker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnProgressCompleted);
            progress = new ProgressModle();
        } // end SVNListModle

        private void ReadFilesProperties(object sender, DoWorkEventArgs e) {
            SvnClient client = new SvnClient();
            SvnLogArgs logArgs = new SvnLogArgs();
            logArgs.RetrieveAllProperties = false;
            while (_stringToRead.Count > 0) {
                string path = _stringToRead[0];
                _stringToRead.RemoveAt(0);
                try {
                    Collection<SvnLogEventArgs> status;
                    if (!client.GetLog(leftRoot + path, logArgs, out status)) {
                        status = null;
                    } // end if
                    leftPathToStatus.Add(path, status);
                } catch (Exception) {
                } // end try
                try {
                    Collection<SvnLogEventArgs> status;
                    if (!client.GetLog(rightRoot + path, logArgs, out status)) {
                        status = null;
                    } // end if
                    rightPathToStatus.Add(path, status);
                } catch (Exception) {
                } // end try
                _worker.ReportProgress(0, path);
            } // end while
            client.Dispose();
        } // end ReadFilesProperties

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e) {
            string path = e.UserState as string;
            if (path == currentPath) {
                OnSvnInfoReady(currentPath);
            } // end if
            progress.Current = progress.Current + 1;
        } // end OnProgressChanged

        private void OnProgressCompleted(object sender, RunWorkerCompletedEventArgs e) {
            progress.Completed();
        } // end OnProgressCompleted

        private void RunWorkerAsync() {
            progress.Restart(0, _stringToRead.Count);
            if (!_worker.IsBusy)
                _worker.RunWorkerAsync();
            // end if
        } // end RunWorkerAsync

        private void OnSvnInfoReady(string path) {
            if (toLoad && SvnDataReady != null) {
                Collection<SvnLogEventArgs> leftStatus;
                Collection<SvnLogEventArgs> rightStatus;
                if (!leftPathToStatus.TryGetValue(path, out leftStatus)) {
                    leftStatus = null;
                } // end if
                if (!rightPathToStatus.TryGetValue(path, out rightStatus)) {
                    rightStatus = null;
                } // end if
                SvnDataReady(this, new SvnDataEventArgs(leftStatus, rightStatus));
            } // end if
        } // end OnSvnInfoReady
    }
}
