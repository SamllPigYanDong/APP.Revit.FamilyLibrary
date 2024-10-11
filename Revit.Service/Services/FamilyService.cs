using Revit.Entity.Entity.Parameters;
using Revit.Entity.Entity;
using Revit.Entity.Interfaces;
using Revit.Service.IServices;
using Autodesk.Revit.DB;
using System.Windows.Forms;
using Revit.Application.ViewModels;
using Tuna.Revit.Extension;
using Revit.Entity.Entity.APIEntity;
using Revit.Shared.Entity.Family;
using Revit.Shared.Entity.Commons.Page;
using Revit.Shared.Entity.Commons;
using Revit.Families;

namespace Revit.Service.Services
{
    public class FamilyService : RevitViewModelBase, IFamilyService
    {
        private IFamilyAppService _familyAppSerivce;

        public FamilyService(IDataContext dataContext, IFamilyAppService familyAppSerivce) : base(dataContext)
        {
            this._familyAppSerivce = familyAppSerivce;
        }



        public async Task LoadFamilies(FamilyPageRequestDto queryParameter, Action<PagedList<FamilyDto>> action)
        {
            await _familyAppSerivce.GetPageListAsync(queryParameter).WebAsync(successCallback: async (x) =>
            {
                if (action != null)
                {
                    action.Invoke(x);
                }
            });
        }


        /// <summary>
        /// 放置族
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        public async Task PromptForFamilyInstancePlacement(FamilyDto familyDto)
        {
            var familyName = Path.GetFileNameWithoutExtension(familyDto.Name);
            //查验本项目中是否存在同名族，询问是否直接放置或者依旧载入
            Family family = _document.GetElements<Family>().FirstOrDefault(x => x.Name == familyName);

            if (family != null)
            {
                var selectResult = MessageBox.Show("存在同名称族，选择是：使用已有族放置，选择否：依旧重新载入", "提示", MessageBoxButtons.YesNo);
                if (selectResult == DialogResult.Yes)
                {
                    try
                    {
                        _uiDocument.PromptForFamilyInstancePlacement(family.GetFamilySymbols().FirstOrDefault());
                        return;
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }
            //下载族
            string filePath = await DownLoadFamily(familyDto);
            //载入族
            LoadFamily(filePath);
        }

        /// <summary>
        /// 载入族
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadFamily(string filePath)
        {
            Family family = null;
            if (File.Exists(filePath))
            {
                _document.NewTransaction(() =>
                {
                    //如果文档内存在相同的族会传输失败
                    var loadResult = _document.LoadFamily(filePath, new FamilyPromptOverLoadOption(), out family);
                    if (!loadResult)
                    {
                        family = _document.GetElements<Family>().FirstOrDefault(x => x.Name == Path.GetFileNameWithoutExtension(filePath));
                    }
                }, "放置族");
                if (family != null)
                {
                    try
                    {
                        _uiDocument.PromptForFamilyInstancePlacement(family.GetFamilySymbols().FirstOrDefault());
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("载入失败", "提示");
                }
            }
            else
            {
                MessageBox.Show("载入失败", "提示");
            }
        }

        /// <summary>
        /// 下载族
        /// </summary>
        /// <param name="familyDto"></param>
        /// <returns></returns>
        private async Task<string> DownLoadFamily(FamilyDto familyDto)
        {
            //var result = await _familyAppSerivce.DownLoadFamily(familyDto.Id);
            //string filePath = "";
            //if (result.Code == ResponseCode.Success)
            //{
            //    var direction = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            //    if (!Directory.Exists(direction))
            //    {
            //        Directory.CreateDirectory(direction);
            //    }
            //    filePath = Path.Combine(direction, familyDto.Name);
            //    using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
            //    {
            //        await fileStream.WriteAsync(result.Content, 0, result.Content.Length);
            //    }
            //}

            return "filePath";
            //return filePath;
        }
    }


}
