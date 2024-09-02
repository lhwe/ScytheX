using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
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
	// Token: 0x0200000D RID: 13
	public partial class LocalPlayerWindow : Window
	{
		// Token: 0x06000083 RID: 131 RVA: 0x000057CC File Offset: 0x000039CC
		public LocalPlayerWindow()
		{
			this.InitializeComponent();
			bool topmost = Settings.Default.Topmost;
			base.Topmost = topmost;
			RotateTransform rotateTransform = new RotateTransform();
			this.LocalPlayerChevronSel.RenderTransform = rotateTransform;
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
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000588C File Offset: 0x00003A8C
		private void JoinDiscord_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000058B8 File Offset: 0x00003AB8
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000058BA File Offset: 0x00003ABA
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000058CC File Offset: 0x00003ACC
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			LocalPlayerWindow.<Exit_Click>d__7 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<LocalPlayerWindow.<Exit_Click>d__7>(ref <Exit_Click>d__);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005903 File Offset: 0x00003B03
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000590C File Offset: 0x00003B0C
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

		// Token: 0x0600008A RID: 138 RVA: 0x00005944 File Offset: 0x00003B44
		private void Purchase_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://scythexrbx.mysellix.io";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005970 File Offset: 0x00003B70
		private void InfiniteJump_Click(object sender, RoutedEventArgs e)
		{
			LocalPlayerWindow.<InfiniteJump_Click>d__11 <InfiniteJump_Click>d__;
			<InfiniteJump_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<InfiniteJump_Click>d__.<>4__this = this;
			<InfiniteJump_Click>d__.<>1__state = -1;
			<InfiniteJump_Click>d__.<>t__builder.Start<LocalPlayerWindow.<InfiniteJump_Click>d__11>(ref <InfiniteJump_Click>d__);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000059A7 File Offset: 0x00003BA7
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

		// Token: 0x0600008D RID: 141 RVA: 0x000059E0 File Offset: 0x00003BE0
		private void TextBox_MouseEnter(object sender, MouseEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (textBox != null)
			{
				this.originalText = textBox.Text;
				textBox.Text = "";
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005A0E File Offset: 0x00003C0E
		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !LocalPlayerWindow.IsTextAllowed(e.Text);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005A24 File Offset: 0x00003C24
		private static bool IsTextAllowed(string text)
		{
			return !new Regex("[^0-9]+").IsMatch(text);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005A3C File Offset: 0x00003C3C
		private void TextBox_MouseLeave(object sender, MouseEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (textBox != null)
			{
				if (string.IsNullOrWhiteSpace(textBox.Text))
				{
					textBox.Text = this.originalText;
					return;
				}
			}
			else
			{
				this.text = this.wstextbox.Text;
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005A80 File Offset: 0x00003C80
		private void TextBox_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (textBox != null)
			{
				DataObject.RemovePastingHandler(textBox, new DataObjectPastingEventHandler(this.TextBox_Pasting));
				DataObject.AddPastingHandler(textBox, new DataObjectPastingEventHandler(this.TextBox_Pasting));
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005ABC File Offset: 0x00003CBC
		private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{
			if (e.DataObject.GetDataPresent(typeof(string)))
			{
				if (!LocalPlayerWindow.IsTextAllowed((string)e.DataObject.GetData(typeof(string))))
				{
					e.CancelCommand();
					return;
				}
			}
			else
			{
				e.CancelCommand();
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005B10 File Offset: 0x00003D10
		private void Fly_Click(object sender, RoutedEventArgs e)
		{
			DoubleAnimation fadeInAnimation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.3));
			DoubleAnimation doubleAnimation = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(0.3));
			doubleAnimation.Completed += delegate(object s, EventArgs a)
			{
				if (this.Fly.Content.ToString() == "")
				{
					this.Fly.Content = "";
					this.Fly.Background = this.EXECUTIONTABINDICATOR.Fill;
				}
				else if (this.InfiniteJump.Content.ToString() == "")
				{
					this.Fly.Content = "";
					this.Fly.Background = this.COLOURBORDER.Fill;
				}
				this.Fly.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
			};
			this.Fly.BeginAnimation(UIElement.OpacityProperty, doubleAnimation);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005BA8 File Offset: 0x00003DA8
		private void ESPTab_Click(object sender, RoutedEventArgs e)
		{
			RotateTransform rotateTransform = this.VisualChevronSel.RenderTransform as RotateTransform;
			if (rotateTransform == null)
			{
				rotateTransform = new RotateTransform();
				this.VisualChevronSel.RenderTransform = rotateTransform;
			}
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
				new VisualsWindow
				{
					Left = base.Left,
					Top = base.Top
				}.Show();
				base.Close();
				base.Close();
				base.Close();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005C98 File Offset: 0x00003E98
		private void wstextbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			LocalPlayerWindow.<wstextbox_TextChanged>d__21 <wstextbox_TextChanged>d__;
			<wstextbox_TextChanged>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<wstextbox_TextChanged>d__.sender = sender;
			<wstextbox_TextChanged>d__.<>1__state = -1;
			<wstextbox_TextChanged>d__.<>t__builder.Start<LocalPlayerWindow.<wstextbox_TextChanged>d__21>(ref <wstextbox_TextChanged>d__);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005CD0 File Offset: 0x00003ED0
		private void wstextbox_KeyDown(object sender, KeyEventArgs e)
		{
			LocalPlayerWindow.<wstextbox_KeyDown>d__22 <wstextbox_KeyDown>d__;
			<wstextbox_KeyDown>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<wstextbox_KeyDown>d__.sender = sender;
			<wstextbox_KeyDown>d__.e = e;
			<wstextbox_KeyDown>d__.<>1__state = -1;
			<wstextbox_KeyDown>d__.<>t__builder.Start<LocalPlayerWindow.<wstextbox_KeyDown>d__22>(ref <wstextbox_KeyDown>d__);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005D10 File Offset: 0x00003F10
		private void jptextbox_KeyDown(object sender, KeyEventArgs e)
		{
			LocalPlayerWindow.<jptextbox_KeyDown>d__23 <jptextbox_KeyDown>d__;
			<jptextbox_KeyDown>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<jptextbox_KeyDown>d__.sender = sender;
			<jptextbox_KeyDown>d__.e = e;
			<jptextbox_KeyDown>d__.<>1__state = -1;
			<jptextbox_KeyDown>d__.<>t__builder.Start<LocalPlayerWindow.<jptextbox_KeyDown>d__23>(ref <jptextbox_KeyDown>d__);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005D50 File Offset: 0x00003F50
		private void jptextbox_TextChanged(object sender, TextChangedEventArgs e)
		{
			LocalPlayerWindow.<jptextbox_TextChanged>d__24 <jptextbox_TextChanged>d__;
			<jptextbox_TextChanged>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<jptextbox_TextChanged>d__.sender = sender;
			<jptextbox_TextChanged>d__.<>1__state = -1;
			<jptextbox_TextChanged>d__.<>t__builder.Start<LocalPlayerWindow.<jptextbox_TextChanged>d__24>(ref <jptextbox_TextChanged>d__);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005D88 File Offset: 0x00003F88
		private void Purchasehelp_Click(object sender, RoutedEventArgs e)
		{
			string fileName = "https://discord.gg/getscythex";
			Process.Start(new ProcessStartInfo
			{
				FileName = fileName,
				UseShellExecute = true
			});
		}

		// Token: 0x04000108 RID: 264
		private string originalText;

		// Token: 0x04000109 RID: 265
		private string text;

		// Token: 0x0400010A RID: 266
		private string text1;
	}
}
