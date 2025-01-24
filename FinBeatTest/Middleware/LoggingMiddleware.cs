using Data.Context;
using Data.Models;

namespace FinBeatTest.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LogContext logContext) => await LogRequestResponseAsync(context, logContext);

        /// <summary>
        /// Запонение логов
        /// </summary>
        /// <param name="context">Контекст Http</param>
        /// <param name="logContext">Контекст бд</param>
        private async Task LogRequestResponseAsync(HttpContext context, LogContext logContext)
        {
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                var log = new ApiLog
                {
                    Path = context.Request.Path,
                    QueryString = context.Request.QueryString.ToString(),
                    Method = context.Request.Method,
                    Timestamp = DateTime.UtcNow
                };

                if(context.Request.Method == "POST")
                {
                    context.Request.EnableBuffering();
                    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();

                    context.Request.Body.Position = 0;
                    log.Payload = body;
                }

                try
                {
                    await _next(context);
                }
                finally
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseBodyText = new StreamReader(responseBody).ReadToEnd();

                    log.ResponseCode = context.Response.StatusCode;
                    log.Response = responseBodyText;
                    log.Timestamp = DateTime.UtcNow;

                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);

                    await SaveLogAsync(log, logContext);
                }
            }
        }

        /// <summary>
        /// Сохранение логов в бд в таблицу "apiLogs"
        /// </summary>
        /// <param name="apiLog">Записи лога</param>
        /// <param name="logContext">Контект бд</param>
        private async Task SaveLogAsync(ApiLog apiLog, LogContext logContext)
        {
            logContext.ApiLogs.Add(apiLog);

            await logContext.SaveChangesAsync();
        }
    }
}
