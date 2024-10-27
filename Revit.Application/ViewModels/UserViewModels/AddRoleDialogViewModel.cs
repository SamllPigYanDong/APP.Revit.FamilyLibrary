using Abp.Application.Services.Dto;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using Prism.Services.Dialogs;
using Revit.Authorization.Roles;
using Revit.Shared;
using Revit.Shared.Entity.Roles;
using Revit.Shared.Extensions.Threading;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Application.ViewModels.UserViewModels
{
    public class AddRoleDialogViewModel : DialogViewModel
    {
        public AddRoleDialogViewModel()
        {
                
        }

        private IRoleAppService _roleAppService;

        private RoleCreateDto _role;
        private readonly IMapper mapper;

        public RoleCreateDto Role
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }


        public AddRoleDialogViewModel(IRoleAppService roleAppService)
        {
            _roleAppService=roleAppService;
        }

        public override async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await _roleAppService.PostRole(Role).WebAsync(successCallback:base.Save);
            });
        }


        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            await SetBusyAsync(async () =>
            {
                long? id = null;

                if (parameters != null && parameters.ContainsKey("Value"))
                    id = parameters.GetValue<RoleCreateDto>("Value").Id;
                var output = await _roleAppService.GetRole(id);
                Role = Map<RoleCreateDto>(output.Role);
            });
        }


    }
}
