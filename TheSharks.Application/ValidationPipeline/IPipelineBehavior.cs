using MediatR;

namespace TheSharks.Application.ValidationPipeline;

public interface IPipelineBehavior<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
}