using TournamentPlayer;

namespace Tournament.Models
{
    public interface IPlayerPayoff
    {
        int Payoff { get; }
        IPlayer Player { get; }
    }
}