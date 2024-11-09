﻿using Abp.Application.Services.Dto;
using Autodesk.Revit.DB;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Services.Dialogs;
using Revit.Application.Models;
using Revit.Authorization.Roles;
using Revit.Authorization.Users;
using Revit.Service.Services;
using Revit.Shared;
using Revit.Shared.Consts;
using Revit.Shared.Entity.Roles;
using Revit.Shared.Entity.Users;
using Revit.Shared.Extensions.Threading;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Admin.Models;
using Revit.Application.Models.Users;
using UserEditModel = Revit.Application.Models.Users.UserEditModel;

namespace Revit.Application.ViewModels.UserViewModels
{
    public partial class AddUserDialogViewModel : DialogViewModel
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IUserAppService _userAppService;

        public AddUserDialogViewModel(
            IRoleAppService roleAppService,
            IUserAppService userAppService
        )
        {
            _roleAppService = roleAppService;
            _userAppService = userAppService;
        }

        public string Title => "添加用户";

        [ObservableProperty]
        private UserCreateOrUpdateModel _input = new UserCreateOrUpdateModel();

        
        private static RolePageRequestDto _rolePageRequestDto = new RolePageRequestDto()
        {
            Name = "",
        };

        /// <summary>
        /// 是否是新建用户
        /// </summary>
        [ ObservableProperty]
        public bool _isNewUser;

        [ObservableProperty] public UserForEditModel _model;
       

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
           

            await SetBusyAsync(async () =>
            {
                UserDto? user = null;
                if (parameters.ContainsKey("Value"))
                    user = parameters.GetValue<UserDto>("Value");

                IsNewUser = user == null;
                Input.SetRandomPassword = IsNewUser;
                Input.SendActivationEmail = IsNewUser;

                await _userAppService.GetEditUser(new NullableIdDto<long>(user?.Id)).WebAsync(GetUserForEditSuccessed);
            });

            //if (parameters.ContainsKey("Config"))
                //PasswordComplexitySetting = parameters.GetValue<GetPasswordComplexitySettingOutput>("Config");
        }

        /// <summary>
        /// 设置编辑用户数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private async Task GetUserForEditSuccessed(GetUserForEditOutput output)
        {
            Model = Map<UserForEditModel>(output);
            //Model.OrganizationUnits = Map<List<OrganizationUnitModel>>(output.AllOrganizationUnits);

            if (IsNewUser)
            {
                //Model.Photo = ImageSource.FromResource(AssetsHelper.ProfileImagePlaceholderNamespace);
                Model.User = new UserEditModel
                {
                    IsActive = true,
                    IsLockoutEnabled = true,
                    ShouldChangePasswordOnNextLogin = true,
                };
            }

            await Task.CompletedTask;
        }

        public override async Task Save()
        {
            Input.User = Model.User;
            //Input.AssignedRoleNames = Model.Roles.Where(x => x.IsAssigned).Select(x => x.RoleName).ToArray();
            //Input.OrganizationUnits = Model.OrganizationUnits.Where(x => x.IsAssigned).Select(x => x.Id).ToList();

            if (!Verify(Input).IsValid) return;

            await SetBusyAsync(async () =>
            {
                var input = Map<CreateOrUpdateUserInput>(Input);
                await _userAppService.CreateOrUpdateUser(input).WebAsync(base.Save);
            }, AppLocalizationKeys.SavingWithThreeDot);
        }



        /// <summary>
        /// 生成可选的组织树
        /// </summary>
        /// <param name="organizationUnits"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        //private ObservableCollection<object> BuildOrganizationTree(
        //    List<OrganizationListModel> organizationUnits, long? parentId = null)
        //{
        //    var masters = organizationUnits
        //        .Where(x => x.ParentId == parentId).ToList();

        //    var childs = organizationUnits
        //        .Where(x => x.ParentId != parentId).ToList();

        //    foreach (OrganizationListModel dpt in masters)
        //        dpt.Items = BuildOrganizationTree(childs, dpt.Id);

        //    return new ObservableCollection<object>(masters);
        //}
    }
}
