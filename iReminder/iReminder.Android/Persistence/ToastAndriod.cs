﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using iReminder.Droid.Persistence;
using iReminder.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(ToastAndriod))]
namespace iReminder.Droid.Persistence
{
    public class ToastAndriod : IToast
    {
        public void ShowMessage(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}