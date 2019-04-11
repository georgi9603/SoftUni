using System;
using System.Linq;

namespace ex_01_Database
{
    public class Database
    {
        private const int initializeStorageSize = 16;
        private int[] storageArray;
        private int index;

        public Database()
        {
            this.storageArray = new int[initializeStorageSize];
            index = - 1;
        }

        public Database(int[] values)
            : this()
        {
            if (values.Length > 16)
            {
                throw new InvalidOperationException();
            }
            createStorageArray(values);
        }

        public void Add(int element)
        {
            if (this.index < initializeStorageSize - 1)
            {
                this.storageArray[++index] = element;
                return;
            }
            throw new InvalidOperationException();
        }

        public void Remove()
        {
            if (this.index < 0)
            {
                throw new InvalidOperationException();
            }

            this.storageArray[this.index] = default(int);
            index--;
        }

        public int[] Fetch()
        {
            return this.storageArray.Take(index + 1).ToArray();
        }

        private void createStorageArray(int[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                this.storageArray[i] = values[i];
                index++;
            }
        }
    }
}