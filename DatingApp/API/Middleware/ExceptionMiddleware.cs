using System;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.Text.Json;
using System.Threading.Tasks;
using API._Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
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
                context.Response.ContentType = "Application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var Response = _env.IsDevelopment() ? new ApiExecption(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiExecption(context.Response.StatusCode, ex.Message, "Internal Server Error");


                var Options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(Response, Options);

                await context.Response.WriteAsync(json);
            }
        }

    }

}