using Abp.Authorization;
using AppFramework.Authorization.Roles;
using AppFramework.Authorization.Users;

namespace AppFramework.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
