using Newtonsoft.Json;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Revit.Service.ApiServices
{
    //public class HttpRestClient
    //{
    //    private readonly string apiUrl;
    //    protected readonly RestClient client;

    //    public HttpRestClient(string apiUrl)
    //    {
    //        this.apiUrl = apiUrl;
    //        client = new RestClient();
    //    }

    //    public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
    //    {
    //        var response = await RequestAsync(baseRequest);
    //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //            return JsonConvert.DeserializeObject<ApiResponse>(response.Content);
    //        else
    //            return new ApiResponse()
    //            {
    //                Code = ResponseCode.Error,
    //                Result = null,
    //                Message = response.ErrorMessage
    //            };
    //    }

    //    public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
    //    {
    //        IRestResponse response = await RequestAsync(baseRequest);
    //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
    //            return JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
    //        else
    //            return new ApiResponse<T>()
    //            {
    //                Code = ResponseCode.Error,
    //                Message = response.ErrorMessage
    //            };
    //    }

    //    private async Task<IRestResponse> RequestAsync(BaseRequest baseRequest)
    //    {
    //        var request =new RestRequest();
    //        request.AddHeader("Host", "localhost:5177");
    //        if (!string.IsNullOrWhiteSpace(baseRequest.ContentType))
    //        {
    //            //request.AddHeader("Content-Type", baseRequest.ContentType);
    //        }
    //        string token = Global.Token;
    //        if (!string.IsNullOrWhiteSpace(baseRequest.Token))
    //        {
    //            token = baseRequest.Token;
    //        }
    //        if (!string.IsNullOrWhiteSpace(token))
    //        {
    //            request.AddHeader("Authorization", "Bearer " + token);
    //        }
    //        if (baseRequest.Parameter != null)
    //        {
    //            var json = JsonConvert.SerializeObject(baseRequest.Parameter);
    //            request.AddParameter(baseRequest.ContentType, json, ParameterType.RequestBody);
    //        }
    //        if (baseRequest.FilePaths != null && baseRequest.FilePaths.Count() > 0)
    //        {
    //            request.AlwaysMultipartFormData = true;
    //            foreach (var filePath in baseRequest.FilePaths)
    //            {
    //                request.AddFile("files", filePath);
    //            }
    //        }
    //        if (baseRequest.FormDatas != null && baseRequest.FormDatas.Count() > 0)
    //        {
    //            request.AlwaysMultipartFormData = true;
    //            foreach (var formData in baseRequest.FormDatas)
    //            {
    //                request.AddParameter(formData.Key, formData.Value);
    //            }
    //        }
    //        var response = await client.ExecuteAsync(request);
    //        if (Global.IsDebug)
    //        {
    //            MessageBox.Show(response.Content.ToString() + response.ErrorMessage);
    //        }
    //        return response;
    //    }
    //}
}
