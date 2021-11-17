using System;
using System.Collections.Generic;
using System.Text;

namespace IPaS_laba_1
{
    class Result
    {
        public bool isFound;
        public int startIndex;
        public int endIndex;

        public Result(bool isFound, int start, int end)
        {
            this.isFound = isFound;
            this.startIndex = start;
            this.endIndex = end;
        }

        public String toString()
        {
            return "It is found: " + isFound + "\n" + "Start index: " + startIndex + "\n" + "End index: " + endIndex;
        }
    }
}
