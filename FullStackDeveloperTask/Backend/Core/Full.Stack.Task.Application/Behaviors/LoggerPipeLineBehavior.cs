using Full.Stack.Task.Domain.Constants;
using Full.Stack.Task.Domain.GeneralResponse;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Behaviors
{
    public sealed class LoggerPipeLineBehavior<TRequest, TResponse>(ILogger<TRequest> _logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = string.Concat(
                typeof(TRequest).Name
                .Select((character, index) => index > 0 && char.IsUpper(character) ? (" " + character) : character.ToString()));

            _logger.LogDebug($"{request.ToString()} | Begin To Execute {requestName}");

            Stopwatch stopwatch = Stopwatch.StartNew();
            TResponse? response;
            try
            {
                response = await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{request.ToString()} | Error In {requestName} | {ex.Message}");
                response = ValidationResponse<TResponse>.Create(EResponseStatus.Exception, "Error_General_Unknown");
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogDebug($"{request.ToString()} | {requestName} Cost Time : {stopwatch.Elapsed.TotalMilliseconds} ms");
                _logger.LogDebug($"{request.ToString()} | {requestName} has been finished successfully");
            }
            return response;
        }
    }
}
