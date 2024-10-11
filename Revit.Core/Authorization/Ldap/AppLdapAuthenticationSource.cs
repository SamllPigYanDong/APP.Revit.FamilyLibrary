using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using AppFramework.Authorization.Users;
using AppFramework.MultiTenancy;

namespace AppFramework.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}