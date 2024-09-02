using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Scythex.Properties
{
	// Token: 0x02000015 RID: 21
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00008DD6 File Offset: 0x00006FD6
		internal Resources()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00008DDE File Offset: 0x00006FDE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Scythex.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00008E0A File Offset: 0x0000700A
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00008E11 File Offset: 0x00007011
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400016B RID: 363
		private static ResourceManager resourceMan;

		// Token: 0x0400016C RID: 364
		private static CultureInfo resourceCulture;
	}
}
