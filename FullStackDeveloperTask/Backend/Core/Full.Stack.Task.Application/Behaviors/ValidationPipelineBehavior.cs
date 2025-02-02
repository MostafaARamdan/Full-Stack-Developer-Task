using FluentValidation;
using Full.Stack.Task.Domain.Constants;
using Full.Stack.Task.Domain.GeneralResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidationResult = FluentValidation.Results.ValidationResult;
using TaskNamespace = System.Threading.Tasks;
namespace Full.Stack.Task.Application.Behaviors
{
    public sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!validators.Any())
                return await next();

            List<Task<FluentValidationResult>> validationTasks = validators
                .Select(validator => validator.ValidateAsync(request, cancellationToken))
                .ToList();

            await TaskNamespace.Task.WhenAll(validationTasks);

            string[] errors = validationTasks
                .SelectMany(validationTasks => validationTasks.Result.Errors)
                .Where(validationFailure => validationFailure != null)
                .Select(failure => failure.ErrorMessage)
                .Distinct()
                .ToArray();

            if (errors.Length != 0)
            {
                return ValidationResponse<TResponse>.Create(EResponseStatus.Error, errors);
            }

            return await next();
        }
    }
}
