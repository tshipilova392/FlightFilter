using System;
using System.Collections.Generic;
using System.Text;

namespace Gridnine.FlightCodingTest
{
    public class FlightsContainer
    {
        private IList<Flight> flights;
        public FlightsContainer(IList<Flight> flights)
        {
            if (!FlightsAreOk(flights)) throw new Exception("Wrong data");
            this.flights = flights;
        }
        public IList<Flight> Filter(Func<Flight,bool> flightIsOk)
        {
            IList<Flight> result = new List<Flight>();
            foreach (var flight in flights)
            {
                if (flightIsOk(flight))
                {
                    result.Add(flight);
                }
            }
            return result;
        }
        private bool FlightsAreOk(IList<Flight> flights)
        {
            if (flights == null) return false;
            foreach(var flight in flights)
            {
                if (!FlightIsOk(flight)) return false;
            }
            return true;
        }
        private bool FlightIsOk(Flight flight)
        {
            if (flight == null) return false;
            var segments = flight.Segments;
            if ((segments == null) || (segments.Count == 0))
                return false;
            return true;
        }
    }
}
