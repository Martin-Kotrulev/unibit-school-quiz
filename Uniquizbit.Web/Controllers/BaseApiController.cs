namespace Uniquizbit.Web.Controllers
{
  using Core.Models;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding;

  [Route("api/[controller]")]
  public class BaseApiController : Controller
  {
    public IActionResult ApiOk(object result, string message = null)
    {
      return Ok(new ApiResponse(result, message));
		}

		public IActionResult ApiOk(string message, bool success = true)
		{
			return Ok(new ApiResponse(message, success));
		}

		public IActionResult ApiBadRequest(ModelStateDictionary modelState)
		{
			return BadRequest(new ApiResponse(modelState));
		}

		public IActionResult ApiBadRequest(string message)
		{
			return BadRequest(new ApiResponse(message, false));
		}

		public IActionResult ApiNotFound(ModelStateDictionary modelState)
		{
			return NotFound(new ApiResponse(modelState));
		}

		public IActionResult ApiNotFound(string message)
		{
			return NotFound(new ApiResponse(message, false));
		}
  }
}