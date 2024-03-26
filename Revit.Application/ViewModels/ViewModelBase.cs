using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Revit.Application.UI;
using Revit.Entity.Interfaces;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Revit.Application.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {


        private DelegateCommand<MenuBar> _navigateCommand;

        public DelegateCommand<MenuBar> NavigateCommand
        { get => _navigateCommand ?? new DelegateCommand<MenuBar>(Navigate); }

        protected virtual void Navigate(MenuBar menuBar)
        {
            
        }


        protected readonly IDataContext _dataContext;
        protected readonly Document _document;
        protected readonly UIDocument _uiDocument;
        protected readonly UIApplication _uiApplication;
        protected readonly Autodesk.Revit.ApplicationServices.Application _application;

#pragma warning disable CS0649 // 从未对字段“ViewModelBase._closeCommand”赋值，字段将一直保持其默认值 null
        private RelayCommand<Window> _closeCommand;
#pragma warning restore CS0649 // 从未对字段“ViewModelBase._closeCommand”赋值，字段将一直保持其默认值 null

        public RelayCommand<Window> CloseCommand { get => _closeCommand ?? new RelayCommand<Window>(CloseWindow); }

        protected virtual void CloseWindow(Window window)
        {
            window.Close();
        }

        public ViewModelBase(IDataContext dataContext)
        {
            _dataContext = dataContext;
            _document = dataContext.Document;
            _uiDocument = dataContext.GetUIDocument();
            _uiApplication = dataContext.GetUIApplication();
            _application = dataContext.GetApplication();
        }


    }
}
