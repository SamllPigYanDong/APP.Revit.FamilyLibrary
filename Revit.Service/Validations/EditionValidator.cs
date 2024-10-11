using Revit.Admin.Models;
using Revit.Shared.Validations;
using FluentValidation;

namespace Revit.Admin.Validations
{
    public class CreateEditionValidator : AbstractValidator<EditionCreateModel>
    {
        public CreateEditionValidator()
        {
            RuleFor(x => x.DisplayName).IsRequired();
        }
    }
}