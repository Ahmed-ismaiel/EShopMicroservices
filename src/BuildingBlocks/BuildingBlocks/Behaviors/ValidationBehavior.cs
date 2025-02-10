using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponce>
        (IEnumerable<IValidator<TRequest>> validations )
        : IPipelineBehavior<TRequest, TResponce>
        where TRequest : ICommand<TResponce>
    {
        public async Task<TResponce> Handle(TRequest request, RequestHandlerDelegate<TResponce> next, CancellationToken cancellationToken)
        {

            var context = new ValidationContext<TRequest>(request);

            var validationReults =
                await Task.WhenAll(validations.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationReults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();

            //throw new NotImplementedException();
        }
    }
}
