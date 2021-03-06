﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace PluralsightDemo
{
    public class PluralsightUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<PluralsightUser>
    {
        public PluralsightUserClaimsPrincipalFactory(UserManager<PluralsightUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(PluralsightUser user)
        {
            var identity =  await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("local",user.Local));
            return identity;
        }
    }
}
