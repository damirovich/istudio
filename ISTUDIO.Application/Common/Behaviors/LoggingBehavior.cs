using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ISTUDIO.Application.Common.Behaviors;

public class LoggingBehavior <TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest
        : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    //Лоигирование и измерение производительности при обработке запросов Mediatr
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestId = Guid.NewGuid().ToString();
        var timer = new Stopwatch();
        _logger.LogInformation($"Begin Request Id: {requestId} Request name: {typeof(TRequest).Name} Date Request: {DateTime.Now}");
        timer.Start();
        var response = await next();
        timer.Stop();
        _logger.LogInformation($"End Request Id: {requestId} Request name: {typeof(TRequest).Name} Elapsed Time: {timer.ElapsedMilliseconds} Date Request: {DateTime.UtcNow}");

        return response;
    }
}
