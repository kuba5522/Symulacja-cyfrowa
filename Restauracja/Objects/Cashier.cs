using System;
namespace Restauracja
{
    public class Cashier
    {
        public Cashier()
        {
            Customer = null;
        }
        public Customer Customer { get; set; }
    }
}
