﻿using Revit.Entity.Entity;
using Revit.Entity.Entity.Dtos;
using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Project;
using Revit.Service.ApiServices;
using Revit.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Service.Services
{
    public class ProjectFileService : BaseService<ProjectFolderDto>, IProjectFileService
    {
        public ProjectFileService(HttpRestClient client) : base(client)
        {
        }

        public async Task<ApiResponse<IEnumerable<ProjectFolderDto>>> UploadFilesAsync(long folderId, ProjectUploadFileDto projectUploadFileDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.POST;
            request.Route = $"api/Folder/{folderId}/File";
            request.FilePaths = projectUploadFileDto.Files;
            return await client.ExecuteAsync<IEnumerable<ProjectFolderDto>>(request);
        }

    }
}