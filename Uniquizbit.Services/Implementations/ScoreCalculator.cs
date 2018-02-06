namespace Uniquizbit.Services.Implementations
{
  using Common.Config;
  using Microsoft.Extensions.Options;
  using System;

  public class ScoreCalculator : IScoreCalculator
  {
    private readonly GradesSettings _gradesSettings;

    public ScoreCalculator(IOptions<GradesSettings> optionsAccessor)
    {
      _gradesSettings = optionsAccessor.Value;
    }

    public double GetScore(double maxScore, double userScore)
    {
      double finalScore = 2.0;
      double scoreInPercentage = Math.Round((((double)userScore / (double)maxScore)) * 100, 2);

      if (scoreInPercentage > _gradesSettings.VeryGood)
      {
        finalScore = 5.0 + Normalize(
          _gradesSettings.Excellent,
          _gradesSettings.VeryGood,
          scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Good)
      {
        finalScore = 4.0 + Normalize(
          _gradesSettings.VeryGood,
          _gradesSettings.Good,
          scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Average)
      {
        finalScore = 3.0 + Normalize(
          _gradesSettings.Good,
          _gradesSettings.Average,
          scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Weak)
      {
        finalScore = 2.0 + Normalize(
          _gradesSettings.Average,
          _gradesSettings.Weak,
          scoreInPercentage);
      }

      return finalScore;
    }

    private double Normalize(double upperBound, double lowerBound, double actualScore)
    {
      double result = Math.Round((actualScore - lowerBound) /
        (upperBound - lowerBound), 2);

      return result;
    }
  }
}