namespace TournamentPlayer
{
    public abstract class Player : IPlayer
    {
        public string Name { get; }
        private IState CurrentState { get; set; }

        public Player(string name, IState startState)
        {
            Name = name;
            CurrentState = startState;
        }

        public Strategy HandleState(Strategy? lastEnemyStrategy)
        {
            CurrentState = CurrentState.HandleState(lastEnemyStrategy);
            return CurrentState.PlayerStrategy;
        }
    }
}