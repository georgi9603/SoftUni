using System.Runtime.InteropServices.ComTypes;

namespace ex_01_Database
{
    public class Person
    {
        public Person(long id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public long Id { get; }
        public string Name { get; }
    }
}