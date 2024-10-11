using Revit.Shared; 
using Revit.MultiTenancy;
using Revit.MultiTenancy.Dto; 
using Prism.Services.Dialogs; 
using System.Threading.Tasks;
using Revit.Admin.Services;

namespace Revit.Admin.ViewModels
{
    public class TenantChangeFeaturesViewModel : HostDialogViewModel
    {
        private int Id;
        private readonly ITenantAppService tenantAppService;

        public IFeaturesService featuresService { get; set; }

        public TenantChangeFeaturesViewModel(IFeaturesService featuresService,
            ITenantAppService tenantAppService)
        {
            this.featuresService = featuresService;
            this.tenantAppService = tenantAppService;
        }

        public override async Task Save()
        {
            await SetBusyAsync(async () =>
            {
                await tenantAppService.UpdateTenantFeatures(new UpdateTenantFeaturesInput() { Id = Id, FeatureValues = featuresService.GetSelectedItems() }).WebAsync(base.Save);
            });
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Id"))
                Id = parameters.GetValue<int>("Id");

            if (parameters.ContainsKey("Value"))
            {
                var output = parameters.GetValue<GetTenantFeaturesEditOutput>("Value");

                featuresService.CreateFeatures(output.Features, output.FeatureValues);
            }
        }
    }
}
