using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DVL_TBC.PersonsApi.Models
{
    public class UnhandledExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public UnhandledExceptionsMiddleware(RequestDelegate next, ILogger<UnhandledExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exc)
            {
                _logger.LogError($"Unhandled Exception. ErrorMessage {exc.Message}");
                throw;
            }
        }
    }
}
