namespace Travel.Entities.Airplanes
{
    using Contracts;
    using Entities.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Airplane : IAirplane
	{
		private readonly List<IBag> baggageCompartment;
		private readonly List<IPassenger> passengers;

		protected Airplane(int seats, int baggageCompartments) {

			this.Seats = seats;
			this.BaggageCompartments = baggageCompartments;

		    this.passengers = new List<IPassenger>();
			this.baggageCompartment = new List<IBag>();
		}

	    public int Seats { get; }
	    public int BaggageCompartments { get; }

		public IReadOnlyCollection<IBag> BaggageCompartment => this.baggageCompartment.AsReadOnly();
		public IReadOnlyCollection<IPassenger> Passengers => this.passengers.AsReadOnly();

		public bool IsOverbooked => this.Passengers.Count > this.Seats;

        public void AddPassenger(IPassenger passenger)
	    {
			this.passengers.Add(passenger);
		}

		public void LoadBag(IBag bag) 
		{
		    if (this.BaggageCompartment.Count > this.BaggageCompartments)
		    {
				throw new InvalidOperationException($"No more bag room in {this.GetType().Name}!");
		    }

			this.baggageCompartment.Add(bag);
		}

        public IPassenger RemovePassenger(int seat)
        {
            IPassenger passenger = this.passengers[seat];

            this.passengers.RemoveAt(seat);

            return passenger;
        }

        public IEnumerable<IBag> EjectPassengerBags(IPassenger passenger)
        {
           var passengerBags = this.BaggageCompartment
                .Where(b => b.Owner == passenger)
                .ToList();

            foreach (var bag in passengerBags)
                this.baggageCompartment.Remove(bag);

            return passengerBags;
        }
    }
}