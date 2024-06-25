using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Common.Validators;

public static class PhoneValidator
{
    public static ValidationCheck ValidatePhone(string? phone)
    {
        var result = new ValidationCheck();

        if (string.IsNullOrWhiteSpace(phone))
        {
            result.AddError("Phone cannot be empty.");
            return result;
        }
        
        var phoneRegex = new Regex(@"^\d{9}$");
        if (!phoneRegex.IsMatch(phone))
        {
            result.AddError("Phone is not in the correct format.");
        }

        return result;

    }
}