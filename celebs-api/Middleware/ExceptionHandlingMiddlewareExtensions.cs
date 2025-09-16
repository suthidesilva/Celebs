using CelebApi.Exceptions;
using System.Net;
using System.Text.Json;


namespace CelebApi.Middleware
{
    public class ExceptionHandlingMiddlewareExtensions(RequestDelegate next)
    {

        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var requestId = Guid.NewGuid().ToString();
            var startTime = DateTime.UtcNow;
            
            try
            {
                Console.WriteLine($"[{requestId}] 📥 {context.Request.Method} {context.Request.Path} - Started at {startTime:yyyy-MM-dd HH:mm:ss.fff}");
                
                await _next(context); // Proceed to controller
                
                var duration = DateTime.UtcNow - startTime;
                Console.WriteLine($"[{requestId}] ✅ {context.Request.Method} {context.Request.Path} - Completed in {duration.TotalMilliseconds}ms with status {context.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                var duration = DateTime.UtcNow - startTime;
                Console.WriteLine($"[{requestId}] ❌ {context.Request.Method} {context.Request.Path} - Failed after {duration.TotalMilliseconds}ms");
                Console.WriteLine($"[{requestId}] Exception: {ex.GetType().Name} - {ex.Message}");
                Console.WriteLine($"[{requestId}] Stack Trace: {ex.StackTrace}");

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    ArgumentNullException => (int)HttpStatusCode.BadRequest,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    TimeoutException => (int)HttpStatusCode.RequestTimeout,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var returnedMessage = context.Response.StatusCode switch
                {
                    (int)HttpStatusCode.InternalServerError => "An unexpected error occurred. Please try again later.",
                    (int)HttpStatusCode.BadRequest => ex.Message,
                    (int)HttpStatusCode.NotFound => ex.Message,
                    (int)HttpStatusCode.Unauthorized => "Unauthorized access.",
                    (int)HttpStatusCode.RequestTimeout => "Request timeout. Please try again.",
                    _ => ex.Message
                };

                var errorResponse = new
                {
                    status = context.Response.StatusCode,
                    message = returnedMessage,
                    requestId = requestId,
                    timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions 
                { 
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
                });
                
                await context.Response.WriteAsync(json);
            }
        }
    }
}
