using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tomater
{
	/// <summary>
	/// Interaction logic for Tasks.xaml
	/// </summary>
	public partial class Tasks : Window
	{
		public Tasks()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			dataGrid1.ItemsSource = TaskList; 
		}

		List<Task> _taskList;
		List<Task> TaskList
		{
			get {
				if (_taskList == null)
				{
					_taskList = new List<Task>();
					_taskList.Add(new Task());
					_taskList.Add(new Task());
				}

				return _taskList;
			}
		}
		private void buttonAdd_Click(object sender, RoutedEventArgs e)
		{
			TaskList.Add(new Task(textTask.Text));
		}

		private Task CurrentItem { get; set; }
		private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
		{
			CurrentItem = dataGrid1.CurrentItem as Task;
		}

		private void buttonDelete_Click(object sender, RoutedEventArgs e)
		{
			
			if (CurrentItem == null)
				return;

			var toDelete = TaskList.FirstOrDefault(t => t.Id.CompareTo(CurrentItem.Id) == 0);
			if (toDelete != null)
			{
				TaskList.Remove(toDelete);
			}
		}

		private void RebindGrid() 
		{
			dataGrid1.ItemsSource = null;
			dataGrid1.ItemsSource = TaskList;
		}

	}
}
