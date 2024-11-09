using ImTools;
using Prism.Commands;
using Prism.Services.Dialogs;
using Revit.Application.Views.FamilyViews.Public.DialogViews;
using Revit.Categories;
using Revit.Families;
using Revit.Service.Services;
using Revit.Shared;
using Revit.Shared.Entity.Family;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Revit.Application.ViewModels.FamilyViewModels
{
    public partial class FamilyLibraryManagerViewModel : NavigationCurdViewModel
    {

        #region Properties

        private readonly IDialogService dialogService;
        private readonly ICategoryAppService _categoryAppService;

        [ObservableProperty]
        private ObservableCollection<ViewCategoryDto> _categories = new ObservableCollection<ViewCategoryDto>();
        #endregion
        #region Commands
        private DelegateCommand<ViewCategoryDto> _addCategoryCommand;


        public DelegateCommand<ViewCategoryDto> AddCategoryCommand
        {
            get => _addCategoryCommand ?? new DelegateCommand<ViewCategoryDto>(AddCategory);
        }

        private DelegateCommand<ViewCategoryDto> _deleteCategoryCommand;


        public DelegateCommand<ViewCategoryDto> DeleteCategoryCommand
        {
            get => _deleteCategoryCommand ?? new DelegateCommand<ViewCategoryDto>(DeleteCategory);
        }



        #endregion
        #region Methods

        private async void DeleteCategory(ViewCategoryDto category)
        {
            if (MessageBox.Show("是否确定删除该分类，该分类中的所有族将清空分类，需要重新进行归类。", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await SetBusyAsync(async () =>
                {
                    
                });
            }
        }

        private void AddCategory(ViewCategoryDto parent)
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(ViewCategoryDto), parent);
            this.dialogService.ShowDialog(nameof(CreateCatagoryDialogView), parameters, async (dialogResult) =>
            {
                if (dialogResult.Result == ButtonResult.Yes)
                {
                    var resultCategory = dialogResult.Parameters.GetValue<CategoryDto>(nameof(CategoryDto));
                    var createCategory = new CategoryCreateDto() { Name = resultCategory.Name, ParentId = parent?.Id ?? 0 };
                    var result = await _categoryAppService.AddCategory(createCategory);
                    if (result != null)
                    {
                        if (parent != null)
                        {
                            parent.Childs.Add(new ViewCategoryDto(result));
                        }
                        else
                        {

                            Categories.Add(new ViewCategoryDto(result));
                        }
                        System.Windows.MessageBox.Show("添加成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            });
        }



        private async void InitCategories()
        {
            await _categoryAppService.GetListAsync().WebAsync((categories) =>
            {
                var list = categories.GetTreeViewCategories();
                var tags = categories.Items.ToList()/*.Where(x => x.CategoryType != CategoryType.Major)*/;
                Categories =
                    new ObservableCollection<ViewCategoryDto>(new List<ViewCategoryDto> { list });
                return Task.CompletedTask;
            });
        }
        #endregion

        public FamilyLibraryManagerViewModel(IDialogService dialogService, ICategoryAppService categoryWebService)
        {
            this.dialogService = dialogService;
            this._categoryAppService = categoryWebService;
            OnNavigatedTo(null);
            InitCategories();
        }



    }
}
