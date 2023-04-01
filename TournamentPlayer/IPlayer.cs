namespace TournamentPlayer
{
    public interface IPlayer
    {
        /// <summary>
        /// Nazwa gracza
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Wykonuje ruch na podstawie ostatniej wybranej strategii przeciwnika
        /// </summary>
        /// <param name="lastEnemyStrategy">Poprzednia strategia przeciwnika. Jeżeli runda jest pierwsza, to wartość jest równa null</param>
        /// <returns>Wybrana strategia gracza</returns>
        Strategy HandleState(Strategy? lastEnemyStrategy);
    }
}