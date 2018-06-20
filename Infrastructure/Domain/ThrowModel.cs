using System;
using Infrastructure.ExtensionMethods;
using Common.HelperClasses;

namespace Infrastructure.Domain
{
    public class ThrowModel
    {
        public virtual Guid Id { get; set; }
        public virtual PlayerModel Player { get; set; }
        public virtual Coordinates StartLocation { get; set; }
        public virtual Coordinates LandingLocation { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual double Distance { get { return CoordinatesHelperMethods.GetDistance(StartLocation, LandingLocation); } }
    }
}
