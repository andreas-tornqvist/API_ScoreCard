using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class PlayerModel
    {
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual ICollection<ScoreModel> Scores { get; set; }
        public virtual ICollection<CardModel> Cards { get; set; }
        public virtual ICollection<GroupModel> Groups { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime RegistrationDate { get; set; }
        public virtual float? Rating { get; set; }
        public virtual ICollection<ScoreModel> Approvals { get; set; }
        public virtual ICollection<CardModel> CardApprovals { get; set; }
    }
}
