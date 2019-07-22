using iReminder.Interfaces;
using iReminder.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        //used to update a user info
        public async Task UpdateUser(UserModel user)
        {
            await dbconnection.UpdateAsync(user);
        }

        //used to drop all table and delete all user settings
        public async Task DeleteAcount()
        {
            await dbconnection.DropTableAsync<Reminder>();
            await dbconnection.DropTableAsync<UserModel>();
            await dbconnection.DropTableAsync<Tasks>();
        }

      

        //used to create user reminder object
        public async Task AddReminder(Reminder reminder)
        {
            await dbconnection.InsertWithChildrenAsync(reminder, true);                    
        }

      

        ////used to insert all elements with its children
        //public async Task InsertAllTasks(List<Tasks> tasks)
        //{
        //    await dbconnection.InsertAllWithChildrenAsync(tasks,true);
        //}

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
            var allreminder = await dbconnection.GetAllWithChildrenAsync<Reminder>();
            //var allreminder = await dbconnection.Table<Reminder>().OrderByDescending(r => r.ReminderId).ToListAsync();
            return allreminder;
        }
      

        //gets all pending reminder from the database
        public async Task<List<Reminder>> GetActiveReminders()
        {
            var allreminder = await dbconnection.Table<Reminder>().Where(r =>r.Status == false).ToListAsync();
            return allreminder;
        }

        //deletes a reminder object from the database
        public async Task DeleteReminder(Reminder reminder)
        {
            await dbconnection.DeleteAsync(reminder);           
        }

        //updates a reminder object in the database
        public async Task UpdateReminder(Reminder reminder )
        {
            await dbconnection.InsertOrReplaceWithChildrenAsync(reminder,true);
        }

    }
}
