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
        public int StatusCode { get; set; }

        public bool Success { get; set; } = false;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Errors { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public ApiResponse(object result, string message)
            : this(200, message)
        {
            Result = result;
            Success = true;
        }

        public ApiResponse(ModelStateDictionary modelState)
            : this(400)
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
            : this(400)
        {
            Errors = result.Errors
                .Select(e => e.Description)
                .ToArray();
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch(statusCode)
            {
                case 401:
                    return "Unauthorized";
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}