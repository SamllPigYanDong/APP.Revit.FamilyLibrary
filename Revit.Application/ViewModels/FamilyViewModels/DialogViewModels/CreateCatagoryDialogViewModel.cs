using System;
using Prism.Services.Dialogs;
using Prism.Commands;
using System.Windows;
using System.Collections.ObjectModel;
using Revit.Shared.Entity.Family;
using Revit.Shared.Models;
using Revit.Shared;
using System.Collections.Generic;

namespace Revit.Application.ViewModels.FamilyViewModels.DialogViewModels
{
    public class CreateCatagoryDialogViewModel : ViewModelBase, IDialogAware
    {

        #region Parameters
        public event Action<IDialogResult> RequestClose;


        private CategoryDto _editingCategory;

        public CategoryDto EditingCategory
        {
            get { return _editingCategory; }
            set { SetProperty(ref _editingCategory, value); }
        }

        private CategoryDto _parentCategory;

        public CategoryDto ParentCategory
        {
            get { return _parentCategory; }
            set { SetProperty(ref _parentCategory, value); }
        }


        private ComboboxItems<CategoryType> _categoryTypeOptions = new ComboboxItems<CategoryType>() { Items = new ObservableCollection<CategoryType>(new List<CategoryType>() { CategoryType.ElementType, CategoryType.Software, CategoryType.Property, CategoryType.Major, CategoryType.Keyword }) };

        public ComboboxItems<CategoryType> CategoryTypeOptions
        {
            get { return _categoryTypeOptions; }
            set { _categoryTypeOptions = value; }
        }


        public string Title => "添加类别窗口";
        #endregion

        #region Commands
        private DelegateCommand _submitCommand;

        public DelegateCommand SubmitCommand
        {
            get => _submitCommand ?? new DelegateCommand(Submit);
        }


        #endregion


        #region Methods

        private void Submit()
        {
            if (string.IsNullOrWhiteSpace(EditingCategory.Name))
            {
                MessageBox.Show("提示:请输入需要创建的类别名称!", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var buttonResult = ButtonResult.Yes;
            var parameters = new DialogParameters();
            parameters.Add(nameof(CategoryDto), EditingCategory);
            var dialogResult = new DialogResult(buttonResult, parameters);
            RequestClose.Invoke(dialogResult);
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
            ParentCategory = parameters.GetValue<CategoryDto>(nameof(CategoryDto));

            EditingCategory = new CategoryDto() { ParentId = ParentCategory == null ? 0 : ParentCategory.Id, Name = "" };
        }
        #endregion


        public CreateCatagoryDialogViewModel() : base()
        {
        }
    }
}
