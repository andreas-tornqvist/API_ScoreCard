﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using Common.Dtos;
using Infrastructure.ExtensionMethods;
using Infrastructure.Domain;

namespace API_ScoreCard.Repositories
{
    public class PlayerRepository : RepositoryBase, IPlayerRepository
    {
        public PlayerRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public Guid CreatePlayer(PlayerPostDto dto)
        {
            var player = dto.ToEntity();
            using (var transaction = Session.BeginTransaction())
            {
                Session.Save(player);
                transaction.Commit();
            }
            return player.Id;
        }
        /// <summary>
        /// Returns null if player does not exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PlayerResponseDto GetPlayer(Guid id)
        {
            var player = Session.Get<PlayerModel>(id);
            return player?.ToDto();
        }

        public bool UpdatePlayer(PlayerPatchDto dto, Guid id)
        {
            var player = Session.Get<PlayerModel>(id);
            if (player == null) return false;
            player.FirstName = dto.FirstName;
            player.LastName = dto.LastName;
            player.Email = dto.Email;
            player.Rating = dto.Rating;
            player.IsActive = dto.IsActive;
            using (var transaction = Session.BeginTransaction())
            {
                Session.Update(player);
                transaction.Commit();
            }
            return true;
        }

        public bool DeletePlayer(Guid id)
        {
            var player = Session.Get<PlayerModel>(id);
            if (player == null) return false;
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(id);
                transaction.Commit();
            }
            return true;
        }
    }
}