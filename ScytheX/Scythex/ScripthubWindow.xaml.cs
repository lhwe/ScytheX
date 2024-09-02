using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace ScythexReborn
{
	// Token: 0x02000005 RID: 5
	public partial class ScripthubWindow : Window
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002131 File Offset: 0x00000331
		public ScripthubWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000213F File Offset: 0x0000033F
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				base.DragMove();
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002150 File Offset: 0x00000350
		private void ExecutionTab_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002152 File Offset: 0x00000352
		private void SettingsTab_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002154 File Offset: 0x00000354
		private void Minimize_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002156 File Offset: 0x00000356
		private void Exit_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
