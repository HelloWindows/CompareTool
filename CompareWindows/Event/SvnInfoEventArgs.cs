using SharpSvn;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CompareWindows.Event {
    public class SvnDataEventArgs : EventArgs {
        public Collection<SvnLogEventArgs> LeftStatus { get; private set; }
        public Collection<SvnLogEventArgs> RightStatus { get; private set; }

        public SvnDataEventArgs(Collection<SvnLogEventArgs> leftStatus, Collection<SvnLogEventArgs> rightStatus) {
            LeftStatus = leftStatus;
            RightStatus = rightStatus;
        } // end SvnDataEventArgs
    }
}
