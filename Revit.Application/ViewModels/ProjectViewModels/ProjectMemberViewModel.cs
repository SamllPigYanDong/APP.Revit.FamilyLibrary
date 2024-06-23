using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity;
using Revit.Entity.Interfaces;
using Revit.Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Revit.Service.IServices;
using Prism.Commands;
using Revit.Entity;
using Autodesk.Revit.DB;
using System.Windows;

namespace Revit.Application.ViewModels.ProjectViewModels
{
    public class ProjectMemberViewModel : ViewModelBase
    {
        private readonly IProjectService projectService;

        #region Parameters
        private ObservableCollection<UserDto> _projectUsers = new ObservableCollection<UserDto>();
        public ObservableCollection<UserDto> ProjectUsers
        {
            get { return _projectUsers; }
            set { SetProperty(ref _projectUsers, value); }
        }
        #endregion
        #region Commands
        private DelegateCommand<UserDto> _deleteUserCommand;

        public DelegateCommand<UserDto> DeleteUserCommand { get => _deleteUserCommand ?? new DelegateCommand<UserDto>(DeleteUser); }


        #endregion




        public ProjectMemberViewModel(IDataContext dataContext, IProjectService projectService) : base(dataContext)
        {
            this.projectService = projectService;
            InitProjectUsers();
        }

        private async void InitProjectUsers()
        {
            var result = await projectService.GetUsers(15);
            if (result != null && result.Code == ResponseCode.Success)
            {
                ProjectUsers = new ObservableCollection<UserDto>(result.Content);
            }
        }

        private async void DeleteUser(UserDto dto)
        {
            var dialogResult = MessageBox.Show("是否确认删除该品？", "提示", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                var result = await projectService.DeleteUser(13, dto.Id);
                if (result.Code == ResponseCode.Success)
                {
                    MessageBox.Show("成功删除", "提示");
                }
                else
                {
                    MessageBox.Show("无法删除最后一个成员", "提示");
                }
            }
        }

    }
}
