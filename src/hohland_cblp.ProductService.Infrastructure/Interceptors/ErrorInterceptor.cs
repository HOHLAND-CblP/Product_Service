using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace hohland_cblp.ProductService.Infrastructure.Interceptors;

public class ErrorInterceptor : Interceptor
{
    private readonly ILogger<LogInterceptor> _logger;

    public ErrorInterceptor(ILogger<LogInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
        ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (RpcException e)
        {
            _logger.LogCritical($"Ошибка выполнения метода {1}. Ошибка = {2}", context.Method, e.Message);
            throw e;
        }
        catch (Exception e)
        {

            _logger.LogCritical($"Ошибка выполнения метода {1}. Ошибка = {2}", context.Method, e.Message);
            throw e;
        }
    }
}
