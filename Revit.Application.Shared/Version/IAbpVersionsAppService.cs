﻿using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Revit.Version.Dtos;
using Revit.Dto;

namespace Revit.Version
{
    public interface IAbpVersionsAppService : IApplicationService
    {
        Task<PagedResultDto<AbpVersionDto>> GetAll(GetAllAbpVersionsInput input);

        Task<AbpVersionDto> GetAbpVersionForView(int id);

        Task<AbpVersionDto> GetAbpVersionForEdit(EntityDto input);

        Task CreateOrEdit(CreateOrEditAbpVersionDto input);

        Task Delete(EntityDto input);

        Task<UpdateFileOutput> CheckVersion(CheckVersionInput input);
    }
}