using Common.HelperClasses;
using System;

namespace Infrastructure.Domain
{
    public class HoleModel
    {
        public virtual Guid Id { get; set; }
        public virtual int Number { get; set; }
        public virtual float Length { get; set; }
        public virtual int Par { get; set; }
        public virtual Coordinates TeeLocation { get; set; }
        public virtual Coordinates TargetLocation { get; set; }
        public virtual CourseModel Course { get; set; }
    }
}
