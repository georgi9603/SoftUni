using Travel.Core.Controllers;
using Travel.Core.Controllers.Contracts;
using Travel.Entities;
using Travel.Entities.Airplanes;
using Travel.Entities.Contracts;
using Travel.Entities.Items;
using Travel.Entities.Items.Contracts;

namespace Travel.Tests
{
    using Entities.Airplanes.Contracts;
    using NUnit.Framework;

    [TestFixture]
    public class FlightControllerTests
    {
        [Test]
        public void TakeOffNormally()
        {
            IItem item = new CellPhone();
            IPassenger passenger = new Passenger("Gosho");
            IBag bag = new Bag(passenger, new IItem[] { item });
            passenger.Bags.Add(bag);
            IAirplane airplane = new LightAirplane();
            airplane.AddPassenger(passenger);
            IAirport airport = new Airport();
            airport.AddPassenger(passenger);
            ITrip trip = new Trip("tuk", "tam", airplane);
            trip.Complete();
            airport.AddTrip(trip);
            IFlightController flightController = new FlightController(airport);
            var actualResult = flightController.TakeOff();
            var expectedResult = "Confiscated bags: 0 (0 items) => $0";

            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
