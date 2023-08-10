using FluentValidation.Results;

namespace Application.Common.Exceptions;
public class NotFoundValidationException : ValidationException
{
    public NotFoundValidationException(IEnumerable<ValidationFailure> failures)
        : base(failures)
    {
    }

    public NotFoundValidationException()
    {
    }

    public NotFoundValidationException(string message)
        : base(message)
    {
    }

    public NotFoundValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}