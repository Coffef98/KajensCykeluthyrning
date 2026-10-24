namespace KajensCykeluthyrning
{
    public class Rental
    {
        public decimal TimeRented { get; set; }
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }

        public Rental(decimal timeRented, Customer customer, Vehicle vehicle)
        {
            TimeRented = timeRented;
            Customer = customer;
            Vehicle = vehicle;

        }

        public bool RentVehicle()
        {
            if (Vehicle.IsRented == true)
            {
                return false;
            }
            else
            {
                Vehicle.IsRented = true;
                return true;
            }
        }
        public bool ReturnVehicle()
        {
            if (Vehicle.IsRented == false)
            {
                return false;
            }
            else
            {
                Vehicle.IsRented = false;
                return true;
            }
        }
        public decimal CalculateDiscount()
        {
            if (Customer.IsMember == true)
            {
                return Vehicle.CalculatePrice(TimeRented) * 0.1m;
            }
            else
            {
                return 0;
            }
        }


    }
}
