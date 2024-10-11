using System.Threading.Tasks;

namespace AppFramework.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
