using Common.HelperClasses;
using System;

namespace Infrastructure.ExtensionMethods
{
    public static class CoordinatesHelperMethods
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
