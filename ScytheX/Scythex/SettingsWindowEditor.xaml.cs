using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scythex.Properties;

namespace ScythexReborn
{
	// Token: 0x02000007 RID: 7
	public partial class SettingsWindowEditor : Window
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002A5C File Offset: 0x00000C5C
		public SettingsWindowEditor()
		{
			this.InitializeComponent();
			if (Settings.Default.AutoInject)
			{
				this.Checkboxtopmost.Content = "";
				this.Checkboxtopmost.Background = this.EXECUTIONTABINDICATOR.Fill;
			}
			bool topmost = Settings.Default.Topmost;
			base.Topmost = topmost;
			string content = new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version"));
			this.actualVersion.Content = content;
			RotateTransform rotateTransform = new RotateTransform();
			this.EditorChervonSel.RenderTransform = rotateTransform;
			this.EditorChervonSel.RenderTransformOrigin = new Point(0.5, 0.5);
			if (rotateTransform.Angle == 0.0)
			{
				DoubleAnimation animation = new DoubleAnimation
				{
					From = new double?(0.0),
					To = new double?((double)180),
					Duration = new Duration(TimeSpan.FromSeconds(0.3))
				};
				rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002B6F File Offset: 0x00000D6F
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B71 File Offset: 0x00000D71
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002B84 File Offset: 0x00000D84
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindowEditor.<Exit_Click>d__3 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<SettingsWindowEditor.<Exit_Click>d__3>(ref <Exit_Click>d__);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002BBB File Offset: 0x00000DBB
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private void UISettings_Click(object sender, RoutedEventArgs e)
		{
			RotateTransform rotateTransform = this.UISettingsChevronSel.RenderTransform as RotateTransform;
			if (rotateTransform == null)
			{
				rotateTransform = new RotateTransform();
				this.UISettingsChevronSel.RenderTransform = rotateTransform;
			}
			this.UISettingsChevronSel.RenderTransformOrigin = new Point(0.5, 0.5);
			if (rotateTransform.Angle == 0.0)
			{
				DoubleAnimation animation = new DoubleAnimation
				{
					From = new double?(0.0),
					To = new double?((double)180),
					Duration = new Duration(TimeSpan.FromSeconds(0.3))
				};
				rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
				new SettingsWindowInterface
				{
					Left = base.Left,
					Top = base.Top
				}.Show();
				base.Close();
				base.Close();
				base.Close();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private void JoinDiscord_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002CE0 File Offset: 0x00000EE0
		private void ExecutionTab_Click(object sender, RoutedEventArgs e)
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

		// Token: 0x06000028 RID: 40 RVA: 0x00002D18 File Offset: 0x00000F18
		private void Purchase_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythexrbx.mysellix.io";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002D44 File Offset: 0x00000F44
		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002D48 File Offset: 0x00000F48
		private void Checkboxtopmost_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Checkboxtopmost.Content.ToString() == "")
				{
					this.Checkboxtopmost.Content = "";
					this.Checkboxtopmost.Background = this.EXECUTIONTABINDICATOR.Fill;
					Settings.Default.AutoInject = true;
					Settings.Default.Save();
				}
				else if (this.Checkboxtopmost.Content.ToString() == "")
				{
					this.Checkboxtopmost.Content = "";
					this.Checkboxtopmost.Background = this.COLOURBORDER.Fill;
					Settings.Default.AutoInject = false;
					Settings.Default.Save();
				}
				this.Checkboxtopmost.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Checkboxtopmost.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private void ConfigTab_Click(object sender, RoutedEventArgs e)
		{
			new LocalPlayerWindow
			{
				Left = base.Left,
				Top = base.Top
			}.Show();
			base.Close();
			base.Close();
			base.Close();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002E18 File Offset: 0x00001018
		private void Purchasehelp_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}
	}
}
