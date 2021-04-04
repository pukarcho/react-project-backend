using IdentityServer4.Test;
using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Contracts.Repositories
{
    public interface IUserRepository
    {
        void AddUser(TestUser user);

        List<TestUser> GetUsers();

        TestUser GetUserById(string id);

        string ChangePassword(User user);
    }
}
