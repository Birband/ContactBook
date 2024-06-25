using System.Text.Json;
using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Common.Exceptions;

/// <summary>
/// Exception for validation errors for handling more than one error message
/// </summary>
public class ValidationException : Exception
{
    public List<string> Errors { get; }
    public ValidationException(ValidationCheck val)
    {
        Errors = val.Errors;
    }
}