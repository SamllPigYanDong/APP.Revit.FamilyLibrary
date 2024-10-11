﻿using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Family;
using Revit.Shared.Entity.Commons;
using System;
using System.Threading.Tasks;

namespace Revit.Service.IServices
{
    public interface IFamilyService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParameter"></param>
        /// <param name="action"></param>
        Task LoadFamilies(FamilyPageRequestDto queryParameter, Action<PagedList<FamilyDto>> action);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        Task PromptForFamilyInstancePlacement(FamilyDto familyDto);
    }
}
