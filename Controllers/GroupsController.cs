using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using App.Controllers;
using App.Controllers.Resources;
using App.Models;
using App.Services;
using App.Services.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;

namespace App.Controllers
{
  [Route("api/[controller]")]
  public class GroupsController : Controller
  {
    private readonly IQuizService _quizService;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    public GroupsController(IQuizService quizService, IMapper mapper, 
      IAuthenticationService authenticationService)
    {
      this._authenticationService = authenticationService;
      this._mapper = mapper;
      this._quizService = quizService;
    }

    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] QuizGroupResource quizGroupResource)
    {
      if (ModelState.IsValid)
      {
        var user = await _authenticationService.GetAuthenticatedUser(User);

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
        quizGroup.OwnerId = user.Id;

        _quizService.CreateGroup(quizGroup);

        return Ok(new ApiResponse(
          $"You successfully created a new group with name { quizGroup.Name }"
        ));
      }

      return BadRequest(new ApiResponse(ModelState));
    }

    [HttpGet("[action]")]
    public IActionResult All([FromQuery] int page, [FromQuery] string search)
    {
      System.Console.WriteLine(page);
      return Ok();
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> Mine([FromQuery] int page)
    {
      var user = await _authenticationService.GetAuthenticatedUser(User);

      var userGroups = _mapper
        .Map<IEnumerable<QuizGroup>, ICollection<QuizGroupResource>>(
          await _quizService.GetUserOwnGroupsAsync(user, page));

      return Ok(new ApiResponse(userGroups));
    }

    
  }
}