using IdentityServer4.Test;
using LoginApi.Configuration;
using LoginApi.Contracts.Repositories;
using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        static List<TestUser> users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "a9ea0f25-b964-409f-bcce-c92326624921",
                Username = "admin",
                Password = "admin",
            }
        };

        public void AddUser(TestUser user)
        {
            users.Add(user);
        }

        public TestUser GetUserById(string id)
        {
            return users.Where(a => a.SubjectId == id).FirstOrDefault();
        }

        public List<TestUser> GetUsers()
        {
            return users;
        }

        public string ChangePassword(User user)
        {
            var oldPassword = users.Where(a => a.SubjectId == user.Id).FirstOrDefault().Password;

            if (oldPassword == user.Password)
            {
                users.Where(a => a.SubjectId == user.Id).FirstOrDefault().Password = user.NewPassword;
                return string.Empty;
            }
            else
            {
                return "Password is incorrect";
            }
        }
    }
}
