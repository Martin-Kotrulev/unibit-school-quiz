namespace Uniquizbit.Web.Controllers
{
  using Core.Models;
  using Data.Models;
  using Services;
  using System.Threading.Tasks;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Identity;

  public class AnswersController : BaseApiController
  {
    private readonly IAnswerService _answerService;
    private readonly UserManager<User> _userManager;

    public AnswersController(UserManager<User> userManager,
        IAnswerService answerService)
    {
      _userManager = userManager;
      _answerService = answerService;
    }

    [Authorize]
    [HttpDelete("{answerId}")]
    public async Task<IActionResult> DeleteAnswer(int answerId)
    {
      var userId = _userManager.GetUserId(User);
      if (await _answerService.DeleteAnswerAsync(answerId, userId))
      {
        return Ok(new ApiResponse("You successfully deleted an answer."));
      }
      else
        return BadRequest(new ApiResponse(
          "You can't delete the specified answer.", false));
    }
  }
}