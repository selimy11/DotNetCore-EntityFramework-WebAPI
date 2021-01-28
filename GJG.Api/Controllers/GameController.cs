using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GJG.Business.Abstract;
using GJG.Business.Concrete;
using GJG.Entities;

namespace GJG.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private IGameService _gameService;

        public GameController()
        {
            _gameService = new GameManager();
        }

        [HttpGet("leaderboard")]
        public List<ReLeaderboard> leaderboard()
        {
            return _gameService.GetLeaderboards();
        }

        [HttpGet("leaderboard/{country}")]
        public List<ReLeaderboard> leaderboard(string country)
        {
            return _gameService.GetLeaderboardByCountry(country);
        }

        [HttpPost("score/submit")]
        public ReSubmit score([FromBody] ReSubmit submit)
        {
            return _gameService.SetScore(submit);
        }

        [HttpGet("user/profile/{userid}")]
        public ReUser user(string userid)
        {
            Guid userGuid = Guid.Parse(userid);

            return _gameService.GetUserByGuid(userGuid);
        }

        [HttpPost("user/create")]
        public ReUser user(ReUser user)
        {
            return _gameService.CreateUser(user);
        }
    }
}
