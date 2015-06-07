using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.View;

namespace TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginView login = new LoginView();
            login.ShowMenu();

            UserView user = new UserView();
            user.Show();
        }
    }
}
