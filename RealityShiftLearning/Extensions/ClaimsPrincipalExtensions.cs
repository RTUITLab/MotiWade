using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealityShiftLearning.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier && c.Value.Contains("-")).Value;
        }
        public static string Avatar(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "urn:github:avatar")?.Value;
        }
        public static string Login(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == "urn:github:login")?.Value;
        }
    }
}
