using App.Models;

namespace App.Services
{
    public interface IQuizService
    {
         void CreateGroup(QuizGroup quizGroup, ApplicationUser owner);

         void CreateQuiz(Quiz quiz, ApplicationUser owner, QuizGroup quizGroup = null);

         void CreateQuestion(Question question, ApplicationUser owner)
    }
}