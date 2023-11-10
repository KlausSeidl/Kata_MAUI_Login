namespace Kata.Core.Services;

/// <summary>
///     Stores core app settings
/// </summary>
public interface ISettingsService
{
    /// <summary>
    ///     url of charisma-api service
    /// </summary>
    string ServerUrl { get; set; }
    
    string Pin { get; set; }
}