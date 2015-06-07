using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Repositories;

namespace TaskManager.Service
{
    public static class AuthenticateService
    {
        public static User LoggedUser { get; private set; }

        public static void AuthenticateUser(string username, string password)
        {
            UserRepository userRepo = new UserRepository("users.txt");
            AuthenticateService.LoggedUser = userRepo.GetByUsernameAndPassword(username, password);
        }
    }
}
