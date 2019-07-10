
using iReminder.DBServices;
using iReminder.Interfaces;
using iReminder.Models;
using iReminder.Views;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iReminder
{
   
    public partial class MainPage : ContentPage
    {
        private Controllers controller = new Controllers();
        private SQLiteAsyncConnection conn; 
        
        public MainPage()
        {
            InitializeComponent();
            conn = DependencyService.Get<IReminder>().Connection();
            
           
        }

      

        protected async override void OnAppearing()
        {
            base.OnAppearing();
           await conn.CreateTableAsync<UserModel>();
            await signUpEntry.TranslateTo(100, 0, 1500, Easing.BounceIn);
        }

        

        void CheckCharacters(Entry entry, Button button, Label label)
        {
            if(entry.Text.Length < 2)
            {
                SuccessSL.IsVisible = false;
                entry.BackgroundColor = Color.Red;
                button.IsEnabled = false;
            }
            else
            {
                entry.BackgroundColor = Color.Transparent;
                SuccessSL.IsVisible = true;
                label.Text = "Your name is awesome fam!";
                button.IsEnabled = true;
            }
        }

        private void SignUpTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckCharacters(signUpEntry, SignupButton, SuccessLbl);
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            var user = new UserModel
            {
              
                Username = signUpEntry.Text
            };

            var action =  controller.RegisterUser(user);
            
                DependencyService.Get<IToast>().ShowMessage("User created successfully!");
                Navigation.InsertPageBefore(new AllPage(), this);
                await Navigation.PopAsync();
                Settings.UserName = signUpEntry.Text;
            
          
           
        }
    }
}
