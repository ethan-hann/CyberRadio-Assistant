// SystemInfo.cs : RadioExt-Helper
// Copyright (C) 2024  Ethan Hann
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

using System.Management;
using System.Reflection;
using System.Text;

namespace RadioExt_Helper.utility;

/// <summary>
/// Responsible for gathering system information used in the log file.
/// </summary>
public static class SystemInfo
{
    private static readonly Version CurrentVersion =
        Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0);

    /// <summary>
    /// Get the header for the log file.
    /// </summary>
    /// <returns></returns>
    public static string GetLogFileHeader()
    {
        StringBuilder sb = new();

        // Program Info
        sb.AppendLine($"Cyber Radio Assistant - Version: {CurrentVersion.Major}.{CurrentVersion.Minor}.{CurrentVersion.Build}");

        // Operating System Information
        sb.AppendLine($"OS Version: {Environment.OSVersion}");
        sb.AppendLine($"64-bit OS: {Environment.Is64BitOperatingSystem}");

        // Processor Information
        sb.AppendLine("Processor Information:");
        sb.Append(GetProcessorInfo());
        sb.AppendLine(); // Adding an empty line

        // Memory Information
        sb.AppendLine("Memory Information:");
        sb.Append(GetMemoryInfo());

        sb.AppendLine("===============================================");

        return sb.ToString();
    }

    private static string GetProcessorInfo()
    {
        StringBuilder sb = new();

        try
        {
            using ManagementObjectSearcher searcher = new("select * from Win32_Processor");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
            {
                sb.AppendLine($"\tName: {obj["Name"]}");
                sb.AppendLine($"\tManufacturer: {obj["Manufacturer"]}");
                sb.AppendLine($"\tProcessorId: {obj["ProcessorId"]}");
                sb.AppendLine($"\tDescription: {obj["Description"]}");
                sb.AppendLine($"\tNumberOfCores: {obj["NumberOfCores"]}");
                sb.AppendLine($"\tNumberOfLogicalProcessors: {obj["NumberOfLogicalProcessors"]}");
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine($"\tError retrieving processor info: {ex.Message}");
        }

        return sb.ToString();
    }

    private static string GetMemoryInfo()
    {
        StringBuilder sb = new();

        try
        {
            using ManagementObjectSearcher searcher = new("select * from Win32_ComputerSystem");
            foreach (var obj in searcher.Get().Cast<ManagementObject>())
            {
                sb.AppendLine($"\tTotal Physical Memory: {obj["TotalPhysicalMemory"]}");
                sb.AppendLine($"\tManufacturer: {obj["Manufacturer"]}");
                sb.AppendLine($"\tModel: {obj["Model"]}");
            }
        }
        catch (Exception ex)
        {
            sb.AppendLine($"\tError retrieving memory info: {ex.Message}");
        }

        return sb.ToString();
    }
}