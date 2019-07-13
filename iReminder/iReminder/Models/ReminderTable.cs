using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;


namespace iReminder.Models
{
    //public enum Status
    // {
    //     pending = 0,
    //     done = 1
    // }

    public class Reminder
    {
        [PrimaryKey] [AutoIncrement]
        public int ReminderId { get; set; }
        public bool Status { get; set; }
        public string ReminderLocation { get; set; }
        //in terms of days
        public DateTime ReminderCreationDate { get; set; }

        [OneToMany]
        public List<Tasks> Things { get; set; }


    }

   
}

