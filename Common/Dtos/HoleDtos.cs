using Common.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class HoleBaseDto
    {
        public int Number { get; set; }
        public float Length { get; set; }
        public int Par { get; set; }
        public Coordinates TeeLocation { get; set; }
        public Coordinates TargetLocation { get; set; }
    }

    public class HolePostDto : HoleBaseDto
    {
        public Guid CourseId { get; set; }
    }

    public class HoleResponseDto : HoleBaseDto
    {
        public CourseResponseDto Course { get; set; }
    }
}
