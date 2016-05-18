using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    public interface IEnvironmentalConfigSettings
    {
        string this[string _Key] { get; }
    }
    
    public class EnvironmentalConfigurationManager
    {
        // Lock the class from derivation.
        private EnvironmentalConfigurationManager()
        {
        }
        
        public static IEnvironmentalConfigSettings AppSettings
        {
            get
            {
                return (m_EnvironmentalConfigManager);
            }
        }
        
        private static EnvironmentalConfigSettings m_EnvironmentalConfigManager = new EnvironmentalConfigSettings();
        
        // Hide implementation class behind interface.
        private class EnvironmentalConfigSettings : IEnvironmentalConfigSettings
        {
            public string this[string _Key]
            {
                get
                {
                    // Read environmental configs section.
                    EnvironmentalConfigs.EnvironmentalConfigParser.EnvironmentalConfigsSection ECS = System.Configuration.ConfigurationManager.GetSection("environmentalConfigs") as EnvironmentalConfigs.EnvironmentalConfigParser.EnvironmentalConfigsSection;

                    // Get system environment variable.
                    string CurrentEnvironment = System.Environment.GetEnvironmentVariable("env", EnvironmentVariableTarget.Machine);

                    // Attempt to get key from specified enviroment settings.
                    string RequestedValue = null;
                    if (!string.IsNullOrWhiteSpace(CurrentEnvironment))
                    {
                        RequestedValue = GetEnvironmentKey(ECS, CurrentEnvironment, _Key);
                    }
                    
                    if (RequestedValue != null)
                    {
                        // Key found in specified environment config.
                        return (RequestedValue);
                    }
                    else
                    {
                        // Key wasnt found. Try 'all' environment settings.
                        RequestedValue = GetEnvironmentKey(ECS, "all", _Key);
                        if (RequestedValue != null)
                        {
                            // Key was found in 'all' environment config.
                            return (RequestedValue);
                        }
                        else
                        {
                            // Key wasnt found in 'all' environment config. Fall back to app settings.
                            return (ConfigurationManager.AppSettings[_Key]);
                        }
                    }
                }
            }

            private static string GetEnvironmentKey(EnvironmentalConfigs.EnvironmentalConfigParser.EnvironmentalConfigsSection _ConfigSection, string _Environment, string _Key)
            {
                string Result = null;
                EnvironmentalConfigs.EnvironmentalConfigParser.EnvironmentConfig ConfigX = _ConfigSection.Environments[_Environment];
                // Was the specified environment config found?
                if (ConfigX != null)
                {
                    Result = ConfigX[_Key];
                }
                return (Result);
            }
        }
    }
}
