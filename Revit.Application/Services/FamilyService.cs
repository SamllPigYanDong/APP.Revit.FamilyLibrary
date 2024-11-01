using Revit.Service.IServices;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using Tuna.Revit.Extension;
using Revit.Shared.Entity.Family;
using Revit.Shared.Entity.Commons.Page;
using Revit.Families;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IO;
using Revit.Shared.Base;
using Revit.Mvvm.Interface;
using Revit.Shared.Extensions.Threading;
using Revit.Mvvm.Extensions;

namespace Revit.Service.Services
{
    public class FamilyService : RevitViewModelBase, IFamilyService
    {
        private IFamilyAppService _familyAppSerivce;

        public FamilyService(IDataContext dataContext, IFamilyAppService familyAppSerivce) : base(dataContext)
        {
            this._familyAppSerivce = familyAppSerivce;
        }




       

       
    }


}
