using System.Collections.Generic;

namespace Tournament.Models
{
    public interface IScoreboard
    {
        IPayoffs[,] GetPlayerPayoffs { get; }
        IEnumerable<string> GetPlayers { get; }

        IPayoffs GetPayoffs(string playerAName, string playerBName);
        int GetPlayerIndex(string playerName);
    }
}