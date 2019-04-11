using System.Linq;

namespace Travel.Entities
{
    using Contracts;
    using System.Collections.Generic;

    public class Airport : IAirport
    {
        private List<IBag> confiscatedBags;
        private List<IBag> checkedInBags;
        private List<ITrip> trips;
        private List<IPassenger> passengers;

        public Airport()
        {
            this.confiscatedBags = new List<IBag>();
            this.checkedInBags = new List<IBag>();
            this.trips = new List<ITrip>();
            this.passengers = new List<IPassenger>();
        }


        public IReadOnlyCollection<IBag> CheckedInBags => checkedInBags.AsReadOnly();

        public IReadOnlyCollection<IBag> ConfiscatedBags => confiscatedBags.AsReadOnly();

        public IReadOnlyCollection<IPassenger> Passengers => passengers.AsReadOnly();

        public IReadOnlyCollection<ITrip> Trips => trips.AsReadOnly();

        public IPassenger GetPassenger(string username) => this.Passengers.FirstOrDefault(p => p.Username == username);

        public ITrip GetTrip(string id) => this.Trips.FirstOrDefault(t => t.Id == id);

        public void AddPassenger(IPassenger passenger) => this.passengers.Add(passenger);

        public void AddTrip(ITrip trip) => this.trips.Add(trip);

        public void AddCheckedBag(IBag bag) => this.checkedInBags.Add(bag);

        public void AddConfiscatedBag(IBag bag) => this.confiscatedBags.Add(bag);
    }
}