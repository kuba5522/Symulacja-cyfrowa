namespace Restauracja
{
    public class Table
    {
        public Table()
        {
            Customer = null;
            Free = true;
        }
        public int NumberOfSeats { get; set; }
        public bool Free { get; set; }
        public Customer Customer { get; set; }
    }
}
