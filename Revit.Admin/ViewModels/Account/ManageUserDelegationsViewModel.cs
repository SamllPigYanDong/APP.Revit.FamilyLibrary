using Abp.Application.Services.Dto;
using Revit.Authorization.Users.Delegation;
using Revit.Authorization.Users.Delegation.Dto;
using Revit.Shared;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Threading.Tasks;
using Revit.Shared.Services;
using Revit.Entity.Interfaces;

namespace Revit.Admin.ViewModels
{
    public class ManageUserDelegationsViewModel : HostDialogViewModel
    {
        private readonly IUserDelegationAppService appService;
        private readonly IHostDialogService dialog;

        public IDataPagerService dataPager { get; private set; }
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<UserDelegationDto> DeleteCommand { get; private set; }

        public GetUserDelegationsInput input;

        public ManageUserDelegationsViewModel(
            IUserDelegationAppService appService,
            IDataPagerService dataPager,
            IHostDialogService dialog)
        {
            this.appService = appService;
            this.dataPager = dataPager;
            this.dialog = dialog;
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand<UserDelegationDto>(Delete);

            input = new GetUserDelegationsInput()
            {
                MaxResultCount = 10,
            };
        }

        private async void Delete(UserDelegationDto obj)
        {
            if (await dialog.Question(Local.Localize("UserDelegationDeleteWarningMessage", obj.Username), "ManageUserDelegationsView"))
            {
                await appService.RemoveDelegation(new EntityDto<long>(obj.Id)).WebAsync(GetDelegatedUsers);
            }
        }

        private async void Add()
        {
            var dialogResilt = await dialog.ShowDialogAsync(AppViews.ManageNewUser, null, "ManageUserDelegationsView");
            if (dialogResilt.Result == ButtonResult.OK)
            {
                await GetDelegatedUsers();
            }
        }

        private async Task GetDelegatedUsers()
        {
            await SetBusyAsync(async () =>
            {
                await appService.GetDelegatedUsers(input).WebAsync(dataPager.SetList);
            });
        }

        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            await GetDelegatedUsers();
        }
    }
}
