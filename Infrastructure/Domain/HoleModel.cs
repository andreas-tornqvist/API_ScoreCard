using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class HoleModel
    {
        public virtual Guid Id { get; set; }
        public virtual int Number { get; set; }
        public virtual float Length { get; set; }
        public virtual int Par { get; set; }
        public virtual Point TeeLocation { get; set; }
        public virtual Point TargetLocation { get; set; }
        public virtual CourseModel Course { get; set; }
    }
}
