using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using GJG.DataAccess.Abstract;
using GJG.Entities;

namespace GJG.DataAccess.Concrete
{
    public class GameRepository : IGameRepository
    {
        public List<ReLeaderboard> GetLeaderboards()
        {
            List<ReLeaderboard> res = new List<ReLeaderboard>();

            using (var GJGDbContext = new GJGDbContext())
            {
                var data = GJGDbContext.Leaderboards.OrderByDescending(p => p.Points).ToList();

                for (int i = 0; i < data.Count; i++)
                {
                    data[i].Rank = i + 1;
                }

                foreach (var leaderboard in data)
                {
                    var ranked = new ReLeaderboard
                    {
                        Country = GetUser(leaderboard.User_Id).Country,
                        Display_Name = GetUser(leaderboard.User_Id).Display_Name,
                        Point = leaderboard.Points,
                        Rank = leaderboard.Rank
                    };

                    res.Add(ranked);
                }
            }

            return res.OrderBy(r => r.Rank).Take(10).ToList();
        }

        public List<ReLeaderboard> GetLeaderboardByCountry(string country)
        {
            List<ReLeaderboard> res = new List<ReLeaderboard>();

            using (var GJGDbContext = new GJGDbContext())
            {
                var data = GJGDbContext.Leaderboards.OrderByDescending(p => p.Points).ToList();

                for (int i = 0; i < data.Count; i++)
                {
                    data[i].Rank = i + 1;
                }

                foreach (var leaderboard in data)
                {
                    var ranked = new ReLeaderboard
                    {
                        Country = GetUser(leaderboard.User_Id).Country,
                        Display_Name = GetUser(leaderboard.User_Id).Display_Name,
                        Point = leaderboard.Points,
                        Rank = leaderboard.Rank
                    };

                    res.Add(ranked);
                }
            }

            return res.Where(c => c.Country == country).OrderBy(r => r.Rank).Take(10).ToList();
        }

        public ReSubmit SetScore(ReSubmit submit)
        {
            ReSubmit res = new ReSubmit();

            using (var GJGDbContext = new GJGDbContext())
            {
                var lb = GetLeaderboard(submit.User_Id);

                var dbLaderboard = new Leaderboard
                {
                    Points = lb.Points + (int)Math.Round(submit.Score_Worth),
                    Rank = lb.Rank,
                    User_Id = lb.User_Id,
                    Timestamp = DateTime.Now.ToFileTime()
                };

                GJGDbContext.Leaderboards.Update(dbLaderboard);

                GJGDbContext.SaveChanges();

                ReRank();

                var s = new ReSubmit
                {
                    User_Id = submit.User_Id,
                    Score_Worth = submit.Score_Worth,
                    Timestamp = dbLaderboard.Timestamp
                };
                res = s;
            }

            return res;
        }

        public ReUser GetUserByGuid(Guid userId)
        {
            ReUser res = new ReUser
            {
                Display_Name = GetUser(userId).Display_Name,
                Points = GetLeaderboard(userId).Points,
                User_Id = userId,
                Rank = GetLeaderboard(userId).Rank
            };

            return res;
        }

        public ReUser CreateUser(ReUser user)
        {
            ReUser res = new ReUser();



            using (var GJGDbContext = new GJGDbContext())
            {
                Guid newGuid = Guid.NewGuid();
                var u = new User
                {
                    Display_Name = user.Display_Name,
                    Country = RegionInfo.CurrentRegion.TwoLetterISORegionName,
                    User_Id = newGuid
                };

                var lb = new Leaderboard
                {
                    User_Id = newGuid,
                    Points = 0,
                    Rank = GetLeaderboards().Count + 1,
                    Timestamp = DateTime.Now.ToFileTime()
                };

                GJGDbContext.Leaderboards.Add(lb);
                GJGDbContext.Users.Add(u);
                GJGDbContext.SaveChanges();

                res.Display_Name = user.Display_Name;
                res.Points = 0;
                res.User_Id = newGuid;
                res.Rank = lb.Rank;
            }

            return res;
        }

        private User GetUser(Guid userId)
        {
            using (var GJGDbContext = new GJGDbContext())
            {
                return GJGDbContext.Users.FirstOrDefault(u => u.User_Id == userId);
            }
        }

        private Leaderboard GetLeaderboard(Guid userId)
        {
            using (var GJGDbContext = new GJGDbContext())
            {
                return GJGDbContext.Leaderboards.FirstOrDefault(u => u.User_Id == userId);
            }
        }

        private void ReRank()
        {
            using (var GJGDbContext = new GJGDbContext())
            {
                var list = GJGDbContext.Leaderboards.OrderByDescending(p=>p.Points).ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Rank = i + 1;
                }

                foreach (var leaderboard in list)
                {
                    GJGDbContext.Leaderboards.Update(leaderboard);
                }

                GJGDbContext.SaveChanges();
            }
        }
    }
}
