using Revit.Service.IServices;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Revit.Authorization.Roles;
using System.Linq;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Permissions;
using Revit.Authorization.Permissions;
using System;
using Prism.Services.Dialogs;
using DryIoc;
using Revit.Application.Views.UserViews.DialogViews;
using Revit.Authorization.Users;
using System.Windows;
using Autodesk.Revit.UI;
using Revit.Authorization.Users.Dto;
using Revit.Shared;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Entity.Roles;


namespace Revit.Application.ViewModels.UserViewModels
{
    public partial class AccountManagerViewViewModel : ViewModelBase
    {
        private readonly IUserAppService userAppService;
        private readonly IRoleAppService roleService;
        private readonly IPermissionAppService permissionService;
        private readonly IDialogService dialogService;
        private ObservableCollection<UserDto> _users = new ObservableCollection<UserDto>();
        public ObservableCollection<UserDto> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private ObservableCollection<RoleDto> _roles = new ObservableCollection<RoleDto>();
        public ObservableCollection<RoleDto> Roles
        {
            get { return _roles; }
            set { SetProperty(ref _roles, value); }
        }

        private ObservableCollection<PermissionDto> _permissions = new ObservableCollection<PermissionDto>();
        public ObservableCollection<PermissionDto> Permissions
        {
            get { return _permissions; }
            set { SetProperty(ref _permissions, value); }
        }

        public PagedList<UserDto> PagedList { get; set; }





        private static UserPageRequestDto _userQueryParameter = new UserPageRequestDto() { PageIndex = 1, PageSize = 10, SearchMessage = "" };
        public UserPageRequestDto UserQueryParameter
        {
            get { return _userQueryParameter; }
            set { SetProperty(ref _userQueryParameter, value); }
        }

        private static RolePageRequestDto _roleQueryParameter = new RolePageRequestDto() { PageIndex = 1, PageSize = 10, SearchMessage = "" };
        public RolePageRequestDto RoleQueryParameter
        {
            get { return _roleQueryParameter; }
            set { SetProperty(ref _roleQueryParameter, value); }
        }


       
        [RelayCommand]
        private async Task AddUsers()
        {
            dialogService.ShowDialog(nameof(AddUserDialogView), null, async (IDialogResult result) =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var userCreateDto = result.Parameters.GetValue<UserCreateDto>(nameof(UserCreateDto));
                    var userDto = await userAppService.CreateOrUpdateUser(userCreateDto);
                    if (userDto != null)
                    {
                        Users.Add(userDto);
                        Users = Users;
                    }
                }
            });
        }

        [RelayCommand]
        private async Task DeleteUser(UserDto user)
        {
            if (MessageBox.Show("删除用户后不可恢复，请是否确认删除！", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {

                await userAppService.DeleteUser(new Abp.Application.Services.Dto.EntityDto<long>(user.Id)).WebAsync(async () => { await GetUsers(); });
            }
        }

        [RelayCommand]
        private async Task GetRolePermissions(RoleDto role)
        {
            var result = await roleService.GetRolePermissions(role.Id);
            if (result != null && result.Any())
            {
                Permissions = new ObservableCollection<PermissionDto>(result);
            }
        }

        public async void Init()
        {
            await GetUsers();
        }

        [RelayCommand]
        private async Task GetUsers()
        {
            await userAppService.GetUsers(UserQueryParameter).WebAsync(successCallback: async (result) =>
            {
                if (result != null)
                {
                    PagedList = result;
                    Users = new ObservableCollection<UserDto>(PagedList.Items);
                }
            });
        }
       
        [RelayCommand]
        private async Task GetPermissions()
        {
            var result = await permissionService.GetAllPermissions();
            if (result != null && result.Any())
            {
                Permissions = new ObservableCollection<PermissionDto>(result);
            }

        }


        public AccountManagerViewViewModel( IUserAppService userAppService, IRoleAppService roleService, IPermissionAppService permissionService, IDialogService dialogService)
        {
            this.userAppService = userAppService;
            this.roleService = roleService;
            this.permissionService = permissionService;
            this.dialogService = dialogService;
        }

    }
}
