namespace Tournament.Models
{
    public interface IPayoffs
    {
        int FirstPlayerPayoff { get; }
        int SecondPlayerPayoff { get; }
    }
}