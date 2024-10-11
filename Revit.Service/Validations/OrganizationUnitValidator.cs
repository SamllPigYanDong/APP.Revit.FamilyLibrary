using Abp.Organizations;
using Revit.Admin.Models;
using Revit.Shared.Validations;
using FluentValidation;

namespace Revit.Admin.Validations
{
    public class OrganizationUnitValidator : AbstractValidator<CreateOrganizationUnitModel>
    {
        public OrganizationUnitValidator()
        {
            RuleFor(x => x.DisplayName).IsRequired().MaxLength(OrganizationUnit.MaxDisplayNameLength);
        }
    }
}