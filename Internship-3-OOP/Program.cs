// See https://aka.ms/new-console-template for more information
using Internship_3_OOP.Classes;
using Internship_3_OOP.Enums;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

Loop loopState = Loop.CONTINUE;

Dictionary<Contact, List<Call>> contacts = new()
{
    { new Contact("Ivan", "123456789", Preferences.FAVOURITE), new List<Call>() { new Call(new DateTime(2023,12,23,12,2,4)), new Call(new DateTime(2023, 11, 26, 16, 15, 4)) } },
    { new Contact("Marko", "987654321", Preferences.REGULAR), new List<Call>() { new Call(), new Call() } },
    { new Contact("Sime", "121212121", Preferences.BLOCKED), new List<Call>() { new Call(), new Call() } },
};

do{
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
            DeleteContact();
            break;
        case "4":
            EditContactPreference();
            break;
        case "5":
            var contact = FindContact();
            Console.Clear();
            ContactManagmentSubmenu(contact);
            break;
        case "6":
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
        Console.WriteLine($"Potvrdi unos kontakta {nameAndSurname}? (da - za unos)");
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

void DeleteContact()
{
    Loop loopState = Loop.CONTINUE;
    do{
        Console.WriteLine(
               "\nBRISANJE KONTAKTA:\n" +
               "****************************\n"
        );
        var contact = FindContact();

        Console.WriteLine(contact.ToString());
        Console.WriteLine($"Jeste li sigurni da želite obrisati kontakt: {contact.nameAndSurname} - {contact.phoneNumber}? (da - za brisanje)");
        loopState = ConfirmationDialogForDataChange();
        if (loopState != Loop.CONTINUE)
        {
            if(contacts.Remove(contact))
                Console.WriteLine("Kontakt uspješno obrisan!\n");
            else
                Console.WriteLine("Greška: Kontakt nije obrisan!\n");
        }
        Console.WriteLine("Zelis li nastavit sa brisanjem kontakata? (da - za nastavak)");
        loopState = ConfirmationDialog();
        Console.Clear();
    }while(loopState == Loop.CONTINUE);
}

void EditContactPreference()
{
    Loop loopState = Loop.CONTINUE;
    do
    {
        Console.WriteLine(
                "\nEDITIRANJE KONTAKTA:\n" +
                "****************************\n"
        );
        var contact = FindContact();
        
        Console.WriteLine(contact.ToString());
        Console.WriteLine("Uredi preferencu? (da - nastavak):");
        if (ConfirmationDialog() == 0)
        {
            contact.preference = InputPreferenceForContact();
        }
        Console.WriteLine("Zelis li nastavit sa uređivanjem kontakata? (da - za nastavak)");
        loopState = ConfirmationDialog();
        Console.Clear();
    } while (loopState == Loop.CONTINUE);
}

void ContactManagmentSubmenu(Contact contact)
{
    
    do
    {
        Console.WriteLine(contact.ToString());
        Console.WriteLine(
            "SUBMENU:\n" +
            "1. Ispis svih poziva\n" +
            "2. Novi poziv\n" +
            "3. Izlaz\n"
            
        );
        Console.WriteLine("Odaberi opciju: ");
        var choice = InputNonEmptyString("Odabir opcije");
        switch (choice)
        {
            case "1":
                PrintAllContactCalls(contact);
                break;
            case "2":
                AddNewContactCall();
                break;
            case "3":
                loopState = Loop.BREAK;
                Console.WriteLine("Izlaz...");
                break;
            default:
                Console.WriteLine("Nepostojeća opcija, pokušaj ponovo...");
                loopState = Loop.CONTINUE;
                break;
        }
        Console.Clear();
    } while (loopState == Loop.CONTINUE);
}

void PrintAllContactCalls(Contact contact)
{
    Console.WriteLine(
            $"\nPOZIVI KONTAKTA {contact.nameAndSurname}:\n" +
            "****************************\n"
    );
    var sortedByDateCalls = contacts[contact].OrderBy(x => x.timeOfCall);
    var calls = sortedByDateCalls;
    foreach (var call in calls)
    {
        Console.WriteLine(call.ToString());
    }
    ContinueAndClearConsole();
}

void AddNewContactCall()
{

}
void ContinueAndClearConsole()
{
    Console.WriteLine("\nPritisni neku tipku za nastavak...");
    var c = Console.ReadKey();
    Console.Clear();
}

Contact FindContact()
{
    Contact? contact;
    bool success;
    do{

        Console.WriteLine("Unesi broj mobitela: ");
        var phoneNumber = InputNonEmptyString("Broj mobitela");
        contact = contacts.Keys.FirstOrDefault(x => x.phoneNumber == phoneNumber);
        success = contact != null;
        if (!success)
        {
            Console.WriteLine("Kontakt sa tim brojem ne postoji, pokušaj ponovo\n");
            ContinueAndClearConsole();
        }
        
    }while(!success);
    return contact;
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
        "\nOdaberi opciju:\n" +
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




