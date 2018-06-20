using Common.Dtos;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtensionMethods
{
    public static class TourExtensionMethods
    {
        public static TourModel ToEntity(this TourPostDto dto, PlayerModel creator, CourseModel course)
        {
            var tour = new TourModel
            {
                Name = dto.Name,
                Description = dto.Description,
                Creator = creator,
                Games = dto.Games.Select(g => g.ToEntity(creator, course, new CardModel
                {
                    IsGatheringCard = true
                })).ToList()
            };
            return tour;
        }
    }
}
