using CommunityToolkit.Mvvm.ComponentModel;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Application.ViewModels
{
    public  class WorkSpaceViewModel : ViewModelBase
    {
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

        public WorkSpaceViewModel(IDataContext dataContext) : base(dataContext)
        {
        }

        #region PrivateMethod

        private void Init()
        {
            InitRecentlyProjects();
            InitRecentlyNews();
            InitRecentlyTasks();
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
