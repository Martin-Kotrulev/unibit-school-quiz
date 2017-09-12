using System.Threading.Tasks;
using App.Controllers.Resources;
using App.Models;
using App.Services;
using App.Services.Security;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
  [Route("api/[controller]")]
  public class QuestionsController : Controller
  {
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public QuestionsController(IQuizService quizService, IMapper mapper,
      IAuthenticationService authenticationService)
    {
      this._authenticationService = authenticationService;
      this._mapper = mapper;
      this._quizService = quizService;
    }

    [HttpPost("{id}/delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
      var userId = _authenticationService.GetAuthenticatedUserId(User);
			var ownQuestion = await _quizService.UserOwnQuestionAsync(id, userId);

			if (ownQuestion)
			{
				var deleted = _quizService.DeleteQuestion(id);

				if (!deleted)
					return BadRequest(new ApiResponse("Question does not exist.", false));
				
				return Ok(new ApiResponse("You successfully deleted the question."));
			}

			return BadRequest(new ApiResponse("You can't delete other users questions.", false));
    }

    [HttpPost("{id}/answers/add")]
    public async Task<IActionResult> AddAnswerForQuestionAsync(int id, [FromBody] AnswerResource answerResource)
    {
      var answer = _mapper.Map<AnswerResource, Answer>(answerResource);
      var userId = _authenticationService.GetAuthenticatedUserId(User);

      if (ModelState.IsValid)
      {
        if (_quizService.UserCanAddQuestion(id, userId))
        {
					answer.QuestionId = id;
          await _quizService.CreateAnswerAsync(answer);
          return Ok(new ApiResponse("You successfully added a new answer."));
        }
        else
          return BadRequest(new ApiResponse(
						"You can't add answers to other users questions.", false));
      }
			else
				return BadRequest(new ApiResponse(ModelState));

    }

    [HttpPost("{questionId}/answers/{answerId}/delete")]
    public async Task<IActionResult> DeleteAnswerFromQuestionAsync(int questionId, int answerId)
    {
      var userId = _authenticationService.GetAuthenticatedUserId(User);
			var ownQuestion = await _quizService.UserOwnQuestionAsync(questionId, userId);

      if (ownQuestion)
      {
        var deleted = await _quizService.DeleteAnswerAsync(answerId);
        if (deleted)
          return Ok(new ApiResponse("You successfully deleted an answer."));
        else
          return BadRequest(new ApiResponse("You successfully deleted an answer.", deleted));
      }
      else
        return BadRequest(new ApiResponse(
					"You can't delete answers from other users questions.", false));
    }
  }
}