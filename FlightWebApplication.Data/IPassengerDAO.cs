using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public interface IPassengerDAO
    {
        public IEnumerable<Passenger> GetPassengers();
        public Passenger GetPassenger(int id);
        public void DeletePassenger(int id);
        public void AddPassenger(Passenger passenger);
        public void UpdatePassenger(Passenger passenger);
    }
}
