﻿using CardBattle.Models;
using CardBattle.Player;
using CardBattle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardBattle.Infrastructure
{
    public class TournamentOrganiser
    {
        private readonly List<IPlayer> _players;
        private readonly CardDealer _dealer;
        private readonly ILogger _logger;

        private List<int> _scores;

        public IEnumerable<int> Scores
        {
            get
            {
                return _scores.AsReadOnly();
            }
        }

        public int HandSize { get; set; }

        public int GamesNumber { get; set; }

        private int PlayersCount
        {
            get
            {
                return _players.Count;
            }
        }

        public TournamentOrganiser(IEnumerable<IPlayer> players, CardDealer dealer, ILogger logger)
        {
            _players = players.ToList();
            _dealer = dealer;
            _logger = logger;

            _scores = new List<int>(players.Select(p => 0));

            HandSize = 5;
            GamesNumber = 1000;
        }

        public void PlayTournament()
        {
            for (var i = 0; i < PlayersCount; i++)
            {
                _players[i].Initialize(PlayersCount, i);
            }

            for (var i = 0; i < GamesNumber; i++)
            {
                PlayGame();
            }

            var winnerIndex = _scores.IndexOf(_scores.Max());
            var winner = _players[winnerIndex];

            var log = this.HandSize+" cards => " + string.Join(", ", _players.Select((player, index) => new { player, score = _scores[index] }).OrderByDescending(v => v.score).Select(v => v.player.Name + ": " + v.score).ToArray());
            
            _logger.Log(LogLevel.Warning, log);
        }

        public void PlayGame()
        {
            var game = new Game(_dealer, _players, HandSize, _logger);

            game.PlayGame();

            var scores = game.Scores;

            var maxScore = scores.Max();
            for(var i = 0; i<PlayersCount; i++)
            {
                if(scores[i] == maxScore)
                {
                    _scores[i]++;
                }
            }
        }
    }
}
