using TournamentPlayer;

namespace Tournament.Models
{
    /// <summary>
    /// Interface macierzy wypłat dla graczy
    /// </summary>
    public interface IPayoffMatrix
    {
        /// <summary>
        /// Macierz wypłat dla gracza pierwszego
        /// </summary>
        byte[,] Payoffs { get; }


        /// <summary>
        /// Zwraca wypłatę dla danego gracza na podstawie wybranych strategii obu graczy
        /// </summary>
        /// <param name="playerIndex">Index danego gracza</param>
        /// <param name="firstPlayerStrategy">Index wybranej strategii pierwszego gracza</param>
        /// <param name="secondPlayerStrategy">Index wybranej strategii drugiego gracza</param>
        /// <returns>Wypłata dla danego graczas</returns>
        byte GetPayoffForPlayer(byte playerIndex, Strategy firstPlayerStrategy, Strategy secondPlayerStrategy);

    }
}
