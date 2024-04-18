using Newtonsoft.Json;
using Revit.Entity;
using Revit.Entity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Revit.Service.ApiServices
{
    public class MyHttpClient
    {
        private readonly string apiUrl;
        protected readonly HttpClient client;

        public MyHttpClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
            client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
        }
        public async Task<ApiResponse> ExecuteAsync(BaseRequest baseRequest)
        {
            var response = await RequestAsync(baseRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
            else
                return new ApiResponse()
                {
                    Code = ResponseCode.Error,
                    Result = null,
                    Message = response.ReasonPhrase
                };
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest)
        {
            var response = await RequestAsync(baseRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ApiResponse<T>>(await response.Content.ReadAsStringAsync());
            else
                return new ApiResponse<T>()
                {
                    Code = ResponseCode.Error,
                    Message = response.ReasonPhrase
                };
        }

        private async Task<HttpResponseMessage> RequestAsync(BaseRequest baseRequest)
        {
            var request = new HttpRequestMessage(baseRequest.Method,baseRequest.Route);
            request.Headers.Add("Host", "localhost:5177");

            if (!string.IsNullOrWhiteSpace(baseRequest.ContentType))
            {
                //request.AddHeader("Content-Type", baseRequest.ContentType);
            }
            string token = Entity.Global.Token;
            if (!string.IsNullOrWhiteSpace(baseRequest.Token))
            {
                token = baseRequest.Token;
            }
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            if (baseRequest.Parameter != null)
            {
                var json =new StringContent( JsonConvert.SerializeObject(baseRequest.Parameter), Encoding.UTF8, "application/json");
                request.Content=json;
            }
            if (baseRequest.FilePaths != null && baseRequest.FilePaths.Count() > 0)
            {
                //request.AlwaysMultipartFormData = true;
                foreach (var filePath in baseRequest.FilePaths)
                {
                    //request.AddFile("files", filePath);
                }
            }
            if (baseRequest.FormDatas != null && baseRequest.FormDatas.Count() > 0)
            {
                //request.AlwaysMultipartFormData = true;
                foreach (var formData in baseRequest.FormDatas)
                {
                    //request.AddParameter(formData.Key, formData.Value);
                }
            }
            var response = await client.SendAsync(request);
            if (Global.IsDebug)
            {
                MessageBox.Show(response.Content.ToString() + response.RequestMessage);
            }
            return response;
        }

    }
}
