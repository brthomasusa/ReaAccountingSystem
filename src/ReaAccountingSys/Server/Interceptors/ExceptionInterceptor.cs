using Microsoft.Extensions.Logging;

namespace ReaAccountingSys.Server.Interceptors
{
    public class ExceptionInterceptor : Interceptor
    {
        // private ILoggerManager _logger;
        private readonly ILogger<ExceptionInterceptor> _logger;
        private Guid _correlationId => Guid.NewGuid();

        public ExceptionInterceptor(ILogger<ExceptionInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception e)
            {
                throw e.Handle(context, _logger, _correlationId);
            }
        }

        public override async Task<TResponse> ClientStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            ServerCallContext context,
            ClientStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(requestStream, context);
            }
            catch (Exception e)
            {
                throw e.Handle(context, _logger, _correlationId);
            }
        }


        public override async Task ServerStreamingServerHandler<TRequest, TResponse>(
            TRequest request,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            ServerStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                await continuation(request, responseStream, context);
            }
            catch (Exception e)
            {
                throw e.Handle(context, _logger, _correlationId);
            }
        }

        public override async Task DuplexStreamingServerHandler<TRequest, TResponse>(
            IAsyncStreamReader<TRequest> requestStream,
            IServerStreamWriter<TResponse> responseStream,
            ServerCallContext context,
            DuplexStreamingServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                await continuation(requestStream, responseStream, context);
            }
            catch (Exception e)
            {
                throw e.Handle(context, _logger, _correlationId);
            }
        }


    }
}