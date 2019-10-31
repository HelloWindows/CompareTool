using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Event {
    public class ProgressEventArgs : EventArgs {
        public int Current { get; private set; }
        public int MaxiMum { get; private set; }
        public string Percentagep { get; private set; }

        public ProgressEventArgs(int current, int maxiMun) {
            Current = current < 0 ? 0 : current;
            MaxiMum = maxiMun < 0 ? 0 : maxiMun;
            if (MaxiMum == 0) {
                Current = 0;
                Percentagep = "100%";
            } else {
                if (Current > MaxiMum) Current = MaxiMum;
                // end if
                float value = (float)Current / MaxiMum;
                Percentagep = string.Format("{0:P}", value);
            } // end if
        } // end ProgressEventArgs
    }
}
