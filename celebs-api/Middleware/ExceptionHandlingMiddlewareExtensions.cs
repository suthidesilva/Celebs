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
            try
            {
                await _next(context); // Proceed to controller
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.GetType().Name} - {ex.Message}");

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = ex switch
                {
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var returnedMessage = context.Response.StatusCode switch
                {
                    (int)HttpStatusCode.InternalServerError => "Unexpected Error: Please Contact the Administator.",
                    _ => ex.Message
                };

                var errorResponse = new
                {
                    status = context.Response.StatusCode,
                    message = returnedMessage
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
