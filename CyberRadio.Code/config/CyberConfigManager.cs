// CyberConfigManager.cs : RadioExt-Helper
// Copyright (C) 2025  Ethan Hann
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using AetherUtils.Core.Configuration;

namespace RadioExt_Helper.config;

/// <summary>
///     Represents a configuration manager for a YAML configuration file using <see cref="ApplicationConfig" /> as the
///     base configuration.
/// </summary>
public sealed class CyberConfigManager(string configFilePath) : ConfigManager<ApplicationConfig>(configFilePath)
{
    private readonly string _configFilePath = configFilePath;

    /// <summary>
    ///     Create a new, empty configuration.
    /// </summary>
    /// <returns>true if the configuration is initialized; false, otherwise.</returns>
    public override bool CreateDefaultConfig()
    {
        if (_configFilePath.Equals(string.Empty))
            return false;

        CurrentConfig = new ApplicationConfig();
        return IsInitialized;
    }
}