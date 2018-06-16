using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class ScoreModel
    {
        public virtual Guid Id { get; set; }
        public virtual PlayerModel Player { get; set; }
        public virtual CardModel Card { get; set; }
        public virtual int Hole { get; set; }
        public virtual int Score { get; set; }
        /// <summary>
        /// Was the disc outside OB during the play
        /// </summary>
        public virtual bool IsOB { get; set; }
        /// <summary>
        /// Did the player score the first throw after landing inside the 10m circle?
        /// </summary>
        public virtual bool IsInsideCirclePutt { get; set; }
        /// <summary>
        /// Did the disc land inside the 10m circle on the -2 or lower throw?
        /// </summary>
        public virtual bool IsCircleHit { get; set; }
        /// <summary>
        /// Did the player score from outside the 10m circle?
        /// </summary>
        public virtual bool IsOutsideCirclePutt { get; set; }
        /// <summary>
        /// Did the disc hit the target on the -2 or lower throw? This property does not overwrite IsCicrleHit, since a disc can hit the target and roll away from the 10m circle
        /// </summary>
        public virtual bool IsTargetHit { get; set; }
        /// <summary>
        /// The state of the score: -1: rejected, 0: not set, 1: approved by 1 player, n: approved by n players.
        /// </summary>
        public virtual int State { get { return ApprovedBy?.Count ?? 0; } }
        public virtual ICollection<PlayerModel> ApprovedBy { get; set; }
        /// <summary>
        /// Did the player DNF the round before or at this hole?
        /// </summary>
        public virtual bool IsDNF { get; set; }
    }
}
