using Prism.Ioc;
using Revit.Entity.Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.Entity
{
    public static class Global
    {
        public static LoginedUserDto User { get; set; } = null;

        private static IServiceProvider _provider;
        public static IServiceProvider Provider
        {
            get { return _provider; }
            set { _provider = value; }
        }

        //private static IServiceCollection _collection;
        //public static IServiceCollection Collection { get => _collection ?? new ServiceCollection(); }



        private static IContainerExtension _containerExtension;
        public static IContainerExtension ContainerExtension
        {
            get { return _containerExtension; }
            set { _containerExtension = value; }
        }

        public static string Token { get; set; }
    }
}
