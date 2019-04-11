using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericBoxOfString
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());

            List<double> listItems = new List<double>();

            for (int i = 0; i < number; i++)
            {
                double result = double.Parse(Console.ReadLine());

                listItems.Add(result);
            }

            double compareInput = double.Parse(Console.ReadLine());
            listItems.Add(compareInput);

            Box<double> box = new Box<double>(listItems);

            Console.WriteLine(box.compareItems());
        }
    }
}
