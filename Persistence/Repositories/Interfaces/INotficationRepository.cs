using App.Models;

namespace App.Persistence.Repositories.Interfaces
{
    public interface INotficationRepository : IRepository<Notification>
    {
         
    }

    public interface IQuizRepositroy : IRepository<Quiz> {}
    public interface IQuizGroupRepository : IRepository<QuizGroup> {}
    public interface IQuizProgres : IRepository<QuizProgress> {}
    public interface IQuizSubscribtion : IRepository<QuizSubscribtion> {}
    public interface IRatingRepository : IRepository<Rating> {}
    public interface IScoreRepository : IRepository<Score> {}
    public interface ITagRepository : IRepository<Tag> {}
}