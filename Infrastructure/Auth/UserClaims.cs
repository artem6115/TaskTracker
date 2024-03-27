using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class UserClaims
    {
        private readonly static AsyncLocal<UserClaims> CurrentUser = new();
        public static UserClaims User
        {
            get => CurrentUser.Value;
            set => CurrentUser.Value = value;
        }
        public long Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }

}
