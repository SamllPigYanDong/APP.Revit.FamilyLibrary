﻿using Abp.Application.Services.Dto;
using Revit.Authorization.Permissions.Dto;
using Revit.Shared;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Linq;
using Revit.Admin.Services;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Revit.Admin.ViewModels
{
    public class SelectedPermissionViewModel : HostDialogViewModel
    {
        public IPermissionTreesService treesService { get; set; }

        public SelectedPermissionViewModel(IPermissionTreesService treesService)
        {
            this.treesService = treesService;
        }

        public override async Task Save()
        {
            base.Save(treesService.GetSelectedItems());
            await Task.CompletedTask;
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                var flatPermissions = parameters.GetValue<ListResultDto<FlatPermissionWithLevelDto>>("Value");
                var permissions = flatPermissions.Items.Select(t => t as FlatPermissionDto).ToList();
                treesService.CreatePermissionTrees(permissions, new List<string>());
            }
        }
    }
}
