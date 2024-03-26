using Revit.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Interfaces;
using Revit.Service.Services;
using System;

namespace Revit.Application.ViewModels
{
    public  class WorkSpaceViewModel : ViewModelBase
    {
        private LoginedUserDto _loginUserDto;
        private readonly IUserService _userService;

        public LoginedUserDto LoginUserDto
        {
            get { return _loginUserDto; }
            set { SetProperty(ref _loginUserDto, value); }
        }



        //[ObservableProperty]
        //public LoginedUserDto _loginedUser = new LoginedUserDto() { UserName = "严冬", Account = "188****3248" };


        //[ObservableProperty]
        //public DateTime _currentDate = DateTime.Now;

        //[ObservableProperty]
        //public DateTime _currentDateHours = DateTime.Now.Date;

        //[ObservableProperty]
        //public int _projectCount = 0;

        //[ObservableProperty]
        //public int _myTaskCount = 0;


        //[ObservableProperty]
        //public ObservableCollection<string> _recentlyFiles=new ObservableCollection<string>() { "123","456"};

        //[ObservableProperty]
        //public ObservableCollection<string> _recentlyNews = new ObservableCollection<string>() { "123", "456" };

        //[ObservableProperty]
        //public ObservableCollection<string> _recentlyTasks = new ObservableCollection<string>() { "123", "456" };

        public WorkSpaceViewModel(IDataContext dataContext, IUserService userService) : base(dataContext)
        {
            this._userService = userService;

            Init();
        }


        private async void InitUser()
        {
            LoginUserDto = Global.User;
        }


        #region PrivateMethod

        private void Init()
        {
            InitUser();
            //InitRecentlyProjects();
            //InitRecentlyNews();
            //InitRecentlyTasks();
        }

        private void InitRecentlyTasks()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 初始化最新动态
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitRecentlyNews()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 初始化最近项目
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitRecentlyProjects()
        {
            throw new NotImplementedException();
        }


        #endregion

    }
}
