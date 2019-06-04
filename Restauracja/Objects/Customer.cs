using System;
namespace Restauracja
{
    public class Customer
    {
        private const double Probability1 = 0.10;
        private const double Probability2 = 0.35;
        private const double Probability3 = 0.35;
        private const double Probability4 = 0.20;
        private static double Rand2;
        public Customer(double rand1, double rand2)
        {
            Rand2 = rand2;
            GroupSize = QuantityOfGroup();
            Id = Guid.NewGuid();
            Meal = false;
            Choice = Convert.ToBoolean(new Time(rand1).UniformDistribution(0,1));
        }

        private static int QuantityOfGroup()
        {
            var Next = Rand2;
            if (Next < Probability1) return 1;
            if (Next > Probability1 && Next < Probability2 + Probability1) return 2;
            if (Next > Probability1 + Probability2 && Next < Probability1 + Probability2 + Probability3) return 3;
            return 4;
        }

        public Guid Id { get; set; }
        public bool Choice { get; set; }
        public bool Meal { get; set; }
        public int GroupSize { get;}
    }
}
