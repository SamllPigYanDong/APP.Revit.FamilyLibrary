using Prism.Commands;
using Prism.Regions;
using Revit.Application.UI;
using Revit.Application.Views.ProjectViews;
using Revit.Entity.Interfaces;
using System.Collections.ObjectModel;

namespace Revit.Application.ViewModels
{
    public class MainProjectViewModel : ViewModelBase
    {
        private readonly IRegionManager regionManager;

        private ObservableCollection<MenuBar> _menuBars = new ObservableCollection<MenuBar>() {
        new MenuBar() { Icon = "RecentlyProject", Title = "最近项目", NameSpace = nameof(RecentlyProjectView) },
        new MenuBar() { Icon = "Project", Title = "全部项目", NameSpace = nameof(TotalProjectView) },
        };
        public ObservableCollection<MenuBar> MenuBars
        {
            get { return _menuBars; }
            set { SetProperty(ref _menuBars, value); }
        }

        protected override void Navigate(MenuBar menuBar)
        {
            if (menuBar != null && !string.IsNullOrWhiteSpace(menuBar.NameSpace))
            {
                //MessageBox.Show(this.regionManager.Regions.Count().ToString());
                regionManager.RequestNavigate("ProjectContent", menuBar.NameSpace);
            }
        }


        public MainProjectViewModel(IDataContext dataContext, IRegionManager regionManager) : base(dataContext)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion("ProjectContent", typeof(RecentlyProjectView));
        }
    }
}
