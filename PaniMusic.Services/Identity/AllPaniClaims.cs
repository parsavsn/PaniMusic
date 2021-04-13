using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace PaniMusic.Services.Identity
{
    public static class AllPaniClaims
    {
        public static List<Claim> AllClaims = new List<Claim>()
        {
            new Claim(PaniClaims.UserPanel, true.ToString()),

            new Claim(PaniClaims.AdminPanel, true.ToString()),

            new Claim(PaniClaims.NewItem, true.ToString()),

            new Claim(PaniClaims.EditItem, true.ToString()),

            new Claim(PaniClaims.DeleteItem, true.ToString())
        };
    }
}
