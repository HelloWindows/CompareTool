using CompareWindows.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Modle {
    public class ProgressModle {
        public event EventHandler<ProgressEventArgs> ProgressChanged;


        private int current;
        public int Current {
            get { return current; }
            set {
                current = value;
                if (ProgressChanged != null) ProgressChanged(this, new ProgressEventArgs(current, maxiMun));
                // end if
            }
        }
        private int maxiMun;
        public int MaxiMum {
            get { return maxiMun; }
            set {
                maxiMun = value;
                if (ProgressChanged != null) ProgressChanged(this, new ProgressEventArgs(current, maxiMun));
                // end if
            }
        }

        public ProgressModle() {
        } // end ProgressModle

        public void Reset(int current, int maximun) {
            this.current = current;
            MaxiMum = maximun;
        } // end  Reset
    }
}
