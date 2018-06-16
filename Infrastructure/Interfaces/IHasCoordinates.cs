using Infrastructure.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IHasCoordinates
    {
        Coordinates Coordinate { get; set; }
    }
}
