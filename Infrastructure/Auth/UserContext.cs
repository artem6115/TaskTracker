using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public static class UserContext
    {
        private readonly static AsyncLocal<UserClaims> CurrentUser = new();
        public static UserClaims User
        {
            get => CurrentUser.Value;
            set => CurrentUser.Value = value;   
        }


    }
}
