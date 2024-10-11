﻿using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Revit.MultiTenancy;
using Revit.MultiTenancy.Dto;
using Revit.Shared.Validations;
using FluentValidation;

namespace Revit.Admin.Validations
{
    public class CreateTenantValidator : AbstractValidator<CreateTenantInput>
    {
        public CreateTenantValidator()
        {
            RuleFor(x => x.TenancyName).IsRequired().Regular(TenantConsts.TenancyNameRegex).MaxLength(AbpTenantBase.MaxTenancyNameLength);
            RuleFor(x => x.Name).IsRequired().MaxLength(AbpTenantBase.MaxNameLength);
            RuleFor(x => x.AdminEmailAddress).IsRequired().Email().MaxLength(AbpUserBase.MaxEmailAddressLength);
            RuleFor(x => x.AdminPassword).MaxLength(AbpUserBase.MaxPasswordLength);
            RuleFor(x => x.ConnectionString).MaxLength(AbpTenantBase.MaxConnectionStringLength);
        }
    }

    public class UpdateTenantValidator : AbstractValidator<TenantEditDto>
    {
        public UpdateTenantValidator()
        {
            RuleFor(x => x.TenancyName).IsRequired().Regular(TenantConsts.TenancyNameRegex).MaxLength(AbpTenantBase.MaxTenancyNameLength);
            RuleFor(x => x.Name).IsRequired().MaxLength(AbpTenantBase.MaxNameLength);
        }
    }
}