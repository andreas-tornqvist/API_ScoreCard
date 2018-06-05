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
            return _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        private void MapGame()
        {
            _modelMapper.Class<GameModel>(e =>
            {
                e.Id(p => p.Id);
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
                e.Property(p => p.TargetLocation, p => p.Column(col => col.SqlType("geography")));
                e.Property(p => p.TeeLocation, p => p.Column(col => col.SqlType("geography")));
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
