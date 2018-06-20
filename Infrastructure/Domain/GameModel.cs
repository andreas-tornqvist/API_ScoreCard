using Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class GameModel
    {
        public virtual Guid Id { get; set; }
        public virtual ICollection<CardModel> Cards { get; set; }
        public virtual PlayerModel Secretary { get; set; }
        public virtual CourseModel Course { get; set; }
        public virtual GameState State { get; set; }
        public virtual TourModel Tour { get; set; }
    }
}
