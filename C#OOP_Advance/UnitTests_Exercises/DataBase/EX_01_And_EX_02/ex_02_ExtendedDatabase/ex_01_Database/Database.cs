using System;
using System.Collections.Generic;
using System.Linq;

namespace ex_01_Database
{
    public class Database
    {
        private List<Person> data;

        public Database()
        {
            this.data = new List<Person>();
        }

        public Database(List<Person> people)
            : this()
        {
            this.data = people;
        }

        public void Add(Person person)
        {
            foreach (var personInData in data)
            {
                if (personInData.Name == person.Name ||
                    personInData.Id == person.Id)
                {
                    throw new InvalidOperationException("Username or Id already exists");
                }
            }

            if (person.Id < 0)
            {
                throw new InvalidOperationException("Id can't be negative");
            }
            else if (String.IsNullOrEmpty(person.Name) ||
                     String.IsNullOrWhiteSpace(person.Name) || person.Name == "null")
            {
                throw new ArgumentNullException("Name can't be null or empty");
            }
            data.Add(person);
        }

        public void Remove(Person person)
        {

            if (!data.Any())
            {
                throw new InvalidOperationException("Data is empty");
            }

            int index = data.FindIndex(a => a.Name == person.Name);

            if (index != -1)
            {
                data.RemoveAt(index);
            }
        }

        public Person FindByUsername(string username)
        {
            if (String.IsNullOrEmpty(username) ||
                String.IsNullOrWhiteSpace(username) || username == "null")
            {
                throw new ArgumentNullException("Name can't be null");
            }

            foreach (var person in data)
            {
                if (person.Name == username)
                {
                    return person;
                }
            }

            throw new InvalidOperationException($"There is no person with name: {username}");
        }

        public Person FindById(long id)
        {
            if (id < 0)
            {
                throw new ArgumentOutOfRangeException("Id can't be negative");
            }

            foreach (var person in data)
            {
                if (person.Id == id)
                {
                    return person;
                }
            }
            throw new InvalidOperationException($"No person with this id: {id}");
        }
    }
}