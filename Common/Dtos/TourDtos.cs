using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class TourBaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TourPostDto : TourBaseDto
    {
        public IEnumerable<GamePostDto> Games { get; set; }
        public Guid? CreatorId { get; set; }
        public Guid CourseId { get; set; }
    }

    public class TourResponseDto : TourBaseDto
    {
        public IEnumerable<GameResponseDto> Games { get; set; }
        public PlayerResponseDto Creator { get; set; }
        public CourseResponseDto Course { get; set; }
    }
}
