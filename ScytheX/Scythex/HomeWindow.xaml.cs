using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScythexReborn
{
	// Token: 0x0200000A RID: 10
	public partial class HomeWindow : Window
	{
		// Token: 0x0600004F RID: 79 RVA: 0x000043F8 File Offset: 0x000025F8
		public HomeWindow()
		{
			this.InitializeComponent();
			NameScope.SetNameScope(this, new NameScope());
			base.RegisterName("HomeWindowElement", this);
			base.Topmost = true;
			WebClient webClient = new WebClient();
			string text = "v1.00.9";
			string text2 = webClient.DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version"));
			string text3 = webClient.DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexannc/master3/announcement"));
			this.AnnouncementBlock.Text = text3;
			if (text2 == "killswitch")
			{
				Thread.Sleep(1500);
				MessageBox.Show("Scythex has been killswitched, please check the\ndiscord for updates.", "Scythex");
				Environment.Exit(0);
			}
			if (text == text2)
			{
				this._timer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMinutes(1.0)
				};
				this._timer.Tick += this.OnTimerTick;
				this._timer.Start();
				this.UpdateUIAsync();
				this.UpdateUI();
			}
			else
			{
				Thread.Sleep(1500);
				MessageBox.Show("This Scythex version is outdated,\nplease redownload in the discord server or on site.", "Scythex");
				Environment.Exit(0);
			}
			this.actualVersion.Content = text;
			if (text != text2)
			{
				this.upto.Content = "this version is outdated!";
			}
			string text4 = Environment.UserName;
			if (text4 == "adus0")
			{
				text4 = "syncvs";
			}
			this.WELCOME_USE.Content = "Welcome, " + text4 + ".";
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004568 File Offset: 0x00002768
		private void OnTimerTick(object sender, EventArgs e)
		{
			HomeWindow.<OnTimerTick>d__2 <OnTimerTick>d__;
			<OnTimerTick>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnTimerTick>d__.<>4__this = this;
			<OnTimerTick>d__.<>1__state = -1;
			<OnTimerTick>d__.<>t__builder.Start<HomeWindow.<OnTimerTick>d__2>(ref <OnTimerTick>d__);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000045A0 File Offset: 0x000027A0
		private Task UpdateUI()
		{
			HomeWindow.<UpdateUI>d__3 <UpdateUI>d__;
			<UpdateUI>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateUI>d__.<>4__this = this;
			<UpdateUI>d__.<>1__state = -1;
			<UpdateUI>d__.<>t__builder.Start<HomeWindow.<UpdateUI>d__3>(ref <UpdateUI>d__);
			return <UpdateUI>d__.<>t__builder.Task;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000045E4 File Offset: 0x000027E4
		private Task UpdateUIAsync()
		{
			HomeWindow.<UpdateUIAsync>d__4 <UpdateUIAsync>d__;
			<UpdateUIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateUIAsync>d__.<>4__this = this;
			<UpdateUIAsync>d__.<>1__state = -1;
			<UpdateUIAsync>d__.<>t__builder.Start<HomeWindow.<UpdateUIAsync>d__4>(ref <UpdateUIAsync>d__);
			return <UpdateUIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004628 File Offset: 0x00002828
		private Task<string> GetRepoLastUpdatedAsync(string repoUrl)
		{
			HomeWindow.<GetRepoLastUpdatedAsync>d__5 <GetRepoLastUpdatedAsync>d__;
			<GetRepoLastUpdatedAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<GetRepoLastUpdatedAsync>d__.repoUrl = repoUrl;
			<GetRepoLastUpdatedAsync>d__.<>1__state = -1;
			<GetRepoLastUpdatedAsync>d__.<>t__builder.Start<HomeWindow.<GetRepoLastUpdatedAsync>d__5>(ref <GetRepoLastUpdatedAsync>d__);
			return <GetRepoLastUpdatedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000466C File Offset: 0x0000286C
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			HomeWindow.<Exit_Click>d__6 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<HomeWindow.<Exit_Click>d__6>(ref <Exit_Click>d__);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000046A3 File Offset: 0x000028A3
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000046AC File Offset: 0x000028AC
		private void ExecutionTab_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.9, TimeSpan.FromSeconds(0.5));
			doubleAnimation.Completed += this.FadeOutAnimation_Completed;
			base.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004702 File Offset: 0x00002902
		private void FadeOutAnimation_Completed(object sender, EventArgs e)
		{
			new MainWindow
			{
				Left = base.Left,
				Top = base.Top
			}.Show();
			base.Close();
			base.Close();
			base.Close();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004738 File Offset: 0x00002938
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000474C File Offset: 0x0000294C
		private void Joindiscord_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004778 File Offset: 0x00002978
		private void Joindiscord_Copy_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythex.lol";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000047A4 File Offset: 0x000029A4
		private void Joindiscord_Copy1_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Credits not yet implemented", "Scythex");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000047B6 File Offset: 0x000029B6
		private void HomeTab_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000047B8 File Offset: 0x000029B8
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DoubleAnimation animation = new DoubleAnimation(0.9, 1.0, TimeSpan.FromSeconds(0.5));
			base.BeginAnimation(UIElement.OpacityProperty, animation);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000047FC File Offset: 0x000029FC
		private void Purchase_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythexrbx.mysellix.io";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x040000AE RID: 174
		private DispatcherTimer _timer;
	}
}
