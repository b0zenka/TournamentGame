using Tournament.Models;
using TournamentPlayer;

namespace Tournament.Logic
{
    internal interface IGame
    {
        IPlayer[] Players { get; }
        IPayoffMatrix PayoffMatrix { get; }
        int Rounds { get; }

        PlayerPayoff[] Play();
    }
}
