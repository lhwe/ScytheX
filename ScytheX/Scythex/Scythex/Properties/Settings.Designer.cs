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
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00008E19 File Offset: 0x00007019
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00008E20 File Offset: 0x00007020
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00008E32 File Offset: 0x00007032
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool IsFirstStartup
		{
			get
			{
				return (bool)this["IsFirstStartup"];
			}
			set
			{
				this["IsFirstStartup"] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008E45 File Offset: 0x00007045
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00008E57 File Offset: 0x00007057
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool Topmost
		{
			get
			{
				return (bool)this["Topmost"];
			}
			set
			{
				this["Topmost"] = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00008E8F File Offset: 0x0000708F
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00008EA1 File Offset: 0x000070A1
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Key
		{
			get
			{
				return (string)this["Key"];
			}
			set
			{
				this["Key"] = value;
			}
		}

		// Token: 0x0400016D RID: 365
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
