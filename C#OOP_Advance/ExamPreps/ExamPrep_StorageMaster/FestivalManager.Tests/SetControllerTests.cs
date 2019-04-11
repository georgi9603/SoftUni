using FestivalManager.Core.Controllers;
using FestivalManager.Core.Controllers.Contracts;
using FestivalManager.Entities;
using FestivalManager.Entities.Contracts;
using FestivalManager.Entities.Instruments;
using FestivalManager.Entities.Sets;

namespace FestivalManager.Tests
{
    using NUnit.Framework;
    using System;

	[TestFixture]
	public class SetControllerTests
    {
		[Test]
	    public void checkIfSetCanNotPerform()
		{
		    ISet set = new Short("Set1");
            IStage stage = new Stage();
		    stage.AddSet(set);
		    ISetController setController = new SetController(stage);


		    var actualResult = setController.PerformSets();
		    var expectedResult = "1. Set1:\r\n-- Did not perform";

		    Assert.That(actualResult,Is.EqualTo(expectedResult));
		}

        [Test]
        public void checkIfSetCanPerformSuccesful()
        {
            TimeSpan timeSpan = new TimeSpan( 0,2,30);

            IInstrument instrument = new Guitar();
            ISet set = new Short("Set1");
            IPerformer performer = new Performer("John" , 20);
            performer.AddInstrument(instrument);
            ISong song = new Song("Hello" , timeSpan);
            IStage stage = new Stage();

            set.AddPerformer(performer);
            set.AddSong(song);
            stage.AddSet(set);
            stage.AddSong(song);
            stage.AddPerformer(performer);
            ISetController setController = new SetController(stage);

            var actualResult = setController.PerformSets();
            var expectedResult = "1. Set1:\r\n-- 1. Hello (02:30)\r\n-- Set Successful";
            
            Assert.That(actualResult,Is.EqualTo(expectedResult));
        }

        [Test]
        public void checkIfWearDecrease()
        {
            TimeSpan timeSpan = new TimeSpan( 0,2,30);

            IInstrument instrument = new Guitar();
            ISet set = new Short("Set1");
            IPerformer performer = new Performer("John" , 20);
            performer.AddInstrument(instrument);
            ISong song = new Song("Hello" , timeSpan);
            IStage stage = new Stage();

            set.AddPerformer(performer);
            set.AddSong(song);
            stage.AddSet(set);
            stage.AddSong(song);
            stage.AddPerformer(performer);

            var wearBeforePerform = instrument.Wear;
            ISetController setController = new SetController(stage);
            var result = setController.PerformSets();
            var wearAfterPerform = instrument.Wear;

            Assert.That(wearAfterPerform,Is.Not.EqualTo(wearBeforePerform));
        }
	}
}