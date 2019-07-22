using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReminder.DBServices
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string UserName
        {
            get => AppSettings.GetValueOrDefault("username", SettingsDefault);
            
            set => AppSettings.AddOrUpdateValue("username", value);            
        }

        public static int ReminderId
        {
            get => AppSettings.GetValueOrDefault("reminderId", 0);

            set => AppSettings.AddOrUpdateValue("reminderId", value);
        }


    }
}


