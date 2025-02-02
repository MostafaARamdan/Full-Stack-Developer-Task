using Full.Stack.Task.Domain.Constants;
using Full.Stack.Task.Domain.GeneralResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Full.Stack.Task.Application.Behaviors
{
    internal static class ValidationResponse<TResult> where TResult : Result
    {
        internal static TResult Create(EResponseStatus status, params string[] errorMessage)
        {
            var resultType = typeof(TResult);
            var resourceType = resultType.GenericTypeArguments[0];

            var resultConstructor = resultType.GetConstructor([resourceType, typeof(EResponseStatus), typeof(string[])]);

            var defaultResource = Activator.CreateInstance(resourceType);

            var resultInstance = resultConstructor?.Invoke([defaultResource, status, errorMessage]);

            return (TResult)resultInstance!;
        }
    }
}
