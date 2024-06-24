using ContactBook.Application.Common.Models;

namespace ContactBook.Application.Common.Validators;

public class PasswordValidator
{
    public static ValidationCheck ValidatePassword(string? password)
    {
        var validationCheck = new ValidationCheck();

        if (string.IsNullOrWhiteSpace(password))
        {
            validationCheck.AddError("Password is required.");
        }
        else
        {
            if (password.Length < 8)
            {
                validationCheck.AddError("Password must be at least 8 characters.");
            }

            if (!password.Any(char.IsDigit))
            {
                validationCheck.AddError("Password must contain a number.");
            }

            if (!password.Any(char.IsUpper))
            {
                validationCheck.AddError("Password must contain an uppercase letter.");
            }

            if (!password.Any(char.IsLower))
            {
                validationCheck.AddError("Password must contain a lowercase letter.");
            }
        }

        return validationCheck;
    }
}
