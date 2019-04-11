using System;
using System.Collections.Generic;

namespace ComparingObjects
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split();
            List<Person> people = new List<Person>();
            while (input[0] != "END")
            {
                string name = input[0];
                int age = int.Parse(input[1]);
                string town = input[2];

                Person person = new Person(name,age,town);

                people.Add(person);

                input = Console.ReadLine().Split();
            }

            int personIndex = int.Parse(Console.ReadLine()) - 1;

            var personToCompare = people[personIndex];

            int countOfEqualPeople = 0;
            foreach (var person in people)
            {
                if (personToCompare.CompareTo(person) == 1)
                {
                    countOfEqualPeople++;
                }
            }

            if (countOfEqualPeople == 1)
            {
            Console.WriteLine($"No matches");
            }
            else
            {
                Console.WriteLine($"{countOfEqualPeople} {people.Count - countOfEqualPeople} {people.Count}");
            }
        }
    }
}
