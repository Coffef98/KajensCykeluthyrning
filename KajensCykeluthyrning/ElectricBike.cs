namespace KajensCykeluthyrning
{
    public class ElectricBike : Vehicle
    {
        public decimal BatteryLevel { get; set; }

        public ElectricBike(int serialNumber) : base(serialNumber, 70m, 18)
        {
            BatteryLevel = 100m;
        }
        public void ConsumeBattery(decimal timeRented)
        {
            BatteryLevel = BatteryLevel - timeRented * 10;
            
            if (BatteryLevel < 0)
            {
                BatteryLevel = 0m;
            }
        }
        public void Charge()
        {
            BatteryLevel = 100;
        }
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Batterinivå: {BatteryLevel} %");
        }
    }
}
