namespace Uniquizbit.Web.Controllers
{
  using AutoMapper;
  using Data.Models;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Models;
  using Microsoft.AspNetCore.Identity;
  using Newtonsoft.Json;
  using Npgsql;
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Data.SqlClient;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;
  using Uniquizbit.Services;

  public class GroupsController : BaseApiController
  {
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GroupsController(IQuizService quizService,
      IMapper mapper,
      UserManager<User> userManager)
    {
      this._userManager = userManager;
      this._mapper = mapper;
      this._quizService = quizService;
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] QuizGroupResource quizGroupResource)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.GetUserAsync(this.User);
        var quizGroup = _mapper.Map<QuizGroupResource, QuizGroup>(quizGroupResource);

        if (await _quizService.GroupExistsAsync(quizGroup))
        {
          return BadRequest(new ApiResponse(
            $"Quiz group with name '{quizGroup.Name}' already exists.", false
          ));
        }

        var existingTags = _quizService.CheckForExistingTags(quizGroupResource.Tags);
        var tagNames = existingTags.Select(t => t.Name);

        // Add the new tags
        foreach (var tag in quizGroupResource.Tags)
        {
          if (!tagNames.Contains(tag))
            quizGroup.Tags.Add(new GroupsTags() 
            {
              Group = quizGroup, 
              Tag = new Tag() { Name = tag}
            }); 
            
        }

        // Add the existing tags
        foreach (var tag in existingTags)
        {
          quizGroup.Tags.Add(new GroupsTags() 
            {
              Group = quizGroup, 
              Tag = tag
            }); 
        }
        
        quizGroup.CreatedOn = DateTime.Now;
        quizGroup.CreatorId = user.Id;
        quizGroup.CreatorName = user.UserName;

        _quizService.CreateGroup(quizGroup);

        return Ok(new ApiResponse(
          _mapper.Map<QuizGroup, QuizGroupResource>(quizGroup),
          $"You successfully created a new group with name '{ quizGroup.Name }'"
        ));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [HttpPost("{id}/[action]")]
    [Authorize]
    public IActionResult Delete(int id)
    {
      var userId = _userManager.GetUserId(this.User);

      if (_quizService.DeleteQuizGroup(id, userId))
      {
        return Ok(new ApiResponse("You successfully deleteted the quiz group."));
      }
      else
      {
        return BadRequest(
          new ApiResponse("Quiz group does not exist, or you are not the owner.", false));
      }
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> All([FromQuery] string search, [FromQuery] int page = 1)
    {
      System.Console.WriteLine(search);
      search = search ?? "";
      ICollection<QuizGroupResource> quizGroups;
      if (search.Contains("*"))
      {
        var searchTags = Regex
          .Matches(search, @"\*(\w+)")
          .Select(m => m.Groups[1].Value)
          .ToList();

        if (searchTags.Count() != 0)
        {
          quizGroups = _mapper
            .Map<IEnumerable<QuizGroup>, ICollection<QuizGroupResource>>(
              await _quizService.SearchQuizGroupsByTagsAsync(searchTags, page)
            );

          return Ok(quizGroups);
        }
      }

      quizGroups = _mapper
        .Map<IEnumerable<QuizGroup>, ICollection<QuizGroupResource>>(
          await _quizService.GetGroupsAsync(page, search: search));

      return Ok(quizGroups);
    }

    [HttpGet("{id}/quizzes/all")]
    public async Task<IActionResult> ListGroupQuizzesAsync(int id, [FromQuery] int page = 1)
    {
      var group = _mapper.Map<QuizGroup, IdNamePairResource>(_quizService.GetGroup(id));
      var groupQuizzes = _mapper
        .Map<IEnumerable<Quiz>, ICollection<QuizResource>>(
          await _quizService.GetGroupQuizzesAsync(id, page));

      return Ok(new 
      {
        Group = group,
        Quizzes = groupQuizzes
      });
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> Mine([FromQuery] int page = 1)
    {
      var userId = _userManager.GetUserId(this.User);

      var userGroups = _mapper
        .Map<IEnumerable<QuizGroup>, ICollection<QuizGroupResource>>(
          await _quizService.GetUserOwnGroupsAsync(userId, page));

      return Ok(userGroups);
    }
  }
}