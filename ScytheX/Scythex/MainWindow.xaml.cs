using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using KeyAuth;
using Microsoft.Web.WebView2.Wpf;
using Newtonsoft.Json;
using Scythex.Properties;

namespace ScythexReborn
{
	// Token: 0x0200000E RID: 14
	public partial class MainWindow : Window
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000062B8 File Offset: 0x000044B8
		private static string CurrentDirectory { get; } = Environment.CurrentDirectory;

		// Token: 0x0600009D RID: 157 RVA: 0x000062C0 File Offset: 0x000044C0
		public MainWindow()
		{
			MainWindow.KeyAuthApp.init();
			try
			{
				this.InitializeComponent();
				this.LoadItemsFromFolder();
				bool topmost = Settings.Default.Topmost;
				base.Topmost = topmost;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Initialization Error: " + ex.Message);
			}
			bool autoInject = Settings.Default.AutoInject;
			if (autoInject)
			{
				MainWindow.checkTimer = new System.Timers.Timer(5000.0);
				MainWindow.checkTimer.Elapsed += MainWindow.OnTimedEvent;
				MainWindow.checkTimer.AutoReset = true;
				MainWindow.checkTimer.Enabled = true;
			}
			else if (!autoInject)
			{
				MainWindow.checkTimer = new System.Timers.Timer(5000.0);
				MainWindow.checkTimer.Elapsed += MainWindow.OnTimedEvent;
				MainWindow.checkTimer.AutoReset = true;
				MainWindow.checkTimer.Enabled = false;
			}
			MainWindow.listening = new System.Timers.Timer(5000.0);
			MainWindow.listening.Elapsed += MainWindow.OnCheckEvent;
			MainWindow.listening.AutoReset = true;
			MainWindow.listening.Enabled = true;
			MainWindow.keychecker = new System.Timers.Timer(300000.0);
			MainWindow.keychecker.Elapsed += MainWindow.OnKeyCheck;
			MainWindow.keychecker.AutoReset = true;
			MainWindow.keychecker.Enabled = true;
			this.Editor.Source = new Uri(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Monaco\\index.html"));
			base.Topmost = true;
			if (new WebClient().DownloadString(new Uri("https://raw.githubusercontent.com/syncvs/scythexver/master2/version")) == "killswitch")
			{
				Thread.Sleep(1500);
				MessageBox.Show("Scythex has been killswitched, please check the\ndiscord for updates.", "Scythex");
				Environment.Exit(0);
			}
			this.Editor.Visibility = Visibility.Visible;
			Console.Write(Settings.Default.Key);
			this.LoadItemsFromFolder();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000064D8 File Offset: 0x000046D8
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

		// Token: 0x0600009F RID: 159 RVA: 0x00006598 File Offset: 0x00004798
		private Task<string> editorv()
		{
			MainWindow.<editorv>d__12 <editorv>d__;
			<editorv>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<editorv>d__.<>4__this = this;
			<editorv>d__.<>1__state = -1;
			<editorv>d__.<>t__builder.Start<MainWindow.<editorv>d__12>(ref <editorv>d__);
			return <editorv>d__.<>t__builder.Task;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000065DC File Offset: 0x000047DC
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<Exit_Click>d__13 <Exit_Click>d__;
			<Exit_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Exit_Click>d__.<>4__this = this;
			<Exit_Click>d__.<>1__state = -1;
			<Exit_Click>d__.<>t__builder.Start<MainWindow.<Exit_Click>d__13>(ref <Exit_Click>d__);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006613 File Offset: 0x00004813
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Minimized;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000661C File Offset: 0x0000481C
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006630 File Offset: 0x00004830
		private void Execute_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<Execute_Click>d__16 <Execute_Click>d__;
			<Execute_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Execute_Click>d__.<>4__this = this;
			<Execute_Click>d__.<>1__state = -1;
			<Execute_Click>d__.<>t__builder.Start<MainWindow.<Execute_Click>d__16>(ref <Execute_Click>d__);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006667 File Offset: 0x00004867
		private void Clear_Click(object sender, RoutedEventArgs e)
		{
			this.Editor.ExecuteScriptAsync("editor.setValue('');");
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000667C File Offset: 0x0000487C
		private void Save_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<Save_Click>d__18 <Save_Click>d__;
			<Save_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Save_Click>d__.<>4__this = this;
			<Save_Click>d__.<>1__state = -1;
			<Save_Click>d__.<>t__builder.Start<MainWindow.<Save_Click>d__18>(ref <Save_Click>d__);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000066B4 File Offset: 0x000048B4
		private void Open_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<Open_Click>d__19 <Open_Click>d__;
			<Open_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Open_Click>d__.<>4__this = this;
			<Open_Click>d__.<>1__state = -1;
			<Open_Click>d__.<>t__builder.Start<MainWindow.<Open_Click>d__19>(ref <Open_Click>d__);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000066EC File Offset: 0x000048EC
		private void Inject_Click(object sender, RoutedEventArgs e)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = "injector.exe",
				UseShellExecute = true,
				Verb = "runas",
				CreateNoWindow = false
			};
			try
			{
				Process.Start(startInfo);
			}
			catch (Win32Exception ex)
			{
				Console.WriteLine("Injection failed: " + ex.Message);
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00006754 File Offset: 0x00004954
		private void ButtonSelection_Click(object sender, RoutedEventArgs e)
		{
			if (this.Scriptslist.Visibility == Visibility.Visible)
			{
				RotateTransform rotateTransform = new RotateTransform();
				this.ChevronSel.RenderTransform = rotateTransform;
				this.UpdateRotationCenter(rotateTransform);
				DoubleAnimation animation = new DoubleAnimation
				{
					From = new double?(0.0),
					To = new double?((double)-180),
					Duration = new Duration(TimeSpan.FromSeconds(0.3))
				};
				rotateTransform.BeginAnimation(RotateTransform.AngleProperty, animation);
				this.Scriptslist.Visibility = Visibility.Hidden;
				return;
			}
			if (this.Scriptslist.Visibility == Visibility.Hidden)
			{
				RotateTransform rotateTransform2 = new RotateTransform();
				this.ChevronSel.RenderTransform = rotateTransform2;
				this.UpdateRotationCenter(rotateTransform2);
				DoubleAnimation animation2 = new DoubleAnimation
				{
					From = new double?((double)-180),
					To = new double?(0.0),
					Duration = new Duration(TimeSpan.FromSeconds(0.3))
				};
				rotateTransform2.BeginAnimation(RotateTransform.AngleProperty, animation2);
				this.Scriptslist.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00006867 File Offset: 0x00004A67
		private void UpdateRotationCenter(RotateTransform rotateTransform)
		{
			rotateTransform.CenterX = this.ChevronSel.ActualWidth / 2.0;
			rotateTransform.CenterY = this.ChevronSel.ActualHeight / 2.0;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000068A0 File Offset: 0x00004AA0
		private void Scriptslist_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.Scriptslist.SelectedIndex != -1)
			{
				string str = JsonConvert.SerializeObject(File.ReadAllText("scripts\\" + this.Scriptslist.SelectedItem.ToString()));
				this.Editor.ExecuteScriptAsync("editor.setValue(" + str + ");");
			}
			MainWindow._counter++;
			Console.WriteLine(string.Format("Selected : {0} times", MainWindow._counter));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006928 File Offset: 0x00004B28
		private void PopulateScriptsList(string directoryPath)
		{
			try
			{
				string[] array = Directory.GetFiles(directoryPath, "*.txt").Concat(Directory.GetFiles(directoryPath, "*.lua")).ToArray<string>();
				this.Scriptslist.Items.Clear();
				foreach (string path in array)
				{
					this.Scriptslist.Items.Add(System.IO.Path.GetFileName(path));
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occurred: " + ex.Message);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000069BC File Offset: 0x00004BBC
		private void Scriptslist_MouseEnter(object sender, MouseEventArgs e)
		{
			string path = this.ButtonSelection.Content.ToString();
			this.Scriptslist.Items.Clear();
			this.Scriptslist.Items.Clear();
			this.Scriptslist.Items.Clear();
			string[] files = Directory.GetFiles(path, "*.txt");
			string[] files2 = Directory.GetFiles(path, "*.lua");
			foreach (string path2 in files)
			{
				this.Scriptslist.Items.Add(System.IO.Path.GetFileName(path2));
			}
			foreach (string path3 in files2)
			{
				this.Scriptslist.Items.Add(System.IO.Path.GetFileName(path3));
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006A7A File Offset: 0x00004C7A
		private void Scriptslist_MouseLeave(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006A7C File Offset: 0x00004C7C
		private void HomeTab_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006A7E File Offset: 0x00004C7E
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006A80 File Offset: 0x00004C80
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

		// Token: 0x060000B1 RID: 177 RVA: 0x00006AB6 File Offset: 0x00004CB6
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

		// Token: 0x060000B2 RID: 178 RVA: 0x00006AEC File Offset: 0x00004CEC
		private void LogoName_MouseDown(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006AF0 File Offset: 0x00004CF0
		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			if (MainWindow.IsProcessRunning("RobloxPlayerBeta"))
			{
				if (!MainWindow.hasLaunched)
				{
					ProcessStartInfo startInfo = new ProcessStartInfo
					{
						FileName = "injector.exe",
						UseShellExecute = true,
						Verb = "runas",
						CreateNoWindow = false
					};
					try
					{
						Process.Start(startInfo);
						return;
					}
					catch (Win32Exception ex)
					{
						Console.WriteLine("Injection failed: " + ex.Message);
						return;
					}
				}
				if (MainWindow.hasLaunched)
				{
					MainWindow.hasLaunched = false;
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006B78 File Offset: 0x00004D78
		private static void OnCheckEvent(object source, ElapsedEventArgs e)
		{
			if (!MainWindow.IsProcessRunning("RobloxPlayerBeta"))
			{
				foreach (Process process in Process.GetProcessesByName("injector"))
				{
					try
					{
						process.Kill();
						process.WaitForExit();
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006BD0 File Offset: 0x00004DD0
		private static void OnKeyCheck(object source, ElapsedEventArgs e)
		{
			MainWindow.<OnKeyCheck>d__35 <OnKeyCheck>d__;
			<OnKeyCheck>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnKeyCheck>d__.<>1__state = -1;
			<OnKeyCheck>d__.<>t__builder.Start<MainWindow.<OnKeyCheck>d__35>(ref <OnKeyCheck>d__);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006C00 File Offset: 0x00004E00
		private static void whatthefuck()
		{
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
			Environment.Exit(0);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006CB5 File Offset: 0x00004EB5
		private static bool IsProcessRunning(string processName)
		{
			return Process.GetProcessesByName(processName).Length != 0;
		}

		// Token: 0x04000132 RID: 306
		public static api KeyAuthApp = new api("Access", "SHcibngqEj", "60fd9f1c2c5e41b8710f18e7c4a7c644b771b3be7258c6423a8290817e710b56", "1.1", null);

		// Token: 0x04000133 RID: 307
		private static System.Timers.Timer checkTimer;

		// Token: 0x04000134 RID: 308
		private static System.Timers.Timer listening;

		// Token: 0x04000135 RID: 309
		private static System.Timers.Timer keychecker;

		// Token: 0x04000136 RID: 310
		private static bool hasLaunched = false;

		// Token: 0x04000138 RID: 312
		private string folderPath = MainWindow.CurrentDirectory + "\\scripts";

		// Token: 0x04000139 RID: 313
		private const long PLATO_ID = 41083L;

		// Token: 0x0400013A RID: 314
		private static int _counter = 0;
	}
}
