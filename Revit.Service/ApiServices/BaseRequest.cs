﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.ApiServices
{
    public class BaseRequest
    {
        public Method Method { get; set; }=Method.GET;
        public string Route { get; set; }
        public string ContentType { get; set; } = "application/json";

        public ParameterType ParameterType { get; set; } = ParameterType.RequestBody;

        public object Parameter { get; set; }
        public string Token { get; set; }
    }
}
