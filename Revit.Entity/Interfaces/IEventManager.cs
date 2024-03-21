using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity.Interfaces
{
    public interface IEventManager
    {
        void Subscribe();//订阅

        void Unsubscribe();//取消订阅

    }
}
