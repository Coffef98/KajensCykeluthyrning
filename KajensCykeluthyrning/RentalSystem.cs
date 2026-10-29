namespace KajensCykeluthyrning
{
    public class RentalSystem
    {

        public List<Vehicle> vehicles = new List<Vehicle>();

        public List<Customer> customers = new List<Customer>();

        public List<Rental> rentals = new List<Rental>();

        public RentalSystem()
        {
            vehicles.Add(new Bike(1));
            vehicles.Add(new Bike(2));

            vehicles.Add(new ElectricBike(3));
            vehicles.Add(new ElectricBike(4));
        }

        public void ShowVehicles()
        {
            Console.WriteLine("\n---- Cykelöversikt ----");

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void AddCustomer()
        {
            Console.WriteLine("\n---- Kundregistrering ----");
            Console.WriteLine("Ange ditt namn:");
            string name = Console.ReadLine();

            foreach (Customer existingCustomer in customers)
            {
                if (existingCustomer.Name == name)
                {
                    Console.WriteLine("Kunden är redan registrerad");
                    return;
                }
            }

            Console.WriteLine("Ange din ålder:");
            bool validAge = int.TryParse(Console.ReadLine(), out int age);
            if (validAge == false || age < 0)
            {
                Console.WriteLine("Ogiltig ålder, ange ett nummer!");
                return;
            }

            Console.WriteLine("Är du medlem? (Ja/Nej)");
            string memberAnswer = Console.ReadLine();

            bool isMember = memberAnswer.ToLower() == "ja";

            Customer customer = new Customer(name, age, isMember);

            customers.Add(customer);
            Console.WriteLine("Kunden har registrerats!");
        }

        public void ShowCustomers()
        {
            Console.WriteLine("\n---- Kundöversikt ----");
            foreach (Customer customer in customers)
            {
                Console.WriteLine($"Namn: {customer.Name}");
                Console.WriteLine($"Ålder: {customer.Age}");
                Console.WriteLine($"Medlemskap: {(customer.IsMember ? "Ja" : "Nej")}");
                Console.WriteLine();
            }
        }

        public void RentBike()
        {
            ShowVehicles();
            Console.WriteLine("Ange serienumret på cykeln du vill hyra:");
            int serialNumber = Convert.ToInt32(Console.ReadLine());
            bool vehicleFound = false;
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.SerialNumber == serialNumber)
                {
                    vehicleFound = true;
                    if (vehicle.IsRented == true)
                    {
                        Console.WriteLine("Cykeln är redan uthyrd");
                        return;
                    }

                    if (vehicle is ElectricBike electricBike)
                    {
                        if (electricBike.BatteryLevel < 30)
                        {
                            Console.WriteLine("För lågt batteri, ladda först!");
                            return;
                        }
                    }

                    ShowCustomers();
                    Console.WriteLine("Ange namnet på kunden som vill hyra:");
                    string customerName = Console.ReadLine();
                    bool customerFound = false;

                    foreach (Customer customer in customers)
                    {
                        if (customer.Name == customerName)
                        {
                            customerFound = true;
                            if (customer.Age >= vehicle.MinimumAge)
                            {
                                Console.WriteLine("Ange hyrestid i timmar. (t.ex 1,5 eller 2)");
                                bool validHours = decimal.TryParse(Console.ReadLine(), out decimal hours);
                                if (validHours == false || hours <= 0)
                                {
                                    Console.WriteLine("Ogiltig tidspann");
                                    return;
                                }

                                Rental rental = new Rental(hours, customer, vehicle);
                                rental.RentVehicle();
                                if (vehicle is ElectricBike bike)
                                {
                                    bike.ConsumeBattery(hours);
                                    bike.DisplayInfo();
                                }
                                rentals.Add(rental);
                                decimal price = vehicle.CalculatePrice(hours);
                                decimal discount = rental.CalculateDiscount();
                                decimal finalPrice = price - discount;
                                Console.WriteLine("\n---- Uthyrningsbekräftelse ----");
                                Console.WriteLine($"Kundnamn: {customer.Name}");
                                Console.WriteLine($"Cykel: {serialNumber} har hyrts ut");
                                Console.WriteLine($"Hyrestid: {rental.TimeRented} timmar");
                                Console.WriteLine($"Rabatt: {discount:F2} kr");
                                Console.WriteLine($"Slutpris: {finalPrice:F2} kr");
                                Console.WriteLine();
                                return;
                            }
                            else if (customer.Age < vehicle.MinimumAge)
                            {
                                Console.WriteLine("Du är för ung");
                                return;
                            }
                        }
                    }
                    if (customerFound == false)
                    {
                        Console.WriteLine("Kunden finns inte registrerad i systemet.");
                        return;
                    }
                }
            }
            if (vehicleFound == false)
            {
                Console.WriteLine("Finns ej cykel i systemet med detta serienummer.");
            }
        }
        public void ReturnBike()
        {
            ShowVehicles();
            Console.WriteLine("Ange serienumret på cykeln du vill lämna tillbaka:");
            int serialnumber = Convert.ToInt32(Console.ReadLine());
            bool vehicleFound = false;
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.SerialNumber == serialnumber)
                {
                    vehicleFound = true;

                    if (vehicle.IsRented == true)
                    {
                        foreach (Rental rental in rentals.AsEnumerable().Reverse())
                        {
                            if (rental.Vehicle.SerialNumber == serialnumber)
                            {
                                rental.ReturnVehicle();

                                decimal price = vehicle.CalculatePrice(rental.TimeRented);
                                decimal discount = rental.CalculateDiscount();
                                decimal finalprice = price - discount;

                                Console.WriteLine("\n---- Kvitto ----");
                                Console.WriteLine($"Kundnamn: {rental.Customer.Name}");
                                Console.WriteLine($"Cykel: {serialnumber} har återlämnats");
                                Console.WriteLine($"Hyrestid: {rental.TimeRented} timmar");
                                Console.WriteLine($"Timpris: {vehicle.HourlyRate:F2} kr");
                                Console.WriteLine($"Ordinarie pris: {price:F2} kr");
                                Console.WriteLine($"Medlemsrabatt: -{discount:F2} kr");
                                Console.WriteLine($"Att betala: {finalprice:F2} kr");
                                Console.WriteLine();
                                return;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cykeln är inte uthyrd.");
                        return;
                    }
                }
            }
            if (vehicleFound == false)
            {
                Console.WriteLine("Finns ej cykel i systemet med detta serienummer.");
            }
        }
        public void ChargeBike()
        {
            ShowVehicles();

            Console.WriteLine("Ange serienumret på cykeln du vill ladda:");
            bool validNumber = int.TryParse(Console.ReadLine(), out int serialNumber);

            if (validNumber == false)
            {
                Console.WriteLine("Ogiltigt serienummer");
                return;
            }

            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.SerialNumber == serialNumber)
                {
                    if (vehicle is ElectricBike electricBike)
                    {
                        if (vehicle.IsRented)
                        {
                            Console.WriteLine("Du kan inte ladda en uthyrd elcykel.");
                            return;
                        }

                        electricBike.Charge();

                        Console.WriteLine($"Elcykel: {serialNumber} har laddats till {electricBike.BatteryLevel}%.");
                        return;
                    }

                    Console.WriteLine("Denna cykel har inget batteri");
                    return;
                }
            }
            Console.WriteLine("Ingen cykel hittades med detta serienummer");
        }
    }
}