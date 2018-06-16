using Common.Dtos;
using Common.Enumerators;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtensionMethods
{
    public static class GameExtensionMethods
    {
        public static GameModel ToEntity(this GamePostDto dto, PlayerModel creator, CourseModel course, CardModel card)
        {
            var game = new GameModel
            {
                Secretary = creator,
                Course = course,
                State = GameState.Pending,
                Cards = new List<CardModel>
                {
                    card
                }
            };
            return game;
        }
        public static GameResponseDto ToDto(this GameModel game)
        {
            var report = new GameResponseDto
            {
                Id = game.Id,
                CreatorFirstName = game.Secretary.FirstName,
                CreatorLastName = game.Secretary.LastName
            };
            return report;
        }
    }
}
