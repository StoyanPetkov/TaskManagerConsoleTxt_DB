using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Repositories;
using TaskManager.Tools;
using TaskManager.Service;

namespace TaskManager.View
{
    public class UserView
    {
        string userFilepath = "users.txt";

        public void Show()
        {
                while (true)
                {
                    try
                    {
                        UserManagmentEnum choice = RenderMenu();
                        switch (choice)
                        {
                            case UserManagmentEnum.View:
                                {
                                    GetAll();
                                    break;
                                }
                            case UserManagmentEnum.Insert:
                                {
                                    Add();
                                    break;
                                }
                            case UserManagmentEnum.Update:
                                {
                                    Update();
                                    break;
                                }
                            case UserManagmentEnum.Select:
                                {
                                    View();
                                    break;
                                }
                            case UserManagmentEnum.Delete:
                                {
                                    Delete();
                                    break;
                                }
                            case UserManagmentEnum.Exit:
                                {
                                    return;
                                }
                            case UserManagmentEnum.TaskMenu:
                                {
                                    TasksMenu();
                                    break;
                                }
                        }
                    }
            

            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.WriteLine("#Try again !");
                Console.ReadKey(true);
            }
                }
        }

        private UserManagmentEnum RenderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-->->->->-> USER MENU <-<-<-<-<--");
                Console.WriteLine();
                Console.WriteLine("#[A]dd new contact");
                Console.WriteLine("#[E]dit contact");
                Console.WriteLine("#[V]iew all contact");
                Console.WriteLine("#[G]et contact");
                Console.WriteLine("#[D]elete contact");
                Console.WriteLine();
                Console.WriteLine("#Go to [T]ask menu");
                Console.WriteLine();
                Console.WriteLine("#E[x]it");
                Console.WriteLine();

                string choice = Console.ReadLine();
                choice = choice.ToUpper();
                switch (choice)
                {
                    case "A":
                        {
                            return UserManagmentEnum.Insert;
                        }
                    case "E":
                        {
                            return UserManagmentEnum.Update;
                        }
                    case "V":
                        {
                            return UserManagmentEnum.View;
                        }
                    case "D":
                        {
                            return UserManagmentEnum.Delete;
                        }
                    case "X":
                        {
                            return UserManagmentEnum.Exit;
                        }
                    case "G":
                        {
                            return UserManagmentEnum.Select;
                        }
                    case "T":
                        {
                            return UserManagmentEnum.TaskMenu;
                        }
                    default:
                        {
                            Console.WriteLine("#Invalid choice. Try again !");
                            Console.ReadKey(true);
                            break;
                        }
                }
            }
        }

        public void TasksMenu()
        {
            User user = new User();
            TaskManagerView taskView = new TaskManagerView(user);
            taskView.ShowMenu();
        }

        private void Delete()
        {
            Console.Clear();

            List<User> list = new List<User>();
            list = GetUsersInfo();
            foreach (var user in list)
            {
                Console.WriteLine("#Username: {0} | ID: {1}", user.UserName, user.UserId);
            }
            Console.WriteLine();
            Console.Write("+Input id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bool check = CheckForUser(id);

            if (check == false)
            {
                Console.Clear();
                Console.WriteLine("#User not found,\nTry again !");
                Console.ReadKey(true);
                return;
            }
            else
            {

                UserRepository userRepo = new UserRepository(userFilepath);
                User user = userRepo.GetById(id);
                userRepo.Delete(user);
                Console.WriteLine();
                Console.WriteLine("#User " + user.UserName + "deleted successfully !");
                Console.ReadKey(true);
            }
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

        private List<User> GetUsersInfo()
        {
            UserRepository userRepo = new UserRepository(userFilepath);
            List<User> usersList = new List<User>();
            usersList = userRepo.ListAllUsers();
            return usersList;
            
        }

        private void View()
        {
            Console.Clear();

            List<User> list = new List<User>();
            list = GetUsersInfo();
            foreach (var user in list)
            {
                Console.WriteLine("#Username: {0} | ID: {1}", user.UserName, user.UserId);
            }
            Console.WriteLine();
            Console.Write("#Input id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bool check = CheckForUser(id);

            if (check == false)
            {
                Console.Clear();
                Console.WriteLine("#User not found,\nTry again !");
                Console.ReadKey(true);
                return;
            }
            else
            {
                User user = new User();
                UserRepository userRepo = new UserRepository(userFilepath);
                user = userRepo.GetById(id);
                Console.WriteLine();
                Console.WriteLine("#ID: " + user.UserId);
                Console.WriteLine("#Username: " + user.UserName);
                Console.WriteLine("#Userpassword: " + user.Password);
                Console.WriteLine("#First name: " + user.FirstName);
                Console.WriteLine("#Last name: " + user.LastName);
                Console.ReadKey(true);
                return;
            }


        }

        private void Update()
        {
            Console.Clear();
            List<User> list = new List<User>();
            list = GetUsersInfo();
            foreach (var user in list)
            {
                Console.WriteLine("#Username: {0} | ID: {1}", user.UserName, user.UserId);
            }
            Console.WriteLine();
            Console.Write("#Input user id: ");
            int id = Convert.ToInt32(Console.ReadLine());
            bool check = CheckForUser(id);
            if (check == false)
            {
                Console.Clear();
                Console.WriteLine("#User not found,\nTry again !");
                Console.ReadKey(true);
                return;
            }
            else
            {
                UserRepository userRepository = new UserRepository(userFilepath);
                User user = userRepository.GetById(id);
                Console.Clear();
                Console.WriteLine("#Editing user: ");
                Console.WriteLine();
                Console.WriteLine("-User id: " + user.UserId);
                Console.WriteLine("-Username: " + user.UserName);
                Console.WriteLine("-First name: " + user.FirstName);
                Console.WriteLine("-Last name: " + user.LastName);
                Console.WriteLine();
                Console.Write("+Input new username: ");
                string newUserName = Console.ReadLine();
                Console.Write("+Input new first name: ");
                string firstName = Console.ReadLine();
                Console.Write("+Input new last name: ");
                string lastName = Console.ReadLine();

                if (!string.IsNullOrEmpty(newUserName))
                {
                    user.UserName = newUserName;
                }
                if (!string.IsNullOrEmpty(firstName))
                {
                    user.FirstName = firstName;
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    user.LastName = lastName;
                }
                Console.WriteLine();
                userRepository.Save(user);
                Console.WriteLine("#User edited successfully");
                Console.ReadKey(true);
            }
        }

        private void Add()
        {
            Console.Clear();
            User user = new User();
            try
            {
                Console.WriteLine("#Add new user");
                Console.Write("+Username: ");
                user.UserName = Console.ReadLine();
                Console.Write("+Password: ");
                user.Password = Console.ReadLine();
                Console.Write("+First name: ");
                user.FirstName = Console.ReadLine();
                Console.Write("+Last name: ");
                user.LastName = Console.ReadLine();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                Console.ReadKey(true);
            }

            UserRepository userRepository = new UserRepository(userFilepath);
            userRepository.Save(user);

            Console.WriteLine("#User added successfully");
            Console.ReadKey(true);

        }

        private void GetAll()
        {
            Console.Clear();

            UserRepository userRepository = new UserRepository(userFilepath);
            List<User> users = userRepository.ListAllUsers();

            foreach (User user in users)
            {
                Console.WriteLine();
                Console.WriteLine("#User id: " + user.UserId);
                Console.WriteLine("#Username: " + user.UserName);
                Console.WriteLine("#User pass: " + user.Password);
                Console.WriteLine("#User's first name: " + user.FirstName);
                Console.WriteLine("#User's last name " + user.LastName);
                Console.WriteLine("------------------------------------------------------");
            }
            
            Console.ReadKey(true);
        }
    }
    }
