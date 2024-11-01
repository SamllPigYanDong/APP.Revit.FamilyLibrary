using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Prism.Regions;
using Revit.Categories;
using Revit.Families;
using Revit.Mvvm.Extensions;
using Revit.Mvvm.Interface;
using Revit.Service.IServices;
using Revit.Service.Services;
using Revit.Shared;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Family;
using Revit.Shared.Extensions.Threading;
using Revit.Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Tuna.Revit.Extension;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    internal class FamilyLibraryPublicViewModel : NavigationCurdViewModel
    {
        #region Commands



        private AsyncRelayCommand<FamilyDto> _promptForFamilyInstancePlacementCommand;

        public AsyncRelayCommand<FamilyDto> PromptForFamilyInstancePlacementCommand
        { get => _promptForFamilyInstancePlacementCommand ?? new AsyncRelayCommand<FamilyDto>(PromptForFamilyInstancePlacement); }


        private AsyncRelayCommand<ViewCategoryDto> _categorySelectionChangedCommand;

        public AsyncRelayCommand<ViewCategoryDto> CategorySelectionChangedCommand
        { get => _categorySelectionChangedCommand ?? new AsyncRelayCommand<ViewCategoryDto>(CategorySelectionChanged); }


        #endregion

        #region Properties
        private readonly IFamilyAppService familyAppSerivce;
        private readonly ICategoryAppService categoryAppService;
        private readonly IDataContext dataContext;

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

        public FamilyLibraryPublicViewModel(IFamilyAppService familyAppSerivce, ICategoryAppService categoryAppService, IDataContext dataContext)
        {
            this.familyAppSerivce = familyAppSerivce;
            this.categoryAppService = categoryAppService;
            this.dataContext = dataContext;
        }

        #region CommandMethods
        /// <summary>
        /// 搜索
        /// </summary>
        protected async void Search()
        {
            QueryParameter.SetSearchMessage();
            LoadFamilies(QueryParameter);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        protected async void ChangeNextPage()
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
        protected void ChangePreviousPage()
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
            await familyAppSerivce.GetPageListAsync(queryParameter).WebAsync(successCallback: async (result) =>
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



        public override async Task OnNavigatedToAsync(NavigationContext navigationContext = null)
        {
            LoadFamilies(_queryParameter);
            var categoryRoot = await categoryAppService.GetTreeViewCategories();
            Categories = new ObservableCollection<ViewCategoryDto>(new List<ViewCategoryDto>() { categoryRoot });
            CategoriesTags = new ObservableCollection<ViewCategoryDto>(categoryAppService.GetCategories(categoryRoot));
        }




        private async Task CategorySelectionChanged(ViewCategoryDto category)
        {
            var ids = categoryAppService.GetCategoryChildIds(category);
            QueryParameter.CategoriesIds = new List<long>(ids);
            LoadFamilies(QueryParameter);
        }

        /// <summary>
        /// 放置族
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        public async Task PromptForFamilyInstancePlacement(FamilyDto familyDto)
        {
            var familyName = Path.GetFileNameWithoutExtension(familyDto.Name);
            //查验本项目中是否存在同名族，询问是否直接放置或者依旧载入
            Family family = dataContext.Document.GetElements<Family>().FirstOrDefault(x => x.Name == familyName);

            if (family == null)
            {
                return;
            }
            var selectResult = MessageBox.Show("存在同名称族，选择是：使用已有族放置，选择否：依旧重新载入", "提示", MessageBoxButtons.YesNo);
            if (selectResult == DialogResult.Yes)
            {
                try
                {
                    dataContext.GetUIDocument().PromptForFamilyInstancePlacement(family.GetFamilySymbols().FirstOrDefault());
                    return;
                }
                catch (Exception)
                {
                    return;
                }
            }
            //下载族
            string filePath = await DownLoadFamily(familyDto);
            //载入族
            LoadFamily(filePath);
        }

        /// <summary>
        /// 载入族
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadFamily(string filePath)
        {
            Family family = null;
            if (File.Exists(filePath))
            {
                dataContext.Document.NewTransaction(() =>
                {
                    //如果文档内存在相同的族会传输失败
                    var loadResult = dataContext.Document.LoadFamily(filePath, new FamilyPromptOverLoadOption(), out family);
                    if (!loadResult)
                    {
                        family = dataContext.Document.GetElements<Family>().FirstOrDefault(x => x.Name == Path.GetFileNameWithoutExtension(filePath));
                    }
                }, "放置族");
                if (family != null)
                {
                    try
                    {
                        dataContext.GetUIDocument().PromptForFamilyInstancePlacement(family.GetFamilySymbols().FirstOrDefault());
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("载入失败", "提示");
                }
            }
            else
            {
                MessageBox.Show("载入失败", "提示");
            }
        }


        /// <summary>
        /// 下载族
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        private async Task<string> DownLoadFamily(FamilyDto familyDto)
        {
            //var result = await _familyAppSerivce.DownLoadFamily(familyDto.Id);
            //string filePath = "";
            //if (result.Code == ResponseCode.Success)
            //{
            //    var direction = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            //    if (!Directory.Exists(direction))
            //    {
            //        Directory.CreateDirectory(direction);
            //    }
            //    filePath = Path.Combine(direction, familyDto.Name);
            //    using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            //    {
            //        await fileStream.WriteAsync(result.Content, 0, result.Content.Length);
            //    }
            //}

            return "filePath";
            //return filePath;
        }

        #endregion

    }
}
