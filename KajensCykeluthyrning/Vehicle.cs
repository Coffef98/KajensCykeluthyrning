namespace KajensCykeluthyrning
{
    public class Vehicle
    {
        public int SerialNumber { get; set; }
        public decimal HourlyRate { get; set; }
        public bool IsRented { get; set; }
        public int MinimumAge { get; set; }

        public Vehicle(int serialNumber, decimal hourlyRate, int minimumAge)
        {
            SerialNumber = serialNumber;
            HourlyRate = hourlyRate;
            IsRented = false;
            MinimumAge = minimumAge;
        }
        public decimal CalculatePrice(decimal hours)
        {
            return HourlyRate * hours;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Serienummer: {SerialNumber}");
            Console.WriteLine($"Timpris: {HourlyRate} kr");
            Console.WriteLine($"Status: {(IsRented ? "Uthyrd" : "Tillgänglig")}");

        }
    }
}
