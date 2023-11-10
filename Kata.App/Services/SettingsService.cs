using Kata.Core.Services;

namespace Kata_Login.Services;

public class SettingsService : ISettingsService
{
    private static IPreferences AppSettings => Preferences.Default;

    public string ServerUrl
    {
        get => AppSettings.Get(ServerUrlKey, ServerUrlDefault).ToLower().Trim();
        set => AppSettings.Set(ServerUrlKey, value);
    }

    public string Pin 
    {
        get => AppSettings.Get(PinKey, string.Empty).ToLower().Trim();
        set => AppSettings.Set(PinKey, value);
    }
    
    #region Setting Constants

    private const string PinKey = "UserPin";
    private const string ServerUrlKey = "prefServerurl";

    /*
     * ServerUrlDefault will be replaced by the JENKINS job. Do not change the variable
     */
    private const string ServerUrlDefault = "";

    #endregion
}