using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
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
	// Token: 0x02000006 RID: 6
	public partial class SettingsWindow : Window
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000233C File Offset: 0x0000053C
		public SettingsWindow()
		{
			this.InitializeComponent();
			bool topmost = Settings.Default.Topmost;
			base.Topmost = topmost;
			string content = new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version"));
			this.actualVersion.Content = content;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002388 File Offset: 0x00000588
		private void JoinDiscord_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023B4 File Offset: 0x000005B4
		private void UpdateRotationCenter(RotateTransform rotateTransform)
		{
			rotateTransform.CenterX = this.EditorChervonSel.ActualWidth / 2.0;
			rotateTransform.CenterY = this.EditorChervonSel.ActualHeight / 2.0;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023EC File Offset: 0x000005EC
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

		// Token: 0x06000015 RID: 21 RVA: 0x000024DC File Offset: 0x000006DC
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

		// Token: 0x06000016 RID: 22 RVA: 0x000025C9 File Offset: 0x000007C9
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025CB File Offset: 0x000007CB
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025DC File Offset: 0x000007DC
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025E4 File Offset: 0x000007E4
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025ED File Offset: 0x000007ED
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002624 File Offset: 0x00000824
		private void Purchase_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythexrbx.mysellix.io";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002650 File Offset: 0x00000850
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

		// Token: 0x0600001D RID: 29 RVA: 0x00002688 File Offset: 0x00000888
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
