using App.Controllers.Resources;
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
		public IActionResult Delete(int id)
		{
			return Ok();
		}

		[HttpPost("{id}/answers/add")]
		public IActionResult AddAnswerForQuestion(int id)
		{
			return Ok();
		}

		[HttpPost("progress")]
		public IActionResult AddProgress([FromBody] ProgressResource progress)
		{
			return Ok();
		}

		[HttpGet("{id}/answers/all")]
		public IActionResult AllQuestionAnswers(int id)
		{
			return Ok();
		}
  }
}