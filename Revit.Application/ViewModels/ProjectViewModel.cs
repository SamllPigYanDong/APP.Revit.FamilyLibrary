using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Revit.Application.UI;
using Revit.Service.Services;
using System;
using System.Collections.ObjectModel;
using Revit.Service.IServices;
using System.Windows;
using System.Windows.Data;
using Revit.Entity.Entity.Parameters;
using Revit.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Prism.Services.Dialogs;
using Prism.Commands;
using Revit.Entity.Entity.Dtos.Project;

namespace Revit.Application.ViewModels
{
    public  class ProjectViewModel : ObservableRecipient
    {
        private readonly IProjectService _projectService;
        private readonly IDialogService dialogService;
        public ObservableCollection<ProjectDto> _recentlyUserProjects = new ObservableCollection<ProjectDto>();
        public ObservableCollection<ProjectDto> RecentlyUserProjects
        {
            get { return _recentlyUserProjects; }
            set { SetProperty(ref _recentlyUserProjects, value); }
        }

        public ObservableCollection<string> _allUserProjects = new ObservableCollection<string>() { "123", "456" };

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

#pragma warning disable CS0649 // 从未对字段“ProjectViewModel._createProjectCommand”赋值，字段将一直保持其默认值 null
        private DelegateCommand<ProjectDto> _createProjectCommand;
#pragma warning restore CS0649 // 从未对字段“ProjectViewModel._createProjectCommand”赋值，字段将一直保持其默认值 null

        public DelegateCommand<ProjectDto> CreateProjectCommand { get => _createProjectCommand ??new  DelegateCommand<ProjectDto>(CreateProject); }

        private async void CreateProject(ProjectDto projectCreateDto)
        {
           var apiResult =await this._projectService.Create(new ProjectCreateDto() {   Icon="123", CreatorId=Global.User.UserId, Introduction="123" ,ProjectAddress="123",ProjectName="123"});
            if (apiResult.Code == ResponseCode.Success && apiResult.Content != null)
            {
                RecentlyUserProjects.Add(apiResult.Content);
                RecentlyUserProjects = new ObservableCollection<ProjectDto>(RecentlyUserProjects);
            }
        }

        public ProjectViewModel(IProjectService projectService,IDialogService dialogService)
        {
            this._projectService = projectService;
            this.dialogService = dialogService;
            Init();
        }

        private  void Init()
        {
           InitRecentlyUserProjects();
        }

        private  async void InitRecentlyUserProjects()
        {
            var apiResult =await  _projectService.GetProjects(new ProjectQueryParameter() { PageIndex = 1, PageSize = 5, UserId = 1 });
            if (apiResult.Code==ResponseCode.Success&&apiResult.Content!=null)
            {
                this.RecentlyUserProjects = new ObservableCollection<ProjectDto>(apiResult.Content);
            }
        }
    }
}