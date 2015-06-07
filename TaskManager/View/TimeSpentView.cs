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

namespace TaskManager.View
{
    public class TimeSpentView
    {
        string userFilepath = "users.txt";
        string tasksFilepath = "tasks.txt";
        string timeSpFilepath = "time.txt";
        private Tasks task = null;

        public TimeSpentView(Tasks task)
        {
            this.task = task;
        }

        public void ShowMenu()
        {
            while (true)
            {
                TimeSpentEnum choice = RenderMenu();
                {
                    try
                    {
                        switch (choice)
                        {
                            case TimeSpentEnum.Finish:
                                {
                                    Finish();
                                    break;
                                }
                            case TimeSpentEnum.Select:
                                {
                                    GetById();
                                    break;
                                }
                            case TimeSpentEnum.View:
                                {
                                    GetByTaskId();
                                    break;
                                }
                            case TimeSpentEnum.Undone:
                                {
                                    CheckUndoneTasks();
                                    break;
                                }
                            case TimeSpentEnum.Exit:
                                {
                                    return;
                                }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                        Console.ReadKey(true);
                    }
                }
            }
        }

        TimeSpentEnum RenderMenu()
        {
             while (true)
            {
                Console.Clear();
                Console.WriteLine("-->->->->-> TIME SPENT MANAGER <-<-<-<-<--");
                Console.WriteLine();
                Console.WriteLine("#[G]et time spent by ID");
                Console.WriteLine("#[V]iew time spent by task ID");
                Console.WriteLine("#[F]inish task");
                Console.WriteLine("#[C]heck for undone tasks");
                Console.WriteLine("#E[x]it");
                Console.WriteLine();
                Console.Write("#Choose: ");

                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "G":
                        {
                            return TimeSpentEnum.Select;
                        }
                    case "V":
                        {
                            return TimeSpentEnum.View;
                        }
                    case "F":
                        {
                            return TimeSpentEnum.Finish;
                        }
                    case "C":
                        {
                            return TimeSpentEnum.Undone;
                        }
                    case "X":
                        {
                            return TimeSpentEnum.Exit;
                        }
                    default:
                        {
                            Console.WriteLine("#Invalid choice !");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }

        private void Finish()
        {
            Console.Clear();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            List<Tasks> taskList = taskRepo.GetTaskByAssignedTo(AuthenticateService.LoggedUser.UserId);
            TimeSpentRepository tSpentRepo = new TimeSpentRepository(timeSpFilepath);
            
            if (taskList != null)
            {
                int i = 1;
                Console.WriteLine("#You have {0} unfinished task(s)",taskList.Count());
                Console.WriteLine();
                foreach (var task in taskList)
                {
                    Console.WriteLine("#Task({0}) title: {1}",i,task.Title);
                    i++;
                    Console.WriteLine("#Task started on: {0}",task.Createdon);
                    int time = 0;
                    time = tSpentRepo.EstimatedTime(task);
                    if (time > 0)
                    {
                        Console.WriteLine("#Estimated time: {0}", tSpentRepo.EstimatedTime(task));
                    }
                    else
                    {
                        time = time - (time+time);
                        Console.WriteLine("#You are late about {0}",time);
                    }
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------");
                }
                Console.Write("#Finish task: ");
                int n = int.Parse(Console.ReadLine());
                tSpentRepo.Add(taskList[n-1], tSpentRepo.EstimatedTime(taskList[n-1]));
                taskRepo.Remove(taskList[n-1]);
                Console.WriteLine();
                Console.WriteLine("#Task successfully finished");
                
            }
            else 
            {
                Console.WriteLine("#There is no tasks for you.");
            }
            Console.ReadKey(true);
        }

        private void GetTasksById()
        {
            Console.Clear();
            TimeSpentRepository tSpentRepo = new TimeSpentRepository(timeSpFilepath);
            List<TimeSpent> tSpentList = new List<TimeSpent>();
            tSpentList = tSpentRepo.GetAllTimeSpent();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            UserRepository userRepo = new UserRepository(userFilepath);

            foreach (var tSpent in tSpentList)
            {
                Console.WriteLine("#TimeSpent ID: {0}  with  task ID: {1}", tSpent.Id, tSpent.Taskid);
            }
        }

        private void GetById()
        {
            Console.Clear();
            GetTasksById();
            Console.WriteLine();
            Console.Write("#Input TimeSpent ID: ");
            int id = int.Parse(Console.ReadLine());
            TimeSpentRepository tSpentRepo = new TimeSpentRepository(timeSpFilepath);
            TimeSpent tSpent = new TimeSpent();
            tSpent = tSpentRepo.GetTimeSpentById(id);
            if (tSpent == null)
            {
                Console.WriteLine("#TimeSpent non exist, Try again !");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("#TimeSpent ID: " + tSpent.Id);
                Console.WriteLine("#TimeSpent UserID: " + tSpent.Userid);
                Console.WriteLine("#TimeSpent TaskID: " + tSpent.Taskid);
                Console.WriteLine("#TimeSpent TimeSpent: " + tSpent.Timespent);
                Console.WriteLine("#Created on: " + tSpent.Date);

                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------");
                Console.ReadKey(true);
            }
        }

        private void GetByTaskId()
        {
            Console.Clear();
            GetTasksById();
            Console.WriteLine();
            Console.Write("#Input task ID: ");
            int id = int.Parse(Console.ReadLine());
            TimeSpentRepository tSpentRepo = new TimeSpentRepository(timeSpFilepath);
            TimeSpent tSpent = new TimeSpent();
            tSpent = tSpentRepo.GetTimeSpentByTaskId(id);
            if (tSpent == null)
            {
                Console.WriteLine("#TimeSpent non exist, Try again !");
                Console.ReadKey(true);
            }
            else
            {
                Console.WriteLine("#TimeSpent ID: " + tSpent.Id);
                Console.WriteLine("#TimeSpent UserID: " + tSpent.Userid);
                Console.WriteLine("#TimeSpent TaskID: " + tSpent.Taskid);
                Console.WriteLine("#TimeSpent TimeSpent: " + tSpent.Timespent);
                Console.WriteLine("#Created on: " + tSpent.Date);

                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------");
                Console.ReadKey(true);
            }
        }

        private void CheckUndoneTasks()
        { }
    }
}
