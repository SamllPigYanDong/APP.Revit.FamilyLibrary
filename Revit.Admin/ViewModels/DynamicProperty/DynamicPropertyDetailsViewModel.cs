﻿using Revit.Shared;
using Revit.Admin.Models;
using Revit.DynamicEntityProperties;
using Revit.DynamicEntityProperties.Dto; 
using Prism.Services.Dialogs;
using System.Threading.Tasks;

namespace Revit.Admin.ViewModels
{
    public class DynamicPropertyDetailsViewModel : HostDialogViewModel
    {
        private DynamicPropertyModel model;
        private readonly IDynamicPropertyAppService appService;

        public DynamicPropertyModel Model
        {
            get { return model; }
            set { model = value; OnPropertyChanged(); }
        }

        public DynamicPropertyDetailsViewModel(IDynamicPropertyAppService appService)
        {
            model = new DynamicPropertyModel();
            this.appService = appService;
        }

        public override async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                var input = Map<DynamicPropertyDto>(Model);

                await WebRequest.Execute(async () =>
                {
                    if (input.Id > 0)
                        await appService.Update(input);
                    else
                        await appService.Add(input);
                }, base.Save);
            });
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Value"))
            {
                var dynamicProperty = parameters.GetValue<DynamicPropertyDto>("Value");
                Model = Map<DynamicPropertyModel>(dynamicProperty);
            }
        }
    }
}
