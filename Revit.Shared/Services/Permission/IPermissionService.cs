﻿namespace Revit.Shared.Services.Permission
{
    /// <summary>
    /// 权限验证接口
    /// </summary>
    public interface IPermissionService
    {
        bool HasPermission(string key);
    }
}