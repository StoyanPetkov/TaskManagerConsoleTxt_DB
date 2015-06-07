using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TaskManager.Entities;

namespace TaskManager.Repositories
{
    public class UserRepository
    {
        private readonly string filepath;

        public UserRepository(string filepath)
        {
            this.filepath = filepath;
        }

        private int GetNextUserId()
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
                        User user = new User();
                        user.UserId = Convert.ToInt32(sr.ReadLine());
                        user.UserName = sr.ReadLine();
                        user.Password = sr.ReadLine();
                        user.FirstName = sr.ReadLine();
                        user.LastName = sr.ReadLine();

                        if (id <= user.UserId)
                        {
                            id = user.UserId + 1;
                        }
                    }
                }
            }
            return id;
        }

        private void Add(User item)
        {
            item.UserId = GetNextUserId();
            FileStream fs = new FileStream(filepath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            using (fs)
            {
                using (sw)
                {
                    sw.WriteLine(item.UserId);
                    sw.WriteLine(item.UserName);
                    sw.WriteLine(item.Password);
                    sw.WriteLine(item.FirstName);
                    sw.WriteLine(item.LastName);
                }
            }

        }

        private void Edit(User item)
        {
            string tempfilepath = "temp." + filepath;
            FileStream ifs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempfilepath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(sr.ReadLine());
                    user.UserName = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();

                    if (user.UserId != item.UserId)
                    {

                        sw.WriteLine(user.UserId);
                        sw.WriteLine(user.UserName);
                        sw.WriteLine(user.Password);
                        sw.WriteLine(user.FirstName);
                        sw.WriteLine(user.LastName);
                    }
                    else
                    {
                        sw.WriteLine(item.UserId);
                        sw.WriteLine(item.UserName);
                        sw.WriteLine(item.Password);
                        sw.WriteLine(item.FirstName);
                        sw.WriteLine(item.LastName);
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

        public User GetById(int id)
        {

            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(sr.ReadLine());
                        user.UserName = sr.ReadLine();
                        user.Password = sr.ReadLine();
                        user.FirstName = sr.ReadLine();
                        user.LastName = sr.ReadLine();

                        if (user.UserId == id)
                        {
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public void Delete(User item)
        {
            string tempFilePath = "temp." + filepath;

            FileStream ifs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    user.UserId = Convert.ToInt32(sr.ReadLine());
                    user.UserName = sr.ReadLine();
                    user.Password = sr.ReadLine();
                    user.FirstName = sr.ReadLine();
                    user.LastName = sr.ReadLine();
                    
                    if (user.UserId != item.UserId)
                    {
                        sw.WriteLine(user.UserId);
                        sw.WriteLine(user.UserName);
                        sw.WriteLine(user.Password);
                        sw.WriteLine(user.FirstName);
                        sw.WriteLine(user.LastName);
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
            File.Move(tempFilePath, filepath);
        }

        public List<User> ListAllUsers()
        {
            List<User> result = new List<User>();
            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(sr.ReadLine());
                        user.UserName = sr.ReadLine();
                        user.Password = sr.ReadLine();
                        user.FirstName = sr.ReadLine();
                        user.LastName = sr.ReadLine();

                        result.Add(user);
                    }
                }
            }
            return result;
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            using (fs)
            {
                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        User user = new User();
                        user.UserId = Convert.ToInt32(sr.ReadLine());
                        user.UserName = sr.ReadLine();
                        user.Password = sr.ReadLine();
                        user.FirstName = sr.ReadLine();
                        user.LastName = sr.ReadLine();

                        if (user.UserName == username && user.Password == password)
                        {
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public void Save(User user)
        {
            if (user.UserId > 0)
            {
                Edit(user);
            }
            else
            {
                Add(user);
            }
        }
    }
}
