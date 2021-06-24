using System;
using System.Collections.Generic;
using System.Text;

namespace Gridnine.FlightCodingTest
{
    public class Filter
    {
        public static bool FlightWithDepartureUntilNow(Flight flight)
        {
            DateTime now = DateTime.Now;
            var firstSegment = flight.Segments[0];
            return firstSegment.DepartureDate < now;
        }

        public static bool FlightThatHasWrongSegment(Flight flight)
        {
            var segments = flight.Segments;
            foreach (var segment in segments)
            {
                if (segment.ArrivalDate < segment.DepartureDate)
                    return true;
            }
            return false;
        }

        public static bool FlightWithMoreThan2HoursGroundTime(Flight flight)
        {
            double groundHours = 0.0;
            var segments = flight.Segments;
            for (var i = 0; i < segments.Count - 1; i++)
            {
                var arrival = segments[i].ArrivalDate;
                var departure = segments[i + 1].DepartureDate;
                var timeGap = departure.Subtract(arrival).TotalHours;
                groundHours += timeGap;
            }
            return groundHours > 2;
        }
    }
}
