using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using SQLiteNetExtensions.Extensions.TextBlob;
using System;
using System.Collections.Generic;
using System.Text;


namespace iReminder.Models
{
    public class Reminder
    {
        [PrimaryKey] [AutoIncrement]
        public int ReminderId { get; set; }
        public bool Status { get; set; }
        public string ReminderLocation { get; set; }
        //in terms of days
        public DateTime ReminderCreationDate { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Tasks> ThingsTodo { get; set; }

        



        public string ListStatus
        {
            get
            {
                if (Status == false)
                    return "Active";

                return "Inactive";
            }
        }

        public string ListDate
        {
            get => (DateTime.Now - ReminderCreationDate).Days.ToString() + "day(s) ago ";
        }

        public string TasksCount
        {
            get
            {
                return ThingsTodo.Count.ToString() + " tasks attached";
            }
        }

    }


    public class Tasks
    {
        [AutoIncrement]
        [PrimaryKey]
        public int TaskId { get; set; }
        public string Events { get; set; }


        [ForeignKey(typeof(Reminder))]
        public int ReminderId { get; set; }

        [ManyToOne]
        public Reminder Reminders { get; set; }

    }


}

