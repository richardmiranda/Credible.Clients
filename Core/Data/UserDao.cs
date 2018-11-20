using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Models;

namespace Core.Data
{
    public interface IUserDao
    {
        IEnumerable<User> FindAll();
    }

    public class UserDao : DaoBase, IUserDao
    {
        public UserDao(Func<IUnitOfWork> uowFactory) : base(uowFactory)
        {
        }

        public IEnumerable<User> FindAll()
        {
            return DBContext.User.AsQueryable();
        }

    }
}