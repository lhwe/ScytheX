using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scythex.Properties;

namespace ScythexReborn
{
	// Token: 0x0200000C RID: 12
	public partial class LoaderWindow : Window
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00005338 File Offset: 0x00003538
		public LoaderWindow()
		{
			this.InitializeComponent();
			base.Topmost = true;
			NameScope.SetNameScope(this, new NameScope());
			base.RegisterName("LoaderWindowElement", this);
			this.Statustext.Content = "Checking version...";
			this.Loaderbar_Copy.Width = 17.0;
			string text = new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version"));
			if (text == "killswitch")
			{
				MessageBox.Show("Scythex is killswitched.", "Scythex");
				Environment.Exit(0);
			}
			if (text == "socket_closed()")
			{
				MessageBox.Show("failed to initialize : socket_closed()", "Scythex");
				Environment.Exit(0);
			}
			LoaderWindow.version != text;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000053FC File Offset: 0x000035FC
		private Task DoWorkAsync()
		{
			LoaderWindow.<DoWorkAsync>d__2 <DoWorkAsync>d__;
			<DoWorkAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DoWorkAsync>d__.<>4__this = this;
			<DoWorkAsync>d__.<>1__state = -1;
			<DoWorkAsync>d__.<>t__builder.Start<LoaderWindow.<DoWorkAsync>d__2>(ref <DoWorkAsync>d__);
			return <DoWorkAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000543F File Offset: 0x0000363F
		private void FadeOutAnimation_Completed(object sender, EventArgs e)
		{
			Settings.Default.IsFirstStartup = true;
			new KeySystemWindow
			{
				Left = base.Left,
				Top = base.Top
			}.Show();
			base.Close();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005474 File Offset: 0x00003674
		private void UpdatePercentageText(string text)
		{
			if (base.Dispatcher.CheckAccess())
			{
				this.Statustext.Content = text;
				return;
			}
			base.Dispatcher.Invoke<object>(() => this.Statustext.Content = text);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000054CC File Offset: 0x000036CC
		private Task PauseAndCheckVersionAsync()
		{
			LoaderWindow.<PauseAndCheckVersionAsync>d__5 <PauseAndCheckVersionAsync>d__;
			<PauseAndCheckVersionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PauseAndCheckVersionAsync>d__.<>4__this = this;
			<PauseAndCheckVersionAsync>d__.<>1__state = -1;
			<PauseAndCheckVersionAsync>d__.<>t__builder.Start<LoaderWindow.<PauseAndCheckVersionAsync>d__5>(ref <PauseAndCheckVersionAsync>d__);
			return <PauseAndCheckVersionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005510 File Offset: 0x00003710
		private Task PauseAndCheckForHotFixesAsync()
		{
			LoaderWindow.<PauseAndCheckForHotFixesAsync>d__6 <PauseAndCheckForHotFixesAsync>d__;
			<PauseAndCheckForHotFixesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PauseAndCheckForHotFixesAsync>d__.<>4__this = this;
			<PauseAndCheckForHotFixesAsync>d__.<>1__state = -1;
			<PauseAndCheckForHotFixesAsync>d__.<>t__builder.Start<LoaderWindow.<PauseAndCheckForHotFixesAsync>d__6>(ref <PauseAndCheckForHotFixesAsync>d__);
			return <PauseAndCheckForHotFixesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005554 File Offset: 0x00003754
		private Task CheckForHotFixesAsync()
		{
			LoaderWindow.<CheckForHotFixesAsync>d__7 <CheckForHotFixesAsync>d__;
			<CheckForHotFixesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckForHotFixesAsync>d__.<>4__this = this;
			<CheckForHotFixesAsync>d__.<>1__state = -1;
			<CheckForHotFixesAsync>d__.<>t__builder.Start<LoaderWindow.<CheckForHotFixesAsync>d__7>(ref <CheckForHotFixesAsync>d__);
			return <CheckForHotFixesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005598 File Offset: 0x00003798
		private Task CheckVersionAsync()
		{
			LoaderWindow.<CheckVersionAsync>d__8 <CheckVersionAsync>d__;
			<CheckVersionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckVersionAsync>d__.<>4__this = this;
			<CheckVersionAsync>d__.<>1__state = -1;
			<CheckVersionAsync>d__.<>t__builder.Start<LoaderWindow.<CheckVersionAsync>d__8>(ref <CheckVersionAsync>d__);
			return <CheckVersionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000055DB File Offset: 0x000037DB
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			((Storyboard)base.FindResource("FadeInStoryboard")).Begin();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000055F4 File Offset: 0x000037F4
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			LoaderWindow.<Exit_Click>d__10 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<LoaderWindow.<Exit_Click>d__10>(ref <Exit_Click>d__);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000562B File Offset: 0x0000382B
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00005634 File Offset: 0x00003834
		private void Loaderbar_Loaded(object sender, RoutedEventArgs e)
		{
			LoaderWindow.<Loaderbar_Loaded>d__12 <Loaderbar_Loaded>d__;
			<Loaderbar_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Loaderbar_Loaded>d__.<>4__this = this;
			<Loaderbar_Loaded>d__.<>1__state = -1;
			<Loaderbar_Loaded>d__.<>t__builder.Start<LoaderWindow.<Loaderbar_Loaded>d__12>(ref <Loaderbar_Loaded>d__);
		}

		// Token: 0x040000FD RID: 253
		private static readonly string version = "v1.01.2";
	}
}
