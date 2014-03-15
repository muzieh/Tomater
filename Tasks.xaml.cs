using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
		private void ButtonAddClick(object sender, RoutedEventArgs e)
		{
			TaskList.Add(new Task(textTask.Text));
		}

		private Task CurrentItem { get; set; }
		private void DataGrid1CurrentCellChanged(object sender, EventArgs e)
		{
			CurrentItem = dataGrid1.CurrentItem as Task;
		}

		private void ButtonDeleteClick(object sender, RoutedEventArgs e)
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
