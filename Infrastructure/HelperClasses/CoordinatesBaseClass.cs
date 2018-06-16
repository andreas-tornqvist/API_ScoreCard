using GeoAPI.Geometries;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HelperClasses
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
    internal static class CoordinatesExtensions
    {
        public static double ToRadians(this double degree)
        {
            return degree * Math.PI / 180f;
        }
        /// <summary>
        /// Returns distance between two points in meters
        /// </summary>
        /// <param name="firstPosition"></param>
        /// <param name="secondPosition"></param>
        /// <returns></returns>
        public static double GetDistance(Coordinates firstPosition, Coordinates secondPosition)
        {
            var deltaTheta = Math.Abs(firstPosition.Latitude.ToRadians() - secondPosition.Latitude.ToRadians());
            var deltaPhi = Math.Abs(firstPosition.Longitude.ToRadians() - secondPosition.Longitude.ToRadians());
            var deltaSigma = Math.Atan(Math.Sqrt(
                Math.Pow(Math.Cos(secondPosition.Latitude.ToRadians()) * Math.Sin(deltaPhi), 2) +
                Math.Pow(
                    Math.Cos(firstPosition.Latitude.ToRadians()) * Math.Sin(secondPosition.Latitude.ToRadians()) -
                    Math.Sin(firstPosition.Latitude.ToRadians()) * Math.Cos(secondPosition.Latitude.ToRadians()) *
                    Math.Cos(deltaPhi)
                    , 2)
                    ) / (
                    Math.Sin(firstPosition.Latitude.ToRadians()) * Math.Sin(secondPosition.Latitude.ToRadians()) +
                    Math.Cos(firstPosition.Latitude.ToRadians()) * Math.Cos(firstPosition.Latitude.ToRadians()) *
                    Math.Cos(deltaPhi)
                    ));
            var meanEarthRadius = (firstPosition.EarthRadius + secondPosition.EarthRadius) / 2;
            return deltaSigma * meanEarthRadius;
        }
    }
}
