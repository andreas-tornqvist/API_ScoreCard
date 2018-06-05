using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class GroupModel
    {
        public virtual Guid Id { get; set; }
        public virtual ICollection<PlayerModel> Players { get; set; }
        public virtual string Name { get; set; }
    }
}
