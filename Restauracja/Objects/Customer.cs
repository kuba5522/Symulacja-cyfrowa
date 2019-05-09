using System;
namespace Restauracja
{
    public class Customer
    {
        private static readonly Random Random = new Random();
        private const double Probability1 = 0.10;
        private const double Probability2 = 0.30;
        private const double Probability3 = 0.35;
        private const double Probability4 = 0.25;
        public Customer()
        {
            GroupSize = QuantityOfGroup();
            Id = Guid.NewGuid();
            Meal = false;
            Choice = Random.Next(0,2)>0;
        }

        private static int QuantityOfGroup()
        {
            var Next = Random.NextDouble();
            if (Next < Probability1) return 1;
            if (Next > Probability1 && Next < Probability2 + Probability1) return 2;
            if (Next > Probability1 + Probability2 && Next < Probability1 + Probability2 + Probability3) return 3;
            return 4;
        }

        public Guid Id{get;}
        public bool Choice { get; set; }
        public bool Meal { get; set; }
        public int GroupSize { get;}
        public int Seats { set; get; }
    }
}
