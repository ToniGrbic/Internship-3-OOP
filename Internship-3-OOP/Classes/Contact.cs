using Internship_3_OOP.Enums;

namespace Internship_3_OOP.Classes
{
    public class Contact
    {
        public string nameAndSurname { get; }
        public string phoneNumber { get; }
        private Preferences preference { get; set; }

        public Contact(string nameAndSurname, string phoneNumber, Preferences preference)
        {
            this.nameAndSurname = nameAndSurname;
            this.phoneNumber = phoneNumber;
            this.preference = preference;
        }

        public void EditPreferece(Preferences preference)
        {             
            this.preference = preference;
        }

        public Preferences GetPreference()
        {
            return preference;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            Contact contact = (Contact)obj;
            return ((phoneNumber == contact.phoneNumber) && (nameAndSurname == contact.nameAndSurname) && preference == contact.preference);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(phoneNumber, nameAndSurname, preference);
        }
        public override string ToString()
        {
            return $"Contact: " + $"{nameAndSurname}\n" +
                   $"\tNumber: {phoneNumber}\n" +
                   $"\tPreference: {preference}\n";
        }
    }
}
