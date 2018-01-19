namespace Uniquizbit.Services
{
    public interface IScoreCalculator
    {
         double GetScore(double maxScore, double userScore);
    }
}