using System;
using System.Linq;
using System.Reflection;
using ex_01_Database;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        //Create array with empty constructor.
        [Test]
        public void 
            InvokingWith_EmptyConstructor_ShouldReturn_ArrayWith_CorrectLength()
        {
            Database database = new Database();

            Type type = typeof(Database);

            int[] field = (int[])type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "storageArray")
                .GetValue(database);

            int actualResult = field.Length;
            int expectedResult = 16;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        //Create array with Invalid Length.
        [Test]
        public void 
            InvokingConstructor_WithMore_ParametersShould_ThrowAnException()
        {
            Database database;

            int[] testArray = new int[17];

            Assert.Throws<InvalidOperationException>(() => database = new Database(testArray));
        }

        //Set index correct
        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16})]
        public void 
            InvokingConstructor_WithValid_ParametersShould_SetCorrectlyIndex(int[] arr)
        {
            Database database = new Database(arr);

            int expectedIndex = arr.Length - 1;

            Type type = typeof(Database);

            int index = (int)type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "index")
                .GetValue(database);

            Assert.That(index, Is.EqualTo(expectedIndex));
        }

        //Add after 16th element should throw exception.
        [Test]
        public void 
            AddingElement_AfterCorrect_LengthShould_ThrowException()
        {
            Database database = new Database(new int[16]);

            int element = 17;

            Assert.Throws<InvalidOperationException>(() => database.Add(element));
        }

        [Test]
        [TestCase(new int[] { })]
        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15})]
        public void 
            Add_ParametersShould_AddCorrectly(int[] arr)
        {
            Database database = new Database(arr);
            int addingConstValue = 16;
            
            database.Add(addingConstValue);

            Type type = typeof(Database);

            int index = (int)type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "index")
                .GetValue(database);

            int[] extractArr = database.Fetch();
            int actualValue = extractArr[index];
            int expectedValue = 16;

            Assert.That(actualValue,Is.EqualTo(expectedValue));
        }

        //TODO test Remove and Fetch methods
        [Test]
        public void
            RemoveShould_ReduceIndex()
        {
            Database database = new Database(new int[] {1 ,2 ,3});


            database.Remove();
            
            Type type = typeof(Database);

            int index = (int)type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(f => f.Name == "index")
                .GetValue(database);
            int expectedIndex = 1;


            Assert.That(index,Is.EqualTo(expectedIndex));
        }


        [Test]
        public void 
            RemoveFrom_EmptyData_ShouldThrow_Exception()
        {
            Database database = new Database();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        [TestCase(new int[] {})]
        [TestCase(new int[] {1 ,2,3})]
        public void 
            FetchReturn_ElementsAsArray(int[] arr)
        {
            Database database = new Database(arr);

            int[] extractedArray = database.Fetch();

            Assert.That(extractedArray,Is.EqualTo(arr));
        }
    }
}
