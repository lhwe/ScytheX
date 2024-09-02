using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Scythex.Properties
{
	// Token: 0x02000016 RID: 22
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.9.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00008E6A File Offset: 0x0000706A
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00008E7C File Offset: 0x0000707C
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool AutoInject
		{
			get
			{
				return (bool)this["AutoInject"];
			}
			set
			{
				this["AutoInject"] = value;
			}
		}
	}
}
