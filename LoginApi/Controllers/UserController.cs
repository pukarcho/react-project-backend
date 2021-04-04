using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using LoginApi.Contracts.Services;
using LoginApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<OWinResponseToken> Register(User user)
        {
            return await _userService.Register(user);
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public async Task<OWinResponseToken> Login(User user)
        {
            return await _userService.Login(user);
        }

        [AllowAnonymous]
        [Route("refresh_token")]
        [HttpPost]
        public async Task<OWinResponseToken> Refresh([FromBody]string refreshToken)
        {
            return await _userService.Refresh(refreshToken);
        }

        [Route("change_password")]
        [HttpPost]
        public string ChangePassword(User user)
        {
            user.Id = HttpContext.User.FindFirstValue(JwtClaimTypes.Subject);
            return _userService.ChangePassword(user);
        }

        [Route("msg")]
        [HttpPost]
        public string GetUserMsg()
        {
            return " is authenticated";
        }
    }
}