using StrategyPattern.Models.StrategyPattern;
using System;
using System.Collections.Generic;

namespace StrategyPattern
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int inputCount = int.Parse(Console.ReadLine());

            SortedSet<Person> setOne = new SortedSet<Person>(new NameComparer());
            SortedSet<Person> setTwo = new SortedSet<Person>(new AgeComparer());

            for (int i = 0; i < inputCount; i++)
            {
                string[] data = Console.ReadLine().Split();

                string name = data[0];
                int age = int.Parse(data[1]);

                Person person = new Person(name, age);
                setOne.Add(person);
                setTwo.Add(person);

            }

            if (setOne.Count > 0)
            {
                Console.WriteLine(String.Join(Environment.NewLine,setOne));
                Console.WriteLine(String.Join(Environment.NewLine,setTwo));
            }
        }
    }
}
