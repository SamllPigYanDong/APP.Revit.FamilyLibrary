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

        private void Submit(string message)
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
                default: throw new ArgumentException("this  message is invaild");
            }
            var parameters = new DialogParameters();
            var categoryIds = categoryService.GetCategories(Categories.FirstOrDefault()).Where(x => x.IsChecked).Select(x => x.Id).ToList();
            var result = new FamilyPutDto()
            {
                Id = AuditingFamily.Id,
                CategoriesIds = categoryIds,
                CreationTime = AuditingFamily.CreationTime,
                CreatorId = AuditingFamily.CreatorId,
                FamilyAuditStatus = audit,
                LastModificationTime = AuditingFamily.LastModificationTime
            };
            parameters.Add(nameof(FamilyPutDto), result);
            RequestClose.Invoke(new DialogResult(ButtonResult.OK, parameters));
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
            Initial();
        }

        private async void Initial()
        {
            var result = await categoryService.GetTreeViewCategories();
            Categories = new ObservableCollection<ViewCategoryDto>(new List<ViewCategoryDto>() { result });
        }
        #endregion


        public AuditingFamilyDialogViewModel( ICategoryAppService categoryAppService) 
        {
            this.categoryAppService = categoryAppService;
        }
    }
}
