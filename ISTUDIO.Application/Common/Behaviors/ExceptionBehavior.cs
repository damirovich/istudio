
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ISTUDIO.Application.Common.Behaviors;

public class ExceptionBehavior <TRequest, TResponse, TException>
    : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : IRequest<TResponse>
    where TException : Exception
{
    // Логгер для записи и регистрации исключений
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse, TException>> _logger;
    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse, TException>> logger)
    {
        _logger = logger;
    }

    // Реализация обработки исключений для указанных типов запроса и исключения
    public Task Handle(TRequest reguest, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        //Запись в Лог тип запроса и ошибку
        _logger.LogError(exception, "Something went wrong while handling request of type { @requestType} IndividualPerson Request  {@nameRequest}", typeof(TRequest), typeof(TRequest).Name);

        state.SetHandled(default);

        return Task.CompletedTask;
    }
}
