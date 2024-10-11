using System.Globalization;

namespace AppFramework.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}