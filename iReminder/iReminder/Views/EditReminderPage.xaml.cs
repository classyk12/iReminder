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
    public partial class EditReminderPage
    {
        public ObservableCollection<Tasks> tasks;
        public List<Tasks> TaskList;
        public Controllers controller = new Controllers();

        public Command<Tasks> RemoveCommand
        {
            get
            {
                return new Command<Tasks>((task) => tasks.Remove(task));

            }
        }

        public Command<Tasks> AddTaskCommand
        {
            get
            {
                return new Command<Tasks>((task) => tasks.Add(task));

            }
        }

        public EditReminderPage(Reminder reminder)
        {
            InitializeComponent();
            tasks = new ObservableCollection<Tasks>();
            Settings.ReminderId= reminder.ReminderId;
            locationentry.Text = reminder.ReminderLocation;
            tasks = new ObservableCollection<Tasks>(reminder.ThingsTodo);
            TasksLV.ItemsSource = tasks;
        }

        private async void CloseNewReminderPage(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(locationentry.Text) || tasks.Count> 0 )
            {
               var action = await DisplayActionSheet("Just making sure boss, looks like you are not done here", "", "", "I know, Just close", "Thats true, Dont close");

                if(action == "I know, Just close")
                {
                    await PopupNavigation.Instance.PopAsync();
                }
                else
                {
                    return;
                }
            }                     

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

        private async void AddTaskAction(object sender, EventArgs e)
        {
            var data = new Tasks { Events = TaskEntry.Text};

            if (string.IsNullOrWhiteSpace(TaskEntry.Text))
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

        private async void AddReminder(object sender, EventArgs e)
        {
            

            //checks if the checkbox is checked or not
            bool check;
            if(ReminderCB.IsChecked == true)
            {
                check = true;            
            }
            else
            {
                check = false;            
            }


            List<Tasks> AllmyTasks = new List<Tasks>(tasks);

            var ReminderData = new Reminder
            {
                ReminderId = Settings.ReminderId,
                ReminderCreationDate = DateTime.Now,
                Status = check,
                ReminderLocation = locationentry.Text,
                ThingsTodo = AllmyTasks,

            };


            if (tasks.Count <= 0 || string.IsNullOrWhiteSpace(locationentry.Text))
            {
                await DisplayAlert("Hey boss!", "I need a location and at least a task to get this done", "ok");
            }
            else
            {
                await controller.UpdateReminder(ReminderData);
                
                DependencyService.Get<IToast>().ShowMessage("Reminder Updated boss");
                locationentry.Text = "";
                ReminderCB.IsChecked = false;
                tasks.Clear();
                await PopupNavigation.Instance.PopAsync();
            }


        }

      
    }
}
