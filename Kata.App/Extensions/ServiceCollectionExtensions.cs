namespace WoundApp.Extensions;

public static class ServiceCollectionExtensions
{
    public static MauiAppBuilder AddServiceAsSingleton<TService>(this MauiAppBuilder builder) where TService : class
    {
        builder.Services.AddSingleton(typeof(TService));
        return builder;
    }

    public static MauiAppBuilder AddServiceAsSingleton<TService, TImplementation>(this MauiAppBuilder builder)
        where TService : class where TImplementation : class
    {
        builder.Services.AddSingleton(typeof(TService), typeof(TImplementation));
        return builder;
    }

    public static MauiAppBuilder AddServiceAsSingleton<TService>(this MauiAppBuilder builder,
        Func<IServiceProvider, TService> implementationFactory) where TService : class
    {
        builder.Services.AddSingleton(typeof(TService), implementationFactory);
        return builder;
    }
}