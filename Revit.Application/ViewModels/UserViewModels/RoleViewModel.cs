using CommunityToolkit.Mvvm.Input;
using Prism.Regions;
using Prism.Services.Dialogs;
using Revit.Authorization.Roles;
using Revit.Shared;
using Revit.Shared.Entity.Permissions;
using Revit.Shared.Entity.Roles;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Services.Datapager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Application.ViewModels.UserViewModels
{
    public partial class RoleViewModel : NavigationCurdViewModel
    {
        private readonly IRoleAppService roleService;
        private ObservableCollection<PermissionDto> _permissionList;

        public ObservableCollection<PermissionDto> PermissionList
        {
            get { return _permissionList; }
            set { SetProperty(ref _permissionList, value); }
        }

        public RoleViewModel(IRoleAppService roleService)
        {
            this.roleService = roleService;
            dataPager.OnPageIndexChangedEventhandler += RoleOnPageIndexChangedEventhandler;
            OnNavigatedToAsync();
        }

        [RelayCommand]
        public async void RoleSelctionChanged()
        {
            if (dataPager.SelectedItem is RoleCreateDto role)
            {
                await SetBusyAsync(async () =>
                {
                    await roleService.GetRolePermissions(role.Id).WebAsync(successCallback: (permissions) =>
                    {
                        PermissionList = new ObservableCollection<PermissionDto>(permissions);
                        return Task.CompletedTask;
                    });
                });
            }
        }

        private async void RoleOnPageIndexChangedEventhandler(object sender, PageIndexChangedEventArgs e)
        {
            await SetBusyAsync(async () =>
            {
                await roleService.GetAllRoles();
            });
        }

        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            await SetBusyAsync(async () =>
            {
                await roleService.GetAllRoles().WebAsync(dataPager.SetList);
            });
        }
    }
}
