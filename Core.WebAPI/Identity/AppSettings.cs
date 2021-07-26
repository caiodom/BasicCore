using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Identity
{
    public class AppSettingsJwks
    {
        public string JwksAuthUrl { get; set; }
    }

    public class AppSettingsJwt {

        public string Secret { get; set; }
        public int HourToExpiration { get; set; }
        public string Issuer { get; set; }
        public string ValidatedIn { get; set; }
    }
}
