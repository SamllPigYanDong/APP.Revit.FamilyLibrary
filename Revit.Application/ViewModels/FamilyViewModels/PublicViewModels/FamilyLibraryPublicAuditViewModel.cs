using Prism.Commands;
using Prism.Services.Dialogs;
using Revit.Application.Views.FamilyViews.Public.DialogViews;
using Revit.Families;
using Revit.Mvvm.Services;
using Revit.Service.IServices;
using Revit.Shared;
using Revit.Shared.Entity.Family;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    public partial class FamilyLibraryPublicAuditViewModel : NavigationCurdViewModel
    {

        #region Properties
        private readonly IFamilyAppService _familyAppService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private ObservableCollection<FamilyDto> _auditingFamilies = new ObservableCollection<FamilyDto>();

        [ObservableProperty]
        private static FamilyPageRequestDto _queryParameter = new FamilyPageRequestDto() {  Name = "", AuditStatus = FamilyAuditStatus.Auditing };


        [ObservableProperty]
        private ComboboxItems<FamilyAuditStatus> _auditStatusOptions = new ComboboxItems<FamilyAuditStatus>() { Items = new ObservableCollection<FamilyAuditStatus>(new List<FamilyAuditStatus>() { FamilyAuditStatus.Auditing, FamilyAuditStatus.Retry, FamilyAuditStatus.Pass, FamilyAuditStatus.NotPass }) };
        #endregion


        #region Commands

        private DelegateCommand _initDataCommand;

        public DelegateCommand InitDataCommand
        {
            get => _initDataCommand ?? new DelegateCommand(ChangeViewFamilies);
        }


        private DelegateCommand<FamilyDto> _auditFamilyCommand;

        public DelegateCommand<FamilyDto> AuditFamilyCommand
        {
            get => _auditFamilyCommand ?? new DelegateCommand<FamilyDto>(AuditFamily);
        }


        private DelegateCommand _filterAuditingFamiliesCommand;

        public DelegateCommand FilterAuditingFamiliesCommand
        {
            get => _filterAuditingFamiliesCommand ?? new DelegateCommand(FilterAuditingFamilies);
        }
        #endregion

        #region Methods
        /// <summary>
        /// 初始化待审核的族
        /// </summary>
        private async void ChangeViewFamilies()
        {
            await SetBusyAsync(async () =>
            {
                await _familyAppService.GetPageListAsync(_queryParameter).WebAsync(dataPager.SetList);
            });
        }


        private void AuditFamily(FamilyDto selectedFamily)
        {
            var parameters = new DialogParameters { { nameof(FamilyDto), selectedFamily } };
            _dialogService.Show(nameof(AuditingFamilyDialogView), parameters, async (dialogResult) =>
            {
                if (dialogResult.Result != ButtonResult.OK) return;
                var familyPutDto = dialogResult.Parameters.GetValue<FamilyPutDto>(nameof(FamilyPutDto));
                await _familyAppService.AuditingPublicAsync(familyPutDto).WebAsync(async () => { ChangeViewFamilies(); });
            });
        }
        #endregion




        private void FilterAuditingFamilies()
        {
            ChangeViewFamilies();
        }

       



        public FamilyLibraryPublicAuditViewModel( IFamilyAppService familyAppService, IDialogService dialogService) 
        {
            _familyAppService = familyAppService;
            _dialogService = dialogService;
        }


    }
}
