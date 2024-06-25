using System.Text.Json;
using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Common.Exceptions;
public class ValidationException : Exception
{
    public List<string> Errors { get; }
    public ValidationException(ValidationCheck val)
    {
        Errors = val.Errors;
    }
}