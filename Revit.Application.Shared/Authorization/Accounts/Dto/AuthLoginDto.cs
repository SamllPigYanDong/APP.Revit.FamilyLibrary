namespace Revit.Authorization.Accounts.Dto
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class AuthLoginDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
