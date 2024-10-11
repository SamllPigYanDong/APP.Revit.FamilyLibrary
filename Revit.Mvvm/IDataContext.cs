﻿using Autodesk.Revit.UI;
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

        Autodesk.Revit.ApplicationServices.Application GetApplication();

    }
}
