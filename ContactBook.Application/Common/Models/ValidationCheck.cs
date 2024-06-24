namespace ContactBook.Application.Common.Models;

public class ValidationCheck
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();

    public ValidationCheck()
    {
        IsValid = true;
    }

    public void AddError(string error)
    {
        IsValid = false;
        Errors.Add(error);
    }
}