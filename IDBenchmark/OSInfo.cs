using System;
using System.Linq;
using System.Management;

namespace IDBenchmark
{
    class OSInfo
    {
        /// <summary>
        /// Returns the FULL name of the operating system running on this computer.
        /// </summary>
        /// <returns>A string containing the operating system name.</returns>
        public static string GetOsFullName()
        {
            var xName = GetHardwareInfo("Win32_OperatingSystem", "OSArchitecture"); 
            var osName = GetHardwareInfo("Win32_OperatingSystem", "Caption");
            
            return $"{osName} ({xName})";
        }

        /// <summary>
        /// Returns the name of the processor.
        /// </summary>
        /// <returns>A string containing the processor name.</returns>
        public static string GetProcessorName()
        {
            return GetHardwareInfo("Win32_Processor");
        }

        /// <summary>
        /// Returns model.
        /// </summary>
        /// <returns>A string containing the model of computer.</returns>
        public static string GetModelName()
        {
            return GetHardwareInfo("Win32_ComputerSystem", "Manufacturer") + " " + GetHardwareInfo("Win32_ComputerSystem", "Model");
        }

        /// <summary>
        /// Returns the memory information.
        /// </summary>
        /// <returns>A string containing the memory information.</returns>
        public static string GetMemoryName()
        {
            var memory = decimal.Parse(GetHardwareInfo("Win32_ComputerSystem", "TotalPhysicalMemory"));
            memory =  decimal.Round(memory / 1024 / 1024 / 1024, 2);

            return $"{memory} GB";
        }

        /// <summary>
        /// Returns the name of the GPU.
        /// </summary>
        /// <returns>A string containing the GPU name.</returns>
        public static string GetGpuName()
        {
            return GetHardwareInfo("Win32_VideoController");
        }

        /// <summary>
        /// Returns the information of the computer hardware.
        /// </summary>
        /// <returns>A string containing the the computer hardware info.</returns>
        private static string GetHardwareInfo(string table, string key = "Name")
        {
            var info = "";
            var searcher = new ManagementObjectSearcher("select * from " + table);
            try
            {
                foreach (var share in searcher.Get().Cast<ManagementObject>())
                {
                    try {
                        info = share[key].ToString();
                    }
                    catch {
                        info = share.ToString();
                    }
                    if (share.Properties.Count <= 0) {
                        info = "";
                    }
                }
            }
            catch (Exception exp)
            {
                info = "Oooops, something went wrong:(\n" + exp.Message.ToString();
            }
            return info.Trim();
        }
    }
}
