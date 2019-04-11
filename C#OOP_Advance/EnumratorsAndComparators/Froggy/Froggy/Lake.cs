using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Froggy
{
    public class Lake: IEnumerable<int>
    {

        public Lake(int[] stonesNumbers)
        {
            Stones = stonesNumbers;
        }

        public int[] Stones { get; set; }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i <= Stones.Length - 1; i+=2)
            {
                yield return Stones[i];
            }

            int oddIndex = (Stones.Length - 1 ) % 2 != 0 ? Stones.Length - 1 : Stones.Length - 2;

            for (int i = oddIndex; i > 0; i-=2)
            {
                yield return Stones[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}