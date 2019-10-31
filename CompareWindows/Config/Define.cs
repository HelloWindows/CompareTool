using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CompareWindows.Config {
    public static class Define {
        public static Color DefaultColor { get { return Color.Black; } }
        public static Color DefferentColor { get { return Color.Red; } }
        public static Color SpecialColor { get { return Color.Blue; } }
        public static Color SameColor { get { return Color.Green; } }
        public static Color SelectedColor { get { return Color.SkyBlue; } }
        public static Color NotSelectedColor { get { return Color.White; } }

        public static Brush DefaultBrush { get { return SystemBrushes.ControlText; } }
        public static Brush DefferentBrush { get { return Brushes.Red; } }
        public static Brush SameBrush { get { return Brushes.Green; } }
        public static Brush DisableBrush { get { return Brushes.Gray; } }
    } // end class Define
} // end namespace CompareWindows.Config 
