using Microsoft.AspNetCore.Builder;
using System;
using System.Linq;
using System.Threading;
using DVL_TBC.PersonsApi.Models;

namespace DVL_TBC.PersonsApi.Extensions
{
    public static class ApiMiddlewares
    {
        public static IApplicationBuilder UseAcceptLanguageHeader(this IApplicationBuilder app,
            string defaultLanguage = "en-US")
            => app.Use((context, next) =>
            {
                var allowedLanguages = new[] {"en-US", "ka-GE"};
                var language = context.Request.Headers["Accept-Language"].ToString().Split(',').FirstOrDefault();
                language = allowedLanguages.Contains(language) ? language : defaultLanguage;

                Thread.CurrentThread.CurrentCulture =
                    new System.Globalization.CultureInfo(language! ?? throw new InvalidOperationException());
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                return next();
            });

        public static IApplicationBuilder UseUnhandledExceptionHandler(this IApplicationBuilder app) =>
            app.UseMiddleware<UnhandledExceptionsMiddleware>();
    }
}
