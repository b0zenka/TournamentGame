namespace TournamentPlayer
{
    public interface IState
    {
        Strategy PlayerStrategy { get; }

        IState HandleState(Strategy? lastOpponentStrategy);
    }
}
