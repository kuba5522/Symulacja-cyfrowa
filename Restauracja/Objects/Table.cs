namespace Restauracja
{
    public class Table
    {
        public Table()
        {
            Customer = null;
        }
        public int NumberOfSeats { get; set; }
        public Customer Customer { get; set; }
    }
}
