using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Services.Dialogs;
using Prism.Commands;
using System.Collections.ObjectModel;
using Revit.Shared.Entity.Family;
using Revit.Families;
using Revit.Shared.Models;
using Revit.Service.Services;
using Revit.Shared;
using Revit.Categories;
using Revit.Shared.Extensions.Threading;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels.DialogViewModels
{
    public class AuditingFamilyDialogViewModel : ViewModelBase, IDialogAware
    {

        #region Parameters

        public event Action<IDialogResult> RequestClose;

        private ObservableCollection<ViewCategoryDto> _categories = new ObservableCollection<ViewCategoryDto>();
        public ObservableCollection<ViewCategoryDto> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private FamilyDto _auditingFamily;

        public FamilyDto AuditingFamily
        {
            get { return _auditingFamily; }
            set { SetProperty(ref _auditingFamily, value); }
        }

        public string Title => "审核族窗口";
        #endregion

        #region Commands
        private DelegateCommand<string> _submitCommand;
        private ICategoryAppService categoryAppService;

        public DelegateCommand<string> SubmitCommand
        {
            get => _submitCommand ?? new DelegateCommand<string>(Submit);
        }


        #endregion


        #region Methods

        private async void Submit(string message)
        {
            var audit = AuditingFamily.FamilyAuditStatus;
            switch (message)
            {
                case "通过":
                    audit=FamilyAuditStatus.Pass;
                    break;
                case "返回修改":
                    audit = FamilyAuditStatus.Retry;
                    break;
                case "拒绝通过":
                    audit = FamilyAuditStatus.NotPass;
                    break;
                default: throw new ArgumentException("this  message is invalid");
            }
            var parameters = new DialogParameters();
           
           
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            AuditingFamily = parameters.GetValue<FamilyDto>(nameof(FamilyDto));
        }

      
        #endregion


        public AuditingFamilyDialogViewModel( ICategoryAppService categoryAppService) 
        {
            this.categoryAppService = categoryAppService;
        }
    }
}
