using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Document = Autodesk.Revit.DB.Document;

namespace Revit.Entity.Interfaces
{
    public interface IDataContext
    {
        ExternalCommandData ExternalCommandData { get; set; }
        Document Document { get; }

        Document GetDocument();

        UIDocument GetUIDocument();

        UIApplication GetUIApplication();
        Application GetApplication();

    }
}
