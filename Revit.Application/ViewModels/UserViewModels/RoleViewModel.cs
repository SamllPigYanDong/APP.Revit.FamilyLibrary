using Prism.Regions;
using Prism.Services.Dialogs;
using Revit.Authorization.Roles;
using Revit.Shared;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Services.Datapager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Application.ViewModels.UserViewModels
{
    public class RoleViewModel : NavigationCurdViewModel
    {
        private readonly IRoleAppService roleService;

        public RoleViewModel(IRoleAppService roleService)
        {
            this.roleService = roleService;
            dataPager.OnPageIndexChangedEventhandler += RoleOnPageIndexChangedEventhandler;
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
