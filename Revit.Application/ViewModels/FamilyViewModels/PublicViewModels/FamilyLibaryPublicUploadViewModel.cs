using Abp.Extensions;
using Prism.Commands;
using Revit.Application.Converter;
using Revit.Entity;
using Revit.Entity.Entity;
using Revit.Entity.Interfaces;
using Revit.Families;
using Revit.Mvvm.Extensions;
using Revit.Service.IServices;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Family;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    //补充获取历史上传的族的方法
    internal class FamilyLibaryPublicUploadViewModel : RevitViewModelBase
    {
        #region Properties
        private IFamilyAppService _familySerivce;

        private ObservableCollection<FamilyDto> _uploadFamilies = new ObservableCollection<FamilyDto>();

        public ObservableCollection<FamilyDto> UploadFamilies
        {
            get { return _uploadFamilies; }
            set { SetProperty(ref _uploadFamilies, value); }
        }


        private static FamilyPageRequestDto _queryParameter = new FamilyPageRequestDto() { PageIndex = 1, PageSize = 8, SearchMessage = "", AuditStatus = FamilyAuditStatus.Auditing };
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


        #region Commands
        private DelegateCommand _uplaodFamilyFilesCommand;

        public DelegateCommand UplaodFamilyFilesCommand
        {
            get => _uplaodFamilyFilesCommand ?? new DelegateCommand(UplaodFamilyFiles);
        }

        private DelegateCommand _getUploadedFamiliesCommand;

        public DelegateCommand GetUploadedFamiliesCommand
        {
            get => _getUploadedFamiliesCommand ?? new DelegateCommand(GetUploadedFamilies);
        }


      


        #endregion

        #region Methods
        private async void UplaodFamilyFiles()
        {
            FileExtension.SelectFile(
                (dialog) =>
                {
                    dialog.Filter = "族文件 (*.rfa)|*.rfa";
                    dialog.Title = "选择一个文件";
                },
                async (dialog) =>
                {
                    var filePath = dialog.FileName;
                    var imagePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(filePath) + ".jpg");
                    FamilyImageExtension.GetImage(filePath, imagePath);
                    var result = await _familySerivce.UploadPublicAsync(Global.User.UserId, new UploadFileDtoBase() { FilesPath = new List<string>() { filePath, imagePath } });
                    if (result != null && result.Any())
                    {
                        UploadFamilies.AddRange(result);
                        RaisePropertyChanged(nameof(UploadFamilies));
                    }
                });
        }

        private async void GetUploadedFamilies()
        {
            await _familySerivce.GetPageListAsync(QueryParameter).WebAsync(successCallback: async (result) =>
            {
                if (result != null )
                {
                    PagedList = result;
                    UploadFamilies = new ObservableCollection<FamilyDto>(result.Items.OrderBy(x => x.CreationTime));
                }
            });

        }

        #endregion


        public FamilyLibaryPublicUploadViewModel(IDataContext dataContext, IFamilyAppService familySerivce) : base(dataContext)
        {
            _familySerivce = familySerivce;
        }


    }
}
