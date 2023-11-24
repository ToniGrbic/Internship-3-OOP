using Internship_3_OOP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Internship_3_OOP.Classes
{
    public class Contact
    {
        public string nameAndSurname { get; set; }
        public string phoneNumber { get; set; }
        public Preferences preference { get; set; }

        public Contact(string nameAndSurname, string phoneNumber, Preferences preference)
        {
            this.nameAndSurname = nameAndSurname;
            this.phoneNumber = phoneNumber;
            this.preference = preference;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is Contact)
                return false;
            Contact contact = (Contact)obj;
            return (phoneNumber == contact.phoneNumber);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(phoneNumber);
        }
        public override string ToString()
        {
            return $"Contact: " + $"{nameAndSurname}\n" +
                   $"\tNumber: {phoneNumber}\n" +
                   $"\tPreference: {preference}\n";
        }
    }
}
