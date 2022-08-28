using FluentValidation;
using GoCode.Application.Common.BaseResponse;
using MediatR;
using System.Net;

namespace GoCode.Application.Common.PipelineBehaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationTasks = _validators.Select(x => x.ValidateAsync(request));

                var results = await Task.WhenAll(validationTasks);
                var errors = results
                    .SelectMany(x => x.Errors)
                    .Where(x => x != null)
                    .Select(x => x.ErrorMessage);

                if (errors.Any())
                {
                    return (TResponse)Activator.CreateInstance(typeof(TResponse), errors,
                       ResponseError.ValidationError, HttpStatusCode.BadRequest, default);
                }
            }

            return await next();
        }
    }
}
