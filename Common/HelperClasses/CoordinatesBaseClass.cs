using Common.Interfaces;
using Infrastructure.ExtensionMethods;
using System;

namespace Common.HelperClasses
{
    public class CoordinatesBaseClass : IHasCoordinates
    {
        public virtual Coordinates Coordinate { get; set; }
    }

    public class Coordinates
    {
        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double EarthRadius => Math.Sqrt(
            (Math.Pow(Math.Pow(_earthEquatorialRadius, 2) * Math.Cos(Latitude.ToRadians()), 2) +
            Math.Pow(Math.Pow(_earthPolarRadius, 2) * Math.Sin(Latitude.ToRadians()), 2)) /
            (Math.Pow(_earthEquatorialRadius * Math.Cos(Latitude.ToRadians()), 2) +
                Math.Pow(_earthPolarRadius * Math.Sin(Latitude.ToRadians()), 2)
                    )
            );
        private double _earthEquatorialRadius => 63781370000;
        private double _earthPolarRadius => 63567523000;
    }
}
