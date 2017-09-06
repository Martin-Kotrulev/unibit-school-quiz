using System;
using System.Threading.Tasks;

namespace App.Persistence.Repositories.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    // Declare repository interfaces here
    // IRepository SomeRepository { get; }
    IAnswerRepository Answers { get; }

    IGroupSubscriptionRepository GroupSubscriptions { get; }

    INotificationRepository Notifications { get; }

    IQuestionRepository Questions { get; }

    IQuizGroupRepository QuizGroups { get; }

    IQuizRepository Quizzes { get; }

    IQuizProgressRepository QuizProgresses { get; }

    IQuizSubscriptionRepository QuizSubscriptions { get; }

    IRatingRepository Ratings { get; }

    IScoreRepository Scores { get; }

    ITagRepository Tags { get; }

    IUserRepository Users { get; }

    void Complete();

    Task<int> CompleteAsync();
  }
}