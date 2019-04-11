using System;
using NUnit.Framework;

namespace AxeTests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void ValidateIfAxeLooseDurabilityAfterAttack()
        {
            Axe axe = new Axe(5,10);
            Dummy dummy = new Dummy(10,10);

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints,Is.EqualTo(9),
                "Axe Durability doesn't change after attack.");
        }

        [Test]
        public void ValidateIfAxeIsAttackingBroken()
        {
            //Apply Act Assert
            Axe axe = new Axe(10,0);
            Dummy dummy = new Dummy(10,10);
            
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }
}
