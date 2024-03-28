using Prism.Commands;
using Prism.Services.Dialogs;
using Revit.Entity;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Revit.Application.ViewModels.ProjectViewModels.ProjectDialogViewModel
{
    public class ProjectCreateDialogViewModel : ViewModelBase, IDialogAware
    {

        private ProjectCreateDto _projectDto=new ProjectCreateDto() { CreatorId=Global.User.UserId};

        public ProjectCreateDto ProjectDto
        {
            get { return _projectDto; }
            set { SetProperty(ref _projectDto, value); }
        }


        private DelegateCommand<string> _closeDialogCommand;
        public DelegateCommand<string> CloseDialogCommand { get => _closeDialogCommand ?? new DelegateCommand<string>(CloseDialog); }

        private DelegateCommand _selecteProjectIconCommand;
        public DelegateCommand SelecteProjectIconCommand { get => _selecteProjectIconCommand ?? new DelegateCommand(SelecteProjectIcon); }

        private  void SelecteProjectIcon()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //获取选择的文件名
                    ProjectDto.Icon = openFileDialog.FileName;
                }
            }
        }

        public ProjectCreateDialogViewModel(IDataContext dataContext) : base(dataContext)
        {
        }

        public string Title => "项目创建窗口";

        public event Action<IDialogResult> RequestClose;

        public  void CloseDialog(string result)
        {
            ButtonResult buttonResult = ButtonResult.None;
            if (result=="True")
            {
                buttonResult = ButtonResult.OK;
            }
            else
            {
                buttonResult = ButtonResult.Cancel;
            }
            var dialogResult = new Prism.Services.Dialogs.DialogResult(buttonResult);
            dialogResult.Parameters.Add("createProject", ProjectDto);
            RaiseRequestClose(dialogResult);
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
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
        }
    }
}
