using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Demo.Dtos;
using Revit.Version.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Demo
{
    public interface IAbpDemoAppService : IApplicationService
    {
        Task<PagedResultDto<AbpDemoDto>> GetAll(GetAllAbpDemoInput input);
    }
}
