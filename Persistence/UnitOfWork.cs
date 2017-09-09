using System.Threading.Tasks;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AppDbContext _context;

    public IAnswerRepository Answers { get; private set; }

    public IGroupSubscriptionRepository GroupSubscriptions { get; private set; }

    public INotificationRepository Notifications { get; private set; }

    public IQuizRepository Quizzes { get; set; }

    public IQuestionRepository Questions { get; private set; }

    public IQuizGroupRepository QuizGroups { get; private set; }

    public IQuizProgressRepository QuizProgresses { get; private set; }

    public IQuizSubscriptionRepository QuizSubscriptions { get; set; }

    public IRatingRepository Ratings { get; private set; }

    public IScoreRepository Scores { get; private set; }

    public ITagRepository Tags { get; private set; }

    public IUserRepository Users { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
      this._context = context;

      this.Answers = new AnswerRepository(_context);
      this.GroupSubscriptions = new GroupSubscriptionRepository(_context);
      this.Notifications = new NotificationRepository(_context);
      this.Questions = new QuestionRepository(_context);
      this.QuizGroups = new QuizGroupRepository(_context);
      this.QuizProgresses = new QuizProgressRepository(_context);
      this.Quizzes = new QuizRepository(_context);
      this.QuizSubscriptions = new QuizSubscriptionRepository(_context);
      this.Ratings = new RatingRepository(_context);
      this.Scores = new ScoreRepository(_context);
      this.Tags = new TagRepository(_context);
      this.Users = new UserRepository(_context);
      this.QuizSubscriptions = new QuizSubscriptionRepository(_context);
    }

    public void Complete()
    {
      _context.SaveChanges();
    }

    public async Task<int> CompleteAsync()
    {
      return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
      _context.Dispose();
    }

  }
}