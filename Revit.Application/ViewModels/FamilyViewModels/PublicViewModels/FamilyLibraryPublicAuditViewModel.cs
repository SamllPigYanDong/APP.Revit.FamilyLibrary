using Prism.Commands;
using Prism.Services.Dialogs;
using Revit.Application.Views.FamilyViews.Public.DialogViews;
using Revit.Families;
using Revit.Service.IServices;
using Revit.Shared;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Family;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    public class FamilyLibraryPublicAuditViewModel : ViewModelBase
    {

        #region Properties
        private readonly IFamilyService _familySerivce;
        private readonly IFamilyAppService _webFamilyService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<FamilyDto> _auditingFamilies = new ObservableCollection<FamilyDto>();

        public ObservableCollection<FamilyDto> AuditingFamilies
        {
            get { return _auditingFamilies; }
            set { SetProperty(ref _auditingFamilies, value); }
        }


        private static FamilyPageRequestDto _queryParameter = new FamilyPageRequestDto() { PageIndex = 1, PageSize = 10, SearchMessage = "", AuditStatus = FamilyAuditStatus.Auditing };
        public FamilyPageRequestDto QueryParameter
        {
            get { return _queryParameter; }
            set { SetProperty(ref _queryParameter, value); }
        }

        private IPagedList<FamilyDto> _pagedList;
        public IPagedList<FamilyDto> PagedList
        {
            get { return _pagedList; }
            set { SetProperty(ref _pagedList, value); }
        }

        private ComboboxItems<FamilyAuditStatus> _auditStatusOptions = new ComboboxItems<FamilyAuditStatus>() { Items = new ObservableCollection<FamilyAuditStatus>(new List<FamilyAuditStatus>() { FamilyAuditStatus.Auditing, FamilyAuditStatus.Retry, FamilyAuditStatus.Pass, FamilyAuditStatus.NotPass }) };

        public ComboboxItems<FamilyAuditStatus> AuditStatusOptions
        {
            get { return _auditStatusOptions; }
            set { _auditStatusOptions = value; }
        }



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
            await _familySerivce.LoadFamilies(_queryParameter, (result) =>
            {
                if (result != null && result.Items != null)
                {
                    PagedList = result;
                    AuditingFamilies = new ObservableCollection<FamilyDto>(result.Items);
                }
                else
                {
                    PagedList = new PagedList<FamilyDto>();
                    AuditingFamilies = new ObservableCollection<FamilyDto>();
                }
            });
        }


        private void AuditFamily(FamilyDto selectedFamily)
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(FamilyDto), selectedFamily);
            _dialogService.ShowDialog(nameof(AuditingFamilyDialogView), parameters, async (dialogResult) =>
            {
                if (dialogResult.Result == ButtonResult.OK)
                {
                    var familyPutDto = dialogResult.Parameters.GetValue<FamilyPutDto>(nameof(FamilyPutDto));
                    await _webFamilyService.AuditingPublicAsync(familyPutDto).WebAsync(async () => { ChangeViewFamilies(); });
                }
            });
        }
        #endregion




        private void FilterAuditingFamilies()
        {
            ChangeViewFamilies();
        }






        public FamilyLibraryPublicAuditViewModel( IFamilyService familySerivce, IFamilyAppService webFamilyService, IDialogService dialogService) 
        {
            _familySerivce = familySerivce;
            _webFamilyService = webFamilyService;
            _dialogService = dialogService;
        }


    }
}
