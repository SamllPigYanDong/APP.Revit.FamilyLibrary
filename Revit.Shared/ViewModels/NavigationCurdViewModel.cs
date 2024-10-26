namespace Revit.Shared
{
    using Prism.Ioc;
    using Prism.Services.Dialogs;
    using Prism.Commands;
    using Revit.Shared.Services.Permission;
    using CommunityToolkit.Mvvm.Input;
    using System.Windows;
    using Revit.Shared.Services.Datapager;
    using Revit.Shared.ViewModels;

    public partial class NavigationCurdViewModel : NavigationViewModel
    {
        public NavigationCurdViewModel()
        { 
            //数据分页服务
            //dataPager = CommandBase.Instance.Container.Resolve<IDataPagerService>();
            //proxyService = CommandBase.Instance.Container.Resolve<IPermissionPorxyService>();
              
            //ExecuteCommand = new DelegateCommand<string>(proxyService.Execute);
            //proxyService.Generate(CreatePermissionItems());
        }
         
        public DelegateCommand<string> ExecuteCommand { get; private set; }
        public IDataPagerService dataPager { get; private set; }
        public IPermissionPorxyService proxyService { get; private set; }

        /// <summary>
        /// 新增
        /// </summary>
        [RelayCommand]
        public async void Add()
        {
            MessageBox.Show(GetPageName("Add"));
            try
            {
                var dialogResult = await dialog.ShowDialogAsync(GetPageName("Add"));
                if (dialogResult.Result == ButtonResult.OK)
                    await OnNavigatedToAsync();
            }
            catch (System.Exception e )
            {

                MessageBox.Show(e.Message+e.InnerException.Message);
            }
          
        }

        /// <summary>
        /// 编辑
        /// </summary>
        public async void Edit()
        {
            DialogParameters param = new DialogParameters();
            param.Add("Value", dataPager.SelectedItem);

            var dialogResult = await dialog.ShowDialogAsync(GetPageName("Edit"), param);
            if (dialogResult.Result == ButtonResult.OK)
                await OnNavigatedToAsync();
        }

        /// <summary>
        /// 获取弹出页名称
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private string GetPageName(string methodName)
        {

            return methodName + GetType().Name.Replace("ViewModel", $"DialogView");
        }

        /// <summary>
        /// 创建模块具备的默认权限选项清单
        /// </summary>
        /// <returns></returns>
        public virtual PermissionItem[] CreatePermissionItems() => new PermissionItem[0];
    }
}
