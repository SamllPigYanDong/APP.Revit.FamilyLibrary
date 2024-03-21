using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Prism.Mvvm;
using RestSharp;
using Revit.Application.Views;
using Revit.Entity.Entity.Parameters;
using Revit.Entity.Interfaces;
using Revit.Service.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Revit.Application.ViewModels
{
    public class TestViewModel : ViewModelBase
    {

        public TestViewModel(IDataContext dataContext) : base(dataContext)
        {
            IExternalEventHandler externalEventHandler = new ExternalEventHandler();
            externalEvent = ExternalEvent.Create(externalEventHandler);
        }

#pragma warning disable CS0649 // 从未对字段“TestViewModel._testCommand”赋值，字段将一直保持其默认值 null
        private AsyncRelayCommand _testCommand;
#pragma warning restore CS0649 // 从未对字段“TestViewModel._testCommand”赋值，字段将一直保持其默认值 null
        private ExternalEvent externalEvent;

        public AsyncRelayCommand TestCommand { get => _testCommand ?? new AsyncRelayCommand(Test); }

#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        private async Task Test()
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
        {
            var query = new ProjectQueryParameter() { PageIndex = 1, PageSize = 5, UserId = 1,SearchMessage="" };
            var client = new RestClient();
            var request = new RestRequest( Method.GET);
            client.BaseUrl =new Uri(@$"http://localhost:5177/api/Projects?searchMessage={query.SearchMessage}&&pageIndex=1&&pageSize=1&&userId=1");
            request.AddHeader("Content-Type", "application/json");
            var response =  client.Get(request);
            MessageBox.Show(response.Content.ToString()+response.ErrorMessage);
        }
    }

    public class ExternalEventHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            MessageBox.Show("response.ErrorMessage");
            var client = new RestClient("http://localhost:5177/api");
            var body = @"{
" + "\n" +
            @"  ""pageIndex"": 1,
" + "\n" +
            @"  ""pageSize"": 5,
" + "\n" +
            @"  ""searchMessage"": """",
" + "\n" +
            @"  ""userId"": 1
" + "\n" +
            @"}";
            var request = new RestRequest("Projects").AddJsonBody(body);
            var response = client.Get(request);
            MessageBox.Show(response.ErrorMessage);
        }

        public string GetName()
        {
            return "EventCommand";
        }
    }
}
