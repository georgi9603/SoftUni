using System;
using System.Collections.Generic;
using System.Reflection;
using ex_01_Database;
using NUnit.Framework;
using System.Linq;

namespace DatabaseTest
{
    [TestFixture]
    public class Tests
    {
        public List<Person> people;
        [SetUp]
        public void Setup()
        {
            people = new List<Person>
            {
                new Person(1234, "Pesho"),
                new Person(4294967296,"Gosho"),
                new Person(0,"Tosho")
            };
        }

        [Test]
        public void
               CreateNotEmpty_Constructor()
        {
            Database db = new Database(people);

            Type type = typeof(Database);

            List<Person> data = (List<Person>)type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "data")
                .GetValue(db);

            bool isEmptyActual = !data.Any();

            Assert.That(isEmptyActual, Is.False);
        }

        [Test]
        public void
            CreateEmpty_Constructor()
        {
            Database db = new Database();

            Type type = typeof(Database);

            List<Person> data = (List<Person>)type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "data")
                .GetValue(db);

            bool isEmptyActual = !data.Any();

            Assert.That(isEmptyActual, Is.True);
        }

        [Test]
        [TestCase(new object[] { 123, "null" })]
        [TestCase(new object[] { 123, " " })]
        [TestCase(new object[] { 123, "" })]
        [TestCase(new object[] { 123, null })]
        public void
            AddNull_EmptyOrWhiteSpace(object[] data)
        {
            Person person = new Person((int)data[0], (string)data[1]);

            Database db = new Database(people);

            Assert.Throws<ArgumentNullException>(() => db.Add(person));
        }

        [Test]
        [TestCase(new object[] { 123, "Gosho" })]
        public void
            AddExistingName_ShouldThrowException(object[] data)
        {
            Person person = new Person((int)data[0], (string)data[1]);

            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>(() => db.Add(person));
        }

        [Test]
        [TestCase(new object[] { 1234, "PeshoLudiq" })]
        public void
            AddExistingId_ShouldThrowException(object[] data)
        {
            Person person = new Person((int)data[0], (string)data[1]);

            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>(() => db.Add(person));
        }

        [Test]
        public void
            RemoveFrom_EmptyDB_ShouldThrowException()
        {
            Person person = new Person(1234, "Gosho");

            Database db = new Database();

            Assert.Throws<InvalidOperationException>(() => db.Remove(person));
        }

        [Test]
        [TestCase("null")]
        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public void
            FindByUsername_WithNullEmptyAndWhitespace_ShouldThrowException(string username)
        {
            Database db = new Database(people);

            Assert.Throws<ArgumentNullException>(() => db.FindByUsername(username));
        }

        [Test]
        [TestCase("ThereIsNoSuchUsername")]
        public void
            FindByUsername_WithNotExistingUsername_ShouldThrowException(string username)
        {
            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>(() => db.FindByUsername(username));
        }

        [Test]
        [TestCase("gosho")]
        public void
            FindByUsername_WithCase_SensitiveUsername_ShouldThrowException(string username)
        {
            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>(() => db.FindByUsername(username));
        }

        [Test]
        [TestCase(-69)]
        public void
            FindById_NegativeId_ShouldThrowException(int id)
        {
            Database db = new Database(people);

            Assert.Throws<ArgumentOutOfRangeException>(() => db.FindById(id));
        }

        [Test]
        [TestCase(69)]
        public void
            FindById_NotExistingId_ShouldThrowException(int id)
        {
            Database db = new Database(people);

            Assert.Throws<InvalidOperationException>(() => db.FindById(id));
        }

        [Test]
        public void
            AddNewPersonShouldAddCorrectly()
        {
            Database db = new Database(people);

            Person person = new Person(1111, "Stamat");

            db.Add(person);

            Type type = typeof(Database);

            List<Person> field = (List<Person>)type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "data")
                .GetValue(db);

            Person addedPerson = field.FirstOrDefault(p => p.Name == "Stamat");

            Assert.That(person.Name, Is.EqualTo(addedPerson.Name));
        }

        [Test]
        public void
            RemovePersonShould_RemoveCorrectly()
        {
            Database db = new Database(people);
            Person person = new Person(1234, "Pesho");

            db.Remove(person);
            Type type = typeof(Database);

            List<Person> field = (List<Person>)type
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "data")
                .GetValue(db);

            Person removedOrDefault = field.FirstOrDefault(p => p.Name == "Pesho");

            Assert.That(removedOrDefault, Is.EqualTo(null));
        }

        [Test]
        public void
            FindByUsername_ShouldFindUserCorrectly()
        {
            Database db = new Database(people);

            Person actualPerson = db.FindByUsername("Pesho");
            Person expectedPerson = new Person(1234, "Pesho");

            Assert.IsTrue(expectedPerson.Name.Equals(actualPerson.Name));
            Assert.IsTrue(expectedPerson.Id.Equals(actualPerson.Id));
        }

        [Test]
        public void
            FindById_ShouldFindUserCorrectly()
        {
            Database db = new Database(people);

            Person actualPerson = db.FindById(1234);
            Person expectedPerson = new Person(1234, "Pesho");

            Assert.IsTrue(expectedPerson.Name.Equals(actualPerson.Name));
            Assert.IsTrue(expectedPerson.Id.Equals(actualPerson.Id));
        }
    }
}