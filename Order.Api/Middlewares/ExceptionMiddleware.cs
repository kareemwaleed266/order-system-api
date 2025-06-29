using Order.Service.HandleResponses;
using System.Net;
using System.Text.Json;

namespace Order.API.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _evironment;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(
        RequestDelegate next,
            IHostEnvironment evironment,
            ILogger<ExceptionMiddleware> logger
            )
        {
            _next = next;
            _evironment = evironment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _evironment.IsDevelopment()
                    ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CustomException((int)HttpStatusCode.InternalServerError);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
