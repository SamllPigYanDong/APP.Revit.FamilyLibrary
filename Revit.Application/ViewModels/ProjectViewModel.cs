using CommunityToolkit.Mvvm.ComponentModel;
using Revit.Application.UI;
using Revit.Service.Services;
using System;
using System.Collections.ObjectModel;
using Revit.Service.IServices;
using Revit.Entity.Entity.Parameters;
using Revit.Entity.Entity;
using Prism.Commands;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Mvvm.Prism;
using Prism.Services.Dialogs;

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

        private DelegateCommand<ProjectDto> _createProjectCommand;

        public DelegateCommand<ProjectDto> CreateProjectCommand 
        { get => _createProjectCommand ??new  DelegateCommand<ProjectDto>(CreateProject).ObservesCanExecute(()=>IsEnable); }

        private DelegateCommand<ProjectDto> _modifyProjectCommand;



        public DelegateCommand<ProjectDto> ModifyProjectCommand
        { get => _modifyProjectCommand ?? new DelegateCommand<ProjectDto>(ModifyProject).ObservesCanExecute(() => IsEnable); }

        private DelegateCommand<ProjectDto> _deleteProjectCommand;

        public DelegateCommand<ProjectDto> DeleteProjectCommand
        { get => _deleteProjectCommand ?? new DelegateCommand<ProjectDto>(DeleteProject).ObservesCanExecute(() => IsEnable); }

       

        private bool _isEnable=true;

        public bool IsEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }


        private async void CreateProject(ProjectDto projectCreateDto)
        {
            IsEnable = false;
            dialogService.ShowDialog("ProjectCreateDialog", new DialogParameters() ,async (res) =>
            {
                if (res.Result==ButtonResult.OK)
                {
                    var projectDto = res.Parameters.GetValue<ProjectCreateDto>("createProject");
                    var apiResult = await this._projectService.Create(projectDto);
                    if (apiResult.Code == ResponseCode.Success && apiResult.Content != null)
                    {
                        RecentlyUserProjects.Add(apiResult.Content);
                        RecentlyUserProjects = new ObservableCollection<ProjectDto>(RecentlyUserProjects);
                    }
                }
            });
            IsEnable = true;
        }
      
        private void ModifyProject(ProjectDto dto)
        {
            throw new NotImplementedException();
        }
        private async void DeleteProject(ProjectDto project)
        {
            IsEnable = false;
            var apiResult = await this._projectService.Delete(project.Id);
            if (apiResult.Code == ResponseCode.Success)
            {
                RecentlyUserProjects.Remove(project);
                RecentlyUserProjects = new ObservableCollection<ProjectDto>(RecentlyUserProjects);
            }
            IsEnable = true;
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