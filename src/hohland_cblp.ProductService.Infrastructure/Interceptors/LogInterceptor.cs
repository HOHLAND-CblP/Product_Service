using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace hohland_cblp.ProductService.Infrastructure.Interceptors;

public class LogInterceptor : Interceptor
{
    private readonly ILogger<LogInterceptor> _logger;

    public LogInterceptor(ILogger<LogInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
        ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        _logger.LogInformation($"Начало вызова метода {context.Method} с реквестом = {request}");
        var response = await continuation(request, context);
        _logger.LogInformation($"Конец {context.Method} с реквестом = {request}, респонс = {response}");
        return response;
    }
}
