using Microsoft.AspNetCore.Builder;

namespace CelebApi.Middleware;

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddlewareExtensions>();
    }
}
