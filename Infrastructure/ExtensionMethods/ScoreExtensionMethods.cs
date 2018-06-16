using Common.Dtos;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtensionMethods
{
    public static class ScoreExtensionMethods
    {
        public static ScoreModel ToEntity(this ScorePostDto dto, PlayerModel player, CardModel card)
        {
            var score = new ScoreModel
            {
                Hole = dto.Hole,
                Score = dto.Score,
                IsCircleHit = dto.IsCircleHit,
                IsDNF = dto.IsDNF,
                IsInsideCirclePutt = dto.IsInsideCirclePutt,
                IsOB = dto.IsOb,
                IsOutsideCirclePutt = dto.IsOutsideCirclePutt,
                IsTargetHit = dto.IsTargetHit,
                Player = player,
                Card = card
            };
            return score;
        }
    }
}
