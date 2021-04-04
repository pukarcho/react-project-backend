using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using LoginApi.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginApi.Configuration
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            GrantValidationResult result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username or password is incorrect.");
            context.Result = result;
            if (!string.IsNullOrEmpty(context.UserName))
            {
                TestUser user = _userRepository.GetUsers().Where(a => a.Username == context.UserName).FirstOrDefault();
                if (user == null)
                {
                    return;
                }
                else
                {
                    if (context.Password == user.Password)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("username", user.Username, ClaimValueTypes.String),
                        };
                        result = new GrantValidationResult(user.SubjectId, context.Request.GrantType, claims);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            context.Result = result;
        }
    }
}
