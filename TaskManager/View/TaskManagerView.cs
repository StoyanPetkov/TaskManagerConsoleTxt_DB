using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Tools;
using TaskManager.Service;
using TaskManager.Repositories;
using TaskManager.Entities;


namespace TaskManager.View
{
    public class TaskManagerView
    {
        string userFilepath = "users.txt";
        string tasksFilepath = "tasks.txt";

        private User user = null;

        public TaskManagerView(User user)
        {
            this.user = user;
        }

        public void ShowMenu()
        {
            while (true)
            {
                TaskManagerEnum choice = RenderMenu();
                {
                    try
                    {
                        switch (choice)
                        {
                            case TaskManagerEnum.View:
                                {
                                    GetAll();
                                    break;
                                }
                            case TaskManagerEnum.Insert:
                                {
                                    Add();
                                    break;
                                }
                            case TaskManagerEnum.Select:
                                {
                                    GetById();
                                    break;
                                }
                            case TaskManagerEnum.Exit:
                                {
                                    return;
                                }
                            case TaskManagerEnum.Update:
                                {
                                    Update();
                                    break;
                                }
                            case TaskManagerEnum.Delete:
                                {
                                    Delete();
                                    break;
                                }
                            case TaskManagerEnum.TimeSpent:
                                {
                                    TimeSpentMenu();
                                    break;
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

        TaskManagerEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-->->->->-> TASK MANAGER <-<-<-<-<--");
                Console.WriteLine();
                Console.WriteLine("#[I]nsert new task");
                Console.WriteLine("#[V]iew task by id");
                Console.WriteLine("#[G]et all tasks");
                Console.WriteLine("#[E]dit task");
                Console.WriteLine("#[D]elete task");
                Console.WriteLine("#E[x]it");
                Console.WriteLine();
                Console.WriteLine("#Go to time [S]pent menu");
                Console.WriteLine();
                Console.Write("#Choose: ");
                string choice = Console.ReadLine();

                switch (choice.ToUpper())
                {
                    case "I":
                        {
                            return TaskManagerEnum.Insert;
                        }
                    case "V":
                        {
                            return TaskManagerEnum.Select;
                        }
                    case "G":
                        {
                            return TaskManagerEnum.View;
                        }
                    case "E":
                        {
                            return TaskManagerEnum.Update;
                        }
                    case "D":
                        {
                            return TaskManagerEnum.Delete;
                        }
                    case "X":
                        {
                            return TaskManagerEnum.Exit;
                        }
                    case "S":
                        {
                            return TaskManagerEnum.TimeSpent;
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

        private void TimeSpentMenu()
        {
            Tasks task = new Tasks();
            TimeSpentView tView = new TimeSpentView(task);
            tView.ShowMenu();
        }

        private void Delete()
        {
            Console.Clear();
            List<Tasks> taskList = new List<Tasks>();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            UserRepository userRepo = new UserRepository(userFilepath);
            taskList = taskRepo.GetAllTasks();
            foreach (var Task in taskList)
            {
                User user = userRepo.GetById(Task.CreatedBy);
                Console.WriteLine("#Title: {0} | ID: {1} | created by: {2}({3}", Task.Title, Task.TaskId, user.UserName, user.UserId + ")");
                Console.WriteLine("-------------------------------------------------------");
            }
            Console.WriteLine();
            Console.Write("-Delete task (id): ");
            int id = int.Parse(Console.ReadLine());
            bool check = CheckForTask(id);
            if (check == true)
            {
                Tasks task = taskRepo.GetTaskById(id);
                taskRepo.Remove(task);
                Console.WriteLine();
                Console.WriteLine("#Task \"{0}\" deleted successfully", task.Title);
                Console.ReadKey(true);

            }
            else
            {
                Console.WriteLine("#Task id not found.");
                Console.ReadKey(true);
            }
        }

        private void GetTaskByIdAndUserName()
        {
            Console.Clear();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            List<Tasks> taskList = taskRepo.GetAllTasks();
            UserRepository userRepo = new UserRepository(userFilepath);

            foreach (var task in taskList)
            {
                User user = userRepo.GetById(task.Assignedto);
                Console.WriteLine("#Task ID: {0}  |  assigned to: {1}", task.TaskId, user.UserName);
            }
        }

        private void GetAll()
        {
            Console.Clear();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            List<Tasks> tasks = taskRepo.GetAllTasks();

            foreach (var task in tasks)
            {
                UserRepository userRepo = new UserRepository(userFilepath);
                User user = new User();
                Console.WriteLine();
                Console.WriteLine("#Task id: " + task.TaskId);
                Console.WriteLine("#Title: " + task.Title);
                Console.WriteLine("#Description: " + task.Description);
                Console.WriteLine("#Estimated time: " + task.EstimatedTime);
                Console.WriteLine("#Created on: " + task.Createdon);
                user = userRepo.GetById(task.CreatedBy);
                Console.WriteLine("#Created by: {0}({1}",user.UserName, task.CreatedBy + ")");
                user = userRepo.GetById(task.Assignedto);
                Console.WriteLine("#Assigned to: {0}({1} ", user.UserName, user.UserId + ")");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------");
            }
            Console.ReadKey(true);
        }

        private void GetById()
        {
            Console.Clear();
            GetTaskByIdAndUserName();
            Console.WriteLine();
            Console.Write("#Input task's ID: ");
            int id = int.Parse(Console.ReadLine());
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            Tasks task = new Tasks();
            task = taskRepo.GetTaskById(id);
            if (task == null)
            {
                Console.WriteLine("#Task not exist, Try again !");
                Console.ReadKey(true);
            }
            else
            {
                UserRepository userRepo = new UserRepository(userFilepath);
                User user = new User();
                user = userRepo.GetById(task.Assignedto);
                Console.WriteLine();
                Console.WriteLine("#Task id: " + task.TaskId);
                Console.WriteLine("#Title: " + task.Title);
                Console.WriteLine("#Description: " + task.Description);
                Console.WriteLine("#Estimated time: " + task.EstimatedTime);
                Console.WriteLine("#Created on: " + task.Createdon);
                Console.WriteLine("#Created by: {0}({1}" + AuthenticateService.LoggedUser.UserName, task.CreatedBy + ")");
                Console.WriteLine("#Assigned to: {0}({1} ", user.UserName, user.UserId + ")");

                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------");
                Console.ReadKey(true);
            }
        }

        private bool CheckForTask(int id)
        {
            bool result = false;
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            Tasks task = taskRepo.GetTaskById(id);
            if (task != null)
            {
                result = true;
            }
            return result;
        }

        private bool CheckForUser(int id)
        {
            bool result = false;
            User user = new User();
            UserRepository userRepo = new UserRepository(userFilepath);
            user = userRepo.GetById(id);
            if (user.UserId > 0)
            {
                result = true;
            }
            return result;
        }

        private void Add()
        {
            Console.Clear();
            Tasks task = new Tasks();
            Console.WriteLine("#You are going to add new task !");
            Console.WriteLine();
            Console.Write("+Task title: ");
            string taskTitle = Console.ReadLine();
            Console.Write("+Description: ");
            string taskDescription = Console.ReadLine();
            Console.Write("+Estimated time: ");
            int estimatedTime = int.Parse(Console.ReadLine());
            UserRepository userRepo = new UserRepository(userFilepath);
            List<User> users = userRepo.ListAllUsers();
            foreach (var user in users)
            {
                Console.Write("#Username: {0}({1}", user.UserName, user.UserId + ")  ");
            }
            Console.WriteLine();
            Console.Write("+Assigned to user with id: ");
            int assignedTo = Convert.ToInt32(Console.ReadLine());
            bool checkUserAssigning = CheckForUser(assignedTo);
            task.CreatedBy = Convert.ToInt32(AuthenticateService.LoggedUser.UserId);
            task.Createdon = DateTime.UtcNow;

            if (checkUserAssigning == true)
            {
                if (!string.IsNullOrEmpty(taskTitle))
                {
                    task.Title = taskTitle;
                }
                if (!string.IsNullOrEmpty(taskDescription))
                {
                    task.Description = taskDescription;
                }
                if (estimatedTime > 0)
                {
                    task.EstimatedTime = estimatedTime;
                }

                    task.Assignedto = assignedTo;
            }
            else
            {
                Console.WriteLine("#Not exist user id,\nTry again !");
            }
            Console.WriteLine();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            taskRepo.Save(task);
            Console.WriteLine("#Task successfully added.");
            Console.ReadKey(true);
            return;
        }

        private void Update()
        {
            Console.Clear();
            TasksRepository taskRepo = new TasksRepository(tasksFilepath);
            UserRepository userRepo = new UserRepository(userFilepath);
            List<Tasks> TaskList = new List<Tasks>();
            TaskList = taskRepo.GetAllTasks();

            foreach (var task in TaskList)
            {
                User toUser = userRepo.GetById(task.Assignedto);
                User byUser = userRepo.GetById(task.CreatedBy);
                Console.WriteLine("#Task ID: {0} | Assigned to: {1}(ID:{3})  | Created by: {2}", task.TaskId, toUser.UserName, byUser.UserName, toUser.UserId);
            }
            Console.WriteLine();
            Console.Write("#Task id: ");
            int taskId = Convert.ToInt32(Console.ReadLine());
            if (CheckForTask(taskId) == true)
            {
                Console.Clear();
                Tasks task = taskRepo.GetTaskById(taskId);
                Console.WriteLine("-Title: " + task.Title);
                Console.WriteLine("-Description: " + task.Description);
                Console.WriteLine("-Estimated time: " + task.EstimatedTime);
                User toUser = userRepo.GetById(task.Assignedto);
                Console.WriteLine("-Assignet to user: " + toUser.UserName + " (ID: " + toUser.UserId + ")");
                Console.WriteLine();
                Console.WriteLine("*******ADD NEW VALUES*******");
                Console.Write("+Title: ");
                string title = Console.ReadLine();
                Console.Write("+Description: ");
                string description = Console.ReadLine();
                Console.Write("+Estimated time: ");
                int estTime = int.Parse(Console.ReadLine());
                Console.Write("+Assignet to: ");
                int assignetTo = int.Parse(Console.ReadLine());

                if (!string.IsNullOrEmpty(title))
                {
                    task.Title = title;
                }
                if (!string.IsNullOrEmpty(description))
                {
                    task.Description = description;
                }
                if (estTime > 0)
                {
                    task.EstimatedTime = estTime;
                }
                if (userRepo.GetById(assignetTo) != null)
                {
                    task.Assignedto = assignetTo;
                }
                taskRepo.Save(task);
                Console.WriteLine();
                Console.WriteLine("#Task successfully edited.");
                Console.ReadKey(true);
            }

        }
    }
}