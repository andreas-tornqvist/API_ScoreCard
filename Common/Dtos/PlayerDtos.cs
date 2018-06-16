using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class PlayerBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float? Rating { get; set; }
        public string Email { get; set; }
    }

    public class PlayerPostDto : PlayerBaseDto
    {
    }

    public class PlayerPatchDto : PlayerBaseDto
    {
        public bool IsActive { get; set; }
    }

    public class PlayerResponseDto : PlayerBaseDto
    {
        public Guid Id { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
