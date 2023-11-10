using Kata.Core.Services;
namespace Kata_Login.Services;

public class SettingsService : ISettingsService
{
    private static IPreferences AppSettings
    {
        get
        {
            return Preferences.Default;
        }
    }

    #region Setting Constants

    private const string ServerUrlKey = "prefServerurl";
    /*
     * ServerUrlDefault will be replaced by the JENKINS job. Do not change the variable
     */
    private const string ServerUrlDefault = ""; 
    
    #endregion

    public string ServerUrl
    {
        get => AppSettings.Get(ServerUrlKey, ServerUrlDefault).ToLower().Trim();
        set => AppSettings.Set(ServerUrlKey, value);
    }
}