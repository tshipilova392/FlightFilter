using System;
using System.Collections.Generic;

namespace Gridnine.FlightCodingTest
{
    class Program
    {
        static void PrintFlights(IList<Flight> flights)
        {
            int i = 1;
            foreach(var flight in flights)
            {
                Console.WriteLine("Flight {0}:", i++);
                foreach (var segment in flight.Segments)
                    Console.WriteLine("\tDepature:{0} \tArrival:{1}", segment.DepartureDate.ToString(),
                                                                    segment.ArrivalDate.ToString());
            }
            Console.WriteLine();
        }

        
        static void Main(string[] args)
        {
            var flightBuilder = new FlightBuilder();
            var flights = flightBuilder.GetFlights();
            Console.WriteLine("All flights:");
            PrintFlights(flights);

            var flightContainer = new FlightsContainer(flights);

            var flightsUntilNow = flightContainer.Filter(Filter.FlightWithDepartureUntilNow);
            Console.WriteLine("Flights with departure until now:");
            PrintFlights(flightsUntilNow);

            var flightsWithWrongSegment = flightContainer.Filter(Filter.FlightThatHasWrongSegment);
            Console.WriteLine("Flights that have segments with arrival defore departure");
            PrintFlights(flightsWithWrongSegment);

            var flightsWithMoreThan2HoursGroundTime = flightContainer.Filter(Filter.FlightWithMoreThan2HoursGroundTime);
            Console.WriteLine("Flights with overall time on the ground more 2 hours");
            PrintFlights(flightsWithMoreThan2HoursGroundTime);
        }
    }
}
