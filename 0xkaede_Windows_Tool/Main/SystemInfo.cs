using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace _0xkaede_Windows_Tool
{
    internal class SystemInfo
    {
		public static string GetProcessorId()
		{
			ManagementClass managementClass = new ManagementClass("win32_processor");
			ManagementObjectCollection instances = managementClass.GetInstances();
			string result = string.Empty;
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = instances.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					result = managementObject.Properties["processorID"].Value.ToString();
				}
			}
			return result;
		}

		public static string GetHDDSerialNo()
		{
			ManagementClass managementClass = new ManagementClass("Win32_LogicalDisk");
			ManagementObjectCollection instances = managementClass.GetInstances();
			string text = "";
			foreach (ManagementBaseObject managementBaseObject in instances)
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				text += Convert.ToString(managementObject["VolumeSerialNumber"]);
			}
			return text;
		}

		public static string GetMACAddress()
		{
			ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection instances = managementClass.GetInstances();
			string text = string.Empty;
			foreach (ManagementBaseObject managementBaseObject in instances)
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				bool flag = text == string.Empty;
				if (flag)
				{
					bool flag2 = (bool)managementObject["IPEnabled"];
					if (flag2)
					{
						text = managementObject["MacAddress"].ToString();
					}
				}
				managementObject.Dispose();
			}
			text = text.Replace(":", "");
			return text;
		}

		public static string GetBoardMaker()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				try
				{
					return managementObject.GetPropertyValue("Manufacturer").ToString();
				}
				catch
				{
				}
			}
			return "Board Maker: Unknown";
		}

		public static string GetBoardProductId()
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				try
				{
					return managementObject.GetPropertyValue("Product").ToString();
				}
				catch
				{
				}
			}
			return "Product: Unknown";
		}
	}
}
