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
	// Token: 0x02000008 RID: 8
	public partial class SettingsWindowInterface : Window
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000031FC File Offset: 0x000013FC
		public SettingsWindowInterface()
		{
			this.InitializeComponent();
			bool topmost = Settings.Default.Topmost;
			if (topmost)
			{
				this.Checkboxtopmost.Content = "";
				this.Checkboxtopmost.Background = this.EXECUTIONTABINDICATOR.Fill;
			}
			base.Topmost = topmost;
			string content = new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version"));
			this.actualVersion.Content = content;
			RotateTransform rotateTransform = new RotateTransform();
			this.UISettingsChevronSel.RenderTransform = rotateTransform;
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
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003306 File Offset: 0x00001506
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003308 File Offset: 0x00001508
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000331C File Offset: 0x0000151C
		private void Editor_Settings_Click(object sender, RoutedEventArgs e)
		{
			RotateTransform rotateTransform = this.EditorChervonSel.RenderTransform as RotateTransform;
			if (rotateTransform == null)
			{
				rotateTransform = new RotateTransform();
				this.EditorChervonSel.RenderTransform = rotateTransform;
			}
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
				new SettingsWindowEditor
				{
					Left = base.Left,
					Top = base.Top
				}.Show();
				base.Close();
				base.Close();
				base.Close();
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003409 File Offset: 0x00001609
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

		// Token: 0x06000034 RID: 52 RVA: 0x00003440 File Offset: 0x00001640
		private void JoinDiscord_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000346C File Offset: 0x0000166C
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindowInterface.<Exit_Click>d__6 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<SettingsWindowInterface.<Exit_Click>d__6>(ref <Exit_Click>d__);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000034A3 File Offset: 0x000016A3
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000034AC File Offset: 0x000016AC
		private void Purchase_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythexrbx.mysellix.io";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000034D8 File Offset: 0x000016D8
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
					Settings.Default.Topmost = true;
					this.Topmost = true;
					Settings.Default.Save();
				}
				else if (this.Checkboxtopmost.Content.ToString() == "")
				{
					this.Checkboxtopmost.Content = "";
					this.Checkboxtopmost.Background = this.COLOURBORDER.Fill;
					Settings.Default.Topmost = false;
					this.Topmost = false;
					Settings.Default.Save();
				}
				this.Checkboxtopmost.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Checkboxtopmost.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003570 File Offset: 0x00001770
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

		// Token: 0x0600003A RID: 58 RVA: 0x000035A8 File Offset: 0x000017A8
		private void Purchasehelp_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000035D4 File Offset: 0x000017D4
		private void Customthemes_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Customthemes.Content.ToString() == "")
				{
					this.Customthemes.Content = "";
					this.Customthemes.Background = this.EXECUTIONTABINDICATOR.Fill;
					Settings.Default.Topmost = true;
					this.Topmost = true;
					Settings.Default.Save();
				}
				else if (this.Customthemes.Content.ToString() == "")
				{
					this.Customthemes.Content = "";
					this.Customthemes.Background = this.COLOURBORDER.Fill;
					Settings.Default.Topmost = false;
					this.Topmost = false;
					Settings.Default.Save();
				}
				this.Customthemes.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Customthemes.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}
	}
}
