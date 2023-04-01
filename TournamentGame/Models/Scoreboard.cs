using System;
using System.Collections.Generic;
using System.Linq;
using Tournament.Extensions;

namespace Tournament.Models
{
    public class Scoreboard : IScoreboard
    {
        private readonly string[] players;
        private readonly Payoffs[,] playerPayoffs;

        /// <summary>
        /// Zwraca tablicę graczy
        /// </summary>
        public IEnumerable<string> GetPlayers => players;
        public IPayoffs[,] GetPlayerPayoffs => playerPayoffs;

        public Scoreboard(string[] players)
        {
            if (players == null || players.Length == 0)
                throw new ArgumentException("Players array is null or empty");

            this.players = players.Distinct().ToArray();
            playerPayoffs = new Payoffs[this.players.Length, this.players.Length];
        }

        public void AddPlayerPayoffs(IPlayerPayoff firstPlayerPayoff, IPlayerPayoff secondPlayerPayoff)
        {
            //Get player index:
            var firstPlayerIndex = GetPlayerIndex(firstPlayerPayoff.Player.Name);
            var secondPlayerIndex = GetPlayerIndex(secondPlayerPayoff.Player.Name);

            if (firstPlayerIndex < 0)
                throw new NullReferenceException("firstPlayerIndex not found.");

            if (secondPlayerIndex < 0)
                throw new NullReferenceException("secondPlayerIndex not found.");

            //Create new payoff:
            var payOff = new Payoffs(firstPlayerPayoff.Payoff, secondPlayerPayoff.Payoff);
            playerPayoffs[firstPlayerIndex, secondPlayerIndex] = payOff;
        }

        public IPayoffs GetPayoffs(string playerAName, string playerBName)
        {
            var playerAIndex = GetPlayerIndex(playerAName);
            var playerBIndex = GetPlayerIndex(playerBName);

            if (playerAIndex < 0 || playerBIndex < 0)
                throw new NullReferenceException("Payoff not  found for players with this names");

            var payoff = GetPayoffByPlayersIndex(playerAIndex, playerBIndex);

            if (payoff == null)
                payoff = GetPayoffByPlayersIndex(playerBIndex, playerAIndex)?.Invert();

            if (payoff == null)
                throw new NullReferenceException($"Payoffs not found for players: {playerAName} and {playerBName}.");

            return payoff;
        }

        public int GetPlayerIndex(string playerName)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].Equals(playerName))
                    return i;
            }

            return -1;
        }

        private Payoffs GetPayoffByPlayersIndex(int firstPlayerIndex, int secondPlayerIndex)
        {
            if (firstPlayerIndex < 0 || secondPlayerIndex < 0)
                throw new ArgumentException("Player index is less than zero.");

            try
            {
                return playerPayoffs[firstPlayerIndex, secondPlayerIndex];
            }
            catch
            {
                return null;
            }
        }
    }
}
