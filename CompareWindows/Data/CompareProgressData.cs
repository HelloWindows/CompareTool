using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompareWindows.Data {
    public class CompareProgressData {
        public int current { get; private set; }
        public int maxProgress { get; private set; }

        public CompareProgressData() {
            current = 0;
            maxProgress = 0;
        } // end CompareProgressData

        public void Reset() {
            current = 0;
            maxProgress = 0;
        } // end Reset

        public void SetMaxProgress(int maxProgress) {
            this.maxProgress = maxProgress;
        } // end SetMaxProgress

        public void Increment() {
            current++;
        } // end SetCurrent
    } // end class CompareProgressData
} // end namespace CompareWindows.Data
