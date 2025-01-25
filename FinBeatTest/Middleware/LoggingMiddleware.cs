using Data.Context;
using Data.Models;
using NLog;

namespace FinBeatTest.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ApplicationContext appContext) => await LogRequestResponseAsync(context, appContext);

        /// <summary>
        /// Запонение логов
        /// </summary>
        /// <param name="context">Контекст Http</param>
        /// <param name="appContext">Контекст бд</param>
        private async Task LogRequestResponseAsync(HttpContext context, ApplicationContext appContext)
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

                if (context.Request.Method == "POST")
                {
                    context.Request.EnableBuffering();
                    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();


                    context.Request.Body.Position = 0;
                    log.Payload = body.Length > 3000 ? body.Substring(0, 3000) : body;
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
                    log.Response = responseBodyText.Length > 3000 ? responseBodyText.Substring(0, 3000) : responseBodyText;
                    log.Timestamp = DateTime.UtcNow;

                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBodyStream);

                    await TrySaveLogAsync(log, appContext);
                }
            }
        }

        /// <summary>
        /// Сохранение логов в бд в таблицу "apiLogs"
        /// </summary>
        /// <param name="apiLog">Записи лога</param>
        /// <param name="appContext">Контект бд</param>
        private async Task TrySaveLogAsync(ApiLog apiLog, ApplicationContext appContext)
        {
            try
            {
                appContext.Add(apiLog);
                await appContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Save log error: {ex.Message}");
            }
        }
    }
}
