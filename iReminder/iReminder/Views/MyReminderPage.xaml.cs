using iReminder.DBServices;
using iReminder.Interfaces;
using iReminder.Models;
using Rg.Plugins.Popup.Services;
using SQLite;
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
	public partial class MyReminderPage : ContentPage
	{
        Controllers controller = new Controllers();
        private SQLiteAsyncConnection conn;
        public ObservableCollection<Reminder> AllReminder;
        //public ObservableCollection<Tasks> AllTasks;


            //used to remove a reminder object from the observable collection
        public Command<Reminder> RemoveCommand
        {
            get
            {
                return new Command<Reminder>((reminder) => AllReminder.Remove(reminder));

            }
        }

        public MyReminderPage ()
		{
			InitializeComponent ();
            conn = DependencyService.Get<IReminder>().Connection();
            
          
            //AllReminder = new ObservableCollection<Reminder>();

        }

        protected async override void OnAppearing()
        {
            WelcomeLabel.Text = "Hey " + Settings.UserName.ToLower();
            await conn.CreateTableAsync<Reminder>();
            await conn.CreateTableAsync<Tasks>();
            await controller.CheckIfDataExist(EmptyContentView, NotEmptyContentView);
            GetData();

            base.OnAppearing();
        }

        private async void GetData()
        {

            var data = await controller.GetAllReminder();
           
            AllReminder = new ObservableCollection<Reminder>(data);
            ReminderLV.ItemsSource = AllReminder;
        }

        private async void NewReminderClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new NewReminderPage(), true);
        }

        private void ReminderLV_Refreshing(object sender, EventArgs e)
        {
            GetData();
            ReminderLV.EndRefresh();
            DependencyService.Get<IToast>().ShowMessage("Reminder List Up to date!");
        }

        private void ReminderLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView listview = sender as ListView;
            listview.SelectedItem = null;
        }

        private async void RemoveReminderAction(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Delete Reminder boss?", "", "", "Go on", "Nah just kidding");
            if(action == "Go on")
            {
                Image image = sender as Image;
                var reminder = image.BindingContext as Reminder;
                RemoveCommand.Execute(reminder);
                await controller.DeleteReminder(reminder);
                DependencyService.Get<IToast>().ShowMessage("All done boss");
            }
            else
            {
                return;
            }
           
        }

        private async void UpdateReminderAction(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var reminder = button.BindingContext as Reminder;
            await PopupNavigation.Instance.PushAsync(new EditReminderPage(reminder),true);
        }

        private async void CloseTaskCV(object sender, EventArgs e)
        {
            await TaskContentView.FadeTo(0, 250, Easing.SpringOut);
            TaskContentView.IsVisible = false;
            NotEmptyContentView.IsVisible = true;
        }

        private void TasksLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listview = sender as ListView;
            listview.SelectedItem = null;
        }

        private async void ViewTasksAction(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var reminder = button.BindingContext as Reminder;
            TasksLV.ItemsSource = reminder.ThingsTodo;
            NotEmptyContentView.IsVisible = false;
            TaskContentView.IsVisible = true;
            await TaskContentView.FadeTo(1,500,Easing.SpringIn);            
        }
    }
}