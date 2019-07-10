using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iReminder.Interfaces;
using SQLite;
using iReminder.Droid.Persistence;

[assembly: Dependency(typeof(iReminderDB))]
namespace iReminder.Droid.Persistence
{
    public class iReminderDB : IReminder
    {
      public  SQLiteAsyncConnection Connection()
        {
            var Documentspath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(Documentspath, "iReminder.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}