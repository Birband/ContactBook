using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Common.Exceptions;
public class ValidationException : Exception
{
    public ValidationException(ValidationCheck val)
    {
        var message = string.Join("\0", val.Errors);
        throw new Exception(message);
    }
}