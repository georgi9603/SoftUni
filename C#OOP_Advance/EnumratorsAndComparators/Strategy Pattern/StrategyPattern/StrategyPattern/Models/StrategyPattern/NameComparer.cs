using System.Collections.Generic;

namespace StrategyPattern.Models.StrategyPattern
{
    public class NameComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            int compareResult = 0;

            compareResult = x.Name.Length.CompareTo(y.Name.Length);

            if (compareResult == 0)
            {
                compareResult = x.Name.ToLower()[0].CompareTo(y.Name.ToLower()[0]);
            }

            return compareResult;
        }
    }
}