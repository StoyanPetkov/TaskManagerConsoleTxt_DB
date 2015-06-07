using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TaskManager.Entities;
using TaskManager.Repositories;
using TaskManager.Service;
using TaskManager.Tools;
using TaskManager.View;

namespace TaskManager.Repositories
{
    public class TimeSpentRepository
    {
        private readonly string filepath;
        int hrs = 0;

        public TimeSpentRepository(string filepath)
        {
            this.filepath = filepath;
        }

        private int GetNextId()
        {
            int id = 1;
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        TimeSpent tSpent = new TimeSpent();
                        tSpent.Id = Convert.ToInt32(sr.ReadLine());
                        tSpent.Userid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Taskid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Timespent = Convert.ToInt32(sr.ReadLine());
                        tSpent.Date =Convert.ToDateTime(sr.ReadLine());

                        if (id <= tSpent.Id )
                        {
                            id = tSpent.Id + 1;
                        }
                    }
                }
            }
            return id;
        }

        public void Add(Tasks task,int hrs)
        {
            TimeSpent item = new TimeSpent();
            item.Id = GetNextId();
            
            FileStream fs = new FileStream(filepath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            using (fs)
            {
                using (sw)
                {
                    sw.WriteLine(item.Id);
                    sw.WriteLine(AuthenticateService.LoggedUser.UserId);
                    sw.WriteLine(task.TaskId);
                    sw.WriteLine(hrs);
                    sw.WriteLine(DateTime.Now);
                }
            }
        }

        private void Edit(TimeSpent item)
        {
            string tempfilepath = "temp." + filepath;
            FileStream ofs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ofs);
            FileStream ifs = new FileStream(tempfilepath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ifs);
            try
            {
                while (!sr.EndOfStream)
                {
                    TimeSpent tSpent = new TimeSpent();
                    tSpent.Id = Convert.ToInt32(sr.ReadLine());
                    tSpent.Userid = Convert.ToInt32(sr.ReadLine());
                    tSpent.Taskid = Convert.ToInt32(sr.ReadLine());
                    tSpent.Timespent = Convert.ToInt32(sr.ReadLine());
                    tSpent.Date = Convert.ToDateTime(sr.ReadLine());

                    if (tSpent.Id != item.Id)
                    {
                        sw.WriteLine(tSpent.Id);
                        sw.WriteLine(tSpent.Userid);
                        sw.WriteLine(tSpent.Taskid);
                        sw.WriteLine(tSpent.Timespent);
                        sw.WriteLine(tSpent.Date);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.Userid);
                        sw.WriteLine(item.Taskid);
                        sw.WriteLine(item.Timespent);
                        sw.WriteLine(item.Date);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }
            File.Delete(filepath);
            File.Move(tempfilepath, filepath);
        }

        private void Remove(TimeSpent item)
        {
            string tempfilepath = "temp." + filepath;
            FileStream ofs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ofs);
            FileStream ifs = new FileStream(tempfilepath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ifs);
            try
            {
                TimeSpent tSpent = new TimeSpent();
                tSpent.Id = Convert.ToInt32(sr.ReadLine());
                tSpent.Userid = Convert.ToInt32(sr.ReadLine());
                tSpent.Taskid = Convert.ToInt32(sr.ReadLine());
                tSpent.Timespent = Convert.ToInt32(sr.ReadLine());
                tSpent.Date = Convert.ToDateTime(sr.ReadLine());

                if (tSpent.Id != item.Id)
                {
                    sw.WriteLine(tSpent.Id);
                    sw.WriteLine(tSpent.Userid);
                    sw.WriteLine(tSpent.Taskid);
                    sw.WriteLine(tSpent.Timespent);
                    sw.WriteLine(tSpent.Date);
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }
            File.Delete(filepath);
            File.Move(tempfilepath, filepath);
        }

        public List<TimeSpent> GetAllTimeSpent()
        {
            List<TimeSpent> result = new List<TimeSpent>();
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        TimeSpent tSpent = new TimeSpent();
                        tSpent.Id = Convert.ToInt32(sr.ReadLine());
                        tSpent.Userid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Taskid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Timespent = Convert.ToInt32(sr.ReadLine());
                        tSpent.Date = Convert.ToDateTime(sr.ReadLine());

                        result.Add(tSpent);
                    }
                }
            }
            return result;
        }

        public TimeSpent GetTimeSpentById(int tspentid)
        {
            TimeSpent result = new TimeSpent();
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        TimeSpent tSpent = new TimeSpent();
                        tSpent.Id = Convert.ToInt32(sr.ReadLine());
                        tSpent.Userid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Taskid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Timespent = Convert.ToInt32(sr.ReadLine());
                        tSpent.Date = Convert.ToDateTime(sr.ReadLine());

                        if (tSpent.Id == tspentid)
                        {
                            result = tSpent;
                        }
                    }
                }
            }

            return result;
        }

        public TimeSpent GetTimeSpentByTaskId(int tspenttaskid)
        {
            TimeSpent result = new TimeSpent();
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        TimeSpent tSpent = new TimeSpent();
                        tSpent.Id = Convert.ToInt32(sr.ReadLine());
                        tSpent.Userid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Taskid = Convert.ToInt32(sr.ReadLine());
                        tSpent.Timespent = Convert.ToInt32(sr.ReadLine());
                        tSpent.Date = Convert.ToDateTime(sr.ReadLine());

                        if (tSpent.Taskid == tspenttaskid)
                        {
                            result = tSpent;
                        }
                    }
                }
            }

            return result;
        }

        public int EstimatedTime(Tasks task)
        {
            List<int> time = CalculateTime(task);
            
            if (time.Count == 1)
            {
                hrs = time[0];
            }
            else
            {
                hrs = (time[1] * 24) + time[0];
            }
                hrs = task.EstimatedTime - hrs;
                return hrs;
        }

        public List<int> CalculateTime(Tasks task)
        {
            int startingTime = DateTimeToInt(task.Createdon);
            int endingTime = DateTimeToInt(DateTime.Now);
            int days = endingTime - startingTime;
            List<int> time = new List<int>();

            if (task.TaskId != 0)
            {
                if (days == 0)
                {
                    time.Add(DateHoursToInt(DateTime.Now) - DateHoursToInt(task.Createdon));
                }
                else
                {
                    if (DateHoursToInt(DateTime.Now) < DateHoursToInt(task.Createdon))
                    {
                        time.Add(24 - (DateHoursToInt(task.Createdon) - DateHoursToInt(DateTime.Now)));
                        days = days - 1;
                        time.Add(days);
                    }
                    else
                    {
                        time.Add(DateHoursToInt(DateTime.Now) - DateHoursToInt(task.Createdon));
                        time.Add(days);
                    }
                }
            }
            return time;
        }

        public int DateTimeToInt(DateTime Date)
        {
            return (int)(Date.Date - new DateTime(1900, 1, 1)).TotalDays + 2;
        }

        public DateTime IntToDateTime(int intDate)
        {
            return new DateTime(1900, 1, 1).AddDays(intDate - 2);
        }

        public int DateHoursToInt(DateTime date)
        {
            int result = (int)date.Hour;
            return result;
        }
    }
}
