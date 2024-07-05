using System;
using System.Management;
using System.Reflection;
using System.Text;

namespace RadioExt_Helper.utility
{
    public sealed class SystemInfo
    {
        private static readonly Version _currentVersion = Assembly.GetExecutingAssembly().GetName().Version ?? new Version(1, 0, 0);

        public static string GetLogFileHeader()
        {
            StringBuilder sb = new();

            // Program Info
            sb.AppendLine($"Cyber Radio Assistant - Version: {_currentVersion.Major}.{_currentVersion.Minor}.{_currentVersion.Build}");

            // Operating System Information
            sb.AppendLine($"OS Version: {Environment.OSVersion}");
            sb.AppendLine($"64-bit OS: {Environment.Is64BitOperatingSystem}");

            // Machine Name and User
            sb.AppendLine($"Machine Name: {Environment.MachineName}");
            sb.AppendLine($"User Name: {Environment.UserName}");

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
}
