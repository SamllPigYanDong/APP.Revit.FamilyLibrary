using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Entity.Dtos
{
    public class BaseDto :BindableBase
    {
        public long CreatorId { get; set; }

       
    }
}
