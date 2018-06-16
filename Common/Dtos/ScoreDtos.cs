using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class ScoreBaseDto
    {
        public int Hole { get; set; }
        public int MyProperty { get; set; }
        public int Score { get; set; }
        public bool IsOb { get; set; }
        public bool IsInsideCirclePutt { get; set; }
        public bool IsCircleHit { get; set; }
        public bool IsOutsideCirclePutt { get; set; }
        public bool IsTargetHit { get; set; }
        public int State { get; set; }
        public bool IsDNF { get; set; }
    }
    public class ScorePostDto : ScoreBaseDto
    {
        public Guid PlayerId { get; set; }
        public Guid CardId { get; set; }
    }
}
