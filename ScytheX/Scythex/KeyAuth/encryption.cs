using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace KeyAuth
{
	// Token: 0x02000010 RID: 16
	public static class encryption
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00008910 File Offset: 0x00006B10
		public static string HashHMAC(string enckey, string resp)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(enckey);
			byte[] bytes2 = Encoding.UTF8.GetBytes(resp);
			return encryption.byte_arr_to_str(new HMACSHA256(bytes).ComputeHash(bytes2));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00008944 File Offset: 0x00006B44
		public static string byte_arr_to_str(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00008988 File Offset: 0x00006B88
		public static byte[] str_to_byte_arr(string hex)
		{
			byte[] result;
			try
			{
				int length = hex.Length;
				byte[] array = new byte[length / 2];
				for (int i = 0; i < length; i += 2)
				{
					array[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
				}
				result = array;
			}
			catch
			{
				api.error("The session has ended, open program again.");
				Environment.Exit(0);
				result = null;
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000089F0 File Offset: 0x00006BF0
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static bool CheckStringsFixedTime(string str1, string str2)
		{
			if (str1.Length != str2.Length)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < str1.Length; i++)
			{
				num |= (int)(str1[i] ^ str2[i]);
			}
			return num == 0;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008A38 File Offset: 0x00006C38
		public static string iv_key()
		{
			return Guid.NewGuid().ToString().Substring(0, 16);
		}
	}
}
