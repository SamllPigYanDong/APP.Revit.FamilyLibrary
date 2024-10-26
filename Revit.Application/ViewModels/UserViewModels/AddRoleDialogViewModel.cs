using Abp.Application.Services.Dto;
using Prism.Services.Dialogs;
using Revit.Authorization.Roles;
using Revit.Authorization.Roles.Dto;
using Revit.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Application.ViewModels.UserViewModels
{
    public class AddRoleDialogViewModel : ViewModelBase, IDialogAware
    {
        private IRoleAppService _roleAppService;

        public AddRoleDialogViewModel(IRoleAppService roleAppService)
        {
            _roleAppService=roleAppService;
        }

        public string Title => "添加角色";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }

        //public override async void OnDialogOpened(IDialogParameters parameters)
        //{
        //    await SetBusyAsync(async () =>
        //    {
        //        //long? id = null;

        //        //if (parameters != null && parameters.ContainsKey("Value"))
        //        //    id = parameters.GetValue<RoleDto>("Value").Id;
        //        //var output = await _roleAppService.GetRoleForEdit(new NullableIdDto(id));
        //        //Role = Map<RoleEditModel>(output.Role);
        //        //treesService.CreatePermissionTrees(output.Permissions, output.GrantedPermissionNames);
        //    });
        //}
    }
}
