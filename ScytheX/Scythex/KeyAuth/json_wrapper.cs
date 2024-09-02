using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace KeyAuth
{
	// Token: 0x02000011 RID: 17
	public class json_wrapper
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00008A60 File Offset: 0x00006C60
		public static bool is_serializable(Type to_check)
		{
			return to_check.IsSerializable || to_check.IsDefined(typeof(DataContractAttribute), true);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00008A80 File Offset: 0x00006C80
		public json_wrapper(object obj_to_work_with)
		{
			this.current_object = obj_to_work_with;
			Type type = this.current_object.GetType();
			this.serializer = new DataContractJsonSerializer(type);
			if (!json_wrapper.is_serializable(type))
			{
				throw new Exception(string.Format("the object {0} isn't a serializable", this.current_object));
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00008AD0 File Offset: 0x00006CD0
		public object string_to_object(string json)
		{
			object result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.Default.GetBytes(json)))
			{
				result = this.serializer.ReadObject(memoryStream);
			}
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008B18 File Offset: 0x00006D18
		public T string_to_generic<T>(string json)
		{
			return (T)((object)this.string_to_object(json));
		}

		// Token: 0x04000166 RID: 358
		private DataContractJsonSerializer serializer;

		// Token: 0x04000167 RID: 359
		private object current_object;
	}
}
