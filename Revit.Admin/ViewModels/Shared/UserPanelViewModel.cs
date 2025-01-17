﻿using Revit.Admin.Services;
using Revit.Shared;
using Prism.Mvvm;
using Prism.Regions;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Commands;

namespace Revit.Admin.ViewModels
{
    public class UserPanelViewModel : NavigationViewModel
    { 
        public IApplicationService appService { get; set; }
        public DelegateCommand<string> ExecuteUserActionCommand { get; private set; }

        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            //appService = ContainerLocator.Container.Resolve<IApplicationService>();
            ExecuteUserActionCommand = new DelegateCommand<string>(appService.ExecuteUserAction);

            await Task.CompletedTask;
        }
    }
}
