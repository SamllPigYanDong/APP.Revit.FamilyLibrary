using CommunityToolkit.Mvvm.Input;
using Prism.Regions;
using Revit.Categories;
using Revit.Families;
using Revit.Shared;
using Revit.Shared.Entity.Family;
using Revit.Shared.Extensions.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Abp.Application.Services.Dto;
using CommunityToolkit.Mvvm.ComponentModel;
using Revit.Commons;
using Revit.Mvvm.Services;
using Revit.Service.Services;
using Revit.Shared.Services.Datapager;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    public partial class FamilyLibraryPublicViewModel : NavigationCurdViewModel
    {
        #region Properties
        private readonly IFamilyAppService _familyAppService;
        private readonly IFamilyService _familyService;
        private readonly ICategoryAppService _categoryAppService;

      


        [ObservableProperty]
        private ObservableCollection<ViewCategoryDto> _categories = new ObservableCollection<ViewCategoryDto>();

        [ObservableProperty]
        private ObservableCollection<ViewCategoryDto> _tages = new ObservableCollection<ViewCategoryDto>();

        [ObservableProperty]
        private FamilyPageRequestDto _queryParameter = new FamilyPageRequestDto() { Name = "", AuditStatus = FamilyAuditStatus.Pass};
        #endregion

        public FamilyLibraryPublicViewModel(IFamilyAppService familyAppService, IFamilyService familyService, ICategoryAppService categoryAppService)
        {
            _familyAppService = familyAppService;
            _familyService = familyService;
            _categoryAppService = categoryAppService;
            dataPager.OnPageIndexChangedEventhandler += DataPager_OnPageIndexChangedEventhandler;
            OnNavigatedToAsync();
            InitCategories();
        }

        private void DataPager_OnPageIndexChangedEventhandler(object sender, PageIndexChangedEventArgs e)
        {
            _queryParameter.SkipCount = e.SkipCount;
            _queryParameter.MaxResultCount = e.PageSize;
            OnNavigatedToAsync();
        }
        #region CommandMethods
        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            await SetBusyAsync(async () =>
            {
                await _familyAppService.GetPageListAsync(_queryParameter).WebAsync(dataPager.SetList);
            });
        }

        private async void InitCategories()
        {
            await _categoryAppService.GetListAsync().WebAsync((categories) =>
            {
                var list = categories.GetTreeViewCategories();
                var tags = categories.Items.ToList().Where(x => x.CategoryType != CategoryType.Major);
                Tages = new ObservableCollection<ViewCategoryDto>(Map<List<ViewCategoryDto>>(tags));
                Categories =
                    new ObservableCollection<ViewCategoryDto>(new List<ViewCategoryDto> { list });
                return Task.CompletedTask;
            });
        }

        [RelayCommand]
        private void CategorySelectionChanged(ViewCategoryDto category)
        {
            if (category == null) return;
            var ids = category.GetNodeCategoriesIds();
            _queryParameter.CategoriesIds = new List<long>(ids);
            _queryParameter.SkipCount = 0;
            OnNavigatedToAsync();
        }

        [RelayCommand]
        public async void Search()
        {
            await OnNavigatedToAsync();
        }

        [RelayCommand]
        public async void PromptForFamilyInstancePlacement()
        {
            if (dataPager.SelectedItem is not FamilyDto familydto) return;
            if (_familyService.PromptForFamilyInstancePlacement(familydto)) return;
            //下载族后加载
            _familyAppService.DownLoadFamily(familydto.Id).WebAsync(async (bytes) =>
            {
                if (await _familyService.LoadFamily(bytes))
                {
                    //弹窗信息
                    MessageBox.Show("载入出错");
                }
            });
            _familyService.PromptForFamilyInstancePlacement(familydto);
        }
        #endregion

    }
}
