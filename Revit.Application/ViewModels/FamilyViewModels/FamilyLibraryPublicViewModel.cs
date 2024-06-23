using Autodesk.Revit.DB;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using ImTools;
using Prism.Commands;
using Revit.Application.Commands;
using Revit.Application.UI;
using Revit.Entity;
using Revit.Entity.Entity;
using Revit.Entity.Entity.APIEntity;
using Revit.Entity.Entity.Dtos.Family;
using Revit.Entity.Entity.Parameters;
using Revit.Entity.Interfaces;
using Revit.Service.IServices;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using Tuna.Revit.Extension;

namespace Revit.Application.ViewModels.FamilyViewModels
{
    internal class FamilyLibraryPublicViewModel : ViewModelBase
    {
        #region Commands

        private DelegateCommand _initDataCommand;

        public DelegateCommand InitDataCommand
        {
            get => _initDataCommand ?? new DelegateCommand(InitFamilies);
        }

        private AsyncRelayCommand<FamilyDto> _promptForFamilyInstancePlacementCommand;


        public AsyncRelayCommand<FamilyDto> PromptForFamilyInstancePlacementCommand 
        { get => _promptForFamilyInstancePlacementCommand ?? new AsyncRelayCommand<FamilyDto>(PromptForFamilyInstancePlacement); }

        #endregion

        #region Properties

        private readonly IFamilyFileService familySerivce;

        private static QueryParameter _queryParameter= new QueryParameter() { PageIndex = 2, PageSize = 16, SearchMessage = "''" };

        private ObservableCollection<FamilyDto> _families = new ObservableCollection<FamilyDto>();
        public ObservableCollection<FamilyDto> Families
        {
            get { return _families; }
            set { SetProperty(ref _families, value); }
        }

        #endregion






        public FamilyLibraryPublicViewModel(IDataContext dataContext, IFamilyFileService familySerivce) : base(dataContext)
        {
            this.familySerivce = familySerivce;
        }



        #region CommandMethods

        private async Task PromptForFamilyInstancePlacement(FamilyDto familyDto)
        {
            var familyName = Path.GetFileNameWithoutExtension(familyDto.Name);



            //查验本项目中是否存在同名族，询问是否直接放置或者依旧载入
            Family family = _document.GetElements<Family>().FirstOrDefault(x => x.Name == familyName);

            if (family != null)
            {
                var selectResult = MessageBox.Show("存在同名称族，选择是：使用已有族放置，选择否：依旧重新载入", "提示", MessageBoxButton.YesNo);
                if (selectResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        _uiDocument.PromptForFamilyInstancePlacement(family.GetFamilySymbols().FirstOrDefault());
                        return;
                    }
                    catch (Exception)
                    {
                        return;
                    }
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
        private void LoadFamily( string filePath)
        {
            Family family = null;
            if (File.Exists(filePath))
            {
                _document.NewTransaction(() =>
                {
                    //如果文档内存在相同的族会传输失败
                    var loadResult = _document.LoadFamily(filePath,new FamilyPromptOverLoadOption(), out family);
                    if (!loadResult)
                    {
                        family = _document.GetElements<Family>().FirstOrDefault(x => x.Name == Path.GetFileNameWithoutExtension(filePath));
                    }
                }, "放置族");
                if (family != null)
                {
                    try
                    {
                        _uiDocument.PromptForFamilyInstancePlacement(family.GetFamilySymbols().FirstOrDefault());
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
        ///
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        private async Task<string> DownLoadFamily(FamilyDto familyDto)
        {
            var result = await familySerivce.DownLoadFamily(familyDto.Id);
            string filePath = "";
            if (result.Code == ResponseCode.Success)
            {
                var direction = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
                if (!Directory.Exists(direction))
                {
                    Directory.CreateDirectory(direction);
                }
                filePath = Path.Combine(direction, familyDto.Name);
                using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                {
                    await fileStream.WriteAsync(result.Content, 0, result.Content.Length);
                }
            }

            return filePath;
        }

        private async void InitFamilies()
        {
            var result = await familySerivce.GetAllAsync(_queryParameter);
            if (result != null && result.Code == ResponseCode.Success)
            {
                Families = new ObservableCollection<FamilyDto>(result.Content.Items);
            }
        }
        #endregion

    }
}
