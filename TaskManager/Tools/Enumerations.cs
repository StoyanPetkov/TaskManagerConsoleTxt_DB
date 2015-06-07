using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Tools
{
    public enum UserManagmentEnum
    {
        Insert = 1,
        Delete = 2,
        Update = 3,
        Select = 4,
        View = 5,
        TaskMenu = 6,
        Exit = 7
    }

    public enum TaskManagerEnum
    {
        Insert = 1,
        Delete = 2,
        Update = 3,
        Select = 4,
        View = 5,
        Exit = 6,
        TimeSpent = 7
    }

    public enum TimeSpentEnum
    {
        Finish = 1,
        Undone = 2,
        Update = 3,
        Select = 4,
        Exit = 5,
        View = 6
    }
}
