using Common.Dtos;
using Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExtensionMethods
{
    public static class PlayerExtensionMethods
    {
        public static PlayerModel ToEntity(this PlayerPostDto dto)
        {
            var player = new PlayerModel
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Rating = dto.Rating
            };
            return player;
        }

        public static PlayerResponseDto ToDto(this PlayerModel player)
        {
            var response = new PlayerResponseDto
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Email = player.Email,
                Rating = player.Rating,
                RegistrationDate = player.RegistrationDate
            };
            return response;
        }
    }
}
