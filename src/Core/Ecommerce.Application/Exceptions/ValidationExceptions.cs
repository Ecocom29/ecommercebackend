using FluentValidation.Results;

namespace Ecommerce.Application.Exceptions;

public class ValidationExceptions : ApplicationException
{
    public IDictionary<string, string[]> Errors {get;}

    public ValidationExceptions() : base("Se presentaron uno o mas errores de validación")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationExceptions(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
        .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
        .ToDictionary(
            failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()
        );
    }
}