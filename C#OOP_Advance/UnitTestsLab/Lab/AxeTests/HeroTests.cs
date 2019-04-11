using System;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

[TestFixture()]
public class HeroTests
{
    [Test]
    public void TestHero()
    {
        Mock<IWeapon> fakeAxe = new Mock<IWeapon>();
        Mock<ITarget> fakeTarget = new Mock<ITarget>();

        fakeAxe.Setup(x => x.AttackPoints).Returns(10);
        fakeAxe.Setup(x => x.DurabilityPoints).Returns(10);

        fakeTarget.Setup(x => x.Health).Returns(0);
        fakeTarget.Setup(x => x.GiveExperience()).Returns(10);
        fakeTarget.Setup(x => x.IsDead()).Returns(true);

        Hero hero = new Hero(fakeAxe.Object,"Gosho");
        hero.Attack(fakeTarget.Object);

        var actualResult = hero.Experience;
        var expectedResult = 10;

        Assert.That(actualResult,Is.EqualTo(actualResult));
    }
}