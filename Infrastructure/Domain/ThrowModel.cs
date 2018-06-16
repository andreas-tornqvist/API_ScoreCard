using Infrastructure.HelperClasses;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class ThrowModel
    {
        public virtual Guid Id { get; set; }
        public virtual PlayerModel Player { get; set; }
        public virtual S StartLocation { get; set; }
        public virtual CoordinatesBaseClass LandingLocation { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual float Distance { get { StartLocation.Coordinate } }
    }
}
