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
            AddNewContact();
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
            Console.WriteLine("Izlaz...");
            break;
        default:
            Console.WriteLine("Nepostojeća opcija, pokušaj ponovo...");
            loopState = Loop.CONTINUE;
            break;
    }
    Console.Clear();
} while(loopState == Loop.CONTINUE);



void PrintAllContacts()
{
    Console.WriteLine(
        $"\nKONTAKTI (Count - {contacts.Count}):\n" +
        "****************************\n");
    foreach (var (contact, calls) in contacts)
    {
        Console.WriteLine(contact.ToString());
    }
    ContinueAndClearConsole();
}
void AddNewContact()
{
    Loop loopState = Loop.CONTINUE;
    bool success;
    string phoneNumber;
    do{
        Console.WriteLine(
               "\nNOVI KONTAKT:\n" +
               "****************************\n"
        );
        Console.WriteLine("Unesi ime i prezime (format: Ime Prezime): ");
        var nameAndSurname = InputNonEmptyString("Ime i prezime");

        do{
            Console.WriteLine("Unesi broj mobitela: ");
            phoneNumber = InputNonEmptyString("Broj mobitela");
            success = contacts.Keys.Count(x => x.phoneNumber == phoneNumber) == 0;
            if (!success)
                Console.WriteLine("Kontakt sa tim brojem već postoji, pokušaj ponovo\n");
        }while (!success);
       
        Console.WriteLine("Unesi preferencu: ");
        var preference = InputPreferenceForContact();
        Contact contact = new Contact(nameAndSurname, phoneNumber, preference);

        Console.WriteLine(contact.ToString());
        Console.WriteLine("Potvrdi unos? (da - za unos)");
        loopState = ConfirmationDialogForDataChange();

        if (loopState != Loop.CONTINUE){
            contacts.Add(contact, new List<Call>());
            Console.WriteLine("Kontakt uspješno dodan!\n");
        }
            
        Console.WriteLine("Zelis li nastavit sa dodavanjem kontakata? (da - za nastavak)");
        loopState = ConfirmationDialog();
        Console.Clear();
    } while (loopState == Loop.CONTINUE);   
}

void ContinueAndClearConsole()
{
    Console.WriteLine("\nPritisni neku tipku za nastavak...");
    var c = Console.ReadKey();
    Console.Clear();
}
string InputNonEmptyString(string message = "unos")
{
    string input;
    do
    {
        input = Console.ReadLine();
        if(input == "")
            Console.WriteLine(message + " nemože biti prazan string, pokušaj ponovo\n");
        
    } while (input == "");
    return input;
}

Loop ConfirmationDialog()
{
    
    var option = InputNonEmptyString();

    if (option.ToLower() == "da"){
        return Loop.CONTINUE;
    }
    return Loop.BREAK;
   
}

Loop ConfirmationDialogForDataChange()
{

    var option = InputNonEmptyString();

    if (option.ToLower() == "da")
    {
        return Loop.BREAK;
    }
    return Loop.CONTINUE;

}

Preferences InputPreferenceForContact()
{
    Console.WriteLine(
        "Odaberi opciju preference:\n" +
        "1 - FAVOURITE\n" +
        "2 - REGULAR\n" +
        "3 - BLOCKED\n"
    );
    do {        
       var choice = InputNonEmptyString("Odabir opcije");
       switch (choice)
       {
            case "1":
                return Preferences.FAVOURITE;
            case "2":
                return Preferences.REGULAR;
            case "3":
                return Preferences.BLOCKED;
            default:
                Console.WriteLine("Nepostojeća opcija, pokušaj ponovo...");
                break;
        }
    } while (true);
}
enum Loop { CONTINUE, BREAK }




