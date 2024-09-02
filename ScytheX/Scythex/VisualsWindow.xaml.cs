using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
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
	// Token: 0x02000009 RID: 9
	public partial class VisualsWindow : Window
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003A84 File Offset: 0x00001C84
		public VisualsWindow()
		{
			this.InitializeComponent();
			bool topmost = Settings.Default.Topmost;
			base.Topmost = topmost;
			RotateTransform rotateTransform = new RotateTransform();
			this.VisualChevronSel.RenderTransform = rotateTransform;
			this.VisualChevronSel.RenderTransformOrigin = new Point(0.5, 0.5);
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

		// Token: 0x0600003F RID: 63 RVA: 0x00003B44 File Offset: 0x00001D44
		private void Tracers_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Tracers.Content.ToString() == "")
				{
					this.Tracers.Content = "";
					this.Tracers.Background = this.EXECUTIONTABINDICATOR.Fill;
					Settings.Default.Topmost = true;
					this.Topmost = true;
					Settings.Default.Save();
				}
				else if (this.Tracers.Content.ToString() == "")
				{
					this.Tracers.Content = "";
					this.Tracers.Background = this.COLOURBORDER.Fill;
					Settings.Default.Topmost = false;
					this.Topmost = false;
					Settings.Default.Save();
				}
				this.Tracers.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Tracers.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003BDC File Offset: 0x00001DDC
		private void Boxes_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Boxes.Content.ToString() == "")
				{
					this.Boxes.Content = "";
					this.Boxes.Background = this.EXECUTIONTABINDICATOR.Fill;
					Settings.Default.Topmost = true;
					this.Topmost = true;
					Settings.Default.Save();
				}
				else if (this.Boxes.Content.ToString() == "")
				{
					this.Boxes.Content = "";
					this.Boxes.Background = this.COLOURBORDER.Fill;
					Settings.Default.Topmost = false;
					this.Topmost = false;
					Settings.Default.Save();
				}
				this.Boxes.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Boxes.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003C74 File Offset: 0x00001E74
		private void Outlines_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Outlines.Content.ToString() == "")
				{
					this.Outlines.Content = "";
					this.Outlines.Background = this.EXECUTIONTABINDICATOR.Fill;
					Settings.Default.Topmost = true;
					this.Topmost = true;
					Settings.Default.Save();
				}
				else if (this.Outlines.Content.ToString() == "")
				{
					this.Outlines.Content = "";
					this.Outlines.Background = this.COLOURBORDER.Fill;
					Settings.Default.Topmost = false;
					this.Topmost = false;
					Settings.Default.Save();
				}
				this.Outlines.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Outlines.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003D0C File Offset: 0x00001F0C
		private void Names_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Names.Content.ToString() == "")
				{
					this.Names.Content = "";
					this.Names.Background = this.EXECUTIONTABINDICATOR.Fill;
					Settings.Default.Topmost = true;
					this.Topmost = true;
					Settings.Default.Save();
				}
				else if (this.Names.Content.ToString() == "")
				{
					this.Names.Content = "";
					this.Names.Background = this.COLOURBORDER.Fill;
					Settings.Default.Topmost = false;
					this.Topmost = false;
					Settings.Default.Save();
				}
				this.Names.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Names.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003DA4 File Offset: 0x00001FA4
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003DA6 File Offset: 0x00001FA6
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003DB8 File Offset: 0x00001FB8
		private void JoinDiscord_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003DE4 File Offset: 0x00001FE4
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			VisualsWindow.<Exit_Click>d__8 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<VisualsWindow.<Exit_Click>d__8>(ref <Exit_Click>d__);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00003E1B File Offset: 0x0000201B
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003E24 File Offset: 0x00002024
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

		// Token: 0x06000049 RID: 73 RVA: 0x00003E5A File Offset: 0x0000205A
		private void SettingsTab_Click(object sender, RoutedEventArgs e)
		{
			new SettingsWindow
			{
				Left = base.Left,
				Top = base.Top
			}.Show();
			base.Close();
			base.Close();
			base.Close();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003E90 File Offset: 0x00002090
		private void Purchase_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythexrbx.mysellix.io";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003EBC File Offset: 0x000020BC
		private void LocalPlayer_Click(object sender, RoutedEventArgs e)
		{
			RotateTransform rotateTransform = this.LocalPlayerChevronSel.RenderTransform as RotateTransform;
			if (rotateTransform == null)
			{
				rotateTransform = new RotateTransform();
				this.LocalPlayerChevronSel.RenderTransform = rotateTransform;
			}
			this.LocalPlayerChevronSel.RenderTransformOrigin = new Point(0.5, 0.5);
			if (rotateTransform.Angle == 0.0)
			{
				DoubleAnimation animation = new DoubleAnimation
				{
					From = new double?(0.0),
					To = new double?((double)180),
					Duration = new Duration(TimeSpan.FromSeconds(0.3))
				};
				rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
				new LocalPlayerWindow
				{
					Left = base.Left,
					Top = base.Top
				}.Show();
				base.Close();
				base.Close();
				base.Close();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003FAC File Offset: 0x000021AC
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
