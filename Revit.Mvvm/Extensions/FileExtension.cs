using Revit.Entity.Entity.Dtos.Project;
using Revit.Entity.Entity;
using Revit.Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Revit.Mvvm.Extensions
{
    public static class FileExtension
    {
        public static void SelectFile(Action<OpenFileDialog> dialogConfig = null, Action<OpenFileDialog> succeessedFunc = null, Action<OpenFileDialog> fialFunc = null)
        {
            //获取文档路径
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "所有文件 (*.*)|*.*";
            openFileDialog.Title = "选择一个文件";
            if (dialogConfig != null)
            {
                dialogConfig.Invoke(openFileDialog);
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (succeessedFunc != null)
                {
                    succeessedFunc.Invoke(openFileDialog);
                }
            }
            if (fialFunc != null)
            {
                fialFunc.Invoke(openFileDialog);
            }
        }


    }
}
