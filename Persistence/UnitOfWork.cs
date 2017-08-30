using System.Threading.Tasks;
using App.Persistence.Repositories.Interfaces;

namespace App.Persistence.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly AppDbContext _context;

    public IAnswerRepository Answers { get; private set; }

    public IGroupSubscribtionRepository GroupSubscribtions { get; private set; }

    public INotficationRepository Notifications { get; private set; }

    public IQuizRepository Quizes { get; set; }

    public IQuestionRepository Questions { get; private set; }

    public IQuizGroupRepository QuizGroups { get; private set; }

    public IQuizProgressRepository QuizProgreseses { get; private set; }

    public IQuizSubscribtionRepository QuizSubscribtions { get; private set; }

    public IRatingRepository Ratings { get; private set; }

    public IScoreRepository Scores { get; private set; }

    public ITagRepository Tags { get; private set; }

    public IUserRepository Users { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
      this._context = context;

      this.Answers = new AnswerRepository(_context);
      this.GroupSubscribtions = new GroupSubscribtionRepository(_context);
      this.Notifications = new NotificationRepository(_context);
      this.Questions = new QuestionRepository(_context);
      this.QuizGroups = new QuizGroupRepository(_context);
      this.QuizProgreseses = new QuizProgressRepository(_context);
      this.Quizes = new QuizRepository(_context);
      this.QuizSubscribtions = new QuizSubscribtionRepository(_context);
      this.Ratings = new RatingRepository(_context);
      this.Scores = new ScoreRepository(_context);
      this.Tags = new TagRepository(_context);
      this.Users = new UserRepository(_context);
    }

    public void Complete()
    {
      _context.SaveChangesAsync();
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