using Revit.Admin.Models;
using Revit.Shared.Validations;
using FluentValidation; 

namespace Revit.Admin.Validations
{
    public class VersionValidator : AbstractValidator<VersionListModel>
    {
        public VersionValidator()
        {
            RuleFor(x => x.Name).IsRequired();
            RuleFor(x => x.Version).IsRequired();
        }
    }
}
