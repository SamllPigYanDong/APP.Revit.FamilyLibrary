using Abp.Extensions;
using Prism.Commands;
using Revit.Application.Converter;
using Revit.Entity;
using Revit.Families;
using Revit.Mvvm.Extensions;
using Revit.Mvvm.Interface;
using Revit.Service.IServices;
using Revit.Shared.Base;
using Revit.Shared.Entity.Commons;
using Revit.Shared.Entity.Family;
using Revit.Shared.Extensions.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Revit.Shared;

namespace Revit.Application.ViewModels.FamilyViewModels.PublicViewModels
{
    //补充获取历史上传的族的方法
    public partial class FamilyLibraryPublicUploadViewModel : NavigationCurdViewModel
    {
        #region Properties
        private readonly IFamilyAppService _familyService;

        [ObservableProperty]
        private static FamilyPageRequestDto _queryParameter = new FamilyPageRequestDto() { Name = "", AuditStatus = FamilyAuditStatus.Auditing };
        #endregion


        #region Commands
        private DelegateCommand _uplaodFamilyFilesCommand;

        public DelegateCommand UplaodFamilyFilesCommand
        {
            get => _uplaodFamilyFilesCommand ?? new DelegateCommand(UploadFamilyFiles);
        }

        private DelegateCommand _getUploadedFamiliesCommand;

        public DelegateCommand GetUploadedFamiliesCommand
        {
            get => _getUploadedFamiliesCommand ?? new DelegateCommand(GetUploadedFamilies);
        }





        #endregion

        #region Methods
        private async void UploadFamilyFiles()
        {
            var dialog = FileExtension.SelectFile(
                 (dialog) =>
                 {
                     dialog.Filter = "族文件 (*.rfa)|*.rfa";
                     dialog.Title = "选择一个文件";
                 });
            var filePath = dialog.FileName;
            var imagePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(filePath) + ".jpg");
            FamilyImageExtension.GetImage(filePath, imagePath);
            var dto = new UploadFileDtoBase() { FilesPath = new List<string>() { filePath, imagePath } };
            await _familyService.UploadPublicAsync(Global.User.UserId, dto).WebAsync(dataPager.SetList);
        }

        private async void GetUploadedFamilies()
        {
            await SetBusyAsync(async () =>
            {
                await _familyService.GetPageListAsync(QueryParameter).WebAsync(dataPager.SetList);
            });
        }

        #endregion


        public FamilyLibraryPublicUploadViewModel(IFamilyAppService familyService) 
        {
            _familyService = familyService;
        }


    }
}
