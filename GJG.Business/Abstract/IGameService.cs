using System;
using System.Collections.Generic;
using System.Text;
using GJG.Entities;

namespace GJG.Business.Abstract
{
    public interface IGameService
    {
        List<ReLeaderboard> GetLeaderboards();

        List<ReLeaderboard> GetLeaderboardByCountry(string country);

        ReSubmit SetScore(ReSubmit submit);

        ReUser GetUserByGuid(Guid userId);

        ReUser CreateUser(ReUser user);
    }
}
