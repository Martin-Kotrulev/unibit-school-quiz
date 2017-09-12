using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace App.Controllers
{
  public class ApiResponse
  {
    public bool Success { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public object Result { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> Errors { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(object result, string message = null)
    {
      Result = result;
      Success = true;
      Message = message;
    }

    public ApiResponse(string message, bool success = true)
    {
      Message = message;
      Success = success;
    }

    public ApiResponse(ModelStateDictionary modelState)
    {
      if (modelState.IsValid)
      {
        throw new ArgumentException("ModelState must be invalid", nameof(modelState));
      }

      Errors = modelState
          .SelectMany(x => x.Value.Errors)
          .Select(e => e.Exception?.Message ?? e.ErrorMessage)
          .ToArray();
    }

    public ApiResponse(IdentityResult result)
    {
      Errors = result.Errors
          .Select(e => e.Description)
          .ToArray();
    }
  }
}