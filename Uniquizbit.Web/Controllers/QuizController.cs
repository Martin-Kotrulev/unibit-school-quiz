namespace Uniquizbit.Web.Controllers
{
  using AutoMapper;
  using Common.Enums;
  using Core.Models;
  using Data.Models;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Identity;
  using Services;
  using System.Threading.Tasks;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Web.Models;

  [Route("api/[controller]")]
  public class QuizzesController : Controller
  {
    private readonly IQuizService _quizService;
    private readonly IQuestionService _questionService;
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public QuizzesController(IQuizService quizService,
      IQuestionService questionService,
      ITagService tagService,
      IMapper mapper,
      UserManager<User> userManager)
    {
      _userManager = userManager;
      _mapper = mapper;
      _quizService = quizService;
      _questionService = questionService;
      _tagService = tagService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddQuiz([FromBody] QuizResource quizResource)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.GetUserAsync(User);
        var quiz = _mapper.Map<QuizResource, Quiz>(quizResource);

        if (await _quizService.QuizExistsAsync(quiz))
        {
          return BadRequest(new ApiResponse(
            $"Quiz group with title '{quiz.Name}' already exists.", false
          ));
        }

        var existingTags = await _tagService.UpdateTagsAsync(quizResource.Tags);

        // Add the tags
        foreach (var tag in existingTags)
        {
          quiz.Tags.Add(new QuizzesTags()
          {
            Quiz = quiz,
            TagId = tag.Id
          });
        }

        // Add creation stamps
        quiz.CreatedOn = DateTime.Now;
        quiz.CreatorId = user.Id;
        quiz.CreatorName = user.UserName;

        await _quizService.AddQuizAsync(quiz);

        return Ok(new ApiResponse(
          _mapper.Map<Quiz, QuizResource>(quiz),
          "You have successfully added a new quiz."));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [Authorize]
    [HttpPost("{quizId}/questions")]
    public async Task<IActionResult> AddQuestionToQuiz(int quizId, [FromBody] QuestionResource questionResource)
    {
      if (ModelState.IsValid)
      {
        var userId = _userManager.GetUserId(this.User);
        var question = _mapper.Map<QuestionResource, Question>(questionResource);

        if (await _quizService.UserCanAddQuestionToQuizAsync(quizId, userId))
        {
          question.QuizId = quizId;
          question.CreatorId = userId;
          
          await _questionService.AddQuestionAsync(question);
        }
        else
        {
          return BadRequest(new ApiResponse(
            "You can't add questions to the specified quiz.",
            false
          ));
        }

        return Ok(new ApiResponse(
          _mapper.Map<Question, QuestionResource>(question),
          "You have successfully created a new question.")
        );
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [Authorize]
    [HttpDelete("{quizId}")]
    public async Task<IActionResult> DeleteQuiz(int quizId)
    {
      var userId = _userManager.GetUserId(User);
      if (await _quizService.DeleteQuizAsync(quizId, userId))
      {
        return Ok(new ApiResponse("You have successfully deleted the quiz."));
      }

      return BadRequest(new ApiResponse("Quiz does not exist, or you are not the owner.", false));
    }

    [Authorize]
    [HttpPost("{id}/enter")]
    public async Task<IActionResult> EnterAsync(int id)
    {
      var userId = _userManager.GetUserId(User);
      var state = await _quizService.EnterQuizAsync(id, userId);

      ApiResponse res = new ApiResponse() { Success = false };
      switch (state)
      {
        case QuizEnum.Ended:
          res.Message = "Quiz is already over.";
          break;
        case QuizEnum.NotStarted:
          res.Message = "Quiz has not started yet.";
          break;
        case QuizEnum.NotExistent:
          res.Message = "Quiz does not exist.";
          break;
        case QuizEnum.StillTaking:
          res.Message = "You are already taking this quiz.";
          res.Success = true;
          return Ok(res);
        case QuizEnum.Enter:
          res.Message = "You successfully entered the quiz.";
          res.Success = true;
          return Ok(res);
      }

      return BadRequest(res);
    }

    [HttpPost("progress")]
    public async Task<IActionResult> AddProgressAsync([FromBody] ProgressAnswerResource progressAnswerResource)
    {
      if (ModelState.IsValid)
      {
        var userId = _userManager.GetUserId(User);
        var progressAnswer = _mapper.Map<ProgressAnswerResource, ProgressAnswer>(progressAnswerResource);
        var progress = await _quizService.AddProgressToQuizAsync(userId, progressAnswer);

        if (progress == null)
        {
          ModelState.AddModelError("Progress error", "Progress does not exist");

          return NotFound(new ApiResponse(ModelState));
        }

        return Ok(new ApiResponse(progress));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [HttpGet]
    public async Task<IActionResult> AllQuizes([FromQuery] string search, [FromQuery] int page = 1)
    {
      search = search ?? "";
      ICollection<QuizResource> quizzes;
      if (search.Contains("*"))
      {
        var searchTags = Regex
          .Matches(search, @"\*(\w+)")
          .Select(m => m.Groups[1].Value)
          .ToList();

        if (searchTags.Count() != 0)
        {
          quizzes = _mapper
            .Map<IEnumerable<Quiz>, ICollection<QuizResource>>(
              await _quizService.SearchQuizzesByTagsAsync(searchTags, page)
            );

          return Ok(quizzes);
        }
      }

      quizzes = _mapper
        .Map<IEnumerable<Quiz>, ICollection<QuizResource>>(
          await _quizService.GetQuizzesAsync(page, search: search));

      return Ok(quizzes);
    }

    [Authorize]
    [HttpGet("{quizId}/questions")]
    public async Task<IActionResult> AllQuestionsForQuizAsync(int quizId)
    {
      var userId = _userManager.GetUserId(User);
      var quiz = await _quizService.FindQuizByIdAsync(quizId);

      if (quiz != null)
      {
        var questions = _mapper.Map<IEnumerable<Question>, ICollection<QuestionResource>>(
          await _questionService.GetQuestionsForQuizAsync(quizId, userId));

        return Ok(new
        {
          Quiz = _mapper.Map<Quiz, IdNamePairResource>(quiz),
          Questions = questions
        });
      }
      else
      {
        ModelState.AddModelError("Quizz error", "Quiz does not exist.");
        return NotFound(new ApiResponse(ModelState));
      }
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> Mine([FromQuery] int page = 1)
    {
      var userId = _userManager.GetUserId(User);

      var userQuizzes = _mapper
        .Map<IEnumerable<Quiz>, ICollection<QuizResource>>(
          await _quizService.GetUserOwnQuizzesAsync(userId, page));

      return Ok(userQuizzes);
    }
  }
}