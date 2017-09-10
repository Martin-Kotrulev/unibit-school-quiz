using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        var user = await _authenticationService.GetAuthenticatedUser(User);
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
        quiz.CreatorId = user.Id;

        _quizService.CreateQuiz(quiz);

        return Ok(new ApiResponse("You successfully added a new quiz."));
      }
      else
      {
        return BadRequest(new ApiResponse(ModelState));
      }
    }

    [HttpPost("question/add")]
    [Authorize]
    public async Task<IActionResult> AddQuestionToQuizAsync([FromBody] QuestionResource questionResource)
    {
      if (ModelState.IsValid)
      {
        var user = await _authenticationService.GetAuthenticatedUser(User);
        var question = _mapper.Map<QuestionResource, Question>(questionResource);

        _quizService.CreateQuestion(question);

        return Ok(new ApiResponse("You successfully created a new question."));
      }
      else
      {
        return BadRequest(new ApiResponse(ModelState));
      }
    }

    [HttpPost("{id}/delete")]
    [Authorize]
    public IActionResult Delete(int id)
    {
      return Ok();
    }

    [HttpPost("{id}/enter")]
    [Authorize]
    public IActionResult Enter(int id)
    {
      return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> All([FromQuery] string search, [FromQuery] int page = 1)
    {
      search = search ?? "";
      ICollection<QuizResource> quizzes;
      System.Console.WriteLine(search);
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
    public IActionResult AllQuestionsForQuiz(int id, [FromQuery] int page)
    {
      return Ok();
    }
  }
}