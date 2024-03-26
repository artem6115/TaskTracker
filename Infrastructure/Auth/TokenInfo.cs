using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Auth
{
    public class TokenInfo
    {
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public string AUDIENCE_REFRASH { get; set; }
   
        public uint LIFETIME { get; set; }
        public uint REFRESH_LIFETIME { get; set; }

        public string KEY { get; set; }

    }
}
