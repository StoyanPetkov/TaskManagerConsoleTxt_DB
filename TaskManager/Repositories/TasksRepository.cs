using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TaskManager.Entities;
using System.Globalization;

namespace TaskManager.Repositories
{
    public class TasksRepository
    {
        private readonly string filepath;

        public TasksRepository(string filepath)
        {
            this.filepath = filepath;
        }

        public int GetNextId()
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
                        Tasks task = new Tasks();
                        task.TaskId = Convert.ToInt32(sr.ReadLine());
                        task.Title = sr.ReadLine();
                        task.Description = sr.ReadLine();
                        task.EstimatedTime = Convert.ToInt32(sr.ReadLine());
                        task.Createdon = Convert.ToDateTime(sr.ReadLine());
                        task.CreatedBy = Convert.ToInt32(sr.ReadLine());
                        task.Assignedto = Convert.ToInt32(sr.ReadLine());

                        if (id <= task.TaskId)
                        {
                            id = task.TaskId + 1;
                        }
                    }
                }
            }
            return id;
        }

        public void Add(Tasks item)
        {
            item.TaskId = GetNextId();
            FileStream fs = new FileStream(filepath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            using (fs)
            {
                using (sw)
                {
                    sw.WriteLine(item.TaskId);
                    sw.WriteLine(item.Title);
                    sw.WriteLine(item.Description);
                    sw.WriteLine(item.EstimatedTime);
                    sw.WriteLine(item.Createdon);
                    sw.WriteLine(item.CreatedBy);
                    sw.WriteLine(item.Assignedto);
                }
            }
        }

        public void Edit(Tasks item)
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
                    Tasks task = new Tasks();
                    task.TaskId = Convert.ToInt32(sr.ReadLine());
                    task.Title = sr.ReadLine();
                    task.Description = sr.ReadLine();
                    task.EstimatedTime = Convert.ToInt32(sr.ReadLine());
                    task.Createdon = Convert.ToDateTime(sr.ReadLine());
                    task.CreatedBy = Convert.ToInt32(sr.ReadLine());
                    task.Assignedto = Convert.ToInt32(sr.ReadLine());

                    if (task.TaskId != item.TaskId)
                    {
                        sw.WriteLine(task.TaskId);
                        sw.WriteLine(task.Title);
                        sw.WriteLine(task.Description);
                        sw.WriteLine(task.EstimatedTime);
                        sw.WriteLine(task.Createdon);
                        sw.WriteLine(task.CreatedBy);
                        sw.WriteLine(task.Assignedto);
                    }
                    else
                    {
                        sw.WriteLine(item.TaskId);
                        sw.WriteLine(item.Title);
                        sw.WriteLine(item.Description);
                        sw.WriteLine(item.EstimatedTime);
                        sw.WriteLine(item.Createdon);
                        sw.WriteLine(item.CreatedBy);
                        sw.WriteLine(item.Assignedto);
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

        public void Remove(Tasks item)
        {
            string tempfilepath = "temp." + filepath;
            FileStream ofs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ofs);
            FileStream ifs = new FileStream(tempfilepath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ifs);
            try
            {
                Tasks task = new Tasks();
                task.TaskId = Convert.ToInt32(sr.ReadLine());
                task.Title = sr.ReadLine();
                task.Description = sr.ReadLine();
                task.EstimatedTime = Convert.ToInt32(sr.ReadLine());
                task.Createdon = Convert.ToDateTime(sr.ReadLine());
                task.CreatedBy = Convert.ToInt32(sr.ReadLine());
                task.Assignedto = Convert.ToInt32(sr.ReadLine());

                if (task.TaskId != item.TaskId)
                {
                    sw.WriteLine(task.TaskId);
                    sw.WriteLine(task.Title);
                    sw.WriteLine(task.Description);
                    sw.WriteLine(task.EstimatedTime);
                    sw.WriteLine(task.Createdon);
                    sw.WriteLine(task.CreatedBy);
                    sw.WriteLine(task.Assignedto);
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

        public List<Tasks> GetAllTasks()
        {
            List<Tasks> result = new List<Tasks>();
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        Tasks task = new Tasks();
                        task.TaskId = Convert.ToInt32(sr.ReadLine());
                        task.Title = sr.ReadLine();
                        task.Description = sr.ReadLine();
                        task.EstimatedTime = Convert.ToInt32(sr.ReadLine());
                        task.Createdon = Convert.ToDateTime(sr.ReadLine());
                        task.CreatedBy = Convert.ToInt32(sr.ReadLine());
                        task.Assignedto = Convert.ToInt32(sr.ReadLine());

                        result.Add(task);
                    }
                }
            }

            return result;
        }

        public Tasks GetTaskById(int taskid)
        {
            Tasks result = new Tasks();
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        Tasks task = new Tasks();
                        task.TaskId = Convert.ToInt32(sr.ReadLine());
                        task.Title = sr.ReadLine();
                        task.Description = sr.ReadLine();
                        task.EstimatedTime = Convert.ToInt32(sr.ReadLine());
                        task.Createdon = Convert.ToDateTime(sr.ReadLine());
                        task.CreatedBy = Convert.ToInt32(sr.ReadLine());
                        task.Assignedto = Convert.ToInt32(sr.ReadLine());

                        if (task.TaskId == taskid)
                        {
                            result = task;
                        }
                    }
                }
            }

            return result;
        }

        public void Save(Tasks task)
        {
            if (task.TaskId > 0)
            {
                Edit(task);
            }
            else
            {
                Add(task);
            }
        }

        public List<Tasks> GetTaskByAssignedTo(int id)
        {
            List<Tasks> taskList = new List<Tasks>();
            FileStream fs = new FileStream(this.filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        Tasks task = new Tasks();
                        task.TaskId = Convert.ToInt32(sr.ReadLine());
                        task.Title = sr.ReadLine();
                        task.Description = sr.ReadLine();
                        task.EstimatedTime = Convert.ToInt32(sr.ReadLine());
                        task.Createdon = Convert.ToDateTime(sr.ReadLine());
                        task.CreatedBy = Convert.ToInt32(sr.ReadLine());
                        task.Assignedto = Convert.ToInt32(sr.ReadLine());

                        if (task.Assignedto == id)
                        {
                            taskList.Add(task);
                        }
                    }
                }
            }

            return taskList;
        }
    }
}
