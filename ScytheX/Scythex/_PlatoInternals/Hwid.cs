using System;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace _PlatoInternals
{
	// Token: 0x02000013 RID: 19
	public static class Hwid
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00008C78 File Offset: 0x00006E78
		public static string Get()
		{
			if (true)
			{
				ManagementObject managementObject = new ManagementObjectSearcher("select * from Win32_OperatingSystem").Get().Cast<ManagementObject>().First<ManagementObject>();
				ManagementObject managementObject2 = new ManagementObjectSearcher("select * from Win32_Processor").Get().Cast<ManagementObject>().First<ManagementObject>();
				ManagementObject managementObject3 = new ManagementObjectSearcher("select * from Win32_Bios").Get().Cast<ManagementObject>().First<ManagementObject>();
				string s = string.Format("{0}{1}{2}{3}{4}{5}{6}", new object[]
				{
					managementObject["SerialNumber"],
					managementObject["RegisteredUser"],
					managementObject2["Name"],
					managementObject2["MaxClockSpeed"],
					managementObject2["SocketDesignation"],
					managementObject3["Name"],
					managementObject3["SerialNumber"]
				});
				using (MD5 md = MD5.Create())
				{
					return BitConverter.ToString(md.ComputeHash(Encoding.UTF8.GetBytes(s))).Replace("-", "");
				}
			}
			throw new NotImplementedException("Please implement an HWID method for this operating system.");
		}
	}
}
