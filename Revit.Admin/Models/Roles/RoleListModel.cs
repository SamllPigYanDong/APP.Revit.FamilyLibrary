﻿using Revit.Shared.Models;
using System;

namespace Revit.Admin.Models
{
    public class RoleListModel : EntityObject
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreationTime { get; set; }
    }
}