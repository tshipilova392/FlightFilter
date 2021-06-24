using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gridnine.FlightCodingTest
{
    public class SimpleTests
    {
        List<Flight> allFlights;
        FlightsContainer flightsContainer;
        [SetUp]
        public void Setup()
        {
            List<Flight> flights = CreateTestFlights();
            allFlights = flights;
            flightsContainer = new FlightsContainer(allFlights);
        }

        [Test]
        public void TestForFirstFilter()
        {
            var flightsUntilNow = flightsContainer.Filter(Filter.FlightWithDepartureUntilNow);
            Assert.AreEqual(flightsUntilNow.Count, 2);
        }

        [Test]
        public void TestForSecondFilter()
        {
            var flightsWithWrongSegment = flightsContainer.Filter(Filter.FlightThatHasWrongSegment);
            Assert.AreEqual(flightsWithWrongSegment.Count, 1);
        }
        [Test]
        public void TestForThirdFilter()
        {
            var flightsWithMoreThan2HoursGroundTime = flightsContainer.Filter(Filter.FlightWithMoreThan2HoursGroundTime);
            Assert.AreEqual(flightsWithMoreThan2HoursGroundTime.Count, 3);
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

        private static List<Flight> CreateTestFlights()
        {
            var _threeDaysFromNow = DateTime.Now.AddDays(3);
            return new List<Flight>
                       {
                           CreateFlight(_threeDaysFromNow.AddDays(-5), _threeDaysFromNow.AddHours(2)),

                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(7)),

                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(3), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(6), _threeDaysFromNow.AddHours(7)),

                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(3), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(5), _threeDaysFromNow.AddHours(7)),

                           //A normal flight with two hour duration
			               CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2)),

                           //A normal multi segment flight
			               CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(3), _threeDaysFromNow.AddHours(5)),
                           
                           //A flight departing in the past
                           CreateFlight(_threeDaysFromNow.AddDays(-6), _threeDaysFromNow),

                           //A flight that departs before it arrives
                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(-6)),

                           //A flight with more than two hours ground time
                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(5), _threeDaysFromNow.AddHours(6)),

                            //Another flight with more than two hours ground time
                           CreateFlight(_threeDaysFromNow, _threeDaysFromNow.AddHours(2), _threeDaysFromNow.AddHours(3), _threeDaysFromNow.AddHours(4), _threeDaysFromNow.AddHours(6), _threeDaysFromNow.AddHours(7))
                       };
        }
    }
}