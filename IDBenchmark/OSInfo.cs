using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                info = "Opps:(\n" + exp.Message.ToString();
            }
            return info.Trim();
        }
    }
}
