using Microsoft.Extensions.Configuration;

namespace AppFramework.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
