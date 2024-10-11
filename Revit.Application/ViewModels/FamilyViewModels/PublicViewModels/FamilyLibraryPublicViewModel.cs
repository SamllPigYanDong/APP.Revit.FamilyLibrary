using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Revit.Families;
using Revit.Service.IServices;
using Revit.Service.Services;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Family;
using Revit.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    internal class FamilyLibraryPublicViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _initDataCommand;

        public DelegateCommand InitDataCommand
        {
            get => _initDataCommand ?? new DelegateCommand(Initial);
        }

        private AsyncRelayCommand<FamilyDto> _promptForFamilyInstancePlacementCommand;

        public AsyncRelayCommand<FamilyDto> PromptForFamilyInstancePlacementCommand
        { get => _promptForFamilyInstancePlacementCommand ?? new AsyncRelayCommand<FamilyDto>(PromptForFamilyInstancePlacement); }


        private AsyncRelayCommand<ViewCategoryDto> _categorySelectionChangedCommand;

        public AsyncRelayCommand<ViewCategoryDto> CategorySelectionChangedCommand
        { get => _categorySelectionChangedCommand ?? new AsyncRelayCommand<ViewCategoryDto>(CategorySelectionChanged); }


        #endregion

        #region Properties
        private readonly IFamilyService _familySerivce;
        private readonly ICategoryService categoryService;

        /// <summary>
        /// 树形分类
        /// </summary>
        private ObservableCollection<ViewCategoryDto> _categoriestag = new ObservableCollection<ViewCategoryDto>();
        public ObservableCollection<ViewCategoryDto> CategoriesTags
        {
            get { return _categoriestag; }
            set { SetProperty(ref _categoriestag, value); }
        }


        private ObservableCollection<ViewCategoryDto> _categories = new ObservableCollection<ViewCategoryDto>();
        public ObservableCollection<ViewCategoryDto> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        private ObservableCollection<FamilyDto> _families = new ObservableCollection<FamilyDto>();
        public ObservableCollection<FamilyDto> Families
        {
            get { return _families; }
            set { SetProperty(ref _families, value); }
        }

        private static FamilyPageRequestDto _queryParameter = new FamilyPageRequestDto() { PageIndex = 1, PageSize = 8, SearchMessage = "", AuditStatus = FamilyAuditStatus.Pass };
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
        #endregion

        public FamilyLibraryPublicViewModel(IFamilyService familySerivce, ICategoryService categoryService)
        {
            _familySerivce = familySerivce;
            this.categoryService = categoryService;
        }

        #region CommandMethods
        /// <summary>
        /// 搜索
        /// </summary>
        protected override async void Search()
        {
            QueryParameter.SetSearchMessage();
            LoadFamilies(QueryParameter);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        protected override async void ChangeNextPage()
        {
            if (!PagedList.HasNextPage)
            {
                return;
            }
            QueryParameter.Next();
            LoadFamilies(QueryParameter);
        }

        /// <summary>
        /// 翻上一页
        /// </summary>
        protected override void ChangePreviousPage()
        {
            if (!PagedList.HasPreviousPage)
            {
                return;
            }
            QueryParameter.Previous();
            LoadFamilies(QueryParameter);
        }

        /// <summary>
        /// 加载族统一方法
        /// </summary>
        /// <param name="queryParameter"></param>
        private async void LoadFamilies(FamilyPageRequestDto queryParameter)
        {
            await _familySerivce.LoadFamilies(queryParameter, (result) =>
             {
                 var list = new List<FamilyDto>();
                 if (result.Items != null)
                 {
                     list = result.Items.ToList();
                 }
                 Families = new ObservableCollection<FamilyDto>(list);
                 PagedList = result;
             });
        }

        /// <summary>
        /// 初始化族列表
        /// </summary>
        private async void Initial()
        {
            LoadFamilies(_queryParameter);
            var categoryRoot = await categoryService.GetTreeViewCategories();
            Categories = new ObservableCollection<ViewCategoryDto>(new List<ViewCategoryDto>() { categoryRoot });
            CategoriesTags = new ObservableCollection<ViewCategoryDto>(categoryService.GetCategories(categoryRoot));
        }

        /// <summary>
        /// 布置族功能
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        private async Task PromptForFamilyInstancePlacement(FamilyDto familyDto)
        {
            await _familySerivce.PromptForFamilyInstancePlacement(familyDto);
        }


        private async Task CategorySelectionChanged(ViewCategoryDto category)
        {
            var ids = categoryService.GetCategoryChildIds(category);
            QueryParameter.CategoriesIds = new List<long>(ids);
            LoadFamilies(QueryParameter);
        }

        #endregion

    }
}
