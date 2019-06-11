
namespace Restauracja
{
    public class Buffet
    {
        public int NumberOfBusyPlace;
        public Buffet()
        {
            NumberOfBusyPlace = 0;
            Customer = null;
        }
        public Customer Customer { get; set; }
    }
}
