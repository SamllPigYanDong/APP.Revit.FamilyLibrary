﻿namespace Revit.Shared.ViewModels
{
    using global::Prism.Commands;
    using global::Prism.Regions;
    using System.Threading.Tasks;
    using Revit.Shared.Interfaces;
    using global::Prism.Ioc;
    using global::Prism.Services.Dialogs;

    public class NavigationViewModel : ViewModelBase, INavigationAware
    {
        public NavigationViewModel()
        {
            this.dialogService = SharedModule.Instance.Container.Resolve<IDialogService>();
            RefreshCommand = new DelegateCommand(async () => await OnNavigatedToAsync());
        }

        private string title;
        public readonly IDialogService dialogService;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }


        public DelegateCommand RefreshCommand { get; private set; }

        #region INavigationAware

        /// <summary>
        /// 导航目标是否重新利用原来的对象
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => false;

        /// <summary>
        /// 导航发生变更时
        /// </summary>
        /// <param name="navigationContext"></param>
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }

        /// <summary>
        /// 导航到达时
        /// </summary>
        /// <param name="navigationContext"></param>
        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            await OnNavigatedToAsync(navigationContext);
        }

        /// <summary>
        /// 异步刷新方法,当页面导航到达时触发该方法
        /// </summary>
        /// <returns></returns> 
        public virtual async Task OnNavigatedToAsync(NavigationContext navigationContext = null) => await Task.CompletedTask;

        #endregion 
    }
}
