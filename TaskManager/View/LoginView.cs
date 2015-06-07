using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Repositories;
using TaskManager.Service;

namespace TaskManager.View
{
    public class LoginView
    {
        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("###  W E L C O M E  ###");
                Console.WriteLine();
                Console.Write("#Input User: ");
                string username = Console.ReadLine();
                Console.Write("#Input password: ");
                string password = Console.ReadLine();
                AuthenticateService.AuthenticateUser(username,password);
                
                if (AuthenticateService.LoggedUser != null)
                {
                    Console.WriteLine();
                    Console.WriteLine("#You are in !");
                    Console.ReadKey(true);
                    break;
                }
                else
                {
                    Console.WriteLine("#Invalid user name or password !");
                    Console.ReadKey(true);
                }
            }
        }
    }
}
