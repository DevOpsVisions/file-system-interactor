
using Dovs.FileSystemInteractor.Interfaces;
using System.Configuration;

namespace Dovs.FileSystemInteractor.Services
{
    /// <summary>  
    /// Service for handling configuration operations.  
    /// </summary>  
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>  
        /// Gets the configuration value for the specified key.  
        /// </summary>  
        /// <param name="key">The key of the configuration value.</param>  
        /// <returns>The configuration value associated with the specified key.</returns>  
        public string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>  
        /// Gets the column names from the configuration.  
        /// </summary>  
        /// <param name="key">The key of the configuration value for column names.</param>  
        /// <returns>A list of column names.</returns>  
        public List<string> GetColumnNames(string key)
        {
            string columnNamesSetting = GetConfigValue(key);
            if (string.IsNullOrEmpty(columnNamesSetting))
            {
                throw new ConfigurationErrorsException($"{key} setting is missing or empty.");
            }

            return new List<string>(columnNamesSetting.Split(','));
        }
    }
}