using KajensCykeluthyrning;

RentalSystem system = new RentalSystem();

while (true)
{
    Console.WriteLine("---- Kajens Cykeluthyrning ----");
    Console.WriteLine("1. Visa cyklar");
    Console.WriteLine("2. Registrera kund");
    Console.WriteLine("3. Visa kunder");
    Console.WriteLine("4. Hyr cykel");
    Console.WriteLine("5. Lämna tillbaka cykel");
    Console.WriteLine("6. Ladda elcykel");
    Console.WriteLine("7. Avsluta");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        system.ShowVehicles();
    }

    else if (choice == "2")
    {
        system.AddCustomer();
    }

    else if (choice == "3")
    {
        system.ShowCustomers();
    }

    else if (choice == "4")
    {
        system.RentBike();
    }

    else if (choice == "5")
    {
        system.ReturnBike();
    }
    else if (choice == "6")
    {
        system.ChargeBike();
    }
    else if (choice == "7")
    {
        break;
    }
    else
    {
        Console.WriteLine("Ogiltigt alternativ");
    }
}

