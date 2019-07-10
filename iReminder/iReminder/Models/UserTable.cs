using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReminder.Models
{
   public class UserModel
    {
       [PrimaryKey][AutoIncrement]
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
