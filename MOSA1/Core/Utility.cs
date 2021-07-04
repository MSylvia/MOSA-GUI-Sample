using System;
using System.Collections.Generic;

namespace MOSA1.Core
{
    public static class Utility
    {
        public static string ToStr(this int i)
        {
            if (i < 0)
            {
                return "-" + Math.Abs(i);
            }
            else
            {
                return i.ToString();
            }
        }

        public static int GetWindowIndex(this List<Window> windows, Window window)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i] == window)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
