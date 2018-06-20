using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class CourseBaseDto
    {
        public string Name { get; set; }
    }

    public class CourseResponseDto : CourseBaseDto
    {
        public IEnumerable<HoleResponseDto> Holes { get; set; }
    }

    public class CoursePostDto : CourseBaseDto
    {
        public IEnumerable<HolePostDto> Holes { get; set; }
    }
}
