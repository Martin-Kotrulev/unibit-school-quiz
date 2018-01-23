namespace Uniquizbit.Services
{
  public interface IScoreCalculator : IService
  {
    double GetScore(double maxScore, double userScore);
  }
}