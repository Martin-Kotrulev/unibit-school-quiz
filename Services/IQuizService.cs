using System.Collections.Generic;
using App.Models;

namespace App.Services
{
    public interface IQuizService
    {
         void CreateGroup(QuizGroup quizGroup, ApplicationUser owner);

         void CreateQuiz(Quiz quiz, ApplicationUser owner, QuizGroup quizGroup = null);

         void CreateQuestion(Question question, ApplicationUser owner);

         void CreateAnswer(Answer answer, Question question);

         void CreateProgress(QuizProgress progress);

         IEnumerable<Quiz> GetQuizes(int page = 1, int pageSize = 10);

         IEnumerable<Quiz> GetGroupQuizes(QuizGroup quizGroup, int page = 1, int pageSize = 10);

         IEnumerable<Quiz> GetUsersQuizes(ApplicationUser user);

         IEnumerable<Answer> GetQuestionAnswers(Question question, int page = 1, int pageSize = 1);

         
    }
}