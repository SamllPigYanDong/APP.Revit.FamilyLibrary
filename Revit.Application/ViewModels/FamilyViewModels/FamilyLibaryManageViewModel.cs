using Prism.Commands;
using Revit.Application.UI;
using Revit.Entity;
using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos.Family;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Interfaces;
using Revit.Extension;
using Revit.Mvvm.Extensions;
using Revit.Service.IServices;
using Revit.Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace Revit.Application.ViewModels.FamilyViewModels
{
    internal class FamilyLibaryManageViewModel : ViewModelBase
    {

        private ObservableCollection<FamilyDto> _uploadFamilies=new ObservableCollection<FamilyDto>();

        public ObservableCollection<FamilyDto> UploadFamilies
        {
            get { return _uploadFamilies; }
            set { _uploadFamilies = value; }
        }


        private IFamilyFileService _familySerivce;

        private DelegateCommand _uplaodFamilyFilesCommand;

        public DelegateCommand UplaodFamilyFilesCommand
        {
            get => _uplaodFamilyFilesCommand ?? new DelegateCommand(UplaodFamilyFiles);
        }

        private async void UplaodFamilyFiles()
        {
            FileExtension.SelectFile(
                (dialog) => {
                    dialog.Filter = "族文件 (*.rfa)|*.rfa";
                    dialog.Title = "选择一个文件";
                },
                async (dialog) =>
                {
                    var filePath = dialog.FileName;
                    var imagePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(filePath) + ".jpg");
                    FamilyImageExtension.GetImage(filePath, imagePath);
                    var result=await _familySerivce.UploadPublicAsync(new UploadFileDtoBase() { Files = new List<string>() { filePath, imagePath } });
                    UploadFamilies.AddRange(result.Content);
                    RaisePropertyChanged(nameof(UploadFamilies));
                });
        }

        public FamilyLibaryManageViewModel(IDataContext dataContext, IFamilyFileService familySerivce) : base(dataContext)
        {
            this._familySerivce = familySerivce;
        }


    }
}
