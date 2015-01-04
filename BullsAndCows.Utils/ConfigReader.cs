namespace BullsAndCows.Utils
{
    using System.Configuration;

    /// <summary>
    /// Reads information from .config files
    /// </summary>
    public static class ConfigReader
    {
        /// <summary>
        /// Get AppSettings value by given key
        /// </summary>
        /// <param name="key">AppSettings key</param>
        /// <returns>string: value</returns>
        public static string GetConfigString(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
