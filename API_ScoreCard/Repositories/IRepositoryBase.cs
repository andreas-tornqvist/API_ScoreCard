﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ScoreCard.Repositories
{
    public interface IRepositoryBase
    {
        ISession Session { get; }
    }
}