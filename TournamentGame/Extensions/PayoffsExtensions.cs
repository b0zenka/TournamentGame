using Tournament.Models;

namespace Tournament.Extensions
{
    internal static class PayoffsExtensions
    {
        public static Payoffs Invert(this Payoffs payoffs)
        {
            return new Payoffs(payoffs.SecondPlayerPayoff, payoffs.FirstPlayerPayoff);
        }
    }
}
