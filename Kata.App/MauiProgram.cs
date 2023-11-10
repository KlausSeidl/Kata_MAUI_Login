using CommunityToolkit.Maui;
using Kata_Login.Navigation;
using Kata.Core.Navigation;
using Microsoft.Extensions.Logging;
using WoundApp.Extensions;

namespace Kata_Login
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                // Navigation
                .AddServiceAsSingleton<NavigationService>()
                .AddServiceAsSingleton<IFlyoutNavigationService>(serviceProvider => serviceProvider.GetRequiredService<NavigationService>())
                .AddServiceAsSingleton<INavigationService>(serviceProvider => serviceProvider.GetRequiredService<NavigationService>())
                // Pages and ViewModels
                // .AddViewModelMapping<ListPage, ListViewModel>()
                // .AddViewModelMapping<QRCodeScanPage, QrCodeScanViewModel>()
                // .AddViewModelMapping<WoundLocationPage, WoundLocationViewModel>()
                // .AddViewModelMapping<WoundLocationRootPage, WoundLocationRootViewModel>()
                // .AddViewModelMapping<PinEntryPage, PinEntryViewModel>()
                // .AddViewModelMapping<PreferencesPage, PreferencesViewModel>()
                ;

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}