using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.Controllers;
using App.Controllers;
using App.Controllers.Resources;
using App.Models;
using App.Services;
using App.Services.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
  [Route("api/[controller]")]
  public class QuizzesController : Controller
  {
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public QuizzesController(IQuizService quizService, IMapper mapper, 
      IAuthenticationService authenticationService)
    {
      this._authenticationService = authenticationService;
      this._mapper = mapper;
      this._quizService = quizService;
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> Add([FromBody] QuizResource quizResource)
    {
      if (ModelState.IsValid)
      {
        var userId = _authenticationService.GetAuthenticatedUserId(User);
        var quiz = _mapper.Map<QuizResource, Quiz>(quizResource);

        if (await _quizService.QuizExistsAsync(quiz))
        {
          return BadRequest(new ApiResponse(
            $"Quiz group with title '{quiz.Title}' already exists.", false
          ));
        }

        var existingTags = _quizService.CheckForExistingTags(quizResource.Tags);
        var tagNames = existingTags.Select(t => t.Name);

        // Add the new tags
        foreach (var tag in quizResource.Tags)
        {
          if (!tagNames.Contains(tag))
            quiz.Tags.Add(new QuizzesTags() 
            {
              Quiz = quiz, 
              Tag = new Tag() { Name = tag }
            }); 
        }

        // Add the existing tags
        foreach (var tag in existingTags)
        {
          quiz.Tags.Add(new QuizzesTags() 
          {
            Quiz = quiz, 
            Tag = tag
          }); 
        }
        
        quiz.CreatedOn = DateTime.Now;
        quiz.CreatorId = userId;

        _quizService.CreateQuiz(quiz);

        return Ok(new ApiResponse("You have successfully added a new quiz."));
      }
      else
      {
        return BadRequest(new ApiResponse(ModelState));
      }
    }

    [HttpPost("{id}/questions/add")]
    [Authorize]
    public IActionResult AddQuestionToQuiz(int id, [FromBody] QuestionResource questionResource)
    {
      if (ModelState.IsValid)
      {
        var userId = _authenticationService.GetAuthenticatedUserId(User);
        var question = _mapper.Map<QuestionResource, Question>(questionResource);

        if (_quizService.UserCanAddQuestion(id, userId))
        {
          question.QuizId = id;
          _quizService.CreateQuestion(question);
        }
        else
        {
          return BadRequest(new ApiResponse(
            "You can't add questions to other users quizzes.", 
            false
          ));
        }
        
        return Ok(new ApiResponse("You have successfully created a new question."));
      }
      else
      {
        return BadRequest(new ApiResponse(ModelState));
      }
    }

    [HttpPost("{id}/[action]")]
    [Authorize]
    public IActionResult Delete(int id)
    {
      var userId = _authenticationService.GetAuthenticatedUserId(User);
      if (_quizService.DeleteQuiz(id, userId))
      {
        return Ok(new ApiResponse("You have successfully deleted the quiz."));
      }
      else
      {
        return BadRequest(new ApiResponse("Quiz does not exist, or you are not the owner.", false));
      }
    }

    [HttpPost("{id}/enter")]
    [Authorize]
    public async Task<IActionResult> EnterAsync(int id)
    {
      var userId = _authenticationService.GetAuthenticatedUserId(User);
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

    [HttpGet("[action]")]
    public async Task<IActionResult> All([FromQuery] string search, [FromQuery] int page = 1)
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

          return Ok(new ApiResponse(quizzes));
        }
      }

      quizzes = _mapper
        .Map<IEnumerable<Quiz>, ICollection<QuizResource>>(
          await _quizService.GetQuizzesAsync(page, search: search));

      return Ok(new ApiResponse(quizzes));
    }

    [HttpGet("{id}/questions/all")]
    [Authorize]
    public async Task<IActionResult> AllQuestionsForQuizAsync(int id)
    {
      var userId = _authenticationService.GetAuthenticatedUserId(User);
      var questions = _mapper.Map<IEnumerable<Question>, ICollection<QuestionResource>>(
        await _quizService.GetQuestionsAsync(id, userId)
      );

      return Ok(new ApiResponse(questions));
    }
  }
}