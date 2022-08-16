using FluentValidation;
using GoCode.Application.Common.BaseResponse;
using MediatR;

namespace GoCode.Application.Common.PipelineBehaviors
{
    public class ValidationBehavior<TReuest, TResponse> : IPipelineBehavior<TReuest, Response<TResponse>>
        where TReuest : IRequest<Response<TResponse>>
    {
        private readonly IEnumerable<IValidator<TReuest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TReuest>> validators)
        {
            _validators = validators;
        }

        public async Task<Response<TResponse>> Handle(TReuest request, CancellationToken cancellationToken, RequestHandlerDelegate<Response<TResponse>> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TReuest>(request);
                var validationTasks = _validators.Select(x => x.ValidateAsync(request));

                var results = await Task.WhenAll(validationTasks);
                var failures = results
                    .SelectMany(x => x.Errors)
                    .Where(x => x != null)
                    .ToList();

                var errors = failures.Select(x => $"{x.PropertyName}: {x.ErrorMessage}");

                if (errors.Any())
                {
                    return ResponseResult.ValidationError<TResponse>(errors);
                }
            }

            return await next();
        }
    }
}
