using System.IO;
using System.Linq;
using Tournament.Models;
using TournamentPlayer;
namespace Tournament.Logic
{
    public class Torunament
    {
        private readonly string[] playersPaths;
        private readonly IPayoffMatrix payoff;
        private readonly int rounds;

        private readonly Scoreboard scoreboard;

        public IScoreboard GetScoreboard => scoreboard;

        public Torunament(string[] playersPaths, IPayoffMatrix payoff, int rounds)
        {
            this.playersPaths = playersPaths;
            this.payoff = payoff;
            this.rounds = rounds;

            var playerNames = LoadAssembly.GetPlayerNames<IPlayer>(playersPaths);
            scoreboard = new Scoreboard(playerNames);
        }

        public void Play()
        {
            for (int i = 0; i < playersPaths.Length; i++)
            {
                for (int j = i; j < playersPaths.Length; j++)
                {
                    var game = new TournamentGame(playersPaths[i], playersPaths[j], payoff, rounds);
                    var payoffs = game.Play();

                    scoreboard.AddPlayerPayoffs(payoffs[0], payoffs[1]);
                }
            }
        }

        public static string[] LoadPlayerPaths(string directory)
        {
            if (!Directory.Exists(directory)) return new string[0];

            var files = Directory.GetFiles(directory, "*.dll").ToList();
            LoadAssembly.ValidatePaths<IPlayer>(files);

            return files.ToArray();
        }
    }
}
