using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.DomainObjects
{
    
    public class Email
    {
        public const int AddressMaxLength = 254;
        public const int AddressMinLength = 5;

        public string Address { get; set; }

        //empty if EF
        public Email()
        {

        }

        public Email(string address)
        {
            if(Validate(address))
                throw new DomainException("Invalid email");

            Address = address;

        }

        public static bool Validate(string email)
                    => new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$")
                               .IsMatch(email);

    }
}
