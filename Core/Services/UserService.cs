using System.Collections.Generic;
using System.Linq;
using Core.Data;
using Core.Models;
using Unity.Attributes;

namespace Core.Services
{
    public interface IUserService
    {
        IEnumerable<User> FindAll();
        User GetUser(int id);
    }

    public class UserService : IUserService
    {
        [Dependency]
        public IUserDao UserDao { get; set; }
        public IEnumerable<User> FindAll()
        {
            return UserDao.FindAll().OrderBy(c=> c.Last_Nm).ThenBy(c=>c.First_Nm);
        }

        public User GetUser(int id)
        {
            return UserDao.FindAll().AsQueryable().FirstOrDefault(x => x.User_Id == id);
        }

    }
}