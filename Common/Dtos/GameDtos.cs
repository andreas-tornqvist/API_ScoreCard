using Common.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class GameBaseDto
    {
        public GameState State { get; set; }
    }

    public class GamePostDto
    {
        public Guid CreatorId { get; set; }
        public Guid CourseId { get; set; }
    }

    public class GamePutDto : GameBaseDto
    {
        public Guid Id { get; set; }
    }

    public class JoinGameDto
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
    }

    public class GameResponseDto
    {
        public Guid Id { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; } 
    }
}
