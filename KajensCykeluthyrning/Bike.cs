namespace KajensCykeluthyrning
{
    public class Bike : Vehicle
    {
        public Bike(int serialNumber) : base(serialNumber, 40m, 15) // Base anropar basklassens (Vehicle) konstruktor
        {

        }  
    }
}
