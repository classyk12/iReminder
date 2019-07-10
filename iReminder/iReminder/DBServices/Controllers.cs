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
        public SQLiteAsyncConnection dbconnection;
        public Controllers()
        {
            dbconnection = DependencyService.Get<IReminder>().Connection();
            

        }

        //used to create the user table and create a user
        public async Task RegisterUser(UserModel user)
        {
           
            await dbconnection.InsertAsync(user);
            
        }

        //used to create user reminder object6
        public async Task<bool> AddReminder(Reminder reminder)
        {
            await dbconnection.CreateTableAsync<Reminder>();
            await dbconnection.InsertAsync(reminder);
            return true;
        }
        
        //checks if the Reminder table is empty or not and peform an action
       public async Task CheckIfDataExist(ContentView emptyCV, ContentView notEmptyCV)
       {
            int data = await dbconnection.Table<Reminder>().CountAsync();
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

        //gets all reminder object from the database
        public async Task<List<Reminder>> GetAllReminder()
        {
           var allreminder = await dbconnection.Table<Reminder>().ToListAsync();
            return allreminder;
        }

        //gets all pending reminder from the database
        public async Task<List<Reminder>> GetActiveReminders()
        {
            var allreminder = await dbconnection.Table<Reminder>().Where(r =>r.Status == false).ToListAsync();
            return allreminder;
        }

        //deletes a reminder object from the database
        public async Task<bool> DeleteReminder(int id)
        {
            var action = await dbconnection.DeleteAsync(id);
            return true;
        }

        //updates a reminder object in the database
        public async Task<bool> UpdateReminder(Reminder reminder )
        {
            var allreminder = await dbconnection.UpdateAsync(reminder);
            return true;
        }

    }
}
