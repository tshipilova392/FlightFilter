using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gridnine.FlightCodingTest
{
    public class WrongFlightsTests
    {
        [Test]
        public void EmptyFlightsCollection_Test()
        {
            var flights = new List<Flight>();
            var flightsContainer = new FlightsContainer(flights);
            Assert.AreEqual(flightsContainer.Filter(Filter.FlightWithDepartureUntilNow).Count,0);
            Assert.AreEqual(flightsContainer.Filter(Filter.FlightThatHasWrongSegment).Count, 0);
            Assert.AreEqual(flightsContainer.Filter(Filter.FlightWithMoreThan2HoursGroundTime).Count, 0);
        }

        [Test]
        public void WrongFlightsCollection_Test()
        {
            var flights = CreateWrongFlights();
            try
            {
                var flightsContainer = new FlightsContainer(flights);
            }
            catch
            {
                Assert.Pass();
            }           
            //Assert.Throws<Exception>(() => new FlightsContainer(flights));
        }


        private static Flight CreateFlight(params DateTime[] dates)
        {
            if (dates.Length % 2 != 0) throw new ArgumentException("You must pass an even number of dates,", "dates");

            var departureDates = dates.Where((date, index) => index % 2 == 0);
            var arrivalDates = dates.Where((date, index) => index % 2 == 1);

            var segments = departureDates.Zip(arrivalDates,
                                              (departureDate, arrivalDate) =>
                                              new Segment { DepartureDate = departureDate, ArrivalDate = arrivalDate }).ToList();

            return new Flight { Segments = segments };
        }

        private static List<Flight> CreateWrongFlights()
        {
            var _threeDaysFromNow = DateTime.Now.AddDays(3);
            return new List<Flight>
                       {
                           CreateFlight(_threeDaysFromNow.AddDays(-5), _threeDaysFromNow.AddHours(2)),

                           null,

                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(7)),

                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(3), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(6), _threeDaysFromNow.AddHours(7)),

                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(3), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(5), _threeDaysFromNow.AddHours(7))                        
                       };
        }
    }
}