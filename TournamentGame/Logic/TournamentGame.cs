using System;
using System.Collections.Generic;
using Tournament.Models;
using TournamentPlayer;

namespace Tournament.Logic
{
    internal sealed class TournamentGame : Game
    {
        private readonly Dictionary<IPlayer, Strategy?> LastPlayerStrategies = new Dictionary<IPlayer, Strategy?>();

        private IPlayer FirstPlayer => Players[0];
        private IPlayer SecondPlayer => Players[1];

        #region Constructors
        public TournamentGame(string firstPlayerPath, string secondPlayerPath, IPayoffMatrix payoff, int rounds)
        {
            if (payoff == null || payoff.Payoffs.Length == 0 || payoff.Payoffs.LongLength == 0)
                throw new ArgumentException("Payoff is null or empty");

            if (rounds <= 0)
                throw new ArgumentException("Rounds count are less than 0.");

            IPlayer firstPlayer = null ?? LoadAssembly.LoadInstance<IPlayer>(firstPlayerPath);
            IPlayer secondPlayer = null ?? LoadAssembly.LoadInstance<IPlayer>(secondPlayerPath);

            if (firstPlayer is null || secondPlayer is null)
                throw new ArgumentException("Invalid path to IPlayer interface");

            Players = new IPlayer[2] { firstPlayer, secondPlayer };
            PayoffMatrix = payoff;
            Rounds = rounds;

            InitLastPlayerStrategies();
        }
        #endregion

        protected override IEnumerable<KeyValuePair<IPlayer, int>> PlayRound()
        {
            //Wykonaj ruch graczy:
            var firstPlayerStrategy = FirstPlayer.HandleState(LastPlayerStrategies[SecondPlayer]);
            var secondPlayerStrategy = SecondPlayer.HandleState(LastPlayerStrategies[FirstPlayer]);

            //Zaktualizuj ostatnie strategie:
            LastPlayerStrategies[FirstPlayer] = firstPlayerStrategy;
            LastPlayerStrategies[SecondPlayer] = secondPlayerStrategy;

            //Pobierz wyniki dla graczy:
            int firstPlayerPayoff = PayoffMatrix.GetPayoffForPlayer(0, firstPlayerStrategy, secondPlayerStrategy);
            int secondPlayerPayoff = PayoffMatrix.GetPayoffForPlayer(1, firstPlayerStrategy, secondPlayerStrategy);

            //Zwróć wyniki dla graczy:
            return new KeyValuePair<IPlayer, int>[2]
            {
                new KeyValuePair<IPlayer, int>(FirstPlayer, firstPlayerPayoff),
                new KeyValuePair<IPlayer, int>(SecondPlayer, secondPlayerPayoff)
            };
        }

        private void InitLastPlayerStrategies()
        {
            foreach (var player in Players)
            {
                LastPlayerStrategies.Add(player, null);
            }
        }
    }


}
