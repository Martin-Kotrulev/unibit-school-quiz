namespace Uniquizbit.Web.Controllers
{
  using AutoMapper;
  using Core.Models;
  using Data.Models;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Identity;
  using Services;
  using System.Threading.Tasks;
  using Web.Models;
  using Microsoft.AspNetCore.Authorization;

  public class QuestionsController : BaseApiController
  {
    private readonly IQuizService _quizService;
    private readonly IQuestionService _questionService;
    private readonly IAnswerService _answerService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public QuestionsController(IQuizService quizService,
      IQuestionService questionService,
      IAnswerService answerService,
      IMapper mapper,
      UserManager<User> userManager)
    {
      _userManager = userManager;
      _mapper = mapper;
      _quizService = quizService;
      _questionService = questionService;
      _answerService = answerService;
    }

    [Authorize]
    [HttpPost("{questionId}/answers")]
    public async Task<IActionResult> AddAnswerForQuestion(int questionId,
      [FromBody] AnswerResource answerResource)
    {
      var answer = _mapper.Map<AnswerResource, Answer>(answerResource);
      var userId = _userManager.GetUserId(User);

      if (ModelState.IsValid)
      {
        if (await _quizService.UserCanAddQuestionToQuizAsync(questionId, userId))
        {
          answer.QuestionId = questionId;
          await _answerService.AddAnswerAsync(answer);
          return Ok(new ApiResponse("You successfully added a new answer."));
        }
        else
          return BadRequest(new ApiResponse(
            "You can't add answer to the specified question.", false));
      }
      else
        return BadRequest(new ApiResponse(ModelState));
    }

    [HttpDelete("{questionId}")]
    public async Task<IActionResult> DeleteAsync(int questionId)
    {
      var userId = _userManager.GetUserId(User);

      if (await _questionService.DeleteQuestionAsync(questionId, userId))
      {
        return Ok(new ApiResponse("You successfully deleted the question."));
      }

      return BadRequest(new ApiResponse("You can't delete the specified question.", false));
    }
  }
}