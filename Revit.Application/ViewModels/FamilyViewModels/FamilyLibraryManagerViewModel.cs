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

namespace Revit.Application.ViewModels.FamilyViewModels
{
    public class FamilyLibraryManagerViewModel : NavigationCurdViewModel
    {

        #region Properties

        private readonly IDialogService dialogService;
        private readonly ICategoryAppService categoryAppService;
        private ObservableCollection<ViewCategoryDto> _categories = new ObservableCollection<ViewCategoryDto>();
        public ObservableCollection<ViewCategoryDto> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }


        #endregion


        //新增类别

        //修改类别

        //删除类别
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
                var result = await categoryAppService.DeleteCategory(category.Id);
                if (result > 0)
                {
                    var parent = categoryAppService.GetCategories(Categories.FirstOrDefault()).FindFirst(x => x.Id == category.ParentId);
                    if (parent != null)
                    {
                        parent.Childs.Remove(category);
                    }
                    MessageBox.Show("删除成功。", "提示", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("删除失败，请稍后再试。", "提示", MessageBoxButton.OK);
                }
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
                    var createCategory = new CategoryCreateDto() { Name = resultCategory.Name, ParentId = parent == null ? 0 : parent.Id };
                    var result = await categoryAppService.AddCategory(createCategory);
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

        private async void Initial()
        {
            await SetBusyAsync(async () =>
             {
                 var result = await categoryAppService.GetAllCategories().WebAsync(successCallback: (result) =>
                 {
                     Categories = new ObservableCollection<ViewCategoryDto>(new List<ViewCategoryDto>() { result });
                     return Task.CompletedTask;
                 }); ;

             });
        }

        private ViewCategoryDto GroupCategories(ViewCategoryDto root, IEnumerable<ViewCategoryDto> categories)
        {
            root.Childs.AddRange(categories.Where(x => x.ParentId == root.Id));
            if (root.Childs != null)
            {
                foreach (var child in root.Childs)
                {
                    GroupCategories(child, categories);
                }
            }
            return root;
        }
        #endregion

        public FamilyLibraryManagerViewModel(IDialogService dialogService, ICategoryAppService categoryWebService)
        {
            this.dialogService = dialogService;
            this.categoryAppService = categoryWebService;
            Initial();
        }



    }
}
