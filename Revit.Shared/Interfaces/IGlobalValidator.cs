using FluentValidation.Results;

namespace Revit.Shared.Interfaces
{
    public interface IGlobalValidator
    {
        ValidationResult Validate<T>(T model);
    }
}