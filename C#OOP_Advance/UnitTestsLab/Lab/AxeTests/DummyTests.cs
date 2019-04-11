using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace AxeTests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void TestIfDummyLoosesHealthIfAttacked()
        {
            Dummy dummy = new Dummy(100, 50);

            dummy.TakeAttack(10);

            Assert.That(dummy.Health, Is.EqualTo(90));
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {
            Dummy dummy = new Dummy(0,50);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(10));
        }

        [Test]
        public void DeadDummyGiveXpIfAttacked()
        {
            Dummy dummy = new Dummy(0, 50);
            
            var result = dummy.GiveExperience();
            var expectedResult = 50;

            Assert.That(expectedResult, Is.EqualTo(result));
        }

        [Test]
        public void AliveDummyCanNotGiveXp()
        {
            Dummy dummy = new Dummy(10,10);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
        }
    }
}