using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using KeyAuth;
using Microsoft.Web.WebView2.Wpf;
using Plato;
using Scythex.Properties;

namespace ScythexReborn
{
	// Token: 0x0200000B RID: 11
	public partial class KeySystemWindow : Window
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004BC8 File Offset: 0x00002DC8
		private static string CurrentDirectory { get; } = Environment.CurrentDirectory;

		// Token: 0x06000062 RID: 98 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public KeySystemWindow()
		{
			try
			{
				this.InitializeComponent();
				this.LoadItemsFromFolder();
				base.Topmost = false;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Initialization Error : " + ex.Message);
			}
			try
			{
				if (new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version")) == "killswitch")
				{
					Thread.Sleep(1500);
					MessageBox.Show("Scythex has been killswitched, please check the\ndiscord for updates.", "Scythex");
					Environment.Exit(0);
				}
			}
			catch (WebException ex2)
			{
				MessageBox.Show("Network error : " + ex2.Message);
			}
			catch (Exception ex3)
			{
				MessageBox.Show("Error : " + ex3.Message);
			}
			KeySystemWindow.KeyAuthApp.init();
			this.LoadItemsFromFolder();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004CD4 File Offset: 0x00002ED4
		private void LoadItemsFromFolder()
		{
			this.Scriptslist.Items.Clear();
			if (Directory.Exists(this.folderPath))
			{
				string[] files = Directory.GetFiles(this.folderPath, "*.txt");
				string[] files2 = Directory.GetFiles(this.folderPath, "*.lua");
				string[] array = files;
				for (int i = 0; i < array.Length; i++)
				{
					string fileName = System.IO.Path.GetFileName(array[i]);
					this.Scriptslist.Items.Add(fileName);
				}
				array = files2;
				for (int i = 0; i < array.Length; i++)
				{
					string fileName2 = System.IO.Path.GetFileName(array[i]);
					this.Scriptslist.Items.Add(fileName2);
				}
				return;
			}
			MessageBox.Show("The folder path '" + this.folderPath + "' does not exist.");
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004D94 File Offset: 0x00002F94
		private void ContinueButton_Click(object sender, RoutedEventArgs e)
		{
			TextBox textBox = base.FindName("Keybox") as TextBox;
			if (textBox != null)
			{
				this.ValidateInput(textBox.Text);
				return;
			}
			MessageBox.Show("Key not entered.", "Scythex");
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004DD4 File Offset: 0x00002FD4
		private void ValidateInput(string input)
		{
			KeySystemWindow.<ValidateInput>d__9 <ValidateInput>d__;
			<ValidateInput>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ValidateInput>d__.<>4__this = this;
			<ValidateInput>d__.input = input;
			<ValidateInput>d__.<>1__state = -1;
			<ValidateInput>d__.<>t__builder.Start<KeySystemWindow.<ValidateInput>d__9>(ref <ValidateInput>d__);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004E14 File Offset: 0x00003014
		private void TriggerFadeOutAnimation()
		{
			Storyboard storyboard = base.FindResource("FadeoutItems") as Storyboard;
			if (storyboard != null)
			{
				storyboard.Completed += this.FadeoutItems_Completed;
				storyboard.Begin();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004E4D File Offset: 0x0000304D
		private void FadeoutItems_Completed(object sender, EventArgs e)
		{
			new MainWindow
			{
				Left = base.Left,
				Top = base.Top
			}.Show();
			base.Close();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004E78 File Offset: 0x00003078
		private void Keylink_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string link = new Boost(41083L).GetLink();
				Process.Start(new ProcessStartInfo
				{
					FileName = link,
					UseShellExecute = true
				});
			}
			catch (WebException ex)
			{
				MessageBox.Show("Network Error : " + ex.Message);
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Error : " + ex2.Message);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004F00 File Offset: 0x00003100
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			DoubleAnimation animation = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(0.5));
			base.BeginAnimation(UIElement.OpacityProperty, animation);
			Settings.Default.IsFirstStartup = false;
			Settings.Default.Save();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004F59 File Offset: 0x00003159
		private void Keybox_TextChanged(object sender, TextChangedEventArgs e)
		{
			this.UpdatePlaceholderVisibility();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004F61 File Offset: 0x00003161
		private void Keybox_GotFocus(object sender, RoutedEventArgs e)
		{
			this.UpdatePlaceholderVisibility();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004F69 File Offset: 0x00003169
		private void Keybox_LostFocus(object sender, RoutedEventArgs e)
		{
			this.UpdatePlaceholderVisibility();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004F71 File Offset: 0x00003171
		private void Keybox_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004F73 File Offset: 0x00003173
		private void Keybox_KeyUp(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004F75 File Offset: 0x00003175
		private void UpdatePlaceholderVisibility()
		{
			this.PlaceholderText.Visibility = (string.IsNullOrWhiteSpace(this.Keybox.Text) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x040000D4 RID: 212
		public static api KeyAuthApp = new api("Access", "SHcibngqEj", "60fd9f1c2c5e41b8710f18e7c4a7c644b771b3be7258c6423a8290817e710b56", "1.1", null);

		// Token: 0x040000D6 RID: 214
		private string folderPath = KeySystemWindow.CurrentDirectory + "\\scripts";

		// Token: 0x040000D7 RID: 215
		private const long PLATO_ID = 41083L;
	}
}
