using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Dtos;

namespace API_ScoreCard.Repositories
{
    public interface ITourRepository
    {
        Guid? CreateTour(TourPostDto dto);
    }
}