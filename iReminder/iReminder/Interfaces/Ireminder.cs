using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace iReminder.Interfaces
{
    public interface IReminder
    {
        SQLiteAsyncConnection Connection();
    }
}
