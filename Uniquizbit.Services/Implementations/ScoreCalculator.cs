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
        finalScore = 6.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Good)
      {
        finalScore = 5.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Average)
      {
        finalScore = 4.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }
      else if (scoreInPercentage > _gradesSettings.Weak)
      {
        finalScore = 3.0 - Difference(_gradesSettings.VeryGood, scoreInPercentage);
      }

      return finalScore;
    }

    private double Difference(double upperBound, double actualScore)
    {
      // Makes sure the difference is always 1.0 point max
      var diff = actualScore - upperBound;
      var diffByTen = diff * 10;

      return Math.Round((((double)diff / (double)diffByTen) * 10), 2);
    }
  }
}