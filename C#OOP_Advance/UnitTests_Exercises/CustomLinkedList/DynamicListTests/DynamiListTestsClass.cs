using System;
using System.Linq;
using System.Reflection;
using CustomLinkedList;
using NUnit.Framework;

namespace DynamiListTestsClass
{
    [TestFixture]
    public class Tests
    {
        private DynamicList<int> list;

        [SetUp]
        public void creatingList()
        {
            list = new DynamicList<int>();
        }

        [Test]
        public void DynamicListContainsCount()
        {
            Type type = typeof(DynamicList<>);

            var actualResult = type.GetProperties().FirstOrDefault(x => x.Name == "Count");
            string expectedResult = "Count";
            
            Assert.That(actualResult.Name,Is.EqualTo(expectedResult),
                $"Initializing empty list should set Count to 0, but was {list.Count}");
        }

        [Test]
        public void CreatingDynamicListShouldCreateEmptyList()
        {
            var expectedResult = 0;
            var actualResult = list.Count;

            Assert.That(actualResult,Is.EqualTo(expectedResult));
            Assert.Throws<ArgumentOutOfRangeException>(() => expectedResult = list[0],
                $"Creating empty list should create empty list but length was {list.Count}");
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(-3)]
        [TestCase(int.MaxValue)]
        public void GetFromEmptyListShouldThrowException(int index)
        {
            var result = 0;

            Assert.Throws<ArgumentOutOfRangeException>(() => result = list[index],
                $"Getting from empty list should throw ArgumentOutOfRangeException, but result was {result}");
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(-3)]
        [TestCase(int.MaxValue)]
        public void SetInvalidIndexShouldThrowException(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>((() => list[index] = 420),
                
                $"Setting on invalid index should throw ArgumentOutOfRangeException, but on index {index} was 420");
        }

        [Test]
        public void AddingElementsShouldIncreaseCounter()
        {
            int expectedCounter = 3;
            list.Add(0);
            list.Add(42);
            list.Add(69);
            int actualCount = list.Count;

            Assert.That(actualCount,Is.EqualTo(expectedCounter),
                $"Adding element in list should increase counter,but count was {list.Count}");
        }

        [Test]
        [TestCase(2)]
        public void GetShouldGetElementCorrectly(int index)
        {
            int expectedElement = 69;
            list.Add(0);
            list.Add(42);
            list.Add(69);
            int actualElement = list[index];

            Assert.That(actualElement,Is.EqualTo(expectedElement),
                $"Getting should get value from index correctly but was {list[index]}");
        }

        [Test]
        [TestCase(2)]
        public void SetShouldSetElementCorrectly(int index)
        {
            int expectedElement = 69;
            list.Add(0);
            list.Add(42);
            list.Add(-3);

            list[2] = 69;
            int actualElement = list[index];
            
            Assert.That(actualElement,Is.EqualTo(expectedElement),
                $"Set should set element correctly but at index, was {list[index]}");
        }

        [Test]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(-3)]
        [TestCase(int.MaxValue)]
        public void RemoveAtInvalidIndexShouldThrowException(int index)
        {
            list.Add(0);
            list.Add(42);
            list.Add(-3);
            var result = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() => result = list.RemoveAt(index),
                $"RemoveAt from invalid index should throw ArgumentOutOfRangeException,but {result}");
        }

        [Test]
        [TestCase(2)]
        public void RemoveAtShouldReturnCorrectElement(int index)
        {
            list.Add(0);
            list.Add(42);
            list.Add(-3);

            int actualElement = list.RemoveAt(index);
            int expectedElement = -3;

            Assert.That(actualElement,Is.EqualTo(expectedElement),
                $"RemoveAt from valid list should return correct element, but was {actualElement}");
        }

        [Test]
        public void RemoveAtDecreaseCounter()
        {
            list.Add(0);
            list.Add(42);
            list.Add(-3);

            int garbage  = list.RemoveAt(2);
            int actualCount = list.Count;
            int expectedCount = 2;

            Assert.That(actualCount,Is.EqualTo(expectedCount),
                $"Removing from list should decrease counter, but counter was {list.Count}");
        }

        [Test]
        public void RemoveAtShouldArrangeList()
        {
            list.Add(0);
            list.Add(42);
            list.Add(-3);
            
            int garbage  = list.RemoveAt(1);
            int actualElement = list[1];
            int expectedElement = -3;

            Assert.That(actualElement,Is.EqualTo(expectedElement),
            $"RemoveAt should arrange list, but didn't");
        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(69)]
        [TestCase(int.MaxValue)]
        public void RemoveInvalidIndexShouldReturnMinusOne(int index)
        {
            int actualResult = list.Remove(index);
            int expectedResult = -1;

           Assert.That(actualResult,Is.EqualTo(expectedResult),
               $"Removing from invalid index should return -1, but was {expectedResult}");
        }

        [Test]
        public void RemoveValidIndexShouldReturnCorrectIndex()
        {
            list.Add(-1);
            list.Add(3);
            list.Add(42);

            int actualResult = list.Remove(-1);
            int expectedResult = 0;

            Assert.That(actualResult,Is.EqualTo(expectedResult),
                $"Removing from valid index should return correct index, but was {actualResult}");
        }

        [Test]
        public void RemoveValidIndexShouldArrangeList()
        {
            list.Add(-1);
            list.Add(3);
            list.Add(42);

            int garbage = list.Remove(-1);
            int actualResult = list[0];
            int expectedResult = 3;

            Assert.That(actualResult,Is.EqualTo(expectedResult),
                $"Removing from valid index should arrange index, but didn't");
        }

        [Test]
        [TestCase(4)]
        public void IndexOfInvalidIndexShouldReturnMinusOne(int index)
        {
            list.Add(-1);
            list.Add(3);
            list.Add(42);

            int actualResult = list.IndexOf(index);
            int expectedResult = -1;

            Assert.That(actualResult,Is.EqualTo(expectedResult),
                $"Looking invalid element should return -1, but was {actualResult}");
        }

        [Test]
        public void IndexOfValidIndexShouldReturnIndexOfElement()
        {
            list.Add(-1);
            list.Add(3);
            list.Add(42);

            int actualResult = list.IndexOf(42);
            int expectedResult = 2;

            Assert.That(actualResult,Is.EqualTo(expectedResult),
                $"Looking for valid element should return {expectedResult}, but was different.");
        }

        [Test]
        public void ContainsInvalidIndexShouldReturnFalse()
        {
            list.Add(-1);
            list.Add(3);
            list.Add(42);

            Assert.IsFalse(list.Contains(4),
                $"Looking for element that doesn't exist should return false,but was true");
        }

        [Test]
        public void ContainsValidIndexShouldReturnTrue()
        {
            list.Add(-1);
            list.Add(3);
            list.Add(42);

            Assert.IsTrue(list.Contains(3),
                $"Looking for element that exists in list should return true,but was false");
        }
    }
}