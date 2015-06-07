using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Entities
{
    public class TimeSpent
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Taskid { get; set; }
        public int Timespent { get; set; }
        public DateTime Date { get; set; }
    }
}
