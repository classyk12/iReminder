using iReminder.DBServices;
using iReminder.Interfaces;
using iReminder.Models;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iReminder.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyReminderPage 
	{
        Controllers controller = new Controllers();
       private SQLiteAsyncConnection conn;
		public MyReminderPage ()
		{
			InitializeComponent ();
            conn = DependencyService.Get<IReminder>().Connection();
           
        }

        protected async override void OnAppearing()
        {

            await conn.CreateTableAsync<Reminder>();
            await conn.CreateTableAsync<Tasks>();
            await controller.CheckIfDataExist(EmptyContentView,NotEmptyContentView);
            
            base.OnAppearing();
        }

        private async void NewReminderClicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new NewReminderPage(), true);
        }
    }
}