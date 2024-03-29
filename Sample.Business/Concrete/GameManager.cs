﻿using System;
using System.Collections.Generic;
using System.Text;
using Sample.Business.Abstract;
using Sample.DataAccess.Abstract;
using Sample.DataAccess.Concrete;
using Sample.Entities;

namespace Sample.Business.Concrete
{
    public class GameManager : IGameService
    {
        private IGameRepository _gameRepository;

        public GameManager()
        {
            _gameRepository=new GameRepository();
        }

        public List<ReLeaderboard> GetLeaderboards()
        {
            return _gameRepository.GetLeaderboards();
        }

        public List<ReLeaderboard> GetLeaderboardByCountry(string country)
        {
            return _gameRepository.GetLeaderboardByCountry(country);
        }

        public ReSubmit SetScore(ReSubmit submit)
        {
            return _gameRepository.SetScore(submit);
        }

        public ReUser GetUserByGuid(Guid userId)
        {
            return _gameRepository.GetUserByGuid(userId);
        }

        public ReUser CreateUser(ReUser user)
        {
            return _gameRepository.CreateUser(user);
        }
    }
}
