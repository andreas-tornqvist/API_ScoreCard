﻿using System;
using System.Collections.Generic;

namespace Infrastructure.Domain
{
    public class CardModel
    {
        public virtual Guid Id { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual DateTime EndTime { get; set; }
        public virtual bool IsGatheringCard { get; set; }
        public virtual ICollection<ScoreModel> Scores { get; set; }
        public virtual ICollection<PlayerModel> Players { get; set; }
        public virtual ICollection<PlayerModel> ApprovingPlayers { get; set; }
        public virtual GameModel Game { get; set; }
    }
}
