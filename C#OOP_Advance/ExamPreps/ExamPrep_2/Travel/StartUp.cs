namespace Travel
{
    using Core;
    using Core.Controllers;
    using Core.IO;
    using Core.IO.Contracts;
    using Entities;
    using Core.Controllers.Contracts;
    using Entities.Contracts;

    public static class StartUp
	{
		public static void Main(string[] args)
		{
			IReader reader = new ConsoleReader();
		    IWriter writer = new ConsoleWriter();
		    IAirport airport = new Airport();
			IAirportController airportController = new AirportController(airport);
			IFlightController flightController = new FlightController(airport);

			var engine = new Engine(reader, writer, airportController, flightController);

			engine.Run();
		}
	}
}