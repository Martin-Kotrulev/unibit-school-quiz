namespace Uniquizbit.Web.Controllers
{
  using AutoMapper;
  using Data.Models;
  using Core.Models;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.EntityFrameworkCore;
  using Models;
  using Microsoft.AspNetCore.Identity;
  using Services;
  using System;
  using System.Collections.Generic;
  using System.Data;
  using System.Data.SqlClient;
  using System.Linq;
  using System.Text.RegularExpressions;
  using System.Threading.Tasks;

  public class GroupsController : BaseApiController
  {
    private readonly IQuizService _quizService;
    private readonly IQuizGroupService _quizGroupService;
    private readonly ITagService _tagService;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GroupsController(IQuizService quizService,
      IQuizGroupService quizGroupService,
      ITagService tagService,
      IMapper mapper,
      UserManager<User> userManager)
    {
      _userManager = userManager;
      _mapper = mapper;
      _quizService = quizService;
      _quizGroupService = quizGroupService;
      _tagService = tagService;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddQuizGroup([FromBody] QuizGroupResource quizGroupResource)
    {
      if (ModelState.IsValid)
      {
        var user = await _userManager.GetUserAsync(this.User);
        var quizGroup = _mapper.Map<QuizGroupResource, QuizGroup>(quizGroupResource);

        if (await _quizGroupService.QuizGroupExistsAsync(quizGroup.Name))
        {
          return BadRequest(new ApiResponse(
            $"Quiz group with name '{quizGroup.Name}' already exists.", false
          ));
        }

        var existingTags = await _tagService.UpdateTagsAsync(quizGroupResource.Tags);
        var tagNames = existingTags.Select(t => t.Name);

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

        await _quizGroupService.AddQuizGroupAsync(quizGroup);

        return Ok(new ApiResponse(
          _mapper.Map<QuizGroup, QuizGroupResource>(quizGroup),
          $"You successfully created a new group with name '{ quizGroup.Name }'"
        ));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [Authorize]
    [HttpDelete("{groupId}")]
    public async Task<IActionResult> DeleteQuizGroup(int groupId)
    {
      var userId = _userManager.GetUserId(this.User);

      if (await _quizGroupService.DeleteQuizGroupAsync(groupId, userId))
      {
        return Ok(new ApiResponse("You successfully deleteted the quiz group."));
      }
      else
      {
        return BadRequest(
          new ApiResponse("You can't delete the specified quiz group.", false));
      }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroups([FromQuery] string search, [FromQuery] int page = 1)
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
              await _quizGroupService.SearchQuizGroupsByTagsAsync(searchTags, page)
            );

          return Ok(quizGroups);
        }
      }

      quizGroups = _mapper
        .Map<IEnumerable<QuizGroup>, ICollection<QuizGroupResource>>(
          await _quizGroupService.GetQuizGroupsAsync(page, search: search));

      return Ok(quizGroups);
    }

    [HttpGet("{id}/quizzes")]
    public async Task<IActionResult> GetGroupQuizzesAsync(int id, [FromQuery] int page = 1)
    {
      var group = _mapper.Map<QuizGroup, IdNamePairResource>(
        await _quizGroupService.FindGroupByIdAsync(id));
      var groupQuizzes = _mapper
        .Map<IEnumerable<Quiz>, ICollection<QuizResource>>(
          await _quizGroupService.GetGroupQuizzesAsync(id, page));

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
          await _quizGroupService.GetUserOwnGroupsAsync(userId, page));

      return Ok(userGroups);
    }
  }
}