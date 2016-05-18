using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvironmentalConfigs.EnvironmentalConfigParser
{
    public class EnvironmentalConfigsSection : System.Configuration.ConfigurationSection
    {
        [System.Configuration.ConfigurationProperty("environments", IsDefaultCollection = false)]
        public EnvironmentConfigCollection Environments
        {
            get
            {
                return (EnvironmentConfigCollection)base["environments"];
            }
        }
    }

    public class EnvironmentConfigCollection : System.Configuration.ConfigurationElementCollection
    {
        public EnvironmentConfigCollection()
        {
            AddElementName = "environment";
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new EnvironmentConfig();
        }

        protected override Object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((EnvironmentConfig)element).Name;
        }

        new public EnvironmentConfig this[string Name]
        {
            get
            {
                return (EnvironmentConfig)BaseGet(Name.ToLower());
            }
        }
    }

    public class EnvironmentConfig : System.Configuration.ConfigurationElementCollection
    {
        public EnvironmentConfig()
        {
            AddElementName = "add";
        }

        [System.Configuration.ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        protected override System.Configuration.ConfigurationElement CreateNewElement()
        {
            return new EnvironmentConfigSetting();
        }

        protected override Object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((EnvironmentConfigSetting)element).Key;
        }

        new public string this[string Name]
        {
            get
            {
                EnvironmentConfigSetting ConfigSettingKVP = (EnvironmentConfigSetting)BaseGet(Name);
                if (ConfigSettingKVP != null)
                {
                    return (ConfigSettingKVP).Value;
                } else
                {
                    return (null);
                }
            }
        }
    }

    public class EnvironmentConfigSetting : System.Configuration.ConfigurationElement
    {
        [System.Configuration.ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }

        [System.Configuration.ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
