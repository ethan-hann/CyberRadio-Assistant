using AetherUtils.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.config
{
    /// <summary>
    /// Represents a configuration manager for a YAML configuration file using <see cref="CyberConfig"/> as the
    /// base configuration.
    /// </summary>
    /// <param name="configFilePath">The path to the configuration file.</param>
    public sealed class CyberConfigManager(string configFilePath) : ConfigManager<CyberConfig>(configFilePath)
    {
        /// <summary>
        /// Create a new, empty configuration.
        /// </summary>
        /// <returns>true if the configuration is initialized; false, otherwise.</returns>
        public override bool CreateDefaultConfig()
        {
            CurrentConfig = new CyberConfig();
            return IsInitialized;
        }
    }
}
