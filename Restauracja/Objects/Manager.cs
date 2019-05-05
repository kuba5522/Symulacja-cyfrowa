using System;
namespace Restauracja
{
    public class Manager
    {
        public Manager()
        {
            Customer = null;
            Free = true;
        }
        public bool Free { get; set; }
        public Customer Customer { get; set; }
    }
}
