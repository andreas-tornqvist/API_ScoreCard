﻿using System;
using System.Collections.Generic;

namespace Infrastructure.Domain
{
    public class CourseModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<HoleModel> Holes { get; set; }
        public virtual ICollection<GameModel> Games { get; set; }
    }
}
