using System;
using System.Collections;
using System.Collections.Generic;

namespace ListyEnumerator
{
    public class ListyIterator<T> : IEnumerable<T>
    {
        private T[] MyList { get; }
        private int index = 0;

        public ListyIterator(T[] items)
        {
            MyList = items;
        }

        public bool HasNext()
        {
            if (index < MyList.Length - 1)
            {
                return true;
            }

            return false;
        }

        public void Move()
        {
            if (index < MyList.Length - 1)
            {
                index++;
                Console.WriteLine(true);
                return;;
            }

            Console.WriteLine(false);
        }

        public void Print()
        {
            if (MyList.Length == 0)
            {
                throw new ArgumentException("Invalid Operation!");
            }
            Console.WriteLine(MyList[index]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < MyList.Length; i++)
            {
                yield return MyList[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}