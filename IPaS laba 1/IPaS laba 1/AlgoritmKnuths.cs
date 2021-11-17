using System;
using System.Collections.Generic;
using System.Text;

namespace IPaS_laba_1
{
    class AlgoritmKnuths
    {
        public int[] prefixFunction(String s)
        {
            int[] p = new int[s.Length];
            int k = 0;
            for (int i = 1; i < s.Length; i++)
            {
                while (k > 0 && s[k] != s[i])
                    k = p[k - 1];
                if (s[k] == s[i])
                    ++k;
                p[i] = k;
            }
            return p;
        }
        public Result Find(String s, String pattern)
        {
            Result defaultResult = new Result(false, 0, 0);
            int m = pattern.Length;
            if (m == 0)
                return defaultResult;
            int[] p = prefixFunction(pattern);
            for (int i = 0, k = 0; i < s.Length; i++)
                for (; ; k = p[k - 1])
                {
                    if (pattern[k] == s[i])
                    {
                        if (++k == m)
                            return new Result(true, i + 1 - m, i);
                        break;
                    }
                    if (k == 0)
                        break;
                }
            return defaultResult;
        }
    }
}
