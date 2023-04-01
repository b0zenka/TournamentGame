using System;
using TournamentPlayer;

namespace Tournament.Models
{
    public class PayoffMatrix : IPayoffMatrix
    {
        public byte[,] Payoffs { get; }

        public PayoffMatrix(byte[,] payouts)
        {
            Payoffs = payouts;
        }

        public byte GetPayoffForPlayer(byte playerIndex, Strategy firstPlayerStrategy, Strategy secondPlayerStrategy)
        {
            if (playerIndex == 0) return GetPayoffForPlayer(firstPlayerStrategy, secondPlayerStrategy);
            else if (playerIndex == 1) return GetPayoffForPlayer(secondPlayerStrategy, firstPlayerStrategy);

            throw new IndexOutOfRangeException("PlayerIndex out of range of Payoffs array.");
        }

        private byte GetPayoffForPlayer(Strategy firstPlayerStrategy, Strategy secondPlayerStrategy)
        {
            return Payoffs[(byte)firstPlayerStrategy, (byte)secondPlayerStrategy];
        }

    }
}
