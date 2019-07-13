using iReminder.DBServices;
using iReminder.Interfaces;
using iReminder.Models;
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
	public partial class UserSettingsPage : ContentPage
	{
        Controllers controller = new Controllers();
		public UserSettingsPage ()
		{
			InitializeComponent ();
		}

        private async  void UpdateName(object sender, EventArgs e)
        {                    
            await EditSL.FadeTo(1, 1000, Easing.Linear);
            editbtn.IsEnabled = false;
            deletebtn.IsEnabled = false;
        }

        private async void DiscardChanges(object sender, EventArgs e)
        {
            await EditSL.FadeTo(0, 1000, Easing.Linear);
            editbtn.IsEnabled = true;
            deletebtn.IsEnabled = true;
        }

        private async void UpdateUser(object sender, EventArgs e)
        {
            var user = new UserModel { Username = EditUsername.Text };
            await controller.UpdateUser(user);
            Settings.UserName = EditUsername.Text;
            EditUsername.Text = "";
            DependencyService.Get<IToast>().ShowMessage("All done boss!");
            await EditSL.FadeTo(0, 1000, Easing.Linear);
            editbtn.IsEnabled = true;
            deletebtn.IsEnabled = true;

        }

        private void EditUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(EditUsername.Text.Length <= 1)
            {
                EditUsername.BackgroundColor = Color.Red;
                changebtn.IsEnabled = false;
            }
            else
            {
                EditUsername.BackgroundColor = Color.Transparent;
                changebtn.IsEnabled = true;
            }
        }

        private async void DeleteAccount(object sender, EventArgs e)
        {
          var action = await DisplayActionSheet("Oh no boss!", "", "", "This action deletes all your data and is not recoverable, continue?", "I know, continue", "Hell No, Abort mission!");
            if(action == "I know, continue")
            {
                await controller.DeleteAcount();
                Settings.UserName = "";
                Application.Current.MainPage = new MainPage();
              

            }
            else
            {
                return;
            }
          
        }
    }
}