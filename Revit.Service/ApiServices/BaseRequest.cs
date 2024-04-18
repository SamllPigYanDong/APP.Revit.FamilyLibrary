using Autodesk.Revit.DB.DirectContext3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.ApiServices
{
    public class BaseRequest
    {
        public HttpMethod Method { get; set; }=HttpMethod.Get;
        public string Route { get; set; }
        public string ContentType { get; set; } = "application/json";


        public object Parameter { get; set; }
        public string Token { get; set; }

        public IEnumerable<string> FilePaths { get; set; }

        public IDictionary<string,object> FormDatas { get; set; } = new Dictionary<string, object>();
    }

    public static class ContentType
    {
        public static string Json { get => "application/json;"; }
        public static string FormData { get => "multipart/form-data;"; }
    
    }

    

}
