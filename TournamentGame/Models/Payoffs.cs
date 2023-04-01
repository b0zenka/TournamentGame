using System;

namespace Tournament.Models
{
    internal class Payoffs : Tuple<int, int>, IPayoffs
    {
        public Payoffs(int firstPlayerPayoff, int secondPlayerPayoff) : base(firstPlayerPayoff, secondPlayerPayoff)
        {
        }

        public int FirstPlayerPayoff => base.Item1;
        public int SecondPlayerPayoff => base.Item2;
    }
}
