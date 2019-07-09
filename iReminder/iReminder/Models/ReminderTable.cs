using System;
using System.Collections.Generic;
using System.Text;

namespace iReminder.Models
{
   public class Reminder
    {
        public int ReminderId { get; set; }
        public bool ReminderStatus { get; set; }
        public string ReminderLocation { get; set; }
        //in terms of days
        public DateTime ReminderCreationDate { get; set; }
        public List<string> Tasks { get; set; }

    }
}
