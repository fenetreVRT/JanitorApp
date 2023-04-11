
using JanitorApp.Models;
using Microsoft.IdentityModel.Tokens;

namespace JanitorApp.Services;


internal class MenuService
{
    private readonly UserService _userService = new();
    private readonly CaseService _caseService = new();

    public async Task CreateNewCase()
    {

        var userRegistration = new UserRegistration();
        Console.WriteLine("**** KUNDUPPGIFTER ****");

        Console.Write("E-postadress: ");
        userRegistration.Email = Console.ReadLine()?.Trim() ?? "";
        var userEntity = await _userService.GetByEmailAsync(userRegistration.Email);

        if (userEntity == null)
        {
            Console.Write("Förnamn: ");
            userRegistration.FirstName = Console.ReadLine() ?? "";

            Console.Write("Efternamn: ");
            userRegistration.LastName = Console.ReadLine() ?? "";

            Console.Write("Telefonnummer: ");
            userRegistration.PhoneNumber = Console.ReadLine() ?? "";

            userEntity = await _userService.SaveAsync(userRegistration);
            if (userEntity == null)
            {
                Console.WriteLine("Kunde inte spara användare i databasen");
                return;
            }
        }
        Console.WriteLine($"Välkommen {userEntity.FirstName}!");

        var caseRegistration = new CaseRegistration();
        caseRegistration.UserId = userEntity.Id;

        Console.WriteLine("**** ÄRENDEUPPGIFTER ****");
        Console.Write("Rubrik: ");
        caseRegistration.Title = Console.ReadLine() ?? "";

        Console.Write("Beskrivning: ");
        caseRegistration.Description = Console.ReadLine() ?? "";

        var caseId = await _caseService.SaveAsync(caseRegistration);


        Console.WriteLine($"Nytt ärende med nummer {caseId} har skapats för användare {userEntity.FirstName} {userEntity.Email} !");
    }

    public async Task ListAllCases()
    {
        Console.WriteLine("**** ALLA ÄRENDEN ****");
        var cases = await _caseService.GetAllAsync();
        if (cases.IsNullOrEmpty())
        {
            Console.WriteLine("Inga ärenden inlagda.");
            return;
        }
        foreach (var caseEntity in cases)
        {
            Console.WriteLine($"Ärende {caseEntity.Id} - - {caseEntity.Title} - - {caseEntity.Status.Name} ({caseEntity.User.Email})");

        }
    }

    public async Task ListSpecificCase()
    {
        Console.WriteLine("**** VÄLJ ETT SPECIFIKT ÄRENDE ****");
        await ListAllCases();
        Console.Write("Ange ärendenummer: ");

        if (!int.TryParse(Console.ReadLine(), out var caseId))
        {
            Console.WriteLine("Ogiltigt ärendenummer!");
            return;
        }

        var caseEntity = await _caseService.GetAsync(caseId);
        if (caseEntity == null)
        {
            Console.WriteLine("Finns inget ärende med det numret.");
            return;
        }

        Console.WriteLine($"Epost: {caseEntity.User.Email}\nFörnamn: {caseEntity.User.FirstName}\nEfternamn: {caseEntity.User.LastName}\nTelefonnummer: {caseEntity.User.PhoneNumber}\nÄrende: {caseEntity.Title}\nÄrendebeskrivning: {caseEntity.Description}\nStatus: {caseEntity.Status.Name}");
    }

    public async Task UpdateCaseStatus()
    {
        Console.WriteLine("**** UPPDATERA ÄRENDE ****");
        await ListAllCases();
        Console.Write("Ange ärendenummer: ");

        if (!int.TryParse(Console.ReadLine(), out var caseId))
        {
            Console.WriteLine("Ogiltigt ärendenummer!");
            return;
        }


        if (!await _caseService.Exists(caseId))
        {
            Console.WriteLine("Finns inget ärende med det numret.");
            return;
        }

        Console.Write("Ange ny status, välj ett av följande alternativ\n");
        Console.WriteLine("1: Ej påbörjad");
        Console.WriteLine("2: Påbörjad");
        Console.WriteLine("3: Avslutad"); ;

        if (!int.TryParse(Console.ReadLine(), out var statusId))
        {
            Console.WriteLine("Ogiltigt statusnummer!");
            return;
        }

        var caseEntity = await _caseService.UpdateAsync(caseId, statusId);
        if (caseEntity == null)
        {
            Console.WriteLine("Ärendetatus kunde inte uppdateras.");
            return;
        }
        Console.WriteLine($"Ärende nummer {caseEntity.Id} uppdateades!\nNy status: {caseEntity.Status.Name}");
    }

    public async Task MainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** ÄRENDEHANTERARE ****");
            Console.WriteLine("1. Skapa ett nytt ärende");
            Console.WriteLine("2. Visa alla ärenden");
            Console.WriteLine("3. Visa ett specifikt ärende");
            Console.WriteLine("4. Uppdatera ärendestatus");
            Console.WriteLine("5. Avsluta ärendehanteraren");
            Console.Write("Välj ett av följande alternativ (1-5): ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    await CreateNewCase();
                    break;

                case "2":
                    Console.Clear();
                    await ListAllCases();
                    break;

                case "3":
                    Console.Clear();
                    await ListSpecificCase();
                    break;

                case "4":
                    Console.Clear();
                    await UpdateCaseStatus();
                    break;
                case "5":
                    Console.Clear();
                    return;
            }

            Console.WriteLine("\nTryck på valfri knapp för att fortsätta...");
            Console.ReadKey();
        }
    }


}
