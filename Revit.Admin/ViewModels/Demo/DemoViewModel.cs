﻿using Revit.Demo;
using Revit.Demo.Dtos;
using Revit.Shared;
using Revit.Shared.Services;
using Prism.Regions; 
using System.Threading.Tasks;

namespace Revit.Admin.ViewModels
{
    public class DemoViewModel: NavigationCurdViewModel
    {
        private readonly IAbpDemoAppService appService;

        public GetAllAbpDemoInput input;

        public DemoViewModel(IAbpDemoAppService appService)
        {
            Title = Local.Localize("DemoManager");
            this.appService = appService;
            input = new GetAllAbpDemoInput()
            {
                MaxResultCount = 10
            };
            dataPager.OnPageIndexChangedEventhandler += DataPager_OnPageIndexChangedEventhandler;
        }

        private void DataPager_OnPageIndexChangedEventhandler(object sender, PageIndexChangedEventArgs e)
        { }

        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            await SetBusyAsync(async () =>
            {
                await GetAbpDemos(input);
            });
        }

        private async Task GetAbpDemos(GetAllAbpDemoInput filter)
        {
            await WebRequest.Execute(() => appService.GetAll(filter), dataPager.SetList);
        } 
    }
}
