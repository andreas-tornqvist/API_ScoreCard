using Infrastructure.Domain;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Spatial.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Types;
using Infrastructure.Types;

namespace Infrastructure.Data
{
    public class NHibernateMapper
    {
        private readonly ModelMapper _modelMapper;

        public NHibernateMapper()
        {
            _modelMapper = new ModelMapper();
            _modelMapper.BeforeMapProperty += (modelInspector, member, propertyCustomizer) =>
            {
                propertyCustomizer.NotNullable(true);

            };
            _modelMapper.BeforeMapClass += (modelInspector, type, classCustomizer) => classCustomizer.Id(type.GetProperty("Id"),
                                           idMapper =>
                                           {
                                               idMapper.Access(Accessor.Property);
                                               idMapper.Generator(Generators.GuidComb);
                                           });
            //+= (modelInspector, member, classCustomizer) => classCustomizer.Id
        }

        public HbmMapping Map()
        {
            MapCourse();
            MapGame();
            MapCard();
            MapHole();
            MapPlayer();
            MapScore();
            MapGroup();
            MapPassword();
            MapTour();
            return _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        private void MapTour()
        {
            _modelMapper.Class<TourModel>(e =>
            {
                e.Id(p => p.Id);
                e.Property(p => p.Name);
                e.Property(p => p.Description, p => p.NotNullable(false));
                e.Set(p => p.Games, mapper =>
                {
                    mapper.Inverse(true);
                    mapper.Key(k => k.Column(col => col.Name("TourId")));
                    mapper.Cascade(Cascade.All);
                }, map => map.OneToMany());
                e.ManyToOne(p => p.Creator, mapper =>
                {
                    mapper.Column(col => col.Name("CreatorId"));
                    mapper.Cascade(Cascade.None);
                    mapper.NotNullable(true);
                });
            });
        }

        private void MapGame()
        {
            _modelMapper.Class<GameModel>(e =>
            {
                e.Id(p => p.Id);
                e.Property(p => p.State, p => { p.Column(col => col.SqlType("int")); });
                e.Set(p => p.Cards, mapper =>
                {
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column(col => col.Name("GameId")));
                }, map => map.OneToMany());
                e.ManyToOne(p => p.Course, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Column("CourseId");
                    mapper.NotNullable(true);
                });
                e.ManyToOne(p => p.Secretary, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Column(col => col.Name("SecretaryId"));
                    mapper.NotNullable(true);
                });
                e.ManyToOne(p => p.Tour, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Column(col => col.Name("TourId"));
                    mapper.NotNullable(false);
                });
            });
        }

        private void MapPassword()
        {
            _modelMapper.Class<PasswordModel>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Password);
                e.Property(p => p.PlayerId);
            });
        }

        private void MapGroup()
        {
            _modelMapper.Class<GroupModel>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Name, p => p.NotNullable(false));
                e.Set(p => p.Players, mapper =>
                {
                    mapper.Inverse(false);
                    mapper.Cascade(Cascade.None);
                    mapper.Table("PlayerGroup");
                    mapper.Key(k => k.Column(col => col.Name("GroupId")));
                }, map => map.ManyToMany(p =>
                {
                    p.ForeignKey("FK_PlayerGroup_Player");
                    p.Column("PlayerId");
                    p.Class(typeof(PlayerModel));
                }));
            });
        }

        private void MapScore()
        {
            _modelMapper.Class<ScoreModel>(e =>
            {
                e.Id(p => p.Id);
                e.Property(p => p.IsOB);
                e.Property(p => p.Score);
                e.Property(p => p.Hole);
                e.ManyToOne(p => p.Card, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Column("CardId");
                    mapper.NotNullable(true);
                });
                e.ManyToOne(p => p.Player, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Column("PlayerId");
                    mapper.NotNullable(true);
                });
                e.Set(p => p.ApprovedBy, mapper =>
                {
                    mapper.Inverse(false);
                    mapper.Table("PlayerApproval");
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column("ScoreId"));
                }, map => map.ManyToMany(p =>
                {
                    p.Column("PlayerId");
                    p.ForeignKey("FK_PlayerApproval_Player");
                    p.Class(typeof(PlayerModel));
                }));
            });
        }

        private void MapPlayer()
        {
            _modelMapper.Class<PlayerModel>(e =>
            {
                e.Id(p => p.Id);
                e.Property(p => p.FirstName);
                e.Property(p => p.LastName);
                e.Property(p => p.Email, p => p.NotNullable(false));
                e.Property(p => p.IsActive);
                e.Property(p => p.RegistrationDate);
                e.Property(p => p.Rating, p => p.NotNullable(false));
                e.Set(p => p.Scores, mapper =>
                {
                    mapper.Key(key => key.Column(col => col.Name("PlayerId")));
                    mapper.Inverse(true);
                }, p => p.OneToMany());
                e.Set(p => p.Cards, mapper =>
                {
                    mapper.Inverse(true);
                    mapper.Cascade(Cascade.None);
                    mapper.Table("CardPlayer");
                    mapper.Key(k => k.Column("PlayerId"));
                }, map => map.ManyToMany(p =>
                {
                    p.Column("CardId");
                    p.ForeignKey("FK_CardPlayer_Card");
                    p.Class(typeof(CardModel));
                }));
                e.Set(p => p.Groups, mapper =>
                {
                    mapper.Inverse(true);
                    mapper.Cascade(Cascade.None);
                    mapper.Table("PlayerGroup");
                    mapper.Key(k => k.Column("PlayerId"));
                }, map => map.ManyToMany(p =>
                {
                    p.Column("GroupId");
                    p.ForeignKey("FK_PlayerGroup_Group");
                    p.Class(typeof(GroupModel));
                }));
                e.Set(p => p.Approvals, mapper =>
                {
                    mapper.Inverse(true);
                    mapper.Cascade(Cascade.None);
                    mapper.Table("PlayerApproval");
                    mapper.Key(k => k.Column("PlayerId"));
                }, map => map.ManyToMany(p =>
                {
                    p.Column("ScoreId");
                    p.ForeignKey("FK_PlayerApproval_Score");
                    p.Class(typeof(ScoreModel));
                }));
                e.Set(p => p.CardApprovals, mapper =>
                {
                    mapper.Inverse(false);
                    mapper.Table("CardApprovingPlayer");
                    mapper.Key(k => k.Column(col => col.Name("PlayerId")));
                    mapper.Cascade(Cascade.All);
                }, map => map.ManyToMany(p =>
                {
                    p.Column("CardId");
                    p.ForeignKey("FK_CardApprovingPlayer_Card");
                    p.Class(typeof(CardModel));
                }));
                e.Set(p => p.CreatedTours, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Inverse(true);
                    mapper.Key(k => k.Column(col => col.Name("CreatorId")));
                }, map => map.OneToMany());
            });
        }

        private void MapHole()
        {
            _modelMapper.Class<HoleModel>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Length);
                e.Property(p => p.Number);
                e.Property(p => p.Par);
                e.Property(p => p.TargetLocation, p => {
                    p.Column(col => col.SqlType("nvarchar(100)"));
                    p.Type<CoordinatesNHibernateType>();
                });
                e.Property(p => p.TeeLocation, p => {
                    p.Column(col => col.SqlType("nvarchar(100)"));
                    p.Type<CoordinatesNHibernateType>();
                });
                e.ManyToOne(p => p.Course, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                    mapper.Column("CourseId");
                    mapper.NotNullable(true);
                });
            });
        }

        private void MapCard()
        {
            _modelMapper.Class<CardModel>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.StartTime);
                e.Property(p => p.EndTime, p => p.NotNullable(false));
                e.Property(p => p.IsGatheringCard);
                e.Set(p => p.Players, mapper =>
                {
                    mapper.Inverse(false);
                    mapper.Key(key => key.Column("CardId"));
                    mapper.Table("CardPlayer");
                    mapper.Cascade(Cascade.None);
                }, map => map.ManyToMany(p =>
                {
                    p.Column("PlayerId");
                    p.ForeignKey("FK_CardPlayer_Player");
                    p.Class(typeof(PlayerModel));
                }));
                e.Set(p => p.Scores, mapper =>
                {
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column(col => col.Name("CardId")));
                    mapper.Inverse(false);
                }, map => map.OneToMany());
                e.Set(p => p.ApprovingPlayers, mapper =>
                {
                    mapper.Inverse(true);
                    mapper.Table("CardApprovingPlayer");
                    mapper.Key(k => k.Column(col => col.Name("CardId")));
                    mapper.Cascade(Cascade.None);
                }, map => map.ManyToMany(p =>
                {
                    p.Column("PlayerId");
                    p.ForeignKey("FK_CardApprovingPlayer_Player");
                    p.Class(typeof(PlayerModel));
                }));
                e.ManyToOne(p => p.Game, mapper =>
                {
                    mapper.NotNullable(true);
                    mapper.Column(col => col.Name("GameId"));
                    mapper.Cascade(Cascade.None);
                });
            });
        }

        private void MapCourse()
        {
            _modelMapper.Class<CourseModel>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Name);
                e.Set(p => p.Holes, mapper =>
                {
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column(col => col.Name("CourseId")));
                }, map => map.OneToMany());
                e.Set(p => p.Games, mapper =>
                {
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column(col => col.Name("CourseId")));
                }, map => map.OneToMany());
            });
        }
    }
}
