﻿namespace Revit.Admin.Models
{
    public struct LanguageStruct
    {
        public LanguageStruct(string icon, string name, string displayName)
        {
            Icon = icon;
            Name = name;
            DisplayName = displayName;
        }

        public string Icon { get; }

        public string Name { get; }

        public string DisplayName { get; }
    }
}
