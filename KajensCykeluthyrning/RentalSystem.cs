using System.Security.Cryptography.X509Certificates;

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
            ElectricBike electricBike = (ElectricBike)vehicles[2];
            electricBike.ConsumeBattery(8);
        }
        
        public void ShowVehicles()
        {
            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.DisplayInfo();
            }
        }

        public void AddCustomer()
        {
            Console.WriteLine("Ange ditt namn:");
            string name = Console.ReadLine();
            
            Console.WriteLine("Ange din ålder:");
            int age = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Är du medlem? (Ja/Nej)"); 
            string memberAnswer = Console.ReadLine(); 

            bool isMember = memberAnswer == "Ja"; // Resulterar i dagsläget att man måste skriva uppercase Ja för att bli medlem. 

            Customer customer = new Customer(name, age, isMember);

            customers.Add(customer);
            Console.WriteLine("Kunden har registrerats!");
        }

        public void ShowCustomers()
        {
            foreach (Customer customer in customers)
            {
                Console.WriteLine($"Namn:{customer.Name} Ålder:{customer.Age} Medlemskap:{customer.IsMember}");
            }
        }

        public void RentBike()
        {
            ShowVehicles();
            Console.WriteLine("Ange serienumret på cykeln du vill hyra:");
            int serialNumber = Convert.ToInt32(Console.ReadLine());

            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.SerialNumber == serialNumber)
                {
                    if (vehicle.IsRented == true)
                    {
                        Console.WriteLine("Cykeln är redan uthyrd");
                        return;
                    }

                    ShowCustomers();
                    Console.WriteLine("Ange namnet på kunden som vill hyra:");
                    string customerName = Console.ReadLine();

                    foreach (Customer customer in customers)
                    {
                        if (customer.Name == customerName)
                        {
                            if (customer.Age >= vehicle.MinimumAge)
                            {
                                Console.WriteLine("Hur länge vill du hyra cykeln?");
                                decimal hours = Convert.ToDecimal(Console.ReadLine());

                                Rental rental = new Rental(hours, customer, vehicle);
                                rental.RentVehicle();
                                if (vehicle is ElectricBike electricBike)
                                {
                                    electricBike.ConsumeBattery(hours);
                                    electricBike.DisplayInfo();
                                }
                                rentals.Add(rental);
                                decimal price = vehicle.CalculatePrice(hours);
                                decimal discount = rental.CalculateDiscount();
                                decimal finalPrice = price - discount;
                                Console.WriteLine("Cykeln har hyrts ut!");
                                Console.WriteLine($"Totalkostnad: {finalPrice:F2} kr");
                                return;
                            }
                            else if (customer.Age < vehicle.MinimumAge)
                            {
                                Console.WriteLine("Du är för ung");
                                return;
                            }
                        }
                    }
                }
            }
        }
            public void ReturnBike()
            {
            ShowVehicles();
            Console.WriteLine("Ange serienumret på cykeln du vill lämna tillbaka:");
            int serialnumber = Convert.ToInt32(Console.ReadLine());

            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.SerialNumber == serialnumber)
                {
                    if (vehicle.IsRented == true)
                    {
                        foreach (Rental rental in rentals)
                        {
                            if (rental.Vehicle.SerialNumber == serialnumber)
                            {
                                rental.ReturnVehicle();
                                Console.WriteLine("Cykeln har lämnats tillbaka");
                                return;
                            }
                        }
                    }
                }
            }
            }
    }
}
