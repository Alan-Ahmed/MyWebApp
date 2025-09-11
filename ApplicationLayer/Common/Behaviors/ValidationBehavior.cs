// ApplicationLayer/Common/Behaviors/ValidationBehavior.cs
using System.Reflection;
using FluentValidation;
using MediatR;
using MyWebApp.ApplicationLayer.Common;

namespace MyWebApp.ApplicationLayer.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));
            var failures = results.SelectMany(r => r.Errors).Where(e => e is not null).ToList();

            if (failures.Count == 0)
                return await next();

            // Bygg OperationResult<*rätt T*> där TResponse = OperationResult<T>
            var tResponse = typeof(TResponse);

            if (tResponse.IsGenericType && tResponse.GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                var innerType = tResponse.GetGenericArguments()[0];

                // Hitta OperationResult.Fail<T>(IEnumerable<string> errors)
                var failMethod = typeof(OperationResult)
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .First(m =>
                        m.Name == nameof(OperationResult.Fail) &&
                        m.IsGenericMethodDefinition &&
                        m.GetParameters().Length == 1 &&
                        m.GetParameters()[0].ParameterType == typeof(IEnumerable<string>)
                    );

                var failGeneric = failMethod.MakeGenericMethod(innerType);
                var errorMessages = failures.Select(f => f.ErrorMessage);
                var failResult = failGeneric.Invoke(null, new object[] { errorMessages });

                return (TResponse)failResult!;
            }

            // Om någon request INTE följer OperationResult<T>, kasta valideringsfel
            throw new ValidationException(failures);
        }
    }
}
