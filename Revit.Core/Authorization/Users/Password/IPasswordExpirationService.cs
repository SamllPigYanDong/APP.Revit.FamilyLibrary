using Abp.Domain.Services;

namespace AppFramework.Authorization.Users.Password
{
    public interface IPasswordExpirationService : IDomainService
    {
        void ForcePasswordExpiredUsersToChangeTheirPassword();
    }
}
