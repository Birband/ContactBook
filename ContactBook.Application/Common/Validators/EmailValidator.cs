using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Common.Validators;
public static class EmailValidator
{
    public static ValidationCheck ValidateEmail(string? email)
    {
        var result = new ValidationCheck();

        if (string.IsNullOrWhiteSpace(email))
        {
            result.AddError("Email cannot be empty.");
            return result;
        }
        
        var emailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
        if (!emailRegex.IsMatch(email))
        {
            result.AddError("Email is not in the correct format.");
        }

        return result;

    }
}
