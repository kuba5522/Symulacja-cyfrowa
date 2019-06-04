using System;

namespace Restauracja
{
    internal class Time
    {
        private readonly double Rand1;
        private readonly double Rand2;

        public Time(double rand)
        {
            Rand1 = rand;
        }

        public Time(double rand, double rand2)
        {
            Rand1 = rand;
            Rand2 = rand2;
        }
        public int GaussianDistribution(int average, int variance)
        {

            var U1 = 1.0 - Rand1;
            var U2 = 1.0 - Rand2;
            var RandStdNormal = Math.Sqrt(-2.0 * Math.Log(U1)) * Math.Sin(2.0 * Math.PI * U2);
            var RandNormal = average + variance * RandStdNormal;
            return (int) RandNormal;
        }

        public int ExponentialDistribution(int mean)
        {
            return Convert.ToInt32(-mean * Math.Log((1-Rand1), Math.E));
        }

        public int UniformDistribution(int a, int b)
        {
            double x = Rand1;
            return Convert.ToInt32(b * x + (1 - x) * a);
        }
    }
}
