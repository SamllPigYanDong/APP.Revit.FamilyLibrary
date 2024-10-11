using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Newtonsoft.Json;
using Revit.Entity;
using Revit.Entity.Entity;
using Revit.Shared.Entity.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static UIFramework.Widget.CustomControls.NativeMethods;

namespace Revit.Service.ApiServices
{
    public class MyHttpClient
    {
        private readonly string apiUrl;
        protected readonly HttpClient client = new HttpClient();

        public MyHttpClient(string apiUrl)
        {
            this.apiUrl = apiUrl;
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
                    Content = null,
                    Message = response.ReasonPhrase
                };
        }

        public async Task<ApiResponse<T>> ExecuteAsync<T>(BaseRequest baseRequest) 
        {
            var response = await RequestAsync(baseRequest);
            if (response != null && response.Content != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var result= JsonConvert.DeserializeObject<ApiResponse<T>>(await response.Content.ReadAsStringAsync());
                    //MessageBox.Show(JsonConvert.SerializeObject(result));
                    return result;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message+e.InnerException?.Message);

                    return new ApiResponse<T>()
                    {
                        Code = ResponseCode.Error,
                        Message = response.ReasonPhrase
                    };
                }
            }
            else
                return new ApiResponse<T>()
                {
                    Code = ResponseCode.Error,
                    Message = response?.ReasonPhrase
                };
        }


        public async Task<ApiResponse<byte[]>> ExecuteStreamAsync(BaseRequest baseRequest)
        {
            var response = await RequestAsync(baseRequest);
            if (response!=null&& response.Content != null && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    var result = await response.Content.ReadAsByteArrayAsync();
                    return new ApiResponse<byte[]>() { Content = result };
                }
                catch (Exception)
                {
                    return new ApiResponse<byte[]>()
                    {
                        Code = ResponseCode.Error,
                        Message = response.ReasonPhrase
                    };
                }
            }
            else
                return new ApiResponse<byte[]>()
                {
                    Code = ResponseCode.Error,
                    Message = response?.ReasonPhrase
                };
        }


        private async Task<HttpResponseMessage> RequestAsync(BaseRequest baseRequest)
        {
            var request = new HttpRequestMessage(baseRequest.Method, baseRequest.Route);
            if (request==null)
            {
                return null;
            }
            request.Headers.Add("Host", "localhost:5177");

            if (!string.IsNullOrWhiteSpace(baseRequest.ContentType))
            {
                //request.Headers.Add("Content-Type", baseRequest.ContentType);
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
                var json = new StringContent(JsonConvert.SerializeObject(baseRequest.Parameter), Encoding.UTF8, "application/json");
                request.Content = json;
            }
            if (baseRequest.FilePaths != null && baseRequest.FilePaths.Count() > 0)
            {
                var formData = new MultipartFormDataContent();
                foreach (var filePath in baseRequest.FilePaths)
                {
                    // 确保文件存在
                    if (!File.Exists(filePath))
                        throw new FileNotFoundException("文件未找到。", filePath);
                    // 添加文件内容
                    var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
                    var fileName = Path.GetFileName(filePath);
                    formData.Add(fileContent, "files", fileName);
                }
                request.Content = formData;
            }
            if (baseRequest.FormDatas != null && baseRequest.FormDatas.Count() > 0)
            {
                using var multiFormData = new MultipartFormDataContent();
                foreach (var formData in baseRequest.FormDatas)
                {
                    multiFormData.Add(new StringContent(formData.Value), formData.Key);
                }
            }
            HttpResponseMessage response = null;
            try
            {
                response = await client.SendAsync(request);
            }
            catch (Exception)
            {
                return response;
            }
            finally 
            {
                if (Global.IsDebug)
                {
                    MessageBox.Show(response?.RequestMessage + response?.ReasonPhrase,"标头");
                }
            }
            return response;
        }

    }
}
