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
        public ICollection<CardModel> Cards { get; set; }
        public virtual CourseModel Course { get; set; }
    }
}
