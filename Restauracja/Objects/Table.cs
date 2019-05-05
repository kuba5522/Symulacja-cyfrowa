namespace Restauracja
{
    public class Table
    {
        public Table()
        {
            Free = true;
        }
        public int NumberOfSeats { get; set; }
        public bool Free { get; set; }
    }
}
