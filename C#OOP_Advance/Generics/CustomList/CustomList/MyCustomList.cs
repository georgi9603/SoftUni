using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomList
{
    public class MyCustomList<T> : IMyCustomList<T>, IEnumerable<T>
        where T : IComparable<T>
    {
        private const int initLenght = 4;

        private T[] array;

        public MyCustomList()
        {
            array = new T[initLenght];
        }

        public T[] Array
        {
            get { return array; }
            private set { array = value; }
        }

        public int Count { get; set; }

        public void Add(T element)
        {
            if (this.Array.Length == this.Count)
            {
                T[] tempArray = new T[2 * Count];

                for (int i = 0; i < Array.Length; i++)
                {
                    tempArray[i] = Array[i];
                }

                Array = tempArray;
            }

            this.Array[Count++] = element;

        }

        public bool Contains(T element)
        {
            bool doesItContain = false;

            for (int i = 0; i < Count; i++)
            {
                if (Array[i].Equals(element))
                {
                    doesItContain = true;
                }
            }

            return doesItContain;
        }

        public int CountGreaterThan(T element)
        {
            int counter = 0;

            for (int i = 0; i < Count; i++)
            {
                if (Array[i].CompareTo(element) > 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 0 ; i < array.Length; ++i)
            {
                yield return array[i];
            }
        }

        public T Max()
        {
            T tempItem = Array[0];

            for (int i = 1; i < Count; i++)
            {
                if (Array[i].CompareTo(tempItem) > 0)
                {
                    tempItem = Array[i];
                }
            }

            return tempItem;
        }

        public T Min()
        {
            T tempItem = Array[0];

            for (int i = 1; i < Count; i++)
            {
                if (Array[i].CompareTo(tempItem) < 0)
                {
                    tempItem = Array[i];
                }
            }

            return tempItem;
        }

        public T Remove(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            T resultValue = Array[index];
            Count--;

            for (int i = index; i < Count; i++)
            {
                Array[i] = Array[i + 1];
            }

            Array[Count] = default(T);

            return resultValue;
        }

        public void Sort()
        {

            for (int i = 0; i < Count; i++)
            {

                for (int j = i + 1; j < Count; j++)
                {
                    if (Array[i].CompareTo(Array[j]) > 0)
                    {
                        T tempValue = Array[i];
                        Array[i] = Array[j];
                        Array[j] = tempValue;
                    }
                }
            }
        }

        public void Swap(int index1, int index2)
        {
            if (index1 < 0 || index1 >= Count
                && index2 < 0 || index2 >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            T tempValue = Array[index1];
            Array[index1] = Array[index2];
            Array[index2] = tempValue;
        }

        public override string ToString()
        {
            return String.Join("\n", Array.Take(this.Count));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}