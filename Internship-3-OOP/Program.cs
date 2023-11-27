// See https://aka.ms/new-console-template for more information
using Internship_3_OOP.Classes;
using Internship_3_OOP.Enums;

const int MAX_PHONE_NUM_LENGTH = 15;

Dictionary<Contact, List<Call>> contacts = new()
{
    { 
        new Contact("Ivan Ivic", "123456789", Preferences.FAVOURITE), 
        new List<Call>() { 
            new Call(new DateTime(2023,11,23,23,2,5), CallStatus.INCOMING), 
            new Call(new DateTime(2023, 11, 26, 16, 15, 4), CallStatus.OUTGOING) 
        } 
    },
    { 
        new Contact("Marko Mlakic", "987654321", Preferences.REGULAR), 
        new List<Call>() { 
            new Call(new DateTime(2023, 11, 11, 13, 2, 4), CallStatus.MISSED), 
            new Call(new DateTime(2023, 11, 15, 14, 21, 15), CallStatus.INCOMING) 
        } 
    },
    { 
        new Contact("Sime Simic", "121212121", Preferences.BLOCKED), 
        new List<Call>() { 
            new Call(new DateTime(2023, 10, 20, 11, 2, 4), CallStatus.OUTGOING), 
            new Call(new DateTime(2023, 9, 17, 11, 5, 4), CallStatus.MISSED) 
        } 
    },
};

List<Call> allCalls = new();
foreach (var (contact, calls) in contacts)
{
    foreach (var call in calls)
    {
        allCalls.Add(call);
    }
}

Loop loopState = Loop.CONTINUE;
do{
    Console.WriteLine(
        "MENU:\n" +
        "1 - Ispis svih kontakata\n" +
        "2 - Dodavanje novih kontakata u imenik\n" +
        "3 - Brisanje kontakata iz imenika\n" +
        "4 - Editiranje preference kontakta\n" +
        "5 - Upravljanje kontaktom\n" +
        "6 - Ispis svih poziva\n" +
        "7 - IZLAZ"
    );
    Console.WriteLine("Odaberi opciju: ");
    var choice = InputNonEmptyString("Odabir opcije");
    switch (choice)
    {
        case "1":
            PrintAllContacts();
            ContinueAndClearConsole();
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
            ContactManagmentSubmenu();
            break;
        case "6":
            PrintAllCalls();
            break;
        case "7":
            loopState = Loop.BREAK;
            Console.WriteLine("Izlaz...");
            break;
        default:
            Console.WriteLine("Nepostojeća opcija, pokušaj ponovo...");
            ContinueAndClearConsole();
            break;
    }
    Console.Clear();
} while(loopState == Loop.CONTINUE);

void PrintAllContacts()
{
    Console.Clear();
    Console.WriteLine(
        $"\nKONTAKTI (Count - {contacts.Count}):\n" +
        "****************************\n");
    foreach (var (contact, calls) in contacts)
    {
        Console.WriteLine(contact.ToString());
    }
}
void AddNewContact()
{
    Loop loopState = Loop.CONTINUE;
    bool success;
    string phoneNumber;

    Console.Clear();
    do{
        Console.WriteLine(
               "\nNOVI KONTAKT:\n" +
               "****************************\n"
        );
        Console.WriteLine("Unesi ime i prezime (format: Ime Prezime): ");
        var nameAndSurname = InputNonEmptyString("Ime i prezime");

        do{
            Console.WriteLine("Unesi broj mobitela: ");
            phoneNumber = InputPhoneNumber();
            success = !contacts.Keys.Any(x => x.phoneNumber == phoneNumber);
            if (!success)
                Console.WriteLine("Kontakt sa tim brojem već postoji, pokušaj ponovo\n");
        }while (!success);
       
        Console.WriteLine("Unesi preferencu: ");
        var preference = InputPreferenceForContact();
        var contact = new Contact(nameAndSurname, phoneNumber, preference);

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
    Console.Clear();

    do{
        PrintAllContacts();
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
        PrintAllContacts();
        Console.WriteLine(
                "\nEDITIRANJE KONTAKTA:\n" +
                "****************************\n"
        );
        var contact = FindContact();
        
        Console.WriteLine(contact.ToString());
        Console.WriteLine("Uredi preferencu? (da - nastavak):");
        if (ConfirmationDialog() == 0)
        {
            var preference = InputPreferenceForContact();
            contact.EditPreferece(preference);
        }
        Console.WriteLine("Zelis li nastavit sa uređivanjem kontakata? (da - za nastavak)");
        loopState = ConfirmationDialog();
        Console.Clear();
    } while (loopState == Loop.CONTINUE);
}

void ContactManagmentSubmenu()
{
    PrintAllContacts();
    var contact = FindContact();
    Console.Clear();
    
    Loop loopState = Loop.CONTINUE;
    do{
        Console.WriteLine(contact.ToString());
        Console.WriteLine(
            "SUBMENU:\n" +
            "1 - Ispis svih poziva\n" +
            "2 - Novi poziv\n" +
            "3 - Izlaz\n"
        );
        Console.WriteLine("Odaberi opciju: ");
        
        var choice = InputNonEmptyString("Odabir opcije");
        switch (choice)
        {
            case "1":
                PrintAllContactCalls(contact);
                break;
            case "2":
                CreateNewContactCall(contact);
                break;
            case "3":
                loopState = Loop.BREAK;
                Console.WriteLine("Izlaz...");
                break;
            default:
                Console.WriteLine("Nepostojeća opcija, pokušaj ponovo...");
                ContinueAndClearConsole();
                break;
        }
        Console.Clear();
    } while (loopState == Loop.CONTINUE);
}

void PrintAllContactCalls(Contact contact)
{
    Console.Clear();
    Console.WriteLine(
            $"\nPOZIVI KONTAKTA {contact.nameAndSurname}:\n" +
            "****************************\n"
    );
    var calls = contacts[contact];
    var callsSortedByDate = calls.OrderBy(call => call.GetTime());
    
    foreach (var call in callsSortedByDate)
    {
        Console.WriteLine(call.ToString());
    }
    ContinueAndClearConsole();
}

void PrintAllCalls()
{
    Console.WriteLine(
           $"\nSVI POZIVI:\n" +
           "****************************\n"
    );
    allCalls.Sort((x, y) => DateTime.Compare(y.GetTime(), x.GetTime()));
    foreach (var call in allCalls)
    {
        Console.WriteLine(call.ToString());
    }
    ContinueAndClearConsole();
}

void CreateNewContactCall(Contact contact)
{
    if(contact.GetPreference() == Preferences.BLOCKED)
    {
        Console.WriteLine("Kontakt je blokiran, ne možeš napraviti poziv!\n");
        ContinueAndClearConsole();
        return;
    }

    var random = new Random();
    var callStatus = (CallStatus)random.Next(0, 3);
    var callDuration = random.Next(1, 20);

    for (int seconds = 0; seconds <= callDuration; seconds++)
    {
        Console.Clear();
        Console.WriteLine($"Poziv u tijeku: {seconds}s \n");
        Thread.Sleep(1000);
    }
    Console.Clear();
    Console.WriteLine(
        "POZIV ZAVRŠEN!\n" +
        "***********************\n" +
        $"TRAJANJE: {callDuration}s\n" +
        $"STATUS: {callStatus}\n"
    );
    var newCall = new Call(callStatus);
    contacts[contact].Add(newCall);
    allCalls.Add(newCall);
    ContinueAndClearConsole();
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
        var phoneNumber = InputPhoneNumber();
        contact = contacts.Keys.FirstOrDefault(x => x.phoneNumber == phoneNumber);
        success = contact != null;
        if (!success)
        {
            Console.WriteLine("Kontakt sa tim brojem ne postoji, pokušaj ponovo\n");
            ContinueAndClearConsole();
        }
        
    }while(!success);
    return contact!;
}
string InputNonEmptyString(string message = "unos")
{
    string input;
    do{
        input = Console.ReadLine()!;
        if(input == "")
            Console.WriteLine(message + " nemože biti prazan string, pokušaj ponovo\n");
        
    } while (input == "");
    return input;
}

string InputPhoneNumber()
{
    string phoneNum = "";
    bool success;
    do{
        phoneNum = InputNonEmptyString();
        success = long.TryParse(phoneNum, out long input);
        if (!success)
        {
            Console.WriteLine("Broj mobitela mora sadržavat samo znamenke, pokušaj ponovo\n");
            continue;
        }
   
        success = phoneNum.Length <= MAX_PHONE_NUM_LENGTH;
        if (!success)
            Console.WriteLine($"Broj mobitela može imat najviše {MAX_PHONE_NUM_LENGTH} znamenki, pokušaj ponovo\n");
    } while (!success);
    return phoneNum;
}

Loop ConfirmationDialog()
{
    var option = InputNonEmptyString();

    if (option.ToLower() == "da")
        return Loop.CONTINUE;

    return Loop.BREAK;
}

Loop ConfirmationDialogForDataChange()
{
    var option = InputNonEmptyString();

    if (option.ToLower() == "da")
        return Loop.BREAK;
    
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




