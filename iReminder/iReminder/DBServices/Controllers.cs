using iReminder.Interfaces;
using iReminder.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iReminder.DBServices
{
   public class Controllers
    {
        private SQLiteAsyncConnection dbconnection;
        public Controllers()
        {
            dbconnection = DependencyService.Get<IReminder>().Connection();    
        }

        //used to create the user table and create a user
        public async Task<bool> RegisterUser(string username)
        {
           await dbconnection.CreateTableAsync<UserModel>();
            await dbconnection.InsertAsync(username);
            return true;
        }

        //used to create user reminder
        public async Task<bool> AddReminder(Reminder reminder)
        {
           await dbconnection.CreateTableAsync<Reminder>();
           await dbconnection.InsertAsync(reminder);
            return true;
        }
        
        //checks if the Reminder table is empty or not and peform an action
       private async Task CheckIfDataExist(ContentView emptyCV, ContentView notEmptyCV)
        {
            var data = await dbconnection.Table<Reminder>().CountAsync();
            if(data <= 0)
            {
                emptyCV.IsVisible = true;
                notEmptyCV.IsVisible = false;
            }
            else
            {
                emptyCV.IsVisible = false;
                notEmptyCV.IsVisible = true;
            }
        }





    }
}
