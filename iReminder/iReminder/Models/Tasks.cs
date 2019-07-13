using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReminder.Models
{
  public  class Tasks
    {
        [AutoIncrement]
        [PrimaryKey]
        public int TaskId { get; set; }
        public string Things { get; set; }

        [ForeignKey(typeof(Reminder))]
        public int ReminderId { get; set; } 
            
    }
}
