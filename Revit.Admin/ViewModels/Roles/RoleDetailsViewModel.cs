using Abp.Application.Services.Dto;
using Revit.Authorization.Roles;
using Revit.Authorization.Roles.Dto;
using Revit.Shared;
using Revit.Admin.Models;
using Prism.Services.Dialogs;
using Revit.Admin.Services;
using System.Threading.Tasks;

namespace Revit.Admin.ViewModels
{
    public class RoleDetailsViewModel : HostDialogViewModel
    {
        #region 字段/属性

        private readonly IRoleAppService appService;
        public IPermissionTreesService treesService { get; set; }
        private RoleEditModel role;

        public RoleEditModel Role
        {
            get { return role; }
            set { role = value; OnPropertyChanged(); }
        }

        #endregion 字段/属性

        public RoleDetailsViewModel(IRoleAppService appService,
            IPermissionTreesService treesService)
        {
            this.appService = appService;
            this.treesService = treesService;
        }

        public override async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await appService.CreateOrUpdateRole(new CreateOrUpdateRoleInput()
                {
                    Role = Map<RoleEditDto>(Role),
                    GrantedPermissionNames = treesService.GetSelectedItems()
                }).WebAsync(base.Save);
            });
        }

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            await SetBusyAsync(async () =>
            {
                int? id = null;

                if (parameters != null && parameters.ContainsKey("Value"))
                    id = parameters.GetValue<RoleListDto>("Value").Id;
                var output = await appService.GetRoleForEdit(new NullableIdDto(id));
                Role = Map<RoleEditModel>(output.Role);
                treesService.CreatePermissionTrees(output.Permissions, output.GrantedPermissionNames);
            });
        }
    }
}
