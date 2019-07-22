using iReminder.DBServices;
using iReminder.Interfaces;
using iReminder.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iReminder.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewReminderPage
	{
        public ObservableCollection<Tasks> tasks;
        public List<Tasks> TaskList;
        public Controllers controller = new Controllers(); 
        public Command<Tasks> RemoveCommand
        {
            get {
                return new Command<Tasks>((task) => tasks.Remove(task));

            }
        }
		public NewReminderPage ()
		{
			InitializeComponent ();
            tasks = new ObservableCollection<Tasks>();
          
		}

        private async void CloseNewReminderPage(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
           
        }


        private async void ShowTaskCVAction(object sender, EventArgs e)
        {
            
            AllSL.IsVisible = false;
            AddTaskCV.IsVisible = true;
            await AddTaskCV.FadeTo(1, 250, Easing.SinIn);
        }

        private async void CloseTaskAction(object sender, EventArgs e)
        {
            await AddTaskCV.FadeTo(0, 250, Easing.Linear);
            AddTaskCV.IsVisible = false;
            AllSL.IsVisible = true;
          
            
        }

        private async void AddTaskAction (object sender, EventArgs e)
        {
            var data = new Tasks { Events = TaskEntry.Text };
           
            if(string.IsNullOrWhiteSpace(TaskEntry.Text))
            {
              await DisplayAlert("Hey boss!", "Enter a task", "ok");
                return;
            }

            else
            {
                tasks.Add(data);
                TaskEntry.Text = "";
                TasksLV.ItemsSource = tasks;
                await AddTaskCV.FadeTo(0, 250, Easing.Linear);
                AddTaskCV.IsVisible = false;
                AllSL.IsVisible = true;
            }
           
        }

        private void TasksLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView item = (ListView)sender;
            item.SelectedItem = null;
        }

        private void DeleteATask(object sender, EventArgs e)
        {
            var image = sender as Image;
            var task = image.BindingContext as Tasks;
            RemoveCommand.Execute(task);
        }

        private async void AddReminder (object sender, EventArgs e)
        {
            List<Tasks> AllmyTasks = new List<Tasks>(tasks);

            var ReminderData = new Reminder
            {
                ReminderCreationDate = DateTime.Now,
                Status = false,
                ReminderLocation = locationentry.Text,
               ThingsTodo = new List<Tasks>(AllmyTasks)
            };
    
           
                      
                           

            if(tasks.Count <= 0 || string.IsNullOrWhiteSpace(locationentry.Text))
            {
               await DisplayAlert("Hey boss!", "I need a location and at least a task to get this done", "ok");
            }
            else
            {
                await controller.AddReminder(ReminderData);
            
                DependencyService.Get<IToast>().ShowMessage("Reminder Created boss");
                locationentry.Text = "";
                
                tasks.Clear();
            }

            

        }
    }
}