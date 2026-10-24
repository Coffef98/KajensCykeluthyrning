using KajensCykeluthyrning;

Bike bikeOne = new Bike(1337);

bikeOne.DisplayInfo();

ElectricBike eBikeOne = new ElectricBike(1);

eBikeOne.ConsumeBattery(3);
eBikeOne.Charge();
eBikeOne.DisplayInfo();


Customer customerOne = new Customer("Christoffer", 28, true);
Console.WriteLine($"Namn: {customerOne.Name}");
Console.WriteLine($"Ålder: {customerOne.Age}");
Console.WriteLine($"Medlem: {customerOne.IsMember}");

Rental rentalOne = new Rental(1.3m, customerOne, eBikeOne);

rentalOne.RentVehicle();
eBikeOne.DisplayInfo();

rentalOne.ReturnVehicle();
eBikeOne.DisplayInfo();

Console.WriteLine($"{eBikeOne.CalculatePrice(rentalOne.TimeRented):F2} kr");

Console.WriteLine($"{rentalOne.CalculateDiscount():F2}");

Console.WriteLine($"{eBikeOne.CalculatePrice(rentalOne.TimeRented) - rentalOne.CalculateDiscount():F2} kr");