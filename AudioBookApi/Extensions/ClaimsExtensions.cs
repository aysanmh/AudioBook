﻿using System.Security.Claims;

namespace AudioBookApi.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {

            return user.Claims.SingleOrDefault(u => u.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        }
    }
}
