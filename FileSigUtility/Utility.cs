using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileSigUtility
{
    public static class Utility
    {
        public static int GetIndexOfPattern(byte[] pattern, byte[] raw, int offset = 0)
        {
            for (int i = offset; i < raw.Length; i++)
            {
                if (offset + pattern.Length > raw.Length)
                {
                    return -1;
                }

                if (MatchPattern(pattern, raw, i))
                {
                    return i;
                }
            }

            return -1;
        }

        private static bool MatchPattern(byte[] pattern, byte[] raw, int offset = 0)
        {
            if (offset + pattern.Length > raw.Length)
            {
                return false;
            }

            for (int i = 0; i < pattern.Length; i++)
            {
                if (raw[offset + i] != pattern[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
