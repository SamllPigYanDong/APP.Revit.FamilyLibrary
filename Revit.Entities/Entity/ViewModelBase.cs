using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Revit.Entity.Entity.UI;
using Revit.Entity.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Revit.Application.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        private DelegateCommand<MenuBar> _navigateCommand;

        public DelegateCommand<MenuBar> NavigateCommand
        { get => _navigateCommand ?? new DelegateCommand<MenuBar>(Navigate); }

        private DelegateCommand _changeNextPageCommand;

        public DelegateCommand ChangeNextPageCommand
        { get => _changeNextPageCommand ?? new DelegateCommand(ChangeNextPage); }

        private DelegateCommand _changePreviousPageCommand;

        public DelegateCommand ChangePreviousPageCommand
        { get => _changePreviousPageCommand ?? new DelegateCommand(ChangePreviousPage); }

        private RelayCommand<Window> _closeCommand;

        public RelayCommand<Window> CloseCommand { get => _closeCommand ?? new RelayCommand<Window>(CloseWindow); }

        private DelegateCommand _searchCommand;
        public DelegateCommand SearchCommand
        { get => _searchCommand ?? new DelegateCommand(Search); }


        private DelegateCommand _submitCommand;
        private bool isBusy;

        public DelegateCommand SubmitCommand
        {
            get => _submitCommand ?? new DelegateCommand(Submit);
        }

        protected virtual void Submit()
        {
          
        }

        protected virtual void ChangePreviousPage()
        {
        }

        protected virtual void Search()
        {
        }

        protected virtual void Navigate(MenuBar menuBar)
        {
        }

        protected virtual void ChangeNextPage()
        {
        }

        protected virtual void CloseWindow(Window window)
        {
            window.Close();
        }


        private readonly IAppMapper mapper;
        private readonly IGlobalValidator validator;

        public bool IsNotBusy => !IsBusy;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                SetProperty(ref isBusy, value);
                OnPropertyChanged(nameof(IsNotBusy));
            }
        }

        public virtual async Task SetBusyAsync(Func<Task> func, string loadingMessage = null)
        {
            IsBusy = true;
            try
            {
                await func();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 实体验证器方法
        /// </summary>
        /// <typeparam name="T">验证结果</typeparam>
        /// <param name="model">验证实体</param>
        /// <returns></returns>
        public virtual ValidationResult Verify<T>(T model, bool ShowError = true)
        {
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid && ShowError)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in validationResult.Errors)
                {
                    stringBuilder.AppendLine(item.ErrorMessage);
                }
                //AppDialogHelper.Warn(stringBuilder.ToString());
            }
            return validationResult;
        }

        /// <summary>
        /// 实体映射方法
        /// </summary>
        /// <typeparam name="T">最终类型</typeparam>
        /// <param name="model">映射实体</param>
        /// <returns></returns>
        public T Map<T>(object model) => mapper.Map<T>(model);

        public ViewModelBase()
        {
         
        }


    }
}
