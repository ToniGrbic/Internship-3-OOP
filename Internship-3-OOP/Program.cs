// See https://aka.ms/new-console-template for more information
using Internship_3_OOP.Classes;
using Internship_3_OOP.Enums;
using System.Collections.Generic;
using System.ComponentModel;

Loop loopState = Loop.CONTINUE;

Dictionary<Contact, List<Call>> contacts = new()
{
    { new Contact("Ivan", "123456789", Preferences.FAVOURITE), new List<Call>() },
    { new Contact("Marko", "987654321", Preferences.REGULAR), new List<Call>() },
    { new Contact("Sime", "123456789", Preferences.BLOCKED), new List<Call>() },

};

if (contacts.Keys.Count(x => x.phoneNumber == "123456789") > 1)
    Console.WriteLine("has contacts with same numbers");

do
{
    Console.WriteLine(
        "MENU:\n" +
        "1. Ispis svih kontakata\n" +
        "2. Dodavanje novih kontakata u imenik\n" +
        "3. Brisanje kontakata iz imenika\n" +
        "4. Editiranje preference kontakta\n" +
        "5. Upravljanje kontaktom\n"
    );
    Console.WriteLine("Odaberi opciju: ");
    var choice = InputNonEmptyString("Odabir opcije");
    switch (choice)
    {
        case "1":
            PrintAllContacts();
            break;
        case "2":
            
            break;
        case "3":
            Console.WriteLine("Brisanje kontakata iz imenika");
            break;
        case "4":
            Console.WriteLine("Editiranje preference kontakta");
            break;
        case "5":
            Console.WriteLine("Upravljanje kontaktom");
            break;
        case "0":
            loopState = Loop.BREAK;
            Console.WriteLine("");
            break;
        default:
            Console.WriteLine("Nepostojeća opcija, pokušaj ponovo");
            loopState = Loop.CONTINUE;
            break;
    }
    Console.Clear();
} while(loopState == Loop.CONTINUE);

void PrintAllContacts()
{
    Console.WriteLine(
        "\nALL CONTACTS:\n" +
        "****************************\n");
    foreach (var (contact, calls) in contacts)
    {
        Console.WriteLine(contact.ToString());
    }
    Console.WriteLine("\nPress any key to continue...");
    var c = Console.ReadKey();
}

string InputNonEmptyString(string message)
{
    string input;
    do
    {
        input = Console.ReadLine();
        if(input == "")
            Console.WriteLine(message + "nemože biti prazan string, pokušaj ponovo\n");
        
    } while (input == "");
    return input;
}

enum Loop { CONTINUE, BREAK }




