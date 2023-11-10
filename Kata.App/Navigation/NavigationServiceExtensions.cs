namespace Kata_Login.Navigation;

public static class NavigationServiceExtensions
{
    public static MauiAppBuilder AddViewModelMapping<TView, TViewModel>(this MauiAppBuilder builder)
        where TView : Page
    {
        builder.Services.AddTransient(typeof(TView));
        builder.Services.AddTransient(typeof(TViewModel));

        NavigationService.ViewModelPageMapping.Add(typeof(TViewModel), typeof(TView));

        return builder;
    }
}