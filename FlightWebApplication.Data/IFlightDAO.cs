using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public interface IFlightDAO
    {
        public IEnumerable<Flight> GetFlights();
        public Flight GetFlight(int id);
        public void DeleteFlight(int id);
        public void AddFlight(Flight flight);
        public void UpdateFlight(Flight flight);

       // public IEnumerable<ActiveFlight> GetTable();
        
    }
}
