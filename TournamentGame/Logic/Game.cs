using System.Collections.Generic;
using System.Linq;
using Tournament.Models;
using TournamentPlayer;

namespace Tournament.Logic
{
    internal abstract class Game : IGame
    {
        #region Properties
        /// <summary>
        /// Gracze biorący udział w grze
        /// </summary>
        public IPlayer[] Players { get; protected set; }

        /// <summary>
        /// Macierz wypłat dla gry
        /// </summary>
        public IPayoffMatrix PayoffMatrix { get; protected set; }

        /// <summary>
        /// Liczba rund w grze
        /// </summary>
        public int Rounds { get; protected set; }
        #endregion

        public PlayerPayoff[] Play()
        {
            var scoreboard = InitScoreboard();

            for (int i = 0; i < Rounds; i++)
            {
                var results = PlayRound();
                AddResultsToScoreboard(scoreboard, in results);
            }

            return scoreboard.ToArray();
        }

        protected abstract IEnumerable<KeyValuePair<IPlayer, int>> PlayRound();

        /// <summary>
        /// Inicjalizacja tabeli wyników dla wszystkich graczy biorących udział w grze
        /// </summary>
        private IEnumerable<PlayerPayoff> InitScoreboard()
        {
            var results = new List<PlayerPayoff>();

            foreach (var player in Players)
                results.Add(new PlayerPayoff(player, 0));

            return results;
        }

        private void AddResultsToScoreboard(IEnumerable<PlayerPayoff> scoreboard, in IEnumerable<KeyValuePair<IPlayer, int>> results)
        {
            foreach (var result in results)
            {
                var playerScoreboard = scoreboard.Single(x => x.Player.Equals(result.Key));
                playerScoreboard.AddPayoff(result.Value);
            }
        }
    }
}
