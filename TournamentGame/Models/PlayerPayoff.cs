using TournamentPlayer;

namespace Tournament.Models
{
    public class PlayerPayoff : IPlayerPayoff
    {
        public IPlayer Player { get; }
        public int Payoff { get; private set; }

        public PlayerPayoff(IPlayer player, int payoff)
        {
            Player = player;
            Payoff = payoff;
        }

        public void AddPayoff(int value)
        {
            Payoff += value;
        }
    }
}
