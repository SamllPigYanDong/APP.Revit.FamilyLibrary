using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace AppFramework.Localization
{
    public static class AppFrameworkLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AppFrameworkConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AppFrameworkLocalizationConfigurer).GetAssembly(),
                        "AppFramework.Localization.AppFramework"
                    )
                )
            );
        }
    }
}