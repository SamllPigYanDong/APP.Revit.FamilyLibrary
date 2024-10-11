using FluentValidation.Results;

namespace Revit.Entity.Interfaces
{
    public interface IGlobalValidator
    {
        ValidationResult Validate<T>(T model);
    }
}