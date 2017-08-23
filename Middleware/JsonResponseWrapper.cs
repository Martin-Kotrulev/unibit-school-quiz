using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Serialization;

namespace App.Middleware
{
    public class JsonResponseWrapper
    {
        private readonly RequestDelegate _next;

        public JsonResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) {
            // Saves current state of the response body
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                // Changes body stream to the current memory stream,
                // so that output from the latter middleweres goes here.
                context.Response.Body = memoryStream;

                // Pass the context to the next middlewares. 
                await _next(context);

                // Return response body to the previous state.
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var bodyStream = new StreamReader(memoryStream).ReadToEnd();
                var resultObj = JsonConvert.DeserializeObject(bodyStream);

                context.Response.ContentType = "application/json";
               
                // Send our modified content to the response body.
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(
                        BuildResponse(context.Response, resultObj),
                        new JsonSerializerSettings 
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }
                    )
                );
            }
        }

        private object BuildResponse(HttpResponse res, object result) {
            var success = (int)res.StatusCode >= 200 && (int)res.StatusCode <= 299;

            if (success) {
                return new {
                    StatusCode = res.StatusCode,
                    Message = "Message",
                    Success = success,
                    Result = result
                };
            } else {
                return new {
                    StatusCode = res.StatusCode,
                    Message = res.StatusCode == 404 ? "Resource not found!" : "",
                    Success = success,
                    Result = result
                };
            }
        }
    }
}