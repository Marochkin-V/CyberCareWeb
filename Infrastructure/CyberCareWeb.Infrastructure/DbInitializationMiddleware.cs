using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CyberCareWeb.Infrastructure
{
    public class DbInitializationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DbInitializationMiddleware> _logger;

        public DbInitializationMiddleware(RequestDelegate next, ILogger<DbInitializationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Инициализация базы данных
            DbInitializer.Initialize(context.RequestServices, _logger);

            // Продолжение обработки запроса
            await _next(context);
        }
    }
}
