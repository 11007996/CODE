using Microsoft.Extensions.DependencyInjection;

namespace LuxVideoDet.Localization;

public static class LuxVideoDetLocalizationServiceCollectionExtensions
{
    public static IServiceCollection AddLuxVideoDetLocalization(this IServiceCollection services)
    {
        services.AddSingleton<IAppLocalizer, AppLocalizer>();
        return services;
    }
}
