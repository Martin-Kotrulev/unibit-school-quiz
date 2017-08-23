using Microsoft.AspNetCore.Builder;

namespace App.Middleware
{
    public static class JsonResponseWrapperExtension
    {
        public static IApplicationBuilder UserJsonResponseWrapper(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JsonResponseWrapper>();
        }
    }
}