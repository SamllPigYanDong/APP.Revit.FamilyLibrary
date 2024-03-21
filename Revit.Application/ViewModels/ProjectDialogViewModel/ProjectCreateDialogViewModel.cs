using Prism.Services.Dialogs;
using Revit.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Application.ViewModels.ProjectDialogViewModel
{
    public class ProjectCreateDialogViewModel : ViewModelBase, IDialogAware
    {
        public ProjectCreateDialogViewModel(IDataContext dataContext) : base(dataContext)
        {
        }

        public string Title => "项目创建窗口";

#pragma warning disable CS0067 // 从不使用事件“ProjectCreateDialogViewModel.RequestClose”
        public event Action<IDialogResult> RequestClose;
#pragma warning restore CS0067 // 从不使用事件“ProjectCreateDialogViewModel.RequestClose”

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
