using System;
using System.Collections.Generic;
using System.Text;
using Sample.Entities;

namespace Sample.DataAccess.Abstract
{
    public interface IGameRepository
    {
        List<ReLeaderboard> GetLeaderboards();

        List<ReLeaderboard> GetLeaderboardByCountry(string country);

        ReSubmit SetScore(ReSubmit submit);

        ReUser GetUserByGuid(Guid userId);

        ReUser CreateUser(ReUser user);
    }
}
