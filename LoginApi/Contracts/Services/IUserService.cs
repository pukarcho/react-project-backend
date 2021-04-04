using LoginApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginApi.Contracts.Services
{
    public interface IUserService
    {
        Task<OWinResponseToken> Register(User user);

        Task<OWinResponseToken> Login(User user);

        Task<OWinResponseToken> Refresh(string refreshToken);

        string ChangePassword(User user);
    }
}
