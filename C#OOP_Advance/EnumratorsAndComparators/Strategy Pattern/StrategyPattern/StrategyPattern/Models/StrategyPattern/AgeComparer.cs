using System.Collections.Generic;

namespace StrategyPattern.Models.StrategyPattern
{
    public class AgeComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return (x.Age.CompareTo(y.Age));
        }
    }
}
