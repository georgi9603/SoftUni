using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericBoxOfString
{
    public class Box<T>
        where T : IComparable<T>
    {
        public Box(List<T> items)
        {
            this.Items = items;
        }

        public List<T> Items { get; set; }

        public void SwapItemsInList(int indexOne, int indexTwo)
        {
            T temp = this.Items[indexOne];
            this.Items[indexOne] = this.Items[indexTwo];
            this.Items[indexTwo] = temp;
        }

        public int compareItems()
        {
            int count = 0;

            foreach (var item in Items)
            {
                if (item.CompareTo(Items.Last()) > 0)
                {
                    count++;
                }
            }

            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Items)
            {
                sb.AppendLine($"{item.GetType().FullName}: {item}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}